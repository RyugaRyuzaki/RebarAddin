
#region Namespaces
using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using Application = Autodesk.Revit.ApplicationServices.Application;
#endregion


namespace R08_ContinueFootingRebar
{
    [Transaction(TransactionMode.Manual)]
    public class ContinueFoundationCmd : IExternalCommand
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

            //    ContinueFoundationViewModel viewModel = new ContinueFoundationViewModel(uidoc, doc);
            //    ContinueFoundationWindow window = new ContinueFoundationWindow(viewModel);
            //    if (window.ShowDialog() == false) return Result.Cancelled;

            //    transGr.Assimilate();
            //}
            MessageBox.Show("This Add-in will be coming soon", "Imformation");
            return Result.Succeeded;
        }
    }
}
