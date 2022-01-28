using System.Collections.ObjectModel;
using System.Linq;

namespace R10_WallShear
{
    public class BarMainModel : BaseViewModel
    {
        #region propery
        private int _NumberWall;
        public int NumberWall { get => _NumberWall; set { _NumberWall = value; OnPropertyChanged(); } }
        private ObservableCollection<BarModel> _BarModels;
        public ObservableCollection<BarModel> BarModels { get { if (_BarModels == null) _BarModels = new ObservableCollection<BarModel>(); return _BarModels; } set { _BarModels = value; OnPropertyChanged(); } }
        private ObservableCollection<BarModel> _BarCornerModels;
        public ObservableCollection<BarModel> BarCornerModels { get { if (_BarCornerModels == null) _BarCornerModels = new ObservableCollection<BarModel>(); return _BarCornerModels; } set { _BarCornerModels = value; OnPropertyChanged(); } }
        private int _nx;
        public int nx { get => _nx; set { _nx = value; OnPropertyChanged(); } }
        private int _ny;
        public int ny { get => _ny; set { _ny = value; OnPropertyChanged(); } }
        private int _nxCorner;
        public int nxCorner { get => _nxCorner; set { _nxCorner = value; OnPropertyChanged(); } }
        private int _nyCorner;
        public int nyCorner { get => _nyCorner; set { _nyCorner = value; OnPropertyChanged(); } }
        private double _SplitOverlap;
        public double SplitOverlap { get => _SplitOverlap; set { _SplitOverlap = value; OnPropertyChanged(); } }
        private double _Overlap;
        public double Overlap { get => _Overlap; set { _Overlap = value; OnPropertyChanged(); } }

        private bool _IsCorner;
        public bool IsCorner { get => _IsCorner; set { _IsCorner = value; OnPropertyChanged(); } }

        private RebarBarModel _Bar;
        public RebarBarModel Bar { get => _Bar; set { _Bar = value; OnPropertyChanged(); } }
        private RebarBarModel _BarCorner;
        public RebarBarModel BarCorner { get => _BarCorner; set { _BarCorner = value; OnPropertyChanged(); } }

        private bool _FixedTop;
        public bool FixedTop { get => _FixedTop; set { _FixedTop = value; OnPropertyChanged(); } }
        private bool _FixedBottom;
        public bool FixedBottom { get => _FixedBottom; set { _FixedBottom = value; OnPropertyChanged(); } }
        #endregion
        public BarMainModel(int numberWall, RebarBarModel rebarBarModel)
        {
            NumberWall = numberWall;
            Bar = rebarBarModel;
            BarCorner = rebarBarModel;
            nx = 2; ny = 2; nxCorner = 2; nyCorner = 2;
            SplitOverlap = 50;
            Overlap = 35;
            FixedTop = false;
            FixedBottom = false;
            IsCorner = false;
        }
        #region BarModel
        public void GetBarModels(InfoModel infoModel, double Cover, double ds)
        {
            if (BarModels.Count != 0)
            {
                BarModels.Clear();
            }
            double deltaX0 = ((infoModel.IsCorner) ? infoModel.L2 : infoModel.L);
            double deltaX = (deltaX0 - 2 * Cover - 2 * ds - Bar.Diameter) / (nx - 1);
            double deltaY = (infoModel.T - 2 * Cover - 2 * ds - Bar.Diameter) / (ny - 1);
            int i = 0;
            while (i < nx)
            {
                double x0 = infoModel.WestPosition + ((infoModel.IsCorner) ? infoModel.L1 : 0) + Cover + ds + Bar.Diameter / 2 + i * deltaX;
                double y0 = infoModel.SouthPosition + Cover + ds + Bar.Diameter / 2;
                var a = new BarModel(i + 1, Bar, SplitOverlap, Overlap, false);
                a.GetX0Y0(x0, y0);
                BarModels.Add(a);
                i++;
            }
            i -= 1;
            int j = 1;
            while (j < ny - 1)
            {
                double x0 = infoModel.WestPosition + ((infoModel.IsCorner) ? infoModel.L1 : 0) + Cover + ds + Bar.Diameter / 2 + i * deltaX;
                double y0 = infoModel.SouthPosition + Cover + ds + Bar.Diameter / 2 + j * deltaY;
                var a = new BarModel(i + j + 1, Bar, SplitOverlap, Overlap, false);
                a.GetX0Y0(x0, y0);
                BarModels.Add(a);
                j++;
            }
            int k = 0;
            while (k < nx)
            {
                double x0 = infoModel.WestPosition + ((infoModel.IsCorner) ? infoModel.L1 : 0) + Cover + ds + Bar.Diameter / 2 + (i - k) * deltaX;
                double y0 = infoModel.SouthPosition + Cover + ds + Bar.Diameter / 2 + j * deltaY;
                var a = new BarModel(i + j + k + 1, Bar, SplitOverlap, Overlap, false);
                a.GetX0Y0(x0, y0);
                BarModels.Add(a);
                k++;
            }
            k -= 1;
            int l = 1;
            while (l < ny - 1)
            {
                double x0 = infoModel.WestPosition + ((infoModel.IsCorner) ? infoModel.L1 : 0) + Cover + ds + Bar.Diameter / 2 + (i - k) * deltaX;
                double y0 = infoModel.SouthPosition + Cover + ds + Bar.Diameter / 2 + (j - l) * deltaY;
                var a = new BarModel(i + j + k + l + 1, Bar, SplitOverlap, Overlap, false);
                a.GetX0Y0(x0, y0);
                BarModels.Add(a);
                l++;
            }

        }
        public void RefreshX0Y0BarModels(InfoModel infoModel, double Cover, double ds)
        {

            double deltaX = (((infoModel.IsCorner) ? infoModel.L2 : infoModel.L) - 2 * Cover - 2 * ds - Bar.Diameter) / (nx - 1);
            double deltaY = (infoModel.T - 2 * Cover - 2 * ds - Bar.Diameter) / (ny - 1);
            int i = 0;
            while (i < nx)
            {
                double x0 = infoModel.WestPosition + ((infoModel.IsCorner) ? infoModel.L1 : 0) + Cover + ds + Bar.Diameter / 2 + i * deltaX;
                double y0 = infoModel.SouthPosition + Cover + ds + Bar.Diameter / 2;
                //var a = new BarModel(i + 1, Bar, SplitOverlap, Overlap);
                //a.GetX0Y0(x0, y0);
                //BarModels.Add(a);
                BarModels[i].GetX0Y0(x0, y0);
                i++;
            }
            i -= 1;
            int j = 1;
            while (j < ny - 1)
            {
                double x0 = infoModel.WestPosition + ((infoModel.IsCorner) ? infoModel.L1 : 0) + Cover + ds + Bar.Diameter / 2 + i * deltaX;
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
                double x0 = infoModel.WestPosition + ((infoModel.IsCorner) ? infoModel.L1 : 0) + Cover + ds + Bar.Diameter / 2 + (i - k) * deltaX;
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
                double x0 = infoModel.WestPosition + ((infoModel.IsCorner) ? infoModel.L1 : 0) + Cover + ds + Bar.Diameter / 2 + (i - k) * deltaX;
                double y0 = infoModel.SouthPosition + Cover + ds + Bar.Diameter / 2 + (j - l) * deltaY;
                //var a = new BarModel(i + j + k + l + 1, Bar, SplitOverlap, Overlap);
                //a.GetX0Y0(x0, y0);
                //BarModels.Add(a);
                BarModels[i + j + k + l].GetX0Y0(x0, y0);
                l++;
            }

        }
        private ObservableCollection<double> GetNextX0Bar(InfoModel infoModelUp, double Cover, double ds, double d)
        {
            ObservableCollection<double> x0 = new ObservableCollection<double>();
           
            double deltaX = ((infoModelUp.IsCorner) ? infoModelUp.L2 : infoModelUp.L - 2 * Cover - 2 * ds - d) / (nx - 1);
            int i = 0;
            while (i < nx)
            {
                if (i == 0)
                {
                    x0.Add(infoModelUp.WestPosition + ((infoModelUp.IsCorner) ? infoModelUp.L1 : 0) + Cover + ds + d / 2 + d + i * deltaX);
                }
                else
                {
                    if (i == nx - 1)
                    {
                        x0.Add(infoModelUp.WestPosition + ((infoModelUp.IsCorner) ? infoModelUp.L1 : 0) + Cover + ds + d / 2 - d + i * deltaX);
                    }
                    else
                    {
                        x0.Add(infoModelUp.WestPosition + ((infoModelUp.IsCorner) ? infoModelUp.L1 : 0) + Cover + ds + d / 2 + d + i * deltaX);
                    }
                }
                i++;
            }
            i -= 1;
            int j = 1;
            while (j < ny - 1)
            {
                x0.Add(infoModelUp.WestPosition + ((infoModelUp.IsCorner) ? infoModelUp.L1 : 0) + Cover + ds + d / 2 + i * deltaX);
                j++;
            }
            int k = 0;
            while (k < nx)
            {
                if (k == 0)
                {
                    x0.Add(infoModelUp.WestPosition + ((infoModelUp.IsCorner) ? infoModelUp.L1 : 0) + Cover + ds + d / 2 - d + (i - k) * deltaX);
                }
                else
                {
                    if (k == nx - 1)
                    {
                        x0.Add(infoModelUp.WestPosition + ((infoModelUp.IsCorner) ? infoModelUp.L1 : 0) + Cover + ds + d / 2 + d + (i - k) * deltaX);
                    }
                    else
                    {
                        x0.Add(infoModelUp.WestPosition + ((infoModelUp.IsCorner) ? infoModelUp.L1 : 0) + Cover + ds + d / 2 + d + (i - k) * deltaX);
                    }
                }
                k++;
            }
            k -= 1;
            int l = 1;
            while (l < ny - 1)
            {
                x0.Add(infoModelUp.WestPosition + ((infoModelUp.IsCorner) ? infoModelUp.L1 : 0) + Cover + ds + d / 2 + (i - k) * deltaX);
                l++;
            }
            return x0;
        }
        private ObservableCollection<double> GetNextY0Bar(InfoModel infoModelUp, double Cover, double ds, double d)
        {
            ObservableCollection<double> y0 = new ObservableCollection<double>();

            double deltaY = (infoModelUp.T - 2 * Cover - 2 * ds - d) / (ny - 1);
            int i = 0;
            while (i < nx)
            {
                y0.Add(infoModelUp.SouthPosition + Cover + ds + d / 2);
                i++;
            }
            i -= 1;
            int j = 1;
            while (j < ny - 1)
            {
                y0.Add(infoModelUp.SouthPosition + Cover + ds + d / 2 + j * deltaY + d);
                j++;
            }
            int k = 0;
            while (k < nx)
            {
                y0.Add(infoModelUp.SouthPosition + Cover + ds + d / 2 + j * deltaY);
                k++;
            }
            k -= 1;
            int l = 1;
            while (l < ny - 1)
            {
                y0.Add(infoModelUp.SouthPosition + Cover + ds + d / 2 + (j - l) * deltaY + d);
                l++;
            }
            return y0;
        }
        public void GetLocationBarModels(InfoModel infoModel, double Cover, double ds0, double dsUp, InfoModel infoModelUp = null, BarMainModel barMainModelUp = null)
        {
            double ds = ds0;
            RefreshX0Y0BarModels(infoModel, Cover, ds);
            for (int i = 0; i < 2*nx+2*(ny-2); i++)
            {
                double x01 = 0, y01 = 0;
                x01 = BarModels[i].X0;
                y01 = BarModels[i].Y0;
                #region
                if (infoModelUp == null)
                {
                    x01 = BarModels[i].X0;
                    y01 = BarModels[i].Y0;
                }
                else
                {
                    bool condition = BarModels[i].IsTopDowels && BarModels[i].TopDowels == 0;
                    if (condition)
                    {
                        ds = dsUp;
                        if (BarModels[i].BarNumber <= nx)
                        {
                            ObservableCollection<BarModel> barModelSouth = new ObservableCollection<BarModel>(BarModels.Where(x => x.BarNumber <= nx && x.IsTopDowels && x.TopDowels == 0).OrderBy(x => x.BarNumber).ToList());
                            if (barModelSouth.Count != 0)
                            {
                                y01 = (infoModelUp.SouthPosition + Cover + ds + Bar.Diameter / 2);
                                if (barModelSouth.Count == 1)
                                {
                                    x01 = GetNextX0Bar(infoModelUp, Cover, ds, Bar.Diameter)[BarModels[i].BarNumber - 1];
                                }
                                else
                                {
                                    double deltaX = (((infoModelUp.IsCorner) ? (infoModelUp.L2) : (infoModelUp.L)) - 2 * Cover - 2 * ds - Bar.Diameter) / (barModelSouth.Count - 1);
                                    int index = barModelSouth.IndexOf(barModelSouth.Where(x => x.BarNumber == BarModels[i].BarNumber).FirstOrDefault());
                                    x01 = infoModelUp.WestPosition + ((infoModelUp.IsCorner) ? (infoModelUp.L1) : (0)) + Cover + ds + Bar.Diameter / 2 + ((index == barModelSouth.Count - 1) ? (-Bar.Diameter) : (Bar.Diameter)) + index * deltaX;
                                }
                            }
                            else
                            {
                                x01 = BarModels[i].X0;
                                y01 = BarModels[i].Y0;
                            }
                        }
                        if (BarModels[i].BarNumber > nx && BarModels[i].BarNumber <= nx + ny - 2)
                        {
                            ObservableCollection<BarModel> barModelEast = new ObservableCollection<BarModel>(BarModels.Where(x => (x.BarNumber > nx && x.BarNumber <= nx + ny - 2) && x.IsTopDowels && x.TopDowels == 0).OrderBy(x => x.BarNumber).ToList());
                            if (barModelEast.Count != 0)
                            {
                                x01 = infoModelUp.EastPosition - ((infoModelUp.IsCorner) ? (infoModelUp.L1) : (0)) - Cover - ds - Bar.Diameter / 2;
                                double deltaY = (infoModelUp.T - 2 * Cover - 2 * ds - Bar.Diameter) / (barModelEast.Count + 1);
                                int index = barModelEast.IndexOf(barModelEast.Where(x => x.BarNumber == BarModels[i].BarNumber).FirstOrDefault());
                                y01 = infoModelUp.SouthPosition + Cover + ds + Bar.Diameter / 2 + (index + 1) * deltaY - Bar.Diameter;
                            }
                            else
                            {
                                x01 = BarModels[i].X0;
                                y01 = BarModels[i].Y0;
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
                                    x01 = GetNextX0Bar(infoModelUp, Cover, ds, Bar.Diameter)[BarModels[i].BarNumber - 1];
                                }
                                else
                                {
                                    double deltaX = (((infoModelUp.IsCorner) ? (infoModelUp.L2) : (infoModelUp.L)) - 2 * Cover - 2 * ds - Bar.Diameter) / (barModelNouth.Count - 1);
                                    int index = barModelNouth.IndexOf(barModelNouth.Where(x => x.BarNumber == BarModels[i].BarNumber).FirstOrDefault());
                                    x01 = infoModelUp.EastPosition - ((infoModelUp.IsCorner) ? (infoModelUp.L1) : (0)) - Cover - ds - Bar.Diameter / 2 + ((index == barModelNouth.Count - 1) ? (Bar.Diameter) : (-Bar.Diameter)) - index * deltaX;
                                }
                            }
                            else
                            {
                                x01 = BarModels[i].X0;
                                y01 = BarModels[i].Y0;
                            }
                        }
                        if (BarModels[i].BarNumber > 2 * nx + ny - 2)
                        {
                            ObservableCollection<BarModel> barModelWest = new ObservableCollection<BarModel>(BarModels.Where(x => (x.BarNumber > 2 * nx + ny - 2) && x.IsTopDowels && x.TopDowels == 0).OrderBy(x => x.BarNumber).ToList());
                            if (barModelWest.Count != 0)
                            {
                                x01 = infoModelUp.WestPosition + ((infoModelUp.IsCorner) ? (infoModelUp.L1) : (0)) + Cover + ds + Bar.Diameter / 2;
                                double deltaY = (infoModelUp.T - 2 * Cover - 2 * ds - Bar.Diameter) / (barModelWest.Count + 1);
                                int index = barModelWest.IndexOf(barModelWest.Where(x => x.BarNumber == BarModels[i].BarNumber).FirstOrDefault());
                                y01 = infoModelUp.NouthPosition - Cover - ds - Bar.Diameter / 2 - (index + 1) * deltaY - Bar.Diameter;
                            }
                            else
                            {
                                x01 = BarModels[i].X0;
                                y01 = BarModels[i].Y0;
                            }
                        }

                    }
                    else
                    {
                        ds = ds0;
                        x01 = GetNextX0Bar(infoModelUp, Cover, ds, Bar.Diameter)[BarModels[i].BarNumber - 1];
                        y01 = GetNextY0Bar(infoModelUp, Cover, ds, Bar.Diameter)[BarModels[i].BarNumber - 1];
                    }
                    
                    #region
                    //if (infoModelUp.IsCorner)
                    //{
                    //    if (BarModels[i].BarNumber <= nx)
                    //    {
                    //        ObservableCollection<BarModel> barModelSouth = new ObservableCollection<BarModel>(BarModels.Where(x => x.BarNumber <= nx && x.IsTopDowels && x.TopDowels == 0).OrderBy(x => x.BarNumber).ToList());
                    //        if (barModelSouth.Count != 0)
                    //        {
                    //            y01 = (infoModelUp.SouthPosition + Cover + ds + BarModels[i].Bar.Diameter / 2);
                    //            if (barModelSouth.Count == 1)
                    //            {
                    //                x01 = GetNextX0Bar(infoModelUp, Cover, ds, BarModels[i].Bar.Diameter)[BarModels[i].BarNumber - 1];
                    //            }
                    //            else
                    //            {
                    //                double deltaX = (infoModelUp.L2 - 2 * Cover - 2 * ds - BarModels[i].Bar.Diameter) / (barModelSouth.Count - 1);
                    //                int index = barModelSouth.IndexOf(barModelSouth.Where(x => x.BarNumber == BarModels[i].BarNumber).FirstOrDefault());
                    //                x01 = infoModelUp.WestPosition + infoModelUp.L1 + Cover + ds + BarModels[i].Bar.Diameter / 2 + ((index == barModelSouth.Count - 1) ? (-BarModels[i].Bar.Diameter) : (BarModels[i].Bar.Diameter)) + index * deltaX;
                    //            }
                    //        }
                    //    }
                    //    if (BarModels[i].BarNumber > nx && BarModels[i].BarNumber <= nx + ny - 2)
                    //    {
                    //        ObservableCollection<BarModel> barModelEast = new ObservableCollection<BarModel>(BarModels.Where(x => (x.BarNumber > nx && x.BarNumber <= nx + ny - 2) && x.IsTopDowels && x.TopDowels == 0).OrderBy(x => x.BarNumber).ToList());
                    //        if (barModelEast.Count != 0)
                    //        {
                    //            x01 = infoModelUp.EastPosition - Cover - ds - BarModels[i].Bar.Diameter / 2;
                    //            double deltaY = (infoModelUp.T - 2 * Cover - 2 * ds - BarModels[i].Bar.Diameter) / (barModelEast.Count + 1);
                    //            int index = barModelEast.IndexOf(barModelEast.Where(x => x.BarNumber == BarModels[i].BarNumber).FirstOrDefault());
                    //            y01 = infoModelUp.SouthPosition + Cover + ds + BarModels[i].Bar.Diameter / 2 + (index + 1) * deltaY - BarModels[i].Bar.Diameter;
                    //        }
                    //    }
                    //    if (BarModels[i].BarNumber > nx + ny - 2 && BarModels[i].BarNumber <= 2 * nx + ny - 2)
                    //    {
                    //        ObservableCollection<BarModel> barModelNouth = new ObservableCollection<BarModel>(BarModels.Where(x => (x.BarNumber > nx + ny - 2 && x.BarNumber <= 2 * nx + ny - 2) && x.IsTopDowels && x.TopDowels == 0).OrderBy(x => x.BarNumber).ToList());
                    //        if (barModelNouth.Count != 0)
                    //        {
                    //            y01 = (infoModelUp.NouthPosition - Cover - ds - BarModels[i].Bar.Diameter / 2);
                    //            if (barModelNouth.Count == 1)
                    //            {
                    //                x01 = GetNextX0Bar(infoModelUp, Cover, ds, BarModels[i].Bar.Diameter)[BarModels[i].BarNumber - 1];
                    //            }
                    //            else
                    //            {
                    //                double deltaX = (infoModelUp.L2 - 2 * Cover - 2 * ds - BarModels[i].Bar.Diameter) / (barModelNouth.Count - 1);
                    //                int index = barModelNouth.IndexOf(barModelNouth.Where(x => x.BarNumber == BarModels[i].BarNumber).FirstOrDefault());
                    //                x01 = infoModelUp.EastPosition - infoModelUp.L1 - Cover - ds - BarModels[i].Bar.Diameter / 2 + ((index == barModelNouth.Count - 1) ? (BarModels[i].Bar.Diameter) : (-BarModels[i].Bar.Diameter)) - index * deltaX;
                    //            }
                    //        }
                    //    }
                    //    if (BarModels[i].BarNumber > 2 * nx + ny - 2)
                    //    {
                    //        ObservableCollection<BarModel> barModelWest = new ObservableCollection<BarModel>(BarModels.Where(x => (x.BarNumber > 2 * nx + ny - 2) && x.IsTopDowels && x.TopDowels == 0).OrderBy(x => x.BarNumber).ToList());
                    //        if (barModelWest.Count != 0)
                    //        {
                    //            x01 = infoModelUp.WestPosition + Cover + ds + BarModels[i].Bar.Diameter / 2;
                    //            double deltaY = (infoModelUp.T - 2 * Cover - 2 * ds - BarModels[i].Bar.Diameter) / (barModelWest.Count + 1);
                    //            int index = barModelWest.IndexOf(barModelWest.Where(x => x.BarNumber == BarModels[i].BarNumber).FirstOrDefault());
                    //            y01 = infoModelUp.NouthPosition - Cover - ds - BarModels[i].Bar.Diameter / 2 - (index + 1) * deltaY - BarModels[i].Bar.Diameter;
                    //        }
                    //    }
                    //}
                    //else
                    //{
                    //    if (BarModels[i].BarNumber <= nx)
                    //    {
                    //        ObservableCollection<BarModel> barModelSouth = new ObservableCollection<BarModel>(BarModels.Where(x => x.BarNumber <= nx && x.IsTopDowels && x.TopDowels == 0).OrderBy(x => x.BarNumber).ToList());
                    //        if (barModelSouth.Count != 0)
                    //        {

                    //            y01 = (infoModelUp.SouthPosition + Cover + ds + BarModels[i].Bar.Diameter / 2);
                    //            if (barModelSouth.Count == 1)
                    //            {
                    //                x01 = GetNextX0Bar(infoModelUp, Cover, ds, BarModels[i].Bar.Diameter)[BarModels[i].BarNumber - 1];
                    //            }
                    //            else
                    //            {
                    //                double deltaX = (infoModelUp.L - 2 * Cover - 2 * ds - BarModels[i].Bar.Diameter) / (barModelSouth.Count - 1);
                    //                int index = barModelSouth.IndexOf(barModelSouth.Where(x => x.BarNumber == BarModels[i].BarNumber).FirstOrDefault());
                    //                x01 = infoModelUp.WestPosition + Cover + ds + BarModels[i].Bar.Diameter / 2 + ((index == barModelSouth.Count - 1) ? (-BarModels[i].Bar.Diameter) : (BarModels[i].Bar.Diameter)) + index * deltaX;
                    //            }
                    //        }
                    //    }
                    //    if (BarModels[i].BarNumber > nx && BarModels[i].BarNumber <= nx + ny - 2)
                    //    {
                    //        ObservableCollection<BarModel> barModelEast = new ObservableCollection<BarModel>(BarModels.Where(x => (x.BarNumber > nx && x.BarNumber <= nx + ny - 2) && x.IsTopDowels && x.TopDowels == 0).OrderBy(x => x.BarNumber).ToList());
                    //        if (barModelEast.Count != 0)
                    //        {
                    //            x01 = infoModelUp.EastPosition - Cover - ds - BarModels[i].Bar.Diameter / 2;
                    //            double deltaY = (infoModelUp.T - 2 * Cover - 2 * ds - BarModels[i].Bar.Diameter) / (barModelEast.Count + 1);
                    //            int index = barModelEast.IndexOf(barModelEast.Where(x => x.BarNumber == BarModels[i].BarNumber).FirstOrDefault());
                    //            y01 = infoModelUp.SouthPosition + Cover + ds + BarModels[i].Bar.Diameter / 2 + (index + 1) * deltaY - BarModels[i].Bar.Diameter;
                    //        }
                    //    }
                    //    if (BarModels[i].BarNumber > nx + ny - 2 && BarModels[i].BarNumber <= 2 * nx + ny - 2)
                    //    {
                    //        ObservableCollection<BarModel> barModelNouth = new ObservableCollection<BarModel>(BarModels.Where(x => (x.BarNumber > nx + ny - 2 && x.BarNumber <= 2 * nx + ny - 2) && x.IsTopDowels && x.TopDowels == 0).OrderBy(x => x.BarNumber).ToList());
                    //        if (barModelNouth.Count != 0)
                    //        {
                    //            y01 = (infoModelUp.NouthPosition - Cover - ds - BarModels[i].Bar.Diameter / 2);
                    //            if (barModelNouth.Count == 1)
                    //            {
                    //                x01 = GetNextX0Bar(infoModelUp, Cover, ds, BarModels[i].Bar.Diameter)[BarModels[i].BarNumber - 1];
                    //            }
                    //            else
                    //            {
                    //                double deltaX = (infoModelUp.L - 2 * Cover - 2 * ds - BarModels[i].Bar.Diameter) / (barModelNouth.Count - 1);
                    //                int index = barModelNouth.IndexOf(barModelNouth.Where(x => x.BarNumber == BarModels[i].BarNumber).FirstOrDefault());
                    //                x01 = infoModelUp.EastPosition - Cover - ds - BarModels[i].Bar.Diameter / 2 + ((index == barModelNouth.Count - 1) ? (BarModels[i].Bar.Diameter) : (-BarModels[i].Bar.Diameter)) - index * deltaX;
                    //            }
                    //        }
                    //    }
                    //    if (BarModels[i].BarNumber > 2 * nx + ny - 2)
                    //    {
                    //        ObservableCollection<BarModel> barModelWest = new ObservableCollection<BarModel>(BarModels.Where(x => (x.BarNumber > 2 * nx + ny - 2) && x.IsTopDowels && x.TopDowels == 0).OrderBy(x => x.BarNumber).ToList());
                    //        if (barModelWest.Count != 0)
                    //        {
                    //            x01 = infoModelUp.WestPosition + Cover + ds + BarModels[i].Bar.Diameter / 2;
                    //            double deltaY = (infoModelUp.T - 2 * Cover - 2 * ds - BarModels[i].Bar.Diameter) / (barModelWest.Count + 1);
                    //            int index = barModelWest.IndexOf(barModelWest.Where(x => x.BarNumber == BarModels[i].BarNumber).FirstOrDefault());
                    //            y01 = infoModelUp.NouthPosition - Cover - ds - BarModels[i].Bar.Diameter / 2 - (index + 1) * deltaY - BarModels[i].Bar.Diameter;
                    //        }
                    //    }
                    //}
                    #endregion
                }
                #endregion

                BarModels[i].GetDowelsLocationBar(infoModel, 2 * nx + 2 * (ny - 2), nx, ny, Cover, SplitOverlap, Overlap, x01, y01);
            }
        }

        #endregion
        #region BarCornerModel
        public void GetBarCornerModels(InfoModel infoModel, double Cover, double ds)
        {
            if (BarCornerModels.Count != 0)
            {
                BarCornerModels.Clear();
            }
            double deltaX = (infoModel.L1 - 2 * Cover - 2 * ds - BarCorner.Diameter) / (nxCorner - 1);
            double deltaY = (infoModel.T - 2 * Cover - 2 * ds - BarCorner.Diameter) / (nyCorner - 1);
            #region Left
            int i = 0;
            while (i < nxCorner)
            {
                double x0 = infoModel.WestPosition + Cover + ds + BarCorner.Diameter / 2 + i * deltaX;
                double y0 = infoModel.SouthPosition + Cover + ds + BarCorner.Diameter / 2;
                var a = new BarModel(i + 1, BarCorner, SplitOverlap, Overlap, true);
                a.GetX0Y0(x0, y0);
                BarCornerModels.Add(a);
                i++;
            }
            i -= 1;
            int j = 1;
            while (j < nyCorner - 1)
            {
                double x0 = infoModel.WestPosition + Cover + ds + BarCorner.Diameter / 2 + i * deltaX;
                double y0 = infoModel.SouthPosition + Cover + ds + BarCorner.Diameter / 2 + j * deltaY;
                var a = new BarModel(i + j + 1, BarCorner, SplitOverlap, Overlap, true);
                a.GetX0Y0(x0, y0);
                BarCornerModels.Add(a);
                j++;
            }
            int k = 0;
            while (k < nxCorner)
            {
                double x0 = infoModel.WestPosition + Cover + ds + BarCorner.Diameter / 2 + (i - k) * deltaX;
                double y0 = infoModel.SouthPosition + Cover + ds + BarCorner.Diameter / 2 + j * deltaY;
                var a = new BarModel(i + j + k + 1, BarCorner, SplitOverlap, Overlap, true);
                a.GetX0Y0(x0, y0);
                BarCornerModels.Add(a);
                k++;
            }
            k -= 1;
            int l = 1;
            while (l < nyCorner - 1)
            {
                double x0 = infoModel.WestPosition + Cover + ds + BarCorner.Diameter / 2 + (i - k) * deltaX;
                double y0 = infoModel.SouthPosition + Cover + ds + BarCorner.Diameter / 2 + (j - l) * deltaY;
                var a = new BarModel(i + j + k + l + 1, BarCorner, SplitOverlap, Overlap, true);
                a.GetX0Y0(x0, y0);
                BarCornerModels.Add(a);
                l++;
            }
            #endregion
            #region right
            int number = i + j + k + l;
            int i1 = 0;
            while (i1 < nxCorner)
            {
                double x0 = infoModel.WestPosition + infoModel.L1 + infoModel.L2 + Cover + ds + BarCorner.Diameter / 2 + i1 * deltaX;
                double y0 = infoModel.SouthPosition + Cover + ds + BarCorner.Diameter / 2;
                var a = new BarModel(i1 + 1 + number, BarCorner, SplitOverlap, Overlap, true);
                a.GetX0Y0(x0, y0);
                BarCornerModels.Add(a);
                i1++;
            }
            i1 -= 1;
            int j1 = 1;
            while (j1 < nyCorner - 1)
            {
                double x0 = infoModel.WestPosition + infoModel.L1 + infoModel.L2 + Cover + ds + BarCorner.Diameter / 2 + i1 * deltaX;
                double y0 = infoModel.SouthPosition + Cover + ds + BarCorner.Diameter / 2 + j1 * deltaY;
                var a = new BarModel(i1 + j1 + 1 + number, BarCorner, SplitOverlap, Overlap, true);
                a.GetX0Y0(x0, y0);
                BarCornerModels.Add(a);
                j1++;
            }
            int k1 = 0;
            while (k1 < nxCorner)
            {
                double x0 = infoModel.WestPosition + infoModel.L1 + infoModel.L2 + Cover + ds + BarCorner.Diameter / 2 + (i1 - k1) * deltaX;
                double y0 = infoModel.SouthPosition + Cover + ds + BarCorner.Diameter / 2 + j1 * deltaY;
                var a = new BarModel(i1 + j1 + k1 + 1 + number, BarCorner, SplitOverlap, Overlap, true);
                a.GetX0Y0(x0, y0);
                BarCornerModels.Add(a);
                k1++;
            }
            k1 -= 1;
            int l1 = 1;
            while (l1 < nyCorner - 1)
            {
                double x0 = infoModel.WestPosition + infoModel.L1 + infoModel.L2 + Cover + ds + BarCorner.Diameter / 2 + (i1 - k1) * deltaX;
                double y0 = infoModel.SouthPosition + Cover + ds + BarCorner.Diameter / 2 + (j1 - l1) * deltaY;
                var a = new BarModel(i1 + j1 + k1 + l1 + 1 + number, BarCorner, SplitOverlap, Overlap, true);
                a.GetX0Y0(x0, y0);
                BarCornerModels.Add(a);
                l1++;
            }
            #endregion
        }
        public void RefreshX0Y0BarCornerModels(InfoModel infoModel, double Cover, double ds)
        {

            double deltaX = (infoModel.L1 - 2 * Cover - 2 * ds - BarCorner.Diameter) / (nxCorner - 1);
            double deltaY = (infoModel.T - 2 * Cover - 2 * ds - BarCorner.Diameter) / (nyCorner - 1);
            #region Left
            int i = 0;
            while (i < nxCorner)
            {
                double x0 = infoModel.WestPosition + Cover + ds + BarCorner.Diameter / 2 + i * deltaX;
                double y0 = infoModel.SouthPosition + Cover + ds + BarCorner.Diameter / 2;

                BarCornerModels[i].GetX0Y0(x0, y0);
                i++;
            }
            i -= 1;
            int j = 1;
            while (j < nyCorner - 1)
            {
                double x0 = infoModel.WestPosition + Cover + ds + BarCorner.Diameter / 2 + i * deltaX;
                double y0 = infoModel.SouthPosition + Cover + ds + BarCorner.Diameter / 2 + j * deltaY;

                BarCornerModels[i + j].GetX0Y0(x0, y0);
                j++;
            }
            int k = 0;
            while (k < nxCorner)
            {
                double x0 = infoModel.WestPosition + Cover + ds + BarCorner.Diameter / 2 + (i - k) * deltaX;
                double y0 = infoModel.SouthPosition + Cover + ds + BarCorner.Diameter / 2 + j * deltaY;

                BarCornerModels[i + j + k].GetX0Y0(x0, y0);
                k++;
            }
            k -= 1;
            int l = 1;
            while (l < nyCorner - 1)
            {
                double x0 = infoModel.WestPosition + Cover + ds + BarCorner.Diameter / 2 + (i - k) * deltaX;
                double y0 = infoModel.SouthPosition + Cover + ds + BarCorner.Diameter / 2 + (j - l) * deltaY;

                BarCornerModels[i + j + k + l].GetX0Y0(x0, y0);
                l++;
            }
            #endregion
            #region right
            int number = i + j + k + l;
            int i1 = 0;
            while (i1 < nxCorner)
            {
                double x0 = infoModel.WestPosition + infoModel.L1 + infoModel.L2 + Cover + ds + BarCorner.Diameter / 2 + i1 * deltaX;
                double y0 = infoModel.SouthPosition + Cover + ds + BarCorner.Diameter / 2;

                BarCornerModels[i1 + number].GetX0Y0(x0, y0);
                i1++;
            }
            i1 -= 1;
            int j1 = 1;
            while (j1 < nyCorner - 1)
            {
                double x0 = infoModel.WestPosition + infoModel.L1 + infoModel.L2 + Cover + ds + BarCorner.Diameter / 2 + i1 * deltaX;
                double y0 = infoModel.SouthPosition + Cover + ds + BarCorner.Diameter / 2 + j1 * deltaY;

                BarCornerModels[i1 + j1 + number].GetX0Y0(x0, y0);
                j1++;
            }
            int k1 = 0;
            while (k1 < nxCorner)
            {
                double x0 = infoModel.WestPosition + infoModel.L1 + infoModel.L2 + Cover + ds + BarCorner.Diameter / 2 + (i1 - k1) * deltaX;
                double y0 = infoModel.SouthPosition + Cover + ds + BarCorner.Diameter / 2 + j1 * deltaY;

                BarCornerModels[i1 + j1 + k1 + number].GetX0Y0(x0, y0);
                k1++;
            }
            k1 -= 1;
            int l1 = 1;
            while (l1 < nyCorner - 1)
            {
                double x0 = infoModel.WestPosition + infoModel.L1 + infoModel.L2 + Cover + ds + BarCorner.Diameter / 2 + (i1 - k1) * deltaX;
                double y0 = infoModel.SouthPosition + Cover + ds + BarCorner.Diameter / 2 + (j1 - l1) * deltaY;

                BarCornerModels[i1 + j1 + k1 + l1 + number].GetX0Y0(x0, y0);
                l1++;
            }
            #endregion
        }
        private ObservableCollection<double> GetNextX0BarCorner(InfoModel infoModel, InfoModel infoModelUp, double Cover, double ds, double d)
        {
            ObservableCollection<double> x0 = new ObservableCollection<double>();
            if (!infoModelUp.IsCorner)
            {
                double deltaX = (infoModel.L1 - 2 * Cover - 2 * ds - BarCorner.Diameter) / (nxCorner - 1);
                #region Left
                int i = 0;
                while (i < nxCorner)
                {
                    x0.Add(infoModelUp.WestPosition + Cover + ds + BarCorner.Diameter / 2 + ((i != nxCorner - 1) ? (BarCorner.Diameter) : (-BarCorner.Diameter)) + i * deltaX);
                    i++;
                }
                i -= 1;
                int j = 1;
                while (j < nyCorner - 1)
                {
                    x0.Add(infoModelUp.WestPosition + Cover + ds + BarCorner.Diameter / 2 + i * deltaX);
                    j++;
                }
                int k = 0;
                while (k < nxCorner)
                {
                    x0.Add(infoModelUp.WestPosition + Cover + ds + BarCorner.Diameter / 2 + ((k == 0) ? (-BarCorner.Diameter) : (BarCorner.Diameter)) + (i - k) * deltaX);
                    k++;
                }
                k -= 1;
                int l = 1;
                while (l < nyCorner - 1)
                {
                    x0.Add(infoModelUp.WestPosition + Cover + ds + BarCorner.Diameter / 2 + (i - k) * deltaX);
                    l++;
                }
                #endregion
                #region right
                int number = i + j + k + l;
                int i1 = 0;
                while (i1 < nxCorner)
                {
                    x0.Add(infoModelUp.EastPosition - (infoModel.L1) + Cover + ds + BarCorner.Diameter / 2 + ((i1 != nxCorner - 1) ? (BarCorner.Diameter) : (-BarCorner.Diameter)) + i1 * deltaX);
                    i1++;
                }
                i1 -= 1;
                int j1 = 1;
                while (j1 < nyCorner - 1)
                {
                    x0.Add(infoModelUp.EastPosition - (infoModel.L1) + Cover + ds + BarCorner.Diameter / 2 + i1 * deltaX);
                    j1++;
                }
                int k1 = 0;
                while (k1 < nxCorner)
                {
                    x0.Add(infoModelUp.EastPosition - (infoModel.L1) + Cover + ds + BarCorner.Diameter / 2 + ((k1 == 0) ? (-BarCorner.Diameter) : (BarCorner.Diameter)) + (i1 - k1) * deltaX);
                    k1++;
                }
                k1 -= 1;
                int l1 = 1;
                while (l1 < nyCorner - 1)
                {
                    x0.Add(infoModelUp.EastPosition - (infoModel.L1) + Cover + ds + BarCorner.Diameter / 2 + (i1 - k1) * deltaX);
                    l1++;
                }
                #endregion
            }
            else
            {
                double deltaX = (infoModelUp.L1 - 2 * Cover - 2 * ds - BarCorner.Diameter) / (nxCorner - 1);
                #region Left
                int i = 0;
                while (i < nxCorner)
                {
                    x0.Add(infoModelUp.WestPosition + Cover + ds + BarCorner.Diameter / 2 + ((i != nxCorner - 1) ? (BarCorner.Diameter) : (-BarCorner.Diameter)) + i * deltaX);
                    i++;
                }
                i -= 1;
                int j = 1;
                while (j < nyCorner - 1)
                {
                    x0.Add(infoModelUp.WestPosition + Cover + ds + BarCorner.Diameter / 2 + i * deltaX);
                    j++;
                }
                int k = 0;
                while (k < nxCorner)
                {
                    x0.Add(infoModelUp.WestPosition + Cover + ds + BarCorner.Diameter / 2 + ((k == 0) ? (-BarCorner.Diameter) : (BarCorner.Diameter)) + (i - k) * deltaX);
                    k++;
                }
                k -= 1;
                int l = 1;
                while (l < nyCorner - 1)
                {
                    x0.Add(infoModelUp.WestPosition + Cover + ds + BarCorner.Diameter / 2 + (i - k) * deltaX);
                    l++;
                }
                #endregion
                #region right
                int number = i + j + k + l;
                int i1 = 0;
                while (i1 < nxCorner)
                {
                    x0.Add(infoModelUp.WestPosition + infoModelUp.L1 + infoModelUp.L2 + Cover + ds + BarCorner.Diameter / 2 + ((i1 != nxCorner - 1) ? (BarCorner.Diameter) : (-BarCorner.Diameter)) + i1 * deltaX);
                    i1++;
                }
                i1 -= 1;
                int j1 = 1;
                while (j1 < nyCorner - 1)
                {
                    x0.Add(infoModelUp.WestPosition + infoModelUp.L1 + infoModelUp.L2 + Cover + ds + BarCorner.Diameter / 2 + i1 * deltaX);
                    j1++;
                }
                int k1 = 0;
                while (k1 < nxCorner)
                {
                    x0.Add(infoModelUp.WestPosition + infoModelUp.L1 + infoModelUp.L2 + Cover + ds + BarCorner.Diameter / 2 + ((k1 == 0) ? (-BarCorner.Diameter) : (BarCorner.Diameter)) + (i1 - k1) * deltaX);
                    k1++;
                }
                k1 -= 1;
                int l1 = 1;
                while (l1 < nyCorner - 1)
                {
                    x0.Add(infoModelUp.WestPosition + infoModelUp.L1 + infoModelUp.L2 + Cover + ds + BarCorner.Diameter / 2 + (i1 - k1) * deltaX);
                    l1++;
                }
                #endregion
            }

            return x0;
        }
        public ObservableCollection<double> GetNextY0BarCorner(InfoModel infoModelUp, double Cover, double ds)
        {
            ObservableCollection<double> y0 = new ObservableCollection<double>();
            double deltaY = (infoModelUp.T - 2 * Cover - 2 * ds - BarCorner.Diameter) / (nyCorner - 1);
            #region Left
            int i = 0;
            while (i < nxCorner)
            {
                y0.Add(infoModelUp.SouthPosition + Cover + ds + BarCorner.Diameter / 2);
                i++;
            }
            i -= 1;
            int j = 1;
            while (j < nyCorner - 1)
            {
                y0.Add(infoModelUp.SouthPosition + Cover + ds + BarCorner.Diameter / 2 + j * deltaY + BarCorner.Diameter);
                j++;
            }
            int k = 0;
            while (k < nxCorner)
            {
                y0.Add(infoModelUp.SouthPosition + Cover + ds + BarCorner.Diameter / 2 + j * deltaY);
                k++;
            }
            k -= 1;
            int l = 1;
            while (l < nyCorner - 1)
            {
                y0.Add(infoModelUp.SouthPosition + Cover + ds + BarCorner.Diameter / 2 + (j - l) * deltaY + BarCorner.Diameter);
                l++;
            }
            #endregion
            #region right
            int number = i + j + k + l;
            int i1 = 0;
            while (i1 < nxCorner)
            {
                y0.Add(infoModelUp.SouthPosition + Cover + ds + BarCorner.Diameter / 2);
                i1++;
            }
            i1 -= 1;
            int j1 = 1;
            while (j1 < nyCorner - 1)
            {
                y0.Add(infoModelUp.SouthPosition + Cover + ds + BarCorner.Diameter / 2 + j1 * deltaY + BarCorner.Diameter);
                j1++;
            }
            int k1 = 0;
            while (k1 < nxCorner)
            {
                y0.Add(infoModelUp.SouthPosition + Cover + ds + BarCorner.Diameter / 2 + j1 * deltaY);
                k1++;
            }
            k1 -= 1;
            int l1 = 1;
            while (l1 < nyCorner - 1)
            {
                y0.Add(infoModelUp.SouthPosition + Cover + ds + BarCorner.Diameter / 2 + (j1 - l1) * deltaY + BarCorner.Diameter);
                l1++;
            }
            #endregion
            return y0;
        }
        public void GetLocationBarCornerModels(InfoModel infoModel, double Cover, double ds0,double dsUp, InfoModel infoModelUp = null)
        {
            double ds = ds0;
            RefreshX0Y0BarCornerModels(infoModel, Cover, ds);
            for (int i = 0; i < BarCornerModels.Count; i++)
            {
                double x01 = BarCornerModels[i].X0;
                double y01 = BarCornerModels[i].Y0;
                #region
                if (infoModelUp == null)
                {
                    x01 = BarCornerModels[i].X0; y01 = BarCornerModels[i].Y0;
                }
                else
                {
                    if (BarCornerModels[i].IsTopDowels&&BarCornerModels[i].TopDowels==0)
                    {
                        ds = dsUp;
                        #region Left
                        if (BarCornerModels[i].BarNumber <= nxCorner)
                        {

                            ObservableCollection<BarModel> barModelSouth = new ObservableCollection<BarModel>(BarCornerModels.Where(x => x.BarNumber <= nxCorner && x.IsTopDowels && x.TopDowels == 0).OrderBy(x => x.BarNumber).ToList());
                            if (barModelSouth.Count != 0)
                            {
                                y01 = (infoModelUp.SouthPosition + Cover + ds + BarCorner.Diameter / 2);
                                if (barModelSouth.Count == 1)
                                {
                                    x01 = GetNextX0BarCorner(infoModel, infoModelUp, Cover, ds, BarCorner.Diameter)[BarCornerModels[i].BarNumber - 1];
                                }
                                else
                                {
                                    double deltaX = (((infoModelUp.IsCorner) ? (infoModelUp.L1) : (infoModel.L1)) - 2 * Cover - 2 * ds - BarCorner.Diameter) / (barModelSouth.Count - 1);
                                    int index = barModelSouth.IndexOf(barModelSouth.Where(x => x.BarNumber == BarCornerModels[i].BarNumber).FirstOrDefault());
                                    x01 = infoModelUp.WestPosition + Cover + ds + BarCorner.Diameter / 2 + ((index == barModelSouth.Count - 1) ? (-BarCorner.Diameter) : (BarCorner.Diameter)) + index * deltaX;
                                }
                            }
                            else
                            {
                                x01 = BarCornerModels[i].X0; y01 = BarCornerModels[i].Y0;
                            }

                        }
                        if (BarCornerModels[i].BarNumber > nxCorner && BarCornerModels[i].BarNumber <= nxCorner + nyCorner - 2)
                        {
                            ObservableCollection<BarModel> barModelEast = new ObservableCollection<BarModel>(BarCornerModels.Where(x => (x.BarNumber > nxCorner && x.BarNumber <= nxCorner + nyCorner - 2) && x.IsTopDowels && x.TopDowels == 0).OrderBy(x => x.BarNumber).ToList());
                            if (barModelEast.Count != 0)
                            {
                                x01 = infoModelUp.WestPosition + ((infoModelUp.IsCorner) ? (infoModelUp.L1) : (infoModel.L1)) - Cover - ds - BarCorner.Diameter / 2;
                                double deltaY = (infoModelUp.T - 2 * Cover - 2 * ds - BarCorner.Diameter) / (barModelEast.Count + 1);
                                int index = barModelEast.IndexOf(barModelEast.Where(x => x.BarNumber == BarCornerModels[i].BarNumber).FirstOrDefault());
                                y01 = infoModelUp.SouthPosition + Cover + ds + BarCorner.Diameter / 2 + (index + 1) * deltaY - BarCorner.Diameter;
                            }
                            else
                            {
                                x01 = BarCornerModels[i].X0; y01 = BarCornerModels[i].Y0;
                            }
                        }
                        if (BarCornerModels[i].BarNumber > nxCorner + nyCorner - 2 && BarCornerModels[i].BarNumber <= 2 * nxCorner + nyCorner - 2)
                        {
                            ObservableCollection<BarModel> barModelNouth = new ObservableCollection<BarModel>(BarCornerModels.Where(x => (x.BarNumber > nxCorner + nyCorner - 2 && x.BarNumber <= 2 * nxCorner + nyCorner - 2) && x.IsTopDowels && x.TopDowels == 0).OrderBy(x => x.BarNumber).ToList());
                            if (barModelNouth.Count != 0)
                            {
                                y01 = (infoModelUp.NouthPosition - Cover - ds - BarCorner.Diameter / 2);
                                if (barModelNouth.Count == 1)
                                {
                                    x01 = GetNextX0BarCorner(infoModel, infoModelUp, Cover, ds, BarCorner.Diameter)[BarCornerModels[i].BarNumber - 1];
                                }
                                else
                                {
                                    double deltaX = (((infoModelUp.IsCorner) ? (infoModelUp.L1) : (infoModel.L1)) - 2 * Cover - 2 * ds - BarCorner.Diameter) / (barModelNouth.Count - 1);
                                    int index = barModelNouth.IndexOf(barModelNouth.Where(x => x.BarNumber == BarCornerModels[i].BarNumber).FirstOrDefault());
                                    x01 = infoModelUp.WestPosition + ((infoModelUp.IsCorner) ? (infoModelUp.L1) : (infoModel.L1)) - Cover - ds - BarCorner.Diameter / 2 + ((index == barModelNouth.Count - 1) ? (BarCorner.Diameter) : (-BarCorner.Diameter)) - index * deltaX;
                                }
                            }
                            else
                            {
                                x01 = BarCornerModels[i].X0; y01 = BarCornerModels[i].Y0;
                            }
                        }
                        int total = 2 * nxCorner + 2 * (nyCorner - 2);
                        if (BarCornerModels[i].BarNumber > 2 * nxCorner + nyCorner - 2 && BarCornerModels[i].BarNumber <= total)
                        {
                            ObservableCollection<BarModel> barModelWest = new ObservableCollection<BarModel>(BarCornerModels.Where(x => (x.BarNumber > 2 * nxCorner + nyCorner - 2 && x.BarNumber <= total) && x.IsTopDowels && x.TopDowels == 0).OrderBy(x => x.BarNumber).ToList());
                            if (barModelWest.Count != 0)
                            {
                                x01 = infoModelUp.WestPosition + Cover + ds + BarCorner.Diameter / 2;
                                double deltaY = (infoModelUp.T - 2 * Cover - 2 * ds - BarCorner.Diameter) / (barModelWest.Count + 1);
                                int index = barModelWest.IndexOf(barModelWest.Where(x => x.BarNumber == BarCornerModels[i].BarNumber).FirstOrDefault());
                                y01 = infoModelUp.NouthPosition - Cover - ds - BarCorner.Diameter / 2 - (index + 1) * deltaY - BarCorner.Diameter;
                            }
                            else
                            {
                                x01 = BarCornerModels[i].X0; y01 = BarCornerModels[i].Y0;
                            }
                        }
                        #endregion
                        #region right
                        if (BarCornerModels[i].BarNumber > total && BarCornerModels[i].BarNumber <= nxCorner + total)
                        {
                            ObservableCollection<BarModel> barModelSouth = new ObservableCollection<BarModel>(BarCornerModels.Where(x => (x.BarNumber > total && x.BarNumber <= nxCorner + total) && x.IsTopDowels && x.TopDowels == 0).OrderBy(x => x.BarNumber).ToList());
                            if (barModelSouth.Count != 0)
                            {
                                y01 = (infoModelUp.SouthPosition + Cover + ds + BarCorner.Diameter / 2);
                                if (barModelSouth.Count == 1)
                                {
                                    x01 = GetNextX0BarCorner(infoModel, infoModelUp, Cover, ds, BarCorner.Diameter)[BarCornerModels[i].BarNumber - 1];
                                }
                                else
                                {
                                    double deltaX = (((infoModelUp.IsCorner) ? (infoModelUp.L1) : (infoModel.L1)) - 2 * Cover - 2 * ds - BarCorner.Diameter) / (barModelSouth.Count - 1);
                                    int index = barModelSouth.IndexOf(barModelSouth.Where(x => x.BarNumber == BarCornerModels[i].BarNumber).FirstOrDefault());
                                    x01 = infoModelUp.EastPosition - ((infoModelUp.IsCorner) ? (infoModelUp.L1) : (infoModel.L1)) + Cover + ds + BarCorner.Diameter / 2 + ((index == barModelSouth.Count - 1) ? (-BarCorner.Diameter) : (BarCorner.Diameter)) + index * deltaX;
                                }
                            }
                            else
                            {
                                x01 = BarCornerModels[i].X0; y01 = BarCornerModels[i].Y0;
                            }
                        }
                        if (BarCornerModels[i].BarNumber > nxCorner + total && BarCornerModels[i].BarNumber <= nxCorner + nyCorner - 2 + total)
                        {
                            ObservableCollection<BarModel> barModelEast = new ObservableCollection<BarModel>(BarCornerModels.Where(x => (x.BarNumber > nxCorner + total && x.BarNumber <= nxCorner + nyCorner - 2 + total) && x.IsTopDowels && x.TopDowels == 0).OrderBy(x => x.BarNumber).ToList());
                            if (barModelEast.Count != 0)
                            {
                                x01 = infoModelUp.EastPosition - Cover - ds - BarCorner.Diameter / 2;
                                double deltaY = (infoModelUp.T - 2 * Cover - 2 * ds - BarCorner.Diameter) / (barModelEast.Count + 1);
                                int index = barModelEast.IndexOf(barModelEast.Where(x => x.BarNumber == BarCornerModels[i].BarNumber).FirstOrDefault());
                                y01 = infoModelUp.SouthPosition + Cover + ds + BarCorner.Diameter / 2 + (index + 1) * deltaY - BarCorner.Diameter;
                            }
                            else
                            {
                                x01 = BarCornerModels[i].X0; y01 = BarCornerModels[i].Y0;
                            }
                        }
                        if (BarCornerModels[i].BarNumber > nxCorner + nyCorner - 2 + total && BarCornerModels[i].BarNumber <= 2 * nxCorner + nyCorner - 2 + total)
                        {
                            ObservableCollection<BarModel> barModelNouth = new ObservableCollection<BarModel>(BarCornerModels.Where(x => (x.BarNumber > nxCorner + nyCorner - 2 + total && x.BarNumber <= 2 * nxCorner + nyCorner - 2 + total) && x.IsTopDowels && x.TopDowels == 0).OrderBy(x => x.BarNumber).ToList());
                            if (barModelNouth.Count != 0)
                            {
                                y01 = (infoModelUp.NouthPosition - Cover - ds - BarCorner.Diameter / 2);
                                if (barModelNouth.Count == 1)
                                {
                                    x01 = GetNextX0BarCorner(infoModel, infoModelUp, Cover, ds, BarCorner.Diameter)[BarCornerModels[i].BarNumber - 1];
                                }
                                else
                                {
                                    double deltaX = (((infoModelUp.IsCorner) ? (infoModelUp.L1) : (infoModel.L1)) - 2 * Cover - 2 * ds - BarCorner.Diameter) / (barModelNouth.Count - 1);
                                    int index = barModelNouth.IndexOf(barModelNouth.Where(x => x.BarNumber == BarCornerModels[i].BarNumber).FirstOrDefault());
                                    x01 = infoModelUp.EastPosition - Cover - ds - BarCorner.Diameter / 2 + ((index == barModelNouth.Count - 1) ? (BarCorner.Diameter) : (-BarCorner.Diameter)) - index * deltaX;
                                }
                            }
                            else
                            {
                                x01 = BarCornerModels[i].X0; y01 = BarCornerModels[i].Y0;
                            }
                        }
                        if (BarCornerModels[i].BarNumber > 2 * nxCorner + nyCorner - 2 + total && BarCornerModels[i].BarNumber <= total + total)
                        {
                            ObservableCollection<BarModel> barModelWest = new ObservableCollection<BarModel>(BarCornerModels.Where(x => (x.BarNumber > 2 * nxCorner + nyCorner - 2 + total && x.BarNumber <= total + total) && x.IsTopDowels && x.TopDowels == 0).OrderBy(x => x.BarNumber).ToList());
                            if (barModelWest.Count != 0)
                            {
                                x01 = infoModelUp.EastPosition - ((infoModelUp.IsCorner) ? (infoModelUp.L1) : (infoModel.L1)) + Cover + ds + BarCorner.Diameter / 2;
                                double deltaY = (infoModelUp.T - 2 * Cover - 2 * ds - BarCorner.Diameter) / (barModelWest.Count + 1);
                                int index = barModelWest.IndexOf(barModelWest.Where(x => x.BarNumber == BarCornerModels[i].BarNumber).FirstOrDefault());
                                y01 = infoModelUp.NouthPosition - Cover - ds - BarCorner.Diameter / 2 - (index + 1) * deltaY - BarCorner.Diameter;
                            }
                            else
                            {
                                x01 = BarCornerModels[i].X0; y01 = BarCornerModels[i].Y0;
                            }
                        }
                        #endregion
                    }
                    else
                    {
                        ds = ds0;
                        x01 = GetNextX0BarCorner(infoModel, infoModelUp, Cover, ds, BarCorner.Diameter)[BarCornerModels[i].BarNumber - 1];
                        y01 = GetNextY0BarCorner(infoModelUp, Cover, ds)[BarCornerModels[i].BarNumber - 1];
                    }
                  
                }
                #endregion
                BarCornerModels[i].GetDowelsLocationBarCorner(infoModel, nxCorner, nyCorner, Cover, SplitOverlap, Overlap, x01, y01);
            }
        }
        #endregion
        #region Push Top
      
        public void RefreshLocationBarModels(InfoModel infoModel, double Cover, double ds0, double dsUP,double dsUpCorner, InfoModel infoModelUp = null)
        {
            double ds = ds0;
            RefreshX0Y0BarModels(infoModel, Cover, ds);
            if (infoModelUp==null)
            {
                for (int i = 0; i < 2 * nx + 2 * (ny - 2); i++)
                {
                    double x01 = 0, y01 = 0;
                    x01 = BarModels[i].X0;
                    y01 = BarModels[i].Y0;
                    BarModels[i].GetDowelsLocationBar(infoModel, 2 * nx + 2 * (ny - 2), nx, ny, Cover, SplitOverlap, Overlap, x01, y01);
                }
            }
            else
            {
                if (infoModel.IsCorner)
                {
                    if (BarCornerModels.Count==0)
                    {
                        RefreshLocationItem1(infoModel, Cover, ds, dsUP, dsUpCorner, infoModelUp);
                    }
                    else
                    {
                        RefreshLocationItem2(infoModel, Cover, ds, dsUP, dsUpCorner, infoModelUp);
                    }
                }
                else
                {
                    RefreshLocationItem1(infoModel, Cover, ds, dsUP, dsUpCorner, infoModelUp);
                }
            }
            
        }
        #endregion
        private void RefreshLocationItem1(InfoModel infoModel, double Cover,double ds,  double dsUP, double dsUpCorner, InfoModel infoModelUp = null)
        {
            if (infoModelUp.IsCorner)
            {
                for (int i = 0; i < 2 * nx + 2 * (ny - 2); i++)
                {
                    double x01 = 0, y01 = 0;
                    x01 = BarModels[i].X0;
                    y01 = BarModels[i].Y0;
                    if (BarModels[i].IsTopDowels && BarModels[i].TopDowels == 0)
                    {
                        ds = dsUP;
                        if (BarModels[i].BarNumber <= nx)
                        {
                            ObservableCollection<BarModel> barModelSouth = new ObservableCollection<BarModel>(BarModels.Where(x => x.BarNumber <= nx && x.IsTopDowels && x.TopDowels == 0).OrderBy(x => x.BarNumber).ToList());
                            if (barModelSouth.Count == 0)
                            {
                                x01 = BarModels[i].X0;
                                y01 = BarModels[i].Y0;
                            }
                            else
                            {
                                y01 = (infoModelUp.SouthPosition + Cover + ds + Bar.Diameter / 2);
                                if (BarModels[i].PushTop.Equals("NONE"))
                                {
                                    x01 = BarModels[i].X0;
                                }
                                else
                                {
                                    if (BarModels[i].PushTop.Equals("Main"))
                                    {
                                        ObservableCollection<BarModel> barModelSouthMain = new ObservableCollection<BarModel>(barModelSouth.Where(x => x.PushTop.Equals("Main")).OrderBy(x => x.BarNumber).ToList());
                                        if (barModelSouthMain.Count == 1)
                                        {
                                            x01 = infoModelUp.WestPosition + infoModelUp.L1 + infoModelUp.L2 / 2 + Bar.Diameter;
                                        }
                                        else
                                        {
                                            double deltaX = (((infoModelUp.L2)) - 2 * Cover - 2 * ds - Bar.Diameter) / (barModelSouthMain.Count - 1);
                                            int index = barModelSouthMain.IndexOf(barModelSouthMain.Where(x => x.BarNumber == BarModels[i].BarNumber).FirstOrDefault());
                                            x01 = infoModelUp.WestPosition + infoModelUp.L1 + Cover + ds + Bar.Diameter / 2 + ((index == barModelSouthMain.Count - 1) ? (-Bar.Diameter) : (Bar.Diameter)) + index * deltaX;
                                        }
                                    }
                                    else
                                    {
                                        ds = dsUpCorner;
                                        y01 = (infoModelUp.SouthPosition + Cover + ds + Bar.Diameter / 2);
                                        if (SetLeftRightBarsModel(infoModel, BarModels[i]))
                                        {
                                            ObservableCollection<BarModel> barModelSouthLeft = new ObservableCollection<BarModel>(barModelSouth.Where(x => x.PushTop.Equals("Corner") && SetLeftRightBarsModel(infoModel, x)).OrderBy(x => x.BarNumber).ToList());
                                            if (barModelSouthLeft.Count == 1)
                                            {
                                                x01 = infoModelUp.WestPosition + infoModelUp.L1 / 2 + Bar.Diameter;
                                            }
                                            else
                                            {
                                                double deltaX = (((infoModelUp.L1)) - 2 * Cover - 2 * ds - Bar.Diameter) / (barModelSouthLeft.Count - 1);
                                                int index = barModelSouthLeft.IndexOf(barModelSouthLeft.Where(x => x.BarNumber == BarModels[i].BarNumber).FirstOrDefault());
                                                x01 = infoModelUp.WestPosition + Cover + ds + Bar.Diameter / 2 + ((index == barModelSouthLeft.Count - 1) ? (-Bar.Diameter) : (Bar.Diameter)) + index * deltaX;
                                            }
                                        }
                                        else
                                        {
                                            ObservableCollection<BarModel> barModelSouthRight = new ObservableCollection<BarModel>(barModelSouth.Where(x => x.PushTop.Equals("Corner") && !SetLeftRightBarsModel(infoModel, x)).OrderBy(x => x.BarNumber).ToList());
                                            if (barModelSouthRight.Count == 1)
                                            {
                                                x01 = infoModelUp.EastPosition - infoModelUp.L1 / 2 + Bar.Diameter;
                                            }
                                            else
                                            {
                                                double deltaX = (((infoModelUp.L1)) - 2 * Cover - 2 * ds - Bar.Diameter) / (barModelSouthRight.Count - 1);
                                                int index = barModelSouthRight.IndexOf(barModelSouthRight.Where(x => x.BarNumber == BarModels[i].BarNumber).FirstOrDefault());
                                                x01 = infoModelUp.EastPosition - infoModelUp.L1 + Cover + ds + Bar.Diameter / 2 + ((index == barModelSouthRight.Count - 1) ? (-Bar.Diameter) : (Bar.Diameter)) + index * deltaX;
                                            }
                                        }
                                    }
                                }
                            }
                        }
                        if (BarModels[i].BarNumber > nx && BarModels[i].BarNumber <= nx + ny - 2)
                        {
                            ObservableCollection<BarModel> barModelEast = new ObservableCollection<BarModel>(BarModels.Where(x => (x.BarNumber > nx && x.BarNumber <= nx + ny - 2) && x.IsTopDowels && x.TopDowels == 0).OrderBy(x => x.BarNumber).ToList());
                            if (barModelEast.Count == 0)
                            {
                                x01 = BarModels[i].X0;
                                y01 = BarModels[i].Y0;
                            }
                            else
                            {
                                if (BarModels[i].PushTop.Equals("Corner"))
                                {
                                    ds = dsUpCorner;
                                    x01 = infoModelUp.EastPosition - Cover - ds - Bar.Diameter / 2;
                                    ObservableCollection<BarModel> barModelEastNoneCorner = new ObservableCollection<BarModel>(barModelEast.Where(x => !x.PushTop.Equals("Corner")).OrderBy(x => x.BarNumber).ToList());
                                    if (barModelEastNoneCorner.Count == 1)
                                    {
                                        y01 = infoModelUp.SouthPosition + infoModelUp.T / 2 + Bar.Diameter;
                                    }
                                    else
                                    {
                                        double deltaY = (infoModelUp.T - 2 * Cover - 2 * ds - Bar.Diameter) / (barModelEastNoneCorner.Count + 1);
                                        int index = barModelEastNoneCorner.IndexOf(barModelEastNoneCorner.Where(x => x.BarNumber == BarModels[i].BarNumber).FirstOrDefault());
                                        y01 = infoModelUp.SouthPosition + Cover + ds + Bar.Diameter / 2 + (index + 1) * deltaY - Bar.Diameter;
                                    }
                                }
                                else
                                {
                                    x01 = infoModelUp.EastPosition - infoModelUp.L1 - Cover - ds - Bar.Diameter / 2;
                                    ObservableCollection<BarModel> barModelEastNoneCorner = new ObservableCollection<BarModel>(barModelEast.Where(x => !x.PushTop.Equals("Corner")).OrderBy(x => x.BarNumber).ToList());
                                    if (barModelEastNoneCorner.Count == 1)
                                    {
                                        y01 = infoModelUp.SouthPosition + infoModelUp.T / 2 + Bar.Diameter;
                                    }
                                    else
                                    {
                                        double deltaY = (infoModelUp.T - 2 * Cover - 2 * ds - Bar.Diameter) / (barModelEastNoneCorner.Count + 1);
                                        int index = barModelEastNoneCorner.IndexOf(barModelEastNoneCorner.Where(x => x.BarNumber == BarModels[i].BarNumber).FirstOrDefault());
                                        y01 = infoModelUp.SouthPosition + Cover + ds + Bar.Diameter / 2 + (index + 1) * deltaY - Bar.Diameter;
                                    }
                                }
                            }
                        }
                        if (BarModels[i].BarNumber > nx + ny - 2 && BarModels[i].BarNumber <= 2 * nx + ny - 2)
                        {
                            ObservableCollection<BarModel> barModelNouth = new ObservableCollection<BarModel>(BarModels.Where(x => (x.BarNumber > nx + ny - 2 && x.BarNumber <= 2 * nx + ny - 2) && x.IsTopDowels && x.TopDowels == 0).OrderBy(x => x.BarNumber).ToList());
                            if (barModelNouth.Count == 0)
                            {
                                x01 = BarModels[i].X0;
                                y01 = BarModels[i].Y0;
                            }
                            else
                            {
                                y01 = (infoModelUp.NouthPosition - Cover - ds - Bar.Diameter / 2);
                                if (BarModels[i].PushTop.Equals("NONE"))
                                {
                                    x01 = BarModels[i].X0;
                                }
                                else
                                {
                                    if (BarModels[i].PushTop.Equals("Main"))
                                    {
                                        ObservableCollection<BarModel> barModelNouthMain = new ObservableCollection<BarModel>(barModelNouth.Where(x => x.PushTop.Equals("Main")).OrderBy(x => x.BarNumber).ToList());
                                        if (barModelNouthMain.Count == 1)
                                        {
                                            x01 = infoModelUp.WestPosition + infoModelUp.L1 + infoModelUp.L2 / 2 + Bar.Diameter;
                                        }
                                        else
                                        {
                                            double deltaX = (((infoModelUp.L2)) - 2 * Cover - 2 * ds - Bar.Diameter) / (barModelNouthMain.Count - 1);
                                            int index = barModelNouthMain.IndexOf(barModelNouthMain.Where(x => x.BarNumber == BarModels[i].BarNumber).FirstOrDefault());
                                            x01 = infoModelUp.EastPosition - (infoModelUp.L1) - Cover - ds - Bar.Diameter / 2 + ((index == barModelNouthMain.Count - 1) ? (Bar.Diameter) : (-Bar.Diameter)) - index * deltaX;
                                        }
                                    }
                                    else
                                    {
                                        ds = dsUpCorner;
                                        y01 = (infoModelUp.NouthPosition - Cover - ds - Bar.Diameter / 2);
                                        if (SetLeftRightBarsModel(infoModel, BarModels[i]))
                                        {
                                            ObservableCollection<BarModel> barModelNouthLeft = new ObservableCollection<BarModel>(barModelNouth.Where(x => x.PushTop.Equals("Corner") && SetLeftRightBarsModel(infoModel, x)).OrderBy(x => x.BarNumber).ToList());
                                            if (barModelNouthLeft.Count == 1)
                                            {
                                                x01 = infoModelUp.WestPosition + infoModelUp.L1 / 2 + Bar.Diameter;
                                            }
                                            else
                                            {
                                                double deltaX = (((infoModelUp.L1)) - 2 * Cover - 2 * ds - Bar.Diameter) / (barModelNouthLeft.Count - 1);
                                                int index = barModelNouthLeft.IndexOf(barModelNouthLeft.Where(x => x.BarNumber == BarModels[i].BarNumber).FirstOrDefault());
                                                x01 = infoModelUp.WestPosition + Cover + ds + Bar.Diameter / 2 + ((index == barModelNouthLeft.Count - 1) ? (-Bar.Diameter) : (Bar.Diameter)) + index * deltaX;
                                            }
                                        }
                                        else
                                        {
                                            ObservableCollection<BarModel> barModelNouthRight = new ObservableCollection<BarModel>(barModelNouth.Where(x => x.PushTop.Equals("Corner") && !SetLeftRightBarsModel(infoModel, x)).OrderBy(x => x.BarNumber).ToList());
                                            if (barModelNouthRight.Count == 1)
                                            {
                                                x01 = infoModelUp.EastPosition - infoModelUp.L1 / 2 + Bar.Diameter;
                                            }
                                            else
                                            {
                                                double deltaX = (((infoModelUp.L1)) - 2 * Cover - 2 * ds - Bar.Diameter) / (barModelNouthRight.Count - 1);
                                                int index = barModelNouthRight.IndexOf(barModelNouthRight.Where(x => x.BarNumber == BarModels[i].BarNumber).FirstOrDefault());
                                                x01 = infoModelUp.EastPosition - infoModelUp.L1 + Cover + ds + Bar.Diameter / 2 + ((index == barModelNouthRight.Count - 1) ? (-Bar.Diameter) : (Bar.Diameter)) + index * deltaX;
                                            }
                                        }

                                    }
                                }
                            }
                        }
                        if (BarModels[i].BarNumber > 2 * nx + ny - 2)
                        {
                            ObservableCollection<BarModel> barModelWest = new ObservableCollection<BarModel>(BarModels.Where(x => (x.BarNumber > 2 * nx + ny - 2) && x.IsTopDowels && x.TopDowels == 0).OrderBy(x => x.BarNumber).ToList());
                            if (barModelWest.Count == 0)
                            {
                                x01 = BarModels[i].X0;
                                y01 = BarModels[i].Y0;
                            }
                            else
                            {
                                if (BarModels[i].PushTop.Equals("Corner"))
                                {
                                    ds = dsUpCorner;
                                    x01 = infoModelUp.WestPosition + Cover + ds + Bar.Diameter / 2;
                                    ObservableCollection<BarModel> barModelWestCorner = new ObservableCollection<BarModel>(barModelWest.Where(x => !x.PushTop.Equals("Corner")).OrderBy(x => x.BarNumber).ToList());
                                    if (barModelWestCorner.Count == 1)
                                    {
                                        y01 = infoModelUp.SouthPosition + infoModelUp.T / 2 - Bar.Diameter;
                                    }
                                    else
                                    {
                                        double deltaY = (infoModelUp.T - 2 * Cover - 2 * ds - Bar.Diameter) / (barModelWestCorner.Count + 1);
                                        int index = barModelWestCorner.IndexOf(barModelWestCorner.Where(x => x.BarNumber == BarModels[i].BarNumber).FirstOrDefault());
                                        y01 = infoModelUp.NouthPosition - Cover - ds - Bar.Diameter / 2 - (index + 1) * deltaY - Bar.Diameter;
                                    }
                                }
                                else
                                {
                                    x01 = infoModelUp.WestPosition + infoModelUp.L1 + Cover + ds + Bar.Diameter / 2;
                                    ObservableCollection<BarModel> barModelWestCorner = new ObservableCollection<BarModel>(barModelWest.Where(x => !x.PushTop.Equals("Corner")).OrderBy(x => x.BarNumber).ToList());
                                    if (barModelWestCorner.Count == 1)
                                    {
                                        y01 = infoModelUp.SouthPosition + infoModelUp.T / 2 - Bar.Diameter;
                                    }
                                    else
                                    {
                                        double deltaY = (infoModelUp.T - 2 * Cover - 2 * ds - Bar.Diameter) / (barModelWestCorner.Count + 1);
                                        int index = barModelWestCorner.IndexOf(barModelWestCorner.Where(x => x.BarNumber == BarModels[i].BarNumber).FirstOrDefault());
                                        y01 = infoModelUp.NouthPosition - Cover - ds - Bar.Diameter / 2 - (index + 1) * deltaY - Bar.Diameter;
                                    }
                                }
                            }

                        }
                    }
                    else
                    {
                        
                        x01 = BarModels[i].X0;
                        y01 = BarModels[i].Y0;
                    }

                    BarModels[i].GetDowelsLocationBar(infoModel, 2 * nx + 2 * (ny - 2), nx, ny, Cover, SplitOverlap, Overlap, x01, y01);
                }
            }
            else
            {
                for (int i = 0; i < 2 * nx + 2 * (ny - 2); i++)
                {
                    double x01 = 0, y01 = 0;
                    x01 = BarModels[i].X0;
                    y01 = BarModels[i].Y0;
                    if (BarModels[i].IsTopDowels && BarModels[i].TopDowels == 0)
                    {
                        ds = dsUP;
                        if (BarModels[i].BarNumber <= nx)
                        {
                            ObservableCollection<BarModel> barModelSouth = new ObservableCollection<BarModel>(BarModels.Where(x => x.BarNumber <= nx && x.IsTopDowels && x.TopDowels == 0).OrderBy(x => x.BarNumber).ToList());
                            if (barModelSouth.Count != 0)
                            {
                                y01 = (infoModelUp.SouthPosition + Cover + ds + Bar.Diameter / 2);
                                if (barModelSouth.Count == 1)
                                {
                                    x01 = GetNextX0Bar(infoModelUp, Cover, ds, Bar.Diameter)[BarModels[i].BarNumber - 1];
                                }
                                else
                                {
                                    double deltaX = (((infoModelUp.L)) - 2 * Cover - 2 * ds - Bar.Diameter) / (barModelSouth.Count - 1);
                                    int index = barModelSouth.IndexOf(barModelSouth.Where(x => x.BarNumber == BarModels[i].BarNumber).FirstOrDefault());
                                    x01 = infoModelUp.WestPosition + Cover + ds + Bar.Diameter / 2 + ((index == barModelSouth.Count - 1) ? (-Bar.Diameter) : (Bar.Diameter)) + index * deltaX;
                                }
                            }
                            else
                            {
                                x01 = BarModels[i].X0;
                                y01 = BarModels[i].Y0;
                            }
                        }
                        if (BarModels[i].BarNumber > nx && BarModels[i].BarNumber <= nx + ny - 2)
                        {
                            ObservableCollection<BarModel> barModelEast = new ObservableCollection<BarModel>(BarModels.Where(x => (x.BarNumber > nx && x.BarNumber <= nx + ny - 2) && x.IsTopDowels && x.TopDowels == 0).OrderBy(x => x.BarNumber).ToList());
                            if (barModelEast.Count != 0)
                            {
                                x01 = infoModelUp.EastPosition - Cover - ds - Bar.Diameter / 2;
                                double deltaY = (infoModelUp.T - 2 * Cover - 2 * ds - Bar.Diameter) / (barModelEast.Count + 1);
                                int index = barModelEast.IndexOf(barModelEast.Where(x => x.BarNumber == BarModels[i].BarNumber).FirstOrDefault());
                                y01 = infoModelUp.SouthPosition + Cover + ds + Bar.Diameter / 2 + (index + 1) * deltaY - Bar.Diameter;
                            }
                            else
                            {
                                x01 = BarModels[i].X0;
                                y01 = BarModels[i].Y0;
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
                                    x01 = GetNextX0Bar(infoModelUp, Cover, ds, Bar.Diameter)[BarModels[i].BarNumber - 1];
                                }
                                else
                                {
                                    double deltaX = (((infoModelUp.L)) - 2 * Cover - 2 * ds - Bar.Diameter) / (barModelNouth.Count - 1);
                                    int index = barModelNouth.IndexOf(barModelNouth.Where(x => x.BarNumber == BarModels[i].BarNumber).FirstOrDefault());
                                    x01 = infoModelUp.EastPosition - Cover - ds - Bar.Diameter / 2 + ((index == barModelNouth.Count - 1) ? (Bar.Diameter) : (-Bar.Diameter)) - index * deltaX;
                                }
                            }
                            else
                            {
                                x01 = BarModels[i].X0;
                                y01 = BarModels[i].Y0;
                            }
                        }
                        if (BarModels[i].BarNumber > 2 * nx + ny - 2)
                        {
                            ObservableCollection<BarModel> barModelWest = new ObservableCollection<BarModel>(BarModels.Where(x => (x.BarNumber > 2 * nx + ny - 2) && x.IsTopDowels && x.TopDowels == 0).OrderBy(x => x.BarNumber).ToList());
                            if (barModelWest.Count != 0)
                            {
                                x01 = infoModelUp.WestPosition + Cover + ds + Bar.Diameter / 2;
                                double deltaY = (infoModelUp.T - 2 * Cover - 2 * ds - Bar.Diameter) / (barModelWest.Count + 1);
                                int index = barModelWest.IndexOf(barModelWest.Where(x => x.BarNumber == BarModels[i].BarNumber).FirstOrDefault());
                                y01 = infoModelUp.NouthPosition - Cover - ds - Bar.Diameter / 2 - (index + 1) * deltaY - Bar.Diameter;
                            }
                            else
                            {
                                x01 = BarModels[i].X0;
                                y01 = BarModels[i].Y0;
                            }
                        }
                    }
                    else
                    {
                        x01 = BarModels[i].X0;
                        y01 = BarModels[i].Y0;
                    }
                    BarModels[i].GetDowelsLocationBar(infoModel, 2 * nx + 2 * (ny - 2), nx, ny, Cover, SplitOverlap, Overlap, x01, y01);
                }
                if (BarModels.Count > 2 * nx + 2 * (ny - 2))
                {
                    for (int i = 2 * nx + 2 * (ny - 2); i < BarModels.Count; i++)
                    {

                    }
                }
            }

        }
        private void RefreshLocationItem2(InfoModel infoModel, double Cover, double ds, double dsUP, double dsUpCorner, InfoModel infoModelUp = null)
        {
            if (infoModelUp.IsCorner)
            {
                for (int i = 0; i < 2 * nx + 2 * (ny - 2); i++)
                {
                    double x01 = 0, y01 = 0;
                    x01 = BarModels[i].X0;
                    y01 = BarModels[i].Y0;
                    if (BarModels[i].IsTopDowels && BarModels[i].TopDowels == 0)
                    {
                        ds = dsUP;
                        if (BarModels[i].BarNumber <= nx)
                        {
                            ObservableCollection<BarModel> barModelSouth = new ObservableCollection<BarModel>(BarModels.Where(x => x.BarNumber <= nx && x.IsTopDowels && x.TopDowels == 0).OrderBy(x => x.BarNumber).ToList());
                            if (barModelSouth.Count == 0)
                            {
                                x01 = BarModels[i].X0;
                                y01 = BarModels[i].Y0;
                            }
                            else
                            {
                                y01 = (infoModelUp.SouthPosition + Cover + ds + Bar.Diameter / 2);
                                if (BarModels[i].PushTop.Equals("NONE"))
                                {
                                    x01 = BarModels[i].X0;
                                }
                                else
                                {
                                    if (BarModels[i].PushTop.Equals("Main"))
                                    {
                                        ObservableCollection<BarModel> barModelSouthMain = new ObservableCollection<BarModel>(barModelSouth.Where(x => x.PushTop.Equals("Main")).OrderBy(x => x.BarNumber).ToList());
                                        if (barModelSouthMain.Count == 1)
                                        {
                                            x01 = infoModelUp.WestPosition + infoModelUp.L1 + infoModelUp.L2 / 2 + Bar.Diameter;
                                        }
                                        else
                                        {
                                            double deltaX = (((infoModelUp.L2)) - 2 * Cover - 2 * ds - Bar.Diameter) / (barModelSouthMain.Count - 1);
                                            int index = barModelSouthMain.IndexOf(barModelSouthMain.Where(x => x.BarNumber == BarModels[i].BarNumber).FirstOrDefault());
                                            x01 = infoModelUp.WestPosition + infoModelUp.L1 + Cover + ds + Bar.Diameter / 2 + ((index == barModelSouthMain.Count - 1) ? (-Bar.Diameter) : (Bar.Diameter)) + index * deltaX;
                                        }
                                    }
                                    else
                                    {
                                        ds = dsUpCorner;
                                        y01 = (infoModelUp.SouthPosition + Cover + ds + Bar.Diameter / 2);
                                        if (SetLeftRightBarsModel(infoModel, BarModels[i]))
                                        {
                                            ObservableCollection<BarModel> barModelSouthLeft = new ObservableCollection<BarModel>(barModelSouth.Where(x => x.PushTop.Equals("Corner") && SetLeftRightBarsModel(infoModel, x)).OrderBy(x => x.BarNumber).ToList());
                                            if (barModelSouthLeft.Count == 1)
                                            {
                                                x01 = infoModelUp.WestPosition + infoModelUp.L1 / 2 + Bar.Diameter;
                                            }
                                            else
                                            {
                                                double deltaX = (((infoModelUp.L1)) - 2 * Cover - 2 * ds - Bar.Diameter) / (barModelSouthLeft.Count - 1);
                                                int index = barModelSouthLeft.IndexOf(barModelSouthLeft.Where(x => x.BarNumber == BarModels[i].BarNumber).FirstOrDefault());
                                                x01 = infoModelUp.WestPosition + Cover + ds + Bar.Diameter / 2 + ((index == barModelSouthLeft.Count - 1) ? (-Bar.Diameter) : (Bar.Diameter)) + index * deltaX;
                                            }
                                        }
                                        else
                                        {
                                            ObservableCollection<BarModel> barModelSouthRight = new ObservableCollection<BarModel>(barModelSouth.Where(x => x.PushTop.Equals("Corner") && !SetLeftRightBarsModel(infoModel, x)).OrderBy(x => x.BarNumber).ToList());
                                            if (barModelSouthRight.Count == 1)
                                            {
                                                x01 = infoModelUp.EastPosition - infoModelUp.L1 / 2 + Bar.Diameter;
                                            }
                                            else
                                            {
                                                double deltaX = (((infoModelUp.L1)) - 2 * Cover - 2 * ds - Bar.Diameter) / (barModelSouthRight.Count - 1);
                                                int index = barModelSouthRight.IndexOf(barModelSouthRight.Where(x => x.BarNumber == BarModels[i].BarNumber).FirstOrDefault());
                                                x01 = infoModelUp.EastPosition - infoModelUp.L1 + Cover + ds + Bar.Diameter / 2 + ((index == barModelSouthRight.Count - 1) ? (-Bar.Diameter) : (Bar.Diameter)) + index * deltaX;
                                            }
                                        }
                                    }
                                }
                            }
                        }
                        if (BarModels[i].BarNumber > nx + ny - 2 && BarModels[i].BarNumber <= 2 * nx + ny - 2)
                        {
                            ObservableCollection<BarModel> barModelNouth = new ObservableCollection<BarModel>(BarModels.Where(x => (x.BarNumber > nx + ny - 2 && x.BarNumber <= 2 * nx + ny - 2) && x.IsTopDowels && x.TopDowels == 0).OrderBy(x => x.BarNumber).ToList());
                            if (barModelNouth.Count == 0)
                            {
                                x01 = BarModels[i].X0;
                                y01 = BarModels[i].Y0;
                            }
                            else
                            {
                                y01 = (infoModelUp.NouthPosition - Cover - ds - Bar.Diameter / 2);
                                if (BarModels[i].PushTop.Equals("NONE"))
                                {
                                    x01 = BarModels[i].X0;
                                }
                                else
                                {
                                    if (BarModels[i].PushTop.Equals("Main"))
                                    {
                                        ObservableCollection<BarModel> barModelNouthMain = new ObservableCollection<BarModel>(barModelNouth.Where(x => x.PushTop.Equals("Main")).OrderBy(x => x.BarNumber).ToList());
                                        if (barModelNouthMain.Count == 1)
                                        {
                                            x01 = infoModelUp.WestPosition + infoModelUp.L1 + infoModelUp.L2 / 2 + Bar.Diameter;
                                        }
                                        else
                                        {
                                            double deltaX = (((infoModelUp.L2)) - 2 * Cover - 2 * ds - Bar.Diameter) / (barModelNouthMain.Count - 1);
                                            int index = barModelNouthMain.IndexOf(barModelNouthMain.Where(x => x.BarNumber == BarModels[i].BarNumber).FirstOrDefault());
                                            x01 = infoModelUp.EastPosition - (infoModelUp.L1) - Cover - ds - Bar.Diameter / 2 + ((index == barModelNouthMain.Count - 1) ? (Bar.Diameter) : (-Bar.Diameter)) - index * deltaX;
                                        }
                                    }
                                    else
                                    {
                                        ds = dsUpCorner;
                                        y01 = (infoModelUp.NouthPosition - Cover - ds - Bar.Diameter / 2);
                                        if (SetLeftRightBarsModel(infoModel, BarModels[i]))
                                        {
                                            ObservableCollection<BarModel> barModelNouthLeft = new ObservableCollection<BarModel>(barModelNouth.Where(x => x.PushTop.Equals("Corner") && SetLeftRightBarsModel(infoModel, x)).OrderBy(x => x.BarNumber).ToList());
                                            if (barModelNouthLeft.Count == 1)
                                            {
                                                x01 = infoModelUp.WestPosition + infoModelUp.L1 / 2 + Bar.Diameter;
                                            }
                                            else
                                            {
                                                double deltaX = (((infoModelUp.L1)) - 2 * Cover - 2 * ds - Bar.Diameter) / (barModelNouthLeft.Count - 1);
                                                int index = barModelNouthLeft.IndexOf(barModelNouthLeft.Where(x => x.BarNumber == BarModels[i].BarNumber).FirstOrDefault());
                                                x01 = infoModelUp.WestPosition + Cover + ds + Bar.Diameter / 2 + ((index == barModelNouthLeft.Count - 1) ? (-Bar.Diameter) : (Bar.Diameter)) + index * deltaX;
                                            }
                                        }
                                        else
                                        {
                                            ObservableCollection<BarModel> barModelNouthRight = new ObservableCollection<BarModel>(barModelNouth.Where(x => x.PushTop.Equals("Corner") && !SetLeftRightBarsModel(infoModel, x)).OrderBy(x => x.BarNumber).ToList());
                                            if (barModelNouthRight.Count == 1)
                                            {
                                                x01 = infoModelUp.EastPosition - infoModelUp.L1 / 2 + Bar.Diameter;
                                            }
                                            else
                                            {
                                                double deltaX = (((infoModelUp.L1)) - 2 * Cover - 2 * ds - Bar.Diameter) / (barModelNouthRight.Count - 1);
                                                int index = barModelNouthRight.IndexOf(barModelNouthRight.Where(x => x.BarNumber == BarModels[i].BarNumber).FirstOrDefault());
                                                x01 = infoModelUp.EastPosition - infoModelUp.L1 + Cover + ds + Bar.Diameter / 2 + ((index == barModelNouthRight.Count - 1) ? (-Bar.Diameter) : (Bar.Diameter)) + index * deltaX;
                                            }
                                        }

                                    }
                                }
                            }
                        }
                    }
                    else
                    {
                        x01 = BarModels[i].X0;
                        y01 = BarModels[i].Y0;
                    }

                    BarModels[i].GetDowelsLocationBar(infoModel, 2 * nx + 2 * (ny - 2), nx, ny, Cover, SplitOverlap, Overlap, x01, y01);
                }
            }
            else
            {
                for (int i = 0; i < 2 * nx + 2 * (ny - 2); i++)
                {
                    double x01 = 0, y01 = 0;
                    x01 = BarModels[i].X0;
                    y01 = BarModels[i].Y0;
                    if (BarModels[i].IsTopDowels && BarModels[i].TopDowels == 0)
                    {
                        ds = dsUP;
                        int totalConer = 2 * nxCorner + 2 * (nyCorner - 2);
                        if (BarModels[i].BarNumber <= nx)
                        {
                            
                            ObservableCollection<BarModel> barModelSouthCornerLeft = new ObservableCollection<BarModel>(BarCornerModels.Where(x => (x.BarNumber <= nxCorner) && x.IsTopDowels && x.TopDowels == 0).OrderBy(x => x.BarNumber).ToList());
                            ObservableCollection<BarModel> barModelSouthCornerRight = new ObservableCollection<BarModel>(BarCornerModels.Where(x => ((x.BarNumber>totalConer)&&(x.BarNumber<=nxCorner+totalConer)) && x.IsTopDowels && x.TopDowels == 0).OrderBy(x => x.BarNumber).ToList());
                            ObservableCollection<BarModel> barModelSouth0 = new ObservableCollection<BarModel>(BarModels.Where(x => x.BarNumber <= nx && x.IsTopDowels && x.TopDowels == 0).OrderBy(x => x.BarNumber).ToList());
                            ObservableCollection<BarModel> barModelSouth = new ObservableCollection<BarModel>();
                            barModelSouth.ToList().ForEach(barModelSouthCornerLeft.Add);
                            barModelSouth.ToList().ForEach(barModelSouth0.Add);
                            barModelSouth.ToList().ForEach(barModelSouthCornerRight.Add);
                            if (barModelSouth.Count != 0)
                            {
                                y01 = (infoModelUp.SouthPosition + Cover + ds + Bar.Diameter / 2);
                                if (barModelSouth.Count == 1)
                                {
                                    x01 = GetNextX0Bar(infoModelUp, Cover, ds, Bar.Diameter)[BarModels[i].BarNumber - 1];
                                }
                                else
                                {
                                    double deltaX = (((infoModelUp.L)) - 2 * Cover - 2 * ds - Bar.Diameter) / (barModelSouth.Count - 1);
                                    int index = barModelSouth.IndexOf(barModelSouth.Where(x => x.X0 == BarModels[i].X0).FirstOrDefault());
                                    x01 = infoModelUp.WestPosition + Cover + ds + Bar.Diameter / 2 + ((index == barModelSouth.Count - 1) ? (-Bar.Diameter) : (Bar.Diameter)) + index * deltaX;
                                }
                            }
                            else
                            {
                                x01 = BarModels[i].X0;
                                y01 = BarModels[i].Y0;
                            }
                        }
                        if (BarModels[i].BarNumber > nx + ny - 2 && BarModels[i].BarNumber <= 2 * nx + ny - 2)
                        {
                            ObservableCollection<BarModel> barModelNouthCornerLeft = new ObservableCollection<BarModel>(BarCornerModels.Where(x => ((x.BarNumber > nxCorner + nyCorner - 2 && x.BarNumber <= 2 * nxCorner + nyCorner - 2)) && x.IsTopDowels && x.TopDowels == 0).OrderBy(x => x.BarNumber).ToList());
                            ObservableCollection<BarModel> barModelNouthCornerRight = new ObservableCollection<BarModel>(BarCornerModels.Where(x => (( x.BarNumber > nxCorner + nyCorner - 2+ totalConer && x.BarNumber <= 2 * nxCorner + nyCorner - 2+ totalConer)) && x.IsTopDowels && x.TopDowels == 0).OrderBy(x => x.BarNumber).ToList());
                            ObservableCollection<BarModel> barModelNouth0 = new ObservableCollection<BarModel>(BarModels.Where(x => (x.BarNumber > nx + ny - 2 && x.BarNumber <= 2 * nx + ny - 2) && x.IsTopDowels && x.TopDowels == 0).OrderBy(x => x.BarNumber).ToList());
                            ObservableCollection<BarModel> barModelNouth = new ObservableCollection<BarModel>();
                            barModelNouth.ToList().ForEach(barModelNouthCornerLeft.Add);
                            barModelNouth.ToList().ForEach(barModelNouth0.Add);
                            barModelNouth.ToList().ForEach(barModelNouth0.Add);
                            if (barModelNouth.Count != 0)
                            {
                                y01 = (infoModelUp.NouthPosition - Cover - ds - Bar.Diameter / 2);
                                if (barModelNouth.Count == 1)
                                {
                                    x01 = GetNextX0Bar(infoModelUp, Cover, ds, Bar.Diameter)[BarModels[i].BarNumber - 1];
                                }
                                else
                                {
                                    double deltaX = (((infoModelUp.L)) - 2 * Cover - 2 * ds - Bar.Diameter) / (barModelNouth.Count - 1);
                                    int index = barModelNouth.IndexOf(barModelNouth.Where(x => x.X0 == BarModels[i].X0).FirstOrDefault());
                                    x01 = infoModelUp.EastPosition - Cover - ds - Bar.Diameter / 2 + ((index == barModelNouth.Count - 1) ? (Bar.Diameter) : (-Bar.Diameter)) - index * deltaX;
                                }
                            }
                            else
                            {
                                x01 = BarModels[i].X0;
                                y01 = BarModels[i].Y0;
                            }
                        }
                    }
                    else
                    {
                        x01 = BarModels[i].X0;
                        y01 = BarModels[i].Y0;
                    }
                    BarModels[i].GetDowelsLocationBar(infoModel, 2 * nx + 2 * (ny - 2), nx, ny, Cover, SplitOverlap, Overlap, x01, y01);
                }
                if (BarModels.Count > 2 * nx + 2 * (ny - 2))
                {
                    for (int i = 2 * nx + 2 * (ny - 2); i < BarModels.Count; i++)
                    {

                    }
                }
            }

        }
        #region Fixed
        //public bool ConditionFixedTop(SectionStyle sectionStyle, InfoModel infoModelUp=null,BarMainModel barMainModelUp=null)
        //{
        //    if (BarModels.Count == 0)
        //    {
        //        return false;
        //    }
        //    else
        //    {
        //        if (infoModelUp == null)
        //        {
        //            return false;

        //        }
        //        else
        //        {
        //            if (barMainModelUp.BarModels.Count == 0)
        //            {
        //                return false;
        //            }
        //            else
        //            {
        //                ObservableCollection<BarModel> barModels = new ObservableCollection<BarModel>(BarModels.Where(x=>x.IsTopDowels&&x.TopDowels==0).ToList());
        //                if (barModels.Count == 0)
        //                {
        //                    if (AddBarModels.Count==0)
        //                    {
        //                        return false;
        //                    }
        //                    else
        //                    {
        //                        return (AddBarModels.Count== barMainModelUp.BarModels.Count);

        //                    }
        //                }
        //                else
        //                {
        //                    if (barModels.Count != barMainModelUp.BarModels.Count)
        //                    {
        //                        return false;
        //                    }
        //                    else
        //                    {
        //                        #region
        //                        //if (sectionStyle == SectionStyle.RECTANGLE)
        //                        //{
        //                        //    ObservableCollection<BarModel> barModelSouth = new ObservableCollection<BarModel>(BarModels.Where(x => x.BarNumber <= nx && x.IsTopDowels && x.TopDowels == 0).ToList());
        //                        //    if (barModelSouth.Count == 0)
        //                        //    {
        //                        //        return false;
        //                        //    }
        //                        //    else
        //                        //    {
        //                        //        if (barModelSouth.Count != barMainModelUp.nx)
        //                        //        {
        //                        //            return false;
        //                        //        }
        //                        //        else
        //                        //        {
        //                        //            ObservableCollection<BarModel> barModelEast = new ObservableCollection<BarModel>(BarModels.Where(x => (x.BarNumber > nx && x.BarNumber <= nx + ny - 2) && x.IsTopDowels && x.TopDowels == 0).ToList());
        //                        //            if (barModelEast.Count==0)
        //                        //            {
        //                        //                return false;
        //                        //            }
        //                        //            else
        //                        //            {
        //                        //                if (barModelEast.Count!=barMainModelUp.ny-2)
        //                        //                {
        //                        //                    return false;
        //                        //                }
        //                        //                else
        //                        //                {
        //                        //                    ObservableCollection<BarModel> barModelNouth = new ObservableCollection<BarModel>(BarModels.Where(x => (x.BarNumber > nx + ny - 2 && x.BarNumber <= 2 * nx + ny - 2) && x.IsTopDowels && x.TopDowels == 0).ToList());
        //                        //                    if (barModelNouth.Count==0)
        //                        //                    {
        //                        //                        return false;
        //                        //                    }
        //                        //                    else
        //                        //                    {
        //                        //                        if (barModelNouth.Count!=barMainModelUp.nx)
        //                        //                        {
        //                        //                            return false;
        //                        //                        }
        //                        //                        else
        //                        //                        {
        //                        //                             ObservableCollection<BarModel> barModelWest = new ObservableCollection<BarModel>(BarModels.Where(x => (x.BarNumber > 2 * nx + ny - 2) && x.IsTopDowels && x.TopDowels == 0).ToList());
        //                        //                            if (barModelWest.Count==0)
        //                        //                            {
        //                        //                                return false;
        //                        //                            }
        //                        //                            else
        //                        //                            {
        //                        //                                return (barModelWest.Count == barMainModelUp.ny - 2);
        //                        //                            }
        //                        //                        }
        //                        //                    }
        //                        //                }
        //                        //            }
        //                        //        }

        //                        //    }

        //                        //}
        //                        //else
        //                        //{
        //                        //    if (nd == 4)
        //                        //    {
        //                        //        return true;
        //                        //    }
        //                        //    else
        //                        //    {
        //                        //        ObservableCollection<BarModel> barModel0 = new ObservableCollection<BarModel>(BarModels.Where(x =>  x.IsTopDowels && x.TopDowels == 0).OrderBy(x => x.BarNumber).ToList());
        //                        //        return barModel0.Count == barMainModelUp.BarModels.Count;
        //                        //    }

        //                        //}
        //                        #endregion
        //                        ObservableCollection<BarModel> barModel0 = new ObservableCollection<BarModel>(BarModels.Where(x => x.IsTopDowels && x.TopDowels == 0).OrderBy(x => x.BarNumber).ToList());
        //                        return barModel0.Count == barMainModelUp.BarModels.Count;
        //                    }
        //                }

        //            }
        //        }
        //    }


        //    return true;
        //}
        //public bool ConditionFixedBottom(SectionStyle sectionStyle, InfoModel infoModelDown = null, BarMainModel barMainModelDown = null)
        //{
        //    if (BarModels.Count == 0)
        //    {
        //        return false;
        //    }
        //    else
        //    {
        //        if (infoModelDown == null)
        //        {
        //            return false;

        //        }
        //        else
        //        {
        //            if (barMainModelDown.BarModels.Count == 0)
        //            {
        //                return false;
        //            }
        //            else
        //            {
        //                if (!barMainModelDown.FixedTop)
        //                {
        //                    return false;
        //                }
        //                else
        //                {
        //                    ObservableCollection<BarModel> barModels = new ObservableCollection<BarModel>(BarModels.Where(x => x.IsBottomDowels && x.BottomDowels == 0).ToList());
        //                    if (barModels.Count!=BarModels.Count)
        //                    {
        //                        return false;
        //                    }
        //                    else
        //                    {
        //                        return true;
        //                    }
        //                }


        //            }
        //        }
        //    }


        //    return true;
        //}
        #endregion
        #region SetLocation
        private bool SetLeftRightBarsModel(InfoModel infoModel,BarModel barModel)
        {

            return (barModel.X0<infoModel.WestPosition+infoModel.L/2)||(PointModel.AreEqual(barModel.X0, infoModel.WestPosition + infoModel.L / 2));
        }
        #endregion
        public bool ConditionShowAddTop()
        {
            if (BarModels.Count == 0)
            {
                return false;
            }
            else
            {
                for (int i = 0; i < BarModels.Count; i++)
                {
                    if (BarModels[i].IsTopDowels && BarModels[i].TopDowels == 0)
                    {
                        return false;
                    }
                }
            }
            return true;
        }
    }
}
