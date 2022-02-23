
#region Namespaces
using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using Autodesk.Revit.DB.Structure;
using System.Windows;
using Application = Autodesk.Revit.ApplicationServices.Application;
using System.Collections.Generic;
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

            Reference reference = uidoc.Selection.PickObject(Autodesk.Revit.UI.Selection.ObjectType.Element);
            Rebar rebar = doc.GetElement(reference) as Rebar;
            RebarFreeFormAccessor rebarFree = rebar.GetFreeFormAccessor();
            IList<Curve> curves = rebarFree.GetCustomDistributionPath();
            List<HermiteSpline> hermites = new List<HermiteSpline>();
            foreach (var item in curves)
            {
                HermiteSpline hermite = item as HermiteSpline;
                if (hermite != null) hermites.Add(hermite);
            }
            IList<XYZ> list = hermites[0].ControlPoints;
           
            using (Transaction tran = new Transaction(doc))
            {
                tran.Start("aa");

                DetailCurve model = doc.Create.NewDetailCurve(doc.ActiveView, HermiteSpline.Create(list,false));
                tran.Commit();
            }
               
            //using (TransactionGroup transG = new TransactionGroup(doc))
            //{
            //    transG.Start("Columns Rebar");
            //    SlabViewModel SlabViewModel
            //        = new SlabViewModel(uidoc, doc);
            //    SlabsWindow window
            //        = new SlabsWindow(SlabViewModel);
            //    bool? showDialog = window.ShowDialog();
            //    if (showDialog == null || showDialog == false)
            //    {
            //        transG.RollBack();
            //        return Result.Cancelled;
            //    }
            //    transG.Assimilate();
            //    return Result.Succeeded;
            //}                                                                
            return Result.Succeeded;
        }
    }
}
