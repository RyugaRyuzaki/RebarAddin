using Autodesk.Revit.DB;
using System;
using System.Collections.Generic;
using System.Linq;

namespace R02_BeamsRebar
{
    public class BeamsBoundBox
    {
        #region Columns and Beams Node
        public static List<Element> GetColumnsBoudingBoxOneBeam(Element beam, Document document)
        {
            List<Element> columns = new List<Element>();


            ElementCategoryFilter categoryFilter
                       = new ElementCategoryFilter(BuiltInCategory.OST_StructuralColumns);
            BoundingBoxXYZ box = beam.get_BoundingBox(document.ActiveView);
            Outline outline = new Outline(box.Min, box.Max);
            BoundingBoxIntersectsFilter bbFilter
                 = new BoundingBoxIntersectsFilter(outline);

            LogicalAndFilter logicalAndFilter
                = new LogicalAndFilter(bbFilter, categoryFilter);
            List<Element> column = new FilteredElementCollector(document, document.ActiveView.Id).WherePasses(logicalAndFilter).ToList();
            foreach (var col in column)
            {
                columns.Add(col);
            }

            columns = columns.Distinct(new DistinctID()).ToList();
            return columns;
        }
        public static List<Element> GetBeamsBoudingBoxOneBeam(Element beam, Document document)
        {
            List<Element> beams = new List<Element>();


            ElementCategoryFilter categoryFilter
                       = new ElementCategoryFilter(BuiltInCategory.OST_StructuralFraming);
            BoundingBoxXYZ box = beam.get_BoundingBox(document.ActiveView);
            Outline outline = new Outline(box.Min, box.Max);
            BoundingBoxIntersectsFilter bbFilter
                 = new BoundingBoxIntersectsFilter(outline);

            LogicalAndFilter logicalAndFilter
                = new LogicalAndFilter(bbFilter, categoryFilter);
            List<ElementId> excluse = new List<ElementId>();
            excluse.Add(beam.Id);
            ExclusionFilter exclusionFilter = new ExclusionFilter(excluse);
            List<Element> b = new FilteredElementCollector(document, document.ActiveView.Id).WherePasses(logicalAndFilter).WherePasses(exclusionFilter).ToList();
            foreach (var col in b)
            {
                beams.Add(col);
            }

            beams = beams.Distinct(new DistinctID()).ToList();
            List<Element> beams1 = new List<Element>();
            Line l = LineProcess.GetLineFromElement(beam);
            foreach (var item in beams)
            {
                Line l0 = LineProcess.GetLineFromElement(item);
                double x = l0.Direction.AngleTo(l.Direction);
                if (!(x < 1e-9 || Math.Abs(x - Math.PI) < 1e-9))
                {
                    beams1.Add(item);
                }
            }
            beams1 = beams1.Distinct(new DistinctID()).ToList();
            return beams1;
        }
        public static List<Element> GetColumnsBoudingBoxBeams(List<Element> beams, Document document)
        {
            List<Element> columns = new List<Element>();
            foreach (var item in beams)
            {
                ElementCategoryFilter categoryFilter
                           = new ElementCategoryFilter(BuiltInCategory.OST_StructuralColumns);
                BoundingBoxXYZ box = item.get_BoundingBox(document.ActiveView);
                Outline outline = new Outline(box.Min, box.Max);
                BoundingBoxIntersectsFilter bbFilter
                     = new BoundingBoxIntersectsFilter(outline);

                LogicalAndFilter logicalAndFilter
                    = new LogicalAndFilter(bbFilter, categoryFilter);
                List<Element> column = new FilteredElementCollector(document, document.ActiveView.Id).WherePasses(logicalAndFilter).ToList();
                foreach (var col in column)
                {
                    columns.Add(col);
                }
            }
            columns = columns.Distinct(new DistinctID()).ToList();
            return columns;
        }
        public static List<Element> GetBeamsBoudingBoxColumns(List<Element> columns, Document document)
        {
            List<Element> beams = new List<Element>();
            foreach (var item in columns)
            {
                ElementCategoryFilter categoryFilter
                           = new ElementCategoryFilter(BuiltInCategory.OST_StructuralFraming);
                BoundingBoxXYZ box = item.get_BoundingBox(document.ActiveView);
                Outline outline = new Outline(box.Min, box.Max);
                BoundingBoxIntersectsFilter bbFilter
                     = new BoundingBoxIntersectsFilter(outline);

                LogicalAndFilter logicalAndFilter
                    = new LogicalAndFilter(bbFilter, categoryFilter);
                List<Element> beam = new FilteredElementCollector(document, document.ActiveView.Id).WherePasses(logicalAndFilter).ToList();
                foreach (var b in beam)
                {
                    beams.Add(b);
                }
            }
            beams = beams.Distinct(new DistinctID()).ToList();
            return beams;
        }
        public static List<Element> GetBeamsBoudingBoxSameTopLevelColumns(List<Element> columns, Document document, Line l, string level)
        {
            List<Element> beams = new List<Element>();
            foreach (var item in columns)
            {
                beams.AddRange(GetBeamsBoudingBoxSameTopLevelOneColumn(item, document, l, level));
            }

            beams = beams.Distinct(new DistinctID()).ToList();
            return beams;
        }
        public static List<Element> GetBeamsBoudingBoxSameTopLevelOneColumn(Element column, Document document, Line l, string level)
        {
            List<Element> beams = new List<Element>();

            ElementCategoryFilter categoryFilter
                       = new ElementCategoryFilter(BuiltInCategory.OST_StructuralFraming);
            BoundingBoxXYZ box = column.get_BoundingBox(document.ActiveView);
            Outline outline = new Outline(box.Min, box.Max);
            BoundingBoxIntersectsFilter bbFilter
                 = new BoundingBoxIntersectsFilter(outline);

            LogicalAndFilter logicalAndFilter
                = new LogicalAndFilter(bbFilter, categoryFilter);
            List<Element> beam = new FilteredElementCollector(document, document.ActiveView.Id).WherePasses(logicalAndFilter).ToList();
            foreach (var item in beam)
            {
                Line l0 = LineProcess.GetLineFromElement(item);
                double x = l0.Direction.AngleTo(l.Direction);
                if (!(x < 1e-9 || Math.Abs(x - Math.PI) < 1e-9))
                {
                    if (item.get_Parameter(BuiltInParameter.INSTANCE_REFERENCE_LEVEL_PARAM).AsValueString() == level)
                    {
                        beams.Add(item);
                    }
                }
            }

            beams = beams.Distinct(new DistinctID()).ToList();
            return beams;
        }
        public static List<Element> GetBeamsBoudingBoxBeams(List<Element> beamsSelection, Document document)
        {
            List<Element> beams = new List<Element>();
            foreach (var item in beamsSelection)
            {
                ElementCategoryFilter categoryFilter
                           = new ElementCategoryFilter(BuiltInCategory.OST_StructuralFraming);
                BoundingBoxXYZ box = item.get_BoundingBox(document.ActiveView);
                Outline outline = new Outline(box.Min, box.Max);
                BoundingBoxIntersectsFilter bbFilter
                     = new BoundingBoxIntersectsFilter(outline);

                LogicalAndFilter logicalAndFilter
                    = new LogicalAndFilter(bbFilter, categoryFilter);
                List<ElementId> excluse = new List<ElementId>();
                excluse.Add(item.Id);
                ExclusionFilter exclusionFilter = new ExclusionFilter(excluse);
                List<Element> beam = new FilteredElementCollector(document, document.ActiveView.Id).WherePasses(logicalAndFilter).WherePasses(exclusionFilter).ToList();
                foreach (var b in beam)
                {
                    beams.Add(b);
                }
            }
            beams = beams.Distinct(new DistinctID()).ToList();
            List<Element> beams1 = new List<Element>();
            Line l = LineProcess.GetLineFromElement(beamsSelection[0]);
            foreach (var item in beams)
            {
                Line l0 = LineProcess.GetLineFromElement(item);
                double x = l0.Direction.AngleTo(l.Direction);
                if (!(x < 1e-9 || Math.Abs(x - Math.PI) < 1e-9))
                {
                    beams1.Add(item);
                }
            }
            beams1 = beams1.Distinct(new DistinctID()).ToList();
            return beams1;
        }
        public static List<Element> GetColumnsSameBaseLevelBeams(List<Element> columns, string level)
        {
            List<Element> columnsLevel = new List<Element>();
            foreach (var item in columns)
            {
                if (item.get_Parameter(BuiltInParameter.FAMILY_BASE_LEVEL_PARAM).AsValueString() == level)
                {
                    columnsLevel.Add(item);
                }
            }
            columnsLevel = columnsLevel.OrderBy(c => (c.Location as LocationPoint).Point.X)
                .ThenBy(c => (c.Location as LocationPoint).Point.Y)
                .ToList();
            return columnsLevel;
        }
        public static List<Element> GetColumnsSameTopLevelBeams(List<Element> columns, string level)
        {
            List<Element> columnsLevel = new List<Element>();
            foreach (var item in columns)
            {
                if (item.get_Parameter(BuiltInParameter.FAMILY_TOP_LEVEL_PARAM).AsValueString() == level)
                {
                    columnsLevel.Add(item);
                }
            }
            columnsLevel = columnsLevel.OrderBy(c => (c.Location as LocationPoint).Point.X)
                .ThenBy(c => (c.Location as LocationPoint).Point.Y)
                .ToList();
            return columnsLevel;
        }
        public static List<Element> GetBeamsPerNodeBeams(List<Element> beams, Document document)
        {
            List<Element> beamsPer = new List<Element>();
            foreach (var item in beams)
            {
                List<Element> beamx = GetBeamsBoudingBoxOneBeam(item, document);
                ElementType elementTypeitem = document.GetElement(item.GetTypeId()) as ElementType;
                double bitem = double.Parse(UnitFormatUtils.Format(document.GetUnits(), SpecTypeId.Length, elementTypeitem.LookupParameter("h").AsDouble(), false));
                double bitemzoffset = Math.Abs(double.Parse(UnitFormatUtils.Format(document.GetUnits(), SpecTypeId.Length, item.get_Parameter(BuiltInParameter.Z_OFFSET_VALUE).AsDouble(), false)));


                foreach (var i in beamx)
                {
                    ElementType elementTypei = document.GetElement(i.GetTypeId()) as ElementType;
                    double bi = double.Parse(UnitFormatUtils.Format(document.GetUnits(), SpecTypeId.Length, elementTypei.LookupParameter("h").AsDouble(), false));
                    double bizoffset = Math.Abs(double.Parse(UnitFormatUtils.Format(document.GetUnits(), SpecTypeId.Length, i.get_Parameter(BuiltInParameter.Z_OFFSET_VALUE).AsDouble(), false)));
                    if (bitem + bitemzoffset <= bi + bizoffset)
                    {
                        beamsPer.Add(i);

                    }

                }
            }
            beamsPer = beamsPer.Distinct(new DistinctID()).ToList();
            return beamsPer;
        }
        public static List<Element> GetBeamsPerNoneNodeBeams(List<Element> beams, Document document)
        {
            List<Element> beamsPer = new List<Element>();
            foreach (var item in beams)
            {
                List<Element> beamx = GetBeamsBoudingBoxOneBeam(item, document);
                ElementType elementTypeitem = document.GetElement(item.GetTypeId()) as ElementType;
                double bitem = double.Parse(UnitFormatUtils.Format(document.GetUnits(), SpecTypeId.Length, elementTypeitem.LookupParameter("h").AsDouble(), false));
                double bitemzoffset = Math.Abs(double.Parse(UnitFormatUtils.Format(document.GetUnits(), SpecTypeId.Length, item.get_Parameter(BuiltInParameter.Z_OFFSET_VALUE).AsDouble(), false)));


                foreach (var i in beamx)
                {
                    ElementType elementTypei = document.GetElement(i.GetTypeId()) as ElementType;
                    double bi = double.Parse(UnitFormatUtils.Format(document.GetUnits(), SpecTypeId.Length, elementTypei.LookupParameter("h").AsDouble(), false));
                    double bizoffset = Math.Abs(double.Parse(UnitFormatUtils.Format(document.GetUnits(), SpecTypeId.Length, i.get_Parameter(BuiltInParameter.Z_OFFSET_VALUE).AsDouble(), false)));
                    if (bitem + bitemzoffset > bi + bizoffset)
                    {
                        beamsPer.Add(i);

                    }

                }
            }
            beamsPer = beamsPer.Distinct(new DistinctID()).ToList();
            return beamsPer;
        }
        public static List<Element> GetBeamsPerNodeOneBeam(Element beam, Document document)
        {
            List<Element> beamsPer = new List<Element>();
            List<Element> beamx = GetBeamsBoudingBoxOneBeam(beam, document);
            ElementType elementTypeitem = document.GetElement(beam.GetTypeId()) as ElementType;
            double bitem = double.Parse(UnitFormatUtils.Format(document.GetUnits(), SpecTypeId.Length, elementTypeitem.LookupParameter("h").AsDouble(), false));
            double bitemzoffset = Math.Abs(double.Parse(UnitFormatUtils.Format(document.GetUnits(), SpecTypeId.Length, beam.get_Parameter(BuiltInParameter.Z_OFFSET_VALUE).AsDouble(), false)));
            foreach (var i in beamx)
            {
                ElementType elementTypei = document.GetElement(i.GetTypeId()) as ElementType;
                double bi = double.Parse(UnitFormatUtils.Format(document.GetUnits(), SpecTypeId.Length, elementTypei.LookupParameter("h").AsDouble(), false));
                double bizoffset = Math.Abs(double.Parse(UnitFormatUtils.Format(document.GetUnits(), SpecTypeId.Length, i.get_Parameter(BuiltInParameter.Z_OFFSET_VALUE).AsDouble(), false)));
                if (bitem + bitemzoffset <= bi + bizoffset)
                {
                    beamsPer.Add(i);
                }
            }

            beamsPer = beamsPer.Distinct(new DistinctID()).ToList();
            return beamsPer;
        }
        public static List<Element> GetBeamsPerNoneNodeOneBeam(Element beam, Document document)
        {
            List<Element> beamsPer = new List<Element>();
            List<Element> beamx = GetBeamsBoudingBoxOneBeam(beam, document);
            ElementType elementTypeitem = document.GetElement(beam.GetTypeId()) as ElementType;
            double bitem = double.Parse(UnitFormatUtils.Format(document.GetUnits(), SpecTypeId.Length, elementTypeitem.LookupParameter("h").AsDouble(), false));
            double bitemzoffset = Math.Abs(double.Parse(UnitFormatUtils.Format(document.GetUnits(), SpecTypeId.Length, beam.get_Parameter(BuiltInParameter.Z_OFFSET_VALUE).AsDouble(), false)));
            foreach (var i in beamx)
            {
                ElementType elementTypei = document.GetElement(i.GetTypeId()) as ElementType;
                double bi = double.Parse(UnitFormatUtils.Format(document.GetUnits(), SpecTypeId.Length, elementTypei.LookupParameter("h").AsDouble(), false));
                double bizoffset = Math.Abs(double.Parse(UnitFormatUtils.Format(document.GetUnits(), SpecTypeId.Length, i.get_Parameter(BuiltInParameter.Z_OFFSET_VALUE).AsDouble(), false)));
                if (bitem + bitemzoffset > bi + bizoffset)
                {
                    beamsPer.Add(i);
                }
            }

            beamsPer = beamsPer.Distinct(new DistinctID()).ToList();
            return beamsPer;
        }
        public static List<Element> GetSpecialBeams(Element beam, Document document, Line l, string level)
        {
            List<Element> beamPer = new List<Element>();
            List<Element> beamNone = GetBeamsPerNoneNodeOneBeam(beam, document);
            List<Element> column1 = GetColumnsBoudingBoxOneBeam(beam, document);
            List<Element> columnLevel1 = GetColumnsSameTopLevelBeams(column1, level);
            List<Element> beam1 = GetBeamsBoudingBoxSameTopLevelColumns(column1, document, l, level);
            beamNone.AddRange(beam1);
            beamNone = RemoveList(beamNone, beam1, l);
            beamPer.AddRange(beamNone);
            beamPer = beamPer.Distinct(new DistinctID()).ToList();
            return LineProcess.ArrangeBeams(beamPer, l);
        }
        public static List<Element> GetAllSpecialBeams(List<Element> beams, Document document, Line l, string level)
        {
            List<Element> beamPer = new List<Element>();
            foreach (var item in beams)
            {
                List<Element> beamNone = GetBeamsPerNoneNodeOneBeam(item, document);
                List<Element> column1 = GetColumnsBoudingBoxOneBeam(item, document);
                List<Element> columnLevel1 = GetColumnsSameTopLevelBeams(column1, level);
                List<Element> beam1 = GetBeamsBoudingBoxSameTopLevelColumns(column1, document, l, level);
                beamNone.AddRange(beam1);
                beamNone = RemoveList(beamNone, beam1, l);
                beamPer.AddRange(beamNone);
            }
            
            beamPer = beamPer.Distinct(new DistinctID()).ToList();
            return LineProcess.ArrangeBeams(beamPer, l);
        }
        public static List<Element> GetSpacialColumns(Element beam, Document document, string level)
        {
            List<Element> columns = GetColumnsBoudingBoxOneBeam(beam, document);
            List<Element> columnsBaseLevel = GetColumnsSameBaseLevelBeams(columns, level);
            List<Element> columnsTopLevel = GetColumnsSameTopLevelBeams(columns, level);
            List<Element> columnsA = GetColumnsBoudingBoxBeams(columnsTopLevel, document);
            List<Element> ColumnsBaseLevelA = GetColumnsSameBaseLevelBeams(columnsA, level);
            columnsBaseLevel.RemoveAll(x => ColumnsBaseLevelA.Any(y => y.Id == x.Id));
            return columnsBaseLevel;
        }
        public static List<Element> GetAllSpacialColumns(List<Element> beams, Document document, string level)
        {
            List<Element> columns = GetColumnsBoudingBoxBeams(beams, document);
            List<Element> columnsBaseLevel = GetColumnsSameBaseLevelBeams(columns, level);
            List<Element> columnsTopLevel = GetColumnsSameTopLevelBeams(columns, level);
            List<Element> columnsA = GetColumnsBoudingBoxBeams(columnsTopLevel, document);
            List<Element> ColumnsBaseLevelA = GetColumnsSameBaseLevelBeams(columnsA, level);
            columnsBaseLevel.RemoveAll(x => ColumnsBaseLevelA.Any(y => y.Id == x.Id));
            return columnsBaseLevel;
        }
        public static List<Element> RemoveList(List<Element> a, List<Element> b, Line l)
        {
            a.RemoveAll(x => b.Any(y => y.Id == x.Id));
            return LineProcess.ArrangeBeams(a, l);
        }
        #endregion
        #region Foundation,floor and Wall
        public static List<Element> GetFloorBoudingBoxBeams(List<Element> beams, Document document)
        {
            List<Element> floor = new List<Element>();
            foreach (var item in beams)
            {
                ElementCategoryFilter categoryFilter
                           = new ElementCategoryFilter(BuiltInCategory.OST_Floors);
                BoundingBoxXYZ box = item.get_BoundingBox(document.ActiveView);
                Outline outline = new Outline(box.Min, box.Max);
                BoundingBoxIntersectsFilter bbFilter
                     = new BoundingBoxIntersectsFilter(outline);

                LogicalAndFilter logicalAndFilter
                    = new LogicalAndFilter(bbFilter, categoryFilter);
                List<Element> column = new FilteredElementCollector(document, document.ActiveView.Id).WherePasses(logicalAndFilter).ToList();
                foreach (var col in column)
                {
                    floor.Add(col);
                }
            }
            floor = floor.Distinct(new DistinctID()).ToList();
            return floor;
        }
        public static List<Element> GetFloorBoudingBoxOneBeam(Element beam, Document document)
        {
            List<Element> floor = new List<Element>();
            ElementCategoryFilter categoryFilter
                       = new ElementCategoryFilter(BuiltInCategory.OST_Floors);
            BoundingBoxXYZ box = beam.get_BoundingBox(document.ActiveView);
            Outline outline = new Outline(box.Min, box.Max);
            BoundingBoxIntersectsFilter bbFilter
                 = new BoundingBoxIntersectsFilter(outline);

            LogicalAndFilter logicalAndFilter
                = new LogicalAndFilter(bbFilter, categoryFilter);
            List<Element> column = new FilteredElementCollector(document, document.ActiveView.Id).WherePasses(logicalAndFilter).ToList();
            foreach (var col in column)
            {
                floor.Add(col);
            }
            floor = floor.Distinct(new DistinctID()).ToList();
            return floor;
        }
        public static List<Element> GetFoundationBoudingBoxBeams(List<Element> beams, Document document)
        {
            List<Element> Foundation = new List<Element>();
            foreach (var item in beams)
            {
                ElementCategoryFilter categoryFilter
                           = new ElementCategoryFilter(BuiltInCategory.OST_StructuralFoundation);
                BoundingBoxXYZ box = item.get_BoundingBox(document.ActiveView);
                Outline outline = new Outline(box.Min, box.Max);
                BoundingBoxIntersectsFilter bbFilter
                     = new BoundingBoxIntersectsFilter(outline);

                LogicalAndFilter logicalAndFilter
                    = new LogicalAndFilter(bbFilter, categoryFilter);
                List<Element> column = new FilteredElementCollector(document, document.ActiveView.Id).WherePasses(logicalAndFilter).ToList();
                foreach (var col in column)
                {
                    Foundation.Add(col);
                }
            }
            Foundation = Foundation.Distinct(new DistinctID()).ToList();
            return Foundation;
        }
        public static List<Element> GetFoundationBoudingBoxOneBeam(Element beam, Document document)
        {
            List<Element> Foundation = new List<Element>();
            ElementCategoryFilter categoryFilter
                       = new ElementCategoryFilter(BuiltInCategory.OST_StructuralFoundation);
            BoundingBoxXYZ box = beam.get_BoundingBox(document.ActiveView);
            Outline outline = new Outline(box.Min, box.Max);
            BoundingBoxIntersectsFilter bbFilter
                 = new BoundingBoxIntersectsFilter(outline);

            LogicalAndFilter logicalAndFilter
                = new LogicalAndFilter(bbFilter, categoryFilter);
            List<Element> column = new FilteredElementCollector(document, document.ActiveView.Id).WherePasses(logicalAndFilter).ToList();
            foreach (var col in column)
            {
                Foundation.Add(col);
            }
            Foundation = Foundation.Distinct(new DistinctID()).ToList();
            return Foundation;
        }
        public static List<Element> GetWallBoudingBoxBeams(List<Element> beams, Document document)
        {
            List<Element> walls = new List<Element>();
            foreach (var item in beams)
            {
                ElementCategoryFilter categoryFilter
                           = new ElementCategoryFilter(BuiltInCategory.OST_Walls);
                BoundingBoxXYZ box = item.get_BoundingBox(document.ActiveView);
                Outline outline = new Outline(box.Min, box.Max);
                BoundingBoxIntersectsFilter bbFilter
                     = new BoundingBoxIntersectsFilter(outline);

                LogicalAndFilter logicalAndFilter
                    = new LogicalAndFilter(bbFilter, categoryFilter);
                List<Element> column = new FilteredElementCollector(document).WherePasses(logicalAndFilter).ToList();
                foreach (var col in column)
                {
                    walls.Add(col);
                }
            }
            walls = walls.Distinct(new DistinctID()).ToList();
            return walls;
        }
        public static List<Element> GetWallBoudingBoxOneBeam(Element beam, Document document)
        {
            List<Element> walls = new List<Element>();
            ElementCategoryFilter categoryFilter
                       = new ElementCategoryFilter(BuiltInCategory.OST_Walls);
            BoundingBoxXYZ box = beam.get_BoundingBox(document.ActiveView);
            Outline outline = new Outline(box.Min, box.Max);
            BoundingBoxIntersectsFilter bbFilter
                 = new BoundingBoxIntersectsFilter(outline);

            LogicalAndFilter logicalAndFilter
                = new LogicalAndFilter(bbFilter, categoryFilter);
            List<Element> column = new FilteredElementCollector(document).WherePasses(logicalAndFilter).ToList();
            foreach (var col in column)
            {
                walls.Add(col);
            }
            walls = walls.Distinct(new DistinctID()).ToList();
            return walls;
        }
        public static List<Element> GetWallBoudingBoxSameTopLevel(List<Element> wallElement, Document document, ElementId level)
        {
            List<Element> walls = new List<Element>();
            for (int i = 0; i < wallElement.Count; i++)
            {
                for (int j = 0; j < wallElement.Count; j++)
                {
                    Wall a = wallElement[j] as Wall;
                    if (WallStructural(a) && WallSameTopLevel(a, level) && WallCompoundStructure(a))
                    {
                        walls.Add(wallElement[j]);
                    }
                }

            }
            walls = walls.Distinct(new DistinctID()).ToList();
            return walls;
        }
        public static bool WallStructural(Wall wall)
        {
            return (wall.get_Parameter(BuiltInParameter.WALL_STRUCTURAL_SIGNIFICANT).AsInteger() == 1);
        }
        public static bool WallSameTopLevel(Wall wall, ElementId level)
        {

            return (wall.get_Parameter(BuiltInParameter.WALL_HEIGHT_TYPE).AsElementId().Equals(level));
        }
        public static bool WallCompoundStructure(Wall wall)
        {
            WallType wallType = wall.WallType;
            CompoundStructure compound = wallType.GetCompoundStructure();
            return (compound.GetWidth() == compound.GetLayerWidth(compound.GetLastCoreLayerIndex()));
        }
        public static bool WallJointBeam(Wall wall, Element beam, Document document)
        {
            System.Windows.Forms.MessageBox.Show(JoinGeometryUtils.AreElementsJoined(document, wall, beam) + "");
            return (!JoinGeometryUtils.AreElementsJoined(document, wall, beam) && JoinGeometryUtils.IsCuttingElementInJoin(document, wall, beam));
        }
        public static List<Element> GetBeamsBoudingBoxSameTopLevelFoundation(List<Element> foundations, Document document, Line l, string level)
        {
            List<Element> beams = new List<Element>();
            foreach (var item in foundations)
            {
                beams.AddRange(GetBeamsBoudingBoxSameTopLevelOneFoundation(item, document, l, level));
            }

            beams = beams.Distinct(new DistinctID()).ToList();
            return beams;
        }
        public static List<Element> GetBeamsBoudingBoxSameTopLevelOneFoundation(Element foundation, Document document, Line l, string level)
        {
            List<Element> beams = new List<Element>();

            ElementCategoryFilter categoryFilter
                       = new ElementCategoryFilter(BuiltInCategory.OST_StructuralFraming);
            BoundingBoxXYZ box = foundation.get_BoundingBox(document.ActiveView);
            Outline outline = new Outline(box.Min, box.Max);
            BoundingBoxIntersectsFilter bbFilter
                 = new BoundingBoxIntersectsFilter(outline);

            LogicalAndFilter logicalAndFilter
                = new LogicalAndFilter(bbFilter, categoryFilter);
            List<Element> beam = new FilteredElementCollector(document, document.ActiveView.Id).WherePasses(logicalAndFilter).ToList();
            foreach (var item in beam)
            {
                Line l0 = LineProcess.GetLineFromElement(item);
                double x = l0.Direction.AngleTo(l.Direction);
                if (!(x < 1e-9 || Math.Abs(x - Math.PI) < 1e-9))
                {
                    if (item.get_Parameter(BuiltInParameter.INSTANCE_REFERENCE_LEVEL_PARAM).AsValueString() == level)
                    {
                        beams.Add(item);
                    }
                }
            }

            beams = beams.Distinct(new DistinctID()).ToList();
            return beams;
        }
        public static List<Element> GetBeamsBoudingBoxSameTopLevelFloor(List<Element> floors, Document document, Line l, string level)
        {
            List<Element> beams = new List<Element>();
            foreach (var item in floors)
            {
                beams.AddRange(GetBeamsBoudingBoxSameTopLevelOneFloor(item, document, l, level));
            }

            beams = beams.Distinct(new DistinctID()).ToList();
            return beams;
        }
        public static List<Element> GetBeamsBoudingBoxSameTopLevelOneFloor(Element floor, Document document, Line l, string level)
        {
            List<Element> beams = new List<Element>();

            ElementCategoryFilter categoryFilter
                       = new ElementCategoryFilter(BuiltInCategory.OST_StructuralFraming);
            BoundingBoxXYZ box = floor.get_BoundingBox(document.ActiveView);
            Outline outline = new Outline(box.Min, box.Max);
            BoundingBoxIntersectsFilter bbFilter
                 = new BoundingBoxIntersectsFilter(outline);

            LogicalAndFilter logicalAndFilter
                = new LogicalAndFilter(bbFilter, categoryFilter);
            List<Element> beam = new FilteredElementCollector(document, document.ActiveView.Id).WherePasses(logicalAndFilter).ToList();
            foreach (var item in beam)
            {
                Line l0 = LineProcess.GetLineFromElement(item);
                double x = l0.Direction.AngleTo(l.Direction);
                if (!(x < 1e-9 || Math.Abs(x - Math.PI) < 1e-9))
                {
                    if (item.get_Parameter(BuiltInParameter.INSTANCE_REFERENCE_LEVEL_PARAM).AsValueString() == level)
                    {
                        beams.Add(item);
                    }
                }
            }

            beams = beams.Distinct(new DistinctID()).ToList();
            return beams;
        }
        public static List<Element> GetBeamsBoudingBoxSameTopLevelWalls(List<Element> walls, Document document, Line l, string level)
        {
            List<Element> beams = new List<Element>();
            foreach (var item in walls)
            {
                beams.AddRange(GetBeamsBoudingBoxSameTopLevelOneWall(item, document, l, level));
            }

            beams = beams.Distinct(new DistinctID()).ToList();
            return beams;
        }
        public static List<Element> GetBeamsBoudingBoxSameTopLevelOneWall(Element wall, Document document, Line l, string level)
        {
            List<Element> beams = new List<Element>();

            ElementCategoryFilter categoryFilter
                       = new ElementCategoryFilter(BuiltInCategory.OST_StructuralFraming);
            BoundingBoxXYZ box = wall.get_BoundingBox(document.ActiveView);
            Outline outline = new Outline(box.Min, box.Max);
            BoundingBoxIntersectsFilter bbFilter
                 = new BoundingBoxIntersectsFilter(outline);

            LogicalAndFilter logicalAndFilter
                = new LogicalAndFilter(bbFilter, categoryFilter);
            List<Element> beam = new FilteredElementCollector(document, document.ActiveView.Id).WherePasses(logicalAndFilter).ToList();
            foreach (var item in beam)
            {
                Line l0 = LineProcess.GetLineFromElement(item);
                double x = l0.Direction.AngleTo(l.Direction);
                if (!(x < 1e-9 || Math.Abs(x - Math.PI) < 1e-9))
                {
                    if (item.get_Parameter(BuiltInParameter.INSTANCE_REFERENCE_LEVEL_PARAM).AsValueString() == level)
                    {
                        beams.Add(item);
                    }
                }
            }

            beams = beams.Distinct(new DistinctID()).ToList();
            return beams;
        }
        #endregion
    }
}
