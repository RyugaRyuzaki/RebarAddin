using Autodesk.Revit.DB;
using System;
using System.Collections.Generic;

namespace R11_FoundationPile
{
    public class PointModel
    {
        public static bool AreEqual(double firstValue, double secondValue,double tolerance = 1.0e-9)
        {
            return (secondValue - tolerance < firstValue&& firstValue < secondValue + tolerance);
        }
        public static XYZ ProjectToPlane(XYZ po, PlanarFace p)
        {
            XYZ vecPoToPlaneOrigin = p.Origin - po;
            if (!(Math.Abs(vecPoToPlaneOrigin.DotProduct(p.FaceNormal)) < 1e-9))
            {

                return po + p.FaceNormal * vecPoToPlaneOrigin.DotProduct(p.FaceNormal);
            }
            return po;
        }
        public static XYZ ProjectToLine(XYZ po, Line l)
        {
            XYZ vecPoToLineOrigin =po- l.GetEndPoint(0) ;
            if (!(Math.Abs(vecPoToLineOrigin.DotProduct(l.Direction)) < 1e-9))
            {

                if (vecPoToLineOrigin.AngleTo(l.Direction)>Math.PI*0.5)
                {
                    return l.GetEndPoint(0) - l.Direction * vecPoToLineOrigin.DotProduct(l.Direction);
                }
                else
                {
                    return l.GetEndPoint(0) + l.Direction * vecPoToLineOrigin.DotProduct(l.Direction);
                }
            }
            else
            {
                return l.GetEndPoint(0);
            }
        }
        public static double DistanceTo2(PlanarFace plane, XYZ point, Document document)
        {
            return Math.Abs(double.Parse(UnitFormatUtils.Format(document.GetUnits(), SpecTypeId.Length, (point - plane.Origin).DotProduct(plane.FaceNormal), false)));

        }
        public static XYZ IntersecPointToPlan(Line line, PlanarFace planarFace)
        {
            XYZ p1 = line.GetEndPoint(0);
            XYZ p2 = line.GetEndPoint(1);
            XYZ p3 = ProjectToPlane(p1,planarFace);
            XYZ p4 = ProjectToPlane(p2,planarFace);
            XYZ p13 = p3 - p1;
            XYZ p24 = p4 - p2;
            if (p13.IsAlmostEqualTo(p24))
            {
                return null;
            }
            else
            {
                if (AreEqual( p13.AngleTo(p24),Math.PI))
                {
                    double p1p3 = p1.DistanceTo(p3);
                    double p2p4 = p2.DistanceTo(p4);
                    double p3p4 = p3.DistanceTo(p4);
                   double p45= (p2p4 / (p2p4 + p1p3))* p3p4;
                    return  p4 + (p3 - p4) * p45;
                }
                else
                {
                    return null;
                }
            }
        }

        public static double DistanceFromPipesCurveToBottomPlanarFacrBeam(Line pipe, PlanarFace left,PlanarFace right,PlanarFace bottom)
        {
            XYZ pLeft = IntersecPointToPlan(pipe, left);
            XYZ pRight = IntersecPointToPlan(pipe, right);
            if (pLeft == null || pRight == null)
            {
               return 1e6;
            }
            else
            {
                XYZ pBLeft = ProjectToPlane(pLeft, bottom);
                XYZ pBRight = ProjectToPlane(pRight, bottom);
                return Math.Min(pLeft.DistanceTo(pBLeft),pRight.DistanceTo(pBRight));
            }
        }
    }
}
