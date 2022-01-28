using System;
using System.Collections.ObjectModel;
using System.Linq;
using static R01_ColumnsRebar.ErrorColumns;

namespace R01_ColumnsRebar
{
    public class BarMainModel : BaseViewModel
    {
        #region propery
        private int _NumberColumn;
        public int NumberColumn { get => _NumberColumn; set { _NumberColumn = value; OnPropertyChanged(); } }
        private ObservableCollection<BarModel> _BarModels;
        public ObservableCollection<BarModel> BarModels { get { if (_BarModels == null) _BarModels = new ObservableCollection<BarModel>(); return _BarModels; } set { _BarModels = value; OnPropertyChanged(); } }
        private ObservableCollection<BarModel> _AddBarModels;
        public ObservableCollection<BarModel> AddBarModels { get { if (_AddBarModels == null) _AddBarModels = new ObservableCollection<BarModel>(); return _AddBarModels; } set { _AddBarModels = value; OnPropertyChanged(); } }
        private int _nx;
        public int nx { get => _nx; set { _nx = value; OnPropertyChanged(); } }
        private int _ny;
        public int ny { get => _ny; set { _ny = value; OnPropertyChanged(); } }
        private int _nd;
        public int nd { get => _nd; set { _nd = value; OnPropertyChanged(); } }
        private int _nxA;
        public int nxA { get => _nxA; set { _nxA = value; OnPropertyChanged(); } }
        private int _nyA;
        public int nyA { get => _nyA; set { _nyA = value; OnPropertyChanged(); } }
        private int _ndA;
        public int ndA { get => _ndA; set { _ndA = value; OnPropertyChanged(); } }
        private double _SplitOverlap;
        public double SplitOverlap { get => _SplitOverlap; set { _SplitOverlap = value; OnPropertyChanged(); } }
        private double _Overlap;
        public double Overlap { get => _Overlap; set { _Overlap = value; OnPropertyChanged(); } }
     
        private RebarBarModel _Bar;
        public RebarBarModel Bar { get => _Bar; set { _Bar = value; OnPropertyChanged(); } }
        private RebarBarModel _AddBar;
        public RebarBarModel AddBar { get => _AddBar; set { _AddBar = value; OnPropertyChanged(); } }
        private int _SelectedBarType;
        public int SelectedBarType { get => _SelectedBarType; set { _SelectedBarType = value; OnPropertyChanged(); } }
        private int _SelectedAddBarType;
        public int SelectedAddBarType { get => _SelectedAddBarType; set { _SelectedAddBarType = value; OnPropertyChanged(); } }
        private bool _FixedTop;
        public bool FixedTop { get => _FixedTop; set { _FixedTop = value; OnPropertyChanged(); } }
        private bool _FixedBottom;
        public bool FixedBottom { get => _FixedBottom; set { _FixedBottom = value; OnPropertyChanged(); } }
        #endregion
        public BarMainModel(int numberColumn, SectionStyle sectionStyle,RebarBarModel rebarBarModel)
        {
            NumberColumn = numberColumn;
            if (sectionStyle == SectionStyle.RECTANGLE)
            {
                nx = 2; ny = 2; nd = 0;
                nxA = 2; nyA = 2; ndA = 0;
            }
            else
            {
                nx = 0; ny = 0; nd = 4;
                nxA = 0; nyA = 0; ndA = 4;
            }
            Bar = Bar;
            AddBar = Bar;
            SplitOverlap = 50;
            Overlap = 35;
            SelectedBarType = 3;
            SelectedAddBarType = 3;
            FixedTop = false;
            FixedBottom = false;
        }
        #region Method
        public void RefreshX0Y0BarModels(SectionStyle sectionStyle, InfoModel infoModel, double Cover, double ds)
        {
            
            if (sectionStyle == SectionStyle.RECTANGLE)
            {
                double deltaX = (infoModel.b - 2 * Cover - 2 * ds - Bar.Diameter) / (nx - 1);
                double deltaY = (infoModel.h - 2 * Cover - 2 * ds - Bar.Diameter) / (ny - 1);
                int i = 0;
                while (i < nx)
                {
                    double x0 = infoModel.WestPosition + Cover + ds + Bar.Diameter / 2 + i * deltaX;
                    double y0 = infoModel.SouthPosition + Cover + ds + Bar.Diameter / 2;
                    //var a = new BarModel(i + 1, Bar, SplitOverlap, Overlap);
                    BarModels[i].GetX0Y0(x0, y0);
                    i++;
                }
                i -= 1;
                int j = 1;
                while (j < ny - 1)
                {
                    double x0 = infoModel.WestPosition + Cover + ds + Bar.Diameter / 2 + i * deltaX;
                    double y0 = infoModel.SouthPosition + Cover + ds + Bar.Diameter / 2 + j * deltaY;
                    //var a = new BarModel(i + j + 1, Bar, SplitOverlap, Overlap);
                    //a.GetX0Y0(x0, y0);
                    //BarModels.Add(a);
                    BarModels[i + j].GetX0Y0(x0, y0);
                    j++;
                }
                int k = 0;
                while (k < nx)
                {
                    double x0 = infoModel.WestPosition + Cover + ds + Bar.Diameter / 2 + (i - k) * deltaX;
                    double y0 = infoModel.SouthPosition + Cover + ds + Bar.Diameter / 2 + j * deltaY;
                    //var a = new BarModel(i + j + k + 1, Bar, SplitOverlap, Overlap);
                    //a.GetX0Y0(x0, y0);
                    //BarModels.Add(a);
                    BarModels[i + j + k].GetX0Y0(x0, y0);
                    k++;
                }
                k -= 1;
                int l = 1;
                while (l < ny - 1)
                {
                    double x0 = infoModel.WestPosition + Cover + ds + Bar.Diameter / 2 + (i - k) * deltaX;
                    double y0 = infoModel.SouthPosition + Cover + ds + Bar.Diameter / 2 + (j - l) * deltaY;
                    //var a = new BarModel(i + j + k + l + 1, Bar, SplitOverlap, Overlap);
                    //a.GetX0Y0(x0, y0);
                    //BarModels.Add(a);
                    BarModels[i + j + k + l].GetX0Y0(x0, y0);
                    l++;
                }
            }
            else
            {

                for (int i = 0; i < nd; i++)
                {
                    double angle = (i * Math.PI * 2) / nd;
                    double x0 = infoModel.PointXPosition + ((infoModel.D * 0.5 - Cover - ds - Bar.Diameter * 0.5)) * Math.Round(Math.Cos(angle), 9);
                    double y0 = infoModel.PointYPosition + ((infoModel.D * 0.5 - Cover - ds - Bar.Diameter * 0.5)) * Math.Round(Math.Sin(angle), 9);
                    //var a = new BarModel(i + 1, Bar, SplitOverlap, Overlap);
                    //a.GetX0Y0(x0, y0);
                   
                    //BarModels.Add(a);
                    BarModels[i].GetX0Y0(x0, y0);
                }
            }

        }
        public void GetBarModels(SectionStyle sectionStyle, InfoModel infoModel,  double Cover, double ds)
        {
            if (BarModels.Count != 0)
            {
                BarModels.Clear();
            }
            if (sectionStyle == SectionStyle.RECTANGLE)
            {
                double deltaX = (infoModel.b - 2 * Cover - 2 * ds - Bar.Diameter) / (nx - 1);
                double deltaY = (infoModel.h - 2 * Cover - 2 * ds - Bar.Diameter) / (ny - 1);
                int i = 0;
                while (i < nx)
                {
                    double x0 = infoModel.WestPosition + Cover + ds + Bar.Diameter / 2 + i * deltaX;
                    double y0 = infoModel.SouthPosition + Cover + ds + Bar.Diameter / 2;
                    var a = new BarModel(i + 1, Bar, SplitOverlap, Overlap);
                    a.GetX0Y0(x0, y0);
                    BarModels.Add(a);
                    i++;
                }
                i -= 1;
                int j = 1;
                while (j < ny - 1)
                {
                    double x0 = infoModel.WestPosition + Cover + ds + Bar.Diameter / 2 + i * deltaX;
                    double y0 = infoModel.SouthPosition + Cover + ds + Bar.Diameter / 2 + j * deltaY;
                    var a = new BarModel(i + j + 1, Bar, SplitOverlap, Overlap);
                    a.GetX0Y0(x0, y0);
                    BarModels.Add(a);
                    j++;
                }
                int k = 0;
                while (k < nx)
                {
                    double x0 = infoModel.WestPosition + Cover + ds + Bar.Diameter / 2 + (i - k) * deltaX;
                    double y0 = infoModel.SouthPosition + Cover + ds + Bar.Diameter / 2 + j * deltaY;
                    var a = new BarModel(i + j + k + 1, Bar, SplitOverlap, Overlap);
                    a.GetX0Y0(x0, y0);
                    BarModels.Add(a);
                    k++;
                }
                k -= 1;
                int l = 1;
                while (l < ny - 1)
                {
                    double x0 = infoModel.WestPosition + Cover + ds + Bar.Diameter / 2 + (i - k) * deltaX;
                    double y0 = infoModel.SouthPosition + Cover + ds + Bar.Diameter / 2 + (j - l) * deltaY;
                    var a = new BarModel(i + j + k + l + 1, Bar, SplitOverlap, Overlap);
                    a.GetX0Y0(x0, y0);
                    BarModels.Add(a);
                    l++;
                }
            }
            else
            {

                for (int i = 0; i < nd; i++)
                {
                    double angle = (i * Math.PI * 2) / nd;
                    double x0 = infoModel.PointXPosition + ((infoModel.D * 0.5 - Cover - ds - Bar.Diameter * 0.5)) * Math.Round(Math.Cos(angle), 9);
                    double y0 = infoModel.PointYPosition + ((infoModel.D * 0.5 - Cover - ds - Bar.Diameter * 0.5)) * Math.Round(Math.Sin(angle), 9);
                    var a = new BarModel(i + 1, Bar, SplitOverlap, Overlap);
                    a.GetX0Y0(x0, y0);
                    BarModels.Add(a);
                }
            }

        }
        public void RefreshX0Y0AddBarModels(SectionStyle sectionStyle, InfoModel infoModel, double Cover, double ds)
        {

            if (sectionStyle == SectionStyle.RECTANGLE)
            {

                double deltaX = (infoModel.b - 2 * Cover - 2 * ds - AddBar.Diameter) / (nxA - 1);
                double deltaY = (infoModel.h - 2 * Cover - 2 * ds - AddBar.Diameter) / (nyA - 1);
                double d = AddBar.Diameter;
                int i = 0;
                while (i < nxA)
                {
                    double x0 = 0;
                    double y0 = (infoModel.SouthPosition + Cover + ds + d / 2);
                    if (i == 0)
                    {
                        x0 = (infoModel.WestPosition + Cover + ds + d / 2 + d + i * deltaX);
                    }
                    else
                    {
                        if (i == nxA - 1)
                        {
                            x0 = (infoModel.WestPosition + Cover + ds + d / 2 - d + i * deltaX);
                        }
                        else
                        {
                            x0 = (infoModel.WestPosition + Cover + ds + d / 2 + i * deltaX);
                        }
                    }
                    //var a = new BarModel(i + 1, AddBar, SplitOverlap, Overlap, L1, L2, L3);
                    //a.GetX0Y0(x0, y0);
                    //AddBarModels.Add(a);
                    AddBarModels[i].GetX0Y0(x0, y0);
                    i++;
                }
                i -= 1;
                int j = 1;
                while (j < nyA - 1)
                {
                    double x0 = infoModel.WestPosition + Cover + ds + d / 2 + i * deltaX;
                    double y0 = 0;
                    if (j == 1)
                    {
                        y0 = (infoModel.SouthPosition + Cover + ds + d / 2 + j * deltaY + d);
                    }
                    else
                    {
                        if (j == nyA - 2)
                        {
                            y0 = (infoModel.SouthPosition + Cover + ds + d / 2 + j * deltaY - d);
                        }
                        else
                        {
                            y0 = (infoModel.SouthPosition + Cover + ds + d / 2 + j * deltaY);
                        }
                    }
                    //var a = new BarModel(i + j + 1, AddBar, SplitOverlap, Overlap, L1, L2, L3);
                    //a.GetX0Y0(x0, y0);
                    //AddBarModels.Add(a);
                    AddBarModels[i + j].GetX0Y0(x0, y0);
                    j++;
                }
                int k = 0;
                while (k < nxA)
                {
                    double x0 = 0;
                    double y0 = (infoModel.SouthPosition + Cover + ds + d / 2 + j * deltaY);
                    if (k == 0)
                    {
                        x0 = (infoModel.WestPosition + Cover + ds + d / 2 - d + (i - k) * deltaX);
                    }
                    else
                    {
                        if (k == nxA - 1)
                        {
                            x0 = (infoModel.WestPosition + Cover + ds + d / 2 + d + (i - k) * deltaX);
                        }
                        else
                        {
                            x0 = (infoModel.WestPosition + Cover + ds + d / 2 + (i - k) * deltaX);
                        }
                    }
                    //var a = new BarModel(i + j + k + 1, AddBar, SplitOverlap, Overlap, L1, L2, L3);
                    //a.GetX0Y0(x0, y0);
                    //AddBarModels.Add(a);
                    AddBarModels[i + j+k].GetX0Y0(x0, y0);
                    k++;
                }
                k -= 1;
                int l = 1;
                while (l < nyA - 1)
                {
                    double x0 = infoModel.WestPosition + Cover + ds + d / 2 + (i - k) * deltaX;
                    double y0 = 0;
                    if (l == 1)
                    {
                        y0 = (infoModel.SouthPosition + Cover + ds + d / 2 + (j - l) * deltaY - d);
                    }
                    else
                    {
                        y0 = (infoModel.SouthPosition + Cover + ds + d / 2 + (j - l) * deltaY);
                    }
                    //var a = new BarModel(i + j + k + l + 1, AddBar, SplitOverlap, Overlap, L1, L2, L3);
                    //a.GetX0Y0(x0, y0);
                    //AddBarModels.Add(a);
                    AddBarModels[i + j + k+l].GetX0Y0(x0, y0);
                    l++;
                }
            }
            else
            {
                double d = AddBar.Diameter;
                double angle1 = d / ((infoModel.D * 0.5 - Cover - ds - d * 0.5) * 2 * Math.PI);
                for (int i = 0; i < ndA; i++)
                {
                    double angle = angle1 + (i * Math.PI * 2) / ndA;
                    double x0 = (infoModel.PointXPosition + ((infoModel.D * 0.5 - Cover - ds - d * 0.5)) * Math.Round(Math.Cos(angle), 9));
                    double y0 = (infoModel.PointYPosition + ((infoModel.D * 0.5 - Cover - ds - d * 0.5)) * Math.Round(Math.Sin(angle), 9));
                    //var a = new BarModel(i + 1, AddBar, SplitOverlap, Overlap, L1, L2, L3);
                    //a.GetX0Y0(x0, y0);
                    //AddBarModels.Add(a);
                    AddBarModels[i].GetX0Y0(x0, y0);
                }
            }


        }
        public void GetAddBarModels(SectionStyle sectionStyle, InfoModel infoModel,  double Cover, double ds, double L1, double L2, double L3)
        {
            
            if (AddBarModels.Count != 0)
            {
                AddBarModels.Clear();
            }
            if (sectionStyle == SectionStyle.RECTANGLE)
            {
               
                double deltaX = (infoModel.b - 2 * Cover - 2 * ds - AddBar.Diameter) / (nxA - 1);
                double deltaY = (infoModel.h - 2 * Cover - 2 * ds - AddBar.Diameter) / (nyA - 1);
                double d = AddBar.Diameter;
                int i = 0;
                while (i < nxA)
                {
                    double x0 = 0;
                    double y0 = (infoModel.SouthPosition + Cover + ds + d / 2);
                    if (i == 0)
                    {
                        x0 = (infoModel.WestPosition + Cover + ds + d / 2 + d + i * deltaX);
                    }
                    else
                    {
                        if (i == nxA - 1)
                        {
                            x0 = (infoModel.WestPosition + Cover + ds + d / 2 - d + i * deltaX);
                        }
                        else
                        {
                            x0 = (infoModel.WestPosition + Cover + ds + d / 2 + i * deltaX);
                        }
                    }
                    var a = new BarModel(i + 1, AddBar, SplitOverlap, Overlap, L1, L2, L3);
                    a.GetX0Y0(x0, y0);
                    AddBarModels.Add(a);
                    i++;
                }
                i -= 1;
                int j = 1;
                while (j < nyA - 1)
                {
                    double x0 = infoModel.WestPosition + Cover + ds + d / 2 + i * deltaX;
                    double y0 = 0;
                    if (j == 1)
                    {
                        y0 = (infoModel.SouthPosition + Cover + ds + d / 2 + j * deltaY + d);
                    }
                    else
                    {
                        if (j == nyA - 2)
                        {
                            y0 = (infoModel.SouthPosition + Cover + ds + d / 2 + j * deltaY - d);
                        }
                        else
                        {
                            y0 = (infoModel.SouthPosition + Cover + ds + d / 2 + j * deltaY);
                        }
                    }
                    var a = new BarModel(i + j + 1, AddBar, SplitOverlap, Overlap, L1, L2, L3);
                    a.GetX0Y0(x0, y0);
                    AddBarModels.Add(a);
                    j++;
                }
                int k = 0;
                while (k < nxA)
                {
                    double x0 = 0;
                    double y0 = (infoModel.SouthPosition + Cover + ds + d / 2 + j * deltaY);
                    if (k == 0)
                    {
                        x0 = (infoModel.WestPosition + Cover + ds + d / 2 - d + (i - k) * deltaX);
                    }
                    else
                    {
                        if (k == nxA - 1)
                        {
                            x0 = (infoModel.WestPosition + Cover + ds + d / 2 + d + (i - k) * deltaX);
                        }
                        else
                        {
                            x0 = (infoModel.WestPosition + Cover + ds + d / 2 + (i - k) * deltaX);
                        }
                    }
                    var a = new BarModel(i + j + k + 1, AddBar, SplitOverlap, Overlap, L1, L2, L3);
                    a.GetX0Y0(x0, y0);
                    AddBarModels.Add(a);
                    k++;
                }
                k -= 1;
                int l = 1;
                while (l < nyA - 1)
                {
                    double x0 = infoModel.WestPosition + Cover + ds + d / 2 + (i - k) * deltaX;
                    double y0 = 0;
                    if (l == 1)
                    {
                        y0 = (infoModel.SouthPosition + Cover + ds + d / 2 + (j - l) * deltaY - d);
                    }
                    else
                    {
                        y0 = (infoModel.SouthPosition + Cover + ds + d / 2 + (j - l) * deltaY);
                    }
                    var a = new BarModel(i + j + k + l + 1, AddBar, SplitOverlap, Overlap, L1, L2, L3);
                    a.GetX0Y0(x0, y0);
                    AddBarModels.Add(a);
                    l++;
                }
            }
            else
            {
                double d = AddBar.Diameter;
                double angle1 = d / ((infoModel.D * 0.5 - Cover - ds - d * 0.5) * 2 * Math.PI);
                for (int i = 0; i < ndA; i++)
                {
                    double angle = angle1 + (i * Math.PI * 2) / ndA;
                    double x0 = (infoModel.PointXPosition + ((infoModel.D * 0.5 - Cover - ds - d * 0.5)) * Math.Round(Math.Cos(angle), 9));
                    double y0 = (infoModel.PointYPosition + ((infoModel.D * 0.5 - Cover - ds - d * 0.5)) * Math.Round(Math.Sin(angle), 9));
                    var a = new BarModel(i + 1, AddBar, SplitOverlap, Overlap, L1, L2, L3);
                    a.GetX0Y0(x0, y0);
                    AddBarModels.Add(a);
                }
            }


        }
        //private ObservableCollection<double> GetNextX0(SectionStyle sectionStyle, InfoModel infoModelNext, double Cover, double ds, double d)
        //{
        //    ObservableCollection<double> x0 = new ObservableCollection<double>();

        //    if (sectionStyle == SectionStyle.RECTANGLE)
        //    {
        //        double deltaX = (infoModelNext.b - 2 * Cover - 2 * ds - d) / (nx - 1);
        //        int i = 0;
        //        while (i < nx)
        //        {
        //            x0.Add(infoModelNext.WestPosition + Cover + ds + d / 2  + i * deltaX);
        //            i++;
        //        }
        //        i -= 1;
        //        int j = 1;
        //        while (j < ny - 1)
        //        {
        //            x0.Add(infoModelNext.WestPosition + Cover + ds + d / 2 + i * deltaX);
        //            j++;
        //        }
        //        int k = 0;
        //        while (k < nx)
        //        {
        //            x0.Add(infoModelNext.WestPosition + Cover + ds + d / 2  + (i - k) * deltaX);
        //            k++;
        //        }
        //        k -= 1;
        //        int l = 1;
        //        while (l < ny - 1)
        //        {
        //            x0.Add(infoModelNext.WestPosition + Cover + ds + d / 2 + (i - k) * deltaX);
        //            l++;
        //        }
        //    }
        //    else
        //    {
        //        for (int i = 0; i < nd; i++)
        //        {
        //            double angle = (i * Math.PI * 2) / nd;
        //            x0.Add(infoModelNext.PointXPosition + ((infoModelNext.D * 0.5 - Cover - ds - d * 0.5)) * Math.Round(Math.Cos(angle), 9));
        //        }
        //    }
        //    return x0;
        //}
        //private ObservableCollection<double> GetNextX0(SectionStyle sectionStyle, InfoModel infoModelNext, double Cover, double ds, double d)
        //{
        //    ObservableCollection<double> x0 = new ObservableCollection<double>();

        //    if (sectionStyle == SectionStyle.RECTANGLE)
        //    {
        //        double deltaX = (infoModelNext.b - 2 * Cover - 2 * ds - d) / (nx - 1);
        //        int i = 0;
        //        while (i < nx)
        //        {
        //            if (i == 0)
        //            {
        //                x0.Add(infoModelNext.WestPosition + Cover + ds + d / 2 +  d + i * deltaX);
        //            }
        //            else
        //            {
        //                if (i == nx - 1)
        //                {
        //                    x0.Add(infoModelNext.WestPosition + Cover + ds + d / 2 -  d + i * deltaX);
        //                }
        //                else
        //                {
        //                    x0.Add(infoModelNext.WestPosition + Cover + ds + d / 2 + i * deltaX);
        //                }
        //            }
        //            i++;
        //        }
        //        i -= 1;
        //        int j = 1;
        //        while (j < ny - 1)
        //        {
        //            x0.Add(infoModelNext.WestPosition + Cover + ds + d / 2 + i * deltaX - d);
        //            j++;
        //        }
        //        int k = 0;
        //        while (k < nx)
        //        {
        //            if (k == 0)
        //            {
        //                x0.Add(infoModelNext.WestPosition + Cover + ds + d / 2 - d + (i - k) * deltaX);
        //            }
        //            else
        //            {
        //                if (k == nx - 1)
        //                {
        //                    x0.Add(infoModelNext.WestPosition + Cover + ds + d / 2 + d + (i - k) * deltaX);
        //                }
        //                else
        //                {
        //                    x0.Add(infoModelNext.WestPosition + Cover + ds + d / 2 + (i - k) * deltaX);
        //                }
        //            }
        //            k++;
        //        }
        //        k -= 1;
        //        int l = 1;
        //        while (l < ny - 1)
        //        {
        //            x0.Add(infoModelNext.WestPosition + Cover + ds + d / 2 + (i - k) * deltaX + d);
        //            l++;
        //        }
        //    }
        //    else
        //    {
        //        for (int i = 0; i < nd; i++)
        //        {
        //            double angle = (i * Math.PI * 2) / nd;
        //            x0.Add(infoModelNext.PointXPosition + ((infoModelNext.D * 0.5 - Cover - ds - d * 0.5 - d)) * Math.Round(Math.Cos(angle), 9));
        //        }
        //    }
        //    return x0;
        //}
        private ObservableCollection<double> GetNextX0(SectionStyle sectionStyle, InfoModel infoModelNext, double Cover, double ds, double d)
        {
            ObservableCollection<double> x0 = new ObservableCollection<double>();

            if (sectionStyle == SectionStyle.RECTANGLE)
            {
                double deltaX = (infoModelNext.b - 2 * Cover - 2 * ds - d) / (nx - 1);
                int i = 0;
                while (i < nx)
                {
                    if (i == 0)
                    {
                        x0.Add(infoModelNext.WestPosition + Cover + ds + d / 2 + d + i * deltaX);
                    }
                    else
                    {
                        if (i == nx - 1)
                        {
                            x0.Add(infoModelNext.WestPosition + Cover + ds + d / 2 - d + i * deltaX);
                        }
                        else
                        {
                            x0.Add(infoModelNext.WestPosition + Cover + ds + d / 2 + d + i * deltaX);
                        }
                    }
                    i++;
                }
                i -= 1;
                int j = 1;
                while (j < ny - 1)
                {
                    x0.Add(infoModelNext.WestPosition + Cover + ds + d / 2 + i * deltaX);
                    j++;
                }
                int k = 0;
                while (k < nx)
                {
                    if (k == 0)
                    {
                        x0.Add(infoModelNext.WestPosition + Cover + ds + d / 2 - d + (i - k) * deltaX);
                    }
                    else
                    {
                        if (k == nx - 1)
                        {
                            x0.Add(infoModelNext.WestPosition + Cover + ds + d / 2 + d + (i - k) * deltaX);
                        }
                        else
                        {
                            x0.Add(infoModelNext.WestPosition + Cover + ds + d / 2 + d + (i - k) * deltaX);
                        }
                    }
                    k++;
                }
                k -= 1;
                int l = 1;
                while (l < ny - 1)
                {
                    x0.Add(infoModelNext.WestPosition + Cover + ds + d / 2 + (i - k) * deltaX);
                    l++;
                }
            }
            else
            {
                double angle1 = d / ((infoModelNext.D * 0.5 - Cover - ds - d * 0.5) * 2 * Math.PI);
                for (int i = 0; i < nd; i++)
                {
                    double angle = angle1 + (i * Math.PI * 2) / nd;
                    x0.Add(infoModelNext.PointXPosition + ((infoModelNext.D * 0.5 - Cover - ds - d * 0.5)) * Math.Round(Math.Cos(angle), 9));
                }
            }
            return x0;
        }
        //private ObservableCollection<double> GetNextY0(SectionStyle sectionStyle, InfoModel infoModelNext, double Cover, double ds, double d)
        //{
        //    ObservableCollection<double> y0 = new ObservableCollection<double>();

        //    if (sectionStyle == SectionStyle.RECTANGLE)
        //    {
        //        double deltaY = (infoModelNext.h - 2 * Cover - 2 * ds - d) / (ny - 1);
        //        int i = 0;
        //        while (i < nx)
        //        {
        //            y0.Add(infoModelNext.SouthPosition + Cover + ds + d/ 2);
        //            i++;
        //        }
        //        i -= 1;
        //        int j = 1;
        //        while (j < ny - 1)
        //        {
        //            y0.Add(infoModelNext.SouthPosition + Cover + ds + d / 2  + j * deltaY);
        //            j++;
        //        }
        //        int k = 0;
        //        while (k < nx)
        //        {
        //            y0.Add(infoModelNext.SouthPosition + Cover + ds + d / 2 + j * deltaY);
        //            k++;
        //        }
        //        k -= 1;
        //        int l = 1;
        //        while (l < ny - 1)
        //        {
        //            y0.Add(infoModelNext.SouthPosition + Cover + ds + d / 2 + (j - l) * deltaY);
        //            l++;
        //        }
        //    }
        //    else
        //    {

        //        for (int i = 0; i < nd; i++)
        //        {
        //            double angle = (i * Math.PI * 2) / nd;
        //            y0.Add(infoModelNext.PointYPosition + ((infoModelNext.D * 0.5 - Cover - ds - d * 0.5)) * Math.Round(Math.Sin(angle), 9));
        //        }
        //    }
        //    return y0;
        //}
        //private ObservableCollection<double> GetNextY0(SectionStyle sectionStyle, InfoModel infoModelNext, double Cover, double ds, double d)
        //{
        //    ObservableCollection<double> y0 = new ObservableCollection<double>();

        //    if (sectionStyle == SectionStyle.RECTANGLE)
        //    {
        //        double deltaY = (infoModelNext.h - 2 * Cover - 2 * ds - d) / (ny - 1);
        //        int i = 0;
        //        while (i < nx)
        //        {
        //            y0.Add(infoModelNext.SouthPosition + Cover + ds + d / 2 + d);
        //            i++;
        //        }
        //        i -= 1;
        //        int j = 1;
        //        while (j < ny - 1)
        //        {
        //            if (j == 1)
        //            {
        //                y0.Add(infoModelNext.SouthPosition + Cover + ds + d / 2 + j * deltaY + d);
        //            }
        //            else
        //            {
        //                if (j == ny - 2)
        //                {
        //                    y0.Add(infoModelNext.SouthPosition + Cover + ds + d / 2 + j * deltaY - d);
        //                }
        //                else
        //                {
        //                    y0.Add(infoModelNext.SouthPosition + Cover + ds + d / 2 + j * deltaY);
        //                }
        //            }

        //            j++;
        //        }
        //        int k = 0;
        //        while (k < nx)
        //        {
        //            y0.Add(infoModelNext.SouthPosition + Cover + ds + d / 2 + j * deltaY - d);
        //            k++;
        //        }
        //        k -= 1;
        //        int l = 1;
        //        while (l < ny - 1)
        //        {
        //            if (l == 1)
        //            {
        //                y0.Add(infoModelNext.SouthPosition + Cover + ds + d / 2 + (j - l) * deltaY - d);
        //            }
        //            else
        //            {
        //                y0.Add(infoModelNext.SouthPosition + Cover + ds + d / 2 + (j - l) * deltaY);
        //            }

        //            l++;
        //        }
        //    }
        //    else
        //    {

        //        for (int i = 0; i < nd; i++)
        //        {
        //            double angle = (i * Math.PI * 2) / nd;
        //            y0.Add(infoModelNext.PointYPosition + ((infoModelNext.D * 0.5 - Cover - ds - d * 0.5 - d)) * Math.Round(Math.Sin(angle), 9));
        //        }
        //    }
        //    return y0;
        //}
        private ObservableCollection<double> GetNextY0(SectionStyle sectionStyle, InfoModel infoModelNext, double Cover, double ds, double d)
        {
            ObservableCollection<double> y0 = new ObservableCollection<double>();

            if (sectionStyle == SectionStyle.RECTANGLE)
            {
                double deltaY = (infoModelNext.h - 2 * Cover - 2 * ds - d) / (ny - 1);
                int i = 0;
                while (i < nx)
                {
                    y0.Add(infoModelNext.SouthPosition + Cover + ds + d / 2);
                    i++;
                }
                i -= 1;
                int j = 1;
                while (j < ny - 1)
                {
                    //if (j == 1)
                    //{
                    //    y0.Add(infoModelNext.SouthPosition + Cover + ds + d / 2 + j * deltaY + d);
                    //}
                    //else
                    //{
                    //    if (j == ny - 2)
                    //    {
                    //        y0.Add(infoModelNext.SouthPosition + Cover + ds + d / 2 + j * deltaY - d);
                    //    }
                    //    else
                    //    {
                    //        y0.Add(infoModelNext.SouthPosition + Cover + ds + d / 2 + d + j * deltaY);
                    //    }
                    //}
                    y0.Add(infoModelNext.SouthPosition + Cover + ds + d / 2 + j * deltaY + d);
                    j++;
                }
                int k = 0;
                while (k < nx)
                {
                    y0.Add(infoModelNext.SouthPosition + Cover + ds + d / 2 + j * deltaY);
                    k++;
                }
                k -= 1;
                int l = 1;
                while (l < ny - 1)
                {
                    //if (l == 1)
                    //{
                    //    y0.Add(infoModelNext.SouthPosition + Cover + ds + d / 2 + (j - l) * deltaY - d);
                    //}
                    //else
                    //{
                    //    y0.Add(infoModelNext.SouthPosition + Cover + ds + d / 2 + (j - l) * deltaY+d);
                    //}
                    y0.Add(infoModelNext.SouthPosition + Cover + ds + d / 2 + (j - l) * deltaY + d);
                    l++;
                }
            }
            else
            {
                double angle1 = d / ((infoModelNext.D * 0.5 - Cover - ds - d * 0.5) * 2 * Math.PI);
                for (int i = 0; i < nd; i++)
                {
                    double angle = angle1 + (i * Math.PI * 2) / nd;
                    y0.Add(infoModelNext.PointYPosition + ((infoModelNext.D * 0.5 - Cover - ds - d * 0.5)) * Math.Round(Math.Sin(angle), 9));
                }
            }
            return y0;
        }
        public void GetLocationBarModels(InfoModel infoModel, double Cover)
        {
            for (int i = 0; i < BarModels.Count; i++)
            {
                BarModels[i].GetLocationBar(infoModel, Cover);
            }
        }
        public void RefreshLocationBarModels(SectionStyle sectionStyle, InfoModel infoModel, double Cover, double ds0,double dsUp, InfoModel infoModelUp = null)
        {

            double ds = ds0;
            RefreshX0Y0BarModels(sectionStyle, infoModel, Cover, ds);
            for (int i = 0; i < BarModels.Count; i++)
            {
                double x01 = 0, y01 = 0;
                if (infoModelUp == null)
                {
                    x01 = BarModels[i].X0;
                    y01 = BarModels[i].Y0;
                }
                else
                {
                    #region exam
                    bool condition = BarModels[i].IsTopDowels && BarModels[i].TopDowels == 0;
                    if (condition)
                    {
                        ds = dsUp;
                        if (sectionStyle == SectionStyle.RECTANGLE)
                        {
                            if (BarModels[i].BarNumber <= nx)
                            {
                                ObservableCollection<BarModel> barModelSouth = new ObservableCollection<BarModel>(BarModels.Where(x => x.BarNumber <= nx && x.IsTopDowels && x.TopDowels == 0).OrderBy(x => x.BarNumber).ToList());
                                if (barModelSouth.Count != 0)
                                {

                                    y01 = (infoModelUp.SouthPosition + Cover + ds + BarModels[i].Bar.Diameter / 2);
                                    if (barModelSouth.Count == 1)
                                    {
                                        x01 = GetNextX0(sectionStyle, infoModelUp, Cover, ds, BarModels[i].Bar.Diameter)[BarModels[i].BarNumber - 1];
                                    }
                                    else
                                    {
                                        double deltaX = (infoModelUp.b - 2 * Cover - 2 * ds - BarModels[i].Bar.Diameter) / (barModelSouth.Count - 1);
                                        int index = barModelSouth.IndexOf(barModelSouth.Where(x => x.BarNumber == BarModels[i].BarNumber).FirstOrDefault());
                                        if (index == 0)
                                        {
                                            x01 = infoModelUp.WestPosition + Cover + ds + BarModels[i].Bar.Diameter / 2 + BarModels[i].Bar.Diameter + index * deltaX;
                                        }
                                        else
                                        {
                                            if (index == barModelSouth.Count - 1)
                                            {
                                                x01 = infoModelUp.WestPosition + Cover + ds + BarModels[i].Bar.Diameter / 2 - BarModels[i].Bar.Diameter + index * deltaX;
                                            }
                                            else
                                            {
                                                x01 = infoModelUp.WestPosition + Cover + ds + BarModels[i].Bar.Diameter / 2 + BarModels[i].Bar.Diameter + index * deltaX;
                                            }
                                        }
                                    }
                                }
                            }
                            if (BarModels[i].BarNumber > nx && BarModels[i].BarNumber <= nx + ny - 2)
                            {
                                ObservableCollection<BarModel> barModelEast = new ObservableCollection<BarModel>(BarModels.Where(x => (x.BarNumber > nx && x.BarNumber <= nx + ny - 2) && x.IsTopDowels && x.TopDowels == 0).OrderBy(x => x.BarNumber).ToList());
                                if (barModelEast.Count != 0)
                                {
                                    x01 = infoModelUp.EastPosition - Cover - ds - BarModels[i].Bar.Diameter / 2;
                                    double deltaY = (infoModelUp.h - 2 * Cover - 2 * ds - BarModels[i].Bar.Diameter) / (barModelEast.Count + 1);
                                    int index = barModelEast.IndexOf(barModelEast.Where(x => x.BarNumber == BarModels[i].BarNumber).FirstOrDefault());
                                    y01 = infoModelUp.SouthPosition + Cover + ds + BarModels[i].Bar.Diameter / 2 + (index + 1) * deltaY - BarModels[i].Bar.Diameter;
                                }
                            }
                            if (BarModels[i].BarNumber > nx + ny - 2 && BarModels[i].BarNumber <= 2 * nx + ny - 2)
                            {
                                ObservableCollection<BarModel> barModelNouth = new ObservableCollection<BarModel>(BarModels.Where(x => (x.BarNumber > nx + ny - 2 && x.BarNumber <= 2 * nx + ny - 2) && x.IsTopDowels && x.TopDowels == 0).OrderBy(x => x.BarNumber).ToList());
                                if (barModelNouth.Count != 0)
                                {
                                    y01 = (infoModelUp.NouthPosition - Cover - ds - BarModels[i].Bar.Diameter / 2);
                                    if (barModelNouth.Count == 1)
                                    {
                                        x01 = GetNextX0(sectionStyle, infoModelUp, Cover, ds, BarModels[i].Bar.Diameter)[BarModels[i].BarNumber - 1];
                                    }
                                    else
                                    {
                                        double deltaX = (infoModelUp.b - 2 * Cover - 2 * ds - BarModels[i].Bar.Diameter) / (barModelNouth.Count - 1);
                                        int index = barModelNouth.IndexOf(barModelNouth.Where(x => x.BarNumber == BarModels[i].BarNumber).FirstOrDefault());
                                        if (index == 0)
                                        {
                                            x01 = infoModelUp.EastPosition - Cover - ds - BarModels[i].Bar.Diameter / 2 - BarModels[i].Bar.Diameter - index * deltaX;
                                        }
                                        else
                                        {
                                            if (index == barModelNouth.Count - 1)
                                            {
                                                x01 = infoModelUp.EastPosition - Cover - ds - BarModels[i].Bar.Diameter / 2 + BarModels[i].Bar.Diameter - index * deltaX;
                                            }
                                            else
                                            {
                                                x01 = infoModelUp.EastPosition - Cover - ds - BarModels[i].Bar.Diameter / 2 - BarModels[i].Bar.Diameter - index * deltaX;
                                            }
                                        }
                                    }
                                }
                            }
                            if (BarModels[i].BarNumber > 2 * nx + ny - 2)
                            {
                                ObservableCollection<BarModel> barModelWest = new ObservableCollection<BarModel>(BarModels.Where(x => (x.BarNumber > 2 * nx + ny - 2) && x.IsTopDowels && x.TopDowels == 0).OrderBy(x => x.BarNumber).ToList());
                                if (barModelWest.Count != 0)
                                {
                                    x01 = infoModelUp.WestPosition + Cover + ds + BarModels[i].Bar.Diameter / 2;
                                    double deltaY = (infoModelUp.h - 2 * Cover - 2 * ds - BarModels[i].Bar.Diameter) / (barModelWest.Count + 1);
                                    int index = barModelWest.IndexOf(barModelWest.Where(x => x.BarNumber == BarModels[i].BarNumber).FirstOrDefault());
                                    y01 = infoModelUp.NouthPosition - Cover - ds - BarModels[i].Bar.Diameter / 2 - (index + 1) * deltaY - BarModels[i].Bar.Diameter;
                                }
                            }
                        }
                        else
                        {
                            if (nd == 4)
                            {
                                x01 = GetNextX0(sectionStyle, infoModelUp, Cover, ds, BarModels[i].Bar.Diameter)[BarModels[i].BarNumber - 1];
                                y01 = GetNextY0(sectionStyle, infoModelUp, Cover, ds, BarModels[i].Bar.Diameter)[BarModels[i].BarNumber - 1];
                            }
                            else
                            {
                                if (BarModels[i].BarNumber % (nd / 4) == 1)
                                {
                                    x01 = GetNextX0(sectionStyle, infoModelUp, Cover, ds, BarModels[i].Bar.Diameter)[BarModels[i].BarNumber - 1];
                                    y01 = GetNextY0(sectionStyle, infoModelUp, Cover, ds, BarModels[i].Bar.Diameter)[BarModels[i].BarNumber - 1];
                                }
                                else
                                {
                                    double d = BarModels[i].Bar.Diameter;
                                    double angle1 =2* d / ((infoModelUp.D * 0.5 - Cover - ds - d * 0.5) * 2 );
                                    ObservableCollection<BarModel> barModel0 = new ObservableCollection<BarModel>();
                                    if (BarModels[i].BarNumber > 1 && BarModels[i].BarNumber < (nd / 4) + 1)
                                    {
                                        barModel0 = new ObservableCollection<BarModel>(BarModels.Where(x => (x.BarNumber > 1 && x.BarNumber < (nd / 4) + 1) && x.IsTopDowels && x.TopDowels == 0).OrderBy(x => x.BarNumber).ToList());
                                        if (barModel0.Count != 0)
                                        {
                                            int index = barModel0.IndexOf(barModel0.Where(x => x.BarNumber == BarModels[i].BarNumber).FirstOrDefault());
                                            double angle = angle1 + ((index + 1) * Math.PI / 2) / (barModel0.Count + 1);
                                            x01 = (infoModelUp.PointXPosition + ((infoModelUp.D * 0.5 - Cover - ds - d * 0.5)) * Math.Round(Math.Cos(angle), 9));
                                            y01 = (infoModelUp.PointYPosition + ((infoModelUp.D * 0.5 - Cover - ds - d * 0.5)) * Math.Round(Math.Sin(angle), 9));
                                        }
                                    }
                                    if (BarModels[i].BarNumber > (nd / 4) + 1 && BarModels[i].BarNumber < 2 * (nd / 4) + 1)
                                    {
                                        barModel0 = new ObservableCollection<BarModel>(BarModels.Where(x => (x.BarNumber > (nd / 4) + 1 && x.BarNumber < 2 * (nd / 4) + 1) && x.IsTopDowels && x.TopDowels == 0).OrderBy(x => x.BarNumber).ToList());
                                        if (barModel0.Count != 0)
                                        {
                                            int index = barModel0.IndexOf(barModel0.Where(x => x.BarNumber == BarModels[i].BarNumber).FirstOrDefault());
                                            double angle = angle1 + ((index + 1) * Math.PI / 2) / (barModel0.Count + 1) + Math.PI / 2;
                                            x01 = (infoModelUp.PointXPosition + ((infoModelUp.D * 0.5 - Cover - ds - d * 0.5)) * Math.Round(Math.Cos(angle), 9));
                                            y01 = (infoModelUp.PointYPosition + ((infoModelUp.D * 0.5 - Cover - ds - d * 0.5)) * Math.Round(Math.Sin(angle), 9));
                                        }
                                    }
                                    if (BarModels[i].BarNumber > 2 * (nd / 4) + 1 && BarModels[i].BarNumber < 3 * (nd / 4) + 1)
                                    {
                                        barModel0 = new ObservableCollection<BarModel>(BarModels.Where(x => (x.BarNumber > 2 * (nd / 4) + 1 && x.BarNumber < 3 * (nd / 4) + 1) && x.IsTopDowels && x.TopDowels == 0).OrderBy(x => x.BarNumber).ToList());
                                        if (barModel0.Count != 0)
                                        {
                                            int index = barModel0.IndexOf(barModel0.Where(x => x.BarNumber == BarModels[i].BarNumber).FirstOrDefault());
                                            double angle = angle1 + ((index + 1) * Math.PI / 2) / (barModel0.Count + 1) + Math.PI;
                                            x01 = (infoModelUp.PointXPosition + ((infoModelUp.D * 0.5 - Cover - ds - d * 0.5)) * Math.Round(Math.Cos(angle), 9));
                                            y01 = (infoModelUp.PointYPosition + ((infoModelUp.D * 0.5 - Cover - ds - d * 0.5)) * Math.Round(Math.Sin(angle), 9));
                                        }
                                    }
                                    if (BarModels[i].BarNumber > 3 * (nd / 4) + 1)
                                    {
                                        barModel0 = new ObservableCollection<BarModel>(BarModels.Where(x => (x.BarNumber > 3 * (nd / 4) + 1) && x.IsTopDowels && x.TopDowels == 0).OrderBy(x => x.BarNumber).ToList());
                                        if (barModel0.Count != 0)
                                        {
                                            int index = barModel0.IndexOf(barModel0.Where(x => x.BarNumber == BarModels[i].BarNumber).FirstOrDefault());
                                            double angle = angle1 + ((index + 1) * Math.PI / 2) / (barModel0.Count + 1) + Math.PI * 3 / 2;
                                            x01 = (infoModelUp.PointXPosition + ((infoModelUp.D * 0.5 - Cover - ds - d * 0.5)) * Math.Round(Math.Cos(angle), 9));
                                            y01 = (infoModelUp.PointYPosition + ((infoModelUp.D * 0.5 - Cover - ds - d * 0.5)) * Math.Round(Math.Sin(angle), 9));
                                        }
                                    }

                                }
                            }

                        }
                    }
                    else
                    {
                        ds = ds0;
                        x01 = GetNextX0(sectionStyle, infoModelUp, Cover, ds, BarModels[i].Bar.Diameter)[BarModels[i].BarNumber - 1];
                        y01 = GetNextY0(sectionStyle, infoModelUp, Cover, ds, BarModels[i].Bar.Diameter)[BarModels[i].BarNumber - 1];
                    }
                    #endregion

                }

                if (sectionStyle == SectionStyle.RECTANGLE)
                {
                    BarModels[i].GetDowelsLocationBarRectangle(infoModel, 2 * nx + 2 * (ny - 2), nx, ny, Cover, SplitOverlap, Overlap, x01, y01);
                }
                else
                {
                    BarModels[i].GetDowelsLocationBarCylindrical(infoModel, nd, Cover, ds, SplitOverlap, Overlap, x01, y01);
                }
            }
        }
        public void RefreshLocationAddBarModels(SectionStyle sectionStyle, InfoModel infoModel,double TopPosition, double Cover, double ds)
        {
            for (int i = 0; i < AddBarModels.Count; i++)
            {
                if (sectionStyle == SectionStyle.RECTANGLE)
                {
                    AddBarModels[i].GetDowelsLocationAddBarRectangle(TopPosition, 2 * nxA + 2 * (nyA - 2), nxA, nyA);
                }
                else
                {
                    AddBarModels[i].GetDowelsLocationAddBarCylindrical(infoModel, TopPosition, ndA, Cover, ds);
                }
            }
        }
        #endregion
        #region Fixed
        public bool ConditionFixedTop(SectionStyle sectionStyle, InfoModel infoModelUp=null,BarMainModel barMainModelUp=null)
        {
            if (BarModels.Count == 0)
            {
                return false;
            }
            else
            {
                if (infoModelUp == null)
                {
                    return false;

                }
                else
                {
                    if (barMainModelUp.BarModels.Count == 0)
                    {
                        return false;
                    }
                    else
                    {
                        ObservableCollection<BarModel> barModels = new ObservableCollection<BarModel>(BarModels.Where(x=>x.IsTopDowels&&x.TopDowels==0).ToList());
                        if (barModels.Count == 0)
                        {
                            if (AddBarModels.Count==0)
                            {
                                return false;
                            }
                            else
                            {
                                return (AddBarModels.Count== barMainModelUp.BarModels.Count);
                               
                            }
                        }
                        else
                        {
                            return barModels.Count == barMainModelUp.BarModels.Count;
                            //if (barModels.Count != barMainModelUp.BarModels.Count)
                            //{
                            //    return false;
                            //}
                            //else
                            //{
                            //    #region
                            //    //if (sectionStyle == SectionStyle.RECTANGLE)
                            //    //{
                            //    //    ObservableCollection<BarModel> barModelSouth = new ObservableCollection<BarModel>(BarModels.Where(x => x.BarNumber <= nx && x.IsTopDowels && x.TopDowels == 0).ToList());
                            //    //    if (barModelSouth.Count == 0)
                            //    //    {
                            //    //        return false;
                            //    //    }
                            //    //    else
                            //    //    {
                            //    //        if (barModelSouth.Count != barMainModelUp.nx)
                            //    //        {
                            //    //            return false;
                            //    //        }
                            //    //        else
                            //    //        {
                            //    //            ObservableCollection<BarModel> barModelEast = new ObservableCollection<BarModel>(BarModels.Where(x => (x.BarNumber > nx && x.BarNumber <= nx + ny - 2) && x.IsTopDowels && x.TopDowels == 0).ToList());
                            //    //            if (barModelEast.Count==0)
                            //    //            {
                            //    //                return false;
                            //    //            }
                            //    //            else
                            //    //            {
                            //    //                if (barModelEast.Count!=barMainModelUp.ny-2)
                            //    //                {
                            //    //                    return false;
                            //    //                }
                            //    //                else
                            //    //                {
                            //    //                    ObservableCollection<BarModel> barModelNouth = new ObservableCollection<BarModel>(BarModels.Where(x => (x.BarNumber > nx + ny - 2 && x.BarNumber <= 2 * nx + ny - 2) && x.IsTopDowels && x.TopDowels == 0).ToList());
                            //    //                    if (barModelNouth.Count==0)
                            //    //                    {
                            //    //                        return false;
                            //    //                    }
                            //    //                    else
                            //    //                    {
                            //    //                        if (barModelNouth.Count!=barMainModelUp.nx)
                            //    //                        {
                            //    //                            return false;
                            //    //                        }
                            //    //                        else
                            //    //                        {
                            //    //                             ObservableCollection<BarModel> barModelWest = new ObservableCollection<BarModel>(BarModels.Where(x => (x.BarNumber > 2 * nx + ny - 2) && x.IsTopDowels && x.TopDowels == 0).ToList());
                            //    //                            if (barModelWest.Count==0)
                            //    //                            {
                            //    //                                return false;
                            //    //                            }
                            //    //                            else
                            //    //                            {
                            //    //                                return (barModelWest.Count == barMainModelUp.ny - 2);
                            //    //                            }
                            //    //                        }
                            //    //                    }
                            //    //                }
                            //    //            }
                            //    //        }

                            //    //    }

                            //    //}
                            //    //else
                            //    //{
                            //    //    if (nd == 4)
                            //    //    {
                            //    //        return true;
                            //    //    }
                            //    //    else
                            //    //    {
                            //    //        ObservableCollection<BarModel> barModel0 = new ObservableCollection<BarModel>(BarModels.Where(x =>  x.IsTopDowels && x.TopDowels == 0).OrderBy(x => x.BarNumber).ToList());
                            //    //        return barModel0.Count == barMainModelUp.BarModels.Count;
                            //    //    }

                            //    //}
                            //    #endregion
                            //    ObservableCollection<BarModel> barModel0 = new ObservableCollection<BarModel>(BarModels.Where(x => x.IsTopDowels && x.TopDowels == 0).OrderBy(x => x.BarNumber).ToList());
                            //    return barModel0.Count == barMainModelUp.BarModels.Count;
                            //}
                        }
                        
                    }
                }
            }
            
            
            return true;
        }
        public bool ConditionFixedBottom(SectionStyle sectionStyle, InfoModel infoModelDown = null, BarMainModel barMainModelDown = null)
        {
            if (BarModels.Count == 0)
            {
                return false;
            }
            else
            {
                if (infoModelDown == null)
                {
                    return false;

                }
                else
                {
                    if (barMainModelDown.BarModels.Count == 0)
                    {
                        return false;
                    }
                    else
                    {
                        if (!barMainModelDown.FixedTop)
                        {
                            return false;
                        }
                        else
                        {
                            ObservableCollection<BarModel> barModels = new ObservableCollection<BarModel>(BarModels.Where(x => x.IsBottomDowels && x.BottomDowels == 0).ToList());
                            if (barModels.Count!=BarModels.Count)
                            {
                                return false;
                            }
                            else
                            {
                                return true;
                            }
                        }
                       
                       
                    }
                }
            }


            return true;
        }
        #endregion
        public bool ConditionShowAddTop()
        {
            if (BarModels.Count==0)
            {
                return false;
            }
            else
            {
                for (int i = 0; i < BarModels.Count; i++)
                {
                    if (BarModels[i].IsTopDowels&&BarModels[i].TopDowels==0)
                    {
                        return false;
                    }
                }
            }
            return true;
        }
    }
}
