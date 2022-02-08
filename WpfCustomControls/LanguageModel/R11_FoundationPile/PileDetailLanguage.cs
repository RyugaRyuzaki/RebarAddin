using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfCustomControls.LanguageModel.R11_FoundationPile
{
    public class PileDetailLanguage : BaseViewModel
    {

        private string _RuleDetail;
        public string RuleDetail { get { return _RuleDetail; } set { _RuleDetail = value; OnPropertyChanged(); } }
        private string _FoundationRule;
        public string FoundationRule { get { return _FoundationRule; } set { _FoundationRule = value; OnPropertyChanged(); } }
        private string _PileRule;
        public string PileRule { get { return _PileRule; } set { _PileRule = value; OnPropertyChanged(); } }
        private string _ApplyRule;
        public string ApplyRule { get { return _ApplyRule; } set { _ApplyRule = value; OnPropertyChanged(); } }
        private string _ModifyRule;
        public string ModifyRule { get { return _ModifyRule; } set { _ModifyRule = value; OnPropertyChanged(); } }

        private string _AllFoundation;
        public string AllFoundation { get { return _AllFoundation; } set { _AllFoundation = value; OnPropertyChanged(); } }

        private string _TestingPile;
        public string TestingPile { get { return _TestingPile; } set { _TestingPile = value; OnPropertyChanged(); } }
        private string _ApplyPile;
        public string ApplyPile { get { return _ApplyPile; } set { _ApplyPile = value; OnPropertyChanged(); } }

        private string _Modify;
        public string Modify { get { return _Modify; } set { _Modify = value; OnPropertyChanged(); } }
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
        public PileDetailLanguage(string language)
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
            RuleDetail = "Rule Detail";
            FoundationRule = "Foundation Rule";
            PileRule = "Pile Rule On Foundation";
            ApplyRule = "Apply Rule";
            ModifyRule = "Modify Rule";
            AllFoundation = "All Foundation";
            TestingPile = "Testing Pile";
            ApplyPile = "Apply";
        }
        private void GetLanguageVN()
        {
            RuleDetail = "Chi tiết quy tắc";
            FoundationRule = "Quy tắc Móng";
            PileRule = "Quy tắc Cọc trong từng Móng";
            ApplyRule = "Áp dụng Quy tắc";
            ModifyRule = "Sửa Quy tắc";
            AllFoundation = "Các Móng";      
            TestingPile = "Cọc thử";

            ApplyPile = "Áp dụng";
        }
    }
}
