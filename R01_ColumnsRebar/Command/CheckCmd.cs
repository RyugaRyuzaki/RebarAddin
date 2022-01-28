
#region Namespaces
using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using Autodesk.Revit.UI.Selection;
using System.Collections.Generic;
using Application = Autodesk.Revit.ApplicationServices.Application;
using System.Linq;
using System.Windows.Forms;
using System;
using System.Diagnostics;
#endregion


namespace R01_ColumnsRebar
{
    [Transaction(TransactionMode.Manual)]
    public class CheckCmd : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData,
            ref string message, ElementSet elements)
        {
            UIApplication uiapp = commandData.Application;
            UIDocument uidoc = uiapp.ActiveUIDocument;
            Application app = uiapp.Application;
            Document doc = uidoc.Document;
            Reference reference = uidoc.Selection.PickObject(ObjectType.Element);
            Element e = doc.GetElement(reference);
           
            
            return Result.Succeeded;
        }
    }
}
