
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace R02_BeamsRebar
{
    public class DrawMainCanvas
    {
        #region Part
        public static void SpanBeams(Canvas canvas, List<InfoModel> infoModels, DrawModel drawModel, int selectedSpan)
        {

            SolidColorBrush colorSelected = drawModel.ColorWhite;
            double hmax = GetHmax(infoModels);
            if (infoModels.Count==1)
            {
                colorSelected = drawModel.ColorFill;
                DimHorizontalBottom(canvas, drawModel.Left + infoModels[0].startPosition / drawModel.Scale, drawModel.Top + hmax / drawModel.Scale + 5 * drawModel.Offset, drawModel.Scale, infoModels[0].Length, 11, drawModel.Offset, drawModel.Extend, drawModel.StrokeDim);
                if (infoModels[0].ConsolLeft)
                {
                    DrawSpanBeam(canvas, drawModel.Left + infoModels[0].startPosition / drawModel.Scale, drawModel.Top - infoModels[0].zOffset / drawModel.Scale, drawModel.Scale, infoModels[0].Length, infoModels[0].h, 0, colorSelected, drawModel.StrokeBound);
                }
                else
                {
                    DrawSpanBeam(canvas, drawModel.Left + infoModels[0].startPosition / drawModel.Scale, drawModel.Top - infoModels[0].zOffset / drawModel.Scale, drawModel.Scale, infoModels[0].Length, infoModels[0].h, 1, colorSelected, drawModel.StrokeBound);
                }
            }
            else
            {
                for (int i = 0; i < infoModels.Count; i++)
                {
                    // draw Dimention
                    DimHorizontalBottom(canvas, drawModel.Left + infoModels[i].startPosition / drawModel.Scale, drawModel.Top + hmax / drawModel.Scale + 5 * drawModel.Offset, drawModel.Scale, infoModels[i].Length, 11, drawModel.Offset, drawModel.Extend, drawModel.StrokeDim);
                    if (i == selectedSpan)
                    {
                        colorSelected = drawModel.ColorFill;
                    }
                    else
                    {
                        colorSelected = drawModel.ColorWhite;
                    }
                    if (i == 0)
                    {
                        DrawSpanBeam(canvas, drawModel.Left + infoModels[i].startPosition / drawModel.Scale, drawModel.Top - infoModels[i].zOffset / drawModel.Scale, drawModel.Scale, infoModels[i].Length, infoModels[i].h, (infoModels[i].ConsolLeft) ? 0 : 2, colorSelected, drawModel.StrokeBound);
                    }
                    else
                    {
                        if (i == infoModels.Count - 1)
                        {
                            DrawSpanBeam(canvas, drawModel.Left + infoModels[i].startPosition / drawModel.Scale, drawModel.Top - infoModels[i].zOffset / drawModel.Scale, drawModel.Scale, infoModels[i].Length, infoModels[i].h, (infoModels[i].ConsolRight) ? 1 : 2, colorSelected, drawModel.StrokeBound);
                        }
                        else
                        {
                            DrawSpanBeam(canvas, drawModel.Left + infoModels[i].startPosition / drawModel.Scale, drawModel.Top - infoModels[i].zOffset / drawModel.Scale, drawModel.Scale, infoModels[i].Length, infoModels[i].h, 2, colorSelected, drawModel.StrokeBound);
                        }
                    }

                }
            }
            
        }
        public static void NodeBeams(Canvas canvas, List<InfoModel> infoModels, List<NodeModel> nodeModels, DrawModel drawModel, int selectedNode)
        {
            SolidColorBrush colorSelected = drawModel.ColorNode;
            double hmax = GetHmax(infoModels);
            if (nodeModels.Count==1)
            {
                colorSelected = drawModel.ColorNodeChoose;
                DimHorizontalBottom(canvas, drawModel.Left + nodeModels[0].Start / drawModel.Scale, drawModel.Top + hmax / drawModel.Scale + 5 * drawModel.Offset, drawModel.Scale, nodeModels[0].Width, 11, drawModel.Offset, drawModel.Extend, drawModel.StrokeDim);
                if ((infoModels[0].ConsolLeft))
                {
                    DrawNodeBeam(canvas, drawModel.Left + nodeModels[0].Start / drawModel.Scale, drawModel.Top, drawModel.Scale, nodeModels[0].NumberNode, hmax, nodeModels[0].Width, nodeModels[0].HLeft, nodeModels[0].HRight, nodeModels[0].ZLeft, nodeModels[0].ZRight, drawModel.Offset, drawModel.Extend, 1, colorSelected, drawModel.StrokeBound);
                }
                else
                {
                    DrawNodeBeam(canvas, drawModel.Left + nodeModels[0].Start / drawModel.Scale, drawModel.Top, drawModel.Scale, nodeModels[0].NumberNode, hmax, nodeModels[0].Width, nodeModels[0].HLeft, nodeModels[0].HRight, nodeModels[0].ZLeft, nodeModels[0].ZRight, drawModel.Offset, drawModel.Extend, 0, colorSelected, drawModel.StrokeBound);
                }
            }
            else
            {
                for (int i = 0; i < nodeModels.Count; i++)
                {
                    if (i == selectedNode)
                    {
                        colorSelected = drawModel.ColorNodeChoose;
                    }
                    else
                    {
                        colorSelected = drawModel.ColorNode;
                    }
                    DimHorizontalBottom(canvas, drawModel.Left + nodeModels[i].Start / drawModel.Scale, drawModel.Top + hmax / drawModel.Scale + 5 * drawModel.Offset, drawModel.Scale, nodeModels[i].Width, 11, drawModel.Offset, drawModel.Extend, drawModel.StrokeDim);
                    if (i == 0)
                    {
                        DrawNodeBeam(canvas, drawModel.Left + nodeModels[i].Start / drawModel.Scale, drawModel.Top, drawModel.Scale, nodeModels[i].NumberNode, hmax, nodeModels[i].Width, nodeModels[i].HLeft, nodeModels[i].HRight, nodeModels[i].ZLeft, nodeModels[i].ZRight, drawModel.Offset, drawModel.Extend, (infoModels[0].ConsolLeft) ? 2 : 0, colorSelected, drawModel.StrokeBound);
                    }
                    else
                    {
                        if (i == nodeModels.Count - 1)
                        {
                            DrawNodeBeam(canvas, drawModel.Left + nodeModels[i].Start / drawModel.Scale, drawModel.Top, drawModel.Scale, nodeModels[i].NumberNode, hmax, nodeModels[i].Width, nodeModels[i].HLeft, nodeModels[i].HRight, nodeModels[i].ZLeft, nodeModels[i].ZRight, drawModel.Offset, drawModel.Extend, (infoModels[infoModels.Count - 1].ConsolRight) ? 2 : 1, colorSelected, drawModel.StrokeBound);
                        }
                        else
                        {
                            DrawNodeBeam(canvas, drawModel.Left + nodeModels[i].Start / drawModel.Scale, drawModel.Top, drawModel.Scale, nodeModels[i].NumberNode, hmax, nodeModels[i].Width, nodeModels[i].HLeft, nodeModels[i].HRight, nodeModels[i].ZLeft, nodeModels[i].ZRight, drawModel.Offset, drawModel.Extend, 2, colorSelected, drawModel.StrokeBound);
                        }
                    }
                }
            }
            
        }
        public static void SpecialNode(Canvas canvas, List<InfoModel> infoModels, List<SpecialNodeModel> SpecialNodeModel, DrawModel drawModel, int selectedSpecialNode)
        {
            for (int i = 0; i < SpecialNodeModel.Count; i++)
            {

                SolidColorBrush colorSelected = drawModel.ColorMainBar;
                if (i == selectedSpecialNode)
                {
                    colorSelected = drawModel.ColorMainBarChoose;
                }
                else
                {
                    colorSelected = drawModel.ColorMainBar;
                }
                if (SpecialNodeModel[i].IsBeamColumn)
                {
                    DrawSpecialColumn(canvas, infoModels, SpecialNodeModel[i], drawModel, colorSelected);
                }
                else
                {
                    DrawSpecialBeam(canvas, infoModels, SpecialNodeModel[i], drawModel, colorSelected);
                }
            }
        }
        public static void DistributeStirrupBeams(Canvas canvas, List<InfoModel> infoModels, List<StirrupModel> stirrupModels, List<DistributeStirrup> distributeStirrups, DrawModel drawModel, int selectedSpan)
        {
            SolidColorBrush colorSelected = drawModel.ColorStirrup;

            for (int i = 0; i < infoModels.Count; i++)
            {
                if (i == selectedSpan)
                {
                    colorSelected = drawModel.ColorStirrupChoose;
                }
                else
                {
                    colorSelected = drawModel.ColorStirrup;
                }
                DrawStirrupDistribute(canvas,
                    drawModel.Left + infoModels[i].startPosition / drawModel.Scale,
                    drawModel.Top - infoModels[i].zOffset / drawModel.Scale,
                    drawModel.Scale,
                    infoModels[i],
                     stirrupModels[i],distributeStirrups[i],colorSelected,drawModel.StrokeStirrup
                    );
            }
        }
        public static void MainTopBarBeams(Canvas canvas, List<MainTopBarModel> mainTopBarModels, SingleMainTopBarModel single, DrawModel drawModel, int selectedStyleMaintop, int selectedMaintop)
        {
            if (selectedStyleMaintop == 0)
            {
                SolidColorBrush colorSelected = drawModel.ColorMainBar;
                if (0 == selectedMaintop)
                {
                    colorSelected = drawModel.ColorMainBarChoose;
                }
                else
                {
                    colorSelected = drawModel.ColorMainBar;
                }
                DrawSingleMainTopBar(canvas, single, colorSelected, drawModel.Left, drawModel.Top, drawModel.Scale, drawModel.StrokeMain);
            }
            else
            {
                DrawMainTopBar(canvas, mainTopBarModels, drawModel, drawModel.Left, drawModel.Top, drawModel.Scale, selectedMaintop, drawModel.StrokeMain);
            }
        }
        public static void MainBottomBarBeams(Canvas canvas,  List<MainBottomBarModel> mainBottomBarModels, DrawModel drawModel, int selectedMainBottom)
        {
            DrawMainBottomBar(canvas, mainBottomBarModels, drawModel, drawModel.Left, drawModel.Top, drawModel.Scale, selectedMainBottom, drawModel.StrokeMain);
        }
        public static void AddTopBarStartBeams(Canvas canvas, AddTopBarModel AddTopBarModel, DrawModel drawModel, int selectedAddTopStart, bool IsStartAddTopChecked)
        {
            DrawAddTopStartBar(canvas, AddTopBarModel, drawModel, drawModel.Left, drawModel.Top, drawModel.Scale, selectedAddTopStart, drawModel.StrokeMain, IsStartAddTopChecked);

        }
        public static void AddTopBarEndBeams(Canvas canvas, AddTopBarModel AddTopBarModel, DrawModel drawModel, int selectedAddTopEnd, bool IsEndAddTopChecked)
        {
            DrawAddTopEndBar(canvas, AddTopBarModel, drawModel, drawModel.Left, drawModel.Top, drawModel.Scale, selectedAddTopEnd, drawModel.StrokeMain, IsEndAddTopChecked);

        }
        public static void AddTopBarMidBeams(Canvas canvas, AddTopBarModel AddTopBarModel, DrawModel drawModel, int selectedAddTopMid)
        {
            for (int i = 0; i < AddTopBarModel.Mid.Count; i++)
            {
                DrawAddTopMidBar(canvas, AddTopBarModel, drawModel, drawModel.Left, drawModel.Top, drawModel.Scale, i, selectedAddTopMid, drawModel.StrokeMain);
            }
        }
        public static void AddTopBar(Canvas canvas, AddTopBarModel AddTopBarModel, DrawModel drawModel, int selectedAddTopStart, int selectedAddTopEnd, int selectedNode, bool IsStartAddTopChecked, bool IsEndAddTopChecked, int selectedAddTopMid)
        {
            DrawAddTopStartBar(canvas, AddTopBarModel, drawModel, drawModel.Left, drawModel.Top, drawModel.Scale, selectedAddTopStart, drawModel.StrokeMain, IsStartAddTopChecked);
            DrawAddTopEndBar(canvas, AddTopBarModel, drawModel, drawModel.Left, drawModel.Top, drawModel.Scale, selectedAddTopEnd, drawModel.StrokeMain, IsEndAddTopChecked);
            for (int i = 0; i < AddTopBarModel.Mid.Count; i++)
            {
                DrawAddTopMidBar(canvas, AddTopBarModel, drawModel, drawModel.Left, drawModel.Top, drawModel.Scale, i, selectedAddTopMid, drawModel.StrokeMain);
            }
        }
        public static void AddBottomBar(Canvas canvas, ObservableCollection<AddBottomBarModel> AddBottomBarModels, DrawModel drawModel, int selectedSpan, List<SelectedBottomModel> SelectedBottomModels)
        {
            for (int i = 0; i < AddBottomBarModels.Count; i++)
            {

                SolidColorBrush colorSelected = drawModel.ColorMainBar;
                if (i == selectedSpan)
                {
                    colorSelected = drawModel.ColorMainBarChoose;
                }
                else
                {
                    colorSelected = drawModel.ColorMainBar;
                }
                DrawAddBottomStartBar(canvas, AddBottomBarModels[i], drawModel, drawModel.Left, drawModel.Top, drawModel.Scale, drawModel.StrokeMain, SelectedBottomModels[i].StartBottomChecked, colorSelected);
                DrawAddBottomEndBar(canvas, AddBottomBarModels[i], drawModel, drawModel.Left, drawModel.Top, drawModel.Scale, drawModel.StrokeMain, SelectedBottomModels[i].EndBottomChecked, colorSelected);
                DrawAddBottomMidBar(canvas, AddBottomBarModels[i], drawModel, drawModel.Left, drawModel.Top, drawModel.Scale, drawModel.StrokeMain, colorSelected);
            }
        }
        public static void SideBar(Canvas canvas, List<SideBarModel> SideBarModel, DrawModel drawModel, int selectedSpan)
        {
            for (int i = 0; i < SideBarModel.Count; i++)
            {

                SolidColorBrush colorSelected = drawModel.ColorMainBar;
                if (i == selectedSpan)
                {
                    colorSelected = drawModel.ColorMainBarChoose;
                }
                else
                {
                    colorSelected = drawModel.ColorMainBar;
                }

                DrawSideBar(canvas, SideBarModel[i], drawModel, drawModel.Left, drawModel.Top, drawModel.Scale, drawModel.StrokeMain, colorSelected);
            }

        }
        public static void SpecialBar(Canvas canvas, List<SpecialBarModel> specialBarModels, DrawModel drawModel, int selectedSpecialNode)
        {
            for (int i = 0; i < specialBarModels.Count; i++)
            {
                SolidColorBrush colorSelected = drawModel.ColorMainBar;
                if (i == selectedSpecialNode)
                {
                    colorSelected = drawModel.ColorMainBarChoose;
                }
                else
                {
                    colorSelected = drawModel.ColorMainBar;
                }
                DrawSpecialBar(canvas, specialBarModels[i], drawModel.StrokeMain, drawModel, colorSelected, drawModel.ColorStirrupChoose);
            }
        }
        #endregion
        #region Property
        public static void DrawSpanBeam(Canvas canvas, double left, double top, double scale, double length, double h, int i, SolidColorBrush colorSelected, double strokeBound)
        {
            Rectangle rec = new Rectangle() { Width = length / scale, Height = h / scale };
            rec.Fill = colorSelected;
            Canvas.SetLeft(rec, left);
            Canvas.SetTop(rec, top);
            Line l1 = new Line() { X1 = left, X2 = left + length / scale, Y1 = top, Y2 = top };
            l1.Stroke = Brushes.Black;
            l1.StrokeThickness = strokeBound;
            Line l2 = new Line() { X1 = left, X2 = left + length / scale, Y1 = top + h / scale, Y2 = top + h / scale };
            l2.Stroke = Brushes.Black;
            l2.StrokeThickness = strokeBound;
            Line l3 = null;
            if (i == 0)
            {
                l3 = new Line() { X1 = left, X2 = left, Y1 = top, Y2 = top + h / scale };
                l3.Stroke = Brushes.Black;
                l3.StrokeThickness = strokeBound;
                canvas.Children.Add(l3);
            }
            if (i == 1)
            {
                l3 = new Line() { X1 = left + length / scale, X2 = left + length / scale, Y1 = top, Y2 = top + h / scale };
                l3.Stroke = Brushes.Black;
                l3.StrokeThickness = strokeBound;
                canvas.Children.Add(l3);
            }
            canvas.Children.Add(l1); canvas.Children.Add(l2); canvas.Children.Add(rec);
        }
        public static void DrawNodeBeam(Canvas canvas, double left, double top, double scale, int node, double hmax, double width, double hl, double hr, double zl, double zr, double offset, double extend, int i, SolidColorBrush colorSelected, double strokeBound)
        {
            Line l1 = new Line()
            {
                X1 = left,
                X2 = left,
                Y1 = top + zl / scale,
                Y2 = top - 3 * offset
            };
            l1.Stroke = colorSelected;
            l1.StrokeThickness = strokeBound;
            Line l2 = new Line()
            {
                X1 = left + width / scale,
                X2 = left + width / scale,
                Y1 = top + zr / scale,
                Y2 = top - 3 * offset
            };
            l2.Stroke = colorSelected;
            l2.StrokeThickness = strokeBound;
            Line l3 = new Line()
            {
                X1 = left,
                X2 = left,
                Y1 = top + zl / scale + hl / scale,
                Y2 = top + Math.Max(hl + zl, hr + zr) / scale + 3 * offset
            };
            l3.Stroke = colorSelected;
            l3.StrokeThickness = strokeBound;
            Line l4 = new Line()
            {
                X1 = left + width / scale,
                X2 = left + width / scale,
                Y1 = top + zr / scale + hr / scale,
                Y2 = top + Math.Max(hl + zl, hr + zr) / scale + 3 * offset
            };
            l4.Stroke = colorSelected;
            l4.StrokeThickness = strokeBound;
            DrawImage.DrawCutHorizontal(canvas, left, top + Math.Max(hl + zl, hr + zr) / scale + 3 * offset, scale, width, extend, offset, colorSelected);
            DrawImage.DrawCutHorizontal(canvas, left, top - 3 * offset, scale, width, extend, offset, colorSelected);
            Line l0 = null;
            if (i == 0)
            {
                l0 = new Line() { X1 = left, X2 = left, Y1 = top + zl / scale, Y2 = top + (hl + zl) / scale };
                l0.Stroke = colorSelected;
                l0.StrokeThickness = strokeBound;
                canvas.Children.Add(l0);
            }
            if (i == 1)
            {
                l0 = new Line() { X1 = left + width / scale, X2 = left + width / scale, Y1 = top + zr / scale, Y2 = top + (hr + zr) / scale };
                l0.Stroke = colorSelected;
                l0.StrokeThickness = strokeBound;
                canvas.Children.Add(l0);
            }
            
            canvas.Children.Add(l1); canvas.Children.Add(l2); canvas.Children.Add(l3); canvas.Children.Add(l4);
            DrawImage.DrawBarNumber(canvas, left + (width / 2) / scale, top - 6 * offset, scale, 15, node.ToString(), colorSelected);
            Line l5 = new Line()
            {
                X1 = left + (width / 2) / scale,
                X2 = left + (width / 2) / scale,
                Y1 = top - 6 * offset + 15,
                Y2 = top + hmax / scale + 4 * offset
            };
            l5.Stroke = colorSelected;
            l5.StrokeThickness = strokeBound;
            l5.StrokeDashArray = new DoubleCollection() { 10, 4, 2 };
            canvas.Children.Add(l5);
        }
        public static void DrawStirrupDistribute(Canvas canvas, double left, double top, double scale, InfoModel infoModel,StirrupModel stirrupModel, DistributeStirrup distributeStirrup, SolidColorBrush colorBrush, double strokeStirrup)
        {
            if (distributeStirrup.Type == 0)
            {
                if (distributeStirrup.S == 0)
                {
                    return;
                }
                if (distributeStirrup.S >= infoModel.Length)
                {
                    return;
                }
                double a = (infoModel.Length - distributeStirrup.S * (int)(infoModel.Length / distributeStirrup.S)) / 2;
                for (int i = 0; i < infoModel.Length / distributeStirrup.S; i++)
                {
                    Line l1 = new Line()
                    {
                        X1 = left + a / scale + i * distributeStirrup.S / scale,
                        X2 = left + a / scale + i * distributeStirrup.S / scale,
                        Y1 = top + (stirrupModel.c / scale - 1),
                        Y2 = top + infoModel.h / scale - (stirrupModel.c / scale - 1)
                    };
                    l1.Stroke = colorBrush;
                    l1.StrokeThickness = strokeStirrup;
                    canvas.Children.Add(l1);
                }

            }
            else
            {
                if ((distributeStirrup.S1 == 0) ||
                    (distributeStirrup.S2 == 0) ||
                    (distributeStirrup.L1 == 0) ||
                    (distributeStirrup.L2 == 0))
                {
                    return;
                }
                if ((distributeStirrup.S1 > infoModel.Length) || (distributeStirrup.S2 > infoModel.Length))
                {
                    return;
                }
                if ((distributeStirrup.L1 > (infoModel.Length - distributeStirrup.L2) / 2) || (distributeStirrup.L2 > infoModel.Length - 2 * distributeStirrup.L1))
                {
                    return;
                }
                double s1 = distributeStirrup.S1 * (int)(distributeStirrup.L1 / distributeStirrup.S1);
                double s2 = distributeStirrup.S2 * (int)(distributeStirrup.L2 / distributeStirrup.S2);
                double a1 = (infoModel.Length - (s1 * 2 + s2)) / 2;
                for (int i = 0; i < distributeStirrup.L1 / distributeStirrup.S1; i++)
                {
                    Line l1 = new Line()
                    {
                        X1 = left + a1 / scale + i * distributeStirrup.S1 / scale,
                        X2 = left + a1 / scale + i * distributeStirrup.S1 / scale,
                        Y1 = top + (stirrupModel.c / scale - 1),
                        Y2 = top + infoModel.h / scale - (stirrupModel.c / scale - 1)
                    };
                    l1.Stroke = colorBrush;
                    l1.StrokeThickness = strokeStirrup;
                    canvas.Children.Add(l1);
                    Line l2 = new Line()
                    {
                        X1 = left + (a1 + s1 + s2) / scale + i * distributeStirrup.S1 / scale,
                        X2 = left + (a1 + s1 + s2) / scale + i * distributeStirrup.S1 / scale,
                        Y1 = top + (stirrupModel.c / scale - 1),
                        Y2 = top + infoModel.h / scale - (stirrupModel.c / scale - 1)
                    };
                    l2.Stroke = colorBrush;
                    l2.StrokeThickness = strokeStirrup;
                    canvas.Children.Add(l2);
                }
                for (int i = 0; i < distributeStirrup.L2 / distributeStirrup.S2; i++)
                {
                    Line l1 = new Line()
                    {
                        X1 = left + (a1 + s1) / scale + i * distributeStirrup.S2 / scale,
                        X2 = left + (a1 + s1) / scale + i * distributeStirrup.S2 / scale,
                        Y1 = top + (stirrupModel.c / scale - 1),
                        Y2 = top + infoModel.h / scale - (stirrupModel.c / scale - 1)
                    };
                    l1.Stroke = colorBrush;
                    l1.StrokeThickness = strokeStirrup;
                    canvas.Children.Add(l1);
                }
            }
        }

        public static void DrawMainTopBar(Canvas canvas, List<MainTopBarModel> mainTopBarModels, DrawModel drawModel, double left, double top, double scale, int selectedMaintop, double strokeMain)
        {
            for (int i = 0; i < mainTopBarModels.Count; i++)
            {
                SolidColorBrush colorSelected = drawModel.ColorMainBar;
                if (i == selectedMaintop)
                {
                    colorSelected = drawModel.ColorMainBarChoose;
                }
                else
                {
                    colorSelected = drawModel.ColorMainBar;
                }
                for (int j = 0; j < mainTopBarModels[i].Location.Count - 1; j++)
                {
                    Line l1 = new Line()
                    {
                        X1 = left + mainTopBarModels[i].Location[j].X / scale,
                        X2 = left + mainTopBarModels[i].Location[j + 1].X / scale,
                        Y1 = top + mainTopBarModels[i].Location[j].Y / scale,
                        Y2 = top + mainTopBarModels[i].Location[j + 1].Y / scale,
                    };
                    l1.Stroke = colorSelected;
                    l1.StrokeThickness = strokeMain;
                    canvas.Children.Add(l1);
                }
            }
        }
        public static void DrawSingleMainTopBar(Canvas canvas, SingleMainTopBarModel single, SolidColorBrush colorMainBar, double left, double top, double scale, double strokeMain)
        {
            
            for (int i = 1; i < single.Location.Count; i++)
            {
                Line l1 = new Line()
                {
                    X1 = left + single.Location[i - 1].X / scale,
                    X2 = left + single.Location[i].X / scale,
                    Y1 = top + single.Location[i - 1].Y / scale,
                    Y2 = top + single.Location[i].Y / scale,
                };
                l1.Stroke = colorMainBar;
                l1.StrokeThickness = strokeMain;
                canvas.Children.Add(l1);

            }
        }
        public static void DrawMainBottomBar(Canvas canvas, List<MainBottomBarModel> mainBottomBarModels, DrawModel drawModel, double left, double top, double scale, int selectedMainBottom, double strokeMain)
        {
            for (int i = 0; i < mainBottomBarModels.Count; i++)
            {
                SolidColorBrush colorSelected = drawModel.ColorMainBar;
                if (i == selectedMainBottom)
                {
                    colorSelected = drawModel.ColorMainBarChoose;
                }
                else
                {
                    colorSelected = drawModel.ColorMainBar;
                }
                for (int j = 0; j < mainBottomBarModels[i].Location.Count - 1; j++)
                {
                    Line l1 = new Line()
                    {
                        X1 = left + mainBottomBarModels[i].Location[j].X / scale,
                        X2 = left + mainBottomBarModels[i].Location[j + 1].X / scale,
                        Y1 = top + mainBottomBarModels[i].Location[j].Y / scale,
                        Y2 = top + mainBottomBarModels[i].Location[j + 1].Y / scale,
                    };
                    l1.Stroke = colorSelected;
                    l1.StrokeThickness = strokeMain;
                    canvas.Children.Add(l1);
                }
            }
        }
        public static void DrawAddTopStartBar(Canvas canvas, AddTopBarModel AddTopBarModel, DrawModel drawModel, double left, double top, double scale, int selectedAddTopStart, double strokeMain, bool IsStartAddTopChecked)
        {
            if (IsStartAddTopChecked)
            {
                if (AddTopBarModel.Start.Model.Count != 0)
                {
                    for (int i = 0; i < AddTopBarModel.Start.Model.Count; i++)
                    {
                        SolidColorBrush colorSelected = drawModel.ColorMainBar;
                        if (i == selectedAddTopStart)
                        {
                            colorSelected = drawModel.ColorMainBarChoose;
                        }
                        else
                        {
                            colorSelected = drawModel.ColorMainBar;
                        }
                        for (int j = 0; j < AddTopBarModel.Start.Model[i].Location.Count - 1; j++)
                        {
                            Line l1 = new Line()
                            {
                                X1 = left + AddTopBarModel.Start.Model[i].Location[j].X / scale,
                                X2 = left + AddTopBarModel.Start.Model[i].Location[j + 1].X / scale,
                                Y1 = top + AddTopBarModel.Start.Model[i].Location[j].Y / scale,
                                Y2 = top + AddTopBarModel.Start.Model[i].Location[j + 1].Y / scale,
                            };
                            l1.Stroke = colorSelected;
                            l1.StrokeThickness = strokeMain;
                            canvas.Children.Add(l1);
                        }
                    }
                }
            }

        }
        public static void DrawAddTopEndBar(Canvas canvas, AddTopBarModel AddTopBarModel, DrawModel drawModel, double left, double top, double scale, int selectedAddTopEnd, double strokeMain, bool IsEndtAddTopChecked)
        {
            if (IsEndtAddTopChecked)
            {
                if (AddTopBarModel.End.Model.Count != 0)
                {
                    for (int i = 0; i < AddTopBarModel.End.Model.Count; i++)
                    {
                        SolidColorBrush colorSelected = drawModel.ColorMainBar;
                        if (i == selectedAddTopEnd)
                        {
                            colorSelected = drawModel.ColorMainBarChoose;
                        }
                        else
                        {
                            colorSelected = drawModel.ColorMainBar;
                        }
                        for (int j = 0; j < AddTopBarModel.End.Model[i].Location.Count - 1; j++)
                        {
                            Line l1 = new Line()
                            {
                                X1 = left + AddTopBarModel.End.Model[i].Location[j].X / scale,
                                X2 = left + AddTopBarModel.End.Model[i].Location[j + 1].X / scale,
                                Y1 = top + AddTopBarModel.End.Model[i].Location[j].Y / scale,
                                Y2 = top + AddTopBarModel.End.Model[i].Location[j + 1].Y / scale,
                            };
                            l1.Stroke = colorSelected;
                            l1.StrokeThickness = strokeMain;
                            canvas.Children.Add(l1);
                        }
                    }
                }
            }
        }
        public static void DrawAddTopMidBar(Canvas canvas, AddTopBarModel AddTopBarModel, DrawModel drawModel, double left, double top, double scale, int selectedNode, int selectedAddTopMid, double strokeMain)
        {
            if (AddTopBarModel.Mid[selectedNode].Model.Count != 0)
            {
                for (int j = 0; j < AddTopBarModel.Mid[selectedNode].Model.Count; j++)
                {
                    SolidColorBrush colorSelected = drawModel.ColorMainBar;
                    if (j == selectedAddTopMid)
                    {
                        colorSelected = drawModel.ColorMainBarChoose;
                    }
                    else
                    {
                        colorSelected = drawModel.ColorMainBar;
                    }
                    for (int k = 0; k < AddTopBarModel.Mid[selectedNode].Model[j].Location.Count - 1; k++)
                    {
                        Line l1 = new Line()
                        {
                            X1 = left + AddTopBarModel.Mid[selectedNode].Model[j].Location[k].X / scale,
                            X2 = left + AddTopBarModel.Mid[selectedNode].Model[j].Location[k + 1].X / scale,
                            Y1 = top + AddTopBarModel.Mid[selectedNode].Model[j].Location[k].Y / scale,
                            Y2 = top + AddTopBarModel.Mid[selectedNode].Model[j].Location[k + 1].Y / scale,
                        };
                        l1.Stroke = colorSelected;
                        l1.StrokeThickness = strokeMain;
                        canvas.Children.Add(l1);
                    }
                }
            }
        }
        public static void DrawAddBottomStartBar(Canvas canvas, AddBottomBarModel AddBottomBarModel, DrawModel drawModel, double left, double top, double scale, double strokeMain, bool IsStartAddBottomChecked, SolidColorBrush colorSelected)
        {
            if (IsStartAddBottomChecked)
            {
                if (AddBottomBarModel.Start.Model.Count != 0)
                {
                    for (int i = 0; i < AddBottomBarModel.Start.Model.Count; i++)
                    {
                        for (int j = 0; j < AddBottomBarModel.Start.Model[i].Location.Count - 1; j++)
                        {
                            Line l1 = new Line()
                            {
                                X1 = left + AddBottomBarModel.Start.Model[i].Location[j].X / scale,
                                X2 = left + AddBottomBarModel.Start.Model[i].Location[j + 1].X / scale,
                                Y1 = top + AddBottomBarModel.Start.Model[i].Location[j].Y / scale,
                                Y2 = top + AddBottomBarModel.Start.Model[i].Location[j + 1].Y / scale,
                            };
                            l1.Stroke = colorSelected;
                            l1.StrokeThickness = strokeMain;
                            canvas.Children.Add(l1);
                        }
                    }
                }
            }
        }
        public static void DrawAddBottomEndBar(Canvas canvas, AddBottomBarModel AddBottomBarModel, DrawModel drawModel, double left, double top, double scale, double strokeMain, bool IsEndAddBottomChecked, SolidColorBrush colorSelected)
        {
            if (IsEndAddBottomChecked)
            {
                if (AddBottomBarModel.End.Model.Count != 0)
                {
                    for (int i = 0; i < AddBottomBarModel.End.Model.Count; i++)
                    {
                        for (int j = 0; j < AddBottomBarModel.End.Model[i].Location.Count - 1; j++)
                        {
                            Line l1 = new Line()
                            {
                                X1 = left + AddBottomBarModel.End.Model[i].Location[j].X / scale,
                                X2 = left + AddBottomBarModel.End.Model[i].Location[j + 1].X / scale,
                                Y1 = top + AddBottomBarModel.End.Model[i].Location[j].Y / scale,
                                Y2 = top + AddBottomBarModel.End.Model[i].Location[j + 1].Y / scale,
                            };
                            l1.Stroke = colorSelected;
                            l1.StrokeThickness = strokeMain;
                            canvas.Children.Add(l1);
                        }
                    }
                }
            }
        }
        public static void DrawAddBottomMidBar(Canvas canvas, AddBottomBarModel AddBottomBarModel, DrawModel drawModel, double left, double top, double scale, double strokeMain, SolidColorBrush colorSelected)
        {
            if (AddBottomBarModel.Mid.Model.Count != 0)
            {
                for (int i = 0; i < AddBottomBarModel.Mid.Model.Count; i++)
                {
                    for (int j = 0; j < AddBottomBarModel.Mid.Model[i].Location.Count - 1; j++)
                    {
                        Line l1 = new Line()
                        {
                            X1 = left + AddBottomBarModel.Mid.Model[i].Location[j].X / scale,
                            X2 = left + AddBottomBarModel.Mid.Model[i].Location[j + 1].X / scale,
                            Y1 = top + AddBottomBarModel.Mid.Model[i].Location[j].Y / scale,
                            Y2 = top + AddBottomBarModel.Mid.Model[i].Location[j + 1].Y / scale,
                        };
                        l1.Stroke = colorSelected;
                        l1.StrokeThickness = strokeMain;
                        canvas.Children.Add(l1);
                    }
                }
            }
        }
        public static void DrawSideBar(Canvas canvas, SideBarModel sideBarModel, DrawModel drawModel, double left, double top, double scale, double strokeMain, SolidColorBrush colorSelected)
        {
            if (sideBarModel.IsSide)
            {
                for (int j = 0; j < sideBarModel.Location.Count - 1; j++)
                {
                    Line l1 = new Line()
                    {
                        X1 = left + sideBarModel.Location[j].X / scale,
                        X2 = left + sideBarModel.Location[j + 1].X / scale,
                        Y1 = top + sideBarModel.Location[j].Y / scale,
                        Y2 = top + sideBarModel.Location[j + 1].Y / scale,
                    };
                    l1.Stroke = colorSelected;
                    l1.StrokeThickness = strokeMain;
                    canvas.Children.Add(l1);
                }
            }
        }
        public static void DrawSpecialColumn(Canvas canvas, List<InfoModel> infoModels, SpecialNodeModel specialNodeModel, DrawModel drawModel, SolidColorBrush colorSelected)
        {
            InfoModel infoModel = infoModels.Where(x => x.NumberSpan == specialNodeModel.NumberSpan).FirstOrDefault();
            Line l1 = new Line() { X1 = drawModel.Left + specialNodeModel.Start / drawModel.Scale, X2 = drawModel.Left + specialNodeModel.Start / drawModel.Scale, Y1 = drawModel.Top + Math.Abs(infoModel.zOffset) / drawModel.Scale, Y2 = drawModel.Top - 3 * drawModel.Offset };
            l1.Stroke = colorSelected;
            l1.StrokeThickness = drawModel.StrokeBound;
            canvas.Children.Add(l1);
            Line l2 = new Line() { X1 = drawModel.Left + specialNodeModel.End / drawModel.Scale, X2 = drawModel.Left + specialNodeModel.End / drawModel.Scale, Y1 = drawModel.Top + Math.Abs(infoModel.zOffset) / drawModel.Scale, Y2 = drawModel.Top - 3 * drawModel.Offset };
            l2.Stroke = colorSelected;
            l2.StrokeThickness = drawModel.StrokeBound;
            canvas.Children.Add(l2);
            Line l3 = new Line() { X1 = drawModel.Left + specialNodeModel.Start / drawModel.Scale, X2 = drawModel.Left + specialNodeModel.End / drawModel.Scale, Y1 = drawModel.Top - 3 * drawModel.Offset, Y2 = drawModel.Top - 3 * drawModel.Offset };
            l3.Stroke = colorSelected;
            l3.StrokeThickness = drawModel.StrokeBound;
            l3.StrokeDashArray = new DoubleCollection() { 10, 5 };
            canvas.Children.Add(l3);
        }
        public static void DrawSpecialBeam(Canvas canvas, List<InfoModel> infoModels, SpecialNodeModel specialNodeModel, DrawModel drawModel, SolidColorBrush colorSelected)
        {
            InfoModel infoModel = infoModels.Where(x => x.NumberSpan == specialNodeModel.NumberSpan).FirstOrDefault();
            Line l1 = new Line() { X1 = drawModel.Left + specialNodeModel.Start / drawModel.Scale, X2 = drawModel.Left + specialNodeModel.Start / drawModel.Scale, Y1 = drawModel.Top + Math.Abs(infoModel.zOffset) / drawModel.Scale, Y2 = drawModel.Top + Math.Abs(infoModel.zOffset) / drawModel.Scale + specialNodeModel.Height / drawModel.Scale };
            l1.Stroke = colorSelected;
            l1.StrokeThickness = drawModel.StrokeBound;
            l1.StrokeDashArray = new DoubleCollection() { 10, 5 };
            canvas.Children.Add(l1);
            Line l2 = new Line() { X1 = drawModel.Left + specialNodeModel.End / drawModel.Scale, X2 = drawModel.Left + specialNodeModel.End / drawModel.Scale, Y1 = drawModel.Top + Math.Abs(infoModel.zOffset) / drawModel.Scale, Y2 = drawModel.Top + Math.Abs(infoModel.zOffset) / drawModel.Scale + specialNodeModel.Height / drawModel.Scale };
            l2.Stroke = colorSelected;
            l2.StrokeThickness = drawModel.StrokeBound;
            l2.StrokeDashArray = new DoubleCollection() { 10, 5 };
            canvas.Children.Add(l2);
            Line l3 = new Line() { X1 = drawModel.Left + specialNodeModel.Start / drawModel.Scale, X2 = drawModel.Left + specialNodeModel.End / drawModel.Scale, Y1 = drawModel.Top + Math.Abs(infoModel.zOffset) / drawModel.Scale + specialNodeModel.Height / drawModel.Scale, Y2 = drawModel.Top + Math.Abs(infoModel.zOffset) / drawModel.Scale + specialNodeModel.Height / drawModel.Scale };
            l3.Stroke = colorSelected;
            l3.StrokeThickness = drawModel.StrokeBound;
            l3.StrokeDashArray = new DoubleCollection() { 10, 5 };
            canvas.Children.Add(l3);
        }
        public static void DrawSpecialBar(Canvas canvas, SpecialBarModel specialBarModel, double strokeMain, DrawModel drawModel, SolidColorBrush colorSpecial, SolidColorBrush colorStirrup)
        {
            if (specialBarModel.IsSP)
            {
                for (int i = 0; i < specialBarModel.LocationSP.Count - 1; i++)
                {
                    Line l1 = new Line()
                    {
                        X1 = drawModel.Left + specialBarModel.LocationSP[i].X / drawModel.Scale,
                        X2 = drawModel.Left + specialBarModel.LocationSP[i + 1].X / drawModel.Scale,
                        Y1 = drawModel.Top + specialBarModel.LocationSP[i].Y / drawModel.Scale,
                        Y2 = drawModel.Top + specialBarModel.LocationSP[i + 1].Y / drawModel.Scale,
                    };
                    l1.Stroke = colorSpecial;
                    l1.StrokeThickness = strokeMain;
                    canvas.Children.Add(l1);
                }
            }
            if (specialBarModel.IsST)
            {
                for (int i = 0; i < specialBarModel.NumberST; i++)
                {
                    for (int j = 0; j < specialBarModel.LocationST[i].Count-1; j++)
                    {
                        Line l1 = new Line()
                        {
                            X1 = drawModel.Left + specialBarModel.LocationST[i][j].X / drawModel.Scale,
                            X2 = drawModel.Left + specialBarModel.LocationST[i][j + 1].X / drawModel.Scale,
                            Y1 = drawModel.Top + specialBarModel.LocationST[i][j].Y / drawModel.Scale,
                            Y2 = drawModel.Top + specialBarModel.LocationST[i][j + 1].Y / drawModel.Scale,
                        };
                        l1.Stroke = colorStirrup;
                        l1.StrokeThickness = strokeMain;
                        canvas.Children.Add(l1);
                    }
                }
            }
        }
        #endregion
        #region Orther
        public static double GetHmax(List<InfoModel> infoModels)
        {
            double hmax = 0;
            foreach (var item in infoModels)
            {
                if (hmax < item.h + Math.Abs(item.zOffset))
                {
                    hmax = item.h + Math.Abs(item.zOffset);
                }
            }
            return hmax;
        }
        #endregion
        #region Dimention
        public static void DimHorizontalBottom(Canvas canvas, double left, double top, double scale, double l, int font, double offset, double extend, double strokeDim)
        {
            Line l1 = new Line() { X1 = left - extend, X2 = left + l / scale + extend, Y1 = top + 2 * offset, Y2 = top + 2 * offset };
            l1.Stroke = Brushes.Black;
            l1.StrokeThickness = strokeDim;
            Line l2 = new Line() { X1 = left - extend, X2 = left + extend, Y1 = top + 2 * offset + extend, Y2 = top + 2 * offset - extend };
            l2.Stroke = Brushes.Black;
            l2.StrokeThickness = strokeDim;
            Line l3 = new Line() { X1 = left - extend + l / scale, X2 = left + extend + l / scale, Y1 = top + 2 * offset + extend, Y2 = top + 2 * offset - extend };
            l3.Stroke = Brushes.Black;
            l3.StrokeThickness = strokeDim;
            Line l4 = new Line() { X1 = left, X2 = left, Y1 = top + 2 * offset + extend, Y2 = top + extend };
            l4.Stroke = Brushes.Black;
            l4.StrokeThickness = strokeDim;
            Line l5 = new Line() { X1 = left + l / scale, X2 = left + l / scale, Y1 = top + 2 * offset + extend, Y2 = top + extend };
            l5.Stroke = Brushes.Black;
            l5.StrokeThickness = strokeDim;
            TextBlock text = new TextBlock();
            text.Text = l.ToString();
            text.FontSize = font;
            text.Foreground = Brushes.Black;
            text.FontFamily = new FontFamily("Tahoma");
            text.Measure(new Size(System.Double.PositiveInfinity, System.Double.PositiveInfinity));
            text.Arrange(new Rect(text.DesiredSize));
            Canvas.SetTop(text, top + 2 * offset - 2 * font);
            Canvas.SetLeft(text, left + l / (2 * scale) - text.ActualWidth / 2);
            canvas.Children.Add(l1);
            canvas.Children.Add(l2);
            canvas.Children.Add(l3);
            canvas.Children.Add(l4);
            canvas.Children.Add(l5);
            canvas.Children.Add(text);
        }
        public static void DimHorizontalTop(Canvas canvas, double left, double top, double scale, double l, int font, double offset, double extend, double strokeDim)
        {
            Line l1 = new Line() { X1 = left - extend, X2 = left + l / scale + extend, Y1 = top - 2 * offset, Y2 = top - 2 * offset };
            l1.Stroke = Brushes.Black;
            l1.StrokeThickness = strokeDim;
            Line l2 = new Line() { X1 = left - extend, X2 = left + extend, Y1 = top - 2 * offset + extend, Y2 = top - 2 * offset - extend };
            l2.Stroke = Brushes.Black;
            l2.StrokeThickness = strokeDim;
            Line l3 = new Line() { X1 = left - extend + l / scale, X2 = left + extend + l / scale, Y1 = top - 2 * offset + extend, Y2 = top - 2 * offset - extend };
            l3.Stroke = Brushes.Black;
            l3.StrokeThickness = strokeDim;
            Line l4 = new Line() { X1 = left, X2 = left, Y1 = top - 2 * offset - extend, Y2 = top - extend };
            l4.Stroke = Brushes.Black;
            l4.StrokeThickness = strokeDim;
            Line l5 = new Line() { X1 = left + l / scale, X2 = left + l / scale, Y1 = top - 2 * offset - extend, Y2 = top - extend };
            l5.Stroke = Brushes.Black;
            l5.StrokeThickness = strokeDim;
            TextBlock text = new TextBlock();
            text.Text = l.ToString();
            text.FontSize = font;
            text.Foreground = Brushes.Black;
            text.FontFamily = new FontFamily("Tahoma");
            text.Measure(new Size(System.Double.PositiveInfinity, System.Double.PositiveInfinity));
            text.Arrange(new Rect(text.DesiredSize));
            Canvas.SetTop(text, top - 2 * offset - 2 * font);
            Canvas.SetLeft(text, left + l / (2 * scale) - text.ActualWidth / 2);
            canvas.Children.Add(l1);
            canvas.Children.Add(l2);
            canvas.Children.Add(l3);
            canvas.Children.Add(l4);
            canvas.Children.Add(l5);
            canvas.Children.Add(text);
        }
        public static void DimVerticalLeft(Canvas canvas, double left, double top, double scale, double l, int font, double offset, double extend, double strokeDim)
        {
            Line l1 = new Line() { X1 = left - 2*offset, X2 = left - 2*offset, Y1 = top-extend, Y2 = top + l / scale+extend };
            l1.Stroke = Brushes.Black;
            l1.StrokeThickness = strokeDim;
            Line l2 = new Line() { X1 = left - extend - 2*offset, X2 = left + extend - 2*offset, Y1 = top + extend, Y2 = top - extend };
            l2.Stroke = Brushes.Black;
            l2.StrokeThickness = strokeDim;
            Line l3 = new Line() { X1 = left - extend - 2*offset, X2 = left + extend -2* offset, Y1 = top + l / scale + extend, Y2 = top + l / scale - extend };
            l3.Stroke = Brushes.Black;
            l3.StrokeThickness = strokeDim;
            Line l4 = new Line() { X1 = left - 2*offset - extend, X2 = left - extend, Y1 = top, Y2 = top };
            l4.Stroke = Brushes.Black;
            l4.StrokeThickness = strokeDim;
            Line l5 = new Line() { X1 = left - 2*offset - extend, X2 = left - extend, Y1 = top + l / scale, Y2 = top + l / scale };
            l5.Stroke = Brushes.Black;
            l5.StrokeThickness = strokeDim;
            TextBlock text = new TextBlock();
            text.Text = l.ToString();
            text.FontSize = font;
            text.Foreground = Brushes.Black;
            text.LayoutTransform = new RotateTransform(-90, 0, 0);
            text.Measure(new Size(System.Double.PositiveInfinity, System.Double.PositiveInfinity));
            text.Arrange(new Rect(text.DesiredSize));
            Canvas.SetTop(text, top + l / (2 * scale)-text.ActualWidth);
            Canvas.SetLeft(text, left - 2*offset - 2*font);
            canvas.Children.Add(l1);
            canvas.Children.Add(l2);
            canvas.Children.Add(l3);
            canvas.Children.Add(l4);
            canvas.Children.Add(l5);
            canvas.Children.Add(text);

        }
        #endregion
        #region Reinforcement Area
        public static void DrawSection(Canvas canvas,List<InfoModel> infoModels, List<StirrupModel> stirrupModels, List<DistributeStirrup> distributeStirrups, double left, double top, double scale,double c,  double d,double a, SolidColorBrush colorStirrup, SolidColorBrush colorMainBar,  int selectedSpan, double dimV, double dimH, double extend, double hook, string level)
        {
            double Top = top + Math.Abs(infoModels[selectedSpan].zOffset) / scale;
            //DrawImage.DrawLineLevel(canvas, left, top, scale, 20, level, 370);
            DrawImage.DrawSection(canvas, scale, left, Top, infoModels[selectedSpan].b, infoModels[selectedSpan].h);
            DimHorizontalTop(canvas, left, Top, scale, infoModels[selectedSpan].b, 11, dimV, extend, 0.5);
            DimVerticalLeft(canvas, left, Top, scale, infoModels[selectedSpan].h, 11, dimH, extend, 0.5);

            if (stirrupModels[selectedSpan].Type == 0)
            {
                DrawImage.DrawStirrup(canvas, left, Top, scale, infoModels[selectedSpan].b, infoModels[selectedSpan].h, c, stirrupModels[selectedSpan].BarS.Diameter, d, colorStirrup);
            }
            else
            {
                DrawImage.DrawStirrup(canvas, left, Top, scale, (infoModels[selectedSpan].b + 2 * c + 2 * stirrupModels[selectedSpan].BarS.Diameter + a) / 2, infoModels[selectedSpan].h, c, stirrupModels[selectedSpan].BarS.Diameter, d, colorStirrup);
                DrawImage.DrawStirrup(canvas, left + (infoModels[selectedSpan].b - 2 * c - 2 * stirrupModels[selectedSpan].BarS.Diameter - a) / (2 * scale), Top, scale, (infoModels[selectedSpan].b + 2 * c + 2 * stirrupModels[selectedSpan].BarS.Diameter + a) / 2, infoModels[selectedSpan].h, c, stirrupModels[selectedSpan].BarS.Diameter, d, colorStirrup);
            }

            if (stirrupModels[selectedSpan].Anti)
            {
                switch (stirrupModels[selectedSpan].Na)
                {
                    case 0:
                        break;
                    case 1:
                        DrawImage.DrawHook(canvas, left, Top + infoModels[selectedSpan].h / (2 * scale), scale, infoModels[selectedSpan].b, c, stirrupModels[selectedSpan].BarA.Diameter, d, hook, colorMainBar);
                        break;
                    case 2:
                        double dis = (infoModels[selectedSpan].h - 2 * c - 2 * stirrupModels[selectedSpan].BarS.Diameter - d) / 3;
                        DrawImage.DrawHook(canvas, left, Top + infoModels[selectedSpan].h / (2 * scale) - dis / (2 * scale), scale, infoModels[selectedSpan].b, c, stirrupModels[selectedSpan].BarA.Diameter, d, hook, colorMainBar);
                        DrawImage.DrawHook(canvas, left, Top + infoModels[selectedSpan].h / (2 * scale) + dis / (2 * scale), scale, infoModels[selectedSpan].b, c, stirrupModels[selectedSpan].BarA.Diameter, d, hook, colorMainBar);
                        break;
                    default:
                        break;
                }
            }
        }
        public static void DrawSectionBar(Canvas canvas, List<InfoModel> infoModels, List<StirrupModel> stirrupModels, List<DistributeStirrup> distributeStirrups, List<SideBarModel> SideBarModel, ObservableCollection<SectionAreaModel> SectionAreaModels, double left, double top, double scale, double c, double ds, double tagV, double tagH, SolidColorBrush colorMainBar, SolidColorBrush tag, int selectedSpan, bool start, bool mid, bool end)
        {
            double s1 = 0, s2 = 0;
            if (distributeStirrups[selectedSpan].Type==0)
            {
                s1 = distributeStirrups[selectedSpan].S;
                s2 = distributeStirrups[selectedSpan].S;
                
            }
            else
            {
                s1 = distributeStirrups[selectedSpan].S1;
                s2 = distributeStirrups[selectedSpan].S2;
            }
            
            //Side
            double Top = top + Math.Abs(infoModels[selectedSpan].zOffset) / scale;
            if (SideBarModel[selectedSpan].IsSide)
            {
                DrawImage.DrawLayerBarTag(canvas, left, Top + (infoModels[selectedSpan].h / 2) / scale, scale, infoModels[selectedSpan].b, c, ds, SideBarModel[selectedSpan].Bar.Diameter, SideBarModel[selectedSpan].NumberBar,tagV,tagH, SideBarModel[selectedSpan].Bar.Type, colorMainBar,tag,false);
            }
            //Start
            if (start)
            {
                DrawImage.DrawTagStirrup(canvas, left, top + infoModels[selectedSpan].h / (2 * scale), scale, infoModels[selectedSpan].b, c, tagH, s1, stirrupModels[selectedSpan].BarS.Diameter, tag);
                for (int i = 0; i < SectionAreaModels[selectedSpan].Start.Count; i++)
                {
                    if (i==0)
                    {
                        DrawImage.DrawLayerBarTag(canvas, left, top + SectionAreaModels[selectedSpan].Start[i].Y0/scale, scale, infoModels[selectedSpan].b, c, ds, SectionAreaModels[selectedSpan].Start[i].Bar.Diameter, SectionAreaModels[selectedSpan].Start[i].NumberBar, tagV, tagH, SectionAreaModels[selectedSpan].Start[i].Bar.Type, colorMainBar, tag, true);
                    }
                    else
                    {
                        if (i== SectionAreaModels[selectedSpan].Start.Count-1)
                        {
                            DrawImage.DrawLayerBarTag(canvas, left, top + SectionAreaModels[selectedSpan].Start[i].Y0 / scale, scale, infoModels[selectedSpan].b, c, ds, SectionAreaModels[selectedSpan].Start[i].Bar.Diameter, SectionAreaModels[selectedSpan].Start[i].NumberBar, tagV, tagH, SectionAreaModels[selectedSpan].Start[i].Bar.Type, colorMainBar, tag, false);
                        }
                        else
                        {
                            bool a = SectionAreaModels[selectedSpan].Start[i].Y0 > Math.Abs(infoModels[selectedSpan].zOffset)+ infoModels[selectedSpan].h / 2;
                            if (SectionAreaModels[selectedSpan].Start[i].NumberBar==1)
                            {
                                DrawImage.DrawLayerBarTagAddTop(canvas, left, top + SectionAreaModels[selectedSpan].Start[i].Y0 / scale, scale, infoModels[selectedSpan].b, c, ds, SectionAreaModels[selectedSpan].Start[i].Bar.Diameter, SectionAreaModels[selectedSpan].Start[i].NumberBar, tagV, tagH, SectionAreaModels[selectedSpan].Start[i].Bar.Type, colorMainBar, tag, a);
                            }
                            else
                            {
                                DrawImage.DrawLayerBarTag(canvas, left, top + SectionAreaModels[selectedSpan].Start[i].Y0 / scale, scale, infoModels[selectedSpan].b, c, ds, SectionAreaModels[selectedSpan].Start[i].Bar.Diameter, SectionAreaModels[selectedSpan].Start[i].NumberBar, tagV, tagH, SectionAreaModels[selectedSpan].Start[i].Bar.Type, colorMainBar, tag, a);
                            }
                        }
                    }
                }
            }
            if (mid)
            {
                DrawImage.DrawTagStirrup(canvas, left, top + infoModels[selectedSpan].h / (2 * scale), scale, infoModels[selectedSpan].b, c, tagH, s2, stirrupModels[selectedSpan].BarS.Diameter, tag);
                for (int i = 0; i < SectionAreaModels[selectedSpan].Middle.Count; i++)
                {
                    if (i == 0)
                    {
                        DrawImage.DrawLayerBarTag(canvas, left, top + SectionAreaModels[selectedSpan].Middle[i].Y0 / scale, scale, infoModels[selectedSpan].b, c, ds, SectionAreaModels[selectedSpan].Middle[i].Bar.Diameter, SectionAreaModels[selectedSpan].Middle[i].NumberBar, tagV, tagH, SectionAreaModels[selectedSpan].Middle[i].Bar.Type, colorMainBar, tag, true);
                    }
                    else
                    {
                        if (i == SectionAreaModels[selectedSpan].Middle.Count - 1)
                        {
                            DrawImage.DrawLayerBarTag(canvas, left, top + SectionAreaModels[selectedSpan].Middle[i].Y0 / scale, scale, infoModels[selectedSpan].b, c, ds, SectionAreaModels[selectedSpan].Middle[i].Bar.Diameter, SectionAreaModels[selectedSpan].Middle[i].NumberBar, tagV, tagH, SectionAreaModels[selectedSpan].Middle[i].Bar.Type, colorMainBar, tag, false);
                        }
                        else
                        {
                            bool a = SectionAreaModels[selectedSpan].Middle[i].Y0 > Math.Abs(infoModels[selectedSpan].zOffset) + infoModels[selectedSpan].h / 2;
                            if (SectionAreaModels[selectedSpan].Middle[i].NumberBar == 1)
                            {
                                DrawImage.DrawLayerBarTagAddTop(canvas, left, top + SectionAreaModels[selectedSpan].Middle[i].Y0 / scale, scale, infoModels[selectedSpan].b, c, ds, SectionAreaModels[selectedSpan].Middle[i].Bar.Diameter, SectionAreaModels[selectedSpan].Middle[i].NumberBar, tagV, tagH, SectionAreaModels[selectedSpan].Middle[i].Bar.Type, colorMainBar, tag, a);
                            }
                            else
                            {
                                DrawImage.DrawLayerBarTag(canvas, left, top + SectionAreaModels[selectedSpan].Middle[i].Y0 / scale, scale, infoModels[selectedSpan].b, c, ds, SectionAreaModels[selectedSpan].Middle[i].Bar.Diameter, SectionAreaModels[selectedSpan].Middle[i].NumberBar, tagV, tagH, SectionAreaModels[selectedSpan].Middle[i].Bar.Type, colorMainBar, tag, a);
                            }
                        }
                    }
                }
            }
            if (end)
            {
                DrawImage.DrawTagStirrup(canvas, left, top + infoModels[selectedSpan].h / (2 * scale), scale, infoModels[selectedSpan].b, c, tagH, s1, stirrupModels[selectedSpan].BarS.Diameter, tag);
                for (int i = 0; i < SectionAreaModels[selectedSpan].End.Count; i++)
                {
                    if (i == 0)
                    {
                        DrawImage.DrawLayerBarTag(canvas, left, top + SectionAreaModels[selectedSpan].End[i].Y0 / scale, scale, infoModels[selectedSpan].b, c, ds, SectionAreaModels[selectedSpan].End[i].Bar.Diameter, SectionAreaModels[selectedSpan].End[i].NumberBar, tagV, tagH, SectionAreaModels[selectedSpan].End[i].Bar.Type, colorMainBar, tag, true);
                    }
                    else
                    {
                        if (i == SectionAreaModels[selectedSpan].End.Count - 1)
                        {
                            DrawImage.DrawLayerBarTag(canvas, left, top + SectionAreaModels[selectedSpan].End[i].Y0 / scale, scale, infoModels[selectedSpan].b, c, ds, SectionAreaModels[selectedSpan].End[i].Bar.Diameter, SectionAreaModels[selectedSpan].End[i].NumberBar, tagV, tagH, SectionAreaModels[selectedSpan].End[i].Bar.Type, colorMainBar, tag, false);
                        }
                        else
                        {
                            bool a = SectionAreaModels[selectedSpan].End[i].Y0 > Math.Abs(infoModels[selectedSpan].zOffset) + infoModels[selectedSpan].h / 2;
                            if (SectionAreaModels[selectedSpan].End[i].NumberBar == 1)
                            {
                                DrawImage.DrawLayerBarTagAddTop(canvas, left, top + SectionAreaModels[selectedSpan].End[i].Y0 / scale, scale, infoModels[selectedSpan].b, c, ds, SectionAreaModels[selectedSpan].End[i].Bar.Diameter, SectionAreaModels[selectedSpan].End[i].NumberBar, tagV, tagH, SectionAreaModels[selectedSpan].End[i].Bar.Type, colorMainBar, tag, a);
                            }
                            else
                            {
                                DrawImage.DrawLayerBarTag(canvas, left, top + SectionAreaModels[selectedSpan].End[i].Y0 / scale, scale, infoModels[selectedSpan].b, c, ds, SectionAreaModels[selectedSpan].End[i].Bar.Diameter, SectionAreaModels[selectedSpan].End[i].NumberBar, tagV, tagH, SectionAreaModels[selectedSpan].End[i].Bar.Type, colorMainBar, tag, a);
                            }
                        }
                    }
                }
            }
        }
        #endregion
        #region 
        public static void DimHorizontalTopOverlap(Canvas canvas, double left, double top, double scale, double l, int font, double offset, double extend, double strokeDim)
        {
            Line l1 = new Line() { X1 = left - extend, X2 = left + l / scale + extend, Y1 = top - 2 * offset, Y2 = top - 2 * offset };
            l1.Stroke = Brushes.Black;
            l1.StrokeThickness = strokeDim;
            Line l2 = new Line() { X1 = left - extend, X2 = left + extend, Y1 = top - 2 * offset + extend, Y2 = top - 2 * offset - extend };
            l2.Stroke = Brushes.Black;
            l2.StrokeThickness = strokeDim;
            Line l3 = new Line() { X1 = left - extend + l / scale, X2 = left + extend + l / scale, Y1 = top - 2 * offset + extend, Y2 = top - 2 * offset - extend };
            l3.Stroke = Brushes.Black;
            l3.StrokeThickness = strokeDim;
            Line l4 = new Line() { X1 = left, X2 = left, Y1 = top - 2 * offset - extend, Y2 = top - extend };
            l4.Stroke = Brushes.Black;
            l4.StrokeThickness = strokeDim;
            Line l5 = new Line() { X1 = left + l / scale, X2 = left + l / scale, Y1 = top - 2 * offset - extend, Y2 = top - extend };
            l5.Stroke = Brushes.Black;
            l5.StrokeThickness = strokeDim;
            TextBlock text = new TextBlock();
            text.Text = "Overlap = "+l.ToString();
            text.FontSize = font;
            text.Foreground = Brushes.Black;
            text.FontFamily = new FontFamily("Tahoma");
            text.Measure(new Size(System.Double.PositiveInfinity, System.Double.PositiveInfinity));
            text.Arrange(new Rect(text.DesiredSize));
            Canvas.SetTop(text, top - 2 * offset - 2 * font);
            Canvas.SetLeft(text, left + l / (2 * scale) - text.ActualWidth / 2);
            canvas.Children.Add(l1);
            canvas.Children.Add(l2);
            canvas.Children.Add(l3);
            canvas.Children.Add(l4);
            canvas.Children.Add(l5);
            canvas.Children.Add(text);
        }
        #endregion
    }
}
