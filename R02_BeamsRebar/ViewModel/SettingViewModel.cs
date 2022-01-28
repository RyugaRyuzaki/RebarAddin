
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using R02_BeamsRebar.View;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;

namespace R02_BeamsRebar.ViewModel
{
    public class SettingViewModel:BaseViewModel
    {
        #region Property
        private BeamsModel _BeamsModel;
        public BeamsModel BeamsModel { get => _BeamsModel; set { _BeamsModel = value; OnPropertyChanged(); } }
        #endregion
        #region Command
        public ICommand LoadSettingViewCommand { get; set; }
        public ICommand TextChangedCommand { get; set; }
        #endregion
        public SettingViewModel(BeamsModel beamsModel)
        {
            BeamsModel = beamsModel;
            #region LoadCommand
            LoadSettingViewCommand = new RelayCommand<BeamsWindow>((p) => { return true; }, (p) =>
            {
                p.canvas.Width = BeamsModel.DrawModel.Width;
                p.canvas.Height = BeamsModel.DrawModel.Height;
                SettingView uc = ProcessInfoBeamRebar.FindChild<SettingView>(p, "SettingUC");
                DrawCanvas1(uc);
                DrawCanvas3(uc);
                DrawCanvas4(uc);
                DrawInfo(p);
            });
            TextChangedCommand = new RelayCommand<BeamsWindow>((p) => { return true; }, (p) =>
            {
                SettingView uc = ProcessInfoBeamRebar.FindChild<SettingView>(p, "SettingUC");
                foreach (var item in ProcessInfoBeamRebar.FindLogicalChildren<System.Windows.Controls.TextBox>(uc))
                {
                    if (!double.TryParse(item.Text, out double S))
                    {
                        MessageBox.Show("DataError", "ERROR", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
                        return;
                    }
                }
            });
            #endregion
        }
        #region Draw
        private void DrawCanvas1(SettingView p)
        {
            p.canvas1.Children.Clear();
            DrawImage.DrawStirrup(p.canvas1, 30, 30, 1, 200, 300, 10, 3, 14, BeamsModel.DrawModel.ColorStirrupChoose);
            DrawImage.DrawSection(p.canvas1, 1, 30, 30, 200, 300);
            DrawImage.DrawLayerMainBarTop(p.canvas1, 30, 30, 1, 200, 10, 3, 14, 3, BeamsModel.DrawModel.ColorMainBarChoose);
            DrawImage.DrawLayerMainBarBottom(p.canvas1, 30, 30, 1, 200, 300, 10, 3, 14, 3, BeamsModel.DrawModel.ColorMainBarChoose);
            DrawImage.DrawLayerMainBarBottom(p.canvas1, 30, 0, 1, 200, 300, 10, 3, 14, 1, BeamsModel.DrawModel.ColorMainBarChoose);
            DrawImage.DrawLayerMainBarBottom(p.canvas1, 30, -30, 1, 200, 300, 10, 3, 14, 1, BeamsModel.DrawModel.ColorMainBarChoose);
            DrawImage.DimHorizontalText(p.canvas1, 30 + 80, 80, 1, 40, 11, 6, 3, "dmin");
            DrawImage.DimHorizontalText(p.canvas1, 30 + 135, 80, 1, 40, 11, 6, 3, "dmin");
            DrawImage.DimVerticalText(p.canvas1, 65, 255, 1, 20, 11, 6, 3, "tmin");
            DrawImage.DimVerticalText(p.canvas1, 65, 285, 1, 20, 11, 6, 3, "tmin");
        }

        private void DrawCanvas3(SettingView p)
        {
            double b = 120, h = 200, c = 5, ds = 3, d = 12, left = 50, top = 100, scale = 1; int n = 2;
            double r = (ds + d) / (2 * scale);

            p.canvas3.Children.Clear();
            DrawImage.DrawStirrup(p.canvas3, left, top, scale, b, h, c, ds, d, BeamsModel.DrawModel.ColorStirrupChoose);
            DrawImage.DrawSection(p.canvas3, scale, left, top, b, h);
            DrawImage.DrawLayerMainBarTag(p.canvas3, left, top + (c + r + ds / 2) / scale, scale, b, c, ds, d, n, 60, 40, BeamsModel.DrawModel.ColorMainBarChoose, BeamsModel.DrawModel.ColorTag, true);
            DrawImage.DrawLayerMainBarTag(p.canvas3, left, top + (c + r + ds / 2) / scale + 30, scale, b, c, ds, d, n, 60, 40, BeamsModel.DrawModel.ColorMainBarChoose, BeamsModel.DrawModel.ColorTag, false);
            DrawImage.DrawLayerMainBarTag(p.canvas3, left, top + (h - c - r - ds / 2) / scale, scale, b, c, ds, d, n, 60, 40, BeamsModel.DrawModel.ColorMainBarChoose, BeamsModel.DrawModel.ColorTag, false);
            DrawImage.DimHorizontalText(p.canvas3, 50, 30, 1, 120, 11, 20, 5, "200");
            DrawImage.DimVerticalText(p.canvas3, 30, 100, 1, 200, 11, 20, 5, "300");
            DrawImage.DimHorizontalText1(p.canvas3, 10, 150, 1, 40, 11, 4, 2, "DimH");
            DrawImage.DimVerticalText1(p.canvas3, 20, 10, 1, 90, 11, 4, 2, "DimV");
            DrawImage.DimHorizontalText1(p.canvas3, 170, 150, 1, 40, 11, 4, 2, "TagH");
            DrawImage.DimVerticalText1(p.canvas3, 40, 60, 1, 60, 11, 4, 2, "TagV");
        }
        private void DrawCanvas4(SettingView p)
        {
            p.canvas4.Children.Clear();
            DrawImage.DrawBeamDetail(p.canvas4);
            DrawImage.DrawBeamDetail1(p.canvas4);
        }
        private void DrawInfo(BeamsWindow p)
        {
            p.canvas.Children.Clear();
            DrawMainCanvas.SpanBeams(p.canvas, BeamsModel.InfoModels, BeamsModel.DrawModel, 1000);
            DrawMainCanvas.NodeBeams(p.canvas, BeamsModel.InfoModels, BeamsModel.NodeModels, BeamsModel.DrawModel, 1000);
            DrawMainCanvas.SpecialNode(p.canvas, BeamsModel.InfoModels, BeamsModel.SpecialNodeModels, BeamsModel.DrawModel, 1000);
        }
        #endregion
    }
}
