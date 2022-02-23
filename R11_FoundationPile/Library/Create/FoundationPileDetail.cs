using Autodesk.Revit.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfCustomControls;
using DSP;
namespace R11_FoundationPile
{
    public class FoundationPileDetail  :BaseViewModel
    {
        public ViewFamilyType FoundationViewType { get; set; }
        public BoundingBoxXYZ FoundationBox { get; set; }
        public ViewPlan FoundationView { get; set; }
        public ViewPlan PildeDetailView { get; set; }
        public ViewPlan PileSpotCoordinateView { get; set; }
      
        public string Type = "@FoundationPlan";
        public const string NamePile = "Piles Plan";
        public const string NameSpotCoordinate = "Piles Spot Coordinate";
        public ElementId Schedule { get; set; }
        public ViewSchedule ViewSchedule { get; set; }
        public FoundationPileDetail(Document document)
        {
        }

        private void GetFoundationViewType(Document document)
        {

            List<ViewFamilyType> list = new FilteredElementCollector(document)
                                 .OfClass(typeof(ViewFamilyType))
                                 .Cast<ViewFamilyType>()
                                 .Where(x => ViewFamily.StructuralPlan == x.ViewFamily && x.Name.Equals(Type)).ToList();
            if (list.Count == 0)
            {
                ViewFamilyType a = new FilteredElementCollector(document)
                                .OfClass(typeof(ViewFamilyType))
                                .Cast<ViewFamilyType>()
                                .Where(x => ViewFamily.StructuralPlan == x.ViewFamily).FirstOrDefault();
                FoundationViewType = a.Duplicate(Type) as ViewFamilyType;
            }
            else
            {
                FoundationViewType = list.Where(x => x.Name.Equals(Type)).FirstOrDefault();
            }
        }
        private void GetBoundBoxXYZ(XYZ max,XYZ min)
        {
            FoundationBox = new BoundingBoxXYZ();
            FoundationBox.Min = min;
            FoundationBox.Max = max;
        }
        public void CeateFoundationPlan(Document document, UnitProject unit,SettingModel settingModel,ColumnModel columnModel, XYZ max, XYZ min)
        {
            GetFoundationViewType(document);
            FoundationView = ViewPlan.Create(document, FoundationViewType.Id, columnModel.BottomLevel.Id);
            
            try
            {
                FoundationView.Name = settingModel.FoundationPlaneName;
            }
            catch (System.Exception)
            {
                FoundationView.Name = settingModel.FoundationPlaneName + "Copy";
            }
            FoundationView.get_Parameter(BuiltInParameter.VIEWER_CROP_REGION_VISIBLE).Set(0);
            FoundationView.ViewTemplateId = settingModel.SelectedFoundationPlanTemplate.Id;
            GetBoundBoxXYZ(max, min);
            FoundationView.CropBox = FoundationBox;
        }
        public void CeatePileDetailPlan(Document document, UnitProject unit, SettingModel settingModel)
        {
            if (FoundationView != null)
            {
               ElementId elementId= FoundationView.Duplicate(ViewDuplicateOption.Duplicate);
                PildeDetailView = document.GetElement(elementId) as ViewPlan;
                PildeDetailView.Name = NamePile;
                PildeDetailView.get_Parameter(BuiltInParameter.VIEWER_CROP_REGION_VISIBLE).Set(0);
                PildeDetailView.ViewTemplateId = settingModel.SelectedPilePlanTemplate.Id;
                PlanViewRange planViewRange = PildeDetailView.GetViewRange();
                planViewRange.SetOffset(PlanViewPlane.CutPlane, -unit.Convert(1.2 * settingModel.HeightFoundation));
                planViewRange.SetOffset(PlanViewPlane.BottomClipPlane, -unit.Convert(1.5 * settingModel.HeightFoundation));
                planViewRange.SetOffset(PlanViewPlane.ViewDepthPlane, -unit.Convert(2 * settingModel.HeightFoundation));
                PildeDetailView.SetViewRange(planViewRange);
            }
        }
        public void CreatePileSpotCoodinateView(Document document, UnitProject unit, SettingModel settingModel)
        {
            ElementId elementId = PildeDetailView.Duplicate(ViewDuplicateOption.Duplicate);
            PileSpotCoordinateView = document.GetElement(elementId) as ViewPlan;
            PileSpotCoordinateView.Name = NameSpotCoordinate;
        }
        #region schedule
        public void CreateSchedule(Document document, SettingModel settingModel)
        {
           BuiltInCategory pile= (settingModel.SelectedCategoyryPile.Equals("Structural Columns")) ? (BuiltInCategory.OST_StructuralColumns) : (BuiltInCategory.OST_StructuralFoundation) ;
                Schedule = new FilteredElementCollector(document).OfCategory(pile).FirstElementId();
            if (Schedule != null && Schedule != ElementId.InvalidElementId)
            {

                ViewSchedule = ViewSchedule.CreateSchedule(document, new ElementId(BuiltInCategory.OST_DetailComponents));
                document.Regenerate();
                ViewSchedule.Name = "SpotCoordinate Pile";
                ScheduleDefinition definition = ViewSchedule.Definition;
                SchedulableField schedulableFieldImage = definition.GetSchedulableFields().FirstOrDefault(sf => sf.GetName(document).Equals("Comments"));
                if (schedulableFieldImage != null)
                {
                    // Add the found field
                    definition.AddField(schedulableFieldImage);
                }
                SchedulableField schedulableFieldElementHost = definition.GetSchedulableFields().FirstOrDefault(sf => sf.GetName(document).Equals("XVector"));
                if (schedulableFieldElementHost != null)
                {
                    // Add the found field
                    definition.AddField(schedulableFieldElementHost);
                }
                SchedulableField schedulableFieldLength = definition.GetSchedulableFields().FirstOrDefault(sf => sf.GetName(document).Equals("YVector"));
                if (schedulableFieldLength != null)
                {
                    // Add the found field
                    definition.AddField(schedulableFieldLength);
                }
            }
        }
        public ScheduleField FindField(ScheduleDefinition definition, SchedulableField sort)
        {
            ScheduleField foundField = null;
            foreach (ScheduleFieldId fieldId in definition.GetFieldOrder())
            {
                foundField = definition.GetField(fieldId);
                if (foundField.ParameterId == sort.ParameterId)
                {
                    return foundField;
                }
            }

            return null;
        }
        #endregion
    }
}
