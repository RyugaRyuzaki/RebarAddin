using Autodesk.Revit.DB;
using System;
using System.Collections.Generic;

namespace R02_BeamsRebar
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
        public static double DistanceTo2(PlanarFace plane, XYZ point, Document document)
        {
            return Math.Abs(double.Parse(UnitFormatUtils.Format(document.GetUnits(), SpecTypeId.Length, (point - plane.Origin).DotProduct(plane.FaceNormal), false)));
        }
      
    }
}
