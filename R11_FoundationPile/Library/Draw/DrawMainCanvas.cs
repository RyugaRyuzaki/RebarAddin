using System;
using System.Linq;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace R11_FoundationPile
{
    public class DrawMainCanvas
    {
        #region
        private static void DrawAxisAndSectionColumn(Canvas canvas, DrawModel drawModel, ColumnModel columnModel,bool IsAxis)
        {
            if(IsAxis)DrawImage.DrawAxis(canvas);
            if (columnModel.Style.Equals("RECTANGLE"))
            {
                DrawImage.DrawSection(canvas, drawModel.Scale, drawModel.Left - columnModel.b * 0.5 / drawModel.Scale, drawModel.Top - columnModel.h * 0.5 / drawModel.Scale, columnModel.b, columnModel.h);
            }
            else
            {
                DrawImage.DrawCylindricalSection(canvas, drawModel.Left, drawModel.Top, drawModel.Scale, columnModel.D);
            }
        }
        private static void DrawBounding(Canvas canvas, DrawModel drawModel, FoundationModel foundationModel, bool dim)
        {
            for (int i = 0; i < foundationModel.BoundingLocation.Count; i++)
            {
                if (i == 0)
                {
                    Line l1 = new Line() { X1 = drawModel.Left + (foundationModel.BoundingLocation[foundationModel.BoundingLocation.Count - 1].X) / drawModel.Scale, X2 = drawModel.Left + (foundationModel.BoundingLocation[i].X) / drawModel.Scale, Y1 = drawModel.Top - (foundationModel.BoundingLocation[foundationModel.BoundingLocation.Count - 1].Y) / drawModel.Scale, Y2 = drawModel.Top - (foundationModel.BoundingLocation[i].Y) / drawModel.Scale };
                    l1.Stroke = Brushes.Black;
                    l1.StrokeThickness = 1;
                    canvas.Children.Add(l1);
                }
                else
                {
                    Line l1 = new Line() { X1 = drawModel.Left + (foundationModel.BoundingLocation[i - 1].X) / drawModel.Scale, X2 = drawModel.Left + (foundationModel.BoundingLocation[i].X) / drawModel.Scale, Y1 = drawModel.Top - (foundationModel.BoundingLocation[i - 1].Y) / drawModel.Scale, Y2 = drawModel.Top - (foundationModel.BoundingLocation[i].Y) / drawModel.Scale };
                    l1.Stroke = Brushes.Black;
                    l1.StrokeThickness = 1;
                    canvas.Children.Add(l1);
                }
            }
            if(dim)
            {
                double minX = foundationModel.BoundingLocation.Min(x => x.X);
                double minY = foundationModel.BoundingLocation.Min(x => x.Y);
                double maxX = foundationModel.BoundingLocation.Max(x => x.X);
                double maxY = foundationModel.BoundingLocation.Max(x => x.Y);
                DrawImage.DimHorizontal(canvas, drawModel.Left + minX / drawModel.Scale, drawModel.Top - maxY / drawModel.Scale, drawModel.Scale, Math.Round((maxX - minX), 3), 11, 40, 5);
                DrawImage.DimVertical(canvas, drawModel.Left + minX / drawModel.Scale, drawModel.Top - maxY / drawModel.Scale, drawModel.Scale, Math.Round((maxY - minY), 3), 11, 40, 5);
            }
            
        }
        private static void DrawPiles(Canvas canvas, DrawModel drawModel, FoundationModel foundationModel, SettingModel settingModel, int numberPile,bool text)
        {
            SolidColorBrush solidColorBrush = drawModel.ColorMainBar;
            for (int i = 0; i < foundationModel.PileModels.Count; i++)
            {
                if (i == numberPile)
                {
                    solidColorBrush = drawModel.ColorMainBarChoose;
                }
                else
                {
                    solidColorBrush = drawModel.ColorMainBar;
                }
                if (settingModel.StyleFamilyType.Equals("RECTANGLE"))
                {
                    DrawImage.DrawSectionDashArraySolidColorBrush(canvas, drawModel.Scale, drawModel.Left - (settingModel.b * 0.5) / drawModel.Scale + foundationModel.PileModels[i].Location.X / drawModel.Scale, drawModel.Top - (settingModel.h * 0.5) / drawModel.Scale - foundationModel.PileModels[i].Location.Y / drawModel.Scale, settingModel.b, settingModel.h, solidColorBrush);
                }
                else
                {
                    DrawImage.DrawCylindricalSectionDashArraySolidColorBrush(canvas, drawModel.Left + foundationModel.PileModels[i].Location.X / drawModel.Scale, drawModel.Top - foundationModel.PileModels[i].Location.Y / drawModel.Scale, drawModel.Scale, settingModel.DiameterPile, solidColorBrush);
                }
               if(text) DrawImage.DrawTextOnePileSection(canvas, drawModel.Left + foundationModel.PileModels[i].Location.X / drawModel.Scale, drawModel.Top - foundationModel.PileModels[i].Location.Y / drawModel.Scale, foundationModel.PileModels[i].PileNumber, solidColorBrush);
            }

        }
        public static void DrawMainFoundation(Canvas canvas, DrawModel drawModel, FoundationModel foundationModel, SettingModel settingModel, int numberPile)
        {
            DrawAxisAndSectionColumn(canvas, drawModel, foundationModel.ColumnModel,true);
            DrawBounding(canvas, drawModel, foundationModel,true);
            DrawPiles(canvas, drawModel, foundationModel, settingModel, numberPile,true);
        }
        private static void DrawSpanOrientation(Canvas canvas, DrawModel drawModel, FoundationModel foundationModel,int Image,string orientation)
        {
            SolidColorBrush solidColorBrushChosse = drawModel.ColorMainBarChoose;
            SolidColorBrush solidColorBrush = drawModel.ColorMainBar;
            //draw Vertical
           
            double x = 0;
            double y = 0;
            switch (Image)
            {
                case 0:
                    if (foundationModel.ColumnModel.Style.Equals("RECTANGLE"))
                    {
                        if (foundationModel.ColumnModel.b<= foundationModel.ColumnModel.h)
                        {
                            x = drawModel.Left;
                            y = drawModel.Top - (foundationModel.BoundingLocation[0].Y * 0.5 + foundationModel.BoundingLocation[1].Y * 0.5) / drawModel.Scale;
                        }
                        else
                        {
                            x= drawModel.Left + (foundationModel.BoundingLocation[0].X*0.5+ foundationModel.BoundingLocation[1].X*0.5) / drawModel.Scale;
                            y = drawModel.Top;
                        }
                    }
                    else
                    {
                        x = drawModel.Left;
                        y = drawModel.Top - (foundationModel.BoundingLocation[0].Y * 0.5 + foundationModel.BoundingLocation[1].Y * 0.5) / drawModel.Scale;
                    }
                    break;
                case 1:x = drawModel.Left; y= drawModel.Top; break;
                case 2:
                    if (foundationModel.ColumnModel.Style.Equals("RECTANGLE"))
                    {
                        if (foundationModel.ColumnModel.b <= foundationModel.ColumnModel.h)
                        {
                            x = drawModel.Left;
                            y = drawModel.Top - (foundationModel.BoundingLocation[1].Y) / drawModel.Scale;
                        }
                        else
                        {
                            x = drawModel.Left + ( foundationModel.BoundingLocation[1].X ) / drawModel.Scale;
                            y = drawModel.Top;
                        }
                    }
                    else
                    {
                        x = drawModel.Left;
                        y = drawModel.Top - (foundationModel.BoundingLocation[1].Y) / drawModel.Scale;
                    }
                    break;
                case 3:x = drawModel.Left; y = drawModel.Top; break;
                default:
                    if (foundationModel.ColumnModel.Style.Equals("RECTANGLE"))
                    {
                        if (foundationModel.ColumnModel.b <= foundationModel.ColumnModel.h)
                        {
                            x = drawModel.Left;
                            y = drawModel.Top - (foundationModel.BoundingLocation[0].Y * 0.5 + foundationModel.BoundingLocation[1].Y * 0.5) / drawModel.Scale;
                        }
                        else
                        {
                            x = drawModel.Left + (foundationModel.BoundingLocation[0].X * 0.5 + foundationModel.BoundingLocation[1].X * 0.5) / drawModel.Scale;
                            y = drawModel.Top;
                        }
                    }
                    else
                    {
                        x = drawModel.Left;
                        y = drawModel.Top - (foundationModel.BoundingLocation[0].Y * 0.5 + foundationModel.BoundingLocation[1].Y * 0.5) / drawModel.Scale;
                    }
                    break;
            }
            DrawImage.DrawHorizontalOrientation(canvas, y, (orientation.Equals("Horizontal")) ? solidColorBrushChosse : solidColorBrush);
            DrawImage.DrawVerticalOrientation(canvas,x, (orientation.Equals("Horizontal")) ? solidColorBrush : solidColorBrushChosse);
           
        }
        public static void DrawBarFoundation(Canvas canvas, DrawModel drawModel, FoundationModel foundationModel, SettingModel settingModel, int numberPile,int Image,string orientation)
        {
            DrawAxisAndSectionColumn(canvas, drawModel, foundationModel.ColumnModel, false);
            DrawBounding(canvas, drawModel, foundationModel,false);
            DrawPiles(canvas, drawModel, foundationModel, settingModel, numberPile,false);
            DrawSpanOrientation(canvas, drawModel, foundationModel, Image, orientation);
           
        }
        #endregion
        #region   Section
        private static void DrawAxisAndSectionColumnSection(Canvas canvas, DrawModel drawModel, ColumnModel columnModel)
        {
            DrawImage.DrawAxisSection(canvas);
            if (columnModel.Style.Equals("RECTANGLE"))
            {
                DrawImage.DrawSection(canvas, drawModel.Scale, drawModel.Left - columnModel.b * 0.5 / drawModel.Scale, drawModel.Top - columnModel.h * 0.5 / drawModel.Scale, columnModel.b, columnModel.h);
            }
            else
            {
                DrawImage.DrawCylindricalSection(canvas, drawModel.Left, drawModel.Top, drawModel.Scale, columnModel.D);
            }
        }
        private static void DrawBoundingSection(Canvas canvas, DrawModel drawModel, FoundationModel foundationModel)
        {
            for (int i = 0; i < foundationModel.BoundingLocation.Count; i++)
            {
                if (i == 0)
                {
                    Line l1 = new Line() { X1 = drawModel.Left + (foundationModel.BoundingLocation[foundationModel.BoundingLocation.Count - 1].X) / drawModel.Scale, X2 = drawModel.Left + (foundationModel.BoundingLocation[i].X) / drawModel.Scale, Y1 = drawModel.Top - (foundationModel.BoundingLocation[foundationModel.BoundingLocation.Count - 1].Y) / drawModel.Scale, Y2 = drawModel.Top - (foundationModel.BoundingLocation[i].Y) / drawModel.Scale };
                    l1.Stroke = Brushes.Black;
                    l1.StrokeThickness = 1;
                    canvas.Children.Add(l1);
                }
                else
                {
                    Line l1 = new Line() { X1 = drawModel.Left + (foundationModel.BoundingLocation[i - 1].X) / drawModel.Scale, X2 = drawModel.Left + (foundationModel.BoundingLocation[i].X) / drawModel.Scale, Y1 = drawModel.Top - (foundationModel.BoundingLocation[i - 1].Y) / drawModel.Scale, Y2 = drawModel.Top - (foundationModel.BoundingLocation[i].Y) / drawModel.Scale };
                    l1.Stroke = Brushes.Black;
                    l1.StrokeThickness = 1;
                    canvas.Children.Add(l1);
                }
            }
            double minX = foundationModel.BoundingLocation.Min(x => x.X);
            double minY = foundationModel.BoundingLocation.Min(x => x.Y);
            double maxX = foundationModel.BoundingLocation.Max(x => x.X);
            double maxY = foundationModel.BoundingLocation.Max(x => x.Y);
            DrawImage.DimHorizontal(canvas, drawModel.Left + minX / drawModel.Scale, drawModel.Top - maxY / drawModel.Scale, drawModel.Scale, Math.Round((maxX - minX), 3), 11, 15, 5);
            DrawImage.DimVertical(canvas, drawModel.Left + minX / drawModel.Scale, drawModel.Top - maxY / drawModel.Scale, drawModel.Scale, Math.Round((maxY - minY), 3), 11, 15, 5);
        }
        private static void DrawPilesSection(Canvas canvas, DrawModel drawModel, FoundationModel foundationModel, SettingModel settingModel, int numberPile)
        {
            SolidColorBrush solidColorBrush = drawModel.ColorMainBar;
            for (int i = 0; i < foundationModel.PileModels.Count; i++)
            {
                if (i == numberPile)
                {
                    solidColorBrush = drawModel.ColorMainBarChoose;
                }
                else
                {
                    solidColorBrush = drawModel.ColorMainBar;
                }
                if (settingModel.StyleFamilyType.Equals("RECTANGLE"))
                {
                    DrawImage.DrawSectionDashArraySolidColorBrush(canvas, drawModel.Scale, drawModel.Left - (settingModel.b * 0.5) / drawModel.Scale + foundationModel.PileModels[i].Location.X / drawModel.Scale, drawModel.Top - (settingModel.h * 0.5) / drawModel.Scale - foundationModel.PileModels[i].Location.Y / drawModel.Scale, settingModel.b, settingModel.h, solidColorBrush);
                }
                else
                {
                    DrawImage.DrawCylindricalSectionDashArraySolidColorBrush(canvas, drawModel.Left + foundationModel.PileModels[i].Location.X / drawModel.Scale, drawModel.Top - foundationModel.PileModels[i].Location.Y / drawModel.Scale, drawModel.Scale, settingModel.DiameterPile, solidColorBrush);
                }
                DrawImage.DrawTextOnePileSection(canvas, drawModel.Left + foundationModel.PileModels[i].Location.X / drawModel.Scale, drawModel.Top - foundationModel.PileModels[i].Location.Y / drawModel.Scale, foundationModel.PileModels[i].PileNumber, solidColorBrush);
            }

        }
        public static void DrawMainFoundationSection(Canvas canvas, DrawModel drawModel, FoundationModel foundationModel, SettingModel settingModel, int numberPile)
        {
            DrawAxisAndSectionColumnSection(canvas, drawModel, foundationModel.ColumnModel);
            DrawBoundingSection(canvas, drawModel, foundationModel);
            DrawPilesSection(canvas, drawModel, foundationModel, settingModel, numberPile);
        }
        #endregion
        #region PileDetail
        private static void DrawBoundingPileDetail(Canvas canvas, DrawModel drawModel, FoundationModel foundationModel, SolidColorBrush solidColorBrush, double minX, double minY)
        {
            
            double left = (foundationModel.ColumnModel.PointXPosition- minX)/drawModel.Scale;
            double top = drawModel.Top-(foundationModel.ColumnModel.PointYPosition - minY) / drawModel.Scale;
            for (int i = 0; i < foundationModel.BoundingLocation.Count; i++)
            {
               
                if (i == 0)
                {
                    Line l1 = new Line() {
                        X1 = left + (foundationModel.BoundingLocation[foundationModel.BoundingLocation.Count - 1].X) / drawModel.Scale,
                        X2 = left + (foundationModel.BoundingLocation[i].X) / drawModel.Scale,
                        Y1 = top - (foundationModel.BoundingLocation[foundationModel.BoundingLocation.Count - 1].Y) / drawModel.Scale,
                        Y2 = top - (foundationModel.BoundingLocation[i].Y) / drawModel.Scale };
                    l1.Stroke = solidColorBrush;
                    l1.StrokeThickness = 1;
                    canvas.Children.Add(l1);
                }
                else
                {
                    Line l1 = new Line() { X1 = left + (foundationModel.BoundingLocation[i - 1].X) / drawModel.Scale, X2 = left + (foundationModel.BoundingLocation[i].X) / drawModel.Scale, Y1 = top - (foundationModel.BoundingLocation[i - 1].Y) / drawModel.Scale, Y2 = top - (foundationModel.BoundingLocation[i].Y) / drawModel.Scale };
                    l1.Stroke = solidColorBrush;
                    l1.StrokeThickness = 1;
                    canvas.Children.Add(l1);
                }
            }
        }
        private static void DrawPilesDetail(Canvas canvas, DrawModel drawModel, FoundationModel foundationModel, SettingModel settingModel, double minX, double minY)
        {
            SolidColorBrush solidColorBrush = drawModel.ColorMainBar;
            double left = (foundationModel.ColumnModel.PointXPosition - minX) / drawModel.Scale;
            double top = drawModel.Top - (foundationModel.ColumnModel.PointYPosition - minY) / drawModel.Scale;
            for (int i = 0; i < foundationModel.PileModels.Count; i++)
            {

                if (settingModel.StyleFamilyType.Equals("RECTANGLE"))
                {
                    DrawImage.DrawSectionDashArraySolidColorBrush(canvas, drawModel.Scale, left - (settingModel.b * 0.5) / drawModel.Scale + foundationModel.PileModels[i].Location.X / drawModel.Scale, top - (settingModel.h * 0.5) / drawModel.Scale - foundationModel.PileModels[i].Location.Y / drawModel.Scale, settingModel.b, settingModel.h, solidColorBrush);
                }
                else
                {
                    DrawImage.DrawCylindricalSectionDashArraySolidColorBrush(canvas, left + foundationModel.PileModels[i].Location.X / drawModel.Scale, top - foundationModel.PileModels[i].Location.Y / drawModel.Scale, drawModel.Scale, settingModel.DiameterPile, solidColorBrush);
                }
                DrawImage.DrawTextOnePileSection(canvas, left+11 + foundationModel.PileModels[i].Location.X / drawModel.Scale, top-11 - foundationModel.PileModels[i].Location.Y / drawModel.Scale, foundationModel.PileModels[i].PileNumber, solidColorBrush);
            }

        }
        public static void DrawMainPileDetail(Canvas canvas, DrawModel drawModel, FoundationModel foundationModel, SettingModel settingModel, SolidColorBrush solidColorBrush, double minX, double minY)
        {
            DrawBoundingPileDetail(canvas, drawModel, foundationModel, solidColorBrush, minX, minY);
            DrawPilesDetail(canvas, drawModel, foundationModel, settingModel, minX, minY);
        }
        #endregion
        #region Bar Section
        private void DrawMainSpanBounding(Canvas canvas , DrawModel drawModel,FoundationModel foundationModel,SettingModel settingModel,int Image, string orientation)
        {
            double topMain = 360;
            switch (Image)
            {
                case 0:
                    if (orientation.Equals("Horizontal"))
                    {
                        double b = Math.Abs(foundationModel.BoundingLocation[0].X - foundationModel.BoundingLocation[foundationModel.BoundingLocation.Count - 1].X);
                        DrawImage.DrawSection(canvas, drawModel.Scale, drawModel.Left - b / (2 * drawModel.Scale), topMain - settingModel.HeightFoundation / drawModel.Scale, b, settingModel.HeightFoundation);
                    }
                    else
                    {
                        double b = Math.Abs(foundationModel.BoundingLocation[0].Y - foundationModel.BoundingLocation[foundationModel.BoundingLocation.Count - 1].Y);
                    }
                    break;
                case 1:break;
                case 2:
                   
                    break;
                case 3: break;
                default:
                   
                    break;
            }
        }
        #endregion
    }
}
