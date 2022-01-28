#region
using Autodesk.Revit.DB;
using System.Collections.Generic;
using System.Linq;
#endregion
namespace R10_WallShear
{
    public class LineProcess
    {
        public static Line GetLineFromElement(Element element)
        {
            Curve curve = (element.Location as LocationCurve).Curve;
            Line line = curve as Line;
            return line;
        }
        public static bool CompareDirectionLine(Line a, Line b)
        {
            return ((PointModel.AreEqual(a.Direction.X,b.Direction.X))&& (PointModel.AreEqual(a.Direction.Y, b.Direction.Y))&& (PointModel.AreEqual(a.Direction.Z, b.Direction.Z)));
        }
        /// <summary>
        /// Sắp xếp các columns theo thứ tự từ dưới lên.
        /// </summary>
        /// <param name="elements"></param>
        /// <param name="document"></param>
        /// <returns></returns>
        //public static List<Element> ArrangeColumns(List<Element> columns)
        //{
        //    List<PlanarFace> planarFaces = new List<PlanarFace>();
        //    foreach (Element col in columns)
        //    {
        //        Solid solid = SolidFace.GetSolidElement(col);
        //        planarFaces = SolidFace.GetPlanrFaceTopBottom(solid);
        //        planarFaces = planarFaces.OrderBy(z => (z.Origin as XYZ).Z).ToList();

        //    }

        //    return columns;
        //}

    }
}
