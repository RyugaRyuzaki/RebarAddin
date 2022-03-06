
#region Namespaces
using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using Autodesk.Revit.UI.Selection;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Windows.Forms;
using Application = Autodesk.Revit.ApplicationServices.Application;
using ThongBao = System.Windows.Forms.MessageBox;
#endregion


namespace R10_WallShear
{
    [Transaction(TransactionMode.Manual)]
    public class WallShearCmd : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData,
            ref string message, ElementSet elements)
        {
            UIApplication uiapp = commandData.Application;
            UIDocument uidoc = uiapp.ActiveUIDocument;
            Application app = uiapp.Application;
            Document doc = uidoc.Document;

            #region


            List<Reference> references = null;
            try
            {
                references = uidoc.Selection.PickObjects(ObjectType.Element, new WallSelectionFilter()).ToList();
                List<Element> walls = new List<Element>();
                foreach (var item in references)
                {
                    walls.Add(doc.GetElement(item));
                }
                walls = walls.OrderBy(x => (SolidFace.GetBottom(x) as PlanarFace).Origin.Z).ToList();
                string Error = ErrorWalls.GetErrorWalls(doc, walls);
                if (!Error.Equals("OK"))
                {
                    string hyperlink = "Do you want to see on Youtube ?";
                    string navigateUri = "https://www.youtube.com/channel/UCQSwGw2vUjad7kUhEOXbqaw";
                    if (ThongBao.Show(Error + "\n" + hyperlink, "ERROR", MessageBoxButtons.YesNo, MessageBoxIcon.Error) == DialogResult.Yes)
                    {
                        Process.Start(new ProcessStartInfo(navigateUri));
                    }
                    return Result.Cancelled;
                }
                else
                {
                    using (TransactionGroup transGr = new TransactionGroup(doc))
                    {
                        transGr.Start("RAPI00TransGr");

                        WallShearViewModel viewModel = new WallShearViewModel(uidoc, doc, walls);
                        WallShearWindow window = new WallShearWindow(viewModel);
                        if (window.ShowDialog() == false) return Result.Cancelled;

                        transGr.Assimilate();
                        return Result.Succeeded;
                    }
                }

            }
            catch (Exception e)
            {
                ThongBao.Show(e.Message);
                return Result.Cancelled;
            }
            #endregion
            //List<List<Element>> AllListElements = GetAllListElement(doc);
            //for (int i = 0; i < AllListElements.Count; i++)
            //{
            //    ThongBao.Show(AllListElements[i].Count + "");
            //}
            //return Result.Succeeded;
        }
        private List<Element> GetAllElement(Document document)
        {
            return new FilteredElementCollector(document).OfCategory(BuiltInCategory.OST_GenericModel).WhereElementIsNotElementType().Where(x => x.LookupParameter("W")!=null).ToList();

        }
        private bool IsNotNullPara(Document document, Element element)
        {
            ElementType elementType = document.GetElement(element.GetTypeId()) as ElementType;
            return elementType.LookupParameter("h") != null;
        }
        private List<List<Element>> GetAllListElement(Document document)
        {
            List<List<Element>> AllListElement = new List<List<Element>>();
            List<Element> AllElements = GetAllElement(document);
            if (AllElements.Count != 0)
            {

                // tạo 1 listtamj bằng với AllElement để cho dữ liệu khỏi bị mất
                List<Element> listTemp = AllElements;
                //lấy 1 biến tổng của các element cần lọc tức là AllElement(listTemp)
                int i = listTemp.Count;
                //bắt đầu chạy ngược nếu mà i==0 thì dừng
                while (i > 0)
                {
                    //tạo list đầu tiên.
                    List<Element> list = new List<Element>();
                    //lấy 1 e0 là Element đầu tiên  của list tạm
                    Element e0 = listTemp[0];
                    //khai báo biến double a0 = e0.LookupParameter("W").AsDouble();
                   
                    double a0 = e0.LookupParameter("W").AsDouble();
                    //ThongBao.Show("Test"+a0);
                    //Bắt đầu chạy 1 vòng lập tìm ra các element nào có b0=a0 bo khai báo trong vòng lập vì không thể so sánh 1 parameter với 1 parameter dc
                    for (int j = 0; j < listTemp.Count; j++)
                    {
                        //khai báo biến double b0 = listTemp[j].LookupParameter("W").AsDouble();
                     
                        double b0 = listTemp[j].LookupParameter("W").AsDouble();
                        //nếu a0=b0 thì mình add list
                        if (AreEqual(b0, a0))
                        {
                            list.Add(listTemp[j]);

                        }
                    }
                    // Remove các element trong listemp từ list
                    for (int k = 0; k < list.Count; k++)
                    {
                        listTemp.Remove(list[k]);
                    }
                    // Add list và AllListElement
                    AllListElement.Add(list);
                    //gán ngược lại biến i vì lúc này số lượng element trong listTemp đã bị thay đổi do đã trừ bớt ra.
                    i = listTemp.Count;
                    // đến khi không còn element nào trong listTemp này tức là i==0 thì vòng white sẽ dừng lại

                }

            }
            return AllListElement;
        }
        public static bool AreEqual(double firstValue, double secondValue, double tolerance = 1.0e-9)
        {
            return (secondValue - tolerance < firstValue && firstValue < secondValue + tolerance);
        }
    }
}
