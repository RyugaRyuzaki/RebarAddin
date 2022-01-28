using Autodesk.Revit.DB;
using System.Collections.Generic;

namespace R10_WallShear
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

}
