using System;
using WpfCustomControls;
namespace R02_BeamsRebar
{
    public class AddBottomBarModel : BaseViewModel
    {
        private ListLayerModel _Start;
        public ListLayerModel Start { get => _Start; set { _Start = value; OnPropertyChanged(); } }
        private ListLayerModel _Mid;
        public ListLayerModel Mid { get => _Mid; set { _Mid = value; OnPropertyChanged(); } }
        private ListLayerModel _End;
        public ListLayerModel End { get => _End; set { _End = value; OnPropertyChanged(); } }
        public AddBottomBarModel()
        {
            Start = new ListLayerModel();
            End = new ListLayerModel();
            Mid = new ListLayerModel();
        }
        public void GetLocationStart(InfoModel infoModel, double start, double c, double dsmax, double dMain, double tmin)
        {
            if (Start.Model.Count != 0)
            {
                double a = 0;
                double b = Math.Abs(infoModel.zOffset) + infoModel.h;
                for (int i = 0; i < Start.Model.Count; i++)
                {
                    int y = (Start.Model[0].NumberBar < 2) ? i : i + 1;
                    double d = (Start.Model[0].NumberBar < 2) ? 0 : dMain;
                    Start.Model[i].X0 = start + c;
                    Start.Model[i].Y0 = b - c - dsmax - Start.Model[i].Bar.Diameter / 2 - a - y * tmin - d;
                    a += Start.Model[i].Bar.Diameter;
                }
                for (int j = 0; j < Start.Model.Count; j++)
                {
                    Start.Model[j].GetLocationStartBottom();
                }
            }
        }
        public void GetLocationEnd(InfoModel infoModel, double end, double c, double dsmax, double dMain, double tmin)
        {
            if (End.Model.Count != 0)
            {
                double a = 0;
                double b = Math.Abs(infoModel.zOffset) + infoModel.h;
                for (int i = 0; i < End.Model.Count; i++)
                {
                    int y = (End.Model[0].NumberBar < 2) ? i : i + 1;
                    double d = (End.Model[0].NumberBar < 2) ? 0 : dMain;
                    End.Model[i].X0 = end - c;
                    End.Model[i].Y0 = b - c - dsmax  - End.Model[i].Bar.Diameter / 2 - a - y * tmin - d;
                    a += End.Model[i].Bar.Diameter;
                }
                for (int j = 0; j < End.Model.Count; j++)
                {
                    End.Model[j].GetLocationEndBottom();
                }
            }
        }
        public void GetLocationMid(InfoModel infoModel, double c, double dsmax, double dMain, double tmin)
        {
            if (Mid.Model.Count != 0)
            {
                double a = 0;
                double b = Math.Abs(infoModel.zOffset) + infoModel.h;
                for (int i = 0; i < Mid.Model.Count; i++)
                {
                    int y = (Mid.Model[0].NumberBar < 2) ? i : i + 1;
                    double d = (Mid.Model[0].NumberBar < 2) ? 0 : dMain;
                    Mid.Model[i].X0 = infoModel.startPosition+ infoModel.Length/2;
                    Mid.Model[i].Y0 = b - c - dsmax - Mid.Model[i].Bar.Diameter / 2 - a - y * tmin - d;
                    a += Mid.Model[i].Bar.Diameter;
                }
                for (int j = 0; j < Mid.Model.Count; j++)
                {
                    Mid.Model[j].GetLocationMidBottom();
                }
            }
        }
    }
}
