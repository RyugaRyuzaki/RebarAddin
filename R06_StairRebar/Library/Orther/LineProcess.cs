#region
using Autodesk.Revit.DB;
using System.Collections.Generic;
using System.Linq;
#endregion
namespace R06_StairRebar.Library.Filter
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
            return ((a.Direction.X == b.Direction.X) && (a.Direction.Y == b.Direction.Y) && (a.Direction.Z == b.Direction.Z));
        }
        //public static bool CompareVectorLine(Line a, Line b) {
        //    bool n1 = CompareDirectionLine(a,b);
        //    bool n2 = (()&&()&&());
        //    return (n1&&n2);
        //}
    }
}
