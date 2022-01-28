using Autodesk.Revit.DB;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace R01_ColumnsRebar
{
    public class DimensionView
    {
        public Dimension HorizontalDetail { get; set; }
        public Dimension HorizontalSection { get; set; }
        public Dimension VerticalSection { get; set; }
        public DimensionType Type { get; set; }
        
        public DimensionView(SettingModel settingModel)
        {
            Type = settingModel.SelectedDimensionType;
        }
        public Reference ChangeReference(Document document, PlanarFace planarFace)
        {                                                                    
            string sam1 = planarFace.Reference.ConvertToStableRepresentation(document);
            string Refer1 = sam1.Replace("SURFACE", "LINEAR");
            Reference pl1 = Reference.ParseFromStableRepresentation(document, Refer1);
            return pl1;
        }
        public ReferenceArray GetReferenceArray(Document document, ObservableCollection<PlanarFace> planarFaces)
        {
            ReferenceArray referenceArray = new ReferenceArray();
            for (int i = 0; i < planarFaces.Count; i++)
            {
                referenceArray.Append(ChangeReference(document, planarFaces[i]));
            }
            return referenceArray;
        }

        #region Detail
        
        
        #endregion
        #region Rectangle
        private Line GetLineDimensionDetailRectangle(ViewSection viewSection, Document document, UnitProject unit, ObservableCollection<PlanarFace> planarFaces, ObservableCollection<InfoModel> infoModels, double offset0, double bmax0)
        {
            double offset = unit.Convert(offset0);
            double bmax = unit.Convert(bmax0 / 2);

            XYZ p1a = PointModel.ProjectToPlane(viewSection.Origin, infoModels[0].West);
            XYZ p2a = PointModel.ProjectToPlane(viewSection.Origin, infoModels[0].West);
            XYZ p1 = PointModel.ProjectToPlane(p1a, planarFaces[0]);
            XYZ p2 = PointModel.ProjectToPlane(p2a, planarFaces[planarFaces.Count - 1]);
            XYZ p3 = p1 + (bmax + offset) * (-1) * viewSection.RightDirection;
            XYZ p4 = p2 + (bmax + offset) * (-1) * viewSection.RightDirection;
            //XYZ p3 = p1 + (offset) * (-1) * viewSection.RightDirection;
            //XYZ p4 = p2 + ( offset) * (-1) * viewSection.RightDirection;
            return Line.CreateBound(p3, p4);
        }
        private Line GetLineDimensionSectionHorizontal(ViewSection viewSection, UnitProject unit, InfoModel infoModel, double offset0)
        {
            double offset = unit.Convert(offset0);
            XYZ p1 = PointModel.ProjectToPlane(viewSection.Origin, infoModel.South);
            XYZ p1a = PointModel.ProjectToPlane(p1, infoModel.West);
            XYZ p1b = p1a + offset * infoModel.West.FaceNormal;
            XYZ p2 = PointModel.ProjectToPlane(viewSection.Origin, infoModel.Nouth);
            XYZ p2a = PointModel.ProjectToPlane(p2, infoModel.West);
            XYZ p2b = p2a + offset * infoModel.West.FaceNormal;
            return Line.CreateBound(p1b, p2b);
        }
        private Line GetLineDimensionSectionVertical(ViewSection viewSection, UnitProject unit, InfoModel infoModel, double offset0)
        {
            double offset = unit.Convert(offset0);
            XYZ p1 = PointModel.ProjectToPlane(viewSection.Origin, infoModel.West);
            XYZ p1a = PointModel.ProjectToPlane(p1, infoModel.Nouth);
            XYZ p1b = p1a + offset * infoModel.Nouth.FaceNormal;
            XYZ p2 = PointModel.ProjectToPlane(viewSection.Origin, infoModel.East);
            XYZ p2a = PointModel.ProjectToPlane(p2, infoModel.Nouth);
            XYZ p2b = p2a + offset * infoModel.Nouth.FaceNormal;
            return Line.CreateBound(p1b, p2b);
        }
        public Reference GetReferenceItemRectangle(ViewSection viewSection, Document document, UnitProject unit, InfoModel infoModel, double l0, bool xy)
        {
            double l = unit.Convert(l0);
            XYZ p1 = PointModel.ProjectToPlane(viewSection.Origin, (xy) ? infoModel.East : infoModel.Nouth);
            XYZ p2 = PointModel.ProjectToPlane(p1, infoModel.Bottom);
            XYZ p3 = p2 + l * XYZ.BasisZ;
            XYZ p4 = p3 + 0.01 * ((xy) ? infoModel.East.FaceNormal : infoModel.Nouth.FaceNormal);
            Line line = Line.CreateBound(p3, p4);
            DetailCurve detailCurve = document.Create.NewDetailCurve(viewSection, line);
            return detailCurve.GeometryCurve.Reference;
        }
        public void CreateDimensionHorizontalSectionRectangle(ViewSection viewSection, Document document, UnitProject unit, InfoModel infoModel, SettingModel settingModel)
        {
            Line line = GetLineDimensionSectionHorizontal(viewSection, unit, infoModel, settingModel.DimV);
            ObservableCollection<PlanarFace> p1 = new ObservableCollection<PlanarFace>();
            p1.Add(infoModel.South); p1.Add(infoModel.Nouth);
            ReferenceArray referenceArray = GetReferenceArray(document, p1);
            Type = settingModel.SelectedDimensionType;
            HorizontalSection = document.Create.NewDimension(viewSection, line, referenceArray, Type);
        }
       
        public void CreateDimensionVerticalSectionRectangle(ViewSection viewSection, Document document, UnitProject unit, InfoModel infoModel, SettingModel settingModel)
        {
            Line line = GetLineDimensionSectionVertical(viewSection, unit, infoModel, settingModel.DimH);
            ObservableCollection<PlanarFace> p1 = new ObservableCollection<PlanarFace>();
            p1.Add(infoModel.West); p1.Add(infoModel.East);
            ReferenceArray referenceArray = GetReferenceArray(document, p1);

            Type = settingModel.SelectedDimensionType;
            VerticalSection = document.Create.NewDimension(viewSection, line, referenceArray, Type);
        }
        public void CreateDimensionHorizontalDetailRectangle(ViewSection viewSection, Document document, UnitProject unit, ObservableCollection<PlanarFace> planarFaces, ObservableCollection<InfoModel> infoModels, SettingModel settingModel, double bmax0)
        {
            Line line = GetLineDimensionDetailRectangle(viewSection, document, unit, planarFaces, infoModels, settingModel.L1, bmax0);
            //ReferenceArray referenceArray = GetReferenceArray(document, planarFaces);
            ReferenceArray referenceArray = new ReferenceArray();
            foreach (var item in planarFaces)
            {
                referenceArray.Append(item.Reference);
            }
            Type = settingModel.SelectedDimensionType;
            HorizontalDetail = document.Create.NewDimension(viewSection, line, referenceArray, Type);
        }
        public void CreateDimensionHorizontalDetailStirrupRectangle(ViewSection viewSection, Document document, UnitProject unit, ObservableCollection<PlanarFace> planarFaces, ObservableCollection<InfoModel> infoModels, ObservableCollection<StirrupModel> stirrupModels, SettingModel settingModel, double bmax0, bool xy)
        {
            Line line = GetLineDimensionDetailRectangle(viewSection, document, unit, planarFaces, infoModels, settingModel.L1, bmax0);
            ReferenceArray referenceArray = GetReferenceArray(document, planarFaces);
            for (int i = 0; i < stirrupModels.Count; i++)
            {
                double l1 = (stirrupModels[i].TypeDis == 0) ? stirrupModels[i].L * 0.25 : stirrupModels[i].L1;
                double l2 = (stirrupModels[i].TypeDis == 0) ? stirrupModels[i].L * 0.5 : stirrupModels[i].L2;
                referenceArray.Append(GetReferenceItemRectangle(viewSection, document, unit, infoModels[i], l1, xy));
                referenceArray.Append(GetReferenceItemRectangle(viewSection, document, unit, infoModels[i], l1 + l2, xy));
            }
            Type = settingModel.SelectedDimensionType;
            HorizontalDetail = document.Create.NewDimension(viewSection, line, referenceArray, Type);
        }
        #endregion
        #region Cylindrical
        private Line GetLineDimensionDetailCylindrical(ViewSection viewSection, Document document, UnitProject unit, ObservableCollection<PlanarFace> planarFaces, ObservableCollection<InfoModel> infoModels, double offset0, double bmax0)
        {
            double offset = unit.Convert(offset0);
            double bmax = unit.Convert(bmax0 / 2);

            XYZ p1a = viewSection.Origin + (bmax + offset) * (-1) * viewSection.RightDirection;
            XYZ p2a = viewSection.Origin + (bmax + offset) * (-1) * viewSection.RightDirection;
            XYZ p1 = PointModel.ProjectToPlane(p1a, planarFaces[0]);
            XYZ p2 = PointModel.ProjectToPlane(p2a, planarFaces[planarFaces.Count - 1]);
            return Line.CreateBound(p1, p2);
        }
        public Reference GetReferenceItemDetailCylindrical(ViewSection viewSection, Document document, UnitProject unit, InfoModel infoModel, double l0)
        {
            double l = unit.Convert(l0);
            XYZ p1a = new XYZ(unit.Convert(infoModel.PointXPosition), unit.Convert(infoModel.PointYPosition), viewSection.Origin.Z);
            XYZ p1 = PointModel.ProjectToPlane(p1a,infoModel.Bottom);
            XYZ p2 = p1 + unit.Convert(infoModel.D / 2) * (-1) * viewSection.RightDirection;
            XYZ p3 = p2 + l * XYZ.BasisZ;
            XYZ p4 = p3 + 0.01 * (-1) * viewSection.RightDirection;
            Line line = Line.CreateBound(p3, p4);
            DetailCurve detailCurve = document.Create.NewDetailCurve(viewSection, line);
           
            return detailCurve.GeometryCurve.Reference;
        }
        public Reference GetReferenceItemSectionCylindrical(ViewSection viewSection, Document document, UnitProject unit, InfoModel infoModel, bool horizontal, bool left)
        {
            XYZ p1 = new XYZ(infoModel.PointPosition.X, infoModel.PointPosition.Y, viewSection.Origin.Z);
            XYZ p1a = p1 + unit.Convert(infoModel.D / 2) * (-1) * viewSection.RightDirection;
            XYZ p1b = p1a + unit.Convert(infoModel.D / 2) * viewSection.UpDirection;
            XYZ p1c = p1a + unit.Convert(infoModel.D / 2) * (-1) * viewSection.UpDirection;
            XYZ p1d = p1 + unit.Convert(infoModel.D / 2) * viewSection.RightDirection;
            XYZ p1e = p1d + unit.Convert(infoModel.D / 2) * viewSection.UpDirection;

            XYZ p3 = null;
            XYZ p4 = null;
            if (horizontal)
            {
                if (left)
                {
                    p3 = p1b;

                }
                else
                {
                    p3 = p1e;
                }
                p4 = p3 + 0.01 * viewSection.UpDirection;
            }
            else
            {
                if (left)
                {
                    p3 = p1b;
                }
                else
                {
                    p3 = p1c;
                }
                p4 = p3 + 0.01 * (-1) * viewSection.RightDirection;
            }
            Line line = Line.CreateBound(p3, p4);
            DetailCurve detailCurve = document.Create.NewDetailCurve(viewSection, line);
            return detailCurve.GeometryCurve.Reference;
        }
        private Line GetLineDimensionSectionCylindrical(ViewSection viewSection, UnitProject unit, InfoModel infoModel, bool horizontal, double offset0)
        {
            double offset = unit.Convert(offset0);
            XYZ p1 = new XYZ(infoModel.PointPosition.X, infoModel.PointPosition.Y, viewSection.Origin.Z);
            XYZ p1a = p1 + unit.Convert(infoModel.D / 2) * (-1) * viewSection.RightDirection;
            XYZ p1b = p1a + unit.Convert(infoModel.D / 2) * viewSection.UpDirection;
            XYZ p1c = p1a + unit.Convert(infoModel.D / 2) * (-1) * viewSection.UpDirection;
            XYZ p1d = p1 + unit.Convert(infoModel.D / 2) * viewSection.RightDirection;
            XYZ p1e = p1d + unit.Convert(infoModel.D / 2) * viewSection.UpDirection;
            XYZ p3 = null;
            XYZ p4 = null;
            if (horizontal)
            {
                p3 = p1b + offset * viewSection.UpDirection;
                p4 = p1e + offset * viewSection.UpDirection;
            }
            else
            {
                p3 = p1b + offset * (-1) * viewSection.RightDirection;
                p4 = p1c + offset * (-1) * viewSection.RightDirection;
            }
            return Line.CreateBound(p3, p4);
        }
        public void CreateDimensionHorizontalSectionCylindrical(ViewSection viewSection, Document document, UnitProject unit, InfoModel infoModel, SettingModel settingModel)
        {
            Line line = GetLineDimensionSectionCylindrical(viewSection, unit, infoModel, true, settingModel.DimH);
            ReferenceArray referenceArray = new ReferenceArray();
            referenceArray.Append(GetReferenceItemSectionCylindrical(viewSection, document, unit, infoModel, true, true));
            referenceArray.Append(GetReferenceItemSectionCylindrical(viewSection, document, unit, infoModel, true, false));
            Type = settingModel.SelectedDimensionType;
            HorizontalSection = document.Create.NewDimension(viewSection, line, referenceArray, Type);
        }
        public void CreateDimensionVerticalSectionCylindrical(ViewSection viewSection, Document document, UnitProject unit, InfoModel infoModel, SettingModel settingModel)
        {
            Line line = GetLineDimensionSectionCylindrical(viewSection, unit, infoModel, false, settingModel.DimV);
            ReferenceArray referenceArray = new ReferenceArray();
            referenceArray.Append(GetReferenceItemSectionCylindrical(viewSection, document, unit, infoModel, false, true));
            referenceArray.Append(GetReferenceItemSectionCylindrical(viewSection, document, unit, infoModel, false, false));
            Type = settingModel.SelectedDimensionType;
            VerticalSection = document.Create.NewDimension(viewSection, line, referenceArray, Type);
        }
        public void CreateDimensionHorizontalDetailCylindrical(ViewSection viewSection, Document document, UnitProject unit, ObservableCollection<PlanarFace> planarFaces, ObservableCollection<InfoModel> infoModels, SettingModel settingModel, double bmax0)
        {
            Line line = GetLineDimensionDetailCylindrical(viewSection, document, unit, planarFaces, infoModels, settingModel.DimH, bmax0);
            ReferenceArray referenceArray = GetReferenceArray(document, planarFaces);
            Type = settingModel.SelectedDimensionType;
            HorizontalDetail = document.Create.NewDimension(viewSection, line, referenceArray, Type);
        }
        public void CreateDimensionHorizontalDetailStirrupCylindrical(ViewSection viewSection, Document document, UnitProject unit, ObservableCollection<PlanarFace> planarFaces, ObservableCollection<InfoModel> infoModels, ObservableCollection<StirrupModel> stirrupModels, SettingModel settingModel, double bmax0)
        {
            Line line = GetLineDimensionDetailCylindrical(viewSection, document, unit, planarFaces, infoModels, settingModel.DimH, bmax0);
            ReferenceArray referenceArray = GetReferenceArray(document, planarFaces);
            for (int i = 0; i < stirrupModels.Count; i++)
            {
                double l1 = (stirrupModels[i].TypeDis == 0) ? stirrupModels[i].L * 0.25 : stirrupModels[i].L1;
                double l2 = (stirrupModels[i].TypeDis == 0) ? stirrupModels[i].L * 0.5 : stirrupModels[i].L2;
                referenceArray.Append(GetReferenceItemDetailCylindrical(viewSection, document, unit, infoModels[i], l1));
                referenceArray.Append(GetReferenceItemDetailCylindrical(viewSection, document, unit, infoModels[i], l1 + l2));
            }
           
            Type = settingModel.SelectedDimensionType;
            HorizontalDetail = document.Create.NewDimension(viewSection, line, referenceArray, Type);
        }
        #endregion
    }
}
