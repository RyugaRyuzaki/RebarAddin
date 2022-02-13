using Autodesk.Revit.DB;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Forms;
using static R01_ColumnsRebar.ErrorColumns;
using DSP;
namespace R01_ColumnsRebar
{
    public class DetailShopView
    {
        public ViewFamilyType Detail { get; set; }
        public BoundingBoxXYZ SectionBox { get; set; }
        public ViewSection DetailShop { get; set; }
        public ElementId Schedule { get; set; }
        public ViewSchedule ViewSchedule { get; set; }
        public string Type = "@ColumnsDetailShop";
        public DetailShopView()
        {
        }
        public void GetNameDetail(Document document)
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
                Detail = a.Duplicate(Type) as ViewFamilyType;
            }
            else
            {
                Detail = list.Where(x => x.Name.Equals(Type)).FirstOrDefault();
            }

        }
        public void GetSectionBox(SectionStyle sectionStyle, Document document, UnitProject unit, ObservableCollection<InfoModel> InfoModels, ObservableCollection<PlanarFace> planarFaces, List<Element> columns, double offset0,double deltal0)
        {
            double offset = unit.Convert(offset0);
            double deltal = unit.Convert(deltal0);
            double h = unit.Convert(InfoModels[InfoModels.Count - 1].TopPosition);
            if (sectionStyle == SectionStyle.RECTANGLE)
            {
                XYZ p1 = InfoModels[InfoModels.Count - 1].West.Origin + unit.Convert(Math.Abs(InfoModels[InfoModels.Count - 1].WestPosition - InfoModels[InfoModels.Count - 1].EastPosition)) * 0.5 * InfoModels[InfoModels.Count - 1].East.FaceNormal;
                XYZ p2 = PointModel.ProjectToPlane(p1, InfoModels[InfoModels.Count - 1].South);
                XYZ p3 = p2 + unit.Convert(Math.Abs(InfoModels[InfoModels.Count - 1].SouthPosition - InfoModels[InfoModels.Count - 1].NouthPosition)) * 0.5 * InfoModels[InfoModels.Count - 1].Nouth.FaceNormal;
                XYZ p4 = PointModel.ProjectToPlane(p3, planarFaces[0]);
                XYZ mid = p4 + h * 0.5 * XYZ.BasisZ;
                double xX = unit.Convert(Math.Abs(InfoModels[0].WestPosition - InfoModels[0].EastPosition)) * 0.5;
                XYZ dirX = InfoModels[0].East.FaceNormal;
                XYZ upX = XYZ.BasisZ;
                XYZ viewDirX = dirX.CrossProduct(upX);
                XYZ minX = new XYZ(-0.5 * xX - offset-deltal, -0.5 * h - offset, 0);
                XYZ maxX = new XYZ(0.5 * xX + offset, 0.5 * h + offset, offset);
                Transform tX = Transform.Identity;
                tX.Origin = mid;
                tX.BasisX = dirX;
                tX.BasisY = upX;
                tX.BasisZ = viewDirX;
                SectionBox = new BoundingBoxXYZ();
                SectionBox.Transform = tX;
                SectionBox.Min = minX;
                SectionBox.Max = maxX;
               
            }
            else
            {
                XYZ p1 = (columns[columns.Count - 1].Location as LocationPoint).Point as XYZ;
                XYZ p2 = PointModel.ProjectToPlane(p1, planarFaces[0]);
                XYZ mid = p2 + h * 0.5 * XYZ.BasisZ;
                double xX = unit.Convert(InfoModels[0].D) * 0.5;
                XYZ dirX = XYZ.BasisX;
                XYZ upX = XYZ.BasisZ;
                XYZ viewDirX = dirX.CrossProduct(upX);
                XYZ minX = new XYZ(-0.5 * xX - offset - deltal, -0.5 * h - offset, 0);
                XYZ maxX = new XYZ(0.5 * xX + offset, 0.5 * h + offset, offset);
                Transform tX = Transform.Identity;
                tX.Origin = mid;
                tX.BasisX = dirX;
                tX.BasisY = upX;
                tX.BasisZ = viewDirX;
                SectionBox = new BoundingBoxXYZ();
                SectionBox.Transform = tX;
                SectionBox.Min = minX;
                SectionBox.Max = maxX;
               
            }


        }
        public void CreateDetailShopView(SectionStyle sectionStyle, Document document, UnitProject unit, ObservableCollection<InfoModel> InfoModels, ObservableCollection<PlanarFace> planarFaces, List<Element> columns, SettingModel settingModel, string nameX,  double offset0, double deltal0)
        {
            GetNameDetail(document);
            GetSectionBox(sectionStyle, document, unit, InfoModels, planarFaces, columns, offset0, deltal0);
            DetailShop = ViewSection.CreateSection(document, Detail.Id, SectionBox);
            try
            {
                DetailShop.Name = nameX;
            }
            catch (System.Exception)
            {
                MessageBox.Show("There is an existing Section View" + "\n" + "Please choose another Beam Name", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            DetailShop.get_Parameter(BuiltInParameter.VIEWER_CROP_REGION_VISIBLE).Set(0);
            DetailShop.ViewTemplateId = settingModel.SelectedDetailShopTemplate.Id;
        }
        public void CreateSchedule(Document document,SettingModel settingModel)
        {
            Schedule = new FilteredElementCollector(document).OfCategory(BuiltInCategory.OST_DetailComponents).FirstElementId();
            if (Schedule!=null&&Schedule!=ElementId.InvalidElementId)
            {
                
                ViewSchedule = ViewSchedule.CreateSchedule(document, new ElementId(BuiltInCategory.OST_DetailComponents));
                document.Regenerate();
                ElementId image = new ElementId(BuiltInParameter.ALL_MODEL_IMAGE);
                ScheduleDefinition definition = ViewSchedule.Definition;
                SchedulableField schedulableFieldImage = definition.GetSchedulableFields().FirstOrDefault(sf => sf.ParameterId == image);

                if (schedulableFieldImage != null)
                {
                    // Add the found field
                    definition.AddField(schedulableFieldImage);
                }
                Parameter elementHostParameter = new FilteredElementCollector(document).OfCategory(BuiltInCategory.OST_DetailComponents)
                    .WhereElementIsElementType().Cast<Parameter>().Where(x => x.Definition.Name.Equals("Element Host")).FirstOrDefault();
                if (elementHostParameter == null)
                {
                    MessageBox.Show("Test");
                }
                SchedulableField schedulableFieldElementHost = definition.GetSchedulableFields().FirstOrDefault(sf => sf.ParameterId == elementHostParameter.Id);
                if (schedulableFieldElementHost != null)
                {
                    // Add the found field
                    definition.AddField(schedulableFieldElementHost);
                }
            }
        }
        //public void AddRegularFieldToSchedule(Document document)
        //{
        //    ElementId image = new ElementId(BuiltInParameter.ALL_MODEL_IMAGE);
        //    ScheduleDefinition definition = ViewSchedule.Definition;
        //    SchedulableField schedulableFieldImage = definition.GetSchedulableFields().FirstOrDefault(sf => sf.ParameterId == image);
            
        //    if (schedulableFieldImage != null)
        //    {
        //        // Add the found field
        //        definition.AddField(schedulableFieldImage);
        //    }
            
        //}
    }
}
