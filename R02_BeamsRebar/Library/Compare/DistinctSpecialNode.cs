
using System;
using System.Collections.Generic;

namespace R02_BeamsRebar
{
    public class DistinctSpecialNode : IEqualityComparer<SpecialNodeModel>
    {
        public bool Equals(SpecialNodeModel x, SpecialNodeModel y)
        {
            return Math.Abs(x.Mid - y.Mid) < 1e-15;
        }

        public int GetHashCode(SpecialNodeModel obj)
        {
            return 1;
        }
    }
}
