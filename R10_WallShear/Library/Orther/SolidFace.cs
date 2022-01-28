
using Autodesk.Revit.DB;
using System;
using System.Collections.Generic;
using System.Linq;

namespace R10_WallShear
{
    public class SolidFace
    {
        public static PlanarFace GetTopFace(Solid solid)
        {
            PlanarFace topFace = null;
            FaceArray faces = solid.Faces;
            foreach (Face f in faces)
            {
                PlanarFace pf = f as PlanarFace;
                if (pf.FaceNormal.IsAlmostEqualTo(new XYZ(0, 0, 1)))
                {
                    topFace = pf;
                }
            }
            return topFace;
        }
        public static List<Solid> GetSolidElement(Reference reference, Document document)
        {
            List<Solid> a = new List<Solid>();
            List<Solid> b = new List<Solid>();
            Element element = document.GetElement(reference);
            Options options = new Options();
            options.ComputeReferences = true;
            GeometryElement geometryElement = element.get_Geometry(options);
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
            return b;
        }
        public static Solid GetSolidOneElement(Element element)
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
        public static List<Solid> GetListSolidOneElement(Element element)
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
            return b;
        }
        public static PlanarFace GetBottom(Element element)
        {
            Solid a = GetSolidOneElement(element);
            FaceArray faceArray = a.Faces;
            List<PlanarFace> planarFaces = new List<PlanarFace>();
            foreach (var item in faceArray)
            {
                PlanarFace a1 = item as PlanarFace;
                if (a1 != null)
                {
                    if ((PointModel.AreEqual(a1.FaceNormal.AngleTo(XYZ.BasisZ), Math.PI)) || (PointModel.AreEqual(a1.FaceNormal.AngleTo(XYZ.BasisZ), 0)))
                    {
                        planarFaces.Add(a1);
                    }
                }
            }
            planarFaces = planarFaces.OrderBy(x => x.Origin.Z).ToList();
            PlanarFace bottom = planarFaces[0];
            return bottom;
        }
        
        public static PlanarFace GetTop(Element element)
        {
            Solid a = GetSolidOneElement(element);
            FaceArray faceArray = a.Faces;
            List<PlanarFace> planarFaces = new List<PlanarFace>();
            foreach (var item in faceArray)
            {
                PlanarFace a1 = item as PlanarFace;
                if (a1 != null)
                {
                    if ((PointModel.AreEqual(a1.FaceNormal.AngleTo(XYZ.BasisZ), Math.PI)) || (PointModel.AreEqual(a1.FaceNormal.AngleTo(XYZ.BasisZ), 0)))
                    {
                        planarFaces.Add(a1);
                    }
                }
            }
            planarFaces = planarFaces.OrderBy(x => x.Origin.Z).ToList();
            PlanarFace bottom = planarFaces[1];
            return bottom;
        }
        public static PlanarFace GetSouth(Element element)
        {
            Solid a = GetSolidOneElement(element);
            Line line = LineProcess.GetLineFromElement(element);
            FaceArray faceArray = a.Faces;
            PlanarFace planarFace = null;
            foreach (var item in faceArray)
            {
                PlanarFace x = item as PlanarFace;
                if (x != null)
                {
                    bool b1 = PointModel.AreEqual(x.FaceNormal.AngleTo(XYZ.BasisZ), Math.PI / 2);
                    bool b2 = PointModel.AreEqual(x.FaceNormal.AngleTo(line.Direction.CrossProduct(XYZ.BasisZ)), 0);
                    bool b3 = PointModel.AreEqual(x.FaceNormal.AngleTo(line.Direction), Math.PI / 2);

                    if (b1 && b2 && b3)
                    {
                        planarFace = x;
                        
                    }
                }
            }
            return planarFace;
        }
        public static PlanarFace GetNouth(Element element)
        {
            Solid a = GetSolidOneElement(element);
            Line line = LineProcess.GetLineFromElement(element);
            FaceArray faceArray = a.Faces;
            PlanarFace planarFace = null;
            foreach (var item in faceArray)
            {
                PlanarFace x = item as PlanarFace;
                if (x != null)
                {
                    bool b1 = PointModel.AreEqual(x.FaceNormal.AngleTo(XYZ.BasisZ), Math.PI / 2);
                    bool b2 = PointModel.AreEqual(x.FaceNormal.AngleTo(line.Direction.CrossProduct((-1)*XYZ.BasisZ)), 0);
                    bool b3 = PointModel.AreEqual(x.FaceNormal.AngleTo(line.Direction), Math.PI / 2);
                    if (b1 && b2 && b3)
                    {
                        planarFace = x;
                       
                    }
                }
            }
            return planarFace;
        }
        public static PlanarFace GetWest(Element element)
        {
            Solid a = GetSolidOneElement(element);
            Line line = LineProcess.GetLineFromElement(element);
            FaceArray faceArray = a.Faces;
            PlanarFace planarFace = null;
            foreach (var item in faceArray)
            {
                PlanarFace x = item as PlanarFace;
                if (x != null)
                {
                    bool b1 = PointModel.AreEqual(x.FaceNormal.AngleTo(XYZ.BasisZ), Math.PI / 2);
                    //bool b2 = x.FaceNormal.AngleTo((-1) * XYZ.BasisX) >= 0 && x.FaceNormal.AngleTo((-1) * XYZ.BasisX) < Math.PI / 4 && x.FaceNormal.AngleTo(XYZ.BasisY) > Math.PI / 4;
                    bool b3 = PointModel.AreEqual(x.FaceNormal.AngleTo(line.Direction), Math.PI);
                    if (b1  && b3)
                    {
                        planarFace = x;
                       
                    }
                }
            }
            return planarFace;
        }
        public static PlanarFace GetEast(Element element)
        {
            Solid a = GetSolidOneElement(element);
            Line line = LineProcess.GetLineFromElement(element);
            FaceArray faceArray = a.Faces;
            PlanarFace planarFace = null;
            foreach (var item in faceArray)
            {
                PlanarFace x = item as PlanarFace;
                if (x != null)
                {
                    bool b1 = PointModel.AreEqual(x.FaceNormal.AngleTo(XYZ.BasisZ), Math.PI / 2);
                    //bool b2 = x.FaceNormal.AngleTo(XYZ.BasisX) >= 0 && x.FaceNormal.AngleTo(XYZ.BasisX) < Math.PI / 4 && x.FaceNormal.AngleTo((-1) * XYZ.BasisY) > Math.PI / 4;
                    bool b3 = PointModel.AreEqual(x.FaceNormal.AngleTo(line.Direction), 0);
                    if (b1  && b3)
                    {
                        planarFace = x;
                        
                    }
                }
            }
            return planarFace;
        }
        public static List<PlanarFace> GetAllPlanarFace(Element element)
        {
            Solid a = GetSolidOneElement(element);
            FaceArray faceArray = a.Faces;
            List<PlanarFace> planarFaces = new List<PlanarFace>();
            foreach (var item in faceArray)
            {
                PlanarFace x = item as PlanarFace;
                if (x != null )
                {
                    planarFaces.Add(x);
                }
            }
            return planarFaces;
        }
        public static List<PlanarFace> GetAllPerpencular(Element element)
        {
            Solid a = GetSolidOneElement(element);
            FaceArray faceArray = a.Faces;
            List<PlanarFace> planarFaces = new List<PlanarFace>();
            foreach (var item in faceArray)
            {
                PlanarFace x = item as PlanarFace;
                if (x != null && (PointModel.AreEqual(x.FaceNormal.AngleTo(XYZ.BasisZ), Math.PI / 2)))
                {
                    planarFaces.Add(x);
                }
            }
            return planarFaces;
        }
        public static List<PlanarFace> GetAllParallelZ(Element element)
        {
            Solid a = GetSolidOneElement(element);
            FaceArray faceArray = a.Faces;
            List<PlanarFace> planarFaces = new List<PlanarFace>();
            foreach (var item in faceArray)
            {
                PlanarFace x = item as PlanarFace;
                if (x != null && (PointModel.AreEqual(x.FaceNormal.AngleTo(XYZ.BasisZ), Math.PI) || PointModel.AreEqual(x.FaceNormal.AngleTo(XYZ.BasisZ), 0)))
                {
                    planarFaces.Add(x);
                }
            }
            return planarFaces;
        }
        public static List<CylindricalFace> GetAllCylindricalFace(Element element)
        {
            Solid a = GetSolidOneElement(element);
            FaceArray faceArray = a.Faces;
            List<CylindricalFace> cylindricalFaces = new List<CylindricalFace>();
            foreach (var item in faceArray)
            {
                CylindricalFace x = item as CylindricalFace;
                if (x != null)
                {
                    cylindricalFaces.Add(x);
                }
            }
            return cylindricalFaces;
        }
        public static List<PlanarFace> GetTopPlanarFaceBeam(Element beam, Document document)
        {
            Solid solid = SolidFace.GetSolidOneElement(beam);
            FaceArray faceArray = solid.Faces;
            List<PlanarFace> planarFaces = new List<PlanarFace>();
            foreach (var item1 in faceArray)
            {
                PlanarFace planarFace = item1 as PlanarFace;
                if (planarFace != null)
                {
                    if (PointModel.AreEqual(planarFace.FaceNormal.AngleTo(XYZ.BasisZ), 0) || PointModel.AreEqual(planarFace.FaceNormal.AngleTo(XYZ.BasisZ), Math.PI))
                    {
                        planarFaces.Add(planarFace);
                    }
                }
            }

            if (planarFaces.Count == 2)
            {
                planarFaces.OrderBy(x => x.Origin.Z);
            }
            return planarFaces;
        }
    }
}
