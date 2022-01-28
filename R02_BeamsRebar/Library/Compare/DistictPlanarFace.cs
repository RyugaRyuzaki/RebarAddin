using Autodesk.Revit.DB;
using System.Collections.Generic;

namespace R02_BeamsRebar
{
    public class DistictPlanarFace : IEqualityComparer<PlanarFace>
    {
        public bool Equals(PlanarFace x, PlanarFace y)
        {
            return x.Origin.Z == y.Origin.Z;
        }

        public int GetHashCode(PlanarFace obj)
        {
            return 1;
        }
    }
    public class DistictPlanarFaceLeftRight : IEqualityComparer<PlanarFace>
    {
        public bool Equals(PlanarFace x, PlanarFace y)
        {
            return (x.XVector.IsAlmostEqualTo(y.XVector)) && (x.YVector.IsAlmostEqualTo(y.YVector));
        }

        public int GetHashCode(PlanarFace obj)
        {
            return 1;
        }
    }
}
