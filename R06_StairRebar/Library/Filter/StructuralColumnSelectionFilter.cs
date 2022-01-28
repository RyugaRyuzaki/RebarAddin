#region Namespaces
using Autodesk.Revit.DB;
using Autodesk.Revit.UI.Selection;
#endregion

namespace R06_StairRebar.Library.Filter
{
    public class StructuralColumnSelectionFilter : ISelectionFilter
    {
        public bool AllowElement(Element elem)
        {
            string name = elem.Category.Name;
            return name.Equals("Structural Columns");

        }

        public bool AllowReference(Reference reference, XYZ position)
        {
            return true;
        }
    }
}
