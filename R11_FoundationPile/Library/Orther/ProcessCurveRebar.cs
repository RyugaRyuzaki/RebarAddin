using Autodesk.Revit.DB;
using DSP;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace R11_FoundationPile
{
    public class ProcessCurveRebar
    {
        #region Image0
        //private static ObservableCollection<Curve> GetCurvesMainItem0(SettingModel settingModel, FoundationModel FoundationModel, FoundationBarModel FoundationBarModel, BarModel BarModel, UnitProject Unit, bool bottom, bool add, double dMainBottom, double dSide, double CoverTop, double CoverBottom, double CoverSide)
        //{

        //    double Diameter = BarModel.Bar.Diameter;
        //    XYZ x = null;
        //    XYZ y = null;
        //    XYZ p0 = FoundationModel.ColumnModel.PointPosition;
        //    ObservableCollection<Curve> curves = new ObservableCollection<Curve>();
        //    bool Horizontal = (FoundationBarModel.SpanOrientation.Equals("Horizontal"));
        //    double x0 = FoundationModel.BoundingLocation[0].X;
        //    double x1 = FoundationModel.BoundingLocation[1].X;
        //    double x2 = FoundationModel.BoundingLocation[2].X;
        //    double x3 = FoundationModel.BoundingLocation[3].X;
        //    double x4 = FoundationModel.BoundingLocation[4].X;
        //    double x5 = FoundationModel.BoundingLocation[5].X;
        //    double y0 = FoundationModel.BoundingLocation[0].Y;
        //    double y1 = FoundationModel.BoundingLocation[1].Y;
        //    double y2 = FoundationModel.BoundingLocation[2].Y;
        //    double y3 = FoundationModel.BoundingLocation[3].Y;
        //    double y4 = FoundationModel.BoundingLocation[4].Y;
        //    double y5 = FoundationModel.BoundingLocation[5].Y;
        //    double deltaZ0 = (bottom) ? (settingModel.HeightFoundation - CoverBottom) : (CoverTop);
        //    if (add)
        //    {
        //        double delta = (settingModel.HeightFoundation - CoverBottom - CoverTop) / (BarModel.Layer + 1);
        //        deltaZ0 = CoverTop + delta * BarModel.Layer;
        //    }
        //    if (FoundationModel.ColumnModel.Style.Equals("RECTANGLE"))
        //    {
        //        x = FoundationModel.ColumnModel.East.FaceNormal;
        //        y = FoundationModel.ColumnModel.Nouth.FaceNormal;
        //    }
        //    else
        //    {
        //        x = XYZ.BasisX;
        //        y = XYZ.BasisY;
        //    }

        //    if (FoundationModel.ColumnModel.Style.Equals("RECTANGLE") && FoundationModel.ColumnModel.b > FoundationModel.ColumnModel.h)
        //    {
        //        if (Horizontal)
        //        {
        //            double plus = 0;
        //            double total = Math.Abs(y0 - y5) - 2 * CoverSide - Diameter;
        //            double detatay5y3 = Math.Abs(y5 - y3) - CoverSide - 0.5 * Diameter;
        //            double detatay3y2 = Math.Abs(y3 - y2);
        //            double detatay2y0 = Math.Abs(y2 - y0) - CoverSide - 0.5 * Diameter;

        //            double deltax0x1 = Math.Abs(x0 - x1);
        //            double deltax1x2 = Math.Abs(x1 - x2);


        //            int ii = (int)((detatay5y3 + detatay3y2) / BarModel.Distance);
        //            for (int i = 0; i < BarModel.Number; i++)
        //            {
        //                double dY = (bottom) ? (i * BarModel.Distance) : (dMainBottom + dSide + Diameter * 0.5 + i * BarModel.Distance);
        //                double deltaY0 = y0 + ((y0 > 0) ? -1 : 1) * (CoverSide + dY);
        //                double deltaY5 = y5 + ((y5 > 0) ? -1 : 1) * (CoverSide + dY);
        //                double dX = (bottom) ? (0) : (dMainBottom + dSide + Diameter * 0.5);
        //                double deltaX0 = x0 + ((x0 > 0) ? -1 : 1) * (CoverSide + dX);
        //                double deltaX1 = x1;
        //                double deltaX2 = x2;
        //                double tanA = Math.Abs(x1 - x2) / Math.Abs(y1 - y2);

        //                if (plus <= detatay5y3)
        //                {
        //                    XYZ p1 = p0 + Unit.Convert(deltaX0) * x + Unit.Convert(deltaY0) * y + Unit.Convert(-deltaZ0) * XYZ.BasisZ;
        //                    XYZ p2 = p0 + Unit.Convert(deltaX1 + ((deltaX1 >= 0) ? 1 : -1) * ((CoverSide - 0.5 * Diameter + i * BarModel.Distance) * tanA)) * x + Unit.Convert(deltaY0) * y + Unit.Convert(-deltaZ0) * XYZ.BasisZ;
        //                    XYZ p1a = p0 + Unit.Convert(deltaX0) * x + Unit.Convert(deltaY5) * y + Unit.Convert(-deltaZ0) * XYZ.BasisZ;
        //                    XYZ p2a = p0 + Unit.Convert(deltaX1 + ((deltaX1 >= 0) ? 1 : -1) * ((CoverSide - 0.5 * Diameter + i * BarModel.Distance) * tanA)) * x + Unit.Convert(deltaY5) * y + Unit.Convert(-deltaZ0) * XYZ.BasisZ;
        //                    curves.Add(Line.CreateBound(p1, p2));
        //                    curves.Add(Line.CreateBound(p1a, p2a));
        //                }
        //                if (plus <= detatay5y3 + detatay3y2 && plus > detatay5y3)
        //                {
        //                    XYZ p1 = p0 + Unit.Convert(deltaX0) * x + Unit.Convert(deltaY0) * y + Unit.Convert(-deltaZ0) * XYZ.BasisZ;
        //                    XYZ p2 = p0 + Unit.Convert(deltaX2 + ((deltaX2 > 0) ? -1 : 1) * (CoverSide - 0.5 * Diameter)) * x + Unit.Convert(deltaY0) * y + Unit.Convert(-deltaZ0) * XYZ.BasisZ;
        //                    curves.Add(Line.CreateBound(p1, p2));

        //                }

        //                plus += BarModel.Distance;
        //            }
        //        }
        //        else
        //        {
        //            double plus = 0;
        //            double total = Math.Abs(x0 - x2) - 2 * CoverSide - Diameter;
        //            double detatay1y2 = Math.Abs(y1 - y2);
        //            double deltax0x1 = Math.Abs(x0 - x1) - CoverSide - 0.5 * Diameter;
        //            double deltax1x2 = Math.Abs(x1 - x2) - CoverSide - 0.5 * Diameter;
        //            for (int i = 0; i < BarModel.Number; i++)
        //            {
        //                double dY = (bottom) ? (0) : (dMainBottom + dSide + Diameter * 0.5);
        //                double deltaY0 = y0 + ((y0 > 0) ? -1 : 1) * (CoverSide + dY);
        //                double deltaY5 = y5 + ((y5 > 0) ? -1 : 1) * (CoverSide + dY);
        //                double dX = (bottom) ? (i * BarModel.Distance) : (dMainBottom + dSide + Diameter * 0.5 + i * BarModel.Distance);
        //                double deltaX0 = x0 + ((x0 > 0) ? -1 : 1) * (CoverSide + dX);
        //                double deltaX5 = x5 + ((x5 > 0) ? -1 : 1) * (CoverSide + dX);

        //                if (plus <= deltax0x1)
        //                {
        //                    XYZ p1 = p0 + Unit.Convert(deltaX0) * x + Unit.Convert(deltaY0) * y + Unit.Convert(-deltaZ0) * XYZ.BasisZ;
        //                    XYZ p2 = p0 + Unit.Convert(deltaX5) * x + Unit.Convert(deltaY5) * y + Unit.Convert(-deltaZ0) * XYZ.BasisZ;
        //                    curves.Add(Line.CreateBound(p1, p2));
        //                }
        //                else
        //                {
        //                    double detataX00 = Math.Abs(plus - deltax0x1);

        //                    double detataY00 = detataX00 * detatay1y2 / deltax1x2;

        //                    XYZ p1 = p0 + Unit.Convert(deltaX0) * x + Unit.Convert(deltaY0 + ((deltaY0 > 0) ? (-detataY00) : (detataY00))) * y + Unit.Convert(-deltaZ0) * XYZ.BasisZ;
        //                    XYZ p2 = p0 + Unit.Convert(deltaX0) * x + Unit.Convert(deltaY5 + ((deltaY5 > 0) ? (-detataY00) : (detataY00))) * y + Unit.Convert(-deltaZ0) * XYZ.BasisZ;
        //                    curves.Add(Line.CreateBound(p1, p2));
        //                }
        //                plus += BarModel.Distance;
        //            }

        //        }
        //    }
        //    else
        //    {
        //        if (Horizontal)
        //        {
        //            double plus = 0;
        //            double total = Math.Abs(y0 - y2) - 2 * CoverSide - Diameter;
        //            double detatax1x2 = Math.Abs(x1 - x2);
        //            double deltay0y1 = Math.Abs(y0 - y1) - CoverSide - 0.5 * Diameter;
        //            double deltay1y2 = Math.Abs(y1 - y2) - CoverSide - 0.5 * Diameter;
        //            for (int i = 0; i < BarModel.Number; i++)
        //            {
        //                double dX = (bottom) ? (0) : (dMainBottom + dSide + Diameter * 0.5);
        //                double deltaX0 = x0 + ((x0 > 0) ? -1 : 1) * (CoverSide + dX);
        //                double deltaX5 = x5 + ((x5 > 0) ? -1 : 1) * (CoverSide + dX);
        //                double dY = (bottom) ? (i * BarModel.Distance) : (dMainBottom + dSide + Diameter * 0.5 + i * BarModel.Distance);
        //                double deltaY0 = y0 + ((y0 > 0) ? -1 : 1) * (CoverSide + dY);
        //                double deltaY5 = y5 + ((y5 > 0) ? -1 : 1) * (CoverSide + dY);

        //                if (plus <= deltay0y1)
        //                {
        //                    XYZ p1 = p0 + Unit.Convert(deltaX0) * x + Unit.Convert(deltaY0) * y + Unit.Convert(-deltaZ0) * XYZ.BasisZ;
        //                    XYZ p2 = p0 + Unit.Convert(deltaX5) * x + Unit.Convert(deltaY0) * y + Unit.Convert(-deltaZ0) * XYZ.BasisZ;
        //                    curves.Add(Line.CreateBound(p1, p2));
        //                }
        //                else
        //                {
        //                    double detataY00 = Math.Abs(plus - deltay0y1);

        //                    double detataX00 = detataY00 * detatax1x2 / deltay1y2;

        //                    XYZ p1 = p0 + Unit.Convert(deltaX0 + ((deltaX0 > 0) ? (-detataX00) : (detataX00))) * x + Unit.Convert(deltaY0) * y + Unit.Convert(-deltaZ0) * XYZ.BasisZ;
        //                    XYZ p2 = p0 + Unit.Convert(deltaX5 + ((deltaX5 > 0) ? (-detataX00) : (detataX00))) * x + Unit.Convert(deltaY0) * y + Unit.Convert(-deltaZ0) * XYZ.BasisZ;
        //                    curves.Add(Line.CreateBound(p1, p2));
        //                }
        //                plus += BarModel.Distance;
        //            }
        //        }
        //        else
        //        {

        //            double plus = 0;
        //            double total = Math.Abs(x0 - x5) - 2 * CoverSide - Diameter;
        //            double detatax5x3 = Math.Abs(x5 - x3) - CoverSide - 0.5 * Diameter;
        //            double detatax3x2 = Math.Abs(x3 - x2);
        //            double detatax2x0 = Math.Abs(x2 - x0) - CoverSide - 0.5 * Diameter;
        //            double deltay0y1 = Math.Abs(y0 - y1);
        //            double deltay1y2 = Math.Abs(y1 - y2);
        //            int ii = (int)((detatax5x3 + detatax3x2) / BarModel.Distance);
        //            for (int i = 0; i < BarModel.Number; i++)
        //            {
        //                double dX = (bottom) ? (i * BarModel.Distance) : (dMainBottom + dSide + Diameter * 0.5 + i * BarModel.Distance);
        //                double deltaX0 = x0 + ((x0 > 0) ? -1 : 1) * (CoverSide + dX);
        //                double deltaX5 = x5 + ((x5 > 0) ? -1 : 1) * (CoverSide + dX);
        //                double dY = (bottom) ? (0) : (dMainBottom + dSide + Diameter * 0.5);
        //                double deltaY0 = y0 + ((y0 > 0) ? -1 : 1) * (CoverSide + dY);
        //                double deltaY1 = y1;
        //                double deltaY2 = y2;
        //                double tanA = Math.Abs(y1 - y2) / Math.Abs(x1 - x2);

        //                if (plus <= detatax5x3)
        //                {

        //                    XYZ p1 = p0 + Unit.Convert(deltaX5) * x + Unit.Convert(deltaY0) * y + Unit.Convert(-deltaZ0) * XYZ.BasisZ;
        //                    XYZ p2 = p0 + Unit.Convert(deltaX5) * x + Unit.Convert(deltaY1 + ((deltaY1 >= 0) ? -1 : 1) * ((CoverSide - 0.5 * Diameter + i * BarModel.Distance) * tanA)) * y + Unit.Convert(-deltaZ0) * XYZ.BasisZ;
        //                    XYZ p1a = p0 + Unit.Convert(deltaX0) * x + Unit.Convert(deltaY0) * y + Unit.Convert(-deltaZ0) * XYZ.BasisZ;
        //                    XYZ p2a = p0 + Unit.Convert(deltaX0) * x + Unit.Convert(deltaY1 + ((deltaY1 >= 0) ? -1 : 1) * ((CoverSide - 0.5 * Diameter + i * BarModel.Distance) * tanA)) * y + Unit.Convert(-deltaZ0) * XYZ.BasisZ;
        //                    curves.Add(Line.CreateBound(p1, p2));
        //                    curves.Add(Line.CreateBound(p1a, p2a));
        //                }
        //                if (plus <= detatax5x3 + detatax3x2 && plus > detatax5x3)
        //                {
        //                    XYZ p1 = p0 + Unit.Convert(deltaX5) * x + Unit.Convert(deltaY0) * y + Unit.Convert(-deltaZ0) * XYZ.BasisZ;
        //                    XYZ p2 = p0 + Unit.Convert(deltaX5) * x + Unit.Convert(deltaY2 + ((deltaY2 > 0) ? -1 : 1) * (CoverSide - 0.5 * Diameter)) * y + Unit.Convert(-deltaZ0) * XYZ.BasisZ;
        //                    curves.Add(Line.CreateBound(p1, p2));

        //                }

        //                plus += BarModel.Distance;
        //            }
        //        }
        //    }

        //    return curves;
        //}
        //private static ObservableCollection<Curve> GetCurvesSecondaryItem0(SettingModel settingModel, FoundationModel FoundationModel, FoundationBarModel FoundationBarModel, BarModel BarModel, UnitProject Unit, bool bottom, bool add, double dMainBottom, double dMainTop, double dSide, double CoverTop, double CoverBottom, double CoverSide)
        //{

        //    double Diameter = BarModel.Bar.Diameter;
        //    XYZ x = null;
        //    XYZ y = null;
        //    XYZ p0 = FoundationModel.ColumnModel.PointPosition;
        //    ObservableCollection<Curve> curves = new ObservableCollection<Curve>();
        //    bool Horizontal = (FoundationBarModel.SpanOrientation.Equals("Horizontal"));
        //    double x0 = FoundationModel.BoundingLocation[0].X;
        //    double x1 = FoundationModel.BoundingLocation[1].X;
        //    double x2 = FoundationModel.BoundingLocation[2].X;
        //    double x3 = FoundationModel.BoundingLocation[3].X;
        //    double x4 = FoundationModel.BoundingLocation[4].X;
        //    double x5 = FoundationModel.BoundingLocation[5].X;
        //    double y0 = FoundationModel.BoundingLocation[0].Y;
        //    double y1 = FoundationModel.BoundingLocation[1].Y;
        //    double y2 = FoundationModel.BoundingLocation[2].Y;
        //    double y3 = FoundationModel.BoundingLocation[3].Y;
        //    double y4 = FoundationModel.BoundingLocation[4].Y;
        //    double y5 = FoundationModel.BoundingLocation[5].Y;
        //    double deltaZ0 = (bottom) ? (settingModel.HeightFoundation - CoverBottom - dMainBottom) : (CoverTop + dMainTop);
        //    if (add)
        //    {
        //        double delta = (settingModel.HeightFoundation - CoverBottom - CoverTop) / (BarModel.Layer + 1);
        //        deltaZ0 = CoverTop + delta * BarModel.Layer;
        //    }
        //    if (FoundationModel.ColumnModel.Style.Equals("RECTANGLE"))
        //    {
        //        x = FoundationModel.ColumnModel.East.FaceNormal;
        //        y = FoundationModel.ColumnModel.Nouth.FaceNormal;
        //    }
        //    else
        //    {
        //        x = XYZ.BasisX;
        //        y = XYZ.BasisY;
        //    }

        //    if (FoundationModel.ColumnModel.Style.Equals("RECTANGLE") && FoundationModel.ColumnModel.b > FoundationModel.ColumnModel.h)
        //    {
        //        if (Horizontal)
        //        {


        //            double plus = 0;
        //            double total = Math.Abs(x0 - x2) - 2 * CoverSide - Diameter;
        //            double detatay1y2 = Math.Abs(y1 - y2);
        //            double deltax0x1 = Math.Abs(x0 - x1) - CoverSide - 0.5 * Diameter;
        //            double deltax1x2 = Math.Abs(x1 - x2) - CoverSide - 0.5 * Diameter;
        //            for (int i = 0; i < BarModel.Number; i++)
        //            {
        //                double dY = (bottom) ? (0) : (dMainBottom + dSide + Diameter * 0.5);
        //                double deltaY0 = y0 + ((y0 > 0) ? -1 : 1) * (CoverSide + dY);
        //                double deltaY5 = y5 + ((y5 > 0) ? -1 : 1) * (CoverSide + dY);
        //                double dX = (bottom) ? (i * BarModel.Distance) : (dMainBottom + dSide + Diameter * 0.5 + i * BarModel.Distance);
        //                double deltaX0 = x0 + ((x0 > 0) ? -1 : 1) * (CoverSide + dX);
        //                double deltaX5 = x5 + ((x5 > 0) ? -1 : 1) * (CoverSide + dX);

        //                if (plus <= deltax0x1)
        //                {
        //                    XYZ p1 = p0 + Unit.Convert(deltaX0) * x + Unit.Convert(deltaY0) * y + Unit.Convert(-deltaZ0) * XYZ.BasisZ;
        //                    XYZ p2 = p0 + Unit.Convert(deltaX5) * x + Unit.Convert(deltaY5) * y + Unit.Convert(-deltaZ0) * XYZ.BasisZ;
        //                    curves.Add(Line.CreateBound(p1, p2));
        //                }
        //                else
        //                {
        //                    double detataX00 = Math.Abs(plus - deltax0x1);

        //                    double detataY00 = detataX00 * detatay1y2 / deltax1x2;

        //                    XYZ p1 = p0 + Unit.Convert(deltaX0) * x + Unit.Convert(deltaY0 + ((deltaY0 > 0) ? (-detataY00) : (detataY00))) * y + Unit.Convert(-deltaZ0) * XYZ.BasisZ;
        //                    XYZ p2 = p0 + Unit.Convert(deltaX0) * x + Unit.Convert(deltaY5 + ((deltaY5 > 0) ? (-detataY00) : (detataY00))) * y + Unit.Convert(-deltaZ0) * XYZ.BasisZ;
        //                    curves.Add(Line.CreateBound(p1, p2));
        //                }
        //                plus += BarModel.Distance;
        //            }

        //        }
        //        else
        //        {
        //            double plus = 0;
        //            double total = Math.Abs(y0 - y5) - 2 * CoverSide - Diameter;
        //            double detatay5y3 = Math.Abs(y5 - y3) - CoverSide - 0.5 * Diameter;
        //            double detatay3y2 = Math.Abs(y3 - y2);
        //            double detatay2y0 = Math.Abs(y2 - y0) - CoverSide - 0.5 * Diameter;

        //            double deltax0x1 = Math.Abs(x0 - x1);
        //            double deltax1x2 = Math.Abs(x1 - x2);


        //            int ii = (int)((detatay5y3 + detatay3y2) / BarModel.Distance);
        //            for (int i = 0; i < BarModel.Number; i++)
        //            {
        //                double dY = (bottom) ? (i * BarModel.Distance) : (dMainBottom + dSide + Diameter * 0.5 + i * BarModel.Distance);
        //                double deltaY0 = y0 + ((y0 > 0) ? -1 : 1) * (CoverSide + dY);
        //                double deltaY5 = y5 + ((y5 > 0) ? -1 : 1) * (CoverSide + dY);
        //                double dX = (bottom) ? (0) : (dMainBottom + dSide + Diameter * 0.5);
        //                double deltaX0 = x0 + ((x0 > 0) ? -1 : 1) * (CoverSide + dX);
        //                double deltaX1 = x1;
        //                double deltaX2 = x2;
        //                double tanA = Math.Abs(x1 - x2) / Math.Abs(y1 - y2);

        //                if (plus <= detatay5y3)
        //                {
        //                    XYZ p1 = p0 + Unit.Convert(deltaX0) * x + Unit.Convert(deltaY0) * y + Unit.Convert(-deltaZ0) * XYZ.BasisZ;
        //                    XYZ p2 = p0 + Unit.Convert(deltaX1 + ((deltaX1 >= 0) ? 1 : -1) * ((CoverSide - 0.5 * Diameter + i * BarModel.Distance) * tanA)) * x + Unit.Convert(deltaY0) * y + Unit.Convert(-deltaZ0) * XYZ.BasisZ;
        //                    XYZ p1a = p0 + Unit.Convert(deltaX0) * x + Unit.Convert(deltaY5) * y + Unit.Convert(-deltaZ0) * XYZ.BasisZ;
        //                    XYZ p2a = p0 + Unit.Convert(deltaX1 + ((deltaX1 >= 0) ? 1 : -1) * ((CoverSide - 0.5 * Diameter + i * BarModel.Distance) * tanA)) * x + Unit.Convert(deltaY5) * y + Unit.Convert(-deltaZ0) * XYZ.BasisZ;
        //                    curves.Add(Line.CreateBound(p1, p2));
        //                    curves.Add(Line.CreateBound(p1a, p2a));
        //                }
        //                if (plus <= detatay5y3 + detatay3y2 && plus > detatay5y3)
        //                {
        //                    XYZ p1 = p0 + Unit.Convert(deltaX0) * x + Unit.Convert(deltaY0) * y + Unit.Convert(-deltaZ0) * XYZ.BasisZ;
        //                    XYZ p2 = p0 + Unit.Convert(deltaX2 + ((deltaX2 > 0) ? -1 : 1) * (CoverSide - 0.5 * Diameter)) * x + Unit.Convert(deltaY0) * y + Unit.Convert(-deltaZ0) * XYZ.BasisZ;
        //                    curves.Add(Line.CreateBound(p1, p2));

        //                }

        //                plus += BarModel.Distance;
        //            }
        //        }
        //    }
        //    else
        //    {
        //        if (Horizontal)
        //        {
        //            double plus = 0;
        //            double total = Math.Abs(x0 - x5) - 2 * CoverSide - Diameter;
        //            double detatax5x3 = Math.Abs(x5 - x3) - CoverSide - 0.5 * Diameter;
        //            double detatax3x2 = Math.Abs(x3 - x2);
        //            double detatax2x0 = Math.Abs(x2 - x0) - CoverSide - 0.5 * Diameter;
        //            double deltay0y1 = Math.Abs(y0 - y1);
        //            double deltay1y2 = Math.Abs(y1 - y2);
        //            int ii = (int)((detatax5x3 + detatax3x2) / BarModel.Distance);
        //            for (int i = 0; i < BarModel.Number; i++)
        //            {
        //                double dX = (bottom) ? (i * BarModel.Distance) : (dMainBottom + dSide + Diameter * 0.5 + i * BarModel.Distance);
        //                double deltaX0 = x0 + ((x0 > 0) ? -1 : 1) * (CoverSide + dX);
        //                double deltaX5 = x5 + ((x5 > 0) ? -1 : 1) * (CoverSide + dX);
        //                double dY = (bottom) ? (0) : (dMainBottom + dSide + Diameter * 0.5);
        //                double deltaY0 = y0 + ((y0 > 0) ? -1 : 1) * (CoverSide + dY);
        //                double deltaY1 = y1;
        //                double deltaY2 = y2;
        //                double tanA = Math.Abs(y1 - y2) / Math.Abs(x1 - x2);

        //                if (plus <= detatax5x3)
        //                {

        //                    XYZ p1 = p0 + Unit.Convert(deltaX5) * x + Unit.Convert(deltaY0) * y + Unit.Convert(-deltaZ0) * XYZ.BasisZ;
        //                    XYZ p2 = p0 + Unit.Convert(deltaX5) * x + Unit.Convert(deltaY1 + ((deltaY1 >= 0) ? -1 : 1) * ((CoverSide - 0.5 * Diameter + i * BarModel.Distance) * tanA)) * y + Unit.Convert(-deltaZ0) * XYZ.BasisZ;
        //                    XYZ p1a = p0 + Unit.Convert(deltaX0) * x + Unit.Convert(deltaY0) * y + Unit.Convert(-deltaZ0) * XYZ.BasisZ;
        //                    XYZ p2a = p0 + Unit.Convert(deltaX0) * x + Unit.Convert(deltaY1 + ((deltaY1 >= 0) ? -1 : 1) * ((CoverSide - 0.5 * Diameter + i * BarModel.Distance) * tanA)) * y + Unit.Convert(-deltaZ0) * XYZ.BasisZ;
        //                    curves.Add(Line.CreateBound(p1, p2));
        //                    curves.Add(Line.CreateBound(p1a, p2a));
        //                }
        //                if (plus <= detatax5x3 + detatax3x2 && plus > detatax5x3)
        //                {
        //                    XYZ p1 = p0 + Unit.Convert(deltaX5) * x + Unit.Convert(deltaY0) * y + Unit.Convert(-deltaZ0) * XYZ.BasisZ;
        //                    XYZ p2 = p0 + Unit.Convert(deltaX5) * x + Unit.Convert(deltaY2 + ((deltaY2 > 0) ? -1 : 1) * (CoverSide - 0.5 * Diameter)) * y + Unit.Convert(-deltaZ0) * XYZ.BasisZ;
        //                    curves.Add(Line.CreateBound(p1, p2));

        //                }

        //                plus += BarModel.Distance;
        //            }
        //        }
        //        else
        //        {
        //            double plus = 0;
        //            double total = Math.Abs(y0 - y2) - 2 * CoverSide - Diameter;
        //            double detatax1x2 = Math.Abs(x1 - x2);
        //            double deltay0y1 = Math.Abs(y0 - y1) - CoverSide - 0.5 * Diameter;
        //            double deltay1y2 = Math.Abs(y1 - y2) - CoverSide - 0.5 * Diameter;
        //            for (int i = 0; i < BarModel.Number; i++)
        //            {
        //                double dX = (bottom) ? (0) : (dMainBottom + dSide + Diameter * 0.5);
        //                double deltaX0 = x0 + ((x0 > 0) ? -1 : 1) * (CoverSide + dX);
        //                double deltaX5 = x5 + ((x5 > 0) ? -1 : 1) * (CoverSide + dX);
        //                double dY = (bottom) ? (i * BarModel.Distance) : (dMainBottom + dSide + Diameter * 0.5 + i * BarModel.Distance);
        //                double deltaY0 = y0 + ((y0 > 0) ? -1 : 1) * (CoverSide + dY);
        //                double deltaY5 = y5 + ((y5 > 0) ? -1 : 1) * (CoverSide + dY);

        //                if (plus <= deltay0y1)
        //                {
        //                    XYZ p1 = p0 + Unit.Convert(deltaX0) * x + Unit.Convert(deltaY0) * y + Unit.Convert(-deltaZ0) * XYZ.BasisZ;
        //                    XYZ p2 = p0 + Unit.Convert(deltaX5) * x + Unit.Convert(deltaY0) * y + Unit.Convert(-deltaZ0) * XYZ.BasisZ;
        //                    curves.Add(Line.CreateBound(p1, p2));
        //                }
        //                else
        //                {
        //                    double detataY00 = Math.Abs(plus - deltay0y1);

        //                    double detataX00 = detataY00 * detatax1x2 / deltay1y2;

        //                    XYZ p1 = p0 + Unit.Convert(deltaX0 + ((deltaX0 > 0) ? (-detataX00) : (detataX00))) * x + Unit.Convert(deltaY0) * y + Unit.Convert(-deltaZ0) * XYZ.BasisZ;
        //                    XYZ p2 = p0 + Unit.Convert(deltaX5 + ((deltaX5 > 0) ? (-detataX00) : (detataX00))) * x + Unit.Convert(deltaY0) * y + Unit.Convert(-deltaZ0) * XYZ.BasisZ;
        //                    curves.Add(Line.CreateBound(p1, p2));
        //                }
        //                plus += BarModel.Distance;
        //            }


        //        }
        //    }

        //    return curves;
        //}
        //private static ObservableCollection<Curve> GetCurvesMainAddItem0(SettingModel settingModel, FoundationModel FoundationModel, FoundationBarModel FoundationBarModel, BarModel BarModel, UnitProject Unit, bool bottom, double dMainBottom, double dSide, double CoverTop, double CoverBottom, double CoverSide, out List<double> Distance)
        //{
        //    Distance = new List<double>();
        //    List<XYZ> ListP0 = new List<XYZ>();
        //    double Diameter = BarModel.Bar.Diameter;
        //    XYZ x = null;
        //    XYZ y = null;
        //    XYZ p0 = FoundationModel.ColumnModel.PointPosition;
        //    ObservableCollection<Curve> curves = new ObservableCollection<Curve>();
        //    bool Horizontal = (FoundationBarModel.SpanOrientation.Equals("Horizontal"));
        //    double x0 = FoundationModel.BoundingLocation[0].X;
        //    double x1 = FoundationModel.BoundingLocation[1].X;
        //    double x2 = FoundationModel.BoundingLocation[2].X;
        //    double x3 = FoundationModel.BoundingLocation[3].X;
        //    double x4 = FoundationModel.BoundingLocation[4].X;
        //    double x5 = FoundationModel.BoundingLocation[5].X;
        //    double y0 = FoundationModel.BoundingLocation[0].Y;
        //    double y1 = FoundationModel.BoundingLocation[1].Y;
        //    double y2 = FoundationModel.BoundingLocation[2].Y;
        //    double y3 = FoundationModel.BoundingLocation[3].Y;
        //    double y4 = FoundationModel.BoundingLocation[4].Y;
        //    double y5 = FoundationModel.BoundingLocation[5].Y;

        //    if (FoundationModel.ColumnModel.Style.Equals("RECTANGLE"))
        //    {
        //        x = FoundationModel.ColumnModel.East.FaceNormal;
        //        y = FoundationModel.ColumnModel.Nouth.FaceNormal;
        //    }
        //    else
        //    {
        //        x = XYZ.BasisX;
        //        y = XYZ.BasisY;
        //    }

        //    if (FoundationModel.ColumnModel.Style.Equals("RECTANGLE") && FoundationModel.ColumnModel.b > FoundationModel.ColumnModel.h)
        //    {
        //        if (Horizontal)
        //        {
        //            double plus = 0;
        //            double total = Math.Abs(y0 - y5) - 2 * CoverSide - Diameter;
        //            double detatay5y3 = Math.Abs(y5 - y3) - CoverSide - 0.5 * Diameter;
        //            double detatay3y2 = Math.Abs(y3 - y2);
        //            double detatay2y0 = Math.Abs(y2 - y0) - CoverSide - 0.5 * Diameter;

        //            double deltax0x1 = Math.Abs(x0 - x1);
        //            double deltax1x2 = Math.Abs(x1 - x2);

        //            int number = (int)(total / BarModel.Distance) + 1;
        //            int ii = (int)((detatay5y3 + detatay3y2) / BarModel.Distance);
        //            for (int i = 0; i < number; i++)
        //            {
        //                double dY = (bottom) ? (i * BarModel.Distance) : (dMainBottom + dSide + Diameter * 0.5 + i * BarModel.Distance);
        //                double deltaY0 = y0 + ((y0 > 0) ? -1 : 1) * (CoverSide + dY);
        //                double deltaY5 = y5 + ((y5 > 0) ? -1 : 1) * (CoverSide + dY);
        //                double dX = (bottom) ? (0) : (dMainBottom + dSide + Diameter * 0.5);
        //                double deltaX0 = x0 + ((x0 > 0) ? -1 : 1) * (CoverSide + dX);
        //                double deltaX1 = x1;
        //                double deltaX2 = x2;
        //                double tanA = Math.Abs(x1 - x2) / Math.Abs(y1 - y2);

        //                if (plus <= detatay5y3)
        //                {
        //                    ListP0.Add(p0 + Unit.Convert(deltaX0) * x + Unit.Convert(deltaY0) * y);
        //                    ListP0.Add(p0 + Unit.Convert(deltaX0) * x + Unit.Convert(deltaY5) * y);
        //                    Distance.Add(((Math.Abs(deltaX0 - (deltaX1 + ((deltaX1 >= 0) ? 1 : -1) * ((CoverSide - 0.5 * Diameter + i * BarModel.Distance) * tanA)))) / (BarModel.Layer + 1)));
        //                    Distance.Add(((Math.Abs((deltaX0) - (deltaX1 + ((deltaX1 >= 0) ? 1 : -1) * ((CoverSide - 0.5 * Diameter + i * BarModel.Distance) * tanA)))) / (BarModel.Layer + 1)));
        //                }
        //                if (plus <= detatay5y3 + detatay3y2 && plus > detatay5y3)
        //                {
        //                    ListP0.Add(p0 + Unit.Convert(deltaX0) * x + Unit.Convert(deltaY0) * y);
        //                    Distance.Add(((Math.Abs((deltaX0) - (deltaX2 + ((deltaX2 > 0) ? -1 : 1) * (CoverSide - 0.5 * Diameter)))) / (BarModel.Layer + 1)));
        //                }

        //                plus += BarModel.Distance;
        //            }
        //        }
        //        else
        //        {
        //            double plus = 0;
        //            double total = Math.Abs(x0 - x2) - 2 * CoverSide - Diameter;
        //            double detatay1y2 = Math.Abs(y1 - y2);
        //            double deltax0x1 = Math.Abs(x0 - x1) - CoverSide - 0.5 * Diameter;
        //            double deltax1x2 = Math.Abs(x1 - x2) - CoverSide - 0.5 * Diameter;
        //            int number = (int)(total / BarModel.Distance) + 1;
        //            for (int i = 0; i < number; i++)
        //            {
        //                double dY = (bottom) ? (0) : (dMainBottom + dSide + Diameter * 0.5);
        //                double deltaY0 = y0 + ((y0 > 0) ? -1 : 1) * (CoverSide + dY);
        //                double deltaY5 = y5 + ((y5 > 0) ? -1 : 1) * (CoverSide + dY);
        //                double dX = (bottom) ? (i * BarModel.Distance) : (dMainBottom + dSide + Diameter * 0.5 + i * BarModel.Distance);
        //                double deltaX0 = x0 + ((x0 > 0) ? -1 : 1) * (CoverSide + dX);
        //                double deltaX5 = x5 + ((x5 > 0) ? -1 : 1) * (CoverSide + dX);

        //                if (plus <= deltax0x1)
        //                {
        //                    ListP0.Add(p0 + Unit.Convert(deltaX0) * x + Unit.Convert(deltaY0) * y);
        //                    Distance.Add(((Math.Abs((deltaY0) - (deltaY5))) / (BarModel.Layer + 1)));
        //                }
        //                else
        //                {
        //                    double detataX00 = Math.Abs(plus - deltax0x1);
        //                    double detataY00 = detataX00 * detatay1y2 / deltax1x2;
        //                    ListP0.Add(p0 + Unit.Convert(deltaX0) * x + Unit.Convert(deltaY0 + ((deltaY0 > 0) ? (-detataY00) : (detataY00))) * y);
        //                    Distance.Add(((Math.Abs((deltaY0 + ((deltaY0 > 0) ? (-detataY00) : (detataY00))) - (deltaY5 + ((deltaY5 > 0) ? (-detataY00) : (detataY00))))) / (BarModel.Layer + 1)));
        //                }
        //                plus += BarModel.Distance;
        //            }

        //        }
        //    }
        //    else
        //    {
        //        if (Horizontal)
        //        {
        //            double plus = 0;
        //            double total = Math.Abs(y0 - y2) - 2 * CoverSide - Diameter;
        //            double detatax1x2 = Math.Abs(x1 - x2);
        //            double deltay0y1 = Math.Abs(y0 - y1) - CoverSide - 0.5 * Diameter;
        //            double deltay1y2 = Math.Abs(y1 - y2) - CoverSide - 0.5 * Diameter;
        //            int number = (int)(total / BarModel.Distance) + 1;
        //            for (int i = 0; i < number; i++)
        //            {
        //                double dX = (bottom) ? (0) : (dMainBottom + dSide + Diameter * 0.5);
        //                double deltaX0 = x0 + ((x0 > 0) ? -1 : 1) * (CoverSide + dX);
        //                double deltaX5 = x5 + ((x5 > 0) ? -1 : 1) * (CoverSide + dX);
        //                double dY = (bottom) ? (i * BarModel.Distance) : (dMainBottom + dSide + Diameter * 0.5 + i * BarModel.Distance);
        //                double deltaY0 = y0 + ((y0 > 0) ? -1 : 1) * (CoverSide + dY);
        //                double deltaY5 = y5 + ((y5 > 0) ? -1 : 1) * (CoverSide + dY);

        //                if (plus <= deltay0y1)
        //                {
        //                    ListP0.Add(p0 + Unit.Convert(deltaX5) * x + Unit.Convert(deltaY0) * y);
        //                    Distance.Add(((Math.Abs((deltaX0) - (deltaX5))) / (BarModel.Layer + 1)));
        //                }
        //                else
        //                {
        //                    double detataY00 = Math.Abs(plus - deltay0y1);
        //                    double detataX00 = detataY00 * detatax1x2 / deltay1y2;
        //                    ListP0.Add(p0 + Unit.Convert(deltaX5 + ((deltaX5 > 0) ? (-detataX00) : (detataX00))) * x + Unit.Convert(deltaY0) * y);
        //                    Distance.Add(((Math.Abs((deltaX0 + ((deltaX0 > 0) ? (-detataX00) : (detataX00))) - (deltaX5 + ((deltaX5 > 0) ? (-detataX00) : (detataX00))))) / (BarModel.Layer + 1)));
        //                }
        //                plus += BarModel.Distance;
        //            }
        //        }
        //        else
        //        {

        //            double plus = 0;
        //            double total = Math.Abs(x0 - x5) - 2 * CoverSide - Diameter;
        //            double detatax5x3 = Math.Abs(x5 - x3) - CoverSide - 0.5 * Diameter;
        //            double detatax3x2 = Math.Abs(x3 - x2);
        //            double detatax2x0 = Math.Abs(x2 - x0) - CoverSide - 0.5 * Diameter;
        //            double deltay0y1 = Math.Abs(y0 - y1);
        //            double deltay1y2 = Math.Abs(y1 - y2);
        //            int ii = (int)((detatax5x3 + detatax3x2) / BarModel.Distance);
        //            int number = (int)(total / BarModel.Distance) + 1;
        //            for (int i = 0; i < number; i++)
        //            {
        //                double dX = (bottom) ? (i * BarModel.Distance) : (dMainBottom + dSide + Diameter * 0.5 + i * BarModel.Distance);
        //                double deltaX0 = x0 + ((x0 > 0) ? -1 : 1) * (CoverSide + dX);
        //                double deltaX5 = x5 + ((x5 > 0) ? -1 : 1) * (CoverSide + dX);
        //                double dY = (bottom) ? (0) : (dMainBottom + dSide + Diameter * 0.5);
        //                double deltaY0 = y0 + ((y0 > 0) ? -1 : 1) * (CoverSide + dY);
        //                double deltaY1 = y1;
        //                double deltaY2 = y2;
        //                double tanA = Math.Abs(y1 - y2) / Math.Abs(x1 - x2);

        //                if (plus <= detatax5x3)
        //                {

        //                    ListP0.Add(p0 + Unit.Convert(deltaX5) * x + Unit.Convert(deltaY0) * y);
        //                    ListP0.Add(p0 + Unit.Convert(deltaX0) * x + Unit.Convert(deltaY0) * y);
        //                    Distance.Add(((Math.Abs((deltaY0) - (deltaY1 + ((deltaY1 >= 0) ? -1 : 1) * ((CoverSide - 0.5 * Diameter + i * BarModel.Distance) * tanA)))) / (BarModel.Layer + 1)));
        //                    Distance.Add(((Math.Abs((deltaY0) - (deltaY1 + ((deltaY1 >= 0) ? -1 : 1) * ((CoverSide - 0.5 * Diameter + i * BarModel.Distance) * tanA)))) / (BarModel.Layer + 1)));
        //                }
        //                if (plus <= detatax5x3 + detatax3x2 && plus > detatax5x3)
        //                {
        //                    ListP0.Add(p0 + Unit.Convert(deltaX5) * x + Unit.Convert(deltaY0) * y);
        //                    Distance.Add(((Math.Abs((deltaY0) - (deltaY2 + ((deltaY2 > 0) ? -1 : 1) * (CoverSide - 0.5 * Diameter)))) / (BarModel.Layer + 1)));
        //                }

        //                plus += BarModel.Distance;
        //            }
        //        }
        //    }
        //    for (int i = 0; i < ListP0.Count; i++)
        //    {
        //        curves.Add(GetAddVerticalItem(settingModel, Unit, ListP0[i], CoverTop, CoverBottom, CoverSide));
        //    }
        //    return curves;
        //}
        //private static ObservableCollection<Curve> GetCurvesSecondaryAddItem0(SettingModel settingModel, FoundationModel FoundationModel, FoundationBarModel FoundationBarModel, BarModel BarModel, UnitProject Unit, bool bottom, double dMainBottom, double dSide, double CoverTop, double CoverBottom, double CoverSide, out List<double> Distance)
        //{
        //    Distance = new List<double>();
        //    List<XYZ> ListP0 = new List<XYZ>();
        //    double Diameter = BarModel.Bar.Diameter;
        //    XYZ x = null;
        //    XYZ y = null;
        //    XYZ p0 = FoundationModel.ColumnModel.PointPosition;
        //    ObservableCollection<Curve> curves = new ObservableCollection<Curve>();
        //    bool Horizontal = (FoundationBarModel.SpanOrientation.Equals("Horizontal"));
        //    double x0 = FoundationModel.BoundingLocation[0].X;
        //    double x1 = FoundationModel.BoundingLocation[1].X;
        //    double x2 = FoundationModel.BoundingLocation[2].X;
        //    double x3 = FoundationModel.BoundingLocation[3].X;
        //    double x4 = FoundationModel.BoundingLocation[4].X;
        //    double x5 = FoundationModel.BoundingLocation[5].X;
        //    double y0 = FoundationModel.BoundingLocation[0].Y;
        //    double y1 = FoundationModel.BoundingLocation[1].Y;
        //    double y2 = FoundationModel.BoundingLocation[2].Y;
        //    double y3 = FoundationModel.BoundingLocation[3].Y;
        //    double y4 = FoundationModel.BoundingLocation[4].Y;
        //    double y5 = FoundationModel.BoundingLocation[5].Y;

        //    if (FoundationModel.ColumnModel.Style.Equals("RECTANGLE"))
        //    {
        //        x = FoundationModel.ColumnModel.East.FaceNormal;
        //        y = FoundationModel.ColumnModel.Nouth.FaceNormal;
        //    }
        //    else
        //    {
        //        x = XYZ.BasisX;
        //        y = XYZ.BasisY;
        //    }

        //    if (FoundationModel.ColumnModel.Style.Equals("RECTANGLE") && FoundationModel.ColumnModel.b > FoundationModel.ColumnModel.h)
        //    {
        //        if (Horizontal)
        //        {
        //            double plus = 0;
        //            double total = Math.Abs(x0 - x2) - 2 * CoverSide - Diameter;
        //            double detatay1y2 = Math.Abs(y1 - y2);
        //            double deltax0x1 = Math.Abs(x0 - x1) - CoverSide - 0.5 * Diameter;
        //            double deltax1x2 = Math.Abs(x1 - x2) - CoverSide - 0.5 * Diameter;
        //            int number = (int)(total / BarModel.Distance) + 1;
        //            for (int i = 0; i < number; i++)
        //            {
        //                double dY = (bottom) ? (0) : (dMainBottom + dSide + Diameter * 0.5);
        //                double deltaY0 = y0 + ((y0 > 0) ? -1 : 1) * (CoverSide + dY);
        //                double deltaY5 = y5 + ((y5 > 0) ? -1 : 1) * (CoverSide + dY);
        //                double dX = (bottom) ? (i * BarModel.Distance) : (dMainBottom + dSide + Diameter * 0.5 + i * BarModel.Distance);
        //                double deltaX0 = x0 + ((x0 > 0) ? -1 : 1) * (CoverSide + dX);
        //                double deltaX5 = x5 + ((x5 > 0) ? -1 : 1) * (CoverSide + dX);

        //                if (plus <= deltax0x1)
        //                {
        //                    ListP0.Add(p0 + Unit.Convert(deltaX0) * x + Unit.Convert(deltaY0) * y);
        //                    Distance.Add(((Math.Abs((deltaY0) - (deltaY5)) / (BarModel.Layer + 1))));
        //                }
        //                else
        //                {
        //                    double detataX00 = Math.Abs(plus - deltax0x1);
        //                    double detataY00 = detataX00 * detatay1y2 / deltax1x2;
        //                    ListP0.Add(p0 + Unit.Convert(deltaX0) * x + Unit.Convert(deltaY0 + ((deltaY0 > 0) ? (-detataY00) : (detataY00))) * y);
        //                    Distance.Add(((Math.Abs((deltaY0 + ((deltaY0 > 0) ? (-detataY00) : (detataY00))) - (deltaY5 + ((deltaY5 > 0) ? (-detataY00) : (detataY00)))) / (BarModel.Layer + 1))));
        //                }
        //                plus += BarModel.Distance;
        //            }

        //        }
        //        else
        //        {
        //            double plus = 0;
        //            double total = Math.Abs(y0 - y5) - 2 * CoverSide - Diameter;
        //            double detatay5y3 = Math.Abs(y5 - y3) - CoverSide - 0.5 * Diameter;
        //            double detatay3y2 = Math.Abs(y3 - y2);
        //            double detatay2y0 = Math.Abs(y2 - y0) - CoverSide - 0.5 * Diameter;

        //            double deltax0x1 = Math.Abs(x0 - x1);
        //            double deltax1x2 = Math.Abs(x1 - x2);


        //            int ii = (int)((detatay5y3 + detatay3y2) / BarModel.Distance);
        //            int number = (int)(total / BarModel.Distance) + 1;
        //            for (int i = 0; i < number; i++)
        //            {
        //                double dY = (bottom) ? (i * BarModel.Distance) : (dMainBottom + dSide + Diameter * 0.5 + i * BarModel.Distance);
        //                double deltaY0 = y0 + ((y0 > 0) ? -1 : 1) * (CoverSide + dY);
        //                double deltaY5 = y5 + ((y5 > 0) ? -1 : 1) * (CoverSide + dY);
        //                double dX = (bottom) ? (0) : (dMainBottom + dSide + Diameter * 0.5);
        //                double deltaX0 = x0 + ((x0 > 0) ? -1 : 1) * (CoverSide + dX);
        //                double deltaX1 = x1;
        //                double deltaX2 = x2;
        //                double tanA = Math.Abs(x1 - x2) / Math.Abs(y1 - y2);

        //                if (plus <= detatay5y3)
        //                {
        //                    ListP0.Add(p0 + Unit.Convert(deltaX0) * x + Unit.Convert(deltaY0) * y);
        //                    Distance.Add(((Math.Abs((deltaX0) - (deltaX1 + ((deltaX1 >= 0) ? 1 : -1) * ((CoverSide - 0.5 * Diameter + i * BarModel.Distance) * tanA))) / (BarModel.Layer + 1))));
        //                    ListP0.Add(p0 + Unit.Convert(deltaX0) * x + Unit.Convert(deltaY5) * y);
        //                    Distance.Add(((Math.Abs((deltaX0) - (deltaX1 + ((deltaX1 >= 0) ? 1 : -1) * ((CoverSide - 0.5 * Diameter + i * BarModel.Distance) * tanA))) / (BarModel.Layer + 1))));
        //                }
        //                if (plus <= detatay5y3 + detatay3y2 && plus > detatay5y3)
        //                {
        //                    ListP0.Add(p0 + Unit.Convert(deltaX0) * x + Unit.Convert(deltaY0) * y);
        //                    Distance.Add(((Math.Abs((deltaX0) - (deltaX2 + ((deltaX2 > 0) ? -1 : 1) * (CoverSide - 0.5 * Diameter))) / (BarModel.Layer + 1))));
        //                }

        //                plus += BarModel.Distance;
        //            }
        //        }
        //    }
        //    else
        //    {
        //        if (Horizontal)
        //        {
        //            double plus = 0;
        //            double total = Math.Abs(x0 - x5) - 2 * CoverSide - Diameter;
        //            double detatax5x3 = Math.Abs(x5 - x3) - CoverSide - 0.5 * Diameter;
        //            double detatax3x2 = Math.Abs(x3 - x2);
        //            double detatax2x0 = Math.Abs(x2 - x0) - CoverSide - 0.5 * Diameter;
        //            double deltay0y1 = Math.Abs(y0 - y1);
        //            double deltay1y2 = Math.Abs(y1 - y2);
        //            int ii = (int)((detatax5x3 + detatax3x2) / BarModel.Distance);
        //            int number = (int)(total / BarModel.Distance) + 1;
        //            for (int i = 0; i < number; i++)
        //            {
        //                double dX = (bottom) ? (i * BarModel.Distance) : (dMainBottom + dSide + Diameter * 0.5 + i * BarModel.Distance);
        //                double deltaX0 = x0 + ((x0 > 0) ? -1 : 1) * (CoverSide + dX);
        //                double deltaX5 = x5 + ((x5 > 0) ? -1 : 1) * (CoverSide + dX);
        //                double dY = (bottom) ? (0) : (dMainBottom + dSide + Diameter * 0.5);
        //                double deltaY0 = y0 + ((y0 > 0) ? -1 : 1) * (CoverSide + dY);
        //                double deltaY1 = y1;
        //                double deltaY2 = y2;
        //                double tanA = Math.Abs(y1 - y2) / Math.Abs(x1 - x2);

        //                if (plus <= detatax5x3)
        //                {
        //                    ListP0.Add(p0 + Unit.Convert(deltaX5) * x + Unit.Convert(deltaY0) * y);
        //                    Distance.Add(((Math.Abs((deltaY0) - (deltaY1 + ((deltaY1 >= 0) ? -1 : 1) * ((CoverSide - 0.5 * Diameter + i * BarModel.Distance) * tanA))) / (BarModel.Layer + 1))));
        //                    ListP0.Add(p0 + Unit.Convert(deltaX0) * x + Unit.Convert(deltaY0) * y);
        //                    Distance.Add(((Math.Abs((deltaY0) - (deltaY1 + ((deltaY1 >= 0) ? -1 : 1) * ((CoverSide - 0.5 * Diameter + i * BarModel.Distance) * tanA))) / (BarModel.Layer + 1))));
        //                }
        //                if (plus <= detatax5x3 + detatax3x2 && plus > detatax5x3)
        //                {
        //                    ListP0.Add(p0 + Unit.Convert(deltaX5) * x + Unit.Convert(deltaY0) * y);
        //                    Distance.Add(((Math.Abs((deltaY0) - (deltaY1 + (deltaY2 + ((deltaY2 > 0) ? -1 : 1) * (CoverSide - 0.5 * Diameter)))) / (BarModel.Layer + 1))));
        //                }

        //                plus += BarModel.Distance;
        //            }
        //        }
        //        else
        //        {
        //            double plus = 0;
        //            double total = Math.Abs(y0 - y2) - 2 * CoverSide - Diameter;
        //            double detatax1x2 = Math.Abs(x1 - x2);
        //            double deltay0y1 = Math.Abs(y0 - y1) - CoverSide - 0.5 * Diameter;
        //            double deltay1y2 = Math.Abs(y1 - y2) - CoverSide - 0.5 * Diameter;
        //            int number = (int)(total / BarModel.Distance) + 1;
        //            for (int i = 0; i < number; i++)
        //            {
        //                double dX = (bottom) ? (0) : (dMainBottom + dSide + Diameter * 0.5);
        //                double deltaX0 = x0 + ((x0 > 0) ? -1 : 1) * (CoverSide + dX);
        //                double deltaX5 = x5 + ((x5 > 0) ? -1 : 1) * (CoverSide + dX);
        //                double dY = (bottom) ? (i * BarModel.Distance) : (dMainBottom + dSide + Diameter * 0.5 + i * BarModel.Distance);
        //                double deltaY0 = y0 + ((y0 > 0) ? -1 : 1) * (CoverSide + dY);
        //                double deltaY5 = y5 + ((y5 > 0) ? -1 : 1) * (CoverSide + dY);

        //                if (plus <= deltay0y1)
        //                {
        //                    ListP0.Add(p0 + Unit.Convert(deltaX0) * x + Unit.Convert(deltaY0) * y);
        //                    Distance.Add(((Math.Abs((deltaX0) - (deltaX5)) / (BarModel.Layer + 1))));
        //                }
        //                else
        //                {
        //                    double detataY00 = Math.Abs(plus - deltay0y1);
        //                    double detataX00 = detataY00 * detatax1x2 / deltay1y2;
        //                    ListP0.Add(p0 + Unit.Convert(deltaX0 + ((deltaX0 > 0) ? (-detataX00) : (detataX00))) * x + Unit.Convert(deltaY0) * y);
        //                    Distance.Add(((Math.Abs((deltaX0) - (deltaX5 + ((deltaX5 > 0) ? (-detataX00) : (detataX00)))) / (BarModel.Layer + 1))));
        //                }
        //                plus += BarModel.Distance;
        //            }


        //        }
        //    }
        //    for (int i = 0; i < ListP0.Count; i++)
        //    {
        //        curves.Add(GetAddVerticalItem(settingModel, Unit, ListP0[i], CoverTop, CoverBottom, CoverSide));
        //    }
        //    return curves;
        //}
        //public static ObservableCollection<Curve> GetCurvesImage0(SettingModel settingModel, FoundationModel FoundationModel, FoundationBarModel FoundationBarModel, BarModel BarModel, UnitProject Unit, double dMainBottom, double dMainTop, double dSide, double CoverTop, double CoverBottom, double CoverSide, out List<double> Distance)
        //{
        //    Distance = new List<double>();
        //    ObservableCollection<Curve> curves = new ObservableCollection<Curve>();
        //    if (BarModel.IsModel)
        //    {
        //        switch (BarModel.Name)
        //        {
        //            case "MainBottom": curves = GetCurvesMainItem0(settingModel, FoundationModel, FoundationBarModel, BarModel, Unit, true, false, dMainBottom, dSide, CoverTop, CoverBottom, CoverSide); break;
        //            case "MainTop": curves = GetCurvesMainItem0(settingModel, FoundationModel, FoundationBarModel, BarModel, Unit, false, false, dMainBottom, dSide, CoverTop, CoverBottom, CoverSide); break;
        //            case "MainAddHorizontal": curves = GetCurvesMainItem0(settingModel, FoundationModel, FoundationBarModel, BarModel, Unit, true, true, dMainBottom, dSide, CoverTop, CoverBottom, CoverSide); break;
        //            case "MainAddVertical": curves = GetCurvesMainAddItem0(settingModel, FoundationModel, FoundationBarModel, BarModel, Unit, true, dMainBottom, dSide, CoverTop, CoverBottom, CoverSide, out Distance); break;
        //            case "SecondaryBottom": curves = GetCurvesSecondaryItem0(settingModel, FoundationModel, FoundationBarModel, BarModel, Unit, true, false, dMainBottom, dMainTop, dSide, CoverTop, CoverBottom, CoverSide); break;
        //            case "SecondaryTop": curves = GetCurvesSecondaryItem0(settingModel, FoundationModel, FoundationBarModel, BarModel, Unit, false, false, dMainBottom, dMainTop, dSide, CoverTop, CoverBottom, CoverSide); break;
        //            case "SecondaryAddHorizontal": curves = GetCurvesSecondaryItem0(settingModel, FoundationModel, FoundationBarModel, BarModel, Unit, true, true, dMainBottom, dMainTop, dSide, CoverTop, CoverBottom, CoverSide); break;
        //            case "SecondaryAddVertical": curves = GetCurvesSecondaryAddItem0(settingModel, FoundationModel, FoundationBarModel, BarModel, Unit, true, dMainBottom, dSide, CoverTop, CoverBottom, CoverSide, out Distance); break;
        //            case "Side": curves = GetCurvesSide(settingModel, FoundationModel, FoundationBarModel, BarModel, Unit, dMainBottom, CoverTop, CoverBottom, CoverSide); break;
        //            default: break;
        //        }
        //    }
        //    return curves;
        //}

        #endregion
        #region Image1
        private static ObservableCollection<Curve> GetCurvesMainItem1(SettingModel settingModel, FoundationModel FoundationModel, FoundationBarModel FoundationBarModel, BarModel BarModel, UnitProject Unit, bool bottom, bool add, double dMainBottom, double dSide, double CoverTop, double CoverBottom, double CoverSide)
        {

            double Diameter = BarModel.Bar.Diameter;
            XYZ x = null;
            XYZ y = null;
            XYZ p0 = FoundationModel.ColumnModel.PointPosition;
            ObservableCollection<Curve> curves = new ObservableCollection<Curve>();
            bool Horizontal = (FoundationBarModel.SpanOrientation.Equals("Horizontal"));
            double deltaZ0 = (bottom) ? (settingModel.HeightFoundation - CoverBottom) : (CoverTop);
            double delta = (settingModel.HeightFoundation - CoverBottom - CoverTop) / (BarModel.Layer + 1);
            if (add)
            {
                deltaZ0 = CoverTop + delta * BarModel.Layer;
            }
            double x0 = FoundationModel.BoundingLocation[0].X;
            double x1 = FoundationModel.BoundingLocation[1].X;
            double xCount = FoundationModel.BoundingLocation[FoundationModel.BoundingLocation.Count - 1].X;
            double y0 = FoundationModel.BoundingLocation[0].Y;
            double y1 = FoundationModel.BoundingLocation[1].Y;
            double yCount = FoundationModel.BoundingLocation[FoundationModel.BoundingLocation.Count - 1].Y;
            double diameterBottom = (bottom) ? (0) : (dMainBottom + dSide);
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

            if (Horizontal)
            {
                if (add)
                {
                    for (int i = 0; i < BarModel.Layer; i++)
                    {
                        XYZ p1 = p0 + Unit.Convert(x0 + ((x0 > 0) ? -1 : 1) * (CoverSide + Diameter * 0.5 + diameterBottom)) * x + Unit.Convert(y0 + ((y0 > 0) ? -1 : 1) * (CoverSide + Diameter * 0.5 + diameterBottom)) * y + Unit.Convert(-deltaZ0 + i * delta) * XYZ.BasisZ;
                        XYZ p2 = p0 + Unit.Convert(xCount + ((xCount > 0) ? -1 : 1) * (CoverSide + Diameter * 0.5 + diameterBottom)) * x + Unit.Convert(y0 + ((y0 > 0) ? -1 : 1) * (CoverSide + Diameter * 0.5 + diameterBottom)) * y + Unit.Convert(-deltaZ0 + i * delta) * XYZ.BasisZ;
                        curves.Add(Line.CreateBound(p1, p2));
                    }
                }
                else
                {
                    XYZ p1 = p0 + Unit.Convert(x0 + ((x0 > 0) ? -1 : 1) * (CoverSide + Diameter * 0.5 + diameterBottom)) * x + Unit.Convert(y0 + ((y0 > 0) ? -1 : 1) * (CoverSide + Diameter * 0.5 + diameterBottom)) * y + Unit.Convert(-deltaZ0) * XYZ.BasisZ;
                    XYZ p2 = p0 + Unit.Convert(xCount + ((xCount > 0) ? -1 : 1) * (CoverSide + Diameter * 0.5 + diameterBottom)) * x + Unit.Convert(y0 + ((y0 > 0) ? -1 : 1) * (CoverSide + Diameter * 0.5 + diameterBottom)) * y + Unit.Convert(-deltaZ0) * XYZ.BasisZ;
                    curves.Add(Line.CreateBound(p1, p2));
                }

            }
            else
            {
                if (add)
                {
                    for (int i = 0; i < BarModel.Layer; i++)
                    {
                        XYZ p1 = p0 + Unit.Convert(x0 + ((x0 > 0) ? -1 : 1) * (CoverSide + Diameter * 0.5 + diameterBottom)) * x + Unit.Convert(y0 + ((y0 > 0) ? -1 : 1) * (CoverSide + Diameter * 0.5 + diameterBottom)) * y + Unit.Convert(-deltaZ0 + i * delta) * XYZ.BasisZ;
                        XYZ p2 = p0 + Unit.Convert(x1 + ((x1 > 0) ? -1 : 1) * (CoverSide + Diameter * 0.5 + diameterBottom)) * x + Unit.Convert(y1 + ((y1 > 0) ? -1 : 1) * (CoverSide + Diameter * 0.5 + diameterBottom)) * y + Unit.Convert(-deltaZ0 + i * delta) * XYZ.BasisZ;
                        curves.Add(Line.CreateBound(p1, p2));
                    }

                }
                else
                {
                    XYZ p1 = p0 + Unit.Convert(x0 + ((x0 > 0) ? -1 : 1) * (CoverSide + Diameter * 0.5 + diameterBottom)) * x + Unit.Convert(y0 + ((y0 > 0) ? -1 : 1) * (CoverSide + Diameter * 0.5 + diameterBottom)) * y + Unit.Convert(-deltaZ0) * XYZ.BasisZ;
                    XYZ p2 = p0 + Unit.Convert(x1 + ((x1 > 0) ? -1 : 1) * (CoverSide + Diameter * 0.5 + diameterBottom)) * x + Unit.Convert(y1 + ((y1 > 0) ? -1 : 1) * (CoverSide + Diameter * 0.5 + diameterBottom)) * y + Unit.Convert(-deltaZ0) * XYZ.BasisZ;
                    curves.Add(Line.CreateBound(p1, p2));
                }

            }

            return curves;
        }

        private static ObservableCollection<Curve> GetCurvesSecondaryItem1(SettingModel settingModel, FoundationModel FoundationModel, FoundationBarModel FoundationBarModel, BarModel BarModel, UnitProject Unit, bool bottom, bool add, double dMainBottom, double dMainTop, double dSide, double CoverTop, double CoverBottom, double CoverSide)
        {

            double Diameter = BarModel.Bar.Diameter;
            XYZ x = null;
            XYZ y = null;
            XYZ p0 = FoundationModel.ColumnModel.PointPosition;
            ObservableCollection<Curve> curves = new ObservableCollection<Curve>();
            bool Horizontal = (FoundationBarModel.SpanOrientation.Equals("Horizontal"));
            double deltaZ0 = (bottom) ? (settingModel.HeightFoundation - CoverBottom - dMainBottom) : (CoverTop + dMainTop);
            double delta = (settingModel.HeightFoundation - CoverBottom - CoverTop) / (BarModel.Layer + 1);
            if (add)
            {

                deltaZ0 = CoverTop + delta * BarModel.Layer;
            }
            double x0 = FoundationModel.BoundingLocation[0].X;
            double x1 = FoundationModel.BoundingLocation[1].X;
            double xCount = FoundationModel.BoundingLocation[FoundationModel.BoundingLocation.Count - 1].X;
            double y0 = FoundationModel.BoundingLocation[0].Y;
            double y1 = FoundationModel.BoundingLocation[1].Y;
            double yCount = FoundationModel.BoundingLocation[FoundationModel.BoundingLocation.Count - 1].Y;
            double diameterBottom = (bottom) ? (0) : (dMainBottom + dSide);
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

            if (Horizontal)
            {
                if (add)
                {
                    for (int i = 0; i < BarModel.Layer; i++)
                    {
                        XYZ p1 = p0 + Unit.Convert(x0 + ((x0 > 0) ? -1 : 1) * (CoverSide + Diameter * 0.5 + diameterBottom)) * x + Unit.Convert(y0 + ((y0 > 0) ? -1 : 1) * (CoverSide + Diameter * 0.5 + diameterBottom)) * y + Unit.Convert(-deltaZ0 + i * delta) * XYZ.BasisZ;
                        XYZ p2 = p0 + Unit.Convert(x1 + ((x1 > 0) ? -1 : 1) * (CoverSide + Diameter * 0.5 + diameterBottom)) * x + Unit.Convert(y1 + ((y1 > 0) ? -1 : 1) * (CoverSide + Diameter * 0.5 + diameterBottom)) * y + Unit.Convert(-deltaZ0 + i * delta) * XYZ.BasisZ;
                        curves.Add(Line.CreateBound(p1, p2));
                    }
                }
                else
                {
                    XYZ p1 = p0 + Unit.Convert(x0 + ((x0 > 0) ? -1 : 1) * (CoverSide + Diameter * 0.5 + diameterBottom)) * x + Unit.Convert(y0 + ((y0 > 0) ? -1 : 1) * (CoverSide + Diameter * 0.5 + diameterBottom)) * y + Unit.Convert(-deltaZ0) * XYZ.BasisZ;
                    XYZ p2 = p0 + Unit.Convert(x1 + ((x1 > 0) ? -1 : 1) * (CoverSide + Diameter * 0.5 + diameterBottom)) * x + Unit.Convert(y1 + ((y1 > 0) ? -1 : 1) * (CoverSide + Diameter * 0.5 + diameterBottom)) * y + Unit.Convert(-deltaZ0) * XYZ.BasisZ;
                    curves.Add(Line.CreateBound(p1, p2));
                }


            }
            else
            {
                if (add)
                {
                    for (int i = 0; i < BarModel.Layer; i++)
                    {
                        XYZ p1 = p0 + Unit.Convert(x0 + ((x0 > 0) ? -1 : 1) * (CoverSide + Diameter * 0.5 + diameterBottom)) * x + Unit.Convert(y0 + ((y0 > 0) ? -1 : 1) * (CoverSide + Diameter * 0.5 + diameterBottom)) * y + Unit.Convert(-deltaZ0 + i * delta) * XYZ.BasisZ;
                        XYZ p2 = p0 + Unit.Convert(xCount + ((xCount > 0) ? -1 : 1) * (CoverSide + Diameter * 0.5 + diameterBottom)) * x + Unit.Convert(y0 + ((y0 > 0) ? -1 : 1) * (CoverSide + Diameter * 0.5 + diameterBottom)) * y + Unit.Convert(-deltaZ0 + i * delta) * XYZ.BasisZ;
                        curves.Add(Line.CreateBound(p1, p2));
                    }
                }
                else
                {
                    XYZ p1 = p0 + Unit.Convert(x0 + ((x0 > 0) ? -1 : 1) * (CoverSide + Diameter * 0.5 + diameterBottom)) * x + Unit.Convert(y0 + ((y0 > 0) ? -1 : 1) * (CoverSide + Diameter * 0.5 + diameterBottom)) * y + Unit.Convert(-deltaZ0) * XYZ.BasisZ;
                    XYZ p2 = p0 + Unit.Convert(xCount + ((xCount > 0) ? -1 : 1) * (CoverSide + Diameter * 0.5 + diameterBottom)) * x + Unit.Convert(y0 + ((y0 > 0) ? -1 : 1) * (CoverSide + Diameter * 0.5 + diameterBottom)) * y + Unit.Convert(-deltaZ0) * XYZ.BasisZ;
                    curves.Add(Line.CreateBound(p1, p2));
                }

            }

            return curves;
        }
        private static ObservableCollection<Curve> GetCurvesMainAddItem1(SettingModel settingModel, FoundationModel FoundationModel, FoundationBarModel FoundationBarModel, BarModel BarModel, UnitProject Unit, bool bottom, double dMainBottom, double dSide, double CoverTop, double CoverBottom, double CoverSide, out List<double> Distance)
        {
            Distance = new List<double>();
            List<XYZ> ListP0 = new List<XYZ>();
            double Diameter = BarModel.Bar.Diameter;
            XYZ x = null;
            XYZ y = null;
            XYZ p0 = FoundationModel.ColumnModel.PointPosition;
            ObservableCollection<Curve> curves = new ObservableCollection<Curve>();
            bool Horizontal = (FoundationBarModel.SpanOrientation.Equals("Horizontal"));

            double x0 = FoundationModel.BoundingLocation[0].X;
            double x1 = FoundationModel.BoundingLocation[1].X;
            double xCount = FoundationModel.BoundingLocation[FoundationModel.BoundingLocation.Count - 1].X;
            double y0 = FoundationModel.BoundingLocation[0].Y;
            double y1 = FoundationModel.BoundingLocation[1].Y;
            double yCount = FoundationModel.BoundingLocation[FoundationModel.BoundingLocation.Count - 1].Y;
            double diameterBottom = (bottom) ? (0) : (dMainBottom + dSide);
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

            if (Horizontal)
            {
                double total = Math.Abs(y0 - y1) - 2 * CoverSide - Diameter;
                int number = (int)(total / BarModel.Distance) + 1;
                for (int i = 0; i < number; i++)
                {
                    XYZ p1 = p0 + Unit.Convert(x0 + ((x0 > 0) ? -1 : 1) * (CoverSide + Diameter * 0.5 + diameterBottom)) * x + Unit.Convert(y0 + ((y0 > 0) ? -1 : 1) * (CoverSide + Diameter * 0.5 + diameterBottom + i * BarModel.Distance)) * y;

                    ListP0.Add(p1);
                    Distance.Add(((Math.Abs((x0 + ((x0 > 0) ? -1 : 1) * (CoverSide + Diameter * 0.5 + diameterBottom)) - (xCount + ((xCount > 0) ? -1 : 1) * (CoverSide + Diameter * 0.5 + diameterBottom))) / (BarModel.Layer + 1))));
                }

            }
            else
            {
                double total = Math.Abs(x0 - xCount) - 2 * CoverSide - Diameter;
                int number = (int)(total / BarModel.Distance) + 1;
                for (int i = 0; i < number; i++)
                {
                    XYZ p1 = p0 + Unit.Convert(x0 + ((x0 > 0) ? -1 : 1) * (CoverSide + Diameter * 0.5 + diameterBottom + i * BarModel.Distance)) * x + Unit.Convert(y0 + ((y0 > 0) ? -1 : 1) * (CoverSide + Diameter * 0.5 + diameterBottom)) * y;

                    ListP0.Add(p1);
                    Distance.Add(((Math.Abs((y0 + ((y0 > 0) ? -1 : 1) * (CoverSide + Diameter * 0.5 + diameterBottom)) - (y1 + ((y1 > 0) ? -1 : 1) * (CoverSide + Diameter * 0.5 + diameterBottom))) / (BarModel.Layer + 1))));
                }

            }
            for (int i = 0; i < ListP0.Count; i++)
            {
                curves.Add(GetAddVerticalItem(settingModel, Unit, ListP0[i], CoverTop, CoverBottom, CoverSide));
            }
            return curves;
        }
        private static ObservableCollection<Curve> GetCurvesSecondaryAddItem1(SettingModel settingModel, FoundationModel FoundationModel, FoundationBarModel FoundationBarModel, BarModel BarModel, UnitProject Unit, bool bottom, double dMainBottom, double dSide, double CoverTop, double CoverBottom, double CoverSide, out List<double> Distance)
        {
            Distance = new List<double>();
            List<XYZ> ListP0 = new List<XYZ>();
            double Diameter = BarModel.Bar.Diameter;
            XYZ x = null;
            XYZ y = null;
            XYZ p0 = FoundationModel.ColumnModel.PointPosition;
            ObservableCollection<Curve> curves = new ObservableCollection<Curve>();
            bool Horizontal = (FoundationBarModel.SpanOrientation.Equals("Horizontal"));

            double x0 = FoundationModel.BoundingLocation[0].X;
            double x1 = FoundationModel.BoundingLocation[1].X;
            double xCount = FoundationModel.BoundingLocation[FoundationModel.BoundingLocation.Count - 1].X;
            double y0 = FoundationModel.BoundingLocation[0].Y;
            double y1 = FoundationModel.BoundingLocation[1].Y;
            double yCount = FoundationModel.BoundingLocation[FoundationModel.BoundingLocation.Count - 1].Y;
            double diameterBottom = (bottom) ? (0) : (dMainBottom + dSide);
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

            if (Horizontal)
            {
                double total = Math.Abs(x0 - xCount) - 2 * CoverSide - Diameter;
                int number = (int)(total / BarModel.Distance) + 1;
                for (int i = 0; i < number; i++)
                {
                    XYZ p1 = p0 + Unit.Convert(x0 + ((x0 > 0) ? -1 : 1) * (CoverSide + Diameter * 0.5 + diameterBottom + i * BarModel.Distance)) * x + Unit.Convert(y0 + ((y0 > 0) ? -1 : 1) * (CoverSide + Diameter * 0.5 + diameterBottom)) * y;

                    ListP0.Add(p1);
                    Distance.Add(((Math.Abs((y0 + ((y0 > 0) ? -1 : 1) * (CoverSide + Diameter * 0.5 + diameterBottom)) - (y1 + ((y1 > 0) ? -1 : 1) * (CoverSide + Diameter * 0.5 + diameterBottom))) / (BarModel.Layer + 1))));
                }
            }
            else
            {
                double total = Math.Abs(y0 - y1) - 2 * CoverSide - Diameter;
                int number = (int)(total / BarModel.Distance) + 1;
                for (int i = 0; i < number; i++)
                {
                    XYZ p1 = p0 + Unit.Convert(x0 + ((x0 > 0) ? -1 : 1) * (CoverSide + Diameter * 0.5 + diameterBottom)) * x + Unit.Convert(y0 + ((y0 > 0) ? -1 : 1) * (CoverSide + Diameter * 0.5 + diameterBottom + i * BarModel.Distance)) * y;
                    ListP0.Add(p1);
                    Distance.Add(((Math.Abs((x0 + ((x0 > 0) ? -1 : 1) * (CoverSide + Diameter * 0.5 + diameterBottom)) - (xCount + ((xCount > 0) ? -1 : 1) * (CoverSide + Diameter * 0.5 + diameterBottom))) / (BarModel.Layer + 1))));
                }
            }
            for (int i = 0; i < ListP0.Count; i++)
            {
                curves.Add(GetAddVerticalItem(settingModel, Unit, ListP0[i], CoverTop, CoverBottom, CoverSide));
            }
            return curves;
        }
        public static ObservableCollection<Curve> GetCurvesImage1(SettingModel settingModel, FoundationModel FoundationModel, FoundationBarModel FoundationBarModel, BarModel BarModel, UnitProject Unit, double dMainBottom, double dMainTop, double dSide, double CoverTop, double CoverBottom, double CoverSide, out List<double> Distance)
        {
            Distance = new List<double>();
            ObservableCollection<Curve> curves = new ObservableCollection<Curve>();
            if (BarModel.IsModel)
            {
                switch (BarModel.Name)
                {
                    case "MainBottom": curves = GetCurvesMainItem1(settingModel, FoundationModel, FoundationBarModel, BarModel, Unit, true, false, dMainBottom, dSide, CoverTop, CoverBottom, CoverSide); break;
                    case "MainTop": curves = GetCurvesMainItem1(settingModel, FoundationModel, FoundationBarModel, BarModel, Unit, false, false, dMainBottom, dSide, CoverTop, CoverBottom, CoverSide); break;
                    case "MainAddHorizontal": curves = GetCurvesMainItem1(settingModel, FoundationModel, FoundationBarModel, BarModel, Unit, true, true, dMainBottom, dSide, CoverTop, CoverBottom, CoverSide); break;
                    case "MainAddVertical": curves = GetCurvesMainAddItem1(settingModel, FoundationModel, FoundationBarModel, BarModel, Unit, true, dMainBottom, dSide, CoverTop, CoverBottom, CoverSide, out Distance); break;
                    case "SecondaryBottom": curves = GetCurvesSecondaryItem1(settingModel, FoundationModel, FoundationBarModel, BarModel, Unit, true, false, dMainBottom, dMainTop, dSide, CoverTop, CoverBottom, CoverSide); break;
                    case "SecondaryTop": curves = GetCurvesSecondaryItem1(settingModel, FoundationModel, FoundationBarModel, BarModel, Unit, false, false, dMainBottom, dMainTop, dSide, CoverTop, CoverBottom, CoverSide); break;
                    case "SecondaryAddHorizontal": curves = GetCurvesSecondaryItem1(settingModel, FoundationModel, FoundationBarModel, BarModel, Unit, true, true, dMainBottom, dMainTop, dSide, CoverTop, CoverBottom, CoverSide); break;
                    case "SecondaryAddVertical": curves = GetCurvesSecondaryAddItem1(settingModel, FoundationModel, FoundationBarModel, BarModel, Unit, true, dMainBottom, dSide, CoverTop, CoverBottom, CoverSide, out Distance); break;
                    case "Side": curves = GetCurvesSide(settingModel, FoundationModel, FoundationBarModel, BarModel, Unit, dMainBottom, CoverTop, CoverBottom, CoverSide); break;
                    default: break;
                }
            }
            return curves;
        }
        #endregion
        #region Image2
        private static ObservableCollection<Curve> GetCurvesMainItem2(SettingModel settingModel, FoundationModel FoundationModel, FoundationBarModel FoundationBarModel, BarModel BarModel, UnitProject Unit, bool bottom, bool addHorizontal, bool addVertical, double dMainBottom, double dSide, double CoverTop, double CoverBottom, double CoverSide, out List<double> Distance)
        {

            Distance = new List<double>();
            List<XYZ> ListP0 = new List<XYZ>();
            double Diameter = BarModel.Bar.Diameter;
            XYZ x = null;
            XYZ y = null;
            XYZ p0 = FoundationModel.ColumnModel.PointPosition;
            List<Curve> curves0 = new List<Curve>();
            bool Horizontal = (FoundationBarModel.SpanOrientation.Equals("Horizontal"));
            double offset = (bottom) ? (CoverSide + Diameter * 0.5) : (CoverSide + dMainBottom + dSide + Diameter * 0.5);
            List<LocationModel> Location = FoundationModel.GetOffsetBoundingFoundation2(settingModel, offset);
            double x0 = Location[0].X;
            double x1 = Location[1].X;
            double x2 = Location[2].X;
            double x3 = Location[3].X;
            double x4 = Location[4].X;
            double y0 = Location[0].Y;
            double y1 = Location[1].Y;
            double y2 = Location[2].Y;
            double y3 = Location[3].Y;
            double y4 = Location[4].Y;
            double deltaZ0 = (bottom) ? (settingModel.HeightFoundation - CoverBottom) : (CoverTop);
            if (addHorizontal)
            {
                double delta = (settingModel.HeightFoundation - CoverBottom - CoverTop) / (BarModel.Layer + 1);
                deltaZ0 = CoverTop + delta * BarModel.Layer;
            }
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
            List<Curve> curve1 = new List<Curve>();
            List<Curve> curve2 = new List<Curve>();
            if (FoundationModel.ColumnModel.Style.Equals("RECTANGLE") && FoundationModel.ColumnModel.b > FoundationModel.ColumnModel.h)
            {
                if (Horizontal)
                {
                    List<double> Distance1 = new List<double>();
                    List<XYZ> ListP01 = new List<XYZ>();
                    curve1 = GetHorizontalQuadrangle(p0, x, y, x2, x1, x0, x2, y2, y1, y0, y2, deltaZ0, BarModel, Unit, CoverTop, CoverBottom, CoverSide, out ListP01, out Distance1);
                    List<double> Distance2 = new List<double>();
                    List<XYZ> ListP02 = new List<XYZ>();
                    curve2 = GetHorizontalQuadrangle(p0, x, y, x3, x4, x0, x2, y3, y4, y0, y2, deltaZ0, BarModel, Unit, CoverTop, CoverBottom, CoverSide, out ListP02, out Distance2);
                    ListP0.AddRange(ListP01);
                    ListP0.AddRange(ListP02);
                    Distance.AddRange(Distance1);
                    Distance.AddRange(Distance2);
                }
                else
                {
                    List<double> Distance1 = new List<double>();
                    List<XYZ> ListP01 = new List<XYZ>();
                    curve1 = GetHorizontalTriangle(p0, y, x, y0, y4, y1, x0, x4, x1, deltaZ0, BarModel, Unit, CoverTop, CoverBottom, CoverSide, out ListP01, out Distance1);
                    List<double> Distance2 = new List<double>();
                    List<XYZ> ListP02 = new List<XYZ>();
                    curve2 = GetHorizontalTrapezoid(p0, y, x, y3, y2, y1, y4, x3, x2, x1, x4, deltaZ0, BarModel, Unit, CoverTop, CoverBottom, CoverSide, out ListP02, out Distance2);
                    ListP0.AddRange(ListP01);
                    ListP0.AddRange(ListP02);
                    Distance.AddRange(Distance1);
                    Distance.AddRange(Distance2);

                }
            }
            else
            {
                if (Horizontal)
                {
                    List<double> Distance1 = new List<double>();
                    List<XYZ> ListP01 = new List<XYZ>();
                    curve1 = GetHorizontalTriangle(p0, x, y, x0, x4, x1, y0, y4, y1, deltaZ0, BarModel, Unit, CoverTop, CoverBottom, CoverSide, out ListP01, out Distance1);
                    List<double> Distance2 = new List<double>();
                    List<XYZ> ListP02 = new List<XYZ>();
                    curve2 = GetHorizontalTrapezoid(p0, x, y, x2, x3, x4, x1, y2, y3, y4, y1, deltaZ0, BarModel, Unit, CoverTop, CoverBottom, CoverSide, out ListP02, out Distance2);
                    ListP0.AddRange(ListP01);
                    ListP0.AddRange(ListP02);
                    Distance.AddRange(Distance1);
                    Distance.AddRange(Distance2);
                }
                else
                {
                    List<double> Distance1 = new List<double>();
                    List<XYZ> ListP01 = new List<XYZ>();
                    curve1 = GetHorizontalQuadrangle(p0, y, x, y3, y4, y0, y2, x3, x4, x0, x0, deltaZ0, BarModel, Unit, CoverTop, CoverBottom, CoverSide, out ListP01, out Distance1);
                    List<double> Distance2 = new List<double>();
                    List<XYZ> ListP02 = new List<XYZ>();
                    curve2 = GetHorizontalQuadrangle(p0, y, x, y2, y1, y0, y2, x2, x1, x0, x0, deltaZ0, BarModel, Unit, CoverTop, CoverBottom, CoverSide, out ListP02, out Distance2);
                    ListP0.AddRange(ListP01);
                    ListP0.AddRange(ListP02);
                    Distance.AddRange(Distance1);
                    Distance.AddRange(Distance2);
                }
            }
            if (addVertical)
            {
                for (int i = 0; i < ListP0.Count; i++)
                {
                    curves0.Add(GetAddVerticalItem(settingModel, Unit, ListP0[i], CoverTop, CoverBottom, CoverSide));
                }
            }
            else
            {
                curves0.AddRange(curve1);
                curves0.AddRange(curve2);
            }

            return new ObservableCollection<Curve>(curves0);
        }
        private static ObservableCollection<Curve> GetCurvesSecondaryItem2(SettingModel settingModel, FoundationModel FoundationModel, FoundationBarModel FoundationBarModel, BarModel BarModel, UnitProject Unit, bool bottom, bool addHorizontal, bool addVertical, double dMainBottom, double dMainTop, double dSide, double CoverTop, double CoverBottom, double CoverSide, out List<double> Distance)
        {
            Distance = new List<double>();
            List<XYZ> ListP0 = new List<XYZ>();
            double Diameter = BarModel.Bar.Diameter;
            XYZ x = null;
            XYZ y = null;
            XYZ p0 = FoundationModel.ColumnModel.PointPosition;
            List<Curve> curves0 = new List<Curve>();
            bool Horizontal = (FoundationBarModel.SpanOrientation.Equals("Horizontal"));
            double offset = (bottom) ? (CoverSide + Diameter * 0.5) : (CoverSide + dMainBottom + dSide + Diameter * 0.5);
            List<LocationModel> Location = FoundationModel.GetOffsetBoundingFoundation2(settingModel, offset);
            double x0 = Location[0].X;
            double x1 = Location[1].X;
            double x2 = Location[2].X;
            double x3 = Location[3].X;
            double x4 = Location[4].X;
            double y0 = Location[0].Y;
            double y1 = Location[1].Y;
            double y2 = Location[2].Y;
            double y3 = Location[3].Y;
            double y4 = Location[4].Y;
            double deltaZ0 = (bottom) ? (settingModel.HeightFoundation - CoverBottom - dMainBottom) : (CoverTop + dMainTop);
            if (addHorizontal)
            {
                double delta = (settingModel.HeightFoundation - CoverBottom - CoverTop) / (BarModel.Layer + 1);
                deltaZ0 = CoverTop + delta * BarModel.Layer;
            }
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
            List<Curve> curve1 = new List<Curve>();
            List<Curve> curve2 = new List<Curve>();
            if (FoundationModel.ColumnModel.Style.Equals("RECTANGLE") && FoundationModel.ColumnModel.b > FoundationModel.ColumnModel.h)
            {
                if (Horizontal)
                {
                    List<double> Distance1 = new List<double>();
                    List<XYZ> ListP01 = new List<XYZ>();
                    curve1 = GetHorizontalTriangle(p0, y, x, y0, y4, y1, x0, x4, x1, deltaZ0, BarModel, Unit, CoverTop, CoverBottom, CoverSide, out ListP01, out Distance1);
                    List<double> Distance2 = new List<double>();
                    List<XYZ> ListP02 = new List<XYZ>();
                    curve2 = GetHorizontalTrapezoid(p0, y, x, y3, y2, y1, y4, x3, x2, x1, x4, deltaZ0, BarModel, Unit, CoverTop, CoverBottom, CoverSide, out ListP02, out Distance2);
                    ListP0.AddRange(ListP01);
                    ListP0.AddRange(ListP02);
                    Distance.AddRange(Distance1);
                    Distance.AddRange(Distance2);

                }
                else
                {

                    List<double> Distance1 = new List<double>();
                    List<XYZ> ListP01 = new List<XYZ>();
                    curve1 = GetHorizontalQuadrangle(p0, x, y, x2, x1, x0, x2, y2, y1, y0, y2, deltaZ0, BarModel, Unit, CoverTop, CoverBottom, CoverSide, out ListP01, out Distance1);
                    List<double> Distance2 = new List<double>();
                    List<XYZ> ListP02 = new List<XYZ>();
                    curve2 = GetHorizontalQuadrangle(p0, x, y, x3, x4, x0, x2, y3, y4, y0, y2, deltaZ0, BarModel, Unit, CoverTop, CoverBottom, CoverSide, out ListP02, out Distance2);
                    ListP0.AddRange(ListP01);
                    ListP0.AddRange(ListP02);
                    Distance.AddRange(Distance1);
                    Distance.AddRange(Distance2);
                }
            }
            else
            {
                if (Horizontal)
                {
                    List<double> Distance1 = new List<double>();
                    List<XYZ> ListP01 = new List<XYZ>();
                    curve1 = GetHorizontalQuadrangle(p0, y, x, y3, y4, y0, y2, x3, x4, x0, x0, deltaZ0, BarModel, Unit, CoverTop, CoverBottom, CoverSide, out ListP01, out Distance1);
                    List<double> Distance2 = new List<double>();
                    List<XYZ> ListP02 = new List<XYZ>();
                    curve2 = GetHorizontalQuadrangle(p0, y, x, y2, y1, y0, y2, x2, x1, x0, x0, deltaZ0, BarModel, Unit, CoverTop, CoverBottom, CoverSide, out ListP02, out Distance2);
                    ListP0.AddRange(ListP01);
                    ListP0.AddRange(ListP02);
                    Distance.AddRange(Distance1);
                    Distance.AddRange(Distance2);


                }
                else
                {
                    List<double> Distance1 = new List<double>();
                    List<XYZ> ListP01 = new List<XYZ>();
                    curve1 = GetHorizontalTriangle(p0, x, y, x0, x4, x1, y0, y4, y1, deltaZ0, BarModel, Unit, CoverTop, CoverBottom, CoverSide, out ListP01, out Distance1);
                    List<double> Distance2 = new List<double>();
                    List<XYZ> ListP02 = new List<XYZ>();
                    curve2 = GetHorizontalTrapezoid(p0, x, y, x2, x3, x4, x1, y2, y3, y4, y1, deltaZ0, BarModel, Unit, CoverTop, CoverBottom, CoverSide, out ListP02, out Distance2);
                    ListP0.AddRange(ListP01);
                    ListP0.AddRange(ListP02);
                    Distance.AddRange(Distance1);
                    Distance.AddRange(Distance2);
                }
            }
            if (addVertical)
            {
                for (int i = 0; i < ListP0.Count; i++)
                {
                    curves0.Add(GetAddVerticalItem(settingModel, Unit, ListP0[i], CoverTop, CoverBottom, CoverSide));
                }
            }
            else
            {
                curves0.AddRange(curve1);
                curves0.AddRange(curve2);
            }

            return new ObservableCollection<Curve>(curves0);
        }
       
        public static ObservableCollection<Curve> GetCurvesImage2(SettingModel settingModel, FoundationModel FoundationModel, FoundationBarModel FoundationBarModel, BarModel BarModel, UnitProject Unit, double dMainBottom, double dMainTop, double dSide, double CoverTop, double CoverBottom, double CoverSide, out List<double> Distance)
        {
            Distance = new List<double>();
            ObservableCollection<Curve> curves = new ObservableCollection<Curve>();
            if (BarModel.IsModel)
            {
                switch (BarModel.Name)
                {
                    case "MainBottom": curves = GetCurvesMainItem2(settingModel, FoundationModel, FoundationBarModel, BarModel, Unit, true, false, false, dMainBottom, dSide, CoverTop, CoverBottom, CoverSide, out Distance); break;
                    case "MainTop": curves = GetCurvesMainItem2(settingModel, FoundationModel, FoundationBarModel, BarModel, Unit, false, false, false, dMainBottom, dSide, CoverTop, CoverBottom, CoverSide, out Distance); break;
                    case "MainAddHorizontal": curves = GetCurvesMainItem2(settingModel, FoundationModel, FoundationBarModel, BarModel, Unit, false, true, false, dMainBottom, dSide, CoverTop, CoverBottom, CoverSide, out Distance); break;
                    case "MainAddVertical": curves = GetCurvesMainItem2(settingModel, FoundationModel, FoundationBarModel, BarModel, Unit, true, true, true, dMainBottom, dSide, CoverTop, CoverBottom, CoverSide, out Distance); break;
                    case "SecondaryBottom": curves = GetCurvesSecondaryItem2(settingModel, FoundationModel, FoundationBarModel, BarModel, Unit, true, false, false, dMainBottom, dMainTop, dSide, CoverTop, CoverBottom, CoverSide, out Distance); break;
                    case "SecondaryTop": curves = GetCurvesSecondaryItem2(settingModel, FoundationModel, FoundationBarModel, BarModel, Unit, false, false, false, dMainBottom, dMainTop, dSide, CoverTop, CoverBottom, CoverSide, out Distance); break;
                    case "SecondaryAddHorizontal": curves = GetCurvesSecondaryItem2(settingModel, FoundationModel, FoundationBarModel, BarModel, Unit, true, true, false, dMainBottom, dMainTop, dSide, CoverTop, CoverBottom, CoverSide, out Distance); break;
                    case "SecondaryAddVertical": curves = GetCurvesSecondaryItem2(settingModel, FoundationModel, FoundationBarModel, BarModel, Unit, true, true, true, dMainBottom, dMainTop, dSide, CoverTop, CoverBottom, CoverSide, out Distance); break;
                    case "Side": curves = GetCurvesSideItem2(settingModel, FoundationModel, FoundationBarModel, BarModel, Unit, dMainBottom, CoverTop, CoverBottom, CoverSide); break;
                    default: break;
                }
            }
            return curves;
        }

        #endregion
        #region Image3
        private static ObservableCollection<Curve> GetCurvesMainItem3(SettingModel settingModel, FoundationModel FoundationModel, FoundationBarModel FoundationBarModel, BarModel BarModel, UnitProject Unit, bool bottom, bool addHorizontal, bool addVertical, double dMainBottom, double dSide, double CoverTop, double CoverBottom, double CoverSide, out List<double> Distance)
        {

            Distance = new List<double>();
            List<XYZ> ListP0 = new List<XYZ>();
            double Diameter = BarModel.Bar.Diameter;
            XYZ x = null;
            XYZ y = null;
            XYZ p0 = FoundationModel.ColumnModel.PointPosition;
            List<Curve> curves0 = new List<Curve>();
            bool Horizontal = (FoundationBarModel.SpanOrientation.Equals("Horizontal"));
            double offset = (bottom) ? (CoverSide + Diameter * 0.5) : (CoverSide + dMainBottom + dSide + Diameter * 0.5);
            List<LocationModel> Location = FoundationModel.GetOffsetBoundingFoundation3(settingModel, offset);
            double x0 = Location[0].X;
            double x1 = Location[1].X;
            double x2 = Location[2].X;
            double x3 = Location[3].X;
            double x4 = Location[4].X;
            double x5 = Location[5].X;
            double y0 = Location[0].Y;
            double y1 = Location[1].Y;
            double y2 = Location[2].Y;
            double y3 = Location[3].Y;
            double y4 = Location[4].Y;
            double y5 = Location[5].Y;
            double deltaZ0 = (bottom) ? (settingModel.HeightFoundation - CoverBottom) : (CoverTop);
            if (addHorizontal)
            {
                double delta = (settingModel.HeightFoundation - CoverBottom - CoverTop) / (BarModel.Layer + 1);
                deltaZ0 = CoverTop + delta * BarModel.Layer;
            }
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
            List<Curve> curve1 = new List<Curve>();
            List<Curve> curve2 = new List<Curve>();
            List<Curve> curve3 = new List<Curve>();
            if (FoundationModel.ColumnModel.Style.Equals("RECTANGLE") && FoundationModel.ColumnModel.b > FoundationModel.ColumnModel.h)
            {
                if (Horizontal)
                {
                    List<double> Distance1 = new List<double>();
                    List<XYZ> ListP01 = new List<XYZ>();
                    curve1 = GetHorizontalTriangle(p0, x, y, x0, x5, x1,  y0, y5, y1, deltaZ0, BarModel, Unit, CoverTop, CoverBottom, CoverSide, out ListP01, out Distance1);
                    List<double> Distance2 = new List<double>();
                    List<XYZ> ListP02 = new List<XYZ>();
                    curve2 = GetHorizontalRectangle(p0, x, y, x4, x2,x1,  x5, y4, y2, y1, y5, deltaZ0, BarModel, Unit, CoverTop, CoverBottom, CoverSide, out ListP02, out Distance2);
                    List<double> Distance3 = new List<double>();
                    List<XYZ> ListP03 = new List<XYZ>();
                    curve3 = GetHorizontalTriangle(p0, x, y, x3, x2, x4, y3, y2, y4, deltaZ0, BarModel, Unit, CoverTop, CoverBottom, CoverSide, out ListP03, out Distance3);
                    ListP0.AddRange(ListP01);
                    ListP0.AddRange(ListP02);
                    ListP0.AddRange(ListP03);
                    Distance.AddRange(Distance1);
                    Distance.AddRange(Distance2);
                    Distance.AddRange(Distance3);
                }
                else
                {
                    List<double> Distance1 = new List<double>();
                    List<XYZ> ListP01 = new List<XYZ>();
                    curve1 = GetHorizontalTrapezoid(p0, y, x, y5, y4, y3,y0, x5, x4, x3,x0, deltaZ0, BarModel, Unit, CoverTop, CoverBottom, CoverSide, out ListP01, out Distance1);
                    List<double> Distance2 = new List<double>();
                    List<XYZ> ListP02 = new List<XYZ>();
                    curve2 = GetHorizontalTrapezoid(p0, y, x, y2, y1, y0, y3, x2, x1, x0, x3, deltaZ0, BarModel, Unit, CoverTop, CoverBottom, CoverSide, out ListP02, out Distance2);
                    ListP0.AddRange(ListP01);
                    ListP0.AddRange(ListP02);
                    Distance.AddRange(Distance1);
                    Distance.AddRange(Distance2);

                }
            }
            else
            {
                if (Horizontal)
                {
                    List<double> Distance1 = new List<double>();
                    List<XYZ> ListP01 = new List<XYZ>();
                    curve1 = GetHorizontalTrapezoid(p0, x, y, x5, x4, x3,x0, y5, y4, y3,y0, deltaZ0, BarModel, Unit, CoverTop, CoverBottom, CoverSide, out ListP01, out Distance1);
                    List<double> Distance2 = new List<double>();
                    List<XYZ> ListP02 = new List<XYZ>();
                    curve2 = GetHorizontalTrapezoid(p0, x, y, x2, x1, x0, x3, y2, y1, y0, y3, deltaZ0, BarModel, Unit, CoverTop, CoverBottom, CoverSide, out ListP02, out Distance2);
                    ListP0.AddRange(ListP01);
                    ListP0.AddRange(ListP02);
                    Distance.AddRange(Distance1);
                    Distance.AddRange(Distance2);
                }
                else
                {
                    List<double> Distance1 = new List<double>();
                    List<XYZ> ListP01 = new List<XYZ>();
                    curve1 = GetHorizontalTriangle(p0, y, x, y3, y2, y4,  x3, x2, x4,  deltaZ0, BarModel, Unit, CoverTop, CoverBottom, CoverSide, out ListP01, out Distance1);
                    List<double> Distance2 = new List<double>();
                    List<XYZ> ListP02 = new List<XYZ>();
                    curve2 = GetHorizontalRectangle(p0, y, x, y1, y5, y4, y2, x1, x5, x4, x2, deltaZ0, BarModel, Unit, CoverTop, CoverBottom, CoverSide, out ListP02, out Distance2);
                    List<double> Distance3 = new List<double>();
                    List<XYZ> ListP03 = new List<XYZ>();
                    curve3 = GetHorizontalTriangle(p0, y, x, y0, y5, y1, x0, x5, x1,  deltaZ0, BarModel, Unit, CoverTop, CoverBottom, CoverSide, out ListP03, out Distance3);
                    ListP0.AddRange(ListP01);
                    ListP0.AddRange(ListP02);
                    ListP0.AddRange(ListP03);
                    Distance.AddRange(Distance1);
                    Distance.AddRange(Distance2);
                    Distance.AddRange(Distance3);
                }
            }
            if (addVertical)
            {
                for (int i = 0; i < ListP0.Count; i++)
                {
                    curves0.Add(GetAddVerticalItem(settingModel, Unit, ListP0[i], CoverTop, CoverBottom, CoverSide));
                }
            }
            else
            {
                curves0.AddRange(curve1);
                curves0.AddRange(curve2);
                curves0.AddRange(curve3);
            }

            return new ObservableCollection<Curve>(curves0);
        }
        private static ObservableCollection<Curve> GetCurvesSecondaryItem3(SettingModel settingModel, FoundationModel FoundationModel, FoundationBarModel FoundationBarModel, BarModel BarModel, UnitProject Unit, bool bottom, bool addHorizontal, bool addVertical, double dMainBottom, double dMainTop, double dSide, double CoverTop, double CoverBottom, double CoverSide, out List<double> Distance)
        {
       
            Distance = new List<double>();
            List<XYZ> ListP0 = new List<XYZ>();
            double Diameter = BarModel.Bar.Diameter;
            XYZ x = null;
            XYZ y = null;
            XYZ p0 = FoundationModel.ColumnModel.PointPosition;
            List<Curve> curves0 = new List<Curve>();
            bool Horizontal = (FoundationBarModel.SpanOrientation.Equals("Horizontal"));
            double offset = (bottom) ? (CoverSide + Diameter * 0.5) : (CoverSide + dMainBottom + dSide + Diameter * 0.5);
            List<LocationModel> Location = FoundationModel.GetOffsetBoundingFoundation3(settingModel, offset);
            double x0 = Location[0].X;
            double x1 = Location[1].X;
            double x2 = Location[2].X;
            double x3 = Location[3].X;
            double x4 = Location[4].X;
            double x5 = Location[5].X;
            double y0 = Location[0].Y;
            double y1 = Location[1].Y;
            double y2 = Location[2].Y;
            double y3 = Location[3].Y;
            double y4 = Location[4].Y;
            double y5 = Location[5].Y;
            double deltaZ0 = (bottom) ? (settingModel.HeightFoundation - CoverBottom - dMainBottom) : (CoverTop + dMainTop);
            if (addHorizontal)
            {
                double delta = (settingModel.HeightFoundation - CoverBottom - CoverTop) / (BarModel.Layer + 1);
                deltaZ0 = CoverTop + delta * BarModel.Layer;
            }
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
            List<Curve> curve1 = new List<Curve>();
            List<Curve> curve2 = new List<Curve>();
            List<Curve> curve3 = new List<Curve>();
            if (FoundationModel.ColumnModel.Style.Equals("RECTANGLE") && FoundationModel.ColumnModel.b > FoundationModel.ColumnModel.h)
            {
                if (Horizontal)
                {
                    
                    List<double> Distance1 = new List<double>();
                    List<XYZ> ListP01 = new List<XYZ>();
                    curve1 = GetHorizontalTrapezoid(p0, y, x, y5, y4, y3,y0, x5, x4,x3, x0, deltaZ0, BarModel, Unit, CoverTop, CoverBottom, CoverSide, out ListP01, out Distance1);
                    List<double> Distance2 = new List<double>();
                    List<XYZ> ListP02 = new List<XYZ>();
                    curve2 = GetHorizontalTrapezoid(p0, y, x, y2, y1, y0, y3, x2, x1, x0, x3, deltaZ0, BarModel, Unit, CoverTop, CoverBottom, CoverSide, out ListP02, out Distance2);
                    ListP0.AddRange(ListP01);
                    ListP0.AddRange(ListP02);
                    Distance.AddRange(Distance1);
                    Distance.AddRange(Distance2);

                }
                else
                {
                    
                    List<double> Distance1 = new List<double>();
                    List<XYZ> ListP01 = new List<XYZ>();
                    curve1 = GetHorizontalTriangle(p0, x, y, x0, x5, x1, y0, y5, y1, deltaZ0, BarModel, Unit, CoverTop, CoverBottom, CoverSide, out ListP01, out Distance1);
                    List<double> Distance2 = new List<double>();
                    List<XYZ> ListP02 = new List<XYZ>();
                    curve2 = GetHorizontalRectangle(p0, x, y, x4, x2, x1, x5, y4, y2, y1, y5, deltaZ0, BarModel, Unit, CoverTop, CoverBottom, CoverSide, out ListP02, out Distance2);
                    List<double> Distance3 = new List<double>();
                    List<XYZ> ListP03 = new List<XYZ>();
                    curve3= GetHorizontalTriangle(p0, x, y, x3, x2, x4, y3, y2, y4, deltaZ0, BarModel, Unit, CoverTop, CoverBottom, CoverSide, out ListP03, out Distance3);
                    ListP0.AddRange(ListP01);
                    ListP0.AddRange(ListP02);
                    ListP0.AddRange(ListP03);
                    Distance.AddRange(Distance1);
                    Distance.AddRange(Distance2);
                    Distance.AddRange(Distance3);
                }
            }
            else
            {
                if (Horizontal)
                {
                    List<double> Distance1 = new List<double>();
                    List<XYZ> ListP01 = new List<XYZ>();
                    
                    curve1 = GetHorizontalTriangle(p0, y, x, y3, y4, y2,  x3, x4, x2, deltaZ0, BarModel, Unit, CoverTop, CoverBottom, CoverSide, out ListP01, out Distance1);
                    
                    List<double> Distance2 = new List<double>();
                    List<XYZ> ListP02 = new List<XYZ>();
                    curve2 = GetHorizontalRectangle(p0, y, x, y1, y5, y4, y2, x1, x5, x4, x2, deltaZ0, BarModel, Unit, CoverTop, CoverBottom, CoverSide, out ListP02, out Distance2);
                    List<double> Distance3 = new List<double>();
                    List<XYZ> ListP03 = new List<XYZ>();
                    curve3 = GetHorizontalTriangle(p0, y, x, y0, y5, y1, x0, x5, x1, deltaZ0, BarModel, Unit, CoverTop, CoverBottom, CoverSide, out ListP03, out Distance3);
                    ListP0.AddRange(ListP01);
                    ListP0.AddRange(ListP02);
                    ListP0.AddRange(ListP03);
                    Distance.AddRange(Distance1);
                    Distance.AddRange(Distance2);
                    Distance.AddRange(Distance3);


                }
                else
                {
                    List<double> Distance1 = new List<double>();
                    List<XYZ> ListP01 = new List<XYZ>();
                    curve1 = GetHorizontalTrapezoid(p0, x, y, x5, x4, x3,x0, y5, y4,y3, y0, deltaZ0, BarModel, Unit, CoverTop, CoverBottom, CoverSide, out ListP01, out Distance1);
                    List<double> Distance2 = new List<double>();
                    List<XYZ> ListP02 = new List<XYZ>();
                    curve2 = GetHorizontalTrapezoid(p0, x, y, x2, x1, x0, x3, y2, y1, y0, y3, deltaZ0, BarModel, Unit, CoverTop, CoverBottom, CoverSide, out ListP02, out Distance2);
                    ListP0.AddRange(ListP01);
                    ListP0.AddRange(ListP02);
                    Distance.AddRange(Distance1);
                    Distance.AddRange(Distance2);
                }
            }
            if (addVertical)
            {
                for (int i = 0; i < ListP0.Count; i++)
                {
                    curves0.Add(GetAddVerticalItem(settingModel, Unit, ListP0[i], CoverTop, CoverBottom, CoverSide));
                }
            }
            else
            {
                curves0.AddRange(curve1);
                curves0.AddRange(curve2);
                curves0.AddRange(curve3);
            }

            return new ObservableCollection<Curve>(curves0);
        }

        public static ObservableCollection<Curve> GetCurvesImage3(SettingModel settingModel, FoundationModel FoundationModel, FoundationBarModel FoundationBarModel, BarModel BarModel, UnitProject Unit, double dMainBottom, double dMainTop, double dSide, double CoverTop, double CoverBottom, double CoverSide, out List<double> Distance)
        {
            Distance = new List<double>();
            ObservableCollection<Curve> curves = new ObservableCollection<Curve>();
            if (BarModel.IsModel)
            {
                switch (BarModel.Name)
                {
                    case "MainBottom": curves = GetCurvesMainItem3(settingModel, FoundationModel, FoundationBarModel, BarModel, Unit, true, false, false, dMainBottom, dSide, CoverTop, CoverBottom, CoverSide, out Distance); break;
                    case "MainTop": curves = GetCurvesMainItem3(settingModel, FoundationModel, FoundationBarModel, BarModel, Unit, false, false, false, dMainBottom, dSide, CoverTop, CoverBottom, CoverSide, out Distance); break;
                    case "MainAddHorizontal": curves = GetCurvesMainItem3(settingModel, FoundationModel, FoundationBarModel, BarModel, Unit, false, true, false, dMainBottom, dSide, CoverTop, CoverBottom, CoverSide, out Distance); break;
                    case "MainAddVertical": curves = GetCurvesMainItem3(settingModel, FoundationModel, FoundationBarModel, BarModel, Unit, true, true, true, dMainBottom, dSide, CoverTop, CoverBottom, CoverSide, out Distance); break;
                    case "SecondaryBottom": curves = GetCurvesSecondaryItem3(settingModel, FoundationModel, FoundationBarModel, BarModel, Unit, true, false, false, dMainBottom, dMainTop, dSide, CoverTop, CoverBottom, CoverSide, out Distance); break;
                    case "SecondaryTop": curves = GetCurvesSecondaryItem2(settingModel, FoundationModel, FoundationBarModel, BarModel, Unit, false, false, false, dMainBottom, dMainTop, dSide, CoverTop, CoverBottom, CoverSide, out Distance); break;
                    case "SecondaryAddHorizontal": curves = GetCurvesSecondaryItem3(settingModel, FoundationModel, FoundationBarModel, BarModel, Unit, true, true, false, dMainBottom, dMainTop, dSide, CoverTop, CoverBottom, CoverSide, out Distance); break;
                    case "SecondaryAddVertical": curves = GetCurvesSecondaryItem3(settingModel, FoundationModel, FoundationBarModel, BarModel, Unit, true, true, true, dMainBottom, dMainTop, dSide, CoverTop, CoverBottom, CoverSide, out Distance); break;
                    case "Side": curves = GetCurvesSideItem3(settingModel, FoundationModel, FoundationBarModel, BarModel, Unit, dMainBottom, CoverTop, CoverBottom, CoverSide); break;
                    default: break;
                }
            }
            return curves;
        }

        #endregion
        #region Image3
        private static ObservableCollection<Curve> GetCurvesMainItem0(SettingModel settingModel, FoundationModel FoundationModel, FoundationBarModel FoundationBarModel, BarModel BarModel, UnitProject Unit, bool bottom, bool addHorizontal, bool addVertical, double dMainBottom, double dSide, double CoverTop, double CoverBottom, double CoverSide, out List<double> Distance)
        {

            Distance = new List<double>();
            List<XYZ> ListP0 = new List<XYZ>();
            double Diameter = BarModel.Bar.Diameter;
            XYZ x = null;
            XYZ y = null;
            XYZ p0 = FoundationModel.ColumnModel.PointPosition;
            List<Curve> curves0 = new List<Curve>();
            bool Horizontal = (FoundationBarModel.SpanOrientation.Equals("Horizontal"));
            double offset = (bottom) ? (CoverSide + Diameter * 0.5) : (CoverSide + dMainBottom + dSide + Diameter * 0.5);
            List<LocationModel> Location = FoundationModel.GetOffsetBoundingFoundation0(settingModel, offset);
            double x0 = Location[0].X;
            double x1 = Location[1].X;
            double x2 = Location[2].X;
            double x3 = Location[3].X;
            double x4 = Location[4].X;
            double x5 = Location[5].X;
            double y0 = Location[0].Y;
            double y1 = Location[1].Y;
            double y2 = Location[2].Y;
            double y3 = Location[3].Y;
            double y4 = Location[4].Y;
            double y5 = Location[5].Y;
            double deltaZ0 = (bottom) ? (settingModel.HeightFoundation - CoverBottom) : (CoverTop);
            if (addHorizontal)
            {
                double delta = (settingModel.HeightFoundation - CoverBottom - CoverTop) / (BarModel.Layer + 1);
                deltaZ0 = CoverTop + delta * BarModel.Layer;
            }
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
            List<Curve> curve1 = new List<Curve>();
            List<Curve> curve2 = new List<Curve>();
            List<Curve> curve3 = new List<Curve>();
            List<Curve> curve4 = new List<Curve>();
            if (FoundationModel.ColumnModel.Style.Equals("RECTANGLE") && FoundationModel.ColumnModel.b > FoundationModel.ColumnModel.h)
            {
                if (Horizontal)
                {
                    double xc05 = x0, yc05 = 0, xc23 = 2, yc23 = 0;
                    double x5c = x0, y5c = y3;
                    double x0c = x0, y0c = y2;
                    List<double> Distance1 = new List<double>();
                    List<XYZ> ListP01 = new List<XYZ>();
                  
                    curve1 = GetHorizontalTrapezoid(p0, x, y, x1, x0, x0c,x2, y1, y0, y0c,y2, deltaZ0, BarModel, Unit, CoverTop, CoverBottom, CoverSide, out ListP01, out Distance1);
                    
                    List<double> Distance2 = new List<double>();
                    List<XYZ> ListP02 = new List<XYZ>();
                    curve2 = GetHorizontalRectangle(p0, x, y, xc05, xc23, x2, x0c, yc05, yc23, y2, y0c, deltaZ0, BarModel, Unit, CoverTop, CoverBottom, CoverSide, out ListP02, out Distance2);
                    List<double> Distance3 = new List<double>();
                    List<XYZ> ListP03 = new List<XYZ>();
                    curve3 = GetHorizontalRectangle(p0, x, y, xc23, xc05, x5c,x3, yc23, yc05, y5c,y3, deltaZ0, BarModel, Unit, CoverTop, CoverBottom, CoverSide, out ListP03, out Distance3);
                    List<double> Distance4 = new List<double>();
                    List<XYZ> ListP04 = new List<XYZ>();
                    curve4 = GetHorizontalTrapezoid(p0, x, y, x5, x4, x3, x5c, y5, y4, y3, y5c, deltaZ0, BarModel, Unit, CoverTop, CoverBottom, CoverSide, out ListP04, out Distance4);
                    ListP0.AddRange(ListP01);
                    ListP0.AddRange(ListP02);
                    ListP0.AddRange(ListP03);
                    ListP0.AddRange(ListP04);
                    Distance.AddRange(Distance1);
                    Distance.AddRange(Distance2);
                    Distance.AddRange(Distance3);
                    Distance.AddRange(Distance4);
                }
                else
                {
                    List<double> Distance1 = new List<double>();
                    List<XYZ> ListP01 = new List<XYZ>();
                    curve1 = GetHorizontalRectangle(p0, y, x, y0, y5, y4, y1, x0, x5, x4, x1, deltaZ0, BarModel, Unit, CoverTop, CoverBottom, CoverSide, out ListP01, out Distance1);
                    List<double> Distance2 = new List<double>();
                    List<XYZ> ListP02 = new List<XYZ>();
                    curve2 = GetHorizontalTrapezoid(p0, y, x, y3, y2, y1, y4, x3, x2, x1, x4, deltaZ0, BarModel, Unit, CoverTop, CoverBottom, CoverSide, out ListP02, out Distance2);
                    ListP0.AddRange(ListP01);
                    ListP0.AddRange(ListP02);
                    Distance.AddRange(Distance1);
                    Distance.AddRange(Distance2);

                }
            }
            else
            {
                if (Horizontal)
                {
                    List<double> Distance1 = new List<double>();
                    List<XYZ> ListP01 = new List<XYZ>();
                    curve1 = GetHorizontalRectangle(p0, x, y, x0, x5, x4, x1, y0, y5, y4, y1, deltaZ0, BarModel, Unit, CoverTop, CoverBottom, CoverSide, out ListP01, out Distance1);
                    List<double> Distance2 = new List<double>();
                    List<XYZ> ListP02 = new List<XYZ>();
                    curve2 = GetHorizontalTrapezoid(p0, x, y, x3, x2, x1, x4, y3, y2, y1, y4, deltaZ0, BarModel, Unit, CoverTop, CoverBottom, CoverSide, out ListP02, out Distance2);
                    ListP0.AddRange(ListP01);
                    ListP0.AddRange(ListP02);
                    Distance.AddRange(Distance1);
                    Distance.AddRange(Distance2);
                }
                else
                {
                    double xc05 = 0, yc05 = y0, xc23 = 0, yc23 = y2;
                    double x5c = x3, y5c = y5;
                    double x0c = x2, y0c = y0;
                    List<double> Distance1 = new List<double>();
                    List<XYZ> ListP01 = new List<XYZ>();
                    curve1 = GetHorizontalTrapezoid(p0, y, x, y5, y4, y3,y5c, x5, x4, x3,x5c, deltaZ0, BarModel, Unit, CoverTop, CoverBottom, CoverSide, out ListP01, out Distance1);
                    List<double> Distance2 = new List<double>();
                    List<XYZ> ListP02 = new List<XYZ>();
                    curve2 = GetHorizontalRectangle(p0, y, x, yc23, yc05, y5c, y3, xc23, xc05, x5c, x3, deltaZ0, BarModel, Unit, CoverTop, CoverBottom, CoverSide, out ListP02, out Distance2);
                    List<double> Distance3 = new List<double>();
                    List<XYZ> ListP03 = new List<XYZ>();
                    curve3 = GetHorizontalRectangle(p0, y, x, yc05, yc23, y2,y0c, xc05, xc23, x2,x0c, deltaZ0, BarModel, Unit, CoverTop, CoverBottom, CoverSide, out ListP03, out Distance3);
                    List<double> Distance4 = new List<double>();
                    List<XYZ> ListP04 = new List<XYZ>();
                    curve4 = GetHorizontalTrapezoid(p0, y, x, y0, y1, y2, yc05, x0, x1, x2, xc05, deltaZ0, BarModel, Unit, CoverTop, CoverBottom, CoverSide, out ListP04, out Distance4);
                    ListP0.AddRange(ListP01);
                    ListP0.AddRange(ListP02);
                    ListP0.AddRange(ListP03);
                    ListP0.AddRange(ListP04);
                    Distance.AddRange(Distance1);
                    Distance.AddRange(Distance2);
                    Distance.AddRange(Distance3);
                    Distance.AddRange(Distance4);
                }
            }
            if (addVertical)
            {
                for (int i = 0; i < ListP0.Count; i++)
                {
                    curves0.Add(GetAddVerticalItem(settingModel, Unit, ListP0[i], CoverTop, CoverBottom, CoverSide));
                }
            }
            else
            {
                curves0.AddRange(curve1);
                curves0.AddRange(curve2);
                curves0.AddRange(curve3);
                curves0.AddRange(curve4);
            }

            return new ObservableCollection<Curve>(curves0);
        }
        private static ObservableCollection<Curve> GetCurvesSecondaryItem0(SettingModel settingModel, FoundationModel FoundationModel, FoundationBarModel FoundationBarModel, BarModel BarModel, UnitProject Unit, bool bottom, bool addHorizontal, bool addVertical, double dMainBottom, double dMainTop, double dSide, double CoverTop, double CoverBottom, double CoverSide, out List<double> Distance)
        {

            Distance = new List<double>();
            List<XYZ> ListP0 = new List<XYZ>();
            double Diameter = BarModel.Bar.Diameter;
            XYZ x = null;
            XYZ y = null;
            XYZ p0 = FoundationModel.ColumnModel.PointPosition;
            List<Curve> curves0 = new List<Curve>();
            bool Horizontal = (FoundationBarModel.SpanOrientation.Equals("Horizontal"));
            double offset = (bottom) ? (CoverSide + Diameter * 0.5) : (CoverSide + dMainBottom + dSide + Diameter * 0.5);
            List<LocationModel> Location = FoundationModel.GetOffsetBoundingFoundation0(settingModel, offset);
            double x0 = Location[0].X;
            double x1 = Location[1].X;
            double x2 = Location[2].X;
            double x3 = Location[3].X;
            double x4 = Location[4].X;
            double x5 = Location[5].X;
            double y0 = Location[0].Y;
            double y1 = Location[1].Y;
            double y2 = Location[2].Y;
            double y3 = Location[3].Y;
            double y4 = Location[4].Y;
            double y5 = Location[5].Y;
            double deltaZ0 = (bottom) ? (settingModel.HeightFoundation - CoverBottom - dMainBottom) : (CoverTop + dMainTop);
            if (addHorizontal)
            {
                double delta = (settingModel.HeightFoundation - CoverBottom - CoverTop) / (BarModel.Layer + 1);
                deltaZ0 = CoverTop + delta * BarModel.Layer;
            }
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
            List<Curve> curve1 = new List<Curve>();
            List<Curve> curve2 = new List<Curve>();
            List<Curve> curve3 = new List<Curve>();
            List<Curve> curve4 = new List<Curve>();
            if (FoundationModel.ColumnModel.Style.Equals("RECTANGLE") && FoundationModel.ColumnModel.b > FoundationModel.ColumnModel.h)
            {
                if (Horizontal)
                {

                    List<double> Distance1 = new List<double>();
                    List<XYZ> ListP01 = new List<XYZ>();
                    curve1 = GetHorizontalRectangle(p0, y, x, y0, y5, y4, y1, x0, x5, x4, x1, deltaZ0, BarModel, Unit, CoverTop, CoverBottom, CoverSide, out ListP01, out Distance1);
                    List<double> Distance2 = new List<double>();
                    List<XYZ> ListP02 = new List<XYZ>();
                    curve2 = GetHorizontalTrapezoid(p0, y, x, y3, y2, y1, y4, x3, x2, x1, x4, deltaZ0, BarModel, Unit, CoverTop, CoverBottom, CoverSide, out ListP02, out Distance2);
                    ListP0.AddRange(ListP01);
                    ListP0.AddRange(ListP02);
                    Distance.AddRange(Distance1);
                    Distance.AddRange(Distance2);

                }
                else
                {
                    double xc05 = x0, yc05 = 0, xc23 = 2, yc23 = 0;
                    double x5c = x0, y5c = y3;
                    double x0c = x0, y0c = y2;
                    List<double> Distance1 = new List<double>();
                    List<XYZ> ListP01 = new List<XYZ>();
                    curve1 = GetHorizontalTrapezoid(p0, x, y, x0, x1, x2, x0c, y0, y1, y2, y0c, deltaZ0, BarModel, Unit, CoverTop, CoverBottom, CoverSide, out ListP01, out Distance1);
                    List<double> Distance2 = new List<double>();
                    List<XYZ> ListP02 = new List<XYZ>();
                    curve2 = GetHorizontalRectangle(p0, x, y, xc05, xc23, x2, x0c, yc05, yc23, y2, y0c, deltaZ0, BarModel, Unit, CoverTop, CoverBottom, CoverSide, out ListP02, out Distance2);
                    List<double> Distance3 = new List<double>();
                    List<XYZ> ListP03 = new List<XYZ>();
                    curve3 = GetHorizontalRectangle(p0, x, y, xc23, xc05, x5c, x3, yc23, yc05, y5c, y3, deltaZ0, BarModel, Unit, CoverTop, CoverBottom, CoverSide, out ListP03, out Distance3);
                    List<double> Distance4 = new List<double>();
                    List<XYZ> ListP04 = new List<XYZ>();
                    curve4 = GetHorizontalTrapezoid(p0, x, y, x5, x4, x3, x5c, y5, y4, y3, y5c, deltaZ0, BarModel, Unit, CoverTop, CoverBottom, CoverSide, out ListP04, out Distance4);
                    ListP0.AddRange(ListP01);
                    ListP0.AddRange(ListP02);
                    ListP0.AddRange(ListP03);
                    ListP0.AddRange(ListP04);
                    Distance.AddRange(Distance1);
                    Distance.AddRange(Distance2);
                    Distance.AddRange(Distance3);
                    Distance.AddRange(Distance4);
                }
            }
            else
            {
                if (Horizontal)
                {
                    double xc05 = 0, yc05 = y0, xc23 = 0, yc23 = y2;
                    double x5c = x3, y5c = y5;
                    double x0c = x2, y0c = y0;
                    List<double> Distance1 = new List<double>();
                    List<XYZ> ListP01 = new List<XYZ>();
                    curve1 = GetHorizontalTrapezoid(p0, y, x, y5, y4, y3, y5c, x5, x4, x3, x5c, deltaZ0, BarModel, Unit, CoverTop, CoverBottom, CoverSide, out ListP01, out Distance1);
                    List<double> Distance2 = new List<double>();
                    List<XYZ> ListP02 = new List<XYZ>();
                    curve2 = GetHorizontalRectangle(p0, y, x, yc23, yc05, y5c, y3, xc23, xc05, x5c, x3, deltaZ0, BarModel, Unit, CoverTop, CoverBottom, CoverSide, out ListP02, out Distance2);
                    List<double> Distance3 = new List<double>();
                    List<XYZ> ListP03 = new List<XYZ>();
                    curve3 = GetHorizontalRectangle(p0, y, x, yc05, yc23, y2, y0c, xc05, xc23, x2, x0c, deltaZ0, BarModel, Unit, CoverTop, CoverBottom, CoverSide, out ListP03, out Distance3);
                    List<double> Distance4 = new List<double>();
                    List<XYZ> ListP04 = new List<XYZ>();
                    curve4 = GetHorizontalTrapezoid(p0, y, x, y1, y0, y0c, y2, x1, x0, x0c, x2, deltaZ0, BarModel, Unit, CoverTop, CoverBottom, CoverSide, out ListP04, out Distance4);
                    ListP0.AddRange(ListP01);
                    ListP0.AddRange(ListP02);
                    ListP0.AddRange(ListP03);
                    ListP0.AddRange(ListP04);
                    Distance.AddRange(Distance1);
                    Distance.AddRange(Distance2);
                    Distance.AddRange(Distance3);
                    Distance.AddRange(Distance4);


                }
                else
                {
                    List<double> Distance1 = new List<double>();
                    List<XYZ> ListP01 = new List<XYZ>();
                    curve1 = GetHorizontalRectangle(p0, x, y, x0, x5, x4, x1, y0, y5, y4, y1, deltaZ0, BarModel, Unit, CoverTop, CoverBottom, CoverSide, out ListP01, out Distance1);
                    List<double> Distance2 = new List<double>();
                    List<XYZ> ListP02 = new List<XYZ>();
                    curve2 = GetHorizontalTrapezoid(p0, x, y, x3, x2, x1, x4, y3, y2, y1, y4, deltaZ0, BarModel, Unit, CoverTop, CoverBottom, CoverSide, out ListP02, out Distance2);
                    ListP0.AddRange(ListP01);
                    ListP0.AddRange(ListP02);
                    Distance.AddRange(Distance1);
                    Distance.AddRange(Distance2);
                }
            }
            if (addVertical)
            {
                for (int i = 0; i < ListP0.Count; i++)
                {
                    curves0.Add(GetAddVerticalItem(settingModel, Unit, ListP0[i], CoverTop, CoverBottom, CoverSide));
                }
            }
            else
            {
                curves0.AddRange(curve1);
                curves0.AddRange(curve2);
                curves0.AddRange(curve3);
                curves0.AddRange(curve4);
            }

            return new ObservableCollection<Curve>(curves0);
        }

        public static ObservableCollection<Curve> GetCurvesImage0A(SettingModel settingModel, FoundationModel FoundationModel, FoundationBarModel FoundationBarModel, BarModel BarModel, UnitProject Unit, double dMainBottom, double dMainTop, double dSide, double CoverTop, double CoverBottom, double CoverSide, out List<double> Distance)
        {
            Distance = new List<double>();
            ObservableCollection<Curve> curves = new ObservableCollection<Curve>();
            if (BarModel.IsModel)
            {
                switch (BarModel.Name)
                {
                    case "MainBottom": curves = GetCurvesMainItem0(settingModel, FoundationModel, FoundationBarModel, BarModel, Unit, true, false, false, dMainBottom, dSide, CoverTop, CoverBottom, CoverSide, out Distance); break;
                    case "MainTop": curves = GetCurvesMainItem0(settingModel, FoundationModel, FoundationBarModel, BarModel, Unit, false, false, false, dMainBottom, dSide, CoverTop, CoverBottom, CoverSide, out Distance); break;
                    case "MainAddHorizontal": curves = GetCurvesMainItem0(settingModel, FoundationModel, FoundationBarModel, BarModel, Unit, false, true, false, dMainBottom, dSide, CoverTop, CoverBottom, CoverSide, out Distance); break;
                    case "MainAddVertical": curves = GetCurvesMainItem0(settingModel, FoundationModel, FoundationBarModel, BarModel, Unit, true, true, true, dMainBottom, dSide, CoverTop, CoverBottom, CoverSide, out Distance); break;
                    case "SecondaryBottom": curves = GetCurvesSecondaryItem0(settingModel, FoundationModel, FoundationBarModel, BarModel, Unit, true, false, false, dMainBottom, dMainTop, dSide, CoverTop, CoverBottom, CoverSide, out Distance); break;
                    case "SecondaryTop": curves = GetCurvesSecondaryItem0(settingModel, FoundationModel, FoundationBarModel, BarModel, Unit, false, false, false, dMainBottom, dMainTop, dSide, CoverTop, CoverBottom, CoverSide, out Distance); break;
                    case "SecondaryAddHorizontal": curves = GetCurvesSecondaryItem0(settingModel, FoundationModel, FoundationBarModel, BarModel, Unit, true, true, false, dMainBottom, dMainTop, dSide, CoverTop, CoverBottom, CoverSide, out Distance); break;
                    case "SecondaryAddVertical": curves = GetCurvesSecondaryItem0(settingModel, FoundationModel, FoundationBarModel, BarModel, Unit, true, true, true, dMainBottom, dMainTop, dSide, CoverTop, CoverBottom, CoverSide, out Distance); break;
                    case "Side": curves = GetCurvesSideItem0(settingModel, FoundationModel, FoundationBarModel, BarModel, Unit, dMainBottom, CoverTop, CoverBottom, CoverSide); break;
                    default: break;
                }
            }
            return curves;
        }

        #endregion
        #region Side
        private static ObservableCollection<Curve> GetCurvesSide(SettingModel settingModel, FoundationModel FoundationModel, FoundationBarModel FoundationBarModel, BarModel BarModel, UnitProject Unit, double dMainBottom, double CoverTop, double CoverBottom, double CoverSide)
        {
            double Diameter = BarModel.Bar.Diameter;
            XYZ x = null;
            XYZ y = null;
            XYZ p0 = FoundationModel.ColumnModel.PointPosition;
            ObservableCollection<Curve> curves = new ObservableCollection<Curve>();
            double delta = (settingModel.HeightFoundation - CoverBottom - CoverTop) / (BarModel.Layer + 1);
            double deltaZ0 = CoverTop + delta * BarModel.Layer;
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
                LocationModel l1, l2 = null;
                if (i == 0)
                {
                    l1 = FoundationModel.BoundingLocation[FoundationModel.BoundingLocation.Count - 1];
                    l2 = FoundationModel.BoundingLocation[0];
                }
                else
                {
                    l1 = FoundationModel.BoundingLocation[i - 1];
                    l2 = FoundationModel.BoundingLocation[i];
                }
                XYZ p1 = p0 + Unit.Convert(l1.X + ((l1.X > 0) ? (-1) : (1)) * (delta1)) * x + Unit.Convert(l1.Y + ((l1.Y > 0) ? (-1) : (1)) * (delta1)) * y + Unit.Convert(-deltaZ0) * XYZ.BasisZ;

                XYZ p2 = p0 + Unit.Convert(l2.X + ((l2.X > 0) ? (-1) : (1)) * (delta1)) * x + Unit.Convert(l2.Y + ((l2.Y > 0) ? (-1) : (1)) * (delta1)) * y + Unit.Convert(-deltaZ0) * XYZ.BasisZ;
                curves.Add(Line.CreateBound(p1, p2));
            }

            return curves;
        }
        private static ObservableCollection<Curve> GetCurvesSideItem2(SettingModel settingModel, FoundationModel FoundationModel, FoundationBarModel FoundationBarModel, BarModel BarModel, UnitProject Unit, double dMainBottom, double CoverTop, double CoverBottom, double CoverSide)
        {
            double Diameter = BarModel.Bar.Diameter;
            XYZ x = null;
            XYZ y = null;
            XYZ p0 = FoundationModel.ColumnModel.PointPosition;
            double offset = (CoverSide + Diameter * 0.5);
            ObservableCollection<Curve> curves = new ObservableCollection<Curve>();
            List<LocationModel> Location = FoundationModel.GetOffsetBoundingFoundation2(settingModel, offset);
            double delta = (settingModel.HeightFoundation - CoverBottom - CoverTop) / (BarModel.Layer + 1);
            double deltaZ0 = CoverTop + delta * BarModel.Layer;
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

            for (int i = 0; i < Location.Count; i++)
            {
                LocationModel l1, l2 = null;
                if (i == 0)
                {
                    l1 = Location[Location.Count - 1];
                    l2 = Location[0];
                }
                else
                {
                    l1 = Location[i - 1];
                    l2 = Location[i];
                }
                XYZ p1 = p0 + Unit.Convert(l1.X) * x + Unit.Convert(l1.Y) * y + Unit.Convert(-deltaZ0) * XYZ.BasisZ;

                XYZ p2 = p0 + Unit.Convert(l2.X) * x + Unit.Convert(l2.Y) * y + Unit.Convert(-deltaZ0) * XYZ.BasisZ;
                curves.Add(Line.CreateBound(p1, p2));
            }

            return curves;
        }
        private static ObservableCollection<Curve> GetCurvesSideItem3(SettingModel settingModel, FoundationModel FoundationModel, FoundationBarModel FoundationBarModel, BarModel BarModel, UnitProject Unit, double dMainBottom, double CoverTop, double CoverBottom, double CoverSide)
        {
            double Diameter = BarModel.Bar.Diameter;
            XYZ x = null;
            XYZ y = null;
            XYZ p0 = FoundationModel.ColumnModel.PointPosition;
            double offset = (CoverSide + Diameter * 0.5);
            ObservableCollection<Curve> curves = new ObservableCollection<Curve>();
            List<LocationModel> Location = FoundationModel.GetOffsetBoundingFoundation3(settingModel, offset);
            double delta = (settingModel.HeightFoundation - CoverBottom - CoverTop) / (BarModel.Layer + 1);
            double deltaZ0 = CoverTop + delta * BarModel.Layer;
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

            for (int i = 0; i < Location.Count; i++)
            {
                LocationModel l1, l2 = null;
                if (i == 0)
                {
                    l1 = Location[Location.Count - 1];
                    l2 = Location[0];
                }
                else
                {
                    l1 = Location[i - 1];
                    l2 = Location[i];
                }
                XYZ p1 = p0 + Unit.Convert(l1.X) * x + Unit.Convert(l1.Y) * y + Unit.Convert(-deltaZ0) * XYZ.BasisZ;

                XYZ p2 = p0 + Unit.Convert(l2.X) * x + Unit.Convert(l2.Y) * y + Unit.Convert(-deltaZ0) * XYZ.BasisZ;
                curves.Add(Line.CreateBound(p1, p2));
            }

            return curves;
        }
        private static ObservableCollection<Curve> GetCurvesSideItem0(SettingModel settingModel, FoundationModel FoundationModel, FoundationBarModel FoundationBarModel, BarModel BarModel, UnitProject Unit, double dMainBottom, double CoverTop, double CoverBottom, double CoverSide)
        {
            double Diameter = BarModel.Bar.Diameter;
            XYZ x = null;
            XYZ y = null;
            XYZ p0 = FoundationModel.ColumnModel.PointPosition;
            double offset = (CoverSide + Diameter * 0.5);
            ObservableCollection<Curve> curves = new ObservableCollection<Curve>();
            List<LocationModel> Location = FoundationModel.GetOffsetBoundingFoundation0(settingModel, offset);
            double delta = (settingModel.HeightFoundation - CoverBottom - CoverTop) / (BarModel.Layer + 1);
            double deltaZ0 = CoverTop + delta * BarModel.Layer;
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

            for (int i = 0; i < Location.Count; i++)
            {
                LocationModel l1, l2 = null;
                if (i == 0)
                {
                    l1 = Location[Location.Count - 1];
                    l2 = Location[0];
                }
                else
                {
                    l1 = Location[i - 1];
                    l2 = Location[i];
                }
                XYZ p1 = p0 + Unit.Convert(l1.X) * x + Unit.Convert(l1.Y) * y + Unit.Convert(-deltaZ0) * XYZ.BasisZ;

                XYZ p2 = p0 + Unit.Convert(l2.X) * x + Unit.Convert(l2.Y) * y + Unit.Convert(-deltaZ0) * XYZ.BasisZ;
                curves.Add(Line.CreateBound(p1, p2));
            }

            return curves;
        }
        #endregion
        #region AddVertical
        private static Curve GetAddVerticalItem(SettingModel settingModel, UnitProject Unit, XYZ p0, double CoverTop, double CoverBottom, double CoverSide)
        {
            double deltaZ2 = settingModel.HeightFoundation - CoverBottom;
            double deltaZ1 = CoverTop;
            XYZ p1 = p0 + Unit.Convert(-deltaZ1) * XYZ.BasisZ;
            XYZ p2 = p0 + Unit.Convert(-deltaZ2) * XYZ.BasisZ;
            return Line.CreateBound(p1, p2);
        }
        #endregion
        #region Item
        private static List<Curve> GetHorizontalTriangle(XYZ p0, XYZ x, XYZ y, double x1, double x2, double x3, double y1, double y2, double y3, double z0, BarModel BarModel, UnitProject Unit, double CoverTop, double CoverBottom, double CoverSide, out List<XYZ> ListP0, out List<double> Distance)
        {
            double deltaBar = BarModel.Bar.RebarBarType.get_Parameter(BuiltInParameter.REBAR_STANDARD_BEND_DIAMETER).AsDouble();
            Distance = new List<double>();
            ListP0 = new List<XYZ>();
            double Diameter = BarModel.Bar.Diameter;
            List<Curve> curves = new List<Curve>();
            double total = Math.Abs(y1 - y2) - BarModel.Distance * 0.5;
            int number = (int)((total) / BarModel.Distance) + 1;
            double tanA2 = (Math.Abs(y1 - y2)) / (Math.Abs(x1 - x2));
            double tanA3 = (Math.Abs(y1 - y3)) / (Math.Abs(x1 - x3));
            for (int i = 0; i < number; i++)
            {
                double yu0 = (y2 + ((y2 > 0) ? ((y1 > y2) ? 1 : -1) : ((y1 < y2) ? -1 : 1)) * (BarModel.Distance * 0.5 + i * BarModel.Distance));
                double xu2 = x2 + ((x2 > 0) ? ((x1 < x2) ? -1 : 1) : ((x1 > x2) ? 1 : -1)) * ((BarModel.Distance * 0.5 + i * BarModel.Distance) / tanA2);
                double xu3 = x3 + ((x3 > 0) ? ((x1 < x3) ? -1 : 1) : ((x1 > x3) ? 1 : -1)) * ((BarModel.Distance * 0.5 + i * BarModel.Distance) / tanA3);
                XYZ p1 = p0 + Unit.Convert(xu2) * x + Unit.Convert(yu0) * y + Unit.Convert(-z0) * XYZ.BasisZ;
                XYZ p2 = p0 + Unit.Convert(xu3) * x + Unit.Convert(yu0) * y + Unit.Convert(-z0) * XYZ.BasisZ;
                if (p1.DistanceTo(p2)>deltaBar)
                {
                   
                    curves.Add(Line.CreateBound(p1, p2));
                    ListP0.Add(p0 + Unit.Convert((xu2 < xu3) ? (xu2) : (xu3)) * x + Unit.Convert(yu0) * y);
                    Distance.Add(Math.Abs(xu2 - xu3) / (BarModel.Layer + 1));
                }
              
            }
            return curves;
        }
        private static List<Curve> GetHorizontalTrapezoid(XYZ p0, XYZ x, XYZ y, double x1, double x2, double x3, double x4, double y1, double y2, double y3, double y4, double z0, BarModel BarModel, UnitProject Unit, double CoverTop, double CoverBottom, double CoverSide, out List<XYZ> ListP0, out List<double> Distance)
        {
            double deltaBar = BarModel.Bar.RebarBarType.get_Parameter(BuiltInParameter.REBAR_STANDARD_BEND_DIAMETER).AsDouble();
            Distance = new List<double>();
            ListP0 = new List<XYZ>();
            double Diameter = BarModel.Bar.Diameter;
            List<Curve> curves = new List<Curve>();
            double total = Math.Abs(y1 - y4) - BarModel.Distance * 0.5 - CoverSide - Diameter * 0.5;
            int number = (int)((total) / BarModel.Distance) + 1;
            if (PointModel.AreEqual(x2, x3))
            {
                double tanA14 = (Math.Abs(y1 - y4)) / (Math.Abs(x1 - x4));
                for (int i = 0; i < number; i++)
                {
                    double yu0 = (y3 + ((y3 > 0) ? ((y2 > y3) ? 1 : -1) : ((y2 < y3) ? -1 : 1)) * (BarModel.Distance * 0.5 + i * BarModel.Distance));
                    double xu2 = x2;
                    double xu4 =x4+ ((x4 > 0) ? ((x1 < x4) ? -1 : 1) : ((x1 > x4) ? 1 : -1)) * ((BarModel.Distance * 0.5 + i * BarModel.Distance) / tanA14);
                    XYZ p1 = p0 + Unit.Convert(xu4) * x + Unit.Convert(yu0) * y + Unit.Convert(-z0) * XYZ.BasisZ;
                    XYZ p2 = p0 + Unit.Convert(xu2) * x + Unit.Convert(yu0) * y + Unit.Convert(-z0) * XYZ.BasisZ;
                    Curve a = Line.CreateBound(p1, p2);
                    if (a.Length > deltaBar)
                    {
                        curves.Add(a);
                        ListP0.Add(p0 + Unit.Convert((xu2 < xu4) ? (xu2) : (xu4)) * x + Unit.Convert(yu0) * y);
                        Distance.Add(Math.Abs(xu4 - xu2) / (BarModel.Layer + 1));
                    }
                  
                }
            }
            else
            {
                if (PointModel.AreEqual(x1, x4))
                {
                    double tanA23 = (Math.Abs(y2 - y3)) / (Math.Abs(x2 - x3));
                    for (int i = 0; i < number; i++)
                    {
                        double yu0 = (y4 + ((y4 > 0) ? ((y1 > y4) ? 1 : -1) : ((y1 < y4) ? -1 : 1)) * (BarModel.Distance * 0.5 + i * BarModel.Distance));
                        double xu1 = x1;
                        double xu3 = x3 + ((x3 > 0) ? ((x1 < x3) ? -1 : 1) : ((x1 > x3) ? 1 : -1)) * ((BarModel.Distance * 0.5 + i * BarModel.Distance) / tanA23);
                        XYZ p1 = p0 + Unit.Convert(xu1) * x + Unit.Convert(yu0) * y + Unit.Convert(-z0) * XYZ.BasisZ;
                        XYZ p2 = p0 + Unit.Convert(xu3) * x + Unit.Convert(yu0) * y + Unit.Convert(-z0) * XYZ.BasisZ;
                        curves.Add(Line.CreateBound(p1, p2));
                        ListP0.Add(p0 + Unit.Convert((xu1 < xu3) ? (xu1) : (xu3)) * x + Unit.Convert(yu0) * y);
                        Distance.Add(Math.Abs(xu1 - xu3) / (BarModel.Layer + 1));
                    }
                }
                else
                {
                    if (PointModel.AreEqual((Math.Abs(x2-x3)),(Math.Abs(x1-x4))))
                    {
                        double tanA = (Math.Abs(y1 - y3)) / (Math.Abs(Math.Abs(x3 - x4) - Math.Abs(x1 - x2)) * 0.5);
                        for (int i = 0; i < number; i++)
                        {

                            double yu0 = (y3 + ((y3 > 0) ? ((y2 > y3) ? 1 : -1) : ((y2 < y3) ? -1 : 1)) * (BarModel.Distance * 0.5 + i * BarModel.Distance));
                            double xu3 = x3 + ((x3 > 0) ? ((x2 < x3) ? -1 : 1) : ((x2 > x3) ? 1 : -1)) * ((BarModel.Distance * 0.5 + i * BarModel.Distance) / tanA);
                            double xu4 = x4 + ((x4 > 0) ? ((x1 < x4) ? -1 : 1) : ((x1 > x4) ? 1 : -1)) * ((BarModel.Distance * 0.5 + i * BarModel.Distance) / tanA);
                            XYZ p1 = p0 + Unit.Convert(xu3) * x + Unit.Convert(yu0) * y + Unit.Convert(-z0) * XYZ.BasisZ;
                            XYZ p2 = p0 + Unit.Convert(xu4) * x + Unit.Convert(yu0) * y + Unit.Convert(-z0) * XYZ.BasisZ;
                            Curve a = Line.CreateBound(p1, p2);
                            if (a.Length > deltaBar)
                            {
                                curves.Add(a);
                                ListP0.Add(p0 + Unit.Convert((xu3 < xu4) ? (xu3) : (xu4)) * x + Unit.Convert(yu0) * y);
                                Distance.Add(Math.Abs(xu3 - xu4) / (BarModel.Layer + 1));
                            }

                        }
                    }
                    else
                    {
                        double tanA23 = (Math.Abs(y2 - y3)) / (Math.Abs(x2 - x3));
                        double tanA14 = (Math.Abs(y1 - y4)) / (Math.Abs(x1 - x4));
                        for (int i = 0; i < number; i++)
                        {

                            double yu0 = (y4 + ((y4 > 0) ? ((y1 > y4) ? 1 : -1) : ((y1 < y4) ? -1 : 1)) * (BarModel.Distance * 0.5 + i * BarModel.Distance));
                            double xu3 = x3 + ((x3 > 0) ? ((x2 < x3) ? -1 : 1) : ((x2 > x3) ? 1 : -1)) * ((BarModel.Distance * 0.5 + i * BarModel.Distance) / tanA23);
                            double xu4 = x4 + ((x4 > 0) ? ((x1 < x4) ? -1 : 1) : ((x1 > x4) ? 1 : -1)) * ((BarModel.Distance * 0.5 + i * BarModel.Distance) / tanA14);
                            XYZ p1 = p0 + Unit.Convert(xu3) * x + Unit.Convert(yu0) * y + Unit.Convert(-z0) * XYZ.BasisZ;
                            XYZ p2 = p0 + Unit.Convert(xu4) * x + Unit.Convert(yu0) * y + Unit.Convert(-z0) * XYZ.BasisZ;
                            Curve a = Line.CreateBound(p1, p2);
                            if (a.Length > deltaBar)
                            {
                                curves.Add(a);
                                ListP0.Add(p0 + Unit.Convert((xu3 < xu4) ? (xu3) : (xu4)) * x + Unit.Convert(yu0) * y);
                                Distance.Add(Math.Abs(xu3 - xu4) / (BarModel.Layer + 1));
                            }

                        }
                    }
                }
            }

            return curves;
        }
        private static List<Curve> GetHorizontalRectangle(XYZ p0, XYZ x, XYZ y, double x1, double x2, double x3, double x4, double y1, double y2, double y3, double y4, double z0, BarModel BarModel, UnitProject Unit, double CoverTop, double CoverBottom, double CoverSide, out List<XYZ> ListP0, out List<double> Distance)
        {
            double deltaBar = BarModel.Bar.RebarBarType.get_Parameter(BuiltInParameter.REBAR_STANDARD_BEND_DIAMETER).AsDouble();
            Distance = new List<double>();
            ListP0 = new List<XYZ>();
            double Diameter = BarModel.Bar.Diameter;
            List<Curve> curves = new List<Curve>();
            double total = Math.Abs(y1 - y4) - BarModel.Distance * 0.5 - CoverSide - Diameter * 0.5;
            int number = (int)((total) / BarModel.Distance) + 1;
            for (int i = 0; i < number; i++)
            {

                double yu0 = (y4 + ((y4 > 0) ? ((y1 > y4) ? 1 : -1) : ((y1 < y4) ? -1 : 1)) * (BarModel.Distance * 0.5 + i * BarModel.Distance));
                double xu3 = x3;
                double xu4 = x4;
                XYZ p1 = p0 + Unit.Convert(xu3) * x + Unit.Convert(yu0) * y + Unit.Convert(-z0) * XYZ.BasisZ;
                XYZ p2 = p0 + Unit.Convert(xu4) * x + Unit.Convert(yu0) * y + Unit.Convert(-z0) * XYZ.BasisZ;
                Curve a = Line.CreateBound(p1, p2);
                if (a.Length > deltaBar)
                {
                    curves.Add(a);
                    ListP0.Add(p0 + Unit.Convert((xu3 < xu4) ? (xu3) : (xu4)) * x + Unit.Convert(yu0) * y);
                    Distance.Add(Math.Abs(xu3 - xu4) / (BarModel.Layer + 1));
                }

            }

            return curves;
        }
        private static List<Curve> GetHorizontalQuadrangle(XYZ p0, XYZ x, XYZ y, double x1, double x2, double x3, double x4, double y1, double y2, double y3, double y4, double z0, BarModel BarModel, UnitProject Unit, double CoverTop, double CoverBottom, double CoverSide, out List<XYZ> ListP0, out List<double> Distance)
        {
            Distance = new List<double>();
            ListP0 = new List<XYZ>();
            double Diameter = BarModel.Bar.Diameter;
            double y23 = y1;
            double tanA23 = (Math.Abs(y2 - y3)) / (Math.Abs(x2 - x3));
            double x23 = x3 + ((x3>0)?((x2<x3)?-1:1):((x2>x3)?1:-1)) * (Math.Abs(y23) / tanA23);
            List<Curve> curves = new List<Curve>();
            List<XYZ> ListP01 = new List<XYZ>();
            List<double> Distance1 = new List<double>();
            List<Curve> curves1 = GetHorizontalTriangle(p0, x, y, x2, x23, x1, y2, y23, y1, z0, BarModel, Unit, CoverTop, CoverBottom, CoverSide, out ListP01, out Distance1);
            List<XYZ> ListP02 = new List<XYZ>();
            List<double> Distance2 = new List<double>();
            List<Curve> curves2 = GetHorizontalTrapezoid(p0, x, y, x3, x4, x1, x23, y3, y4, y1, y23, z0, BarModel, Unit, CoverTop, CoverBottom, CoverSide, out ListP02, out Distance2);
            curves.AddRange(curves1);
            curves.AddRange(curves2);
            ListP0.AddRange(ListP01);
            ListP0.AddRange(ListP02);
            Distance.AddRange(Distance1);
            Distance.AddRange(Distance2);
            return curves;
        }
        #endregion
    }
}
