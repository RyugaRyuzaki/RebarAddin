
#region Namespaces
using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using System.Windows;
using Application = Autodesk.Revit.ApplicationServices.Application;
#endregion


namespace R04_SlabsRebar
{
    [Transaction(TransactionMode.Manual)]
    public class SlabsRebarCmd : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData,
            ref string message, ElementSet elements)
        {
            UIApplication uiapp = commandData.Application;
            UIDocument uidoc = uiapp.ActiveUIDocument;
            Application app = uiapp.Application;
            Document doc = uidoc.Document;

            // code


            using (TransactionGroup transG = new TransactionGroup(doc))
            {
                transG.Start("Columns Rebar");
                SlabViewModel SlabViewModel
                    = new SlabViewModel(uidoc, doc);
                SlabsWindow window
                    = new SlabsWindow(SlabViewModel);
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
}
