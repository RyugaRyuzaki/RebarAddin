#region Namespaces
using Autodesk.Revit.DB;
using Autodesk.Revit.UI.Selection;
#endregion

namespace R06_StairRebar.Library.Filter
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
