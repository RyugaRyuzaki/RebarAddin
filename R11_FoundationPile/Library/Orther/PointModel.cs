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
      
    }
}
