#region Namespaces
using Autodesk.Revit.DB;
using Autodesk.Revit.UI.Selection;
#endregion

namespace R10_WallShear
{
    public class WallSelectionFilter : ISelectionFilter
    {
        public bool AllowElement(Element elem)
        {
            if (elem is Wall == false)
            {
                return false;
            }
            else
            {
                int a = elem.get_Parameter(BuiltInParameter.WALL_STRUCTURAL_USAGE_PARAM).AsInteger();
                if (a != 2)
                {
                    return false;
                }
                else
                {
                    int i = elem.get_Parameter(BuiltInParameter.WALL_STRUCTURAL_SIGNIFICANT).AsInteger();
                    //return i == 1;
                    if (i != 1)
                    {
                        return false;
                    }
                    else
                    {
                        return WallCompoundStructure(elem as Wall);
                    }
                }

            }

        }

        public bool AllowReference(Reference reference, XYZ position)
        {
            return true;
        }
        public static bool WallCompoundStructure(Wall wall)
        {
            WallType wallType = wall.WallType;
            CompoundStructure compound = wallType.GetCompoundStructure();
            CompoundStructureLayer compoundStructureLayer = compound.GetLayers()[0];
            return PointModel.AreEqual(compound.GetWidth() ,compoundStructureLayer.Width);
        }
    }
}
