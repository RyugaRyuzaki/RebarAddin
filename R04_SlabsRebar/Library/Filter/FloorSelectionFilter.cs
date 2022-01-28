#region Namespaces
using Autodesk.Revit.DB;
using Autodesk.Revit.UI.Selection;
#endregion

namespace R04_SlabsRebar.Library.Filter
{
    public class FloorSelectionFilter : ISelectionFilter
    {
        public bool AllowElement(Element elem)
        {
            return elem is Floor;

        }

        public bool AllowReference(Reference reference, XYZ position)
        {
            return true;
        }
    }
}
