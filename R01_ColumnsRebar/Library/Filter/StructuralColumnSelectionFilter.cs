#region Namespaces
using Autodesk.Revit.DB;
using Autodesk.Revit.UI.Selection;
#endregion

namespace R01_ColumnsRebar
{
    public class StructuralColumnSelectionFilter : ISelectionFilter
    {
        public bool AllowElement(Element elem)
        {
            return elem.Category.Name.Equals("Structural Columns");

        }

        public bool AllowReference(Reference reference, XYZ position)
        {
            return true;
        }
    }
}
