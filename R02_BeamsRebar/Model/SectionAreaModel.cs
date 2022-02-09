
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using WpfCustomControls;
namespace R02_BeamsRebar
{
    public class SectionAreaModel : BaseViewModel
    {
        private int _NumberSpan;
        public int NumberSpan { get => _NumberSpan; set { _NumberSpan = value; OnPropertyChanged(); } }
        private List<SectionModel> _Start;
        public List<SectionModel> Start { get => _Start; set { _Start = value; OnPropertyChanged(); } }
        private string _DetailStart;
        public string DetailStart { get => _DetailStart; set { _DetailStart = value; OnPropertyChanged(); } }
        private string _NameStart;
        public string NameStart { get => _NameStart; set { _NameStart = value; OnPropertyChanged(); } }
        private List<SectionModel> _Middle;
        public List<SectionModel> Middle { get => _Middle; set { _Middle = value; OnPropertyChanged(); } }
        private string _DetailMiddle;
        public string DetailMiddle { get => _DetailMiddle; set { _DetailMiddle = value; OnPropertyChanged(); } }
        private string _NameMiddle;
        public string NameMiddle { get => _NameMiddle; set { _NameMiddle = value; OnPropertyChanged(); } }
        private List<SectionModel> _End;
        public List<SectionModel> End { get => _End; set { _End = value; OnPropertyChanged(); } }
        private string _DetailEnd;
        public string DetailEnd { get => _DetailEnd; set { _DetailEnd = value; OnPropertyChanged(); } }
        private string _NamelEnd;
        public string NamelEnd { get => _NamelEnd; set { _NamelEnd = value; OnPropertyChanged(); } }
        public SectionAreaModel(int span)
        {
            NumberSpan = span;
            Start = new List<SectionModel>();
            Middle = new List<SectionModel>();
            End = new List<SectionModel>();
        }
        public void GetNameSection(int NumberSpan,SettingModel settingModel)
        {
            NameStart= settingModel.DetailViewName + " SP" + NumberSpan + " " + settingModel.PrefixSection + "1-1";
            NameMiddle = settingModel.DetailViewName + " SP" + NumberSpan + " " + settingModel.PrefixSection + "2-2";
            NamelEnd = settingModel.DetailViewName + " SP" + NumberSpan + " " + settingModel.PrefixSection + "3-3";
        }
        public void GetBar(InfoModel infoModel0, InfoModel infoModel, List<MainTopBarModel> MainTopBarModel, SingleMainTopBarModel SingleMainTopBarModel, List<MainBottomBarModel> MainBottomBarModel, AddTopBarModel AddTopBarModel, AddBottomBarModel AddBottomBarModel, SelectedIndexModel SelectedIndexModel)
        {
            if (Start.Count==0)
            {
                Start.Add(GetMainTop( infoModel, MainTopBarModel, SingleMainTopBarModel, SelectedIndexModel));
                Start.AddRange(GetAddTopStart(infoModel0,infoModel, AddTopBarModel));
                Start.AddRange(GetAddBottomStart(infoModel, AddBottomBarModel));
                Start.Add(GetMainBottom(infoModel, MainBottomBarModel));
            }
            else
            {
                Start.Clear();
                Start.Add(GetMainTop( infoModel, MainTopBarModel, SingleMainTopBarModel, SelectedIndexModel));
                Start.AddRange(GetAddTopStart(infoModel0,infoModel, AddTopBarModel));
                Start.AddRange(GetAddBottomStart(infoModel, AddBottomBarModel));
                Start.Add(GetMainBottom(infoModel, MainBottomBarModel));
            }
            if (End.Count == 0)
            {
                End.Add(GetMainTop( infoModel, MainTopBarModel, SingleMainTopBarModel, SelectedIndexModel));
                End.AddRange(GetAddTopEnd(infoModel, AddTopBarModel));
                End.AddRange(GetAddBottomEnd(infoModel, AddBottomBarModel));
                End.Add(GetMainBottom(infoModel, MainBottomBarModel));
            }
            else
            {
                End.Clear();
                End.Add(GetMainTop( infoModel, MainTopBarModel, SingleMainTopBarModel, SelectedIndexModel));
                End.AddRange(GetAddTopEnd(infoModel, AddTopBarModel));
                End.AddRange(GetAddBottomEnd(infoModel, AddBottomBarModel));
                End.Add(GetMainBottom(infoModel, MainBottomBarModel));
            }
            if (Middle.Count == 0)
            {
                Middle.Add(GetMainTop( infoModel, MainTopBarModel, SingleMainTopBarModel, SelectedIndexModel));
                Middle.AddRange(GetAddBottomMid(infoModel, AddBottomBarModel));
                Middle.Add(GetMainBottom(infoModel, MainBottomBarModel));
            }
            else
            {
                Middle.Clear();
                Middle.Add(GetMainTop( infoModel, MainTopBarModel, SingleMainTopBarModel, SelectedIndexModel));
                Middle.AddRange(GetAddBottomMid(infoModel, AddBottomBarModel));
                Middle.Add(GetMainBottom(infoModel, MainBottomBarModel));
            }
            DetailStart = "( ";
            for (int i = 0; i < Start.Count; i++)
            {
                if (i == Start.Count - 1)
                {
                    DetailStart += Start[i].Detail;
                }
                else
                {
                    DetailStart += Start[i].Detail + "+";
                }

            }
            DetailStart += " )";

            DetailMiddle = "( ";
            for (int i = 0; i < Middle.Count; i++)
            {
                if (i == Middle.Count - 1)
                {
                    DetailMiddle += Middle[i].Detail;
                }
                else
                {
                    DetailMiddle += Middle[i].Detail + "+";
                }
            }
            DetailMiddle += " )";

            DetailEnd = "( ";

            for (int i = 0; i < End.Count; i++)
            {
                if (i == End.Count - 1)
                {
                    DetailEnd += End[i].Detail;
                }
                else
                {
                    DetailEnd += End[i].Detail + "+";
                }
            }
            DetailEnd += " )";
        }
        private SectionModel GetMainTop(InfoModel infoModel, List<MainTopBarModel> MainTopBarModel, SingleMainTopBarModel SingleMainTopBarModel, SelectedIndexModel selectedIndexModel)
        {
            SectionModel a = null;
            if (selectedIndexModel.StyleMainTop == 0)
            {
                if (SingleMainTopBarModel.Location.Count <= 4)
                {
                    return new SectionModel(SingleMainTopBarModel.Bar, SingleMainTopBarModel.NumberBar,  SingleMainTopBarModel.Location[1].Y);
                }
                else
                {
                    if (infoModel.ConsolLeft)
                    {
                        return new SectionModel(SingleMainTopBarModel.Bar, SingleMainTopBarModel.NumberBar,  SingleMainTopBarModel.Location[1].Y);
                    }
                    else
                    {
                        for (int i = 0; i < SingleMainTopBarModel.Location.Count; i++)
                        {
                            if (infoModel.startPosition < SingleMainTopBarModel.Location[i].X)
                            {
                                return new SectionModel(SingleMainTopBarModel.Bar, SingleMainTopBarModel.NumberBar,  SingleMainTopBarModel.Location[i].Y);
                            }
                        }
                    }
                }
            }
            else
            {
                if (infoModel.ConsolLeft)
                {
                    return new SectionModel(MainTopBarModel[0].Bar, MainTopBarModel[0].NumberBar,  MainTopBarModel[0].Location[1].Y);
                }
                else
                {
                    for (int i = 0; i < MainTopBarModel.Count; i++)
                    {
                        if (infoModel.startPosition < MainTopBarModel[i].X0 + MainTopBarModel[i].Length)
                        {
                            return new SectionModel(MainTopBarModel[i].Bar, MainTopBarModel[i].NumberBar,  MainTopBarModel[i].Location[1].Y);
                        }
                    }
                }
            }
            return a;
        }
        private SectionModel GetMainBottom(InfoModel infoModel, List<MainBottomBarModel> MainBottomBarModel)
        {
            SectionModel a = null;
            if (MainBottomBarModel.Count==1)
            {
                return new SectionModel(MainBottomBarModel[0].Bar, MainBottomBarModel[0].NumberBar, MainBottomBarModel[0].Location[1].Y);
            }
            else
            {
                for (int i = 0; i < MainBottomBarModel.Count; i++)
                {
                    if (infoModel.startPosition < MainBottomBarModel[i].X0 + MainBottomBarModel[i].Length)
                    {
                        return new SectionModel(MainBottomBarModel[i].Bar, MainBottomBarModel[i].NumberBar, MainBottomBarModel[i].Location[1].Y);
                    }
                }
            }
            return a;
        }
        private List<SectionModel> GetAddTopStart(InfoModel infoModel0, InfoModel infoModel, AddTopBarModel AddTopBarModel)
        {
            List<SectionModel> a = new List<SectionModel>();
            if (!infoModel.ConsolLeft)
            {
                if (AddTopBarModel.Start.Model.Count != 0)
                {
                    if (infoModel.startPosition < AddTopBarModel.Start.Model[0].X0 + AddTopBarModel.Start.Model[0].L)
                    {
                        for (int i = 0; i < AddTopBarModel.Start.Model.Count; i++)
                        {
                            a.Add(new SectionModel(AddTopBarModel.Start.Model[i].Bar, AddTopBarModel.Start.Model[i].NumberBar, AddTopBarModel.Start.Model[i].Y0));
                        }
                    }
                    else
                    {
                        if (AddTopBarModel.Mid.Count != 0)
                        {
                            for (int i = 0; i < AddTopBarModel.Mid.Count; i++)
                            {
                                if (AddTopBarModel.Mid[i].Model.Count != 0)
                                {
                                    if ((infoModel.startPosition > AddTopBarModel.Mid[i].Model[0].X0) && (infoModel.startPosition < AddTopBarModel.Mid[i].Model[0].X0 + AddTopBarModel.Mid[i].Model[0].La))
                                    {
                                        for (int j = 0; j < AddTopBarModel.Mid[i].Model.Count; j++)
                                        {
                                            a.Add(new SectionModel(AddTopBarModel.Mid[i].Model[j].Bar, AddTopBarModel.Mid[i].Model[j].NumberBar,  AddTopBarModel.Mid[i].Model[j].Y0));
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
                else
                {
                    if (AddTopBarModel.Mid!=null)
                    {
                        for (int i = 0; i < AddTopBarModel.Mid.Count; i++)
                        {
                            if (AddTopBarModel.Mid[i].Model.Count != 0)
                            {
                                if ((infoModel.startPosition > AddTopBarModel.Mid[i].Model[0].X0) && (infoModel.startPosition < AddTopBarModel.Mid[i].Model[0].X0 + AddTopBarModel.Mid[i].Model[0].La))
                                {
                                    for (int j = 0; j < AddTopBarModel.Mid[i].Model.Count; j++)
                                    {
                                        a.Add(new SectionModel(AddTopBarModel.Mid[i].Model[j].Bar, AddTopBarModel.Mid[i].Model[j].NumberBar,  AddTopBarModel.Mid[i].Model[j].Y0));
                                    }
                                }
                            }
                        }
                    }
                }
            }
            return a;
        }
        private List<SectionModel> GetAddTopEnd(InfoModel infoModel, AddTopBarModel AddTopBarModel)
        {
            List<SectionModel> a = new List<SectionModel>();
            if (!infoModel.ConsolRight)
            {
                if (AddTopBarModel.End.Model.Count != 0)
                {
                    if ((infoModel.endPosition > AddTopBarModel.End.Model[0].X0 - AddTopBarModel.End.Model[0].L))
                    {
                        for (int i = 0; i < AddTopBarModel.End.Model.Count; i++)
                        {
                            a.Add(new SectionModel(AddTopBarModel.End.Model[i].Bar, AddTopBarModel.End.Model[i].NumberBar, AddTopBarModel.End.Model[i].Y0));
                        }
                    }
                    else
                    {
                        if (AddTopBarModel.Mid != null)
                        {
                            for (int i = 0; i < AddTopBarModel.Mid.Count; i++)
                            {
                                if (AddTopBarModel.Mid[i].Model.Count!=0)
                                {
                                    if ((infoModel.endPosition < AddTopBarModel.Mid[i].Model[0].X0)&&(infoModel.endPosition > AddTopBarModel.Mid[i].Model[0].X0 - AddTopBarModel.Mid[i].Model[0].L))
                                    {
                                        for (int j = 0; j < AddTopBarModel.Mid[i].Model.Count; j++)
                                        {
                                            a.Add(new SectionModel(AddTopBarModel.Mid[i].Model[j].Bar, AddTopBarModel.Mid[i].Model[j].NumberBar, AddTopBarModel.Mid[i].Model[j].Y0));
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
                else
                {
                    if (AddTopBarModel.Mid != null)
                    {
                        for (int i = 0; i < AddTopBarModel.Mid.Count; i++)
                        {
                            if (AddTopBarModel.Mid[i].Model.Count != 0)
                            {
                                if ((infoModel.endPosition < AddTopBarModel.Mid[i].Model[0].X0) && (infoModel.endPosition > AddTopBarModel.Mid[i].Model[0].X0 - AddTopBarModel.Mid[i].Model[0].L))
                                {
                                    for (int j = 0; j < AddTopBarModel.Mid[i].Model.Count; j++)
                                    {
                                        a.Add(new SectionModel(AddTopBarModel.Mid[i].Model[j].Bar, AddTopBarModel.Mid[i].Model[j].NumberBar, AddTopBarModel.Mid[i].Model[j].Y0));
                                    }
                                }
                            }
                        }
                    }
                }
            }
            return a;
        }
        private List<SectionModel> GetAddBottomStart(InfoModel infoModel, AddBottomBarModel AddBottomBarModel)
        {
            List<SectionModel> a = new List<SectionModel>();
            if (AddBottomBarModel.Start.Model.Count!=0)
            {
                for (int i = 0; i < AddBottomBarModel.Start.Model.Count; i++)
                {
                    a.Add(new SectionModel(AddBottomBarModel.Start.Model[i].Bar, AddBottomBarModel.Start.Model[i].NumberBar, AddBottomBarModel.Start.Model[i].Y0));
                }
            }
            return a;
        }
        private List<SectionModel> GetAddBottomEnd(InfoModel infoModel, AddBottomBarModel AddBottomBarModel)
        {
            List<SectionModel> a = new List<SectionModel>();
            if (AddBottomBarModel.End.Model.Count != 0)
            {
                for (int i = 0; i < AddBottomBarModel.End.Model.Count; i++)
                {
                    a.Add(new SectionModel(AddBottomBarModel.End.Model[i].Bar, AddBottomBarModel.End.Model[i].NumberBar, AddBottomBarModel.End.Model[i].Y0));
                }
            }
            return a;
        }
        private List<SectionModel> GetAddBottomMid(InfoModel infoModel, AddBottomBarModel AddBottomBarModel)
        {
            List<SectionModel> a = new List<SectionModel>();
            if (AddBottomBarModel.Mid.Model.Count != 0)
            {
                for (int i = 0; i < AddBottomBarModel.Mid.Model.Count; i++)
                {
                    a.Add(new SectionModel(AddBottomBarModel.Mid.Model[i].Bar, AddBottomBarModel.Mid.Model[i].NumberBar, AddBottomBarModel.Mid.Model[i].Y0));
                }
            }
            return a;
        }
    }

}
