using Autodesk.Revit.DB;
using System;
using System.Collections.Generic;
using System.Linq;

namespace R10_WallShear
{
    public class ErrorWalls
    {

        public static string GetErrorWalls(Document document, List<Element> walls)
        {
            string error = "OK";
            if (!SameFamilyType(walls, document)) error = "One of Walls is not same Family Type";
            if (!IsnotVerticalWalls(document, walls)) error = "One of Walls is not Vertical Style";
            if (!OneSolidWalls(walls)) error = "One solid in Walls";
            if (!ContinueWalls(walls, document)) error = "Walls are not continune Walls";
            if (!CompareLineDirection(walls, document)) error = "Walls are not Same Line Direction";
            if (!LineWalls(walls, document)) error = "Walls are not Line Walls";
            if (!CompareReactangle(walls, document)) error = "One of Walls is not Reactangle or has Opening";
            if (!RotateWalls(walls, document)) error = "One of Walls is rotated";
            if (!CompareProperty(walls, document)) error = "Higher Level Wall has Thickness or Lenght  larger than Lower Level Wall";
            if (!ComparePropertyDL(walls, document)) error = "Wall's Length must be >= 4  and <= 8  Wall's Thickness";
            if (!CompareOutSide(walls, document)) error = "Higher Level Wall is outside Lower Level Wall";
            if (!CompareOverTopPlanarFaceBeam(walls, document)) error = "There is a beam Higher Top PlanrFace Wall";
            if (!JoinBeamsToWalls(walls, document)) error = "Beams must be joint to Walls";
            if (!JoinWallsToFoundation(walls[0], document)) error = "First Wall must be jointed Foundation or Error Foundation";
            if (!JoinWallsToFloorFoundation(walls[0], document)) error = "First Wall must be jointed Floor as Foundation or Error Floor";
            if (!JoinWallsToBeamFoundation(walls[0], document)) error = "Error Beams in bottom Walls";
            if (!CompareDecimal(document)) { error = "Decimal Place must be >=3, Check the Project Unit"; }
            return error;
        }








        #region

        private static bool Opening(List<Element> walls, Document document)
        {
            for (int i = 0; i < walls.Count; i++)
            {
                List<PlanarFace> planarFaces = SolidFace.GetAllPlanarFace(walls[i]);
                if (planarFaces.Count != 6)
                {
                    return false;
                }
            }
            return true;
        }
        private static bool SameFamilyType(List<Element> columns, Document document)
        {
            Element element = columns[0];
            for (int i = 0; i < columns.Count; i++)
            {
                if (element.get_Parameter(BuiltInParameter.ELEM_FAMILY_PARAM).AsValueString() != columns[i].get_Parameter(BuiltInParameter.ELEM_FAMILY_PARAM).AsValueString())
                {
                    return false;
                }
            }
            return true;
        }
        private static bool IsnotVerticalWalls(Document document, List<Element> columns)
        {
            for (int i = 0; i < columns.Count; i++)
            {
                return VerticalOneWall(document, columns[i]);
            }

            return true;
        }
        private static bool OneSolidWalls(List<Element> columns)
        {
            foreach (Element item in columns)
            {
                if (SolidFace.GetListSolidOneElement(item).Count != 1)
                {
                    return false;
                }
            }
            return true;
        }
        private static bool ContinueWalls(List<Element> columns, Document document)
        {
            if (columns.Count == 1)
            {
                return true;
            }
            else
            {
                for (int i = 1; i < columns.Count; i++)
                {
                    PlanarFace b1 = SolidFace.GetTop(columns[i - 1]);
                    PlanarFace b2 = SolidFace.GetBottom(columns[i]);
                    if (b1 == null || b2 == null)
                    {
                        return false;
                    }
                    if (!PointModel.AreEqual(PointModel.DistanceTo2(b1, b2.Origin, document), 0))
                    {
                        return false;
                    }
                }
            }

            return true;
        }
        private static bool CompareLineDirection(List<Element> walls, Document document)
        {

            Line line0 = LineProcess.GetLineFromElement(walls[0]);
            for (int i = 0; i < walls.Count; i++)
            {
                if (!LineProcess.CompareDirectionLine(line0, LineProcess.GetLineFromElement(walls[i]))) return false;
            }
            return true;
        }
        private static bool LineWalls(List<Element> walls, Document document)
        {

            for (int i = 0; i < walls.Count; i++)
            {
                try
                {
                    return ((walls[i].Location as LocationCurve).Curve as Line != null);
                }
                catch (Exception)
                {

                    return false;
                }
            }
            return true;
        }
        private static bool CompareReactangle(List<Element> walls, Document document)
        {
            for (int i = 0; i < walls.Count; i++)
            {
                if (!IsRectangle(document, walls[i]))
                {
                    return false;
                }
            }
            return true;
        }
        private static bool RotateWalls(List<Element> walls, Document document)
        {
            if (walls.Count == 1)
            {
                return true;
            }
            else
            {
                for (int i = 1; i < walls.Count; i++)
                {
                    PlanarFace south1 = SolidFace.GetSouth(walls[i - 1]);
                    PlanarFace nouth1 = SolidFace.GetNouth(walls[i - 1]);
                    PlanarFace east1 = SolidFace.GetEast(walls[i - 1]);
                    PlanarFace west1 = SolidFace.GetWest(walls[i - 1]);
                    PlanarFace south2 = SolidFace.GetSouth(walls[i]);
                    PlanarFace nouth2 = SolidFace.GetNouth(walls[i]);
                    PlanarFace east2 = SolidFace.GetEast(walls[i]);
                    PlanarFace west2 = SolidFace.GetWest(walls[i]);
                    bool bsouth = PointModel.AreEqual(south1.FaceNormal.AngleTo(south2.FaceNormal), 0);
                    bool bnouth = PointModel.AreEqual(nouth1.FaceNormal.AngleTo(nouth2.FaceNormal), 0);
                    bool beast = PointModel.AreEqual(east1.FaceNormal.AngleTo(east2.FaceNormal), 0);
                    bool bwest = PointModel.AreEqual(west1.FaceNormal.AngleTo(west2.FaceNormal), 0);
                    if (!bsouth || !bnouth || !beast || !bwest)
                        return false;
                }
            }

            return true;
        }
        private static bool CompareProperty(List<Element> walls, Document document)
        {
            if (walls.Count == 1)
            {
                return true;
            }
            else
            {
                for (int i = 1; i < walls.Count; i++)
                {
                    PlanarFace south1 = SolidFace.GetSouth(walls[i - 1]);
                    PlanarFace nouth1 = SolidFace.GetNouth(walls[i - 1]);
                    PlanarFace east1 = SolidFace.GetEast(walls[i - 1]);
                    PlanarFace west1 = SolidFace.GetWest(walls[i - 1]);
                    PlanarFace south2 = SolidFace.GetSouth(walls[i]);
                    PlanarFace nouth2 = SolidFace.GetNouth(walls[i]);
                    PlanarFace east2 = SolidFace.GetEast(walls[i]);
                    PlanarFace west2 = SolidFace.GetWest(walls[i]);
                    double b1 = PointModel.DistanceTo2(south1, nouth1.Origin, document);
                    double b2 = PointModel.DistanceTo2(south2, nouth2.Origin, document);
                    double h1 = PointModel.DistanceTo2(east1, west1.Origin, document);
                    double h2 = PointModel.DistanceTo2(east2, west2.Origin, document);
                    if (b1 < b2 || h1 < h2)
                    {
                        return false;
                    }
                }
            }

            return true;
        }
        private static bool CompareOutSide(List<Element> walls, Document document)
        {
            if (walls.Count == 1)
            {
                return true;
            }
            else
            {
                for (int i = 1; i < walls.Count; i++)
                {
                    PlanarFace south1 = SolidFace.GetSouth(walls[i - 1]);
                    PlanarFace nouth1 = SolidFace.GetNouth(walls[i - 1]);
                    PlanarFace east1 = SolidFace.GetEast(walls[i - 1]);
                    PlanarFace west1 = SolidFace.GetWest(walls[i - 1]);
                    PlanarFace south2 = SolidFace.GetSouth(walls[i]);
                    PlanarFace nouth2 = SolidFace.GetNouth(walls[i]);
                    PlanarFace east2 = SolidFace.GetEast(walls[i]);
                    PlanarFace west2 = SolidFace.GetWest(walls[i]);
                    double b0 = PointModel.DistanceTo2(south1, nouth1.Origin, document);
                    double h0 = PointModel.DistanceTo2(east1, west1.Origin, document);
                    double totalB1 = PointModel.DistanceTo2(south1, south2.Origin, document) + PointModel.DistanceTo2(nouth1, south2.Origin, document);
                    double totalB2 = PointModel.DistanceTo2(south1, nouth2.Origin, document) + PointModel.DistanceTo2(nouth1, nouth2.Origin, document);
                    double totalH1 = PointModel.DistanceTo2(east1, east2.Origin, document) + PointModel.DistanceTo2(west1, east2.Origin, document);
                    double totalH2 = PointModel.DistanceTo2(east1, west2.Origin, document) + PointModel.DistanceTo2(west1, west2.Origin, document);
                    if (totalB1 > b0 || totalB2 > b0 || totalH1 > h0 || totalH2 > h0)
                    {
                        return false;
                    }
                }
            }

            return true;
        }
        private static bool CompareOverTopPlanarFaceBeam(List<Element> walls, Document document)
        {

            for (int i = 0; i < walls.Count; i++)
            {
                PlanarFace planarFaceTopColumn = SolidFace.GetTop(walls[i]);
                List<Element> beams = WallBoundingBox.GetBeamsBoudingBoxSameTopLevelPerpencularZOneWall(walls[i], document);
                if (beams.Count != 0)
                {
                    for (int j = 0; j < beams.Count; j++)
                    {
                        List<PlanarFace> planarFaces = SolidFace.GetTopPlanarFaceBeam(beams[j], document);
                        if (planarFaces.Count != 0)
                        {
                            if (planarFaces[planarFaces.Count - 1].Origin.Z > planarFaceTopColumn.Origin.Z)
                            {
                                return false;
                            }

                        }
                    }
                }
            }
            return true;
        }
        private static bool JoinBeamsToWalls(List<Element> walls, Document document)
        {
            for (int i = 0; i < walls.Count; i++)
            {
                List<Element> beams = WallBoundingBox.GetBeamsBoudingBoxSameTopLevelPerpencularZOneWall(walls[i], document);

                if (beams.Count != 0)
                {
                    for (int j = 0; j < beams.Count; j++)
                    {
                        if (!JoinGeometryUtils.AreElementsJoined(document, beams[j], walls[i]))
                        {
                            return false;
                        }
                        if (JoinGeometryUtils.IsCuttingElementInJoin(document, beams[j], walls[i]))
                        {
                            return false;
                        }
                    }
                }
            }
            return true;
        }
        private static bool JoinWallsToFoundation(Element wall0, Document document)
        {
            Element foundation = WallBoundingBox.GetFoundationBoudingBoxOneWall(wall0, document);

            if (foundation != null)
            {
                if (!JoinGeometryUtils.AreElementsJoined(document, wall0, foundation)) return false;
                if (JoinGeometryUtils.IsCuttingElementInJoin(document, wall0, foundation)) return false;
                PlanarFace bottom = SolidFace.GetBottom(wall0);
                List<PlanarFace> planarFaces = SolidFace.GetTopPlanarFaceBeam(foundation, document);
                if (planarFaces.Count != 0)
                {
                    planarFaces = planarFaces.OrderBy(x => x.Origin.Z).ToList();
                    if (!PointModel.AreEqual(PointModel.DistanceTo2(planarFaces[planarFaces.Count - 1], bottom.Origin, document), 0)) return false;
                }
            }
            return true;
        }
        private static bool JoinWallsToFloorFoundation(Element wall0, Document document)
        {
            Element foundation = WallBoundingBox.GetFoundationBoudingBoxOneWall(wall0, document);

            if (foundation == null)
            {
                Element floor = WallBoundingBox.GetFloorBoudingBoxOneWall(wall0, document);
                if (floor != null)
                {
                    if (!JoinGeometryUtils.AreElementsJoined(document, wall0, floor)) return false;
                    if (JoinGeometryUtils.IsCuttingElementInJoin(document, wall0, floor)) return false;
                    PlanarFace bottom = SolidFace.GetBottom(wall0);
                    List<PlanarFace> planarFaces = SolidFace.GetTopPlanarFaceBeam(floor, document);
                    if (planarFaces.Count != 0)
                    {
                        planarFaces = planarFaces.OrderBy(x => x.Origin.Z).ToList();
                        if (!PointModel.AreEqual(PointModel.DistanceTo2(planarFaces[planarFaces.Count - 1], bottom.Origin, document), 0)) return false;
                    }
                }
            }
            return true;
        }

        private static bool JoinWallsToBeamFoundation(Element wall0, Document document)
        {

            Element foundation = WallBoundingBox.GetFoundationBoudingBoxOneWall(wall0, document);
            if (foundation == null)
            {
                Element floor = WallBoundingBox.GetFloorBoudingBoxOneWall(wall0, document);
                if (floor == null)
                {
                    List<Element> beams = WallBoundingBox.GetBeamsBoudingBoxSameBottomLevelPerpencularZOneWall(wall0, document);
                    if (beams.Count != 0)
                    {
                        List<PlanarFace> planarFaces = new List<PlanarFace>();
                        PlanarFace bottom = SolidFace.GetBottom(wall0);
                        for (int i = 0; i < beams.Count; i++)
                        {
                            List<PlanarFace> p1 = SolidFace.GetTopPlanarFaceBeam(beams[i], document);
                            planarFaces.AddRange(p1);
                        }
                        if (planarFaces.Count != 0)
                        {

                            planarFaces = planarFaces.Where(x => PointModel.AreEqual(PointModel.DistanceTo2(x, bottom.Origin, document), 0)).ToList();
                            if (planarFaces.Count == 0) return false;
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
        private static bool ComparePropertyDL(List<Element> walls, Document document)
        {
            for (int i = 0; i < walls.Count; i++)
            {

                PlanarFace south2 = SolidFace.GetSouth(walls[i]);
                PlanarFace nouth2 = SolidFace.GetNouth(walls[i]);
                PlanarFace east2 = SolidFace.GetEast(walls[i]);
                PlanarFace west2 = SolidFace.GetWest(walls[i]);
                double D = PointModel.DistanceTo2(south2, nouth2.Origin, document);
                double H = PointModel.DistanceTo2(east2, west2.Origin, document);
                if (H < 3 * D || H > 8 * D)
                {
                    return false;
                }
            }
            return true;
        }
        #endregion
        #region Error List
        public static List<string> ErrorString = new List<string>()
        {
            "OK",
            "One of Columns is not same Family Type",
            "One of Columns is not Vertical Style",
            "One solid in columns ",
            "Columns are not continune columns",
            "One of Columns is not Reactangle or Cylindrical",
            "One of Columns is rotated",
            "Higher Level Column has b or h or T larger than Lower Level Column",
            "Higher Level Column is outside Lower Level Column",
            "There is a beam Higher Top PlanrFace Column",
            "Beams must be joint to Columns",
            "Columns must be jointed Foundation or Error Foundation",
            "Columns must be jointed Floor as Foundation or Error Floor",
            "Columns must be jointed Wall or Error Wall ",
            "Error Beams in bottom Columns"
        };


        #endregion
        #region
        private static bool VerticalOneWall(Document document, Element wall)
        {
            return (wall.get_Parameter(BuiltInParameter.WALL_CROSS_SECTION).AsInteger() == 1);
        }
        #endregion

        public static bool IsRectangle(Document document, Element wall)
        {

            List<PlanarFace> planarFacesParallelZ = SolidFace.GetAllParallelZ(wall);
            List<PlanarFace> planarFacesPerpencular = SolidFace.GetAllPerpencular(wall);
            if (planarFacesParallelZ.Count == 2 && planarFacesPerpencular.Count == 4)
            {

                PlanarFace south = SolidFace.GetSouth(wall);
                if (south == null)
                {
                    System.Windows.Forms.MessageBox.Show("Test");
                }
                PlanarFace nouth = SolidFace.GetNouth(wall);

                PlanarFace east = SolidFace.GetEast(wall);

                PlanarFace west = SolidFace.GetWest(wall);

                bool b1 = PointModel.AreEqual(south.FaceNormal.AngleTo(nouth.FaceNormal), Math.PI) && PointModel.AreEqual(south.FaceNormal.AngleTo(east.FaceNormal), Math.PI / 2);

                bool b2 = PointModel.AreEqual(east.FaceNormal.AngleTo(west.FaceNormal), Math.PI) && PointModel.AreEqual(east.FaceNormal.AngleTo(south.FaceNormal), Math.PI / 2);

                if (!(b1 && b2))
                {
                    return false;
                }
                else
                {
                    return true;
                }

            }
            else
            {
                return false;
            }
        }
        private static double GetwidthColumn(Document document, Element column)
        {
            double a = 0;
            PlanarFace south = SolidFace.GetSouth(column);
            PlanarFace nouth = SolidFace.GetNouth(column);
            a = PointModel.DistanceTo2(south, nouth.Origin, document);
            return a;
        }
        private static double GetHeightColumn(Document document, Element column)
        {
            double a = 0;
            PlanarFace east = SolidFace.GetEast(column);
            PlanarFace west = SolidFace.GetWest(column);
            a = PointModel.DistanceTo2(east, west.Origin, document);
            return a;
        }
        public static double GetDiameter(Document document, Element column)
        {
            double a = 0;
            CylindricalFace CylindricalFace = SolidFace.GetAllCylindricalFace(column)[0];
            CylindricalSurface cylindricalSurface = CylindricalFace.GetSurface() as CylindricalSurface;
            if (cylindricalSurface != null)
            {
                a = 2 * Math.Abs(double.Parse(UnitFormatUtils.Format(document.GetUnits(), SpecTypeId.Length, cylindricalSurface.Radius, false)));
            }
            return a;
        }
    }
}
