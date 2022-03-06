using System.Collections.Generic;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using WpfCustomControls.CustomControls;

namespace WpfCustomControls.ViewModel
{
    public class TaskBarViewModel : BaseViewModel
    {

        public ICommand LoadTaskBarControlCommand { get; set; }
        public ICommand MouseLeftButtonDownCommand { get; set; }
        public ICommand CloseWindowCommand { get; set; }
        public ICommand GotoWebCommand { get; set; }
        public ICommand ShowAccountcommand { get; set; }
   
        public TaskBarViewModel()
        {
           
            LoadTaskBarControlCommand = new RelayCommand<TaskBarControl>((p) => { return true; }, (p) =>
            {
                DrawLogo(p);
            });
            MouseLeftButtonDownCommand = new RelayCommand<Window>((p) => { return true; }, (p) =>
            {
                p.DragMove();
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
            //SelectionLanguageChangedCommand = new RelayCommand<Window>((p) => { return true; }, (p) =>
            //{
            //    Languages.ChangeLanguages(SelectedLanguage);
            //});
            
           
        }
        private void DrawLogo(TaskBarControl p)
        {
            DrawIcon.DrawLogo(p.LogoCanvas);
        }
    }
}
