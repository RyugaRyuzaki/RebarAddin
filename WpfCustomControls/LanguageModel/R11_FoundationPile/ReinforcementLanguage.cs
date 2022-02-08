using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfCustomControls.LanguageModel.R11_FoundationPile
{
    public class ReinforcementLanguage : BaseViewModel
    {

        private string _ListFoundation;
        public string ListFoundation { get { return _ListFoundation; } set { _ListFoundation = value; OnPropertyChanged(); } }
        private string _GroupFoundation;
        public string GroupFoundation { get { return _GroupFoundation; } set { _GroupFoundation = value; OnPropertyChanged(); } }
        private string _Representative;
        public string Representative { get { return _Representative; } set { _Representative = value; OnPropertyChanged(); } }
        private string _SpanOrientation;
        public string SpanOrientation { get { return _SpanOrientation; } set { _SpanOrientation = value; OnPropertyChanged(); } }
        private string _MainSpan;
        public string MainSpan { get { return _MainSpan; } set { _MainSpan = value; OnPropertyChanged(); } }
        private string _SecondarySpan;
        public string SecondarySpan { get { return _SecondarySpan; } set { _SecondarySpan = value; OnPropertyChanged(); } }

        private string _Bar;
        public string Bar { get { return _Bar; } set { _Bar = value; OnPropertyChanged(); } }
        private string _HookLength;
        public string HookLength { get { return _HookLength; } set { _HookLength = value; OnPropertyChanged(); } }

        private string _Distance;
        public string Distance { get { return _Distance; } set { _Distance = value; OnPropertyChanged(); } }

        private string _FixNumber;
        public string FixNumber { get { return _FixNumber; } set { _FixNumber = value; OnPropertyChanged(); } }
        private string _Number;
        public string Number { get { return _Number; } set { _Number = value; OnPropertyChanged(); } }


        private string _AddTopBar;
        public string AddTopBar { get { return _AddTopBar; } set { _AddTopBar = value; OnPropertyChanged(); } }

        private string _AddHorizontalBar;
        public string AddHorizontalBar { get { return _AddHorizontalBar; } set { _AddHorizontalBar = value; OnPropertyChanged(); } }
        private string _AddVerticalBar;
        public string AddVerticalBar { get { return _AddVerticalBar; } set { _AddVerticalBar = value; OnPropertyChanged(); } }

      
        public ReinforcementLanguage(string language)
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
            ListFoundation = "List Foundation";
            GroupFoundation = "Group Foundation";
            Representative = "Representative";
            SpanOrientation = "Span Orientation";
            MainSpan = "Main Span";
            SecondarySpan = "Secondary Span";
            Bar = "Bar";
            HookLength = "Hook Length";

            Distance = "Distance";
            FixNumber = "Fix Number";
            Number = "Number";

            AddTopBar = "Add Top Bar";

            AddHorizontalBar = "Add Horizontal Bar";

            AddVerticalBar = "Add Vertical Bar";

           
        }
        private void GetLanguageVN()
        {
            ListFoundation = "Danh Sách Móng";
            GroupFoundation = "Nhóm Móng";
            Representative = "Đại diện";
            SpanOrientation = "Phương Nhịp";
            MainSpan = "Phương Chính";
            SecondarySpan = "Phương phụ";
            Bar = "Thép";
            HookLength = "Dài móc";

            Distance = "KC";

            FixNumber = "Chỉnh số";
            Number = "Số";

            AddTopBar = "Thêm Lớp trên";

            AddHorizontalBar = "Thêm Thép ngang";

            AddVerticalBar = "Thêm thép dọc";

            
        }
    }
}
