using Autodesk.Revit.DB;
using DSP;
using System;
using System.Collections.ObjectModel;

namespace R11_FoundationPile
{
    public class ProcessCurveRebar
    {
        #region Bottom
        public static ObservableCollection<Curve> GetCurvesMainItem0(SettingModel settingModel, FoundationModel FoundationModel, FoundationBarModel FoundationBarModel, BarModel BarModel, UnitProject Unit, bool bottom, double dMainBottom, double dSide, double CoverTop, double CoverBottom, double CoverSide)
        {

            double Diameter = BarModel.Bar.Diameter;
            XYZ x = null;
            XYZ y = null;
            XYZ p0 = FoundationModel.ColumnModel.PointPosition;
            ObservableCollection<Curve> curves = new ObservableCollection<Curve>();
            bool Horizontal = (FoundationBarModel.SpanOrientation.Equals("Horizontal"));
            double x0 = FoundationModel.BoundingLocation[0].X;
            double x1 = FoundationModel.BoundingLocation[1].X;
            double x2 = FoundationModel.BoundingLocation[2].X;
            double x3 = FoundationModel.BoundingLocation[3].X;
            double x4 = FoundationModel.BoundingLocation[4].X;
            double x5 = FoundationModel.BoundingLocation[5].X;
            double y0 = FoundationModel.BoundingLocation[0].Y;
            double y1 = FoundationModel.BoundingLocation[1].Y;
            double y2 = FoundationModel.BoundingLocation[2].Y;
            double y3 = FoundationModel.BoundingLocation[3].Y;
            double y4 = FoundationModel.BoundingLocation[4].Y;
            double y5 = FoundationModel.BoundingLocation[5].Y;
            double deltaZ0 = (bottom) ? (settingModel.HeightFoundation - CoverBottom) : (CoverTop);
            if (FoundationModel.ColumnModel.Style.Equals("RECTANGLE"))
            {
                x = FoundationModel.ColumnModel.East.FaceNormal;
                y = FoundationModel.ColumnModel.Nouth.FaceNormal;
            }
            else
            {
                x = XYZ.BasisX;
                y = XYZ.BasisY;
            }

            if (FoundationModel.ColumnModel.Style.Equals("RECTANGLE") && FoundationModel.ColumnModel.b > FoundationModel.ColumnModel.h)
            {
                if (Horizontal)
                {
                    double plus = 0;
                    double total = Math.Abs(y0 - y5) - 2 * CoverSide - Diameter;
                    double detatay5y3 = Math.Abs(y5 - y3) - CoverSide - 0.5 * Diameter;
                    double detatay3y2 = Math.Abs(y3 - y2);
                    double detatay2y0 = Math.Abs(y2 - y0) - CoverSide - 0.5 * Diameter;

                    double deltax0x1 = Math.Abs(x0 - x1);
                    double deltax1x2 = Math.Abs(x1 - x2);


                    int ii = (int)((detatay5y3 + detatay3y2) / BarModel.Distance);
                    for (int i = 0; i < BarModel.Number; i++)
                    {
                        double dY = (bottom) ? (i * BarModel.Distance) : (dMainBottom + dSide + Diameter * 0.5 + i * BarModel.Distance);
                        double deltaY0 = y0 + ((y0 > 0) ? -1 : 1) * (CoverSide + dY);
                        double deltaY5 = y5 + ((y5 > 0) ? -1 : 1) * (CoverSide + dY);
                        double dX = (bottom) ? (0) : (dMainBottom + dSide + Diameter * 0.5);
                        double deltaX0 = x0 + ((x0 > 0) ? -1 : 1) * (CoverSide + dX);
                        double deltaX1 = x1;
                        double deltaX2 = x2;
                        double tanA = Math.Abs(x1 - x2) / Math.Abs(y1 - y2);

                        if (plus <= detatay5y3)
                        {
                            XYZ p1 = p0 + Unit.Convert(deltaX0) * x + Unit.Convert(deltaY0) * y + Unit.Convert(-deltaZ0) * XYZ.BasisZ;
                            XYZ p2 = p0 + Unit.Convert(deltaX1 + ((deltaX1 >= 0) ? 1 : -1) * ((CoverSide - 0.5 * Diameter + i * BarModel.Distance) * tanA)) * x + Unit.Convert(deltaY0) * y + Unit.Convert(-deltaZ0) * XYZ.BasisZ;
                            XYZ p1a = p0 + Unit.Convert(deltaX0) * x + Unit.Convert(deltaY5) * y + Unit.Convert(-deltaZ0) * XYZ.BasisZ;
                            XYZ p2a = p0 + Unit.Convert(deltaX1 + ((deltaX1 >= 0) ? 1 : -1) * ((CoverSide - 0.5 * Diameter + i * BarModel.Distance) * tanA)) * x + Unit.Convert(deltaY5) * y + Unit.Convert(-deltaZ0) * XYZ.BasisZ;
                            curves.Add(Line.CreateBound(p1, p2));
                            curves.Add(Line.CreateBound(p1a, p2a));
                        }
                        if (plus <= detatay5y3 + detatay3y2 && plus > detatay5y3)
                        {
                            XYZ p1 = p0 + Unit.Convert(deltaX0) * x + Unit.Convert(deltaY0) * y + Unit.Convert(-deltaZ0) * XYZ.BasisZ;
                            XYZ p2 = p0 + Unit.Convert(deltaX2 + ((deltaX2 > 0) ? -1 : 1) * (CoverSide - 0.5 * Diameter)) * x + Unit.Convert(deltaY0) * y + Unit.Convert(-deltaZ0) * XYZ.BasisZ;
                            curves.Add(Line.CreateBound(p1, p2));

                        }

                        plus += BarModel.Distance;
                    }
                }
                else
                {
                    double plus = 0;
                    double total = Math.Abs(x0 - x2) - 2 * CoverSide - Diameter;
                    double detatay1y2 = Math.Abs(y1 - y2);
                    double deltax0x1 = Math.Abs(x0 - x1) - CoverSide - 0.5 * Diameter;
                    double deltax1x2 = Math.Abs(x1 - x2) - CoverSide - 0.5 * Diameter;
                    for (int i = 0; i < BarModel.Number; i++)
                    {
                        double dY = (bottom) ? (0) : (dMainBottom + dSide + Diameter * 0.5);
                        double deltaY0 = y0 + ((y0 > 0) ? -1 : 1) * (CoverSide + dY);
                        double deltaY5 = y5 + ((y5 > 0) ? -1 : 1) * (CoverSide + dY);
                        double dX = (bottom) ? (i * BarModel.Distance) : (dMainBottom + dSide + Diameter * 0.5 + i * BarModel.Distance);
                        double deltaX0 = x0 + ((x0 > 0) ? -1 : 1) * (CoverSide + dX);
                        double deltaX5 = x5 + ((x5 > 0) ? -1 : 1) * (CoverSide + dX);

                        if (plus <= deltax0x1)
                        {
                            XYZ p1 = p0 + Unit.Convert(deltaX0) * x + Unit.Convert(deltaY0) * y + Unit.Convert(-deltaZ0) * XYZ.BasisZ;
                            XYZ p2 = p0 + Unit.Convert(deltaX5) * x + Unit.Convert(deltaY5) * y + Unit.Convert(-deltaZ0) * XYZ.BasisZ;
                            curves.Add(Line.CreateBound(p1, p2));
                        }
                        else
                        {
                            double detataX00 = Math.Abs(plus - deltax0x1);

                            double detataY00 = detataX00 * detatay1y2 / deltax1x2;

                            XYZ p1 = p0 + Unit.Convert(deltaX0) * x + Unit.Convert(deltaY0 + ((deltaY0 > 0) ? (-detataY00) : (detataY00))) * y + Unit.Convert(-deltaZ0) * XYZ.BasisZ;
                            XYZ p2 = p0 + Unit.Convert(deltaX0) * x + Unit.Convert(deltaY5 + ((deltaY5 > 0) ? (-detataY00) : (detataY00))) * y + Unit.Convert(-deltaZ0) * XYZ.BasisZ;
                            curves.Add(Line.CreateBound(p1, p2));
                        }
                        plus += BarModel.Distance;
                    }

                }
            }
            else
            {
                if (Horizontal)
                {
                    double plus = 0;
                    double total = Math.Abs(y0 - y2) - 2 * CoverSide - Diameter;
                    double detatax1x2 = Math.Abs(x1 - x2);
                    double deltay0y1 = Math.Abs(y0 - y1) - CoverSide - 0.5 * Diameter;
                    double deltay1y2 = Math.Abs(y1 - y2) - CoverSide - 0.5 * Diameter;
                    for (int i = 0; i < BarModel.Number; i++)
                    {
                        double dX = (bottom) ? (0) : (dMainBottom + dSide + Diameter * 0.5);
                        double deltaX0 = x0 + ((x0 > 0) ? -1 : 1) * (CoverSide + dX);
                        double deltaX5 = x5 + ((x5 > 0) ? -1 : 1) * (CoverSide + dX);
                        double dY = (bottom) ? (i * BarModel.Distance) : (dMainBottom + dSide + Diameter * 0.5 + i * BarModel.Distance);
                        double deltaY0 = y0 + ((y0 > 0) ? -1 : 1) * (CoverSide + dY);
                        double deltaY5 = y5 + ((y5 > 0) ? -1 : 1) * (CoverSide + dY);

                        if (plus <= deltay0y1)
                        {
                            XYZ p1 = p0 + Unit.Convert(deltaX0) * x + Unit.Convert(deltaY0) * y + Unit.Convert(-deltaZ0) * XYZ.BasisZ;
                            XYZ p2 = p0 + Unit.Convert(deltaX5) * x + Unit.Convert(deltaY0) * y + Unit.Convert(-deltaZ0) * XYZ.BasisZ;
                            curves.Add(Line.CreateBound(p1, p2));
                        }
                        else
                        {
                            double detataY00 = Math.Abs(plus - deltay0y1);

                            double detataX00 = detataY00 * detatax1x2 / deltay1y2;

                            XYZ p1 = p0 + Unit.Convert(deltaX0 + ((deltaX0 > 0) ? (-detataX00) : (detataX00))) * x + Unit.Convert(deltaY0) * y + Unit.Convert(-deltaZ0) * XYZ.BasisZ;
                            XYZ p2 = p0 + Unit.Convert(deltaX5 + ((deltaX5 > 0) ? (-detataX00) : (detataX00))) * x + Unit.Convert(deltaY0) * y + Unit.Convert(-deltaZ0) * XYZ.BasisZ;
                            curves.Add(Line.CreateBound(p1, p2));
                        }
                        plus += BarModel.Distance;
                    }
                }
                else
                {

                    double plus = 0;
                    double total = Math.Abs(x0 - x5) - 2 * CoverSide - Diameter;
                    double detatax5x3 = Math.Abs(x5 - x3) - CoverSide - 0.5 * Diameter;
                    double detatax3x2 = Math.Abs(x3 - x2);
                    double detatax2x0 = Math.Abs(x2 - x0) - CoverSide - 0.5 * Diameter;
                    double deltay0y1 = Math.Abs(y0 - y1);
                    double deltay1y2 = Math.Abs(y1 - y2);
                    int ii = (int)((detatax5x3 + detatax3x2) / BarModel.Distance);
                    for (int i = 0; i < BarModel.Number; i++)
                    {
                        double dX = (bottom) ? (i * BarModel.Distance) : (dMainBottom + dSide + Diameter * 0.5 + i * BarModel.Distance);
                        double deltaX0 = x0 + ((x0 > 0) ? -1 : 1) * (CoverSide + dX);
                        double deltaX5 = x5 + ((x5 > 0) ? -1 : 1) * (CoverSide + dX);
                        double dY = (bottom) ? (0) : (dMainBottom + dSide + Diameter * 0.5);
                        double deltaY0 = y0 + ((y0 > 0) ? -1 : 1) * (CoverSide + dY);
                        double deltaY1 = y1;
                        double deltaY2 = y2;
                        double tanA = Math.Abs(y1 - y2) / Math.Abs(x1 - x2);

                        if (plus <= detatax5x3)
                        {

                            XYZ p1 = p0 + Unit.Convert(deltaX5) * x + Unit.Convert(deltaY0) * y + Unit.Convert(-deltaZ0) * XYZ.BasisZ;
                            XYZ p2 = p0 + Unit.Convert(deltaX5) * x + Unit.Convert(deltaY1 + ((deltaY1 >= 0) ? -1 : 1) * ((CoverSide - 0.5 * Diameter + i * BarModel.Distance) * tanA)) * y + Unit.Convert(-deltaZ0) * XYZ.BasisZ;
                            XYZ p1a = p0 + Unit.Convert(deltaX0) * x + Unit.Convert(deltaY0) * y + Unit.Convert(-deltaZ0) * XYZ.BasisZ;
                            XYZ p2a = p0 + Unit.Convert(deltaX0) * x + Unit.Convert(deltaY1 + ((deltaY1 >= 0) ? -1 : 1) * ((CoverSide - 0.5 * Diameter + i * BarModel.Distance) * tanA)) * y + Unit.Convert(-deltaZ0) * XYZ.BasisZ;
                            curves.Add(Line.CreateBound(p1, p2));
                            curves.Add(Line.CreateBound(p1a, p2a));
                        }
                        if (plus <= detatax5x3 + detatax3x2 && plus > detatax5x3)
                        {
                            XYZ p1 = p0 + Unit.Convert(deltaX5) * x + Unit.Convert(deltaY0) * y + Unit.Convert(-deltaZ0) * XYZ.BasisZ;
                            XYZ p2 = p0 + Unit.Convert(deltaX5) * x + Unit.Convert(deltaY2 + ((deltaY2 > 0) ? -1 : 1) * (CoverSide - 0.5 * Diameter)) * y + Unit.Convert(-deltaZ0) * XYZ.BasisZ;
                            curves.Add(Line.CreateBound(p1, p2));

                        }

                        plus += BarModel.Distance;
                    }
                }
            }

            return curves;
        }
        public static ObservableCollection<Curve> GetCurvesSecondaryItem0(SettingModel settingModel, FoundationModel FoundationModel, FoundationBarModel FoundationBarModel, BarModel BarModel, UnitProject Unit, bool bottom, double dMainBottom, double dMainTop, double dSide, double CoverTop, double CoverBottom, double CoverSide)
        {

            double Diameter = BarModel.Bar.Diameter;
            XYZ x = null;
            XYZ y = null;
            XYZ p0 = FoundationModel.ColumnModel.PointPosition;
            ObservableCollection<Curve> curves = new ObservableCollection<Curve>();
            bool Horizontal = (FoundationBarModel.SpanOrientation.Equals("Horizontal"));
            double x0 = FoundationModel.BoundingLocation[0].X;
            double x1 = FoundationModel.BoundingLocation[1].X;
            double x2 = FoundationModel.BoundingLocation[2].X;
            double x3 = FoundationModel.BoundingLocation[3].X;
            double x4 = FoundationModel.BoundingLocation[4].X;
            double x5 = FoundationModel.BoundingLocation[5].X;
            double y0 = FoundationModel.BoundingLocation[0].Y;
            double y1 = FoundationModel.BoundingLocation[1].Y;
            double y2 = FoundationModel.BoundingLocation[2].Y;
            double y3 = FoundationModel.BoundingLocation[3].Y;
            double y4 = FoundationModel.BoundingLocation[4].Y;
            double y5 = FoundationModel.BoundingLocation[5].Y;
            double deltaZ0 = (bottom) ? (settingModel.HeightFoundation - CoverBottom - dMainBottom) : (CoverTop + dMainTop);
            if (FoundationModel.ColumnModel.Style.Equals("RECTANGLE"))
            {
                x = FoundationModel.ColumnModel.East.FaceNormal;
                y = FoundationModel.ColumnModel.Nouth.FaceNormal;
            }
            else
            {
                x = XYZ.BasisX;
                y = XYZ.BasisY;
            }

            if (FoundationModel.ColumnModel.Style.Equals("RECTANGLE") && FoundationModel.ColumnModel.b > FoundationModel.ColumnModel.h)
            {
                if (Horizontal)
                {


                    double plus = 0;
                    double total = Math.Abs(x0 - x2) - 2 * CoverSide - Diameter;
                    double detatay1y2 = Math.Abs(y1 - y2);
                    double deltax0x1 = Math.Abs(x0 - x1) - CoverSide - 0.5 * Diameter;
                    double deltax1x2 = Math.Abs(x1 - x2) - CoverSide - 0.5 * Diameter;
                    for (int i = 0; i < BarModel.Number; i++)
                    {
                        double dY = (bottom) ? (0) : (dMainBottom + dSide + Diameter * 0.5);
                        double deltaY0 = y0 + ((y0 > 0) ? -1 : 1) * (CoverSide + dY);
                        double deltaY5 = y5 + ((y5 > 0) ? -1 : 1) * (CoverSide + dY);
                        double dX = (bottom) ? (i * BarModel.Distance) : (dMainBottom + dSide + Diameter * 0.5 + i * BarModel.Distance);
                        double deltaX0 = x0 + ((x0 > 0) ? -1 : 1) * (CoverSide + dX);
                        double deltaX5 = x5 + ((x5 > 0) ? -1 : 1) * (CoverSide + dX);

                        if (plus <= deltax0x1)
                        {
                            XYZ p1 = p0 + Unit.Convert(deltaX0) * x + Unit.Convert(deltaY0) * y + Unit.Convert(-deltaZ0) * XYZ.BasisZ;
                            XYZ p2 = p0 + Unit.Convert(deltaX5) * x + Unit.Convert(deltaY5) * y + Unit.Convert(-deltaZ0) * XYZ.BasisZ;
                            curves.Add(Line.CreateBound(p1, p2));
                        }
                        else
                        {
                            double detataX00 = Math.Abs(plus - deltax0x1);

                            double detataY00 = detataX00 * detatay1y2 / deltax1x2;

                            XYZ p1 = p0 + Unit.Convert(deltaX0) * x + Unit.Convert(deltaY0 + ((deltaY0 > 0) ? (-detataY00) : (detataY00))) * y + Unit.Convert(-deltaZ0) * XYZ.BasisZ;
                            XYZ p2 = p0 + Unit.Convert(deltaX0) * x + Unit.Convert(deltaY5 + ((deltaY5 > 0) ? (-detataY00) : (detataY00))) * y + Unit.Convert(-deltaZ0) * XYZ.BasisZ;
                            curves.Add(Line.CreateBound(p1, p2));
                        }
                        plus += BarModel.Distance;
                    }

                }
                else
                {
                    double plus = 0;
                    double total = Math.Abs(y0 - y5) - 2 * CoverSide - Diameter;
                    double detatay5y3 = Math.Abs(y5 - y3) - CoverSide - 0.5 * Diameter;
                    double detatay3y2 = Math.Abs(y3 - y2);
                    double detatay2y0 = Math.Abs(y2 - y0) - CoverSide - 0.5 * Diameter;

                    double deltax0x1 = Math.Abs(x0 - x1);
                    double deltax1x2 = Math.Abs(x1 - x2);


                    int ii = (int)((detatay5y3 + detatay3y2) / BarModel.Distance);
                    for (int i = 0; i < BarModel.Number; i++)
                    {
                        double dY = (bottom) ? (i * BarModel.Distance) : (dMainBottom + dSide + Diameter * 0.5 + i * BarModel.Distance);
                        double deltaY0 = y0 + ((y0 > 0) ? -1 : 1) * (CoverSide + dY);
                        double deltaY5 = y5 + ((y5 > 0) ? -1 : 1) * (CoverSide + dY);
                        double dX = (bottom) ? (0) : (dMainBottom + dSide + Diameter * 0.5);
                        double deltaX0 = x0 + ((x0 > 0) ? -1 : 1) * (CoverSide + dX);
                        double deltaX1 = x1;
                        double deltaX2 = x2;
                        double tanA = Math.Abs(x1 - x2) / Math.Abs(y1 - y2);

                        if (plus <= detatay5y3)
                        {
                            XYZ p1 = p0 + Unit.Convert(deltaX0) * x + Unit.Convert(deltaY0) * y + Unit.Convert(-deltaZ0) * XYZ.BasisZ;
                            XYZ p2 = p0 + Unit.Convert(deltaX1 + ((deltaX1 >= 0) ? 1 : -1) * ((CoverSide - 0.5 * Diameter + i * BarModel.Distance) * tanA)) * x + Unit.Convert(deltaY0) * y + Unit.Convert(-deltaZ0) * XYZ.BasisZ;
                            XYZ p1a = p0 + Unit.Convert(deltaX0) * x + Unit.Convert(deltaY5) * y + Unit.Convert(-deltaZ0) * XYZ.BasisZ;
                            XYZ p2a = p0 + Unit.Convert(deltaX1 + ((deltaX1 >= 0) ? 1 : -1) * ((CoverSide - 0.5 * Diameter + i * BarModel.Distance) * tanA)) * x + Unit.Convert(deltaY5) * y + Unit.Convert(-deltaZ0) * XYZ.BasisZ;
                            curves.Add(Line.CreateBound(p1, p2));
                            curves.Add(Line.CreateBound(p1a, p2a));
                        }
                        if (plus <= detatay5y3 + detatay3y2 && plus > detatay5y3)
                        {
                            XYZ p1 = p0 + Unit.Convert(deltaX0) * x + Unit.Convert(deltaY0) * y + Unit.Convert(-deltaZ0) * XYZ.BasisZ;
                            XYZ p2 = p0 + Unit.Convert(deltaX2 + ((deltaX2 > 0) ? -1 : 1) * (CoverSide - 0.5 * Diameter)) * x + Unit.Convert(deltaY0) * y + Unit.Convert(-deltaZ0) * XYZ.BasisZ;
                            curves.Add(Line.CreateBound(p1, p2));

                        }

                        plus += BarModel.Distance;
                    }
                }
            }
            else
            {
                if (Horizontal)
                {
                    double plus = 0;
                    double total = Math.Abs(x0 - x5) - 2 * CoverSide - Diameter;
                    double detatax5x3 = Math.Abs(x5 - x3) - CoverSide - 0.5 * Diameter;
                    double detatax3x2 = Math.Abs(x3 - x2);
                    double detatax2x0 = Math.Abs(x2 - x0) - CoverSide - 0.5 * Diameter;
                    double deltay0y1 = Math.Abs(y0 - y1);
                    double deltay1y2 = Math.Abs(y1 - y2);
                    int ii = (int)((detatax5x3 + detatax3x2) / BarModel.Distance);
                    for (int i = 0; i < BarModel.Number; i++)
                    {
                        double dX = (bottom) ? (i * BarModel.Distance) : (dMainBottom + dSide + Diameter * 0.5 + i * BarModel.Distance);
                        double deltaX0 = x0 + ((x0 > 0) ? -1 : 1) * (CoverSide + dX);
                        double deltaX5 = x5 + ((x5 > 0) ? -1 : 1) * (CoverSide + dX);
                        double dY = (bottom) ? (0) : (dMainBottom + dSide + Diameter * 0.5);
                        double deltaY0 = y0 + ((y0 > 0) ? -1 : 1) * (CoverSide + dY);
                        double deltaY1 = y1;
                        double deltaY2 = y2;
                        double tanA = Math.Abs(y1 - y2) / Math.Abs(x1 - x2);

                        if (plus <= detatax5x3)
                        {

                            XYZ p1 = p0 + Unit.Convert(deltaX5) * x + Unit.Convert(deltaY0) * y + Unit.Convert(-deltaZ0) * XYZ.BasisZ;
                            XYZ p2 = p0 + Unit.Convert(deltaX5) * x + Unit.Convert(deltaY1 + ((deltaY1 >= 0) ? -1 : 1) * ((CoverSide - 0.5 * Diameter + i * BarModel.Distance) * tanA)) * y + Unit.Convert(-deltaZ0) * XYZ.BasisZ;
                            XYZ p1a = p0 + Unit.Convert(deltaX0) * x + Unit.Convert(deltaY0) * y + Unit.Convert(-deltaZ0) * XYZ.BasisZ;
                            XYZ p2a = p0 + Unit.Convert(deltaX0) * x + Unit.Convert(deltaY1 + ((deltaY1 >= 0) ? -1 : 1) * ((CoverSide - 0.5 * Diameter + i * BarModel.Distance) * tanA)) * y + Unit.Convert(-deltaZ0) * XYZ.BasisZ;
                            curves.Add(Line.CreateBound(p1, p2));
                            curves.Add(Line.CreateBound(p1a, p2a));
                        }
                        if (plus <= detatax5x3 + detatax3x2 && plus > detatax5x3)
                        {
                            XYZ p1 = p0 + Unit.Convert(deltaX5) * x + Unit.Convert(deltaY0) * y + Unit.Convert(-deltaZ0) * XYZ.BasisZ;
                            XYZ p2 = p0 + Unit.Convert(deltaX5) * x + Unit.Convert(deltaY2 + ((deltaY2 > 0) ? -1 : 1) * (CoverSide - 0.5 * Diameter)) * y + Unit.Convert(-deltaZ0) * XYZ.BasisZ;
                            curves.Add(Line.CreateBound(p1, p2));

                        }

                        plus += BarModel.Distance;
                    }
                }
                else
                {
                    double plus = 0;
                    double total = Math.Abs(y0 - y2) - 2 * CoverSide - Diameter;
                    double detatax1x2 = Math.Abs(x1 - x2);
                    double deltay0y1 = Math.Abs(y0 - y1) - CoverSide - 0.5 * Diameter;
                    double deltay1y2 = Math.Abs(y1 - y2) - CoverSide - 0.5 * Diameter;
                    for (int i = 0; i < BarModel.Number; i++)
                    {
                        double dX = (bottom) ? (0) : (dMainBottom + dSide + Diameter * 0.5);
                        double deltaX0 = x0 + ((x0 > 0) ? -1 : 1) * (CoverSide + dX);
                        double deltaX5 = x5 + ((x5 > 0) ? -1 : 1) * (CoverSide + dX);
                        double dY = (bottom) ? (i * BarModel.Distance) : (dMainBottom + dSide + Diameter * 0.5 + i * BarModel.Distance);
                        double deltaY0 = y0 + ((y0 > 0) ? -1 : 1) * (CoverSide + dY);
                        double deltaY5 = y5 + ((y5 > 0) ? -1 : 1) * (CoverSide + dY);

                        if (plus <= deltay0y1)
                        {
                            XYZ p1 = p0 + Unit.Convert(deltaX0) * x + Unit.Convert(deltaY0) * y + Unit.Convert(-deltaZ0) * XYZ.BasisZ;
                            XYZ p2 = p0 + Unit.Convert(deltaX5) * x + Unit.Convert(deltaY0) * y + Unit.Convert(-deltaZ0) * XYZ.BasisZ;
                            curves.Add(Line.CreateBound(p1, p2));
                        }
                        else
                        {
                            double detataY00 = Math.Abs(plus - deltay0y1);

                            double detataX00 = detataY00 * detatax1x2 / deltay1y2;

                            XYZ p1 = p0 + Unit.Convert(deltaX0 + ((deltaX0 > 0) ? (-detataX00) : (detataX00))) * x + Unit.Convert(deltaY0) * y + Unit.Convert(-deltaZ0) * XYZ.BasisZ;
                            XYZ p2 = p0 + Unit.Convert(deltaX5 + ((deltaX5 > 0) ? (-detataX00) : (detataX00))) * x + Unit.Convert(deltaY0) * y + Unit.Convert(-deltaZ0) * XYZ.BasisZ;
                            curves.Add(Line.CreateBound(p1, p2));
                        }
                        plus += BarModel.Distance;
                    }


                }
            }

            return curves;
        }
        public static ObservableCollection<Curve> GetCurvesSideItem0(SettingModel settingModel, FoundationModel FoundationModel, FoundationBarModel FoundationBarModel, BarModel BarModel, UnitProject Unit, double dMainBottom,  double CoverTop, double CoverBottom, double CoverSide)
        {
            double Diameter = BarModel.Bar.Diameter;
            XYZ x = null;
            XYZ y = null;
            XYZ p0 = FoundationModel.ColumnModel.PointPosition;
            ObservableCollection<Curve> curves = new ObservableCollection<Curve>();
            double delta = (settingModel.HeightFoundation - CoverBottom - CoverTop)/(BarModel.Number+1);
            double deltaZ0 = CoverTop + delta * BarModel.Number;
            double delta1 = CoverSide + dMainBottom + Diameter * 0.5;
            if (FoundationModel.ColumnModel.Style.Equals("RECTANGLE"))
            {
                x = FoundationModel.ColumnModel.East.FaceNormal;
                y = FoundationModel.ColumnModel.Nouth.FaceNormal;
            }
            else
            {
                x = XYZ.BasisX;
                y = XYZ.BasisY;
            }

            for (int i = 0; i < FoundationModel.BoundingLocation.Count; i++)
            {
                LocationModel l1,l2 = null;
                if (i == 0)
                {
                    l1 = FoundationModel.BoundingLocation[FoundationModel.BoundingLocation.Count - 1];
                    l2 = FoundationModel.BoundingLocation[0];
                }
                else
                {
                    l1 = FoundationModel.BoundingLocation[i-1];
                    l2 = FoundationModel.BoundingLocation[i];
                }
                XYZ p1 = p0 + Unit.Convert(l1.X+((l1.X>0) ?(-1):(1))*(delta1)) * x + Unit.Convert(l1.Y + ((l1.Y > 0) ? (-1) : (1)) * (delta1)) * y + Unit.Convert(-deltaZ0) * XYZ.BasisZ;

                XYZ p2 = p0 + Unit.Convert(l2.X + ((l2.X > 0) ? (-1) : (1)) * (delta1)) * x + Unit.Convert(l2.Y + ((l2.Y > 0) ? (-1) : (1)) * (delta1)) * y + Unit.Convert(-deltaZ0) * XYZ.BasisZ;
                curves.Add(Line.CreateBound(p1, p2));
            }
           
            return curves;
        }
        public static ObservableCollection<Curve> GetCurvesImage0(SettingModel settingModel, FoundationModel FoundationModel, FoundationBarModel FoundationBarModel, BarModel BarModel, UnitProject Unit,  double dMainBottom, double dMainTop, double dSide, double CoverTop, double CoverBottom, double CoverSide)
        {
            ObservableCollection<Curve> curves = new ObservableCollection<Curve>();
            if (BarModel.IsModel)
            {
                switch (BarModel.Name)
                {
                    case "MainBottom":
                        curves = GetCurvesMainItem0(settingModel, FoundationModel, FoundationBarModel, BarModel, Unit, true, dMainBottom, dSide, CoverTop, CoverBottom, CoverSide);
                        break;
                    case "MainTop":
                        curves = GetCurvesMainItem0(settingModel, FoundationModel, FoundationBarModel, BarModel, Unit, false, dMainBottom, dSide, CoverTop, CoverBottom, CoverSide);
                        break;
                    case "MainAddHorizontal": break;
                    case "MainAddVertical": break;
                    case "SecondaryBottom":
                        curves = GetCurvesSecondaryItem0(settingModel, FoundationModel, FoundationBarModel, BarModel, Unit, true, dMainBottom, dMainTop, dSide, CoverTop, CoverBottom, CoverSide);
                        break;
                    case "SecondaryTop":
                        curves = GetCurvesSecondaryItem0(settingModel, FoundationModel, FoundationBarModel, BarModel, Unit, false, dMainBottom, dMainTop, dSide, CoverTop, CoverBottom, CoverSide);
                        break;
                    case "SecondaryAddHorizontal": break;
                    case "SecondaryAddVertical": break;
                    case "Side":
                        curves = GetCurvesSideItem0(settingModel, FoundationModel, FoundationBarModel, BarModel, Unit, dMainBottom,CoverTop, CoverBottom, CoverSide);
                        break;
                    default: break;
                }
            }
            return curves;
        }
        #endregion
    }
}
