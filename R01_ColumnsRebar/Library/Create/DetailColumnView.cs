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
    public class DetailColumnView
    {
        public ViewFamilyType Detail { get; set; }
        public BoundingBoxXYZ SectionBoxX { get; set; }
        public BoundingBoxXYZ SectionBoxY { get; set; }
        public ViewSection DetailViewX { get; set; }
        public ViewSection DetailViewY { get; set; }
        public string Type = "@ColumnDetail";
        public ElementId Schedule { get; set; }
        public ViewSchedule ViewSchedule { get; set; }
        public DetailColumnView()
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
        public void GetSectionBox(SectionStyle sectionStyle, Document document, UnitProject unit, ObservableCollection<InfoModel> InfoModels, ObservableCollection<PlanarFace> planarFaces, List<Element> columns, double offset0)
        {
            double offset = unit.Convert(offset0);
            double h = unit.Convert(InfoModels[InfoModels.Count - 1].TopPosition);
            if (sectionStyle==SectionStyle.RECTANGLE)
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
                XYZ minX = new XYZ(-0.5 * xX - offset, -0.5 * h - offset, 0);
                XYZ maxX = new XYZ(0.5 * xX + offset, 0.5 * h + offset, offset);
                Transform tX = Transform.Identity;
                tX.Origin = mid;
                tX.BasisX = dirX;
                tX.BasisY = upX;
                tX.BasisZ = viewDirX;
                SectionBoxX = new BoundingBoxXYZ();
                SectionBoxX.Transform = tX;
                SectionBoxX.Min = minX;
                SectionBoxX.Max = maxX;
                double xY = unit.Convert(Math.Abs(InfoModels[0].SouthPosition - InfoModels[0].NouthPosition)) * 0.5;
                XYZ dirY = InfoModels[0].Nouth.FaceNormal;
                XYZ upY = XYZ.BasisZ;
                XYZ viewDirY = dirY.CrossProduct(upY);
                XYZ minY = new XYZ(-0.5 * xY - offset, -0.5 * h - offset, 0);
                XYZ maxY = new XYZ(0.5 * xY + offset, 0.5 * h + offset, offset);
                Transform tY = Transform.Identity;
                tY.Origin = mid;
                tY.BasisX = dirY;
                tY.BasisY = upY;
                tY.BasisZ = viewDirY;
                SectionBoxY = new BoundingBoxXYZ();
                SectionBoxY.Transform = tY;
                SectionBoxY.Min = minY;
                SectionBoxY.Max = maxY;
            }
            else
            {
                XYZ p1 = (columns[columns.Count-1].Location as LocationPoint).Point as XYZ;
                XYZ p2 = PointModel.ProjectToPlane(p1, planarFaces[0]);
                XYZ mid = p2 + h * 0.5 * XYZ.BasisZ;
                double xX = unit.Convert(InfoModels[0].D) * 0.5;
                XYZ dirX = XYZ.BasisX;
                XYZ upX = XYZ.BasisZ;
                XYZ viewDirX = dirX.CrossProduct(upX);
                XYZ minX = new XYZ(-0.5 * xX - offset, -0.5 * h - offset, 0);
                XYZ maxX = new XYZ(0.5 * xX + offset, 0.5 * h + offset, offset);
                Transform tX = Transform.Identity;
                tX.Origin = mid;
                tX.BasisX = dirX;
                tX.BasisY = upX;
                tX.BasisZ = viewDirX;
                SectionBoxX = new BoundingBoxXYZ();
                SectionBoxX.Transform = tX;
                SectionBoxX.Min = minX;
                SectionBoxX.Max = maxX;
                double xY = unit.Convert(InfoModels[0].D) * 0.5;
                XYZ dirY = XYZ.BasisY;
                XYZ upY = XYZ.BasisZ;
                XYZ viewDirY = dirY.CrossProduct(upY);
                XYZ minY = new XYZ(-0.5 * xY - offset, -0.5 * h - offset, 0);
                XYZ maxY = new XYZ(0.5 * xY + offset, 0.5 * h + offset, offset);
                Transform tY = Transform.Identity;
                tY.Origin = mid;
                tY.BasisX = dirY;
                tY.BasisY = upY;
                tY.BasisZ = viewDirY;
                SectionBoxY = new BoundingBoxXYZ();
                SectionBoxY.Transform = tY;
                SectionBoxY.Min = minY;
                SectionBoxY.Max = maxY;
            }

           
        }
        public void CeateDetailView(SectionStyle sectionStyle, Document document, UnitProject unit, ObservableCollection<InfoModel> InfoModels, ObservableCollection<PlanarFace> planarFaces, List<Element> columns, SettingModel settingModel, string nameX, string nameY, double offset0)
        {
            GetNameDetail(document);
            GetSectionBox(sectionStyle,document, unit, InfoModels, planarFaces, columns, offset0);
            DetailViewX = ViewSection.CreateSection(document, Detail.Id, SectionBoxX);
            try
            {
                DetailViewX.Name = nameX;
            }
            catch (System.Exception)
            {
                MessageBox.Show("There is an existing Section View" + "\n" + "Please choose another Beam Name", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                DetailViewX.Name = nameX+"A";
            }
            DetailViewX.get_Parameter(BuiltInParameter.VIEWER_CROP_REGION_VISIBLE).Set(0);
            DetailViewX.ViewTemplateId = settingModel.SelectedDetailTemplate.Id;
            DetailViewY = ViewSection.CreateSection(document, Detail.Id, SectionBoxY);
            try
            {
                DetailViewY.Name = nameY;
            }
            catch (System.Exception)
            {
                MessageBox.Show("There is an existing Section View" + "\n" + "Please choose another Beam Name", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                DetailViewY.Name = nameY+"A";
            }
            DetailViewY.get_Parameter(BuiltInParameter.VIEWER_CROP_REGION_VISIBLE).Set(0);
            DetailViewY.ViewTemplateId = settingModel.SelectedDetailTemplate.Id;
        }
        public void CreateSchedule(Document document, SettingModel settingModel)
        {
            Schedule = new FilteredElementCollector(document).OfCategory(BuiltInCategory.OST_DetailComponents).FirstElementId();
            if (Schedule != null && Schedule != ElementId.InvalidElementId)
            {

                ViewSchedule = ViewSchedule.CreateSchedule(document, new ElementId(BuiltInCategory.OST_DetailComponents));
                document.Regenerate();
                ViewSchedule.Name = "Detail";
                ElementId image = new ElementId(BuiltInParameter.ALL_MODEL_IMAGE);
                ScheduleDefinition definition = ViewSchedule.Definition;
                SchedulableField schedulableFieldImage = definition.GetSchedulableFields().FirstOrDefault(sf => sf.GetName(document).Equals("Image"));
                if (schedulableFieldImage != null)
                {
                    // Add the found field
                    definition.AddField(schedulableFieldImage);
                }
                SchedulableField schedulableFieldElementHost = definition.GetSchedulableFields().FirstOrDefault(sf => sf.GetName(document).Equals("Element Host"));
                if (schedulableFieldElementHost != null)
                {
                    // Add the found field
                    definition.AddField(schedulableFieldElementHost);
                }
                SchedulableField schedulableFieldLength = definition.GetSchedulableFields().FirstOrDefault(sf => sf.GetName(document).Equals("Length"));
                if (schedulableFieldLength != null)
                {
                    // Add the found field
                    definition.AddField(schedulableFieldLength);
                }
                ScheduleFilter scheduleFilter = new ScheduleFilter(FindField(definition, schedulableFieldElementHost).FieldId, ScheduleFilterType.BeginsWith, settingModel.ColumnsName);
                definition.AddFilter(scheduleFilter);
            }
        }
        public  ScheduleField FindField(ScheduleDefinition definition, SchedulableField sort)
        {
            ScheduleField foundField = null;
            foreach (ScheduleFieldId fieldId in definition.GetFieldOrder())
            {
                foundField = definition.GetField(fieldId);
                if (foundField.ParameterId==sort.ParameterId)
                {
                    return foundField;
                }
            }

            return null;
        }
    }
   
}
