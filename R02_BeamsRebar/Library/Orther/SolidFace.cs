
using Autodesk.Revit.DB;
using System;
using System.Collections.Generic;
using System.Linq;

namespace R02_BeamsRebar
{
    public class SolidFace
    {

        public static bool ParallelLine(Line a, XYZ b)
        {
            double y = a.Direction.AngleTo(b);
            if (!(y < 1e-9 || Math.Abs(y - Math.PI) < 1e-9))
            {
                return false;
            }
            return true;
        }
        public static bool ParallelVector(XYZ a, XYZ b)
        {
            double y = a.AngleTo(b);
            if (!(y < 1e-9 || Math.Abs(y - Math.PI) < 1e-9))
            {
                return false;
            }
            return true;
        }
        public static List<PlanarFace> GetAllPlanrFace(Solid solid)
        {
            List<PlanarFace> a = new List<PlanarFace>();
            FaceArray faceArray = solid.Faces;
            foreach (Face item in faceArray)
            {
                a.Add(item as PlanarFace);
            }
            return a;
        }
        public static List<PlanarFace> GetPlanrFacePerpendicular(Solid solid, XYZ vector)
        {
            List<PlanarFace> a = new List<PlanarFace>();
            FaceArray faceArray = solid.Faces;
            foreach (Face item in faceArray)
            {

                PlanarFace x = item as PlanarFace;
                double y = vector.AngleTo(x.FaceNormal);
                if ((y < 1e-9 || Math.Abs(y - Math.PI) < 1e-9))
                {
                    a.Add(x);
                }
            }
            List<PlanarFace> b = (a.OrderBy(x => x.Origin.X).ThenBy(x => x.Origin.Y)).ToList();
            return b;
        }
        public static List<PlanarFace> GetPlanrFacePerpendicularOne(Element element, XYZ vector)
        {
            List<PlanarFace> a = new List<PlanarFace>();
            Solid s = GetSolidElement(element);
            List<PlanarFace> planar = GetPlanrFacePerpendicular(s, vector);
            foreach (var i in planar)
            {
                a.Add(i);
            }
            return ArrangePlanrFace(a, vector);
        }
        public static List<PlanarFace> GetAllPlanrFacePerpendicular(List<Element> elements, XYZ vector)
        {
            List<PlanarFace> a = new List<PlanarFace>();
            foreach (var item in elements)
            {
                Solid s = GetSolidElement(item);
                List<PlanarFace> planar = GetPlanrFacePerpendicular(s, vector);
                foreach (var i in planar)
                {
                    a.Add(i);
                }
            }
            return a;
        }
        public static List<PlanarFace> GetPlanrFaceTopBottom(Solid solid)
        {
            List<PlanarFace> a = new List<PlanarFace>();
            FaceArray faceArray = solid.Faces;
            foreach (Face item in faceArray)
            {

                PlanarFace x = item as PlanarFace;
                if ((x.FaceNormal.IsAlmostEqualTo(new XYZ(0, 0, 1))) || (x.FaceNormal.IsAlmostEqualTo(new XYZ(0, 0, -1))))
                {
                    a.Add(x);
                }
            }
            a = a.Distinct(new DistictPlanarFace()).ToList();
            return a;
        }
        public static List<PlanarFace> GetPlanrFaceLeftRight(Solid solid, Line line)
        {
            XYZ l = line.Direction;
            List<PlanarFace> a = new List<PlanarFace>();
            List<PlanarFace> b = new List<PlanarFace>();
            FaceArray faceArray = solid.Faces;
            foreach (Face item in faceArray)
            {
                PlanarFace x = item as PlanarFace;
                a.Add(x);
                if ((x.FaceNormal.IsAlmostEqualTo(new XYZ(0, 0, 1))) || (x.FaceNormal.IsAlmostEqualTo(new XYZ(0, 0, -1))))
                {
                    b.Add(x);
                }
            }
            List<PlanarFace> c = GetPlanrFacePerpendicular(solid, l);
            a.RemoveAll(x => b.Any(y => y.Id == x.Id));
            a.RemoveAll(x => c.Any(y => y.Id == x.Id));
            a = a.Distinct(new DistictPlanarFaceLeftRight()).ToList();
            return a;
        }
        public static List<PlanarFace> GetPlanrFaceLeftRight1(Solid solid, Line line)
        {
            XYZ l = line.Direction;
            List<PlanarFace> b = new List<PlanarFace>();
            FaceArray faceArray = solid.Faces;
            foreach (Face item in faceArray)
            {
                PlanarFace x = item as PlanarFace;
                if (Math.Abs(x.FaceNormal.DotProduct(l))<1e-9&& Math.Abs(x.FaceNormal.DotProduct(new XYZ(0,0,1)))<1e-9)
                {
                    b.Add(x);
                }
            }
            return b;
        }
        public static Solid GetSolidElement(Element element)
        {
            List<Solid> a = new List<Solid>();
            List<Solid> b = new List<Solid>();
            Solid c = null;
            Options options = new Options();
            options.ComputeReferences = true;
            GeometryElement geometryElement = element.get_Geometry(options) as GeometryElement;
            foreach (GeometryObject geometryObject in geometryElement)
            {
                Solid solid = geometryObject as Solid;
                if (solid != null)
                {
                    a.Add(solid);
                }
                else
                {
                    GeometryInstance geometryInstance = geometryObject as GeometryInstance;
                    GeometryElement geometryElement1 = geometryInstance.GetSymbolGeometry();
                    foreach (GeometryObject geometryObject1 in geometryElement1)
                    {
                        Solid solid1 = geometryObject1 as Solid;
                        if (solid1 != null)
                        {
                            a.Add(solid1);
                        }
                    }
                }
            }
            foreach (Solid item in a)
            {
                if (item.Volume != 0) { b.Add(item); }
            }
            if (b.Count == 1) { c = b[0]; } else { c = null; }
            return c;
        }
        public static List<PlanarFace> ArrangePlanrFace(List<PlanarFace> planarFaces, XYZ vector)
        {
            if (ParallelVector(vector, new XYZ(0, 1, 0)))
            {
                planarFaces = planarFaces.OrderBy(x => x.Origin.Y).ToList();

            }
            else
            {
                if (ParallelVector(vector, new XYZ(1, 0, 0)))
                {
                    planarFaces = planarFaces.OrderBy(x => x.Origin.X).ToList();

                }
                else
                {
                    if ((vector.AngleTo(new XYZ(0, 1, 0)) < Math.PI / 4) || (vector.AngleTo(new XYZ(0, 1, 0)) > 3 * Math.PI / 4))
                    {
                        planarFaces = planarFaces.OrderBy(x => x.Origin.Y).ThenBy(x => x.Origin.X).ToList();

                    }
                    else
                    {
                        planarFaces = planarFaces.OrderBy(x => x.Origin.X).ThenBy(x => x.Origin.Y).ToList();

                    }
                }
            }
            return planarFaces;
        }
        public static List<PlanarFace> ArrangePlanrFaceNormal(List<PlanarFace> planarFaces, XYZ vector)
        {
            if (ParallelVector(vector, new XYZ(0, 1, 0)))
            {
                planarFaces = planarFaces.OrderByDescending(x => x.Origin.Y).ToList();

            }
            else
            {
                if (ParallelVector(vector, new XYZ(1, 0, 0)))
                {
                    planarFaces = planarFaces.OrderBy(x => x.Origin.X).ToList();

                }
                else
                {
                    if ((vector.AngleTo(new XYZ(0, 1, 0)) < Math.PI / 4) || (vector.AngleTo(new XYZ(0, 1, 0)) > 3 * Math.PI / 4))
                    {
                        planarFaces = planarFaces.OrderByDescending(x => x.Origin.Y).ThenBy(x => x.Origin.X).ToList();

                    }
                    else
                    {
                        planarFaces = planarFaces.OrderByDescending(x => x.Origin.X).ThenBy(x => x.Origin.Y).ToList();

                    }
                }
            }
            return planarFaces;
        }
        public static List<PlanarFace> AddRangePlanarFace(List<PlanarFace> planrColumns, List<PlanarFace> planrBeams, XYZ l)
        {
            List<PlanarFace> planr = new List<PlanarFace>();
            foreach (var item in planrColumns)
            {
                planr.Add(item);
            }
            planr.AddRange(planrBeams);
            planr = ArrangePlanrFace(planr, l);
            return planr;
        }

    }
}
