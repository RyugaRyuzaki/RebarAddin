using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace R01_ColumnsRebar
{
    public class CompareBarModel : IComparer<BarModel>
    {
        public int Compare(BarModel x, BarModel y)
        {
            return ConditionSameBarModel(x, y);
        }
        private static bool ConditionSameBarModel(BarModel b1, BarModel b2)
        {
            if (!b1.Bar.Type.Equals(b2.Bar.Type)) return false;
            if (!(PointModel.AreEqual(b1.Bar.Diameter, b2.Bar.Diameter))) return false;
            if ((b1.IsTopDowels != b2.IsTopDowels))
            {
                return false;
            }
            else
            {
                if (b1.TopDowels != b2.TopDowels)
                {
                    return false;
                }
                else
                {
                    if (b1.TopDowels != 0)
                    {
                        if (!PointModel.AreEqual(b1.LaTopDowels, b2.LaTopDowels)) return false;
                    }
                }
            }
            if (b1.IsBottomDowels != b2.IsBottomDowels)
            {
                return false;
            }
            else
            {
                if (b1.BottomDowels != b2.BottomDowels)
                {
                    return false;
                }
                else
                {
                    if (b1.BottomDowels == 0)
                    {
                        if (!PointModel.AreEqual(b1.LcBottomDowels, b2.LcBottomDowels)) return false;
                    }
                    else
                    {
                        if ((!PointModel.AreEqual(b1.LaBottomDowels, b2.LaBottomDowels)) || (!PointModel.AreEqual(b1.LbBottomDowels, b2.LbBottomDowels))) return false;
                    }
                }
            }
            if (b1.Location.Count != b2.Location.Count)
            {
                return false;
            }
            else
            {
                if (!PointModel.AreEqual(GetLenght(b1), GetLenght(b2))) return false;
            }

            return true;

        }
        private static double GetLenght(BarModel bar)
        {
            double a = 0;
            for (int i = 1; i < bar.Location.Count; i++)
            {
                a += Math.Sqrt((bar.Location[i - 1].X - bar.Location[i].X) * (bar.Location[i - 1].X - bar.Location[i].X) + (bar.Location[i - 1].Y - bar.Location[i].Y) * (bar.Location[i - 1].Y - bar.Location[i].Y) + (bar.Location[i - 1].Z - bar.Location[i].Z) * (bar.Location[i - 1].Z - bar.Location[i].Z));
            }
            return a;
        }
    }
}
