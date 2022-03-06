using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfCustomControls;
namespace R01_ColumnsRebar.LanguageModel
{
    public class SettingLanguage : BaseViewModel
    {

        private string _RebarShapeHook;
        public string RebarShapeHook { get { return _RebarShapeHook; } set { _RebarShapeHook = value; OnPropertyChanged(); } }
        private string _StirrupShape;
        public string StirrupShape { get { return _StirrupShape; } set { _StirrupShape = value; OnPropertyChanged(); } }
        private string _AntiShape;
        public string AntiShape { get { return _AntiShape; } set { _AntiShape = value; OnPropertyChanged(); } }
        private string _Hook;
        public string Hook { get { return _Hook; } set { _Hook = value; OnPropertyChanged(); } }

        private string _ViewProperty;
        public string ViewProperty { get { return _ViewProperty; } set { _ViewProperty = value; OnPropertyChanged(); } }
        private string _DetailTemplate;
        public string DetailTemplate { get { return _DetailTemplate; } set { _DetailTemplate = value; OnPropertyChanged(); } }

        private string _SectionTemplate;
        public string SectionTemplate { get { return _SectionTemplate; } set { _SectionTemplate = value; OnPropertyChanged(); } }
        private string _DetailShopTemplate;
        public string DetailShopTemplate { get { return _DetailShopTemplate; } set { _DetailShopTemplate = value; OnPropertyChanged(); } }
        private string _ParameterColumns;
        public string ParameterColumns { get { return _ParameterColumns; } set { _ParameterColumns = value; OnPropertyChanged(); } }
        private string _ColumnsName;
        public string ColumnsName { get { return _ColumnsName; } set { _ColumnsName = value; OnPropertyChanged(); } }
        private string _DetailViewName;
        public string DetailViewName { get { return _DetailViewName; } set { _DetailViewName = value; OnPropertyChanged(); } }
        private string _PrefixLevel;
        public string PrefixLevel { get { return _PrefixLevel; } set { _PrefixLevel = value; OnPropertyChanged(); } }
        private string _PrefixSection;
        public string PrefixSection { get { return _PrefixSection; } set { _PrefixSection = value; OnPropertyChanged(); } }
        private string _SectionViewName;
        public string SectionViewName { get { return _SectionViewName; } set { _SectionViewName = value; OnPropertyChanged(); } }
        private string _AnotationProperty;
        public string AnotationProperty { get { return _AnotationProperty; } set { _AnotationProperty = value; OnPropertyChanged(); } }
        private string _DimentionType;
        public string DimentionType { get { return _DimentionType; } set { _DimentionType = value; OnPropertyChanged(); } }
        private string _DimDiameterType;
        public string DimDiameterType { get { return _DimDiameterType; } set { _DimDiameterType = value; OnPropertyChanged(); } }
        private string _MainRebarTags;
        public string MainRebarTags { get { return _MainRebarTags; } set { _MainRebarTags = value; OnPropertyChanged(); } }
        private string _MultiRebarTags;
        public string MultiRebarTags { get { return _MultiRebarTags; } set { _MultiRebarTags = value; OnPropertyChanged(); } }
        private string _StirrupsRebarTags;
        public string StirrupsRebarTags { get { return _StirrupsRebarTags; } set { _StirrupsRebarTags = value; OnPropertyChanged(); } }
        private string _DetailItemTags;
        public string DetailItemTags { get { return _DetailItemTags; } set { _DetailItemTags = value; OnPropertyChanged(); } }
        private string _DetailDistanceTags;
        public string DetailDistanceTags { get { return _DetailDistanceTags; } set { _DetailDistanceTags = value; OnPropertyChanged(); } }
        private string _TextNote;
        public string TextNote { get { return _TextNote; } set { _TextNote = value; OnPropertyChanged(); } }
        private string _ReinforcementStructural;
        public string ReinforcementStructural { get { return _ReinforcementStructural; } set { _ReinforcementStructural = value; OnPropertyChanged(); } }
        private string _SectionSetting;
        public string SectionSetting { get { return _SectionSetting; } set { _SectionSetting = value; OnPropertyChanged(); } }
        private string _DetailSetting;
        public string DetailSetting { get { return _DetailSetting; } set { _DetailSetting = value; OnPropertyChanged(); } }
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
            RebarShapeHook = "Rebar Shape and Hook";
            StirrupShape = "StirrupShape";
            AntiShape = "AntiShape";
            Hook = "Hook";
            ViewProperty = "View Property";
            DetailTemplate = "Detail Template";
            SectionTemplate = "Section Template";
            DetailShopTemplate = "DetailShop Template";
            ParameterColumns = "Parameter Columns";
            ColumnsName = "Columns Name";
            DetailViewName = "DetailView Name";
            PrefixLevel = "Prefix Level";
            PrefixSection = "Prefix Section";
            SectionViewName = "SectionView Name";
            AnotationProperty = "Anotation Property";
            DimentionType = "Dimention Type";
            DimDiameterType = "Dim-Diameter Type";
            MainRebarTags = "Main Rebar Tags";
            MultiRebarTags = "Multi-Rebar Tags";
            StirrupsRebarTags = "Stirrups Rebar Tags";
            DetailItemTags = "DetailItem Tags";
            DetailDistanceTags = "Detail Distance Tags";
            TextNote = "TextNote";
            ReinforcementStructural = "Reinforcement Structural";
            SectionSetting = "Section Setting";
            DetailSetting = "Detail Setting";
        }
        private void GetLanguageVN()
        {
            RebarShapeHook = "Dạng Thép Và Hook";
            StirrupShape = "Dạng Thép Đai";
            AntiShape = "Anti Đai";
            Hook = "Móc Hook";
            ViewProperty = "Thông Số View";
            DetailTemplate = "Chi tiết Template";
            SectionTemplate = "Mặt cắt Template";
            DetailShopTemplate = "Shop Template";
            ParameterColumns = "Parameter Cột";
            ColumnsName = "Tên Cột";
            DetailViewName = "Tên Chi tiết";
            PrefixLevel = "Tiền tố Level";
            PrefixSection = "Tiền tố Section";
            SectionViewName = "Tên Mặt cắt";
            AnotationProperty = "Thông số Kích thước";
            DimentionType = "Type Dim";
            DimDiameterType = "Type Dim D";
            MainRebarTags = "Tag Thép chủ";
            MultiRebarTags = "Tag Thép nhóm";
            StirrupsRebarTags = "Tag Thép Đai";
            DetailItemTags = "Tag Detail-Item";
            DetailDistanceTags = "Tag Detail KC";
            TextNote = "Text Ghi chú";
            ReinforcementStructural = "Ghi chú Thép";
            SectionSetting = "Ghi chú Mặt cắt";
            DetailSetting = "Ghi chú chi tiết";
        }
    }
}
