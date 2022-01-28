using Autodesk.Revit.DB;
using System.Collections.Generic;

namespace R01_ColumnsRebar
{
    public class DistictPlanarFaceID : IEqualityComparer<PlanarFace>
    {
        public bool Equals(PlanarFace x, PlanarFace y)
        {
            return x.Id == y.Id;
        }

        public int GetHashCode(PlanarFace obj)
        {
            return 1;
        }
    }

}
