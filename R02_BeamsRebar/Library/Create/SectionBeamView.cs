using Autodesk.Revit.DB;
using System;
using System.Collections.Generic;
using System.Linq;

namespace R02_BeamsRebar
{
    public class SectionBeamView
    {
        public ViewFamilyType Section { get; set; }
        public BoundingBoxXYZ StartSection { get; set; }
        public BoundingBoxXYZ MidSection { get; set; }
        public BoundingBoxXYZ EndSection { get; set; }
        public ViewSection StartView { get; set; }
        public ViewSection MidView { get; set; }
        public ViewSection EndView { get; set; }
        public string Type = "@BeamSection";
        public SectionBeamView()
        {
        }
        public void GetNameSection(Document document)
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
        public BoundingBoxXYZ CreateSectionBox(Document document, InfoModel infoModel, double location)
        {
            Line l = LineProcess.GetLineFromElement(infoModel.Element);
            ElementType elementType = document.GetElement(infoModel.Element.GetTypeId()) as ElementType;
            double b = elementType.LookupParameter("b").AsDouble();
            double h = elementType.LookupParameter("h").AsDouble();
            XYZ p0 = l.GetEndPoint(0);
            XYZ p01 = p0 + (b / 2) * (-1) * infoModel.LeftRightPlanar[0].FaceNormal;
            XYZ p =PointModel.ProjectToPlane(p01, infoModel.StartPlanar);
            XYZ q = PointModel.ProjectToPlane(p01, infoModel.EndPlanar);
            XYZ v = q - p;
            BoundingBoxXYZ boxXYZ = infoModel.Element.get_BoundingBox(null);
            double minZ = boxXYZ.Min.Z;
            double maxZ = boxXYZ.Max.Z;
            XYZ min = new XYZ(-3 * b, minZ - h/3, 0);
            XYZ max = new XYZ(3 * b, maxZ + h/3, h);


            XYZ midpoint = new XYZ((p + location * v).X, (p + location * v).Y, v.Z);
            XYZ walldir = v.Normalize();
            XYZ up = XYZ.BasisZ;
            XYZ viewdir = walldir.CrossProduct(up);

            Transform t = Transform.Identity;
            t.Origin = midpoint;
            t.BasisX = -viewdir;
            t.BasisY = up;
            t.BasisZ = walldir;

            BoundingBoxXYZ a = new BoundingBoxXYZ();
            a.Transform = t;
            a.Min = min;
            a.Max = max;
            return a;
        }
        public void CeateSectionView(Document document, InfoModel infoModel, SettingModel settingModel)
        {
            GetNameSection(document);
            List<double> location = new List<double>() { 0.125, 0.5, 0.875 };
            StartSection = CreateSectionBox(document, infoModel, location[0]);
            StartView = ViewSection.CreateSection(document, Section.Id, StartSection);
            StartView.get_Parameter(BuiltInParameter.VIEWER_CROP_REGION_VISIBLE).Set(0);
            MidSection = CreateSectionBox(document, infoModel, location[1]);
            MidView = ViewSection.CreateSection(document, Section.Id, MidSection);
            MidView.get_Parameter(BuiltInParameter.VIEWER_CROP_REGION_VISIBLE).Set(0);
            EndSection = CreateSectionBox(document, infoModel, location[2]);
            EndView = ViewSection.CreateSection(document, Section.Id, EndSection);
            EndView.get_Parameter(BuiltInParameter.VIEWER_CROP_REGION_VISIBLE).Set(0);
            StartView.ViewTemplateId = settingModel.SelectedSectionTemplate.Id;
            MidView.ViewTemplateId = settingModel.SelectedSectionTemplate.Id;
            EndView.ViewTemplateId = settingModel.SelectedSectionTemplate.Id;
            
        }
       
        
    }
}
