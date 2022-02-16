#region Namespaces
using Autodesk.Revit.UI;
using System.Windows.Forms;
#endregion

namespace R00_Startup
{
    /// <summary>
    /// Tham khảo:
    /// 1. http://bit.ly/2l3Jsf6
    /// 2. https://autode.sk/2mtSaUb
    /// </summary>
    public class DSPApp : IExternalApplication
    {
        public Result OnStartup(UIControlledApplication app)
        {
            //LicenseViewModel licenseViewModel = new LicenseViewModel();
            //if (licenseViewModel.LicenseModel.CheckInternet)
            //{
            //    if (licenseViewModel.LicenseModel.DayTimeout<0|| licenseViewModel.LicenseModel.DayTimeout>30)
            //    {
            //        MessageBox.Show("This app is upgrading process");
            //    }
            //    else
            //    {
            //        CreateRibbonPanel(app);
            //    }
                
            //}
            //else
            //{
            //    MessageBox.Show("Connect Internet to use this Add-in", "DSP Rebar");
            //}
            CreateRibbonPanel(app);
            return Result.Succeeded;
        }

        public Result OnShutdown(UIControlledApplication app)
        {
            return Result.Succeeded;
        }

        private void CreateRibbonPanel(UIControlledApplication a)
        {
            DSPConstraint dlqConstraint = new DSPConstraint();
            RibbonUtils ribbonUtils = new RibbonUtils(a.ControlledApplication);

            // Tạo Ribbon tab
            string ribbonName = "DSP Rebar";
            a.CreateRibbonTab(ribbonName);
            CreateRibbonAutoJoin(a, ribbonUtils, ribbonName);
            CreateRibbonRebar(a, ribbonUtils, ribbonName);
            CreateRibbonAbout(a, ribbonUtils, ribbonName);


        }
        private void CreateRibbonAutoJoin(UIControlledApplication a, RibbonUtils ribbonUtils, string ribbonName)
        {
            string autojoint = "Autojoin";
            RibbonPanel panelAutojoint = a.CreateRibbonPanel(ribbonName, autojoint);

            PushButtonData pushButtonDataAutojoint
                = ribbonUtils.CreatePushButtonData("AutoJoin",
                "AutoJoin", "R05_AutoJoint.dll",
                "R05_AutoJoint.AutoJointCmd", "AutoJoint.png",
                "Auto joint many Structural Category", "",
                "Select rule jointed be for joint", "",
                "https://www.youtube.com/channel/UCQSwGw2vUjad7kUhEOXbqaw");

            panelAutojoint.AddItem(pushButtonDataAutojoint);
        }
        private void CreateRibbonRebar(UIControlledApplication a, RibbonUtils ribbonUtils, string ribbonName)
        {
            #region Rebar
            string rebar = "Structural Rebar";
            RibbonPanel panelRebar = a.CreateRibbonPanel(ribbonName, rebar);
            PushButtonData pushButtonDataColumnsRebar
                = ribbonUtils.CreatePushButtonData("Columns",
                "Columns", "R01_ColumnsRebar.dll",
                "R01_ColumnsRebar.ColumnsRebarCmd", "ColumnRebar.png",
                "Reactangle and Cylin Columns Rebar", "",
                "Select Columns with None-Error", "",
                "https://www.youtube.com/channel/UCQSwGw2vUjad7kUhEOXbqaw");
            PushButtonData pushButtonDataBeamsRebar
               = ribbonUtils.CreatePushButtonData("Beams",
               "Beams", "R02_BeamsRebar.dll",
               "R02_BeamsRebar.BeamsRebarCmd", "BeamRebar.png",
               "Reactangle Beams Rebar", "",
               "Select Beams with None-Error", "",
               "https://www.youtube.com/channel/UCQSwGw2vUjad7kUhEOXbqaw");
            PushButtonData pushButtonDataFoundationRebar
              = ribbonUtils.CreatePushButtonData("Footing",
              "Footing", "R03_FoundationRebar.dll",
              "R03_FoundationRebar.FoundationRebarCmd", "FoundationRebar.png",
              "Foundation Rebar", "",
              "Select Foundation with None-Error", "",
              "https://www.youtube.com/channel/UCQSwGw2vUjad7kUhEOXbqaw");
            PushButtonData pushButtonDataContinueFoundationRebar
             = ribbonUtils.CreatePushButtonData("ContinueFooting",
             "Continue\nFooting", "R08_ContinueFootingRebar.dll",
             "R08_ContinueFootingRebar.ContinueFoundationCmd", "ContinueFooting.png",
             "ContinueFooting Rebar", "",
             "Select ContinueFooting with None-Error", "",
             "https://www.youtube.com/channel/UCQSwGw2vUjad7kUhEOXbqaw");
            PushButtonData pushButtonDataSlabsRebar
            = ribbonUtils.CreatePushButtonData("SlabsRebar",
            "Slabs", "R04_SlabsRebar.dll",
            "R04_SlabsRebar.SlabsRebarCmd", "SlabRebar.png",
            "SlabRebar Rebar", "",
            "Select Slabs with None-Error", "",
            "https://www.youtube.com/channel/UCQSwGw2vUjad7kUhEOXbqaw");
            PushButtonData pushButtonDataWallsRebar
           = ribbonUtils.CreatePushButtonData("WallsRebar",
           "Walls", "R09_WallsRebar.dll",
           "R09_WallsRebar.WallsRebarCmd", "WallsRebar.png",
           "Walls Rebar", "",
           "Select Walls with None-Error", "",
           "https://www.youtube.com/channel/UCQSwGw2vUjad7kUhEOXbqaw");
            PushButtonData pushButtonDataWallShear
           = ribbonUtils.CreatePushButtonData("WallShear",
           "Wall\nShear", "R10_WallShear.dll",
           "R10_WallShear.WallShearCmd", "WallShear.png",
           "Wall Shear", "",
           "Select Walls with None-Error", "",
           "https://www.youtube.com/channel/UCQSwGw2vUjad7kUhEOXbqaw");
            PushButtonData pushButtonDataStairRebar
          = ribbonUtils.CreatePushButtonData("StairRebar",
          "Model Stair\nPlace Rebar", "R06_StairRebar.dll",
          "R06_StairRebar.StairRebarCmd", "StairRebar.png",
          "Stair Rebar", "",
          "Select 4 point , create Stair and place Rebar on Stair", "",
          "https://www.youtube.com/channel/UCQSwGw2vUjad7kUhEOXbqaw");
            panelRebar.AddItem(pushButtonDataColumnsRebar);
            panelRebar.AddItem(pushButtonDataBeamsRebar);
            panelRebar.AddItem(pushButtonDataFoundationRebar);
            panelRebar.AddItem(pushButtonDataContinueFoundationRebar);
            panelRebar.AddItem(pushButtonDataSlabsRebar);
            panelRebar.AddItem(pushButtonDataWallsRebar);
            panelRebar.AddItem(pushButtonDataWallShear);
            panelRebar.AddItem(pushButtonDataStairRebar);
            #endregion
        }
        private void CreateRibbonAbout(UIControlledApplication a, RibbonUtils ribbonUtils, string ribbonName)
        {
            #region About
            string about = "About";
            RibbonPanel panelAbout = a.CreateRibbonPanel(ribbonName, about);
            PushButtonData pushButtonDataAbout
                = ribbonUtils.CreatePushButtonData("About",
                "Infomation", "R07_AboutAuthor.dll",
                "R07_AboutAuthor.AboutCmd", "About.png",
                "About Add-in and Author", "",
                "License and Donate", "",
                "https://www.youtube.com/channel/UCQSwGw2vUjad7kUhEOXbqaw");

            panelAbout.AddItem(pushButtonDataAbout);

            #endregion 
        }
    }
}