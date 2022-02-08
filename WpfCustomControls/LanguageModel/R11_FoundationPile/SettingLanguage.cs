using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfCustomControls.LanguageModel.R11_FoundationPile
{
    public class SettingLanguage : BaseViewModel
    {

        private string _PileSetting;
        public string PileSetting { get { return _PileSetting; } set { _PileSetting = value; OnPropertyChanged(); } }
        private string _PileCategory;
        public string PileCategory { get { return _PileCategory; } set { _PileCategory = value; OnPropertyChanged(); } }
        private string _PileFamily;
        public string PileFamily { get { return _PileFamily; } set { _PileFamily = value; OnPropertyChanged(); } }
        private string _PileFamilyType;
        public string PileFamilyType { get { return _PileFamilyType; } set { _PileFamilyType = value; OnPropertyChanged(); } }
        private string _PileLength;
        public string PileLength { get { return _PileLength; } set { _PileLength = value; OnPropertyChanged(); } }
        private string _PilePlanTemplate;
        public string PilePlanTemplate { get { return _PilePlanTemplate; } set { _PilePlanTemplate = value; OnPropertyChanged(); } }
        private string _PilePrefix;
        public string PilePrefix { get { return _PilePrefix; } set { _PilePrefix = value; OnPropertyChanged(); } }
        private string _FoundationSetting;
        public string FoundationSetting { get { return _FoundationSetting; } set { _FoundationSetting = value; OnPropertyChanged(); } }
        private string _FoundationCategory;
        public string FoundationCategory { get { return _FoundationCategory; } set { _FoundationCategory = value; OnPropertyChanged(); } }
        private string _FoundationType;
        public string FoundationType { get { return _FoundationType; } set { _FoundationType = value; OnPropertyChanged(); } }
        private string _FoundationHeight;
        public string FoundationHeight { get { return _FoundationHeight; } set { _FoundationHeight = value; OnPropertyChanged(); } }
        private string _CreateFormwork;
        public string CreateFormwork { get { return _CreateFormwork; } set { _CreateFormwork = value; OnPropertyChanged(); } }
        private string _FoundationPlanTemplate;
        public string FoundationPlanTemplate { get { return _FoundationPlanTemplate; } set { _FoundationPlanTemplate = value; OnPropertyChanged(); } }
        private string _FoundationSectionTemplate;
        public string FoundationSectionTemplate { get { return _FoundationSectionTemplate; } set { _FoundationSectionTemplate = value; OnPropertyChanged(); } }
        private string _FoundationDetailTemplate;
        public string FoundationDetailTemplate { get { return _FoundationDetailTemplate; } set { _FoundationDetailTemplate = value; OnPropertyChanged(); } }
        private string _FoundationPlanName;
        public string FoundationPlanName { get { return _FoundationPlanName; } set { _FoundationPlanName = value; OnPropertyChanged(); } }
        private string _FoundationPlanNamePrefix;
        public string FoundationPlanNamePrefix { get { return _FoundationPlanNamePrefix; } set { _FoundationPlanNamePrefix = value; OnPropertyChanged(); } }
        private string _SetParameter;
        public string SetParameter { get { return _SetParameter; } set { _SetParameter = value; OnPropertyChanged(); } }
        private string _DimensionType;
        public string DimensionType { get { return _DimensionType; } set { _DimensionType = value; OnPropertyChanged(); } }
        private string _TextType;
        public string TextType { get { return _TextType; } set { _TextType = value; OnPropertyChanged(); } }
        private string _UseTag;
        public string UseTag { get { return _UseTag; } set { _UseTag = value; OnPropertyChanged(); } }
        private string _FoundationTag;
        public string FoundationTag { get { return _FoundationTag; } set { _FoundationTag = value; OnPropertyChanged(); } }
        private string _PileTag;
        public string PileTag { get { return _PileTag; } set { _PileTag = value; OnPropertyChanged(); } }
        private string _OffsetDim;
        public string OffsetDim { get { return _OffsetDim; } set { _OffsetDim = value; OnPropertyChanged(); } }
        private string _CoverProperty;
        public string CoverProperty { get { return _CoverProperty; } set { _CoverProperty = value; OnPropertyChanged(); } }
        private string _CoverTop;
        public string CoverTop { get { return _CoverTop; } set { _CoverTop = value; OnPropertyChanged(); } }
        private string _CoverSide;
        public string CoverSide { get { return _CoverSide; } set { _CoverSide = value; OnPropertyChanged(); } }
        private string _CoverBottom;
        public string CoverBottom { get { return _CoverBottom; } set { _CoverBottom = value; OnPropertyChanged(); } }
        public SettingLanguage(string language)
        {
            ChangedLanguage(language);
        }
        public void ChangedLanguage(string language)
        {
            switch (language)
            {
                case "EN": GetLanguageEN(); break;
                case "VN": GetLanguageVN(); break;
                default: GetLanguageEN(); break;
            }
        }
        private void GetLanguageEN()
        {
            PileSetting = "Pile Setting";
            PileCategory = "Pile Category";
            PileFamily = "Pile Family";
            PileFamilyType = "Pile FamilyType";
            PileLength = "Length";
            PilePlanTemplate = "Pile Plan Template";
            PilePrefix = "Pile Prefix";
            FoundationSetting = "Foundation Setting";
            FoundationCategory = "Foundation Category";
            FoundationType = "Foundation Type";
            FoundationHeight = "Foundation Height";
            CreateFormwork = "Create Formwork";
            FoundationPlanTemplate = "Foundation Plan TP";
            FoundationSectionTemplate = "Foundation Section TP";
            FoundationDetailTemplate = "Foundation Detail TP";
            FoundationPlanName = "Foundation Plan Name";
            FoundationPlanNamePrefix = "Foundation Prefix";
            SetParameter = "Set Parameter";
            DimensionType = "Dimension Type";
            TextType = "Text Type";
            UseTag = "Use Tag";
            FoundationTag = "Foundation Tag";
            PileTag = "Pile Tag";
            OffsetDim = "Offset Dim";
            CoverProperty = "Cover Property";
            CoverTop = "Cover Top";
            CoverSide = "Cover Side";
            CoverBottom = "Cover Bottom";
        }
        private void GetLanguageVN()
        {
            PileSetting = "Cài Đặt Cọc";
            PileCategory = "Category Cọc";
            PileFamily = "Family Cọc";
            PileFamilyType = "FamilyType Cọc";
            PileLength = "Dài";
            PilePlanTemplate = "MB Cọc Template";
            PilePrefix = "Cọc tiền tố";
            FoundationSetting = "Cài đặt Móng";
            FoundationCategory = "Category Móng";
            FoundationType = "Type Móng";
            FoundationHeight = "Chiều cao Móng";
            CreateFormwork = "Tạo Bê tông lót";
            FoundationPlanTemplate = "MB Móng TP";
            FoundationSectionTemplate = "MC Móng TP";
            FoundationDetailTemplate = "CT Móng TP";
            FoundationPlanName = "Tên MB Móng";
            FoundationPlanNamePrefix = "Tiền tố Móng";
            SetParameter = "Đặt Parameter";
            DimensionType = "Type Kích thước";
            TextType = "Type Chữ";
            UseTag = "Dùng Tag";
            FoundationTag = "Tag Móng";
            PileTag = "Tag Cọc";
            OffsetDim = "Offset Dim";
            CoverProperty = "Lớp BT Bảo vệ";
            CoverTop = "Lớp trên";
            CoverSide = "Lớp bên";
            CoverBottom = "Lớp dưới";
        }
    }
}
