#region
using Autodesk.Revit.DB;
using System;
using System.Collections.Generic;
using System.Linq;
#endregion
namespace R02_BeamsRebar
{
    public class LineProcess
    {
        /// <summary>
        /// Lấy Line từ dầm
        /// </summary>
        /// <param name="element"></param>
        /// <returns></returns>
        public static Line GetLineFromElement(Element element)
        {
            return (element.Location as LocationCurve).Curve as Line;
        }
        /// <summary>
        /// Lấy List Line
        /// </summary>
        /// <param name="elements"></param>
        /// <returns></returns>
        public static List<Line> GetListLineFromElements(List<Element> elements)
        {
            List<Line> a = new List<Line>();
            foreach (Element item in elements)
            {
                a.Add(GetLineFromElement(item));
            }
            return a;
        }
        /// <summary>
        /// So sánh Diẻction
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static bool Com1(Line a, Line b)
        {
            double x = Math.Abs((a.Direction.X - b.Direction.X));
            double y = Math.Abs((a.Direction.Y - b.Direction.Y));
            double z = Math.Abs((a.Direction.Z - b.Direction.Z));
            return ((x <= 1e-9) && (y <= 1e-9) && (z <= 1e-9));
        }
        public static bool CompareVectorListLine(List<Line> a)
        {
            Line l0 = a[0];
            for (int i = 1; i < a.Count; i++)
            {
                double x = l0.Direction.AngleTo(a[i].Direction);
                if (!(l0.Direction.IsAlmostEqualTo(a[i].Direction)))
                {
                    if (x < Math.PI) { return false; }
                }
            }
            return true;
        }

        public static List<Element> ArrangeBeams(List<Element> beams, Line l)
        {
            if (SolidFace.ParallelLine(l, new XYZ(0, 1, 0)))
            {
                beams = beams.OrderBy(y => ((y.Location as LocationCurve).Curve as Line).GetEndPoint(0).Y)
                .ThenBy(y => ((y.Location as LocationCurve).Curve as Line).GetEndPoint(0).X)
                .ThenBy(y => ((y.Location as LocationCurve).Curve as Line).GetEndPoint(1).Y)
                .ThenBy(y => ((y.Location as LocationCurve).Curve as Line).GetEndPoint(1).X)
                .ToList();

            }
            else
            {
                if (SolidFace.ParallelLine(l, new XYZ(1, 0, 0)))
                {
                    beams = beams.OrderBy(y => ((y.Location as LocationCurve).Curve as Line).GetEndPoint(0).X)
                .ThenBy(y => ((y.Location as LocationCurve).Curve as Line).GetEndPoint(0).Y)
                .ThenBy(y => ((y.Location as LocationCurve).Curve as Line).GetEndPoint(1).X)
                .ThenBy(y => ((y.Location as LocationCurve).Curve as Line).GetEndPoint(1).Y)
                .ToList();
                }
                else
                {
                    if ((l.Direction.AngleTo(new XYZ(0, 1, 0)) < Math.PI / 4) || (l.Direction.AngleTo(new XYZ(0, 1, 0)) > 3 * Math.PI / 4))
                    {
                        beams = beams.OrderBy(y => ((y.Location as LocationCurve).Curve as Line).GetEndPoint(0).Y)
                    .ThenBy(y => ((y.Location as LocationCurve).Curve as Line).GetEndPoint(0).X)
                    .ThenBy(y => ((y.Location as LocationCurve).Curve as Line).GetEndPoint(1).Y)
                    .ThenBy(y => ((y.Location as LocationCurve).Curve as Line).GetEndPoint(1).X)
                    .ToList();
                    }
                    else
                    {
                        beams = beams.OrderBy(y => ((y.Location as LocationCurve).Curve as Line).GetEndPoint(0).X)
                .ThenBy(y => ((y.Location as LocationCurve).Curve as Line).GetEndPoint(0).Y)
                .ThenBy(y => ((y.Location as LocationCurve).Curve as Line).GetEndPoint(1).X)
                .ThenBy(y => ((y.Location as LocationCurve).Curve as Line).GetEndPoint(1).Y)
                .ToList();
                    }
                }
            }
            return beams;
        }
        /// <summary>
        /// Element war arrange
        /// </summary>
        /// <param name="beams"></param>
        /// <returns></returns>
        public static List<Line> GetLine(List<Element> beams, Line l)
        {
            List<Line> b = new List<Line>();
            foreach (var item in beams)
            {
                b.Add(GetLineFromElement(item));
            }
            if (SolidFace.ParallelLine(l, new XYZ(0, 1, 0)))
            {
                b = b.OrderBy(y => y.GetEndPoint(0).Y)
                .ThenBy(y => y.GetEndPoint(0).X)
                .ThenBy(y => y.GetEndPoint(1).Y)
                .ThenBy(y => y.GetEndPoint(1).X)
                .ToList();

            }
            else
            {
                if (SolidFace.ParallelLine(l, new XYZ(1, 0, 0)))
                {
                    b = b.OrderBy(y => y.GetEndPoint(0).X)
                .ThenBy(y => y.GetEndPoint(0).Y)
                .ThenBy(y => y.GetEndPoint(1).X)
                .ThenBy(y => y.GetEndPoint(1).Y)
                .ToList();
                }
                else
                {
                    if ((l.Direction.AngleTo(new XYZ(0, 1, 0)) < Math.PI / 4) || (l.Direction.AngleTo(new XYZ(0, 1, 0)) > 3 * Math.PI / 4))
                    {
                        b = b.OrderBy(y => y.GetEndPoint(0).Y)
                    .ThenBy(y => y.GetEndPoint(0).X)
                    .ThenBy(y => y.GetEndPoint(1).Y)
                    .ThenBy(y => y.GetEndPoint(1).X)
                    .ToList();
                    }
                    else
                    {
                        b = b.OrderBy(y => y.GetEndPoint(0).X)
                .ThenBy(y => y.GetEndPoint(0).Y)
                .ThenBy(y => y.GetEndPoint(1).X)
                .ThenBy(y => y.GetEndPoint(1).Y)
                .ToList();
                    }
                }
            }
            return b;
        }
        public static List<XYZ> GetPoint(List<Line> b, Line l)
        {
            List<XYZ> a = new List<XYZ>();
            foreach (var item in b)
            {
                a.Add(item.GetEndPoint(0));
                a.Add(item.GetEndPoint(1));
            }
            if (SolidFace.ParallelLine(l, new XYZ(0, 1, 0)))
            {
                a = a.OrderBy(y => y.Y)
                .ThenBy(y => y.X)
                .ToList();

            }
            else
            {
                if (SolidFace.ParallelLine(l, new XYZ(1, 0, 0)))
                {
                    a = a.OrderBy(y => y.X).ThenBy(y => y.Y).ToList();
                }
                else
                {
                    if ((l.Direction.AngleTo(new XYZ(0, 1, 0)) < Math.PI / 4) || (l.Direction.AngleTo(new XYZ(0, 1, 0)) > 3 * Math.PI / 4))
                    {
                        a = a.OrderBy(y => y.Y).ThenBy(y => y.X).ToList();
                    }
                    else
                    {
                        a = a.OrderBy(y => y.X).ThenBy(y => y.Y).ThenBy(y => y.X).ThenBy(y => y.Y).ToList();
                    }
                }
            }
            List<XYZ> c = new List<XYZ>();
            c.Add(a[0]);
            c.Add(a[a.Count - 1]);
            return c;
        }
        
    }
}
