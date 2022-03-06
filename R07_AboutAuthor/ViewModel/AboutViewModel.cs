#region Namespaces
using System.Diagnostics;
using System.Windows.Input;
using WpfCustomControls.ViewModel;
using WpfCustomControls;
using System.Windows;
#endregion

namespace R07_AboutAuthor
{
    public class AboutViewModel : BaseViewModel
    {
     
        private string _Version;
        public string Version { get => _Version; set { _Version = value; OnPropertyChanged(); } }
      
        private string _Support;
        public string Support { get => _Support; set { _Support = value; OnPropertyChanged(); } }
        private string _ContactUs;
        public string ContactUs { get => _ContactUs; set { _ContactUs = value; OnPropertyChanged(); } }
        private string _DonateBank;
        public string DonateBank { get => _DonateBank; set { _DonateBank = value; OnPropertyChanged(); } }
        private string _DonateInfo;
        public string DonateInfo { get => _DonateInfo; set { _DonateInfo = value; OnPropertyChanged(); } }
        private string _CopyRight;
        public string CopyRight { get => _CopyRight; set { _CopyRight = value; OnPropertyChanged(); } }

        private TaskBarViewModel _TaskBarViewModel;
        public TaskBarViewModel TaskBarViewModel { get { return _TaskBarViewModel; } set { _TaskBarViewModel = value; OnPropertyChanged(); } }
        private Languages _Languages;
        public Languages Languages { get { return _Languages; } set { _Languages = value; OnPropertyChanged(); } }
        public ICommand YoutubeCommand { get; set; }
        public ICommand SelectionLanguageChangedCommand { get; set; }
        public AboutViewModel()
        {
            Languages = new Languages("EN");
            TaskBarViewModel = new TaskBarViewModel();
            Version = "Demo";
            Support = "https://www.youtube.com/channel/UCQSwGw2vUjad7kUhEOXbqaw";
            ContactUs = "ryuzaki2005@gmail.com";
            DonateBank = "Ngân Hàng TMCP TechCombank";
            DonateInfo = "19036016100019 BUI TRONG VUONG";
            CopyRight = "Copyright 2021-2022 © Ryuga Ryuzaki";
            YoutubeCommand = new RelayCommand<AboutWindow>((p) => { return true; }, (p) => {
                Process.Start(new ProcessStartInfo(Support));
                p.Close();
            });
            SelectionLanguageChangedCommand = new RelayCommand<Window>((p) => { return true; }, (p) =>
            {
                Languages.ChangeLanguages();
            });
        }

    }
}
