
using Autodesk.Revit.DB;
using System.Collections.Generic;

namespace R04_SlabsRebar
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
                    GeometryElement geometryElement1 = geometryInstance.GetInstanceGeometry();
                    foreach (GeometryObject geometryObject1 in geometryElement1)
                    {
                        Solid solid1 = geometryObject as Solid;
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
    }
}
