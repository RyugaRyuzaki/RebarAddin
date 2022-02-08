using System.Collections.Generic;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using WpfCustomControls.CustomControls;
using WpfCustomControls.LanguageModel;

namespace WpfCustomControls.ViewModel
{
    public class TaskBarViewModel : BaseViewModel
    {

        private Languages _Languages;
        public Languages Languages { get { return _Languages; } set { _Languages = value; OnPropertyChanged(); } }

        private List<string> _AllLanguages;
        public List<string> AllLanguages { get { if (_AllLanguages == null) { _AllLanguages = new List<string>() { "EN", "VN" }; } return _AllLanguages; } set { _AllLanguages = value; OnPropertyChanged(); } }

        private string _SelectedLanguage;
        public string SelectedLanguage { get { return _SelectedLanguage; } set { _SelectedLanguage = value; OnPropertyChanged(); } }

        public ICommand LoadTaskBarControlCommand { get; set; }
        public ICommand MouseLeftButtonDownCommand { get; set; }
        public ICommand CloseWindowCommand { get; set; }
        public ICommand GotoWebCommand { get; set; }
        public ICommand ShowAccountcommand { get; set; }
        public ICommand SelectionLanguageChangedCommand { get; set; }
        public TaskBarViewModel()
        {
            SelectedLanguage = AllLanguages[0];
            Languages = new Languages(SelectedLanguage);
            LoadTaskBarControlCommand = new RelayCommand<TaskBarControl>((p) => { return true; }, (p) =>
            {
                DrawLogo(p);
            });
            MouseLeftButtonDownCommand = new RelayCommand<Window>((p) => { return true; }, (p) =>
            {
                p.DragMove();
            });
            CloseWindowCommand = new RelayCommand<Window>((p) => { return true; }, (p) =>
            {
                p.Close();
            });
            GotoWebCommand = new RelayCommand<Window>((p) => { return true; }, (p) =>
            {
                string navigateUri = "https://www.youtube.com/channel/UCQSwGw2vUjad7kUhEOXbqaw";
                Process.Start(new ProcessStartInfo(navigateUri));
            });
            ShowAccountcommand = new RelayCommand<Window>((p) => { return true; }, (p) =>
            {
                AccountViewModel accountViewModel = new AccountViewModel();
                AccountWindow accountWindow = new AccountWindow(accountViewModel);
                accountWindow.Show();
            });
            SelectionLanguageChangedCommand = new RelayCommand<Window>((p) => { return true; }, (p) =>
            {
                Languages.ChangeLanguages(SelectedLanguage);
            });
            
           
        }
        private void DrawLogo(TaskBarControl p)
        {
            DrawIcon.DrawLogo(p.LogoCanvas);
        }
    }
}
