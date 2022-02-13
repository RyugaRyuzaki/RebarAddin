using Autodesk.Revit.DB;
using System;
using System.Collections.Generic;
using DSP;
namespace R02_BeamsRebar
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
        public ReferenceArray GetReferenceArray(Document document, List<PlanarFace> planarFaces)
        {
            ReferenceArray referenceArray = new ReferenceArray();
            for (int i = 0; i < planarFaces.Count; i++)
            {
                referenceArray.Append(ChangeReference(document, planarFaces[i]));
            }
            return referenceArray;
        }
        private double GetHmax(Document document,List<InfoModel> infoModels)
        {
            double hmax0 = 0;
            for (int i = 0; i < infoModels.Count; i++)
            {
                ElementType elementType = document.GetElement(infoModels[i].Element.GetTypeId()) as ElementType;
                double h = elementType.LookupParameter("h").AsDouble();
                if (hmax0 < h)
                {
                    hmax0 = h;
                }
            }
            return hmax0;
        }
        private double GetOffsetDimH(Document document, SettingModel settingModel, List<InfoModel> infoModels)
        {
            double bmax0 = 0;
            double bmax = 0;
            for (int i = 0; i < infoModels.Count; i++)
            {
                ElementType elementType = document.GetElement(infoModels[i].Element.GetTypeId()) as ElementType;
                double b = elementType.LookupParameter("b").AsDouble();
                if (bmax0 < b)
                {
                    bmax0 = b;
                }
                if (bmax < infoModels[i].b)
                {
                    bmax = infoModels[i].b;
                }
            }
            double offset = settingModel.DimH * bmax0 / bmax;
            return offset;
        }
        private double GetOffsetDimV(Document document, SettingModel settingModel, List<InfoModel> infoModels)
        {
            double bmax0 = 0;
            double bmax = 0;
            for (int i = 0; i < infoModels.Count; i++)
            {
                ElementType elementType = document.GetElement(infoModels[i].Element.GetTypeId()) as ElementType;
                double b = elementType.LookupParameter("b").AsDouble();
                if (bmax0 < b)
                {
                    bmax0 = b;
                }
                if (bmax < infoModels[i].b)
                {
                    bmax = infoModels[i].b;
                }
            }
            double offset = settingModel.DimV * bmax0 / bmax;
            return offset;
        }
        private double GetOffsetDetail(Document document, SettingModel settingModel, List<InfoModel> infoModels)
        {
            double hmax0 = 0;
            double hmax = 0;
            for (int i = 0; i < infoModels.Count; i++)
            {
                ElementType elementType = document.GetElement(infoModels[i].Element.GetTypeId()) as ElementType;
                double h = elementType.LookupParameter("h").AsDouble();
                if (hmax0 < h)
                {
                    hmax0 = h;
                }
                if (hmax < infoModels[i].h)
                {
                    hmax = infoModels[i].h;
                }
            }
            double offset = settingModel.L1 * hmax0 / hmax;
            return offset;
        }
        private XYZ GetPointOriginDetail(List<InfoModel> infoModels)
        {
            Line l = LineProcess.GetLineFromElement(infoModels[0].Element);
            XYZ a = l.GetEndPoint(0);
            return a;
        }
        private Line GetLineDimensionDetail(Document document, List<PlanarFace> planarFaces, List<InfoModel> infoModels, double offset, bool updown)
        {
            double hmax = GetHmax(document, infoModels);
            XYZ a = GetPointOriginDetail(infoModels);
            XYZ p1 = PointModel.ProjectToPlane(a, planarFaces[0]);
            XYZ p2 = PointModel.ProjectToPlane(a, planarFaces[planarFaces.Count-1]);
            XYZ p3 = null;
            XYZ p4 = null;
            if (updown)
            {
                p3 = new XYZ(p1.X, p1.Y, a.Z +offset);
                p4 = new XYZ(p2.X, p2.Y, a.Z + offset);
            }
            else
            {
                p3 = new XYZ(p1.X, p1.Y, a.Z - offset-hmax);
                p4 = new XYZ(p2.X, p2.Y, a.Z - offset-hmax);
            }
            Line line = null;
            if (p3!=null&&p4!=null)
            {
                line = Line.CreateBound(p3, p4);
            }
            return line;
        }
        private Line GetLineDimensionHorizontal(ViewSection viewSection, List<PlanarFace> planarFaces, List<PlanarFace> planarPer, double offset)
        {
            
            if (planarFaces.Count > 2 || planarFaces.Count == 0) return null;
            if (planarPer.Count > 2 || planarPer.Count == 0) return null;
            PlanarFace planarPer0 = (planarPer[0].Origin.Z>planarPer[1].Origin.Z)?planarPer[0]:planarPer[1];
            XYZ p1 = PointModel.ProjectToPlane(planarFaces[1].Origin, planarPer0);
            XYZ p2 = PointModel.ProjectToPlane(planarFaces[0].Origin, planarPer0);
            Line line = Line.CreateBound((new XYZ(p1.X, p1.Y, p1.Z + offset)), (new XYZ(p2.X, p2.Y, p2.Z + offset)));
            return line;
        }
        private Line GetLineDimensionVertical(ViewSection viewSection, List<PlanarFace> planarFaces, List<PlanarFace> planarPer, double offset)
        {
            
            if (planarFaces.Count > 2 || planarFaces.Count == 0) return null;
            if (planarPer.Count > 2 || planarPer.Count == 0) return null;
            PlanarFace planarPer0 = null;
            for (int i = 0; i < planarPer.Count; i++)
            {
                if ((Math.Abs(planarPer[i].FaceNormal.AngleTo(viewSection.RightDirection) - Math.PI) < 1e-9))
                {
                    planarPer0 = planarPer[i];
                }
            }
            XYZ p1 = PointModel.ProjectToPlane(planarFaces[0].Origin, planarPer0);
            XYZ p2 = PointModel.ProjectToPlane(planarFaces[1].Origin, planarPer0);
            Line l1 = Line.CreateBound(p1, p2);
            Line line = l1.CreateTransformed(Transform.CreateTranslation(offset* (-1*viewSection.RightDirection))) as Line;

            return line;
        }
        public void CreateDimensionHorizontalDetail(Autodesk.Revit.DB.View view, Document document,UnitProject unit, List<PlanarFace> planarFaces, SettingModel settingModel, List<InfoModel> infoModels, bool updown, double offset0)
        {
            double offset = unit.Convert(offset0);
            Line line = GetLineDimensionDetail(document, planarFaces, infoModels, offset, updown);
            //ReferenceArray referenceArray = GetReferenceArray(document, planarFaces);
            ReferenceArray referenceArray = new ReferenceArray();
            foreach (var item in planarFaces)
            {
                referenceArray.Append(item.Reference);
            }
            Type = settingModel.SelectedDimensionType;
            HorizontalDetail = document.Create.NewDimension(view, line, referenceArray, Type);
        }
        public void CreateDimensionHorizontalSection(ViewSection viewSection, Document document, UnitProject unit, List<PlanarFace> planarFaces, List<PlanarFace> planarPer, SettingModel settingModel, List<InfoModel> infoModels)
        {
            double offset = unit.Convert(settingModel.DimV);
            Line line = GetLineDimensionHorizontal(viewSection, planarFaces, planarPer, offset);
            List<PlanarFace> p1 = new List<PlanarFace>();
            ReferenceArray referenceArray = GetReferenceArray(document, planarFaces);
            //ReferenceArray referenceArray = new ReferenceArray();
            //foreach (var item in p1)
            //{
            //    referenceArray.Append(item.Reference);
            //}
            Type = settingModel.SelectedDimensionType;
            HorizontalSection = document.Create.NewDimension(viewSection, line, referenceArray, Type);
        }
        public void CreateDimensionVerticalSection(ViewSection viewSection, Document document, UnitProject unit, List<PlanarFace> planarFaces, List<PlanarFace> planarPer, SettingModel settingModel, List<InfoModel> infoModels)
        {
            double offset = unit.Convert(settingModel.DimH);
            Line line = GetLineDimensionVertical(viewSection, planarFaces, planarPer, offset);
            ReferenceArray referenceArray = GetReferenceArray(document, planarFaces);
            //ReferenceArray referenceArray = new ReferenceArray();
            //foreach (var item in planarFaces)
            //{
            //    referenceArray.Append(item.Reference);
            //}
            Type = settingModel.SelectedDimensionType;
            VerticalSection = document.Create.NewDimension(viewSection, line, referenceArray, Type);
        }
        public void CreateDimensionHorizontalAddTopBar(Autodesk.Revit.DB.View view, Document document, UnitProject unit, List<PlanarFace> planarFaces, ReferenceArray referenceArray, SettingModel settingModel, List<InfoModel> infoModels, bool updown, double offset0)
        {
            double offset = unit.Convert(offset0);
            Line line = GetLineDimensionDetail(document, planarFaces, infoModels, offset, updown);
            Type = settingModel.SelectedDimensionType;
            HorizontalSection = document.Create.NewDimension(view, line, referenceArray, Type);
        }
        public void CreateDimensionHorizontalStirrup(Autodesk.Revit.DB.View view, Document document, UnitProject unit, List<PlanarFace> planarFaces, ReferenceArray referenceArray, SettingModel settingModel, List<InfoModel> infoModels, bool updown, double offset0)
        {
            double offset = unit.Convert(offset0);
            Line line = GetLineDimensionDetail(document, planarFaces, infoModels, offset, updown);
            Type = settingModel.SelectedDimensionType;
            HorizontalSection = document.Create.NewDimension(view, line, referenceArray, Type);
        }
    }
}
