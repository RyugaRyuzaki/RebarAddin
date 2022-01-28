using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace R10_WallShear
{
    public class DrawMainCanvas
    {
        #region Section
        public static void DrawSection(Canvas canvas, InfoModel infoModel, DrawModel drawModelSection, double Cover)
        {
            double left = drawModelSection.Left + infoModel.WestPosition / (drawModelSection.Scale);
            double top = drawModelSection.Top - infoModel.NouthPosition / (drawModelSection.Scale);
            DrawImage.DrawAxis(canvas, false);
            DrawImage.DrawSection(canvas, drawModelSection.Scale, left, top, infoModel.L, infoModel.T);

            DrawImage.DimVertical(canvas, drawModelSection.Left, top, drawModelSection.Scale, infoModel.T, 11, 20, 5);
            if (!PointModel.AreEqual(infoModel.SouthPosition, 0)) DrawImage.DimVertical(canvas, drawModelSection.Left, drawModelSection.Top - infoModel.SouthPosition / (drawModelSection.Scale), drawModelSection.Scale, infoModel.SouthPosition, 11, 20, 5);
            if (!infoModel.IsCorner)
            {
                DrawImage.DimHorizontal(canvas, left, drawModelSection.Top, drawModelSection.Scale, infoModel.L, 11, -20, -5);
            }
            else
            {
                DrawImage.DimHorizontal(canvas, left, drawModelSection.Top, drawModelSection.Scale, infoModel.L1, 11, -20, -5);
                DrawImage.DimHorizontal(canvas, left + infoModel.L1 / drawModelSection.Scale, drawModelSection.Top, drawModelSection.Scale, infoModel.L2, 11, -20, -5);
                DrawImage.DimHorizontal(canvas, left + infoModel.L1 / drawModelSection.Scale + infoModel.L2 / drawModelSection.Scale, drawModelSection.Top, drawModelSection.Scale, infoModel.L1, 11, -20, -5);
                DrawLineCorner(canvas, infoModel, drawModelSection);
            }

            if (!PointModel.AreEqual(infoModel.WestPosition, 0)) DrawImage.DimHorizontal(canvas, drawModelSection.Left, drawModelSection.Top, drawModelSection.Scale, infoModel.WestPosition, 11, -20, -5);
        }
        public static void DrawStirrupsAndSection(Canvas canvas, InfoModel infoModel, StirrupModel stirrupModel, BarMainModel barMainModel, DrawModel drawModelSection, double Cover, double d, double dc)
        {
            double left = drawModelSection.Left + infoModel.WestPosition / (drawModelSection.Scale);
            double top = drawModelSection.Top - infoModel.NouthPosition / (drawModelSection.Scale);
            DrawImage.DrawAxis(canvas, false);
            DrawImage.DrawSection(canvas, drawModelSection.Scale, left, top, infoModel.L, infoModel.T);
            DrawImage.DrawStirrup(canvas, left, top, drawModelSection.Scale, infoModel.L, infoModel.T, Cover, stirrupModel.BarS.Diameter, d, drawModelSection.ColorStirrupChoose);
            DrawImage.DimVertical(canvas, drawModelSection.Left, top, drawModelSection.Scale, infoModel.T, 11, 20, 5);
            if (!PointModel.AreEqual(infoModel.SouthPosition, 0)) DrawImage.DimVertical(canvas, drawModelSection.Left, drawModelSection.Top - infoModel.SouthPosition / (drawModelSection.Scale), drawModelSection.Scale, infoModel.SouthPosition, 11, 20, 5);
            if (infoModel.IsCorner)
            {
                DrawImage.DimHorizontal(canvas, left, drawModelSection.Top, drawModelSection.Scale, infoModel.L1, 11, -20, -5);
                DrawImage.DimHorizontal(canvas, left + infoModel.L1 / drawModelSection.Scale, drawModelSection.Top, drawModelSection.Scale, infoModel.L2, 11, -20, -5);
                DrawImage.DimHorizontal(canvas, left + infoModel.L1 / drawModelSection.Scale + infoModel.L2 / drawModelSection.Scale, drawModelSection.Top, drawModelSection.Scale, infoModel.L1, 11, -20, -5);
                DrawImage.DrawStirrup(canvas, left, top, drawModelSection.Scale, infoModel.L1, infoModel.T, Cover, stirrupModel.BarSCorner.Diameter, dc, drawModelSection.ColorMainBarChoose);
                DrawImage.DrawStirrup(canvas, left + (infoModel.L1 + infoModel.L2) / drawModelSection.Scale, top, drawModelSection.Scale, infoModel.L1, infoModel.T, Cover, stirrupModel.BarSCorner.Diameter, dc, drawModelSection.ColorMainBarChoose);
                DrawLineCorner(canvas, infoModel, drawModelSection);
            }
            else
            {
                DrawImage.DimHorizontal(canvas, left, drawModelSection.Top, drawModelSection.Scale, infoModel.L, 11, -20, -5);
            }
            if (!PointModel.AreEqual(infoModel.WestPosition, 0)) DrawImage.DimHorizontal(canvas, drawModelSection.Left, drawModelSection.Top, drawModelSection.Scale, infoModel.WestPosition, 11, -20, -5);
            if (barMainModel.BarModels.Count != 0)
            {
                SolidColorBrush solidColorBrush = drawModelSection.ColorMainBar;
                for (int i = 0; i < barMainModel.BarModels.Count; i++)
                {
                    double left1 = drawModelSection.Left + barMainModel.BarModels[i].X0 / drawModelSection.Scale;
                    double top1 = drawModelSection.Top - barMainModel.BarModels[i].Y0 / drawModelSection.Scale;
                    DrawImage.DrawOneBarSection(canvas, left1, top1, drawModelSection.Scale, barMainModel.Bar.Diameter, drawModelSection.ColorMainBar);
                }
            }
            if (barMainModel.BarCornerModels.Count != 0)
            {
                SolidColorBrush solidColorBrush = drawModelSection.ColorMainBar;
                for (int i = 0; i < barMainModel.BarCornerModels.Count; i++)
                {
                    double left1 = drawModelSection.Left + barMainModel.BarCornerModels[i].X0 / drawModelSection.Scale;
                    double top1 = drawModelSection.Top - barMainModel.BarCornerModels[i].Y0 / drawModelSection.Scale;
                    DrawImage.DrawOneBarSection(canvas, left1, top1, drawModelSection.Scale, barMainModel.BarCorner.Diameter, solidColorBrush);
                }
            }
        }
        public static void DrawAddStirrupsAndSection(Canvas canvas, InfoModel infoModel, StirrupModel stirrupModel, BarMainModel barMainModel, DrawModel drawModelSection, double Cover, double d)
        {
            double left = drawModelSection.Left + infoModel.WestPosition / (drawModelSection.Scale);
            double top = drawModelSection.Top - infoModel.NouthPosition / (drawModelSection.Scale);
            DrawImage.DrawAxis(canvas, false);
            DrawImage.DrawSection(canvas, drawModelSection.Scale, left, top, infoModel.L, infoModel.T);
            DrawImage.DrawStirrup(canvas, left, top, drawModelSection.Scale, infoModel.L, infoModel.T, Cover, stirrupModel.BarS.Diameter, d, drawModelSection.ColorStirrup);
            DrawImage.DimVertical(canvas, drawModelSection.Left, top, drawModelSection.Scale, infoModel.T, 11, 20, 5);
            if (!PointModel.AreEqual(infoModel.SouthPosition, 0)) DrawImage.DimVertical(canvas, drawModelSection.Left, drawModelSection.Top - infoModel.SouthPosition / (drawModelSection.Scale), drawModelSection.Scale, infoModel.SouthPosition, 11, 20, 5);
            if (infoModel.IsCorner)
            {
                DrawImage.DimHorizontal(canvas, left, drawModelSection.Top, drawModelSection.Scale, infoModel.L1, 11, -20, -5);
                DrawImage.DimHorizontal(canvas, left + infoModel.L1 / drawModelSection.Scale, drawModelSection.Top, drawModelSection.Scale, infoModel.L2, 11, -20, -5);
                DrawImage.DimHorizontal(canvas, left + infoModel.L1 / drawModelSection.Scale + infoModel.L2 / drawModelSection.Scale, drawModelSection.Top, drawModelSection.Scale, infoModel.L1, 11, -20, -5);
                DrawImage.DrawStirrup(canvas, left, top, drawModelSection.Scale, infoModel.L1, infoModel.T, Cover, stirrupModel.BarSCorner.Diameter, d, drawModelSection.ColorMainBarChoose);
                DrawImage.DrawStirrup(canvas, left + (infoModel.L1 + infoModel.L2) / drawModelSection.Scale, top, drawModelSection.Scale, infoModel.L1, infoModel.T, Cover, stirrupModel.BarSCorner.Diameter, d, drawModelSection.ColorMainBarChoose);
                DrawLineCorner(canvas, infoModel, drawModelSection);

                if (stirrupModel.AddH)
                {
                    if (stirrupModel.DistanceH > 0 && stirrupModel.DistanceH < infoModel.L2)
                    {
                        if (stirrupModel.TypeH == 0)
                        {
                            if (stirrupModel.aH > 0 && stirrupModel.aH < infoModel.L2 - 2 * Cover)
                            {
                                int n = 1;
                                double delta = stirrupModel.DistanceH;
                                if (barMainModel.BarModels.Count == 0)
                                {
                                    n = (int)((infoModel.L2) / stirrupModel.DistanceH);
                                }
                                else
                                {
                                    n = (barMainModel.nx % 2 == 0) ? barMainModel.nx / 2 : (barMainModel.nx - 1) / 2;
                                }
                                for (int i = 0; i < n; i++)
                                {
                                    DrawImage.DrawStirrup(canvas, left + infoModel.L1 / drawModelSection.Scale + (i) * delta / drawModelSection.Scale, top, drawModelSection.Scale, stirrupModel.aH + 2 * Cover, infoModel.T, Cover, stirrupModel.BarH.Diameter, d, drawModelSection.ColorMainBarChoose);
                                }
                            }
                        }
                        else
                        {
                            int n = 1;
                            double overight = Cover + stirrupModel.BarH.Diameter + d / 2;
                            double delta = 0;
                            if (barMainModel.BarModels.Count == 0)
                            {
                                delta = stirrupModel.DistanceH / 2;
                                n = (int)((infoModel.L2) / stirrupModel.DistanceH);
                            }
                            else
                            {
                                delta = (infoModel.L2 - 2 * Cover - 2 * stirrupModel.BarS.Diameter - d) / (barMainModel.nx - 1);
                                n = barMainModel.nx ;
                            }
                            double hook = Math.PI / 2;
                            switch (stirrupModel.TypeH)
                            {
                                case 1: hook = Math.PI / 2; break;
                                case 2: hook = Math.PI * 0.75; break;
                                case 3: hook = Math.PI; break;
                                default: hook = Math.PI / 2; break;
                            }
                            for (int i = 0; i < n; i++)
                            {
                                DrawImage.DrawHookVertical(canvas, left + infoModel.L1 / drawModelSection.Scale + overight / drawModelSection.Scale + (i) * stirrupModel.DistanceH / drawModelSection.Scale, top, drawModelSection.Scale, infoModel.T, Cover, stirrupModel.BarH.Diameter, d, hook, drawModelSection.ColorMainBarChoose);
                            }
                        }
                    }
                        

                }
                if (stirrupModel.AddHCorner)
                {
                    if (stirrupModel.TypeHCorner == 0)
                    {
                        if (stirrupModel.aHCorner > 0 && stirrupModel.aHCorner < infoModel.L1 - 2 * Cover)
                        {
                            double delta = infoModel.L1 / 2 - stirrupModel.aHCorner / 2;
                            DrawImage.DrawStirrup(canvas, left + delta / drawModelSection.Scale, top, drawModelSection.Scale, stirrupModel.aHCorner, infoModel.T, Cover, stirrupModel.BarHCorner.Diameter, d, drawModelSection.ColorMainBarChoose);
                            DrawImage.DrawStirrup(canvas, left + (infoModel.L1 + infoModel.L2 + delta) / drawModelSection.Scale, top, drawModelSection.Scale, stirrupModel.aHCorner, infoModel.T, Cover, stirrupModel.BarHCorner.Diameter, d, drawModelSection.ColorMainBarChoose);
                        }
                    }
                    else
                    {
                        double delta = (infoModel.L1 - 2 * Cover) / (stirrupModel.nHCorner + 1);
                        double hook = Math.PI / 2;
                        switch (stirrupModel.TypeHCorner)
                        {
                            case 1: hook = Math.PI / 2; break;
                            case 2: hook = Math.PI * 0.75; break;
                            case 3: hook = Math.PI; break;
                            default: hook = Math.PI / 2; break;
                        }
                        for (int i = 0; i < stirrupModel.nHCorner; i++)
                        {
                            DrawImage.DrawHookVertical(canvas, left + Cover / drawModelSection.Scale + (i + 1) * delta / drawModelSection.Scale, top, drawModelSection.Scale, infoModel.T, Cover, stirrupModel.BarHCorner.Diameter, d, hook, drawModelSection.ColorMainBarChoose);
                            DrawImage.DrawHookVertical(canvas, left + Cover / drawModelSection.Scale + (infoModel.L1 + infoModel.L2) / drawModelSection.Scale + (i + 1) * delta / drawModelSection.Scale, top, drawModelSection.Scale, infoModel.T, Cover, stirrupModel.BarHCorner.Diameter, d, hook, drawModelSection.ColorMainBarChoose);
                        }
                    }
                }
                if (stirrupModel.AddVCorner)
                {
                    if (stirrupModel.TypeVCorner == 0)
                    {

                        if (stirrupModel.aVCorner > 0 && stirrupModel.aVCorner < infoModel.T - 2 * Cover)
                        {
                            double delta = infoModel.T / 2 - stirrupModel.aVCorner / 2;
                            DrawImage.DrawStirrup(canvas, left, top + delta / drawModelSection.Scale, drawModelSection.Scale, infoModel.T, stirrupModel.aVCorner, Cover, stirrupModel.BarVCorner.Diameter, d, drawModelSection.ColorMainBarChoose);
                            DrawImage.DrawStirrup(canvas, left + (infoModel.L1 + infoModel.L2) / drawModelSection.Scale, top + delta / drawModelSection.Scale, drawModelSection.Scale, infoModel.T, stirrupModel.aVCorner, Cover, stirrupModel.BarVCorner.Diameter, d, drawModelSection.ColorMainBarChoose);
                        }
                    }
                    else
                    {
                        double delta = (infoModel.T - 2 * Cover) / (stirrupModel.nVCorner + 1);
                        double hook = Math.PI / 2;
                        switch (stirrupModel.TypeVCorner)
                        {
                            case 1: hook = Math.PI / 2; break;
                            case 2: hook = Math.PI * 0.75; break;
                            case 3: hook = Math.PI; break;
                            default: hook = Math.PI / 2; break;
                        }
                        for (int i = 0; i < stirrupModel.nVCorner; i++)
                        {
                            DrawImage.DrawHook(canvas, left, top + (Cover / drawModelSection.Scale) + (i + 1) * delta / drawModelSection.Scale, drawModelSection.Scale, infoModel.L1, Cover, stirrupModel.BarVCorner.Diameter, d, hook, drawModelSection.ColorMainBarChoose);
                            DrawImage.DrawHook(canvas, left + (infoModel.L1 + infoModel.L2) / drawModelSection.Scale, top + (Cover / drawModelSection.Scale) + (i + 1) * delta / drawModelSection.Scale, drawModelSection.Scale, infoModel.L1, Cover, stirrupModel.BarVCorner.Diameter, d, hook, drawModelSection.ColorMainBarChoose);
                        }
                    }
                }
            }
            else
            {
                DrawImage.DimHorizontal(canvas, left, drawModelSection.Top, drawModelSection.Scale, infoModel.L, 11, -20, -5);
                if (stirrupModel.AddH)
                {
                    if (stirrupModel.DistanceH > 0 && stirrupModel.DistanceH < infoModel.L)
                    {
                        if (stirrupModel.TypeH == 0)
                        {
                            if (stirrupModel.aH > 0 && stirrupModel.aH < infoModel.L - 2 * Cover)
                            {
                                int n = 1;
                                double delta = stirrupModel.DistanceH;
                                if (barMainModel.BarModels.Count == 0)
                                {
                                    n = (int)((infoModel.L) / stirrupModel.DistanceH);
                                }
                                else
                                {
                                    n = (barMainModel.nx % 2 == 0) ? barMainModel.nx / 2 : (barMainModel.nx - 1) / 2;
                                }
                                for (int i = 0; i < n; i++)
                                {
                                    DrawImage.DrawStirrup(canvas, left + (i) * delta / drawModelSection.Scale, top, drawModelSection.Scale, stirrupModel.aH + 2 * Cover, infoModel.T, Cover, stirrupModel.BarH.Diameter, d, drawModelSection.ColorMainBarChoose);
                                }
                            }
                        }
                        else
                        {
                            int n = 1;
                            double overight = Cover + stirrupModel.BarH.Diameter + d / 2;
                            double delta = 0;
                            if (barMainModel.BarModels.Count == 0)
                            {
                                delta = stirrupModel.DistanceH / 2;
                                n = (int)((infoModel.L) / stirrupModel.DistanceH);
                            }
                            else
                            {
                                delta = (infoModel.L - 2 * Cover - 2 * stirrupModel.BarS.Diameter - d) / (barMainModel.nx - 1);
                                n = barMainModel.nx - 2;
                            }


                            double hook = Math.PI / 2;
                            switch (stirrupModel.TypeH)
                            {
                                case 1: hook = Math.PI / 2; break;
                                case 2: hook = Math.PI * 0.75; break;
                                case 3: hook = Math.PI; break;
                                default: hook = Math.PI / 2; break;
                            }
                            for (int i = 0; i < n; i++)
                            {
                                DrawImage.DrawHookVertical(canvas, left + overight / drawModelSection.Scale + delta / drawModelSection.Scale + (i) * stirrupModel.DistanceH / drawModelSection.Scale, top, drawModelSection.Scale, infoModel.T, Cover, stirrupModel.BarH.Diameter, d, hook, drawModelSection.ColorMainBarChoose);
                            }
                        }
                    }
                       

                }
            }
            if (!PointModel.AreEqual(infoModel.WestPosition, 0)) DrawImage.DimHorizontal(canvas, drawModelSection.Left, drawModelSection.Top, drawModelSection.Scale, infoModel.WestPosition, 11, -20, -5);
            if (barMainModel.BarModels.Count != 0)
            {
                SolidColorBrush solidColorBrush = drawModelSection.ColorMainBar;
                for (int i = 0; i < barMainModel.BarModels.Count; i++)
                {

                    double left1 = drawModelSection.Left + barMainModel.BarModels[i].X0 / drawModelSection.Scale;
                    double top1 = drawModelSection.Top - barMainModel.BarModels[i].Y0 / drawModelSection.Scale;
                    DrawImage.DrawOneBarSection(canvas, left1, top1, drawModelSection.Scale, barMainModel.Bar.Diameter, drawModelSection.ColorMainBar);

                }
            }
            if (barMainModel.BarCornerModels.Count != 0)
            {
                SolidColorBrush solidColorBrush = drawModelSection.ColorMainBar;
                for (int i = 0; i < barMainModel.BarCornerModels.Count; i++)
                {
                    double left1 = drawModelSection.Left + barMainModel.BarCornerModels[i].X0 / drawModelSection.Scale;
                    double top1 = drawModelSection.Top - barMainModel.BarCornerModels[i].Y0 / drawModelSection.Scale;
                    DrawImage.DrawOneBarSection(canvas, left1, top1, drawModelSection.Scale, barMainModel.BarCorner.Diameter, solidColorBrush);
                }
            }

        }
        public static void DrawSectionMainBar(Canvas canvas, InfoModel infoModel, StirrupModel stirrupModel, BarMainModel barMainModel, DrawModel drawModelSection, double Cover, int chooseBar,int chooseBarCorner)
        {
            double left = drawModelSection.Left + infoModel.WestPosition / (drawModelSection.Scale);
            double top = drawModelSection.Top - infoModel.NouthPosition / (drawModelSection.Scale);
            DrawImage.DrawAxis(canvas, false);
            DrawImage.DrawSection(canvas, drawModelSection.Scale, left, top, infoModel.L, infoModel.T);
            DrawImage.DrawStirrup(canvas, left, top, drawModelSection.Scale, infoModel.L, infoModel.T, Cover, stirrupModel.BarS.Diameter, barMainModel.Bar.Diameter, drawModelSection.ColorStirrup);
            if (infoModel.IsCorner)
            {
                DrawImage.DrawStirrup(canvas, left, top, drawModelSection.Scale, infoModel.L1, infoModel.T, Cover, stirrupModel.BarSCorner.Diameter, barMainModel.BarCorner.Diameter, drawModelSection.ColorStirrup);
                DrawImage.DrawStirrup(canvas, left + (infoModel.L1 + infoModel.L2) / drawModelSection.Scale, top, drawModelSection.Scale, infoModel.L1, infoModel.T, Cover, stirrupModel.BarSCorner.Diameter, barMainModel.BarCorner.Diameter, drawModelSection.ColorStirrup);
                DrawLineCorner(canvas, infoModel, drawModelSection);
            }
            if (barMainModel.BarModels.Count != 0)
            {
                SolidColorBrush solidColorBrush = drawModelSection.ColorMainBar;
                for (int i = 0; i < barMainModel.BarModels.Count; i++)
                {
                    if (i == chooseBar)
                    {
                        solidColorBrush = drawModelSection.ColorMainBarChoose;
                    }
                    else
                    {
                        solidColorBrush = Brushes.Aqua;
                    }
                    double left1 = drawModelSection.Left + barMainModel.BarModels[i].X0 / drawModelSection.Scale;
                    double top1 = drawModelSection.Top - barMainModel.BarModels[i].Y0 / drawModelSection.Scale;
                    DrawImage.DrawOneBarSection(canvas, left1, top1, drawModelSection.Scale, barMainModel.Bar.Diameter, solidColorBrush);
                    double x0 = 0;
                    double y0 = 0;

                    Getx0y0Bar(i, barMainModel, out x0, out y0);
                    DrawImage.DrawTextOneBarSection(canvas, left1 + x0, top1 + y0, barMainModel.BarModels[i].BarNumber, drawModelSection.ColorMainBar);
                }
            }
            if (barMainModel.BarCornerModels.Count != 0)
            {
                SolidColorBrush solidColorBrush = drawModelSection.ColorMainBar;
                for (int i = 0; i < barMainModel.BarCornerModels.Count; i++)
                {
                    if (i == chooseBarCorner)
                    {
                        solidColorBrush = drawModelSection.ColorMainBarChoose;
                    }
                    else
                    {
                        solidColorBrush = Brushes.Orange;
                    }
                    double left1 = drawModelSection.Left + barMainModel.BarCornerModels[i].X0 / drawModelSection.Scale;
                    double top1 = drawModelSection.Top - barMainModel.BarCornerModels[i].Y0 / drawModelSection.Scale;
                    DrawImage.DrawOneBarSection(canvas, left1, top1, drawModelSection.Scale, barMainModel.BarCorner.Diameter, solidColorBrush);
                    double x0 = 0;
                    double y0 = 0;

                    Getx0y0BarCorner(i, barMainModel, out x0, out y0);
                    DrawImage.DrawTextOneBarSection(canvas, left1 + x0, top1 + y0, barMainModel.BarCornerModels[i].BarNumber, drawModelSection.ColorMainBar);
                }
            }
        }
        public static void DrawSectionTopDowels(Canvas canvas, InfoModel infoModel, StirrupModel stirrupModel, BarMainModel barMainModel, DrawModel drawModelSection, double Cover, int chooseBar, int chooseBarCorner,InfoModel infoModelup=null, StirrupModel stirrupModelUp=null, BarMainModel barMainModelUp=null)
        {
            double left = drawModelSection.Left + infoModel.WestPosition / (drawModelSection.Scale);
            double top = drawModelSection.Top - infoModel.NouthPosition / (drawModelSection.Scale);
            DrawImage.DrawAxis(canvas, false);
            DrawImage.DrawSectionDashArray(canvas, drawModelSection.Scale, left, top, infoModel.L, infoModel.T);
            DrawImage.DrawStirrupDashArray(canvas, left, top, drawModelSection.Scale, infoModel.L, infoModel.T, Cover, stirrupModel.BarS.Diameter, barMainModel.Bar.Diameter, drawModelSection.ColorStirrup);
            if (infoModel.IsCorner)
            {
                DrawImage.DrawStirrupDashArray(canvas, left, top, drawModelSection.Scale, infoModel.L1, infoModel.T, Cover, stirrupModel.BarSCorner.Diameter, barMainModel.BarCorner.Diameter, drawModelSection.ColorStirrup);
                DrawImage.DrawStirrupDashArray(canvas, left + (infoModel.L1 + infoModel.L2) / drawModelSection.Scale, top, drawModelSection.Scale, infoModel.L1, infoModel.T, Cover, stirrupModel.BarSCorner.Diameter, barMainModel.BarCorner.Diameter, drawModelSection.ColorStirrup);
                DrawLineCorner(canvas, infoModel, drawModelSection);
            }
            if (barMainModel.BarModels.Count != 0)
            {
                SolidColorBrush solidColorBrush = drawModelSection.ColorMainBar;
                for (int i = 0; i < barMainModel.BarModels.Count; i++)
                {
                    if (i == chooseBar)
                    {
                        solidColorBrush = drawModelSection.ColorMainBarChoose;
                    }
                    else
                    {
                        solidColorBrush = Brushes.Aqua;
                    }
                    //double left1 = drawModelSection.Left + barMainModel.BarModels[i].X0 / drawModelSection.Scale;
                    //double top1 = drawModelSection.Top - barMainModel.BarModels[i].Y0 / drawModelSection.Scale;
                    //DrawImage.DrawOneBarSection(canvas, left1, top1, drawModelSection.Scale, barMainModel.Bar.Diameter, solidColorBrush);
                    DrawSectionBarTopDowelsItem(canvas, drawModelSection, barMainModel.BarModels[i], solidColorBrush);
                }
                //DrawSectionBarTopDowels(canvas, drawModelSection, barMainModel, chooseBar);
            }
            if (barMainModel.BarCornerModels.Count != 0)
            {
                SolidColorBrush solidColorBrush = drawModelSection.ColorMainBar;
                for (int i = 0; i < barMainModel.BarCornerModels.Count; i++)
                {
                    if (i == chooseBarCorner)
                    {
                        solidColorBrush = drawModelSection.ColorMainBarChoose;
                    }
                    else
                    {
                        solidColorBrush = Brushes.Orange;
                    }
                    //double left1 = drawModelSection.Left + barMainModel.BarCornerModels[i].X0 / drawModelSection.Scale;
                    //double top1 = drawModelSection.Top - barMainModel.BarCornerModels[i].Y0 / drawModelSection.Scale;
                    //DrawImage.DrawOneBarSection(canvas, left1, top1, drawModelSection.Scale, barMainModel.BarCorner.Diameter, solidColorBrush);
                    DrawSectionBarTopDowelsItem(canvas, drawModelSection, barMainModel.BarCornerModels[i], solidColorBrush);
                }
                //DrawSectionBarCornerTopDowels(canvas, drawModelSection, barMainModel, chooseBarCorner);
            }
            if (infoModelup != null)
            {
                double leftUp = drawModelSection.Left + infoModelup.WestPosition / (drawModelSection.Scale);
                double topUp = drawModelSection.Top - infoModelup.NouthPosition / (drawModelSection.Scale);
                DrawImage.DrawSection(canvas, drawModelSection.Scale, leftUp, topUp, infoModelup.L, infoModelup.T);
                DrawImage.DrawStirrup(canvas, leftUp, topUp, drawModelSection.Scale, infoModelup.L, infoModelup.T, Cover, stirrupModelUp.BarS.Diameter, barMainModelUp.Bar.Diameter, drawModelSection.ColorStirrup);
                if (infoModelup.IsCorner)
                {
                    DrawImage.DrawStirrup(canvas, leftUp, topUp, drawModelSection.Scale, infoModelup.L1, infoModelup.T, Cover, stirrupModelUp.BarSCorner.Diameter, barMainModelUp.BarCorner.Diameter, drawModelSection.ColorStirrup);
                    DrawImage.DrawStirrup(canvas, leftUp + (infoModelup.L1 + infoModelup.L2) / drawModelSection.Scale, topUp, drawModelSection.Scale, infoModelup.L1, infoModelup.T, Cover, stirrupModelUp.BarSCorner.Diameter, barMainModelUp.BarCorner.Diameter, drawModelSection.ColorStirrup);
                }
            }
           
        }

        #endregion
        #region main
        public static void DrawInfoColumns(Canvas canvas, WallsModel wallsModel, int choose)
        {
            double p30 = wallsModel.InfoModels[0].BottomPosition;
            for (int i = 0; i < wallsModel.InfoModels.Count; i++)
            {
                DrawWall(canvas, wallsModel.DrawModel, wallsModel.InfoModels[i], p30, (choose == i));
            }
            DrawFoundationBottom(canvas, wallsModel.DrawModel.Left, wallsModel.DrawModel.Top, wallsModel.DrawModel.Scale, p30, wallsModel.DrawModel.StrokeBound, 50, wallsModel.DrawModel.ColorBound);
        }
        public static void DrawStirrup(Canvas canvas, WallsModel wallsModel, int choose)
        {

            for (int i = 0; i < wallsModel.StirrupModels.Count; i++)
            {
                DrawStirrupItem(canvas, wallsModel.DrawModel, wallsModel.InfoModels[i], wallsModel.StirrupModels[i], wallsModel.Cover, (i == choose));
            }
        }
        #endregion
        #region InfoColumn

        private static void DrawWall(Canvas canvas, DrawModel drawModel, InfoModel infoModel, double p30, bool choose)
        {
            DrawWallItem(canvas, drawModel.Left, drawModel.Top, drawModel.Scale, infoModel.WestPosition, infoModel.EastPosition, infoModel.BottomPosition, infoModel.TopPosition, drawModel.StrokeBound, drawModel.ColorBound, drawModel.ColorFill, choose);
            DrawWallItemLevel(canvas, drawModel, infoModel);
            DrawWallItemBeamTopLevel(canvas, drawModel.Left, drawModel.Top, drawModel.Scale, infoModel.WestPosition, infoModel.EastPosition, infoModel.BottomPosition, infoModel.TopPosition, drawModel.StrokeBound, 20, infoModel.hb, infoModel.zb, drawModel.ColorBound);
            DrawImage.DimVertical(canvas, drawModel.Left - 40, drawModel.Top - infoModel.TopPosition / drawModel.Scale, drawModel.Scale, (infoModel.TopPosition - infoModel.BottomPosition), 11, 40, 5);
            DrawWallItemDimBeamTopLevel(canvas, drawModel.Left, drawModel.Top, drawModel.Scale, infoModel.TopPosition, 40, infoModel.hb, infoModel.zb);
        }

        private static void DrawWallItem(Canvas canvas, double left, double top, double scale, double p1, double p2, double p3, double p4, double stroke, SolidColorBrush solidColorBrush, SolidColorBrush chooseColor, bool choose)
        {
            Line l1 = new Line() { X1 = left + p1 / scale, X2 = left + p1 / scale, Y1 = top - p3 / scale, Y2 = top - p4 / scale };
            l1.Stroke = solidColorBrush;
            l1.StrokeThickness = stroke;
            canvas.Children.Add(l1);
            Line l2 = new Line() { X1 = left + p2 / scale, X2 = left + p2 / scale, Y1 = top - p3 / scale, Y2 = top - p4 / scale };
            l2.Stroke = solidColorBrush;
            l2.StrokeThickness = stroke;
            canvas.Children.Add(l2);
            Line l3 = new Line() { X1 = left + p1 / scale, X2 = left + p2 / scale, Y1 = top - p3 / scale, Y2 = top - p3 / scale };
            l3.Stroke = solidColorBrush;
            l3.StrokeThickness = stroke;
            canvas.Children.Add(l3);
            Line l4 = new Line() { X1 = left + p1 / scale, X2 = left + p2 / scale, Y1 = top - p4 / scale, Y2 = top - p4 / scale };
            l4.Stroke = solidColorBrush;
            l4.StrokeThickness = stroke;
            canvas.Children.Add(l4);
            if (choose)
            {
                Rectangle r = new Rectangle() { Width = (p2 - p1) / scale, Height = (p4 - p3) / scale };
                r.Fill = chooseColor;
                Canvas.SetTop(r, top - p4 / scale);
                Canvas.SetLeft(r, left + p1 / scale);
                canvas.Children.Add(r);
            }
            DrawImage.DimHorizontal(canvas, left + p1 / scale, top - (p4 + p3) * 0.5 / scale, scale, Math.Abs(p2 - p1), 11, 20, 5);
        }

        public static void DrawWallItemLevel(Canvas canvas, DrawModel drawModel, InfoModel infoModel)
        {
            Line l1 = new Line()
            {
                X1 = 0,
                X2 = 600,
                Y1 = drawModel.Top - (infoModel.BottomPosition - infoModel.eB) / drawModel.Scale,
                Y2 = drawModel.Top - (infoModel.BottomPosition - infoModel.eB) / drawModel.Scale
            };
            l1.Stroke = drawModel.ColorBound;
            l1.StrokeThickness = drawModel.StrokeBound;
            l1.StrokeDashArray = new DoubleCollection() { 10, 5, 2 };
            canvas.Children.Add(l1);
            TextBlock text = new TextBlock();
            text.Text = infoModel.BottomLevel.Name;
            text.FontSize = 15;
            text.Foreground = Brushes.Black;
            text.FontFamily = new FontFamily("Tahoma");
            text.Measure(new Size(System.Double.PositiveInfinity, System.Double.PositiveInfinity));
            text.Arrange(new Rect(text.DesiredSize));
            Canvas.SetTop(text, drawModel.Top - (infoModel.BottomPosition - infoModel.eB) / drawModel.Scale);
            Canvas.SetLeft(text, 0);
            canvas.Children.Add(text);
            Line l2 = new Line()
            {
                X1 = 0,
                X2 = 600,
                Y1 = drawModel.Top - (infoModel.TopPosition - infoModel.eT) / drawModel.Scale,
                Y2 = drawModel.Top - (infoModel.TopPosition - infoModel.eT) / drawModel.Scale
            };
            l2.Stroke = drawModel.ColorBound;
            l2.StrokeThickness = drawModel.StrokeBound;
            l2.StrokeDashArray = new DoubleCollection() { 10, 5, 2 };
            TextBlock text1 = new TextBlock();
            text1.Text = infoModel.TopLevel.Name;
            text1.FontSize = 15;
            text1.Foreground = Brushes.Black;
            text1.FontFamily = new FontFamily("Tahoma");
            text1.Measure(new Size(System.Double.PositiveInfinity, System.Double.PositiveInfinity));
            text1.Arrange(new Rect(text.DesiredSize));
            Canvas.SetTop(text1, drawModel.Top - (infoModel.TopPosition - infoModel.eT) / drawModel.Scale);
            Canvas.SetLeft(text1, 0);
            canvas.Children.Add(text1);
            canvas.Children.Add(l2);
        }
        private static void DrawWallItemBeamTopLevel(Canvas canvas, double left, double top, double scale, double p1, double p2, double p3, double p4, double stroke, double offset, double hb, double zb, SolidColorBrush solidColorBrush)
        {
            if (!PointModel.AreEqual(hb + zb, 0))
            {
                Line l1 = new Line() { X1 = left - offset, X2 = left + p1 / scale, Y1 = top - (p4 - zb) / scale, Y2 = top - (p4 - zb) / scale };
                l1.Stroke = solidColorBrush;
                l1.StrokeThickness = stroke;
                canvas.Children.Add(l1);
                Line l2 = new Line() { X1 = left - offset, X2 = left + p1 / scale, Y1 = top - (p4 - zb - hb) / scale, Y2 = top - (p4 - zb - hb) / scale };
                l2.Stroke = solidColorBrush;
                l2.StrokeThickness = stroke;
                canvas.Children.Add(l2);
                Line l3 = new Line() { X1 = left - offset, X2 = left - offset, Y1 = top - (p4 - zb) / scale, Y2 = top - (p4 - zb - hb) / scale };
                l3.Stroke = solidColorBrush;
                l3.StrokeThickness = stroke;
                l3.StrokeDashArray = new DoubleCollection() { 5, 2 };
                canvas.Children.Add(l3);
                Line l4 = new Line() { X1 = left + p2 / scale, X2 = left + p2 / scale + offset, Y1 = top - (p4 - zb) / scale, Y2 = top - (p4 - zb) / scale };
                l4.Stroke = solidColorBrush;
                l4.StrokeThickness = stroke;
                canvas.Children.Add(l4);
                Line l5 = new Line() { X1 = left + p2 / scale, X2 = left + p2 / scale + offset, Y1 = top - (p4 - zb - hb) / scale, Y2 = top - (p4 - zb - hb) / scale };
                l5.Stroke = solidColorBrush;
                l5.StrokeThickness = stroke;
                canvas.Children.Add(l5);
                Line l6 = new Line() { X1 = left + p2 / scale + offset, X2 = left + p2 / scale + offset, Y1 = top - (p4 - zb) / scale, Y2 = top - (p4 - zb - hb) / scale };
                l6.Stroke = solidColorBrush;
                l6.StrokeThickness = stroke;
                l6.StrokeDashArray = new DoubleCollection() { 5, 2 };
                canvas.Children.Add(l6);
            }

        }
        private static void DrawWallItemDimBeamTopLevel(Canvas canvas, double left, double top, double scale, double p4, double offset, double hb, double zb)
        {
            if (!PointModel.AreEqual(hb + zb, 0))
            {
                DrawImage.DimVertical(canvas, left - offset, top - (p4 - zb) / scale, scale, hb, 11, 20, 5);
                if (!PointModel.AreEqual(zb, 0))
                {
                    DrawImage.DimVertical(canvas, left - offset, top - (p4) / scale, scale, zb, 11, 20, 5);
                }
            }

        }

        public static void DrawFoundationBottom(Canvas canvas, double left, double top, double scale, double p30, double stroke, double offset, SolidColorBrush solidColorBrush)
        {
            if (!PointModel.AreEqual(p30, 0))
            {
                Line l1 = new Line() { X1 = left - offset, X2 = 640 - (left - offset), Y1 = top, Y2 = top };
                l1.Stroke = solidColorBrush;
                l1.StrokeThickness = stroke;
                canvas.Children.Add(l1);
                Line l2 = new Line() { X1 = left - offset, X2 = 640 - (left - offset), Y1 = top - p30 / scale, Y2 = top - p30 / scale };
                l2.Stroke = solidColorBrush;
                l2.StrokeThickness = stroke;
                canvas.Children.Add(l2);
                Line l3 = new Line() { X1 = left - offset, X2 = left - offset, Y1 = top, Y2 = top - p30 / scale };
                l3.Stroke = solidColorBrush;
                l3.StrokeThickness = stroke;
                l3.StrokeDashArray = new DoubleCollection() { 5, 2 };
                canvas.Children.Add(l3);
                Line l4 = new Line() { X1 = 640 - (left - offset), X2 = 640 - (left - offset), Y1 = top, Y2 = top - p30 / scale };
                l4.Stroke = solidColorBrush;
                l4.StrokeThickness = stroke;
                l4.StrokeDashArray = new DoubleCollection() { 5, 2 };
                canvas.Children.Add(l4);
                DrawImage.DimVertical(canvas, left - offset, top - (p30) / scale, scale, p30, 11, 30, 5);
            }
        }
        #endregion
        #region Stirrup
        private static void DrawStirrupItem(Canvas canvas, DrawModel drawModel, InfoModel infoModel, StirrupModel stirrupModel, double Cover, bool choose)
        {
            if (stirrupModel.TypeDis == 0)
            {

                DrawStirrupItem0(canvas, drawModel.Left, drawModel.Top, drawModel.Scale, infoModel.WestPosition, infoModel.EastPosition, infoModel.BottomPosition, Cover, drawModel.StrokeStirrup, stirrupModel, choose ? drawModel.ColorStirrupChoose : drawModel.ColorStirrup);
            }
            else
            {
                DrawStirrupItem1(canvas, drawModel.Left, drawModel.Top, drawModel.Scale, infoModel.WestPosition, infoModel.EastPosition, infoModel.BottomPosition, Cover, drawModel.StrokeStirrup, stirrupModel, choose ? drawModel.ColorStirrupChoose : drawModel.ColorStirrup);
            }
        }

        private static void DrawStirrupItem0(Canvas canvas, double left, double top, double scale, double p1, double p2, double p3, double Cover, double stroke, StirrupModel stirrupModel, SolidColorBrush solidColorBrush)
        {
            if (stirrupModel.S != 0)
            {
                int n = (int)(stirrupModel.L / stirrupModel.S);
                double del = 0.5 * (stirrupModel.L - n * stirrupModel.S);

                for (int i = 0; i < n + 1; i++)
                {
                    Line l1 = new Line() { X1 = left + p1 / scale + Cover / scale, X2 = left + p2 / scale - Cover / scale, Y1 = top - p3 / scale - del / scale - i * stirrupModel.S / scale, Y2 = top - p3 / scale - del / scale - i * stirrupModel.S / scale };
                    l1.Stroke = solidColorBrush;
                    l1.StrokeThickness = stroke;
                    canvas.Children.Add(l1);
                }
            }
        }
        private static void DrawStirrupItem1(Canvas canvas, double left, double top, double scale, double p1, double p2, double p3, double Cover, double stroke, StirrupModel stirrupModel, SolidColorBrush solidColorBrush)
        {
            if (stirrupModel.S1 != 0 && stirrupModel.S2 != 0)
            {
                int n1 = (int)(stirrupModel.L1 / stirrupModel.S1);
                double del1 = 0.5 * (stirrupModel.L1 - n1 * stirrupModel.S1);
                for (int i = 0; i < n1 + 1; i++)
                {
                    Line l1 = new Line() { X1 = left + p1 / scale + Cover / scale, X2 = left + p2 / scale - Cover / scale, Y1 = top - p3 / scale - del1 / scale - i * stirrupModel.S1 / scale, Y2 = top - p3 / scale - del1 / scale - i * stirrupModel.S1 / scale };
                    l1.Stroke = solidColorBrush;
                    l1.StrokeThickness = stroke;
                    canvas.Children.Add(l1);
                }
                int n2 = (int)(stirrupModel.L2 / stirrupModel.S2);
                double del2 = 0.5 * (stirrupModel.L2 - n2 * stirrupModel.S2);
                for (int i = 0; i < n2 + 1; i++)
                {
                    Line l1 = new Line() { X1 = left + p1 / scale + Cover / scale, X2 = left + p2 / scale - Cover / scale, Y1 = top - p3 / scale - del2 / scale - stirrupModel.L1 / scale - i * stirrupModel.S2 / scale, Y2 = top - p3 / scale - del2 / scale - stirrupModel.L1 / scale - i * stirrupModel.S2 / scale };
                    l1.Stroke = solidColorBrush;
                    l1.StrokeThickness = stroke;
                    canvas.Children.Add(l1);
                }
                for (int i = 0; i < n1 + 1; i++)
                {
                    Line l1 = new Line() { X1 = left + p1 / scale + Cover / scale, X2 = left + p2 / scale - Cover / scale, Y1 = top - p3 / scale - del1 / scale - stirrupModel.L1 / scale - stirrupModel.L2 / scale - i * stirrupModel.S1 / scale, Y2 = top - p3 / scale - del1 / scale - stirrupModel.L1 / scale - stirrupModel.L2 / scale - i * stirrupModel.S1 / scale };
                    l1.Stroke = solidColorBrush;
                    l1.StrokeThickness = stroke;
                    canvas.Children.Add(l1);
                }
            }
        }
        #endregion
        #region BarMain


        public static void DrawBarMains(Canvas canvas, DrawModel drawModel, BarMainModel barMainModel, int numberBar,int numberBarCorner)
        {
            if (barMainModel.BarModels.Count != 0)
            {
                ObservableCollection<BarModel> XbarModels = new ObservableCollection<BarModel>(barMainModel.BarModels.Where(x => (x.BarNumber <= barMainModel.nx)).ToList());
                DrawBarMainItem(canvas, drawModel.Left, drawModel.Top, drawModel.Scale, drawModel.StrokeMain, drawModel, XbarModels, numberBar);
            }
            if (barMainModel.BarCornerModels.Count != 0)
            {
                int total = 2 * barMainModel.nxCorner + 2 * (barMainModel.nyCorner - 2);
                ObservableCollection<BarModel> XbarCornerModels = new ObservableCollection<BarModel>(barMainModel.BarCornerModels.Where(x => (x.BarNumber <= barMainModel.nxCorner)||(x.BarNumber>total&&x.BarNumber<=total+barMainModel.nxCorner)).ToList());
                DrawBarMainItem(canvas, drawModel.Left, drawModel.Top, drawModel.Scale, drawModel.StrokeMain, drawModel, XbarCornerModels, numberBarCorner);
            }
        }

        private static void DrawBarMainItem(Canvas canvas, double left, double top, double scale, double stroke, DrawModel drawModel, ObservableCollection<BarModel> barModels, int numberBar)
        {
            if (barModels.Count != 0)
            {

                for (int i = 0; i < barModels.Count; i++)
                {
                    if (barModels[i].Location.Count > 1)
                    {
                        for (int j = 1; j < barModels[i].Location.Count; j++)
                        {
                            double x1 = (barModels[i].Location[j - 1].X) / scale;
                            double x2 = (barModels[i].Location[j].X) / scale;
                            Line l1 = new Line() { X1 = left + x1, X2 = left + x2, Y1 = top - barModels[i].Location[j - 1].Z / scale, Y2 = top - barModels[i].Location[j].Z / scale };
                            l1.Stroke = (barModels[i].BarNumber == numberBar) ? drawModel.ColorMainBarChoose : drawModel.ColorMainBar;
                            l1.StrokeThickness = stroke;
                            canvas.Children.Add(l1);
                        }
                    }
                }
            }

        }

        #endregion
        #region Dowels

        private static void DrawSectionBarTopDowels(Canvas canvas, DrawModel drawModelSection, BarMainModel barMainModel, int choose)
        {
            SolidColorBrush solidColorBrush = drawModelSection.ColorMainBar;
            for (int i = 0; i < barMainModel.BarModels.Count; i++)
            {
                if (barMainModel.BarModels[i].IsTopDowels)
                {
                    if (i == choose)
                    {
                        solidColorBrush = drawModelSection.ColorMainBarChoose;
                    }
                    else
                    {
                        if (barMainModel.BarModels[i].TopDowels == 0)
                        {
                            if (barMainModel.BarModels[i].BarNumber % 2 == 0)
                            {
                                solidColorBrush = Brushes.Aqua;
                            }
                            else
                            {
                                solidColorBrush = Brushes.Orange;
                            }
                        }
                        else
                        {
                            solidColorBrush = drawModelSection.ColorMainBar;
                        }

                    }
                    DrawSectionBarTopDowelsItem(canvas, drawModelSection, barMainModel.BarModels[i], solidColorBrush);
                }
            }

        }
        private static void DrawSectionBarCornerTopDowels(Canvas canvas, DrawModel drawModelSection, BarMainModel barMainModel, int choose)
        {
            SolidColorBrush solidColorBrush = drawModelSection.ColorMainBar;
            for (int i = 0; i < barMainModel.BarCornerModels.Count; i++)
            {
                if (barMainModel.BarCornerModels[i].IsTopDowels)
                {
                    if (i == choose)
                    {
                        solidColorBrush = drawModelSection.ColorMainBarChoose;
                    }
                    else
                    {
                        if (barMainModel.BarCornerModels[i].TopDowels == 0)
                        {
                            if (barMainModel.BarModels[i].BarNumber % 2 == 0)
                            {
                                solidColorBrush = Brushes.Aqua;
                            }
                            else
                            {
                                solidColorBrush = Brushes.Orange;
                            }
                        }
                        else
                        {
                            solidColorBrush = drawModelSection.ColorMainBar;
                        }

                    }
                    DrawSectionBarTopDowelsItem(canvas, drawModelSection, barMainModel.BarCornerModels[i], solidColorBrush);
                }
            }

        }
        private static void DrawSectionBarTopDowelsItem(Canvas canvas, DrawModel drawModelSection, BarModel barModel, SolidColorBrush solidColorBrush)
        {
            if (barModel.TopDowels == 0)
            {
                //if (!PointModel.AreEqual(barModel.Location[barModel.Location.Count - 3].Y, barModel.Location[barModel.Location.Count - 2].Y))
                //{
                //    DrawImage.DrawLineItemDowels(canvas, drawModelSection.Left, drawModelSection.Top, drawModelSection.Scale, barModel.Location[barModel.Location.Count - 3], barModel.Location[barModel.Location.Count - 2], solidColorBrush, false, barModel.Bar.Diameter);
                //}
               
                DrawImage.DrawOneBarSection(canvas, drawModelSection.Left + barModel.Location[barModel.Location.Count - 1].X / drawModelSection.Scale, drawModelSection.Top - barModel.Location[barModel.Location.Count - 1].Y / drawModelSection.Scale, drawModelSection.Scale, barModel.Bar.Diameter, solidColorBrush);
            }
            else
            {
                if (barModel.LaTopDowels != 0)
                {
                    DrawImage.DrawLineItemDowels(canvas, drawModelSection.Left, drawModelSection.Top, drawModelSection.Scale, barModel.Location[barModel.Location.Count - 2], barModel.Location[barModel.Location.Count - 1], solidColorBrush, false, barModel.Bar.Diameter);
                }
                DrawImage.DrawOneBarSection(canvas, drawModelSection.Left + barModel.Location[barModel.Location.Count - 2].X / drawModelSection.Scale, drawModelSection.Top - barModel.Location[barModel.Location.Count - 2].Y / drawModelSection.Scale, drawModelSection.Scale, barModel.Bar.Diameter, solidColorBrush);
            }
        }
        //private static void DrawAddBarTopDowelsItem(Canvas canvas, DrawModel drawModelDowels, BarModel barModel, SolidColorBrush solidColorBrush)
        //{
        //    DrawImage.DrawLineItemDowels(canvas, drawModelDowels.Left, drawModelDowels.Top, drawModelDowels.Scale, barModel.Location[0], barModel.Location[1], solidColorBrush, true, barModel.Bar.Diameter);
        //    DrawImage.DrawOneBarSection(canvas, drawModelDowels.Left + barModel.Location[1].X / drawModelDowels.Scale, drawModelDowels.Top - barModel.Location[1].Y / drawModelDowels.Scale, drawModelDowels.Scale, barModel.Bar.Diameter, solidColorBrush);
        //}
        //public static void DrawBarBottomDowels(Canvas canvas, DrawModel drawModelDowels, BarMainModel barMainModel, int choose, BarMainModel barMainModelDown=null)
        //{
        //    if (barMainModel.BarModels.Count != 0)
        //    {
        //        SolidColorBrush solidColorBrush = drawModelDowels.ColorMainBar;
        //        for (int i = 0; i < barMainModel.BarModels.Count; i++)
        //        {
        //            if (barMainModel.BarModels[i].IsBottomDowels)
        //            {
        //                if (i == choose)
        //                {
        //                    solidColorBrush = drawModelDowels.ColorMainBarChoose;
        //                }
        //                else
        //                {
        //                    solidColorBrush = drawModelDowels.ColorMainBar;

        //                }
        //                DrawBarBottomDowelsItem(canvas, drawModelDowels, barMainModel.BarModels[i], solidColorBrush);
        //            }
        //        }
        //    }
        //    if (barMainModelDown != null)
        //    {
        //        if (barMainModelDown.BarModels.Count!=0)
        //        {
        //            for (int i = 0; i < barMainModelDown.BarModels.Count; i++)
        //            {

        //                SolidColorBrush solidColorBrush = drawModelDowels.ColorMainBar;
        //                if (barMainModelDown.BarModels[i].IsTopDowels&& barMainModelDown.BarModels[i].TopDowels==0)
        //                {
        //                    if (barMainModelDown.BarModels[i].BarNumber%2==0)
        //                    {
        //                        solidColorBrush = Brushes.Aqua;
        //                    }
        //                    else
        //                    {
        //                        solidColorBrush = Brushes.Orange;
        //                    }
        //                    DrawImage.DrawLineItemDowels(canvas, drawModelDowels.Left, drawModelDowels.Top, drawModelDowels.Scale, barMainModelDown.BarModels[i].Location[barMainModelDown.BarModels[i].Location.Count - 3], barMainModelDown.BarModels[i].Location[barMainModelDown.BarModels[i].Location.Count - 2], solidColorBrush, false, barMainModelDown.BarModels[i].Bar.Diameter);
        //                    DrawImage.DrawOneBarSection(canvas, drawModelDowels.Left + barMainModelDown.BarModels[i].Location[barMainModelDown.BarModels[i].Location.Count - 2].X / drawModelDowels.Scale, drawModelDowels.Top - barMainModelDown.BarModels[i].Location[barMainModelDown.BarModels[i].Location.Count - 2].Y / drawModelDowels.Scale, drawModelDowels.Scale, barMainModelDown.BarModels[i].Bar.Diameter, solidColorBrush);
        //                }
        //            }

        //        }
        //        if (barMainModelDown.AddBarModels.Count!=0)
        //        {
        //            SolidColorBrush solidColorBrush = drawModelDowels.ColorMainBar;
        //            for (int i = 0; i < barMainModelDown.AddBarModels.Count; i++)
        //            {
        //                if (barMainModelDown.BarModels[i].BarNumber % 2 == 0)
        //                {
        //                    solidColorBrush = Brushes.Aqua;
        //                }
        //                else
        //                {
        //                    solidColorBrush = Brushes.Orange;
        //                }
        //                DrawImage.DrawLineItemDowels(canvas, drawModelDowels.Left, drawModelDowels.Top, drawModelDowels.Scale, barMainModelDown.AddBarModels[i].Location[0], barMainModelDown.AddBarModels[i].Location[1], solidColorBrush, false, barMainModelDown.AddBarModels[i].Bar.Diameter);
        //                DrawImage.DrawOneBarSection(canvas, drawModelDowels.Left + barMainModelDown.AddBarModels[i].Location[1].X / drawModelDowels.Scale, drawModelDowels.Top - barMainModelDown.AddBarModels[i].Location[1].Y / drawModelDowels.Scale, drawModelDowels.Scale, barMainModelDown.AddBarModels[i].Bar.Diameter, solidColorBrush);
        //            }
        //        }
        //    }
        //}
        //private static void DrawBarBottomDowelsItem(Canvas canvas, DrawModel drawModelDowels, BarModel barModel, SolidColorBrush solidColorBrush)
        //{
        //    if (barModel.BottomDowels == 0)
        //    {
        //        DrawImage.DrawOneBarSection(canvas, drawModelDowels.Left + barModel.Location[0].X / drawModelDowels.Scale, drawModelDowels.Top - barModel.Location[0].Y / drawModelDowels.Scale, drawModelDowels.Scale, barModel.Bar.Diameter, Brushes.Black);
        //    }
        //    else
        //    {
        //        if (barModel.LaBottomDowels != 0)
        //        {
        //            DrawImage.DrawLineItemDowels(canvas, drawModelDowels.Left, drawModelDowels.Top, drawModelDowels.Scale, barModel.Location[0], barModel.Location[1], solidColorBrush, false, barModel.Bar.Diameter);
        //        }
        //        DrawImage.DrawOneBarSection(canvas, drawModelDowels.Left + barModel.Location[1].X / drawModelDowels.Scale, drawModelDowels.Top - barModel.Location[1].Y / drawModelDowels.Scale, drawModelDowels.Scale, barModel.Bar.Diameter, Brushes.Black);
        //    }
        //}
        #endregion
        #region Item
        private static void DrawLineCorner(Canvas canvas, InfoModel infoModel, DrawModel drawModelSection)
        {
            Line l1 = new Line() { X1 = drawModelSection.Left + infoModel.L1 / drawModelSection.Scale + infoModel.WestPosition / drawModelSection.Scale, X2 = drawModelSection.Left + infoModel.L1 / drawModelSection.Scale + infoModel.WestPosition / drawModelSection.Scale, Y1 = drawModelSection.Top - infoModel.NouthPosition / drawModelSection.Scale, Y2 = drawModelSection.Top - infoModel.SouthPosition / drawModelSection.Scale };
            l1.Stroke = drawModelSection.ColorBound;
            l1.StrokeThickness = drawModelSection.StrokeBound;
            l1.StrokeDashArray = new DoubleCollection() { 4, 2 };
            canvas.Children.Add(l1);
            Line l2 = new Line() { X1 = drawModelSection.Left + (infoModel.L1 + infoModel.L2) / drawModelSection.Scale + infoModel.WestPosition / drawModelSection.Scale, X2 = drawModelSection.Left + (infoModel.L1 + infoModel.L2) / drawModelSection.Scale + infoModel.WestPosition / drawModelSection.Scale, Y1 = drawModelSection.Top - infoModel.NouthPosition / drawModelSection.Scale, Y2 = drawModelSection.Top - infoModel.SouthPosition / drawModelSection.Scale };
            l2.Stroke = drawModelSection.ColorBound;
            l2.StrokeThickness = drawModelSection.StrokeBound;
            l2.StrokeDashArray = new DoubleCollection() { 4, 2 };
            canvas.Children.Add(l2);
        }
        private static void Getx0y0Bar(int i, BarMainModel barMainModel, out double x0, out double y0)
        {
            x0 = 0; y0 = 0;
            if (i == 0)
            {
                x0 = 11;
            }
            if (i < barMainModel.nx)
            {
                y0 = -11;
            }
            if (i == barMainModel.nx - 1)
            {
                x0 = -11;
            }
            if (i >= barMainModel.nx && i <= barMainModel.nx + barMainModel.ny - 2)
            {
                x0 = -11;
                if (i == barMainModel.nx + barMainModel.ny - 2)
                {
                    y0 = 15;
                }
            }
            if (i > barMainModel.nx + barMainModel.ny - 2 && i <= barMainModel.nx + barMainModel.ny - 2 + barMainModel.nx - 1)
            {
                y0 = 15;
                if (i == barMainModel.nx + barMainModel.ny - 2 + barMainModel.nx - 1)
                {
                    x0 = 11;
                }
            }
            if (i > barMainModel.nx + barMainModel.ny - 2 + barMainModel.nx - 1)
            {
                x0 = 11;
            }
        }
        private static void Getx0y0BarCorner(int i, BarMainModel barMainModel, out double x0, out double y0)
        {
            x0 = 0; y0 = 0;
            if (i == 0)
            {
                x0 = 11;
            }
            if (i < barMainModel.nxCorner)
            {
                y0 = -11;
            }
            if (i == barMainModel.nxCorner - 1)
            {
                x0 = -11;
            }
            if (i >= barMainModel.nxCorner
                && i <= barMainModel.nxCorner + barMainModel.nyCorner - 2)
            {
                x0 = -11;
                if (i == barMainModel.nxCorner + barMainModel.nyCorner - 2)
                {
                    y0 = 15;
                }
            }
            if (i > barMainModel.nxCorner + barMainModel.nyCorner - 2 && i <= barMainModel.nxCorner + barMainModel.nyCorner - 2 + barMainModel.nxCorner - 1)
            {
                y0 = 15;
                if (i == barMainModel.nxCorner + barMainModel.nyCorner - 2 + barMainModel.nxCorner - 1)
                {
                    x0 = 11;
                }
            }
            int a = 2 * (barMainModel.nxCorner) + 2 * (barMainModel.nyCorner - 2);
            if (i > barMainModel.nxCorner + barMainModel.nyCorner - 2 + barMainModel.nxCorner - 1 && i < a)
            {
                x0 = 11;
            }

            if (i == 0 + a)
            {
                x0 = 11;
            }
            if (i < barMainModel.nxCorner + a)
            {
                y0 = -11;
            }
            if (i == barMainModel.nxCorner - 1 + a)
            {
                x0 = -11;
            }
            if (i >= barMainModel.nxCorner + a
                && i <= barMainModel.nxCorner + barMainModel.nyCorner - 2 + a)
            {
                x0 = -11;
                if (i == barMainModel.nxCorner + barMainModel.nyCorner - 2 + a)
                {
                    y0 = 15;
                }
            }
            if (i > barMainModel.nxCorner + barMainModel.nyCorner - 2 + a && i <= barMainModel.nxCorner + barMainModel.nyCorner - 2 + barMainModel.nxCorner - 1 + a)
            {
                y0 = 15;
                if (i == barMainModel.nxCorner + barMainModel.nyCorner - 2 + barMainModel.nxCorner - 1 + a)
                {
                    x0 = 11;
                }
            }
            if (i > barMainModel.nxCorner + barMainModel.nyCorner - 2 + barMainModel.nxCorner - 1 + a)
            {
                x0 = 11;
            }
        }
        #endregion
    }
}
