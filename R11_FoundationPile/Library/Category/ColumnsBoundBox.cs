using Autodesk.Revit.DB;
using System;
using System.Collections.Generic;
using System.Linq;

namespace R11_FoundationPile
{
    public class ColumnsBoundingBox
    {
        #region Columns and Beams Node
       
       
        public static List<Element> GetBeamsBoudingBoxSameTopLevelColumns(List<Element> columns, Document document,  string level)
        {
            List<Element> beams = new List<Element>();
            foreach (var item in columns)
            {
                beams.AddRange(GetBeamsBoudingBoxSameTopLevelPerpencularZOneColumn(item, document));
            }

            beams = beams.Distinct(new DistinctID()).ToList();
            return beams;
        }
        public static List<Element> GetBeamsBoudingBoxSameTopLevelPerpencularZOneColumn(Element column, Document document)
        {
            string level = column.get_Parameter(BuiltInParameter.LEVEL_PARAM).AsValueString();
            List<Element> beams = new List<Element>();

            ElementCategoryFilter categoryFilter
                       = new ElementCategoryFilter(BuiltInCategory.OST_StructuralFraming);
            BoundingBoxXYZ box = column.get_BoundingBox(null);
            Outline outline = new Outline(box.Min, box.Max);
            BoundingBoxIntersectsFilter bbFilter
                 = new BoundingBoxIntersectsFilter(outline);
            ElementIntersectsElementFilter elementIntersectsFilter = new ElementIntersectsElementFilter(column);
            LogicalAndFilter logicalAndFilter
                = new LogicalAndFilter(bbFilter, categoryFilter);
            
            List<Element> beam = new FilteredElementCollector(document).WherePasses(logicalAndFilter).ToList();
            foreach (var item in beam)
            {
                if (item.get_Parameter(BuiltInParameter.INSTANCE_REFERENCE_LEVEL_PARAM).AsValueString() == level)
                {
                    if (SolidFace.GetTopPlanarFaceBeam(item,document).Count!=0)
                    {
                        beams.Add(item);
                    }
                }
            }
            beams = beams.Distinct(new DistinctID()).ToList();
            
            return beams;
        }
        public static List<Element> GetBeamsBoudingBoxSameBottomLevelPerpencularZOneColumn(Element column, Document document)
        {
            string level = column.get_Parameter(BuiltInParameter.FAMILY_BASE_LEVEL_PARAM).AsValueString();
            List<Element> beams = new List<Element>();

            ElementCategoryFilter categoryFilter
                       = new ElementCategoryFilter(BuiltInCategory.OST_StructuralFraming);
            BoundingBoxXYZ box = column.get_BoundingBox(null);
            Outline outline = new Outline(box.Min, box.Max);
            BoundingBoxIntersectsFilter bbFilter
                 = new BoundingBoxIntersectsFilter(outline);
            LogicalAndFilter logicalAndFilter
                = new LogicalAndFilter(bbFilter, categoryFilter);
            List<Element> beam = new FilteredElementCollector(document).WherePasses(logicalAndFilter).ToList();
            foreach (var item in beam)
            {
                if (item.get_Parameter(BuiltInParameter.INSTANCE_REFERENCE_LEVEL_PARAM).AsValueString() == level)
                {
                    if (SolidFace.GetTopPlanarFaceBeam(item, document).Count != 0)
                    {
                        beams.Add(item);
                    }
                }
            }
            beams = beams.Distinct(new DistinctID()).ToList();
            return beams;
        }
        #endregion
        #region Foundation,floor and Wall
        public static Element GetFoundationBoudingBoxOneColumn(Element column, Document document)
        {
            List<Level> levels = new FilteredElementCollector(document).OfClass(typeof(Level)).Cast<Level>().ToList();
            levels.OrderBy(x => x.get_Parameter(BuiltInParameter.LEVEL_ELEV).AsDouble());
            Level level0 = levels[0];
            ElementId level = column.get_Parameter(BuiltInParameter.FAMILY_BASE_LEVEL_PARAM).AsElementId();
            if (!level.Equals(level0.Id))
            {
                return null;
            }
            ElementCategoryFilter categoryFilter
                       = new ElementCategoryFilter(BuiltInCategory.OST_StructuralFoundation);
            BoundingBoxXYZ box = column.get_BoundingBox(null);
            Outline outline = new Outline(box.Min, box.Max);
            BoundingBoxIntersectsFilter bbFilter
                 = new BoundingBoxIntersectsFilter(outline);

            LogicalAndFilter logicalAndFilter
                = new LogicalAndFilter(bbFilter, categoryFilter);
            Element element= new FilteredElementCollector(document).WherePasses(logicalAndFilter).Where(x => x.get_Parameter(BuiltInParameter.FAMILY_LEVEL_PARAM).AsElementId() == level).FirstOrDefault();
            if (element==null)
            {
                return null;
            }
            else
            {
                List<Solid> solids = SolidFace.GetListSolidOneElement(element);
                if (solids.Count != 1)
                {
                    return null;
                }
                else
                {
                    FaceArray faceArray = solids[0].Faces;
                    List<PlanarFace> planarFaces = new List<PlanarFace>();
                    foreach (var item in faceArray)
                    {
                        PlanarFace planarFace = item as PlanarFace;
                        if (planarFace != null)
                        {
                            if (PointModel.AreEqual(planarFace.FaceNormal.AngleTo(XYZ.BasisZ), 0) || PointModel.AreEqual(planarFace.FaceNormal.AngleTo(XYZ.BasisZ), Math.PI))
                            {
                                planarFaces.Add(planarFace);
                            }
                        }
                    }
                    if (planarFaces.Count == 0)
                    {
                        return null;
                    }
                    else
                    {
                        return element;
                    }
                }
            }
            
        }
        public static bool WallCompoundStructure(Wall wall)
        {
            WallType wallType = wall.WallType;
            CompoundStructure compound = wallType.GetCompoundStructure();
            return (compound.GetWidth() == compound.GetLayerWidth(compound.GetLastCoreLayerIndex()));
        }
        public static Element GetWallBoudingBoxOneColumn(Element column, Document document)
        {
            List<Level> levels = new FilteredElementCollector(document).OfClass(typeof(Level)).Cast<Level>().ToList();
            levels.OrderBy(x => x.get_Parameter(BuiltInParameter.LEVEL_ELEV).AsDouble());
            Level level0 = levels[0];
            ElementId level = column.get_Parameter(BuiltInParameter.FAMILY_BASE_LEVEL_PARAM).AsElementId();
            if (!level.Equals(level0))
            {
                return null;
            }
            List<Element> walls = new List<Element>();
            ElementCategoryFilter categoryFilter
                       = new ElementCategoryFilter(BuiltInCategory.OST_Walls);
            BoundingBoxXYZ box = column.get_BoundingBox(null);
            Outline outline = new Outline(box.Min, box.Max);
            BoundingBoxIntersectsFilter bbFilter
                 = new BoundingBoxIntersectsFilter(outline);

            LogicalAndFilter logicalAndFilter
                = new LogicalAndFilter(bbFilter, categoryFilter);
            Element element = new FilteredElementCollector(document).WherePasses(logicalAndFilter)
                .Where(x => x.get_Parameter(BuiltInParameter.WALL_HEIGHT_TYPE).AsElementId() == level)
                .Where(x=>x.get_Parameter(BuiltInParameter.WALL_STRUCTURAL_SIGNIFICANT).AsInteger()==1)
                .FirstOrDefault();
            if (element == null)
            {
                return null;
            }
            else
            {
                List<Solid> solids = SolidFace.GetListSolidOneElement(element);
                if (solids.Count != 1)
                {
                    return null;
                }
                else
                {
                    FaceArray faceArray = solids[0].Faces;
                    List<PlanarFace> planarFaces = new List<PlanarFace>();
                    foreach (var item in faceArray)
                    {
                        PlanarFace planarFace = item as PlanarFace;
                        if (planarFace != null)
                        {
                            if (PointModel.AreEqual(planarFace.FaceNormal.AngleTo(XYZ.BasisZ), 0) || PointModel.AreEqual(planarFace.FaceNormal.AngleTo(XYZ.BasisZ), Math.PI))
                            {
                                planarFaces.Add(planarFace);
                            }
                        }
                    }
                    if (planarFaces.Count != 2)
                    {
                        return null;
                    }
                    else
                    {
                        return element;
                    }
                }
            }
        }
        public static Element GetFloorBoudingBoxOneColumn(Element column, Document document)
        {
            List<Level> levels = new FilteredElementCollector(document).OfClass(typeof(Level)).Cast<Level>().ToList();
            levels.OrderBy(x => x.get_Parameter(BuiltInParameter.LEVEL_ELEV).AsDouble());
            Level level0 = levels[0];
            ElementId level = column.get_Parameter(BuiltInParameter.FAMILY_BASE_LEVEL_PARAM).AsElementId();
            if (!level.Equals(level0.Id))
            {
                return null;
            }
            List<Element> walls = new List<Element>();
            ElementCategoryFilter categoryFilter
                       = new ElementCategoryFilter(BuiltInCategory.OST_Floors);
            BoundingBoxXYZ box = column.get_BoundingBox(null);
            Outline outline = new Outline(box.Min, box.Max);
            BoundingBoxIntersectsFilter bbFilter
                 = new BoundingBoxIntersectsFilter(outline);

            LogicalAndFilter logicalAndFilter
                = new LogicalAndFilter(bbFilter, categoryFilter);
            Element element = new FilteredElementCollector(document).WherePasses(logicalAndFilter)
                .Where(x => x.get_Parameter(BuiltInParameter.LEVEL_PARAM).AsElementId() == level)
                .Where(x => x.get_Parameter(BuiltInParameter.FLOOR_PARAM_IS_STRUCTURAL).AsInteger() == 1)
                .FirstOrDefault();
            if (element == null)
            {
                
                return null;
            }
            else
            {
                List<Solid> solids = SolidFace.GetListSolidOneElement(element);
                if (solids.Count != 1)
                {
                    return null;
                }
                else
                {
                    FaceArray faceArray = solids[0].Faces;
                    List<PlanarFace> planarFaces = new List<PlanarFace>();
                    foreach (var item in faceArray)
                    {
                        PlanarFace planarFace = item as PlanarFace;
                        if (planarFace != null)
                        {
                            if (PointModel.AreEqual(planarFace.FaceNormal.AngleTo(XYZ.BasisZ), 0) || PointModel.AreEqual(planarFace.FaceNormal.AngleTo(XYZ.BasisZ), Math.PI))
                            {
                                planarFaces.Add(planarFace);
                            }
                        }
                    }
                    if (planarFaces.Count != 2)
                    {
                        return null;
                    }
                    else
                    {
                        return element;
                    }
                }
            }
        }

        #endregion
    }
}
