
#region Namespaces
using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using Application = Autodesk.Revit.ApplicationServices.Application;
#endregion


namespace R06_StairRebar
{
    [Transaction(TransactionMode.Manual)]
    public class StairRebarCmd : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData,
            ref string message, ElementSet elements)
        {
            UIApplication uiapp = commandData.Application;
            UIDocument uidoc = uiapp.ActiveUIDocument;
            Application app = uiapp.Application;
            Document doc = uidoc.Document;

            // code



           
            using (TransactionGroup transGr = new TransactionGroup(doc))
            {
                transGr.Start("RAPI00TransGr");

                StairViewModel viewModel = new StairViewModel(uidoc, doc);
                StairWindow window = new StairWindow(viewModel);
                if (window.ShowDialog() == false) 
                {
                    transGr.RollBack();
                    return Result.Cancelled;
                }
                transGr.Assimilate();
                return Result.Succeeded;
            }
           
        }
    }
}
