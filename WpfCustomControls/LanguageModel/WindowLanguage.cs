using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfCustomControls.LanguageModel
{
    public class WindowLanguage : BaseViewModel
    {

        private string _OK;
        public string OK { get { return _OK; } set { _OK = value; OnPropertyChanged(); } }

        private string _Cancel;
        public string Cancel { get { return _Cancel; } set { _Cancel = value; OnPropertyChanged(); } }

        #region R01
        private string _Column;
        public string Column { get { return _Column; } set { _Column = value; OnPropertyChanged(); } }
        #endregion

        #region R11
        private string _R11_CreateFoundationPile;
        public string R11_CreateFoundationPile { get { return _R11_CreateFoundationPile; } set { _R11_CreateFoundationPile = value; OnPropertyChanged(); } }
        private string _R11_CreatePileDetail;
        public string R11_CreatePileDetail { get { return _R11_CreatePileDetail; } set { _R11_CreatePileDetail = value; OnPropertyChanged(); } }
        private string _R11_CreateReinforcement;
        public string R11_CreateReinforcement { get { return _R11_CreateReinforcement; } set { _R11_CreateReinforcement = value; OnPropertyChanged(); } }
        #endregion

        public WindowLanguage(string languge)
        {
            ChangedLanguage(languge);
        }
        public void ChangedLanguage(string languge)
        {
            switch (languge)
            {
                case "EN": GetLanguageEN(); break;
                case "VN": GetLanguageVN(); break;
                default: GetLanguageEN(); break;
            }
        }
        private void GetLanguageEN()
        {
            OK = "OK";
            Cancel = "Cancel";
            Column = "Columns";
            R11_CreateFoundationPile = "Create FoundationPile";
            R11_CreatePileDetail = "Create PileDetail";
            R11_CreateReinforcement = "Create Reinforcement";
            
        }
        private void GetLanguageVN()
        {
            OK = "Thực Hiện";
            Cancel = "Huỷ";
            Column = "Cột";
            R11_CreateFoundationPile = "Tạo Móng Cọc";
            R11_CreatePileDetail = "Tạo Chi tiết Cọc";
            R11_CreateReinforcement = "Tạo Cốt Thép";
        }
    }
}
