using Autodesk.Revit.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;

namespace R11_FoundationPile
{
    public class ErrorColumns
    {
       
        public static string GetErrorColumns(Document document, List<Element> columns)
        {
            string error = "OK";
            //if (!SameFamilyType(columns, document)) error = 1;
            //if (!IsnotVerticalColumns(document, columns)) error = 2;
            //if (!OneSolidColumns(columns)) error = 3;
            //if (!ContinueColumns(columns,document)) error = 4;
            //if (!CompareReactangleCylindical(columns,document)) error = 5;
            //if (!RotateColumns(columns,document)) error = 6;
            //if (!CompareProperty(columns,document)) error = 7;
            //if (!CompareOutSide(columns,document)) error = 8;
            //if (!CompareOverTopPlanarFaceBeam(columns,document)) error = 9;
            //if (!JoinBeamsToColumns(columns,document)) error = 10;
            //if (!JoinColumnsToFoundation(columns[0], document)) error = 11;
            //if (!JoinColumnsToFloorFoundation(columns[0], document)) error = 12;
            //if (!JoinColumnsToWallFoundation(columns[0], document)) error = 13;
            //if (!JoinColumnsToBeamFoundation(columns[0], document)) error = 14;
            if (!CompareDecimal(document)) { error = "Check Digit Decimal"; }
            return error;
        }
        #region
       
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
        private static bool IsnotVerticalColumns(Document document, List<Element> columns)
        {
            for (int i = 0; i < columns.Count; i++)
            {
                return VerticalOneColumn(document, columns[i]);
            }

            return true;
        }
        private static bool OneSolidColumns(List<Element> columns)
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
        private static bool ContinueColumns(List<Element> columns,Document document)
        {
            if (columns.Count==1)
            {
                return true;
            }
            else
            {
                for (int i = 1; i < columns.Count; i++)
                {
                    PlanarFace b1 = SolidFace.GetTop(columns[i-1]);
                    PlanarFace b2 = SolidFace.GetBottom(columns[i]);
                    if (b1==null||b2==null)
                    {
                        return false;
                    }
                    if (!PointModel.AreEqual(PointModel.DistanceTo2(b1,b2.Origin,document),0))
                    {
                        return false;
                    }
                }
            }
            
            return true;
        }
        private static bool CompareReactangleCylindical(List<Element> columns, Document document)
        {
            for (int i = 0; i < columns.Count; i++)
            {
                if (GetSectionStyle(document, columns[i])==SectionStyle.ORTHER)
                {
                    return false;
                }
            }
            return true;
        }
        private static bool RotateColumns(List<Element> columns, Document document)
        {
            if (columns.Count==1)
            {
                return true;
            }
            else
            {
                for (int i = 1; i < columns.Count; i++)
                {
                    SectionStyle a = GetSectionStyle(document, columns[i - 1]);
                    SectionStyle b = GetSectionStyle(document, columns[i]);
                    if (a == SectionStyle.RECTANGLE && b == SectionStyle.RECTANGLE)
                    {
                        PlanarFace south1 = SolidFace.GetSouth(columns[i - 1]);
                        PlanarFace nouth1 = SolidFace.GetNouth(columns[i - 1]);
                        PlanarFace east1 = SolidFace.GetEast(columns[i - 1]);
                        PlanarFace west1 = SolidFace.GetWest(columns[i - 1]);
                        PlanarFace south2 = SolidFace.GetSouth(columns[i]);
                        PlanarFace nouth2 = SolidFace.GetNouth(columns[i]);
                        PlanarFace east2 = SolidFace.GetEast(columns[i]);
                        PlanarFace west2 = SolidFace.GetWest(columns[i]);
                        bool bsouth = PointModel.AreEqual(south1.FaceNormal.AngleTo(south2.FaceNormal), 0);
                        bool bnouth = PointModel.AreEqual(nouth1.FaceNormal.AngleTo(nouth2.FaceNormal), 0);
                        bool beast = PointModel.AreEqual(east1.FaceNormal.AngleTo(east2.FaceNormal), 0);
                        bool bwest = PointModel.AreEqual(west1.FaceNormal.AngleTo(west2.FaceNormal), 0);
                        if(!bsouth||!bnouth||!beast||!bwest)
                            return false;
                    }
                }
            }
           
            return true;
        }
        private static bool CompareProperty(List<Element> columns, Document document)
        {
            if (columns.Count == 1)
            {
                return true;
            }
            else
            {
                for (int i = 1; i < columns.Count; i++)
                {
                    SectionStyle a = GetSectionStyle(document, columns[i - 1]);
                    SectionStyle b = GetSectionStyle(document, columns[i]);
                    if (a==SectionStyle.RECTANGLE&&b==SectionStyle.RECTANGLE)
                    {
                        PlanarFace south1 = SolidFace.GetSouth(columns[i - 1]);
                        PlanarFace nouth1 = SolidFace.GetNouth(columns[i - 1]);
                        PlanarFace east1 = SolidFace.GetEast(columns[i - 1]);
                        PlanarFace west1 = SolidFace.GetWest(columns[i - 1]);
                        PlanarFace south2 = SolidFace.GetSouth(columns[i]);
                        PlanarFace nouth2 = SolidFace.GetNouth(columns[i]);
                        PlanarFace east2 = SolidFace.GetEast(columns[i]);
                        PlanarFace west2 = SolidFace.GetWest(columns[i]);
                        double b1 = PointModel.DistanceTo2(south1, nouth1.Origin, document);
                        double b2 = PointModel.DistanceTo2(south2, nouth2.Origin, document);
                        double h1 = PointModel.DistanceTo2(east1, west1.Origin, document);
                        double h2 = PointModel.DistanceTo2(east2, west2.Origin, document);
                        if (b1<b2||h1<h2)
                        {
                            return false;
                        }
                    }
                    if (a==SectionStyle.CYLINDICAL&&b==SectionStyle.CYLINDICAL)
                    {
                        double d1 = GetDiameter(document, columns[i - 1]);
                        double d2 = GetDiameter(document, columns[i]);
                        if (d1<d2)
                        {
                            return false;
                        }
                    }
                }
            }

            return true;
        }
        private static bool CompareOutSide(List<Element> columns, Document document)
        {
            if (columns.Count == 1)
            {
                return true;
            }
            else
            {
                for (int i = 1; i < columns.Count; i++)
                {
                    SectionStyle a = GetSectionStyle(document, columns[i - 1]);
                    SectionStyle b = GetSectionStyle(document, columns[i]);
                    if (a == SectionStyle.RECTANGLE && b == SectionStyle.RECTANGLE)
                    {
                        PlanarFace south1 = SolidFace.GetSouth(columns[i - 1]);
                        PlanarFace nouth1 = SolidFace.GetNouth(columns[i - 1]);
                        PlanarFace east1 = SolidFace.GetEast(columns[i - 1]);
                        PlanarFace west1 = SolidFace.GetWest(columns[i - 1]);
                        PlanarFace south2 = SolidFace.GetSouth(columns[i]);
                        PlanarFace nouth2 = SolidFace.GetNouth(columns[i]);
                        PlanarFace east2 = SolidFace.GetEast(columns[i]);
                        PlanarFace west2 = SolidFace.GetWest(columns[i]);
                        double b0 = PointModel.DistanceTo2(south1, nouth1.Origin, document);
                        double h0 = PointModel.DistanceTo2(east1, west1.Origin, document);
                        double totalB1 = PointModel.DistanceTo2(south1, south2.Origin, document)+ PointModel.DistanceTo2(nouth1, south2.Origin, document);
                        double totalB2 = PointModel.DistanceTo2(south1, nouth2.Origin, document)+ PointModel.DistanceTo2(nouth1, nouth2.Origin, document);
                        double totalH1 = PointModel.DistanceTo2(east1, east2.Origin, document)+ PointModel.DistanceTo2(west1, east2.Origin, document);
                        double totalH2 = PointModel.DistanceTo2(east1, west2.Origin, document)+ PointModel.DistanceTo2(west1, west2.Origin, document);
                        if (totalB1>b0||totalB2>b0|| totalH1>h0||totalH2>h0)
                        {
                            return false;
                        }
                    }
                    if (a == SectionStyle.CYLINDICAL && b == SectionStyle.CYLINDICAL)
                    {
                        double d1 = GetDiameter(document, columns[i - 1]);
                        double d2 = GetDiameter(document, columns[i]);
                        XYZ x1 = (columns[i - 1].Location as LocationPoint).Point as XYZ;
                        XYZ x2 = (columns[i].Location as LocationPoint).Point as XYZ;
                        double distance = Math.Abs(double.Parse(UnitFormatUtils.Format(document.GetUnits(), SpecTypeId.Length, x1.DistanceTo(x2), false)));
                        if (PointModel.AreEqual(d1,d2))
                        {
                            if (!PointModel.AreEqual(distance, 0))
                            {
                                return false;
                            }
                        }
                        else
                        {
                            if (distance+d2/2>d1/2)
                            {
                                return false;
                            }
                        }
                    }
                }
            }

            return true;
        }
        private static bool CompareOverTopPlanarFaceBeam(List<Element> columns, Document document)
        {
            for (int i = 0; i < columns.Count; i++)
            {
                PlanarFace planarFaceTopColumn = SolidFace.GetTop(columns[i]);
                List<Element> beams = ColumnsBoundingBox.GetBeamsBoudingBoxSameTopLevelPerpencularZOneColumn(columns[i], document);
                if (beams.Count!=0)
                {
                    for (int j = 0; j < beams.Count; j++)
                    {
                        List<PlanarFace> planarFaces = SolidFace.GetTopPlanarFaceBeam(beams[j], document);
                        if (planarFaces.Count!=0)
                        {
                           if(planarFaces[planarFaces.Count - 1].Origin.Z > planarFaceTopColumn.Origin.Z)
                            {
                                return false;
                            }
                                
                        }
                    }
                }
            }
            return true;
        }
        private static bool JoinBeamsToColumns(List<Element> columns, Document document)
        {
            for (int i = 0; i < columns.Count; i++)
            {
                List<Element> beams = ColumnsBoundingBox.GetBeamsBoudingBoxSameTopLevelPerpencularZOneColumn(columns[i], document);
                
                if (beams.Count != 0)
                {
                    for (int j = 0; j < beams.Count; j++)
                    {
                        if (!JoinGeometryUtils.AreElementsJoined(document, beams[j], columns[i]))
                        {
                            return false;
                        }
                        if (JoinGeometryUtils.IsCuttingElementInJoin(document, beams[j], columns[i]))
                        {
                            return false;
                        }
                    }
                }
            }
            return true;
        }
        private static bool JoinColumnsToFoundation(Element column0, Document document)
        {
            Element foundation = ColumnsBoundingBox.GetFoundationBoudingBoxOneColumn(column0, document);
            if (foundation!=null)
            {
                if (!JoinGeometryUtils.AreElementsJoined(document, column0, foundation)) return false;
                if (JoinGeometryUtils.IsCuttingElementInJoin(document, column0, foundation)) return false;
                PlanarFace bottom = SolidFace.GetBottom(column0);
                List<PlanarFace> planarFaces = SolidFace.GetTopPlanarFaceBeam(foundation, document);
                if (planarFaces.Count!=0)
                {
                    planarFaces=planarFaces.OrderBy(x => x.Origin.Z).ToList();
                    if (!PointModel.AreEqual(PointModel.DistanceTo2(planarFaces[planarFaces.Count - 1], bottom.Origin, document), 0)) return false;
                }
            }
            return true;
        }
        private static bool JoinColumnsToFloorFoundation(Element column0, Document document)
        {
            Element foundation = ColumnsBoundingBox.GetFoundationBoudingBoxOneColumn(column0, document);
            if (foundation==null)
            {
                Element floor = ColumnsBoundingBox.GetFloorBoudingBoxOneColumn(column0, document);
                if (floor != null)
                {
                    if (!JoinGeometryUtils.AreElementsJoined(document, column0, floor)) return false;
                    if (JoinGeometryUtils.IsCuttingElementInJoin(document, column0, floor)) return false;
                    PlanarFace bottom = SolidFace.GetBottom(column0);
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
        private static bool JoinColumnsToWallFoundation(Element column0, Document document)
        {
            Element foundation = ColumnsBoundingBox.GetFoundationBoudingBoxOneColumn(column0, document);
            if (foundation == null)
            {
                Element floor = ColumnsBoundingBox.GetFloorBoudingBoxOneColumn(column0, document);
                if (floor == null)
                {
                    
                    Element wall = ColumnsBoundingBox.GetWallBoudingBoxOneColumn(column0, document);
                    if (wall != null)
                    {
                        if (!JoinGeometryUtils.AreElementsJoined(document, column0, wall)) return false;
                        if (JoinGeometryUtils.IsCuttingElementInJoin(document, column0, wall)) return false;
                        PlanarFace bottom = SolidFace.GetBottom(column0);
                        List<PlanarFace> planarFaces = SolidFace.GetTopPlanarFaceBeam(wall, document);
                        if (planarFaces.Count != 0)
                        {
                            planarFaces.OrderBy(x => x.Origin.Z);
                            if (!PointModel.AreEqual(PointModel.DistanceTo2(planarFaces[planarFaces.Count - 1], bottom.Origin, document), 0)) return false;
                        }
                    }
                }
            }
            return true;
        }
        private static bool JoinColumnsToBeamFoundation(Element column0, Document document)
        {
            Element foundation = ColumnsBoundingBox.GetFoundationBoudingBoxOneColumn(column0, document);
            if (foundation == null)
            {
                Element floor = ColumnsBoundingBox.GetFloorBoudingBoxOneColumn(column0, document);
                if (floor == null)
                {
                    Element wall = ColumnsBoundingBox.GetWallBoudingBoxOneColumn(column0, document);
                    if (wall == null)
                    {
                        List<Element> beams = ColumnsBoundingBox.GetBeamsBoudingBoxSameBottomLevelPerpencularZOneColumn(column0, document);
                        if (beams.Count!=0)
                        {
                            List<PlanarFace> planarFaces = new List<PlanarFace>();
                            PlanarFace bottom = SolidFace.GetBottom(column0);
                            for (int i = 0; i < beams.Count; i++)
                            {
                                List<PlanarFace> p1 = SolidFace.GetTopPlanarFaceBeam(beams[i], document);
                                planarFaces.AddRange(p1);
                            }
                            if (planarFaces.Count!=0)
                            {
                                planarFaces.OrderBy(x => x.Origin.Z);
                                if (!PointModel.AreEqual(PointModel.DistanceTo2(planarFaces[0], bottom.Origin, document), 0)) return false;
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
            if (forgeTypeId == UnitTypeId.Feet)
            {
                if (formatOptions.Accuracy >= 1)
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
            "Higher Level Column has b or h or D larger than Lower Level Column ",
            "Higher Level Column is outside Lower Level Column ",
            "There is a beam Higher Top PlanrFace Column ",
            "Beams must be joint to Columns ",
            "Columns must be jointed Foundation or Error Foundation",
            "Columns must be jointed Floor as Foundation or Error Floor ",
            "Columns must be jointed Wall or Error Wall ",
            "Error Beams in bottom Columns "
        };
        

        #endregion
        #region
        private static bool VerticalOneColumn(Document document, Element column)
        {
            return (column.get_Parameter(BuiltInParameter.SLANTED_COLUMN_TYPE_PARAM).AsValueString().Equals("Vertical"));
        }
        #endregion
        public  enum SectionStyle { RECTANGLE, CYLINDICAL, ORTHER }
        public static SectionStyle GetSectionStyle(Document document, Element column)
        {
            SectionStyle a = SectionStyle.ORTHER;
            List<PlanarFace> planarFacesParallelZ = SolidFace.GetAllParallelZ(column);
            List<PlanarFace> planarFacesPerpencular = SolidFace.GetAllPerpencular(column);
            List<CylindricalFace> CylindricalFace = SolidFace.GetAllCylindricalFace(column);
            if (planarFacesParallelZ.Count==2&&planarFacesPerpencular.Count==4&&CylindricalFace.Count==0)
            {
                PlanarFace south = SolidFace.GetSouth(column);
                PlanarFace nouth = SolidFace.GetNouth(column);
                PlanarFace east = SolidFace.GetEast(column);
                PlanarFace west = SolidFace.GetWest(column);
                bool b1 = PointModel.AreEqual(south.FaceNormal.AngleTo(nouth.FaceNormal), Math.PI) && PointModel.AreEqual(south.FaceNormal.AngleTo(east.FaceNormal), Math.PI/2);
                bool b2 = PointModel.AreEqual(east.FaceNormal.AngleTo(west.FaceNormal), Math.PI) && PointModel.AreEqual(east.FaceNormal.AngleTo(south.FaceNormal), Math.PI/2);
                if (b1&&b2)
                {
                    a = SectionStyle.RECTANGLE;
                }
               
            }
            if (planarFacesParallelZ.Count==2&&planarFacesPerpencular.Count==0&&CylindricalFace.Count==2)
            {
                a = SectionStyle.CYLINDICAL;
            }
            return a;
        }
        private static double GetwidthColumn(Document document, Element column)
        {
            double a = 0;
            PlanarFace south = SolidFace.GetSouth(column);
            PlanarFace nouth = SolidFace.GetNouth(column);
            a=PointModel.DistanceTo2(south, nouth.Origin, document);
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
