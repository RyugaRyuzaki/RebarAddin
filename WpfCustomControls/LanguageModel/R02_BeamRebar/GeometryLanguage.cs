using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfCustomControls.LanguageModel.R02_BeamRebar
{
    public class GeometryLanguage : BaseViewModel
    {

        private string _Identification;
        public string Identification { get { return _Identification; } set { _Identification = value; OnPropertyChanged(); } }
        private string _FamilyName;
        public string FamilyName { get { return _FamilyName; } set { _FamilyName = value; OnPropertyChanged(); } }
        private string _TypeName;
        public string TypeName { get { return _TypeName; } set { _TypeName = value; OnPropertyChanged(); } }
        private string _BeamParameter;
        public string BeamParameter { get { return _BeamParameter; } set { _BeamParameter = value; OnPropertyChanged(); } }

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
        private string _ParameterBeams;
        public string ParameterBeams { get { return _ParameterBeams; } set { _ParameterBeams = value; OnPropertyChanged(); } }
        private string _BeamsName;
        public string BeamsName { get { return _BeamsName; } set { _BeamsName = value; OnPropertyChanged(); } }
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
        public GeometryLanguage(string language)
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
            Identification = "Identification";
            FamilyName = "Family Name";
            TypeName = "Type Name";
            BeamParameter = "Beams Parameter";
            RebarShapeHook = "Rebar Shape and Hook";
            StirrupShape = "StirrupShape";
            AntiShape = "AntiShape";
            Hook = "Hook";
            ViewProperty = "View Property";
            DetailTemplate = "Detail Template";
            SectionTemplate = "Section Template";
            DetailShopTemplate = "DetailShop Template";
            ParameterBeams = "Parameter Beams";
            BeamsName = "Beams Name";
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

        }
        private void GetLanguageVN()
        {
            Identification = "Nhận dạng Cột";
            FamilyName = "Tên Family";
            TypeName = "Tên Type";
            BeamParameter = "Thông số Dầm";
            RebarShapeHook = "Dạng Thép Và Hook";
            StirrupShape = "Dạng Thép Đai";
            AntiShape = "Anti Đai";
            Hook = "Móc Hook";
            ViewProperty = "Thông Số View";
            DetailTemplate = "Chi tiết Template";
            SectionTemplate = "Mặt cắt Template";
            DetailShopTemplate = "Shop Template";
            ParameterBeams = "Parameter Dầm";
            BeamsName = "Tên Dầm";
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

        }
    }
}
