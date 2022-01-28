
#region Namespaces
using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using Autodesk.Revit.UI.Selection;
using System.Collections.Generic;
using Application = Autodesk.Revit.ApplicationServices.Application;
using System.Linq;
using System.Windows.Forms;
using System;
using System.Diagnostics;
#endregion


namespace R01_ColumnsRebar
{
    [Transaction(TransactionMode.Manual)]
    public class ColumnsRebarCmd : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData,
            ref string message, ElementSet elements)
        {
            UIApplication uiapp = commandData.Application;
            UIDocument uidoc = uiapp.ActiveUIDocument;
            Application app = uiapp.Application;
            Document doc = uidoc.Document;

            List<Reference> references = null;
            try
            {
                references = uidoc.Selection.PickObjects(ObjectType.Element, new StructuralColumnSelectionFilter()).ToList();
                List<Element> columns = new List<Element>();
                foreach (var item in references)
                {
                    columns.Add(doc.GetElement(item));
                }
                columns = columns.OrderBy(x => (SolidFace.GetBottom(x) as PlanarFace).Origin.Z).ToList();
                int Error = ErrorColumns.GetErrorColumns(doc, columns);
                if (Error != 0)
                {
                    string hyperlink = "Do you want to see on Youtube ?";
                    string navigateUri = "https://www.youtube.com/channel/UCQSwGw2vUjad7kUhEOXbqaw";
                    if (MessageBox.Show("E " + Error + " : " + ErrorColumns.ErrorString[Error] + "\n" + hyperlink, "ERROR", MessageBoxButtons.YesNo, MessageBoxIcon.Error) == DialogResult.Yes)
                    {
                        Process.Start(new ProcessStartInfo(navigateUri));
                    }
                    return Result.Cancelled;
                }
                else
                {
                    using (TransactionGroup transG = new TransactionGroup(doc))
                    {
                        transG.Start("Columns Rebar");
                        ColumnsViewModel columnsViewModel
                            = new ColumnsViewModel(uidoc, doc, columns);
                        ColumnsWindow window
                            = new ColumnsWindow(columnsViewModel);
                        bool? showDialog = window.ShowDialog();
                        if (showDialog == null || showDialog == false)
                        {
                            transG.RollBack();
                            return Result.Cancelled;
                        }
                        transG.Assimilate();
                        return Result.Succeeded;
                    }
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
                return Result.Cancelled;
            }
            //MessageBox.Show("This Add-in will be coming soon", "Imformation");
            //return Result.Succeeded;
        }
    }
}
