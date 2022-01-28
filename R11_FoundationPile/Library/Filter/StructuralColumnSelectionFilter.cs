#region Namespaces
using Autodesk.Revit.DB;
using Autodesk.Revit.UI.Selection;
#endregion

namespace R11_FoundationPile.Library.Filter
{
    public class StructuralColumnSelectionFilter : ISelectionFilter
    {
        public bool AllowElement(Element elem)
        {
            string name = elem.Category.Name;
            if (!name.Equals("Structural Columns"))
            {
                return false;
            }
            else
            {
                if (!elem.get_Parameter(BuiltInParameter.SLANTED_COLUMN_TYPE_PARAM).AsValueString().Equals("Vertical"))
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
        }

        public bool AllowReference(Reference reference, XYZ position)
        {
            return true;
        }
    }
}
