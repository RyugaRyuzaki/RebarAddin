using Autodesk.Revit.DB;
using System.Collections.Generic;
namespace R02_BeamsRebar
{
    public class DistinctID : IEqualityComparer<Element>
    {
        public bool Equals(Element x, Element y)
        {
            return x.Id == y.Id;
        }

        public int GetHashCode(Element obj)
        {
            return 1;
        }
    }
}
