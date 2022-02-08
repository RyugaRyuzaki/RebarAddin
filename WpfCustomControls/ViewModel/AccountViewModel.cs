using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace WpfCustomControls.ViewModel
{
    public class AccountViewModel:BaseViewModel
    {
        public ICommand LoadAccountCommand { get; set; }
        public ICommand MouseLeftButtonDownCommand { get; set; }
        public ICommand CloseWindowCommand { get; set; }
        public ICommand GotoWebCommand { get; set; }
        public AccountViewModel()
        {
            LoadAccountCommand = new RelayCommand<AccountWindow>((p) => { return true; }, (p) =>
            {
                DrawLogo(p);
               
            });
            MouseLeftButtonDownCommand = new RelayCommand<AccountWindow>((p) => { return true; }, (p) =>
            {
                p.DragMove();
            });
            CloseWindowCommand = new RelayCommand<AccountWindow>((p) => { return true; }, (p) =>
            {
                p.Close();
            });
            GotoWebCommand = new RelayCommand<AccountWindow>((p) => { return true; }, (p) =>
            {
                string navigateUri = "https://www.youtube.com/channel/UCQSwGw2vUjad7kUhEOXbqaw";
                Process.Start(new ProcessStartInfo(navigateUri));
            });
        }
        private void DrawLogo(AccountWindow p)
        {
            DrawIcon.DrawLogo(p.LogoCanvas);
        }
    }
}
