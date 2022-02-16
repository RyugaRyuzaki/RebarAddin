
#region Namespaces
using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.DB.ExtensibleStorage;
using Autodesk.Revit.UI;
using R11_FoundationPile.Library.Filter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using Application = Autodesk.Revit.ApplicationServices.Application;
   
#endregion


namespace R11_FoundationPile
{
    [Transaction(TransactionMode.Manual)]
    [Regeneration(RegenerationOption.Manual)]
    public class FoundationPileCmd : IExternalCommand
    {
        public virtual  Result Execute(ExternalCommandData commandData,
            ref string message, ElementSet elements)
        {
            UIApplication uiapp = commandData.Application;
            UIDocument uidoc = uiapp.ActiveUIDocument;
            Application app = uiapp.Application;
            Document doc = uidoc.Document;
            //Reference r = uidoc.Selection.PickObject(Autodesk.Revit.UI.Selection.ObjectType.Face);
            //System.Windows.Forms.MessageBox.Show(r.ConvertToStableRepresentation(doc).ToString());
            //return Result.Succeeded;
            List<Reference> references = null;
            try
            {

                references = uidoc.Selection.PickObjects(Autodesk.Revit.UI.Selection.ObjectType.Element, new StructuralColumnSelectionFilter()).ToList();
                List<Element> columns = new List<Element>();
                foreach (var item in references)
                {
                    columns.Add(doc.GetElement(item));
                }
                columns = columns.Where(x => ErrorColumns.GetSectionStyle(doc, x) != ErrorColumns.SectionStyle.ORTHER).ToList();

                columns = columns.OrderBy(x => (x.Location as LocationPoint).Point.X).ThenBy(x => (x.Location as LocationPoint).Point.X).ToList();
                FloorType family = new FilteredElementCollector(doc)
                    .OfClass(typeof(FloorType))
                    .Cast<FloorType>()
                    .Where(x => x.IsFoundationSlab).FirstOrDefault();
                using (TransactionGroup transGr = new TransactionGroup(doc))
                {
                    transGr.Start("RAPI00TransGr");

                    FoundationPileViewModel viewModel = new FoundationPileViewModel(uiapp,uidoc, doc, columns);
                    FoundationPileWindow window = new FoundationPileWindow(viewModel);

                    //window.Show();
                    if (window.ShowDialog() == false) return Result.Cancelled;

                    transGr.Assimilate();
                }

                return Result.Succeeded;
            }
            catch (Exception e)
            {

                message = e.Message;
                return Result.Cancelled;
            }


        }
      

    }


}
