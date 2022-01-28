
#region Namespaces
using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Autodesk.Revit.UI.Selection;
using Application = Autodesk.Revit.ApplicationServices.Application;
#endregion


namespace R03_FoundationRebar
{
    [Transaction(TransactionMode.Manual)]
    public class FoundationRebarCmd : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData,
            ref string message, ElementSet elements)
        {
            UIApplication uiapp = commandData.Application;
            UIDocument uidoc = uiapp.ActiveUIDocument;
            Application app = uiapp.Application;
            Document doc = uidoc.Document;

            // code

            //Reference reference = null;
            //try
            //{
            //    reference = uidoc.Selection.PickObject(ObjectType.Element);
            //    if (reference==null)
            //    {
            //        return Result.Cancelled;
            //    }
            //    else
            //    {
            //        Element e = doc.GetElement(reference);
            //        if (ErrorFoundation.Error(e,doc)!=0)
            //        {
            //            if (MessageBox.Show("E " + ErrorFoundation.Error(e, doc).ToString() + " : " + ErrorFoundation.Errorstring[ErrorFoundation.Error(e, doc)], "ERROR", MessageBoxButtons.YesNo, MessageBoxIcon.Error) == DialogResult.Yes)
            //            {

            //            }
            //            else
            //            {
            //                return Result.Cancelled;
            //            };
            //        }
            //        else
            //        {
            //            using (TransactionGroup transGr = new TransactionGroup(doc))
            //            {
            //                transGr.Start("RAPI00TransGr");

            //                FoundationViewModel viewModel = new FoundationViewModel(uidoc, doc);
            //                FoundationWindow window = new FoundationWindow(viewModel);
            //                if (window.ShowDialog() == false) return Result.Cancelled;

            //                transGr.Assimilate();
            //            }
            //        }


            //    }

            //}
            //catch (System.Exception e)
            //{

            //    System.Windows.Forms.MessageBox.Show(e.Message);
            //}


            MessageBox.Show("This Add-in will be coming soon", "Imformation");
            return Result.Succeeded;
        }
    }
}
