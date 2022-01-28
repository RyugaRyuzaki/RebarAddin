
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

            // code

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
                    if (MessageBox.Show(Error + "\n" + hyperlink, "ERROR", MessageBoxButtons.YesNo, MessageBoxIcon.Error) == DialogResult.Yes)
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
                MessageBox.Show(e.Message);
                return Result.Cancelled;
            }
        }
    }
}
