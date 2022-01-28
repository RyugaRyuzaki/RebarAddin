using Autodesk.Revit.DB;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using static R01_ColumnsRebar.ErrorColumns;

namespace R01_ColumnsRebar
{
    public class ProcessDetailItem
    {
        #region stirrup Detail
        public static ObservableCollection<DetailItem> GetStirrupsDetail(SectionStyle sectionStyle,ObservableCollection<InfoModel> infoModels, ObservableCollection<StirrupModel> stirrupModels,bool xy, double Cover)
        {
            ObservableCollection<DetailItem> stirrupsDetail = new ObservableCollection<DetailItem>();

            double x = (infoModels[0].WestPosition + infoModels[0].EastPosition) / 2;
            double y = (infoModels[0].SouthPosition + infoModels[0].NouthPosition) / 2;
            for (int i = 0; i < stirrupModels.Count; i++)
            {
                
                double l = (sectionStyle == SectionStyle.RECTANGLE) ? ((xy)?(infoModels[i].b - 2 * Cover):(infoModels[i].h - 2 * Cover)) : (infoModels[i].D - 2 * Cover);
                double diameter = stirrupModels[i].BarS.Diameter;
                double x0 = (sectionStyle == SectionStyle.RECTANGLE) ? ((infoModels[i].WestPosition+infoModels[i].EastPosition)/2-x) : (infoModels[i].PointXPosition);
                double y0 = (sectionStyle == SectionStyle.RECTANGLE) ? ((infoModels[i].SouthPosition+infoModels[i].NouthPosition)/2-y) : (infoModels[i].PointYPosition);
                if (stirrupModels[i].TypeDis==0)
                {
                    
                    double distance = stirrupModels[i].S;
                    var a = new DetailItem();
                    int number = (int)(stirrupModels[i].L / stirrupModels[i].S)+1;
                    double overight = (stirrupModels[i].L - (number - 1) * stirrupModels[i].S) / 2;
                    a.GetPropertyStirrup(overight, distance, diameter, number, l);
                    a.AllLocation.Add(new LocationBarModel(x0, y0, infoModels[i].BottomPosition + overight));
                    a.AllLocation.Add(new LocationBarModel(x0, y0, infoModels[i].BottomPosition+stirrupModels[i].L - overight));
                    
                    stirrupsDetail.Add(a);
                    
                }
                else
                {
                    double distance1 = stirrupModels[i].S1;
                    var a1 = new DetailItem();
                    int number1 = (int)(stirrupModels[i].L1 / stirrupModels[i].S1) + 1;
                    double overight1 = (stirrupModels[i].L1 - (number1 - 1) * stirrupModels[i].S1) / 2;
                    a1.GetPropertyStirrup(overight1, distance1, diameter, number1, l);
                    a1.AllLocation.Add(new LocationBarModel(x0, y0, infoModels[i].BottomPosition + overight1));
                    a1.AllLocation.Add(new LocationBarModel(x0, y0, infoModels[i].BottomPosition + stirrupModels[i].L1 - overight1));
                    stirrupsDetail.Add(a1);
                    double distance2 = stirrupModels[i].S2;
                    var a2 = new DetailItem();
                    int number2 = (int)(stirrupModels[i].L2 / stirrupModels[i].S2) + 1;
                    double overight2 = (stirrupModels[i].L2 - (number2 - 1) * stirrupModels[i].S2) / 2;
                    a2.GetPropertyStirrup(overight2, distance2, diameter, number2, l);
                    a2.AllLocation.Add(new LocationBarModel(x0, y0, infoModels[i].BottomPosition+ stirrupModels[i].L1 + overight2));
                    a2.AllLocation.Add(new LocationBarModel(x0, y0, infoModels[i].BottomPosition +stirrupModels[i].L1+stirrupModels[i].L2-overight2));
                    stirrupsDetail.Add(a2);
                    var a3 = new DetailItem();
                    a3.GetPropertyStirrup(overight1, distance1, diameter, number1, l);
                    a3.AllLocation.Add(new LocationBarModel(x0, y0, infoModels[i].BottomPosition + stirrupModels[i].L1 + stirrupModels[i].L2 + overight1));
                    a3.AllLocation.Add(new LocationBarModel(x0, y0, infoModels[i].BottomPosition + stirrupModels[i].L - overight1));
                    stirrupsDetail.Add(a3);
                }
                
            }
           
            return stirrupsDetail;
        }

        #endregion
        #region stirrup Section
        public static ObservableCollection<DetailItem> GetStirrupSection(SectionStyle sectionStyle, ObservableCollection<InfoModel> infoModels, ObservableCollection<StirrupModel> stirrupModels,  double Cover)
        {
            ObservableCollection<DetailItem> stirrupSection = new ObservableCollection<DetailItem>();

            for (int i = 0; i < stirrupModels.Count; i++)
            {
                double diameter = stirrupModels[i].BarS.Diameter;
                double l = 5* diameter;
                double la = (sectionStyle == SectionStyle.RECTANGLE) ? infoModels[i].b - 2 * Cover : (infoModels[i].D - 2 * Cover);
                double lb = (sectionStyle == SectionStyle.RECTANGLE) ? (infoModels[i].h - 2 * Cover) : (infoModels[i].D - 2 * Cover);
                double x0 = (sectionStyle == SectionStyle.RECTANGLE) ? ((infoModels[i].WestPosition + infoModels[i].EastPosition) / 2 ) : (infoModels[i].PointXPosition);
                double y0 = (sectionStyle == SectionStyle.RECTANGLE) ? (infoModels[i].NouthPosition) : (infoModels[i].PointYPosition);
                double distance =(stirrupModels[i].TypeDis==0)? stirrupModels[i].S:stirrupModels[i].S2;
                var a = new DetailItem();
                int number = (stirrupModels[i].TypeDis == 0) ? (int)(stirrupModels[i].L / stirrupModels[i].S) + 1: (int)(stirrupModels[i].L2 / stirrupModels[i].S2);
                a.GetPropertyStirrupSection(sectionStyle,distance, diameter, number, l, la, lb);
                a.Location = new LocationBarModel(x0, y0, stirrupModels[i].L1+stirrupModels[i].L2*0.5+infoModels[i].BottomPosition);
                a.RebarNumber = i + 1;
                stirrupSection.Add(a);
            }

            return stirrupSection;
        }
        public static ObservableCollection<DetailItem> GetAddHSection(SectionStyle sectionStyle, ObservableCollection<InfoModel> infoModels, ObservableCollection<StirrupModel> stirrupModels, double Cover,int number0)
        {
            ObservableCollection<DetailItem> AddHSection = new ObservableCollection<DetailItem>();
            int number1 = number0+1;
            for (int i = 0; i < stirrupModels.Count; i++)
            {
                if (stirrupModels[i].AddH)
                {
                    double diameter = stirrupModels[i].BarH.Diameter;
                    double l = 0;
                    double la = 0;
                    double lb = 0;
                    double x0 = 0;
                    double y0 = 0;
                    double distance = (stirrupModels[i].TypeDis == 0) ? stirrupModels[i].S : stirrupModels[i].S2;
                    if (sectionStyle==SectionStyle.RECTANGLE)
                    {
                        if (stirrupModels[i].TypeH==0)
                        {
                            l = 5 * diameter;
                            la = stirrupModels[i].aH;
                            lb = infoModels[i].h - 2 * Cover;
                            x0 = (infoModels[i].WestPosition + infoModels[i].EastPosition) / 2 ;
                            y0 = infoModels[i].NouthPosition ;
                            var a = new DetailItem();
                            int number = (stirrupModels[i].TypeDis == 0) ? (int)(stirrupModels[i].L / stirrupModels[i].S) + 1 : (int)(stirrupModels[i].L2 / stirrupModels[i].S2);
                            a.GetPropertyAddHSection(distance, diameter, number, l, la, lb);
                            a.Type = DetailItemStyle.DT04A;
                            a.Location = new LocationBarModel(x0, y0, stirrupModels[i].L * 0.5 + infoModels[i].BottomPosition);
                            a.RebarNumber = number1;
                            AddHSection.Add(a);
                        }
                        else
                        {
                            l = infoModels[i].h - 2 * Cover; 
                            la = 5 * diameter;
                            lb = 5 * diameter;
                            x0 = infoModels[i].WestPosition+Cover;
                            y0 = infoModels[i].NouthPosition - Cover;
                            double delta = (infoModels[i].b-2*Cover) / (stirrupModels[i].nH + 1);
                            for (int j = 0; j < stirrupModels[i].nH; j++)
                            {
                                var a = new DetailItem();
                                int number = (stirrupModels[i].TypeDis == 0) ? (int)(stirrupModels[i].L / stirrupModels[i].S) + 1 : (int)(stirrupModels[i].L2 / stirrupModels[i].S2);
                                a.GetPropertyAddHSection(distance, diameter, number, l, la, lb);
                                a.Location = new LocationBarModel(x0+(j+1)*delta-diameter, y0,  stirrupModels[i].L*0.5 + infoModels[i].BottomPosition);
                                switch (stirrupModels[i].TypeH)
                                {
                                    case 1: a.Type = DetailItemStyle.DT05A; break;
                                    case 2: a.Type = DetailItemStyle.DT06A; break;
                                    case 3: a.Type = DetailItemStyle.DT07A; break;
                                    default: a.Type = DetailItemStyle.DT05A; break;
                                }
                                a.RebarNumber = number1;
                                AddHSection.Add(a);
                            }
                        }
                    }
                    else
                    {
                        l = 5 * diameter;
                        la = (infoModels[i].D-2*Cover)*0.5*Math.Sqrt(2);
                        lb = (infoModels[i].D - 2 * Cover) * 0.5 * Math.Sqrt(2);
                        x0 = infoModels[i].PointXPosition;
                        y0 = infoModels[i].PointYPosition+ (infoModels[i].D - 2 * Cover) * 0.5 * Math.Sqrt(2)*0.5+Cover;
                        var a = new DetailItem();
                        int number = (stirrupModels[i].TypeDis == 0) ? (int)(stirrupModels[i].L / stirrupModels[i].S) + 1 : (int)(stirrupModels[i].L2 / stirrupModels[i].S2);
                        a.GetPropertyAddHSection(distance, diameter, number, l, la, lb);
                        a.Type = DetailItemStyle.DT04A;
                        a.Location = new LocationBarModel(x0, y0, stirrupModels[i].L * 0.5 + infoModels[i].BottomPosition);
                        a.RebarNumber = number1;
                        AddHSection.Add(a);
                    }
                    number1++;
                }
               
            }

            return AddHSection;
        }
        public static ObservableCollection<DetailItem> GetAddVSection(SectionStyle sectionStyle, ObservableCollection<InfoModel> infoModels, ObservableCollection<StirrupModel> stirrupModels, double Cover, int number0)
        {
            ObservableCollection<DetailItem> AddVSection = new ObservableCollection<DetailItem>();
            int number1 = number0 + 1;
            for (int i = 0; i < stirrupModels.Count; i++)
            {
                if (stirrupModels[i].AddV)
                {
                    double diameter = stirrupModels[i].BarV.Diameter;
                    double l = 0;
                    double la = 0;
                    double lb = 0;
                    double x0 = 0;
                    double y0 = 0;
                    double distance = (stirrupModels[i].TypeDis == 0) ? stirrupModels[i].S : stirrupModels[i].S2;
                    if (sectionStyle == SectionStyle.RECTANGLE)
                    {
                        if (stirrupModels[i].TypeV == 0)
                        {
                            l = 5 * diameter;
                            la =  infoModels[i].b - 2 * Cover;
                            lb = stirrupModels[i].aV;
                            x0 = (infoModels[i].WestPosition + infoModels[i].EastPosition) / 2;
                            y0 = (infoModels[i].NouthPosition+infoModels[i].SouthPosition)/2+ stirrupModels[i].aV/2;
                            var a = new DetailItem();
                            int number = (stirrupModels[i].TypeDis == 0) ? (int)(stirrupModels[i].L / stirrupModels[i].S) + 1 : (int)(stirrupModels[i].L2 / stirrupModels[i].S2);
                            a.GetPropertyAddHSection(distance, diameter, number, l, la, lb);
                            a.Type = DetailItemStyle.DT04A;
                            a.Location = new LocationBarModel(x0, y0, stirrupModels[i].L * 0.5 + infoModels[i].BottomPosition);
                            a.RebarNumber = number1;
                            AddVSection.Add(a);
                        }
                        else
                        {
                            l = infoModels[i].b - 2 * Cover;
                            la = 5 * diameter;
                            lb = 5 * diameter;
                            x0 = infoModels[i].WestPosition + Cover;
                            y0 = infoModels[i].SouthPosition - Cover;
                            double delta = (infoModels[i].h - 2 * Cover) / (stirrupModels[i].nV + 1);
                            for (int j = 0; j < stirrupModels[i].nV; j++)
                            {
                                var a = new DetailItem();
                                int number = (stirrupModels[i].TypeDis == 0) ? (int)(stirrupModels[i].L / stirrupModels[i].S) + 1 : (int)(stirrupModels[i].L2 / stirrupModels[i].S2);
                                a.GetPropertyAddHSection(distance, diameter, number, l, la, lb);
                                a.Location = new LocationBarModel(x0 , y0 + (j + 1) * delta - diameter, stirrupModels[i].L * 0.5 + infoModels[i].BottomPosition);
                                switch (stirrupModels[i].TypeV)
                                {
                                    case 1: a.Type = DetailItemStyle.DT05A; break;
                                    case 2: a.Type = DetailItemStyle.DT06A; break;
                                    case 3: a.Type = DetailItemStyle.DT07A; break;
                                    default: a.Type = DetailItemStyle.DT05A; break;
                                }
                                a.RebarNumber = number1;
                                AddVSection.Add(a);
                            }
                        }
                    }
                    else
                    {
                        l = 5 * diameter;
                        la = (infoModels[i].D - 2 * Cover) * 0.5 * Math.Sqrt(2);
                        lb = (infoModels[i].D - 2 * Cover) * 0.5 * Math.Sqrt(2);
                        x0 = infoModels[i].PointXPosition;
                        y0 = infoModels[i].PointYPosition + (infoModels[i].D - 2 * Cover) * 0.5 * Math.Sqrt(2) * 0.5 + Cover;
                        var a = new DetailItem();
                        int number = (stirrupModels[i].TypeDis == 0) ? (int)(stirrupModels[i].L / stirrupModels[i].S) + 1 : (int)(stirrupModels[i].L2 / stirrupModels[i].S2);
                        a.GetPropertyAddHSection(distance, diameter, number, l, la, lb);
                        a.Type = DetailItemStyle.DT04A;
                        a.Location = new LocationBarModel(x0, y0, stirrupModels[i].L * 0.5 + infoModels[i].BottomPosition);
                        a.RebarNumber = number1;
                        AddVSection.Add(a);
                    }
                    number1++;
                }
            }
            return AddVSection;
        }
        #endregion
    }
}
