#region Namespaces
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Windows.Forms;
using System.Windows.Input;
using Autodesk.Revit.ApplicationServices;
using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.DB.Structure;
using Autodesk.Revit.UI;
using Autodesk.Revit.UI.Selection;
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
        public ICommand OKCommand { get; set; }
        public ICommand YoutubeCommand { get; set; }
        public AboutViewModel()
        {
            Version = "Demo";
            Support = "https://www.youtube.com/channel/UCQSwGw2vUjad7kUhEOXbqaw";
            ContactUs = "ryuzaki2005@gmail.com";
            DonateBank = "Ngân Hàng TMCP TechCombank";
            DonateInfo = "19036016100019 BUI TRONG VUONG";
            CopyRight = "Copyright 2021-2022 © Ryuga Ryuzaki";
            OKCommand = new RelayCommand<AboutWindow>((p) => { return true; }, (p) => { p.Close(); });
            YoutubeCommand = new RelayCommand<AboutWindow>((p) => { return true; }, (p) => {
                Process.Start(new ProcessStartInfo(Support));
                p.Close();
            });
        }

    }
}
