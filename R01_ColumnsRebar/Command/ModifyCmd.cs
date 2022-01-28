
#region Namespaces
using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using Application = Autodesk.Revit.ApplicationServices.Application;
using Autodesk.Revit.UI.Selection;
using System.Collections.Generic;
using System;
using System.Linq;
#endregion


namespace R01_ColumnsRebar.Command
{
    [Transaction(TransactionMode.Manual)]
    public class ModifyCmd : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData,
            ref string message, ElementSet elements)
        {
            UIApplication uiapp = commandData.Application;
            UIDocument uidoc = uiapp.ActiveUIDocument;
            Application app = uiapp.Application;
            Document doc = uidoc.Document;

            List<Reference> references = uidoc.Selection.PickObjects(ObjectType.Element).ToList();
            List<Element> column = new List<Element>();
            foreach (var item in references)
            {
                column.Add(doc.GetElement(item));
            }
            column = column.OrderBy(x => (GetBottom(x) as PlanarFace).Origin.Z).ToList();
            foreach (var item in column)
            {
                System.Windows.Forms.MessageBox.Show(item.Id+"");
            }
            return Result.Succeeded;
        }
        public static PlanarFace GetBottom(Element element)
        {
            Solid a = SolidFace.GetSolidOneElement(element);
            FaceArray faceArray = a.Faces;
            List<PlanarFace> planarFaces = new List<PlanarFace>();
            foreach (var item in faceArray)
            {
                PlanarFace a1 = item as PlanarFace;
                if (a1 != null)
                {
                    if ((PointModel.AreEqual(a1.FaceNormal.AngleTo(XYZ.BasisZ), Math.PI)) || (PointModel.AreEqual(a1.FaceNormal.AngleTo(XYZ.BasisZ), 0)))
                    {
                        planarFaces.Add(a1);
                    }
                }
            }
            planarFaces = planarFaces.OrderBy(x => x.Origin.Z).ToList();
            PlanarFace bottom = planarFaces[0];
            return bottom;
        }
        public static bool Compare(PlanarFace a, PlanarFace b)
        {
            return a.Origin.Z > b.Origin.Z;
        }
    }
}
