
#region Namespaces
using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using Application = Autodesk.Revit.ApplicationServices.Application;
#endregion


namespace R09_WallsRebar
{
    [Transaction(TransactionMode.Manual)]
    public class WallsRebarCmd : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData,
            ref string message, ElementSet elements)
        {
            UIApplication uiapp = commandData.Application;
            UIDocument uidoc = uiapp.ActiveUIDocument;
            Application app = uiapp.Application;
            Document doc = uidoc.Document;

            // code



            //using (TransactionGroup transGr = new TransactionGroup(doc))
            //{
            //    transGr.Start("RAPI00TransGr");

            //    WallsViewModel viewModel = new WallsViewModel(uidoc, doc);
            //    WallsWindow window = new WallsWindow(viewModel);
            //    if (window.ShowDialog() == false) return Result.Cancelled;

            //    transGr.Assimilate();
            //}
            MessageBox.Show("This Add-in will be coming soon", "Imformation");
            return Result.Succeeded;
        }
    }
}
