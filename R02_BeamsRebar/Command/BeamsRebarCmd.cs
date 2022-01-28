
#region Namespaces
using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Application = Autodesk.Revit.ApplicationServices.Application;
using Autodesk.Revit.UI.Selection;
using System;
using System.Diagnostics;
using System.Windows.Documents;
#endregion

namespace R02_BeamsRebar
{
    [Transaction(TransactionMode.Manual)]
    public class BeamsRebarCmd : IExternalCommand
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
                
                references = uidoc.Selection.PickObjects(ObjectType.Element, new BeamSelectionFilter()).ToList();
                if (references.Count==0)
                {
                    return Result.Cancelled;
                }
                List<Element> Beams = GetElements(references, doc);
                Line l = LineProcess.GetLineFromElement(Beams[0]);
                Beams = LineProcess.ArrangeBeams(Beams, l);
                int Error = ErrorBeams.Error(Beams, doc);
                if (Error != 0)
                {
                    string hyperlink="Do you want to see on Youtube ?";
                    string navigateUri = "https://www.youtube.com/channel/UCQSwGw2vUjad7kUhEOXbqaw";
                    if (MessageBox.Show("E " + Error + " : " + ErrorBeams.ErrorString[Error]+"\n"+hyperlink, "ERROR", MessageBoxButtons.YesNo, MessageBoxIcon.Error) == DialogResult.Yes)
                    {
                        Process.Start(new ProcessStartInfo(navigateUri));
                        return Result.Cancelled;
                    }
                    return Result.Cancelled;
                }
                else
                {
                    using (TransactionGroup transG = new TransactionGroup(doc))
                    {
                        transG.Start("Join Beam with Floor");
                        BeamsViewModel viewModel
                            = new BeamsViewModel(uidoc, doc, Beams);
                        BeamsWindow window
                            = new BeamsWindow(viewModel);
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

        }
        #region Get beams
        private  List<Element> GetElements(List<Reference> references, Document document)
        {
            List<Element> beams = new List<Element>(references.Count);
            foreach (Reference item in references)
            {
                beams.Add(document.GetElement(item));

            }
            return beams;
        }
        #endregion
    }
}
