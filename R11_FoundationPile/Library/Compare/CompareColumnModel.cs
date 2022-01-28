using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace R11_FoundationPile
{
    public class CompareColumnModel : EqualityComparer<ColumnModel>
    {
        public override bool Equals(ColumnModel column1, ColumnModel column2)
        {
            if (!column1.Style.Equals(column2.Style))
            {
                return false;
            }
            else
            {
                if (column1.Style.Equals("RECTANGLE"))
                {
                    if (PointModel.AreEqual(column1.b, column2.b) && PointModel.AreEqual(column1.h, column2.h))
                    {
                        return true;
                    }
                    else
                    {
                        if (PointModel.AreEqual(column1.b, column2.h) && PointModel.AreEqual(column1.h, column2.b))
                        {
                            return true;
                        }
                        else
                        {
                            return false;
                        }
                    }
                }
                else
                {
                    if (PointModel.AreEqual(column1.D, column2.D))
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
        }

        public override int GetHashCode(ColumnModel obj)
        {
            return 1;
        }
    }
}
