using Autodesk.Revit.DB;
using System;
using System.Collections.Generic;
using System.Linq;
namespace R02_BeamsRebar
{
    public class ErrorBeams
    {
        public static int Error(List<Element> elements, Document document)
        {
            int a = 0;
            if (!CompareProperty(elements, document)) { a = 1; }
            if (!SameFamilyTpye(elements)) { a = 2; }
            if (!SameLevel(elements)) { a = 3; }
            if (!CompareWidthBeams(elements, document)) { a = 4; }
            if (!CompareZOffset(elements, document)) { a = 5; }
            if (!CompareZJustify(elements)) { a = 6; }
            if (!ComparePropertyBeams(elements, document)) { a = 7; }
            if (!ReactangleShape(elements)) { a = 8; }
            if (!CompareDirectionBeams(elements)) { a = 9; }
            if (!CompareSameDirectionBeams(elements)) { a = 10; }
            if (!OneSolidElement(elements)) { a = 11; }
            if (!CompareNoneNode(elements, document)) { a = 12; }
            if (!JointBeamToColumns(elements, document)) { a = 13; }
            if (!JointBeamToFloor(elements, document)) { a = 14; }
            if (!JointFloorToBeam(elements, document)) { a = 15; }
            if (!JointBeamToFoundation(elements, document)) { a = 16; }
            if (!JointBeamToWall(elements, document)) { a = 17; }
            if (!JointBeamNoneNodeToBeam(elements, document)) { a = 18; }
            if (!SlpitBeams(elements, document)) { a = 19; }
            if (!ContinuneBeams(elements, document)) { a = 20; }
            if (!CompareDecimal(document)) { a = 21; }
            if (!CompareLine(elements, document)) { a = 22; }
            return a;
        }

        public static List<string> ErrorString = new List<string>()
        {
            "OK",
            "Can not find parameter b or h",
            "One of Beams is not same Family Type",
            "One of Beams is not same Level",
            "One of Beams is not same Width",
            "One of Parameter Zoffset Beams must be <=0",
            "One of Parameter ZJustify Beams is not the same",
            "One of Beams is not same Property",
            "One of Beams is not Rectangle Shape",
            "One of Beams is not same Direction",
            "Beams is not Direction",
            "There is more then 1 Solid in Beams",
            "Error - Node (Beams, Columns, Foundation, Wall, Floor as Foundation)",
            "Beams must be jointed Columns ",
            "Beams must be jointed Floors ",
            "Floors must be jointed Beams ",
            "Beams must be jointed Foundation ",
            "Beams must be jointed Walls ",
            "Special Beams must be jointed Beams ",
            "There is some Splited Beam ",
            "Beams are not continune Beams ",
            "Unit Porject is m, Round digit diferent 1 ",
            "Beam is not Line ",
        };
        #region get all exception
        public static bool CompareProperty(List<Element> beams, Document document)
        {
            foreach (var item in beams)
            {
                ElementType elementType = document.GetElement(item.GetTypeId()) as ElementType;
                if (elementType.LookupParameter("b").AsDouble() == 0)
                {
                    return false;
                }
                if (elementType.LookupParameter("h").AsDouble() == 0)
                {
                    return false;
                }
            }
            return true;
        }
        public static bool JointBeamToColumns(List<Element> beams, Document doc)
        {
            string level = beams[0].get_Parameter(BuiltInParameter.INSTANCE_REFERENCE_LEVEL_PARAM).AsValueString();
            Line l = LineProcess.GetLineFromElement(beams[0]);
            foreach (var item in beams)
            {
                List<Element> columns = BeamsBoundBox.GetColumnsBoudingBoxOneBeam(item, doc);
                List<Element> columnsTopLevel = BeamsBoundBox.GetColumnsSameTopLevelBeams(columns, level);
                if (columnsTopLevel.Count != 0)
                {
                    foreach (var i in columnsTopLevel)
                    {
                        if (!JoinGeometryUtils.AreElementsJoined(doc, item, i))
                        {
                            return false;
                        }
                        if (JoinGeometryUtils.IsCuttingElementInJoin(doc, item, i))
                        {
                            return false;
                        }
                    }
                }
            }
            return true;
        }
        public static bool JointBeamToFloor(List<Element> beams, Document doc)
        {
            string level = beams[0].get_Parameter(BuiltInParameter.INSTANCE_REFERENCE_LEVEL_PARAM).AsValueString();
            Line l = LineProcess.GetLineFromElement(beams[0]);
            List<Level> levels = new FilteredElementCollector(doc).OfClass(typeof(Level)).Cast<Level>().ToList();
            levels.OrderBy(x => x.get_Parameter(BuiltInParameter.LEVEL_ELEV).AsDouble());
            Level level1 = levels[0];
            if (beams[0].get_Parameter(BuiltInParameter.STRUCTURAL_REFERENCE_LEVEL_ELEVATION).AsDouble() == level1.get_Parameter(BuiltInParameter.LEVEL_ELEV).AsDouble())
            {
                foreach (var item in beams)
                {
                    List<Element> floors = BeamsBoundBox.GetFloorBoudingBoxOneBeam(item, doc);
                    if (floors.Count != 0)
                    {
                        foreach (var ii in floors)
                        {

                            if (!JoinGeometryUtils.AreElementsJoined(doc, item, ii))
                            {
                                return false;
                            }
                            if (JoinGeometryUtils.IsCuttingElementInJoin(doc, item, ii))
                            {
                                return false;
                            }
                        }
                    }
                }
            }


            return true;
        }
        public static bool JointBeamNoneNodeToBeam(List<Element> beams, Document doc)
        {
            Line line = LineProcess.GetLineFromElement(beams[0]);
            string level = beams[0].get_Parameter(BuiltInParameter.INSTANCE_REFERENCE_LEVEL_PARAM).AsValueString();
           
            foreach (var item in beams)
            {
                List<Element> beamsSpecial = BeamsBoundBox.GetSpecialBeams(item, doc, line, level);
                if (beamsSpecial.Count != 0)
                {
                    foreach (var ii in beamsSpecial)
                    {

                        if (!JoinGeometryUtils.AreElementsJoined(doc, ii, item))
                        {
                            return false;
                        }
                        if (JoinGeometryUtils.IsCuttingElementInJoin(doc, ii, item))
                        {
                            return false;
                        }
                    }
                }
            }

            return true;
        }
        public static bool JointFloorToBeam(List<Element> beams, Document doc)
        {
            string level = beams[0].get_Parameter(BuiltInParameter.INSTANCE_REFERENCE_LEVEL_PARAM).AsValueString();
            Line l = LineProcess.GetLineFromElement(beams[0]);
            List<Level> levels = new FilteredElementCollector(doc).OfClass(typeof(Level)).Cast<Level>().ToList();
            levels.OrderBy(x => x.get_Parameter(BuiltInParameter.LEVEL_ELEV).AsDouble());
            Level level1 = levels[0];
            if (!(beams[0].get_Parameter(BuiltInParameter.STRUCTURAL_REFERENCE_LEVEL_ELEVATION).AsDouble() == level1.get_Parameter(BuiltInParameter.LEVEL_ELEV).AsDouble()))
            {
                foreach (var item in beams)
                {
                    List<Element> floors = BeamsBoundBox.GetFloorBoudingBoxOneBeam(item, doc);
                    if (floors.Count != 0)
                    {
                        foreach (var ii in floors)
                        {

                            if (!JoinGeometryUtils.AreElementsJoined(doc, item, ii))
                            {
                                return false;
                            }
                            if (JoinGeometryUtils.IsCuttingElementInJoin(doc, ii, item))
                            {
                                return false;
                            }
                        }
                    }
                }
            }

            return true;
        }
        public static bool JointBeamToFoundation(List<Element> beams, Document doc)
        {
            List<Level> levels = new FilteredElementCollector(doc).OfClass(typeof(Level)).Cast<Level>().ToList();
            levels.OrderBy(x => x.get_Parameter(BuiltInParameter.LEVEL_ELEV).AsDouble());
            Level level = levels[0];
            if (beams[0].get_Parameter(BuiltInParameter.STRUCTURAL_REFERENCE_LEVEL_ELEVATION).AsDouble() == level.get_Parameter(BuiltInParameter.LEVEL_ELEV).AsDouble())
            {
                foreach (var item in beams)
                {
                    List<Element> foundation = BeamsBoundBox.GetFoundationBoudingBoxOneBeam(item, doc);
                    if (foundation.Count != 0)
                    {
                        foreach (var i in foundation)
                        {

                            if (!JoinGeometryUtils.AreElementsJoined(doc, item, i))
                            {
                                return false;
                            }
                            if (JoinGeometryUtils.IsCuttingElementInJoin(doc, item, i))
                            {
                                return false;
                            }
                        }
                    }
                }

            }
            return true;
        }
        public static bool JointBeamToWall(List<Element> beams, Document doc)
        {
            foreach (var item in beams)
            {
                List<Element> wall = BeamsBoundBox.GetWallBoudingBoxOneBeam(item, doc);
                if (wall.Count != 0)
                {
                    foreach (var i in wall)
                    {
                        if (!JoinGeometryUtils.AreElementsJoined(doc, item, i))
                        {
                            return false;
                        }
                        if (JoinGeometryUtils.IsCuttingElementInJoin(doc, item, i))
                        {
                            return false;
                        }
                    }
                }
            }
            return true;
        }
        public static bool Joint(List<Element> beams, Document doc)
        {
            string level = beams[0].get_Parameter(BuiltInParameter.INSTANCE_REFERENCE_LEVEL_PARAM).AsValueString();
            Line l = LineProcess.GetLineFromElement(beams[0]);

            foreach (var item in beams)
            {
                List<Element> columns = BeamsBoundBox.GetColumnsBoudingBoxOneBeam(item, doc);
                List<Element> columnsTopLevel = BeamsBoundBox.GetColumnsSameTopLevelBeams(columns, level);
                foreach (var i in columnsTopLevel)
                {
                    if (!JoinGeometryUtils.AreElementsJoined(doc, item, i))
                    {

                        return false;
                    }

                }
            }
            return true;
        }
        public static bool CompareZOffset(List<Element> beams, Document document)
        {
            foreach (var item in beams)
            {
                ElementType elementTypeitem = document.GetElement(item.GetTypeId()) as ElementType;
                double h = double.Parse(UnitFormatUtils.Format(document.GetUnits(), SpecTypeId.Length, elementTypeitem.LookupParameter("h").AsDouble(), false));
                double b = double.Parse(UnitFormatUtils.Format(document.GetUnits(), SpecTypeId.Length, item.get_Parameter(BuiltInParameter.Z_OFFSET_VALUE).AsDouble(), false));
                if ((b > 0) || (Math.Abs(b) > h / 2))
                {
                    return false;
                }
            }
            return true;
        }
        public static bool CompareZJustify(List<Element> beams)
        {
            for (int i = 1; i < beams.Count; i++)
            {
                string b1 = beams[i - 1].get_Parameter(BuiltInParameter.Z_JUSTIFICATION).AsValueString();
                string b2 = beams[i].get_Parameter(BuiltInParameter.Z_JUSTIFICATION).AsValueString();
                if (!b1.Equals(b2))
                {
                    return false;
                }
            }

            return true;
        }
        public static bool OneSolidElement(List<Element> beams)
        {
            for (int i = 0; i < beams.Count; i++)
            {
                Solid solid = SolidFace.GetSolidElement(beams[i]);
                if (solid == null)
                {
                    return false;
                }
            }

            return true;
        }
        public static bool ReactangleShape(List<Element> beams)
        {
            Line l = LineProcess.GetLineFromElement(beams[0]);
            for (int i = 0; i < beams.Count; i++)
            {
                Solid solid = SolidFace.GetSolidElement(beams[i]);
                FaceArray faceArray = solid.Faces;

                List<PlanarFace> planarFace = new List<PlanarFace>();
                foreach (var item in faceArray)
                {
                    var x = item as PlanarFace;
                    if (x != null)
                    {
                        planarFace.Add(item as PlanarFace);
                    }
                }
                int a = 0;
                foreach (var item in planarFace)
                {
                    if (PointModel.AreEqual(l.Direction.AngleTo(item.FaceNormal), Math.PI / 2))
                    {
                        a++;
                    }
                }
                if (a < 4)
                {

                    return false;
                }
                if (a % 4 != 0)
                {
                    return false;
                }
            }
            return true;
        }
        public static bool SameFamilyTpye(List<Element> elements)
        {
            Element e = elements[0];
            for (int i = 1; i < elements.Count; i++)
            {
                if (e.get_Parameter(BuiltInParameter.ELEM_FAMILY_PARAM).AsValueString() != elements[i].get_Parameter(BuiltInParameter.ELEM_FAMILY_PARAM).AsValueString())
                {
                    return false;
                }
            }
            return true;
        }
        public static bool CompareDirectionBeams(List<Element> elements)
        {
            List<Line> lines = LineProcess.GetListLineFromElements(elements);
            return LineProcess.CompareVectorListLine(lines);
        }
        public static bool SameLevel(List<Element> elements)
        {
            Element e = elements[0];
            for (int i = 1; i < elements.Count; i++)
            {
                if (e.get_Parameter(BuiltInParameter.STRUCTURAL_REFERENCE_LEVEL_ELEVATION).AsDouble() != elements[i].get_Parameter(BuiltInParameter.STRUCTURAL_REFERENCE_LEVEL_ELEVATION).AsDouble())
                {
                    return false;
                }
            }
            return true;
        }
        public static bool CompareWidthBeams(List<Element> elements, Document document)
        {
            ElementType elementType0 = document.GetElement(elements[0].GetTypeId()) as ElementType;
            double b0 = double.Parse(UnitFormatUtils.Format(document.GetUnits(), SpecTypeId.Length, elementType0.LookupParameter("b").AsDouble(), false));
            for (int i = 1; i < elements.Count; i++)
            {
                ElementType elementTypei = document.GetElement(elements[i].GetTypeId()) as ElementType;
                double bi = double.Parse(UnitFormatUtils.Format(document.GetUnits(), SpecTypeId.Length, elementTypei.LookupParameter("b").AsDouble(), false));
                if (bi != b0)
                {
                    return false;
                }
            }
            return true;
        }
        public static bool CompareSameDirectionBeams(List<Element> elements)
        {
            List<Line> lines = LineProcess.GetListLineFromElements(elements);
            for (int i = 1; i < lines.Count; i++)
            {
                XYZ a = lines[0].GetEndPoint(0).Subtract(lines[i].GetEndPoint(0));
                double x = lines[0].Direction.AngleTo(a);
                if (!(x < 1e-9 || Math.Abs(x - Math.PI) < 1e-9))
                {
                    return false;
                }
            }
            return true;
        }
        public static bool ErrorNodeFoundation(List<Element> beams, Document doc)
        {
            List<Level> levels = new FilteredElementCollector(doc).OfClass(typeof(Level)).Cast<Level>().ToList();
            levels.OrderBy(x => x.get_Parameter(BuiltInParameter.LEVEL_ELEV).AsDouble());
            Level level = levels[0];
            ElementId level0 = beams[0].get_Parameter(BuiltInParameter.INSTANCE_REFERENCE_LEVEL_PARAM).AsElementId();
            Line l = LineProcess.GetLineFromElement(beams[0]);
            string level1 = beams[0].get_Parameter(BuiltInParameter.INSTANCE_REFERENCE_LEVEL_PARAM).AsValueString();
            if (beams[0].get_Parameter(BuiltInParameter.STRUCTURAL_REFERENCE_LEVEL_ELEVATION).AsDouble() == level.get_Parameter(BuiltInParameter.LEVEL_ELEV).AsDouble())
            {
                List<Element> foundation = BeamsBoundBox.GetFoundationBoudingBoxBeams(beams, doc);
                if (foundation.Count == 0 )
                {
                    return false;
                }
                if (foundation.Count != 0)
                {
                    for (int i = 0; i < foundation.Count; i++)
                    {
                        Solid a = SolidFace.GetSolidElement(foundation[i]);
                        List<PlanarFace> planarFaces = SolidFace.GetPlanrFacePerpendicular(a, l.Direction);
                        if (planarFaces.Count != 2)
                        {
                            return false;
                        }
                    }
                }
            }
            return true;
        }
        public static bool ErrorNodeFloorAsFoundation(List<Element> beams, Document doc)
        {
            List<Level> levels = new FilteredElementCollector(doc).OfClass(typeof(Level)).Cast<Level>().ToList();
            levels.OrderBy(x => x.get_Parameter(BuiltInParameter.LEVEL_ELEV).AsDouble());
            Level level = levels[0];
            ElementId level0 = beams[0].get_Parameter(BuiltInParameter.INSTANCE_REFERENCE_LEVEL_PARAM).AsElementId();
            Line l = LineProcess.GetLineFromElement(beams[0]);
            List<Element> walls = BeamsBoundBox.GetWallBoudingBoxBeams(beams, doc);
            string level1 = beams[0].get_Parameter(BuiltInParameter.INSTANCE_REFERENCE_LEVEL_PARAM).AsValueString();
            List<Element> columns = BeamsBoundBox.GetColumnsBoudingBoxBeams(beams, doc);
            List<Element> columnsLevel = BeamsBoundBox.GetColumnsSameTopLevelBeams(columns, level1);
            List<Element> b1 = BeamsBoundBox.GetBeamsPerNodeBeams(beams, doc);
            if (beams[0].get_Parameter(BuiltInParameter.STRUCTURAL_REFERENCE_LEVEL_ELEVATION).AsDouble() == level.get_Parameter(BuiltInParameter.LEVEL_ELEV).AsDouble())
            {
                List<Element> floor = BeamsBoundBox.GetFloorBoudingBoxBeams(beams, doc);
                if (floor.Count == 0 )
                {
                    return false;
                }
                if (floor.Count != 0)
                {
                    for (int j = 0; j < floor.Count; j++)
                    {
                        Solid a = SolidFace.GetSolidElement(floor[j]);
                        List<PlanarFace> planarFaces = SolidFace.GetPlanrFacePerpendicular(a, l.Direction);
                        if (planarFaces.Count != 2)
                        {
                            return false;
                        }
                    }
                }
            }
            return true;
        }
        public static bool ErrorNodeWalls(List<Element> beams, Document doc)
        {
            List<Level> levels = new FilteredElementCollector(doc).OfClass(typeof(Level)).Cast<Level>().ToList();
            levels.OrderBy(x => x.get_Parameter(BuiltInParameter.LEVEL_ELEV).AsDouble());
            Level level = levels[0];
            ElementId level0 = beams[0].get_Parameter(BuiltInParameter.INSTANCE_REFERENCE_LEVEL_PARAM).AsElementId();
            Line l = LineProcess.GetLineFromElement(beams[0]);
            List<Element> walls = BeamsBoundBox.GetWallBoudingBoxBeams(beams, doc);
            string level1 = beams[0].get_Parameter(BuiltInParameter.INSTANCE_REFERENCE_LEVEL_PARAM).AsValueString();
            List<Element> columns = BeamsBoundBox.GetColumnsBoudingBoxBeams(beams, doc);
            List<Element> columnsLevel = BeamsBoundBox.GetColumnsSameTopLevelBeams(columns, level1);
            List<Element> b1 = BeamsBoundBox.GetBeamsPerNodeBeams(beams, doc);
            if (walls.Count == 0 )
            {
                return false;
            }
            else
            {
                for (int k = 0; k < walls.Count; k++)
                {
                    Solid a = SolidFace.GetSolidElement(walls[k]);
                    List<PlanarFace> planarFaces = SolidFace.GetPlanrFacePerpendicular(a, l.Direction);
                    if (planarFaces.Count != 2)
                    {
                        return false;
                    }
                }
            }
            return true;
        }
        public static bool ErrorNodeColumns(List<Element> beams, Document doc)
        {
            List<Level> levels = new FilteredElementCollector(doc).OfClass(typeof(Level)).Cast<Level>().ToList();
            levels.OrderBy(x => x.get_Parameter(BuiltInParameter.LEVEL_ELEV).AsDouble());
            Level level = levels[0];
            ElementId level0 = beams[0].get_Parameter(BuiltInParameter.INSTANCE_REFERENCE_LEVEL_PARAM).AsElementId();
            Line l = LineProcess.GetLineFromElement(beams[0]);
            List<Element> walls = BeamsBoundBox.GetWallBoudingBoxBeams(beams, doc);
            string level1 = beams[0].get_Parameter(BuiltInParameter.INSTANCE_REFERENCE_LEVEL_PARAM).AsValueString();
            List<Element> columns = BeamsBoundBox.GetColumnsBoudingBoxBeams(beams, doc);
            List<Element> columnsLevel = BeamsBoundBox.GetColumnsSameTopLevelBeams(columns, level1);
            List<Element> b1 = BeamsBoundBox.GetBeamsPerNodeBeams(beams, doc);
            if ( columnsLevel.Count == 0)
            {
                return false;
            }
            else
            {
                for (int i = 0; i < columnsLevel.Count; i++)
                {
                    Solid a = SolidFace.GetSolidElement(columnsLevel[i]);
                    List<PlanarFace> planarFaces = SolidFace.GetPlanrFacePerpendicular(a, l.Direction);
                    if (planarFaces.Count != 2)
                    {
                        return false;
                    }
                }
            }
            return true;
        }
        public static bool ErrorNodeBeamPerpencular(List<Element> beams, Document doc)
        {
            List<Level> levels = new FilteredElementCollector(doc).OfClass(typeof(Level)).Cast<Level>().ToList();
            levels.OrderBy(x => x.get_Parameter(BuiltInParameter.LEVEL_ELEV).AsDouble());
            Level level = levels[0];
            ElementId level0 = beams[0].get_Parameter(BuiltInParameter.INSTANCE_REFERENCE_LEVEL_PARAM).AsElementId();
            Line l = LineProcess.GetLineFromElement(beams[0]);
            List<Element> walls = BeamsBoundBox.GetWallBoudingBoxBeams(beams, doc);
            string level1 = beams[0].get_Parameter(BuiltInParameter.INSTANCE_REFERENCE_LEVEL_PARAM).AsValueString();
            List<Element> columns = BeamsBoundBox.GetColumnsBoudingBoxBeams(beams, doc);
            List<Element> columnsLevel = BeamsBoundBox.GetColumnsSameTopLevelBeams(columns, level1);
            List<Element> b1 = BeamsBoundBox.GetBeamsPerNodeBeams(beams, doc);
            if ( b1.Count == 0 )
            {
                return false;
            }
            else
            {
                for (int i = 0; i < b1.Count; i++)
                {
                    Solid a = SolidFace.GetSolidElement(b1[i]);
                    List<PlanarFace> planarFaces = SolidFace.GetPlanrFacePerpendicular(a, l.Direction);
                    if (planarFaces.Count != 2)
                    {
                        return false;
                    }
                }
            }
            return true;
        }
        public static bool CompareNoneNode(List<Element> beams, Document doc)
        {
            List<Level> levels = new FilteredElementCollector(doc).OfClass(typeof(Level)).Cast<Level>().ToList();
            levels.OrderBy(x => x.get_Parameter(BuiltInParameter.LEVEL_ELEV).AsDouble());
            Level level = levels[0];
            ElementId level0 = beams[0].get_Parameter(BuiltInParameter.INSTANCE_REFERENCE_LEVEL_PARAM).AsElementId();
            Line l = LineProcess.GetLineFromElement(beams[0]);
            List<Element> walls = BeamsBoundBox.GetWallBoudingBoxBeams(beams, doc);
            string level1 = beams[0].get_Parameter(BuiltInParameter.INSTANCE_REFERENCE_LEVEL_PARAM).AsValueString();
            List<Element> columns = BeamsBoundBox.GetColumnsBoudingBoxBeams(beams, doc);
            List<Element> columnsLevel = BeamsBoundBox.GetColumnsSameTopLevelBeams(columns, level1);
            List<Element> b1 = BeamsBoundBox.GetBeamsPerNodeBeams(beams, doc);
            if (beams[0].get_Parameter(BuiltInParameter.STRUCTURAL_REFERENCE_LEVEL_ELEVATION).AsDouble() == level.get_Parameter(BuiltInParameter.LEVEL_ELEV).AsDouble())
            {
                List<Element> floor = BeamsBoundBox.GetFloorBoudingBoxBeams(beams, doc);
                List<Element> foundation = BeamsBoundBox.GetFoundationBoudingBoxBeams(beams, doc);
                if (floor.Count == 0 && foundation.Count == 0 && walls.Count == 0 && b1.Count == 0 && columnsLevel.Count == 0)
                {
                    return false;
                }
                if (foundation.Count != 0)
                {
                    for (int i = 0; i < foundation.Count; i++)
                    {
                        Solid a = SolidFace.GetSolidElement(foundation[i]);
                        List<PlanarFace> planarFaces = SolidFace.GetPlanrFacePerpendicular(a, l.Direction);
                        if (planarFaces.Count != 2)
                        {
                            return false;
                        }
                    }
                }
                if (floor.Count != 0)
                {
                    for (int j = 0; j < floor.Count; j++)
                    {
                        Solid a = SolidFace.GetSolidElement(floor[j]);
                        List<PlanarFace> planarFaces = SolidFace.GetPlanrFacePerpendicular(a, l.Direction);
                        if (planarFaces.Count != 2)
                        {
                            return false;
                        }
                    }
                }
                if (walls.Count != 0)
                {
                    for (int k = 0; k < walls.Count; k++)
                    {
                        Solid a = SolidFace.GetSolidElement(walls[k]);
                        List<PlanarFace> planarFaces = SolidFace.GetPlanrFacePerpendicular(a, l.Direction);
                        if (planarFaces.Count != 2)
                        {
                            return false;
                        }
                    }
                }
                if (columnsLevel.Count != 0)
                {
                    for (int i = 0; i < columnsLevel.Count; i++)
                    {
                        Solid a = SolidFace.GetSolidElement(columnsLevel[i]);
                        List<PlanarFace> planarFaces = SolidFace.GetPlanrFacePerpendicular(a, l.Direction);
                        if (planarFaces.Count != 2)
                        {
                            return false;
                        }
                    }
                }
                if (b1.Count != 0)
                {
                    for (int i = 0; i < b1.Count; i++)
                    {
                        Solid a = SolidFace.GetSolidElement(b1[i]);
                        List<PlanarFace> planarFaces = SolidFace.GetPlanrFacePerpendicular(a, l.Direction);
                        if (planarFaces.Count != 2)
                        {
                            return false;
                        }
                    }
                }
            }
            else
            {

                if (columnsLevel.Count == 0 && b1.Count == 0 && walls.Count == 0)
                {
                    return false;
                }
                if (columnsLevel.Count != 0)
                {
                    for (int i = 0; i < columnsLevel.Count; i++)
                    {
                        Solid a = SolidFace.GetSolidElement(columnsLevel[i]);
                        List<PlanarFace> planarFaces = SolidFace.GetPlanrFacePerpendicular(a, l.Direction);
                        if (planarFaces.Count != 2)
                        {
                            return false;
                        }
                    }
                }
                if (walls.Count != 0)
                {
                    for (int k = 0; k < walls.Count; k++)
                    {
                        Solid a = SolidFace.GetSolidElement(walls[k]);
                        List<PlanarFace> planarFaces = SolidFace.GetPlanrFacePerpendicular(a, l.Direction);
                        if (planarFaces.Count != 2)
                        {
                            return false;
                        }
                    }
                }
                if (b1.Count != 0)
                {
                    for (int i = 0; i < b1.Count; i++)
                    {
                        Solid a = SolidFace.GetSolidElement(b1[i]);
                        List<PlanarFace> planarFaces = SolidFace.GetPlanrFacePerpendicular(a, l.Direction);
                        if (planarFaces.Count != 2)
                        {
                            return false;
                        }
                    }
                }

            }
            return true;
        }
        public static bool ComparePropertyBeams(List<Element> beams, Document doc)
        {
            if (beams.Count == 1)
            {
                return true;
            }
            double crossSection = beams[0].get_Parameter(BuiltInParameter.STRUCTURAL_BEND_DIR_ANGLE).AsDouble();
            string yzJustifi = beams[0].get_Parameter(BuiltInParameter.YZ_JUSTIFICATION).AsValueString();
            string yJustifi = beams[0].get_Parameter(BuiltInParameter.Y_JUSTIFICATION).AsValueString();
            string zJustifi = beams[0].get_Parameter(BuiltInParameter.Z_JUSTIFICATION).AsValueString();
            double yOffset = beams[0].get_Parameter(BuiltInParameter.Y_OFFSET_VALUE).AsDouble();
            for (int i = 1; i < beams.Count; i++)
            {
                bool crossSectionBool = AreEquals(beams[i].get_Parameter(BuiltInParameter.STRUCTURAL_BEND_DIR_ANGLE).AsDouble(), crossSection);
                bool yOffsetBool = AreEquals(beams[i].get_Parameter(BuiltInParameter.Y_OFFSET_VALUE).AsDouble(), yOffset);
                bool yzJustifiBool = beams[i].get_Parameter(BuiltInParameter.YZ_JUSTIFICATION).AsValueString().Equals(yzJustifi);
                bool yJustifiBool = beams[i].get_Parameter(BuiltInParameter.Y_JUSTIFICATION).AsValueString().Equals(yJustifi);
                bool zJustifiBool = beams[i].get_Parameter(BuiltInParameter.Z_JUSTIFICATION).AsValueString().Equals(zJustifi);
                if (!crossSectionBool || !yOffsetBool || !yzJustifiBool || !yJustifiBool || !zJustifiBool)
                {
                    return false;
                }
            }
            return true;
        }
        private static bool ContinuneBeams(List<Element> elements, Document document)
        {
            for (int i = 0; i < elements.Count - 1; i++)
            {
                Line l1 = LineProcess.GetLineFromElement(elements[i]);
                Line l2 = LineProcess.GetLineFromElement(elements[i + 1]);
                XYZ a1l1 = l1.GetEndPoint(0);
                XYZ a2l1 = l1.GetEndPoint(1);
                XYZ a1l2 = l2.GetEndPoint(0);
                XYZ a2l2 = l2.GetEndPoint(1);
                bool b1 = PointModel.AreEqual(a1l1.DistanceTo(a1l2), 0);
                bool b2 = PointModel.AreEqual(a1l1.DistanceTo(a2l2), 0);
                bool b3 = PointModel.AreEqual(a2l1.DistanceTo(a1l2), 0);
                bool b4 = PointModel.AreEqual(a2l1.DistanceTo(a2l2), 0);
                if (!b1 && !b2 && !b3 && !b4)
                {
                    return false;
                }
            }
            return true;
        }
        private static bool SlpitBeams(List<Element> elements, Document document)
        {
            Line l = LineProcess.GetLineFromElement(elements[0]);
            XYZ vector = l.Direction;
            for (int i = 0; i < elements.Count - 1; i++)
            {
                Solid a1 = SolidFace.GetSolidElement(elements[i]);
                Solid a2 = SolidFace.GetSolidElement(elements[i + 1]);
                List<PlanarFace> pera1 = SolidFace.GetPlanrFacePerpendicular(a1, vector);
                List<PlanarFace> pera2 = SolidFace.GetPlanrFacePerpendicular(a2, vector);
                if (pera1.Count != 0)
                {
                    for (int j = 0; j < pera1.Count; j++)
                    {
                        if (pera2.Count != 0)
                        {
                            for (int k = 0; k < pera2.Count; k++)
                            {
                                if (PointModel.AreEqual((PointModel.DistanceTo2(pera1[j], pera2[k].Origin, document)), 0))
                                {
                                    return false;
                                }
                            }
                        }

                    }
                }

            }

            return true;
        }
        private static bool CompareDecimal(Document document)
        {
            FormatOptions formatOptions = document.GetUnits().GetFormatOptions(SpecTypeId.Length);
            ForgeTypeId forgeTypeId = formatOptions.GetUnitTypeId();
            if (forgeTypeId == UnitTypeId.Meters)
            {
                if (formatOptions.Accuracy >= 1)
                {
                    return false;
                }
            }
            return true;
        }
        private static bool CompareLine(List<Element> elements, Document document)
        {
            for (int i = 0; i < elements.Count; i++)
            {
                Line l = LineProcess.GetLineFromElement(elements[i]);
                if (l == null)
                {
                    return false;
                }
            }
            return true;
        }
        #endregion
        #region get property beams
        public static string GetFamilyTypeName(Element element)
        {
            string a = "";
            a = element.get_Parameter(BuiltInParameter.ELEM_FAMILY_PARAM).AsValueString();
            return a;
        }
        public static string GetTypes(List<Element> beams)
        {
            string a = "";
            for (int i = 0; i < beams.Count; i++)
            {
                a += ((beams[i].get_Parameter(BuiltInParameter.ELEM_TYPE_PARAM).AsValueString()) + " ");

            }

            return a;
        }
        public static bool SameFamilyTpyeMainBeam(List<Element> elements, Element mainElement)
        {
            for (int i = 1; i < elements.Count; i++)
            {
                if (mainElement.get_Parameter(BuiltInParameter.ELEM_FAMILY_PARAM).AsValueString() != elements[i].get_Parameter(BuiltInParameter.ELEM_FAMILY_PARAM).AsValueString())
                {
                    return false;
                }
            }
            return true;
        }
        //public static List<ElementId> GetElementIdsNotSameRebarCover(List<Element> elements, Document document)
        //{
        //    List<ElementId> a = new List<ElementId>();
        //    foreach (var item in elements)
        //    {
        //        if (!CompareRebarCoverFace(item, document))
        //        {
        //            ElementId e = item.Id as ElementId;
        //            a.Add(e);
        //        }
        //    }
        //    return a;
        //}
        #endregion
        #region Get beams
        public static List<Element> GetElements(List<Reference> references, Document document)
        {
            List<Element> beams = new List<Element>(references.Count);
            foreach (Reference item in references)
            {
                beams.Add(document.GetElement(item));

            }
            return beams;
        }
        #endregion
        private static bool AreEquals(double a, double b, double t = 1e-10)
        {
            return Math.Abs(a - b) < t;
        }
    }
}