using Autodesk.Revit.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using static R01_ColumnsRebar.ErrorColumns;

namespace R01_ColumnsRebar
{
    public class SectionColumnView
    {
        public ViewFamilyType Section { get; set; }
        public BoundingBoxXYZ StartSection { get; set; }
        public BoundingBoxXYZ MidSection { get; set; }
        public BoundingBoxXYZ EndSection { get; set; }
        public ViewSection StartView { get; set; }
        public ViewSection MidView { get; set; }
        public ViewSection EndView { get; set; }
        public string Type = "@ColumnSection";
        public SectionColumnView()
        {
        }
        private void GetNameSection(Document document)
        {
            List<ViewFamilyType> list = new FilteredElementCollector(document)
                                .OfClass(typeof(ViewFamilyType))
                                .Cast<ViewFamilyType>()
                                .Where(x => ViewFamily.Section == x.ViewFamily && x.Name.Equals(Type)).ToList();
            if (list.Count == 0)
            {
                ViewFamilyType a = new FilteredElementCollector(document)
                                .OfClass(typeof(ViewFamilyType))
                                .Cast<ViewFamilyType>()
                                .Where(x => ViewFamily.Section == x.ViewFamily).FirstOrDefault();
                Section = a.Duplicate(Type) as ViewFamilyType;
            }
            else
            {
                Section = list.Where(x => x.Name.Equals(Type)).FirstOrDefault();
            }
        }
        private BoundingBoxXYZ CreateSectionBox(SectionStyle sectionStyle, UnitProject unit, Document document, InfoModel infoModel, double location0, double offset0)
        {
            BoundingBoxXYZ a = new BoundingBoxXYZ();
            double offset = unit.Convert(offset0);
            double location = unit.Convert(location0);
            if (sectionStyle == SectionStyle.RECTANGLE)
            {
                double b = unit.Convert(infoModel.b);
                double h = unit.Convert(infoModel.h);
                XYZ p1 = infoModel.West.Origin + unit.Convert(Math.Abs(infoModel.WestPosition - infoModel.EastPosition)) * 0.5 * infoModel.East.FaceNormal;
                XYZ p2 = PointModel.ProjectToPlane(p1, infoModel.South);
                XYZ p3 = p2 + unit.Convert(Math.Abs(infoModel.SouthPosition - infoModel.NouthPosition)) * 0.5 * infoModel.Nouth.FaceNormal;
                XYZ p4 = PointModel.ProjectToPlane(p3, infoModel.Bottom);
                XYZ mid = p4 + location * XYZ.BasisZ;
                XYZ min = new XYZ(-b / 2 - offset / 2, -h / 2 - offset / 2, 0);
                XYZ max = new XYZ(b / 2 + 2.5 * offset, h / 2 + offset / 2, offset / 2);
                Transform t = Transform.CreateRotation(XYZ.BasisX, Math.PI);
                t.Origin = mid;
                a.Transform = t;
                a.Min = min;
                a.Max = max;
            }
            else
            {
                double D = unit.Convert(infoModel.D);
                XYZ p1 = (infoModel.Element.Location as LocationPoint).Point as XYZ;
                XYZ p2 = PointModel.ProjectToPlane(p1, infoModel.Bottom);
                XYZ mid = p2 + location * XYZ.BasisZ;
                XYZ min = new XYZ(-D / 2 - offset / 2, -D / 2 - offset / 2, 0);
                XYZ max = new XYZ(D / 2 + 2.5 * offset, D / 2 + offset / 2, offset / 2);
                Transform t = Transform.CreateRotation(XYZ.BasisX, Math.PI);
                t.Origin = mid;
                a.Transform = t;
                a.Min = min;
                a.Max = max;
            }

            return a;
        }
        public void CreateSectionView(SectionStyle sectionStyle, UnitProject unit, Document document, InfoModel infoModel, StirrupModel stirrupModel, SettingModel settingModel, double offset0)
        {
            GetNameSection(document);
            double location1 = 0, location2 = 0, location3 = 0;
            if (stirrupModel.TypeDis == 0)
            {
                location1 = stirrupModel.L * 0.125;
                location2 = stirrupModel.L * 0.5;
                location3 = stirrupModel.L * 0.875;
            }
            else
            {
                location1 = stirrupModel.L1 * 0.5;
                location2 = stirrupModel.L1 + stirrupModel.L2 * 0.5;
                location3 = stirrupModel.L1 + stirrupModel.L2 + stirrupModel.L1 * 0.5;
            }
            //StartSection = CreateSectionBox(sectionStyle, unit, document, infoModel, location1, offset0);
            //StartView = ViewSection.CreateSection(document, Section.Id, StartSection);
            //StartView.get_Parameter(BuiltInParameter.VIEWER_CROP_REGION_VISIBLE).Set(0);
            //StartView.ViewTemplateId = settingModel.SelectedSectionTemplate.Id;
            //StartView.Name = settingModel.DetailViewName + " " + infoModel.NumberColumn + " " + settingModel.PrefixSection + " 1-1";

            //EndSection = CreateSectionBox(sectionStyle, unit, document, infoModel, location3, offset0);
            //EndView = ViewSection.CreateSection(document, Section.Id, EndSection);
            //EndView.get_Parameter(BuiltInParameter.VIEWER_CROP_REGION_VISIBLE).Set(0);
            //EndView.ViewTemplateId = settingModel.SelectedSectionTemplate.Id;
            //EndView.Name = settingModel.DetailViewName + " " + infoModel.NumberColumn + " " + settingModel.PrefixSection + " 3-3";

            MidSection = CreateSectionBox(sectionStyle, unit, document, infoModel, location2, offset0);
            MidView = ViewSection.CreateSection(document, Section.Id, MidSection);
            MidView.get_Parameter(BuiltInParameter.VIEWER_CROP_REGION_VISIBLE).Set(0);
            MidView.ViewTemplateId = settingModel.SelectedSectionTemplate.Id;
            MidView.Name = settingModel.DetailViewName + " " + infoModel.NumberColumn + " " + settingModel.PrefixSection ;
        }


    }
}
