
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
namespace R02_BeamsRebar
{
    public class AddTopBarModel : BaseViewModel
    {
        private ListLayerModel _Start;
        public ListLayerModel Start { get => _Start; set { _Start = value; OnPropertyChanged(); } }
        private ObservableCollection<ListLayerModel> _Mid;
        public ObservableCollection<ListLayerModel> Mid { get => _Mid; set { _Mid = value; OnPropertyChanged(); } }
        private ListLayerModel _End;
        public ListLayerModel End { get => _End; set { _End = value; OnPropertyChanged(); } }
        public AddTopBarModel(int node)
        {
            if (node != 0)
            {
                Mid = new ObservableCollection<ListLayerModel>();
                for (int i = 0; i < node; i++)
                {
                    Mid.Add(new ListLayerModel());
                }
            }
            Start = new ListLayerModel();
            End = new ListLayerModel();
        }
        public void GetLocationStart(List<InfoModel> infoModels, List<DistributeStirrup> distributeStirrups, List<StirrupModel> stirrupModels, List<NodeModel> AllNodeModel, List<MainTopBarModel> MainTopBarModel, SingleMainTopBarModel SingleMainTopBarModel, SelectedIndexModel SelectedIndexModel, double c, double tmin)
        {
            
            if (Start.Model.Count != 0)
            {
                double ds = ProcessInfoBeamRebar.GetDiameterStirrupMax(infoModels, distributeStirrups, stirrupModels);
                double dMainTop = GetDMainTop(MainTopBarModel, SingleMainTopBarModel, SelectedIndexModel);
                double a = 0;
                if (Start.Model[0].NumberBar < 2)
                {
                    for (int i = 0; i < Start.Model.Count; i++)
                    {
                        Start.Model[i].X0 = c;
                        Start.Model[i].Y0 = Math.Abs(infoModels[0].zOffset) + c + ds + Start.Model[i].Bar.Diameter / 2 + a + i * tmin;
                        a += Start.Model[i].Bar.Diameter;
                    }
                }
                else
                {
                    for (int i = 0; i < Start.Model.Count; i++)
                    {
                        Start.Model[i].X0 = c;
                        Start.Model[i].Y0 = Math.Abs(infoModels[0].zOffset) + c + ds + Start.Model[i].Bar.Diameter / 2 + a + (i + 1) * tmin+dMainTop;
                        a += Start.Model[i].Bar.Diameter;
                    }
                }

                for (int i = 0; i < Start.Model.Count; i++)
                {
                    Start.Model[i].GetLocationStart();
                }
            }

        }
        public void GetLocationEnd(List<InfoModel> infoModels, List<DistributeStirrup> distributeStirrups, List<StirrupModel> stirrupModels, List<NodeModel> AllNodeModel, List<MainTopBarModel> MainTopBarModel, SingleMainTopBarModel SingleMainTopBarModel, SelectedIndexModel SelectedIndexModel, double c, double tmin)
        {
           
            if (End.Model.Count != 0)
            {
                double ds = ProcessInfoBeamRebar.GetDiameterStirrupMax(infoModels, distributeStirrups, stirrupModels);
                double dMainTop = GetDMainTop(MainTopBarModel, SingleMainTopBarModel, SelectedIndexModel);
                double a = 0;
                if (End.Model[0].NumberBar < 2)
                {
                    for (int i = 0; i < End.Model.Count; i++)
                    {
                        End.Model[i].X0 = AllNodeModel[AllNodeModel.Count - 1].End - c;
                        End.Model[i].Y0 = Math.Abs(infoModels[infoModels.Count-1].zOffset) + c + ds + End.Model[i].Bar.Diameter / 2 + a + i * tmin;
                        a += End.Model[i].Bar.Diameter;
                    }
                }
                else
                {
                    for (int i = 0; i < End.Model.Count; i++)
                    {
                        End.Model[i].X0 = AllNodeModel[AllNodeModel.Count - 1].End - c;
                        End.Model[i].Y0 = Math.Abs(infoModels[infoModels.Count - 1].zOffset) + c + ds + End.Model[i].Bar.Diameter / 2 + a + (i + 1) * tmin + dMainTop;
                        a += End.Model[i].Bar.Diameter;
                    }
                }

                for (int i = 0; i < End.Model.Count; i++)
                {
                    End.Model[i].GetLocationEnd();
                }
            }

        }
        public void GetLocationMid(List<InfoModel> infoModels, List<DistributeStirrup> distributeStirrups, List<StirrupModel> stirrupModels, List<NodeModel> AllNodeModel, List<MainTopBarModel> MainTopBarModel, SingleMainTopBarModel SingleMainTopBarModel, SelectedIndexModel SelectedIndexModel, ObservableCollection<int> AllNodes, double c, double tmin)
        {
            double ds = ProcessInfoBeamRebar.GetDiameterStirrupMax(infoModels, distributeStirrups, stirrupModels);
            double dMainTop = GetDMainTop(MainTopBarModel, SingleMainTopBarModel, SelectedIndexModel);
            for (int i = 0; i < AllNodes.Count; i++)
            {
                NodeModel nodeModel = AllNodeModel.Where(x => x.NumberNode == AllNodes[i]).FirstOrDefault();

                if (nodeModel.ZLeft != nodeModel.ZRight)
                {
                    if (Mid[i].Model.Count != 0)
                    {
                        double a = 0;
                        double b = (nodeModel.ZLeft - nodeModel.ZRight);
                        for (int j = 0; j < Mid[i].Model.Count; j++)
                        {
                            Mid[i].Model[j].X0 = nodeModel.Start + nodeModel.Width / 2;
                            if (Mid[i].Model[0].NumberBar < 2)
                            {
                                Mid[i].Model[j].Y0 = Math.Abs(nodeModel.ZLeft) + (nodeModel.ZRight - nodeModel.ZLeft) / 2 + c + ds + Mid[i].Model[j].Bar.Diameter / 2 + a + j * tmin;

                            }
                            else
                            {
                                Mid[i].Model[j].Y0 = Math.Abs(nodeModel.ZLeft) + (nodeModel.ZRight - nodeModel.ZLeft) / 2 + c + ds + Mid[i].Model[j].Bar.Diameter / 2 + a + (j + 1) * tmin + dMainTop;
                            }
                            a += Mid[i].Model[j].Bar.Diameter;
                        }
                        for (int k = 0; k < Mid[i].Model.Count; k++)
                        {
                            Mid[i].Model[k].GetLocatoionMid(nodeModel.Width, c, b, true);
                        }

                    }
                }
                else
                {
                    if (Mid[i].Model.Count != 0)
                    {
                        double a = 0;
                        double b = Math.Abs(nodeModel.ZLeft - nodeModel.ZRight);
                        for (int j = 0; j < Mid[i].Model.Count; j++)
                        {
                            Mid[i].Model[j].X0 = nodeModel.Start + nodeModel.Width / 2;
                            if (Mid[i].Model[0].NumberBar < 2)
                            {
                                Mid[i].Model[j].Y0 = Math.Abs(nodeModel.ZLeft) + c + ds + Mid[i].Model[j].Bar.Diameter / 2 + a + j * tmin;

                            }
                            else
                            {
                                Mid[i].Model[j].Y0 = Math.Abs(nodeModel.ZLeft) + c + ds + Mid[i].Model[j].Bar.Diameter / 2 + a + (j + 1) * tmin + dMainTop;
                            }
                            a += Mid[i].Model[j].Bar.Diameter;
                        }
                        for (int k = 0; k < Mid[i].Model.Count; k++)
                        {
                            Mid[i].Model[k].GetLocatoionMid(nodeModel.Width, c, b, false);
                        }
                    }
                }
                
            }
        }
        private double GetDMainTop(List<MainTopBarModel> MainTopBarModel, SingleMainTopBarModel SingleMainTopBarModel, SelectedIndexModel SelectedIndexModel)
        {
            double dMainTop = 0;
            if (MainTopBarModel.Count == 1)
            {
                dMainTop = SingleMainTopBarModel.Bar.Diameter;
            }
            else
            {
                if (SelectedIndexModel.StyleMainTop == 0)
                {
                    dMainTop = SingleMainTopBarModel.Bar.Diameter;
                }
                else
                {
                    for (int i = 0; i < MainTopBarModel.Count; i++)
                    {
                        if (dMainTop<MainTopBarModel[i].Bar.Diameter)
                        {
                            dMainTop = MainTopBarModel[i].Bar.Diameter;
                        }
                    }
                }
            }
            return dMainTop;
        }
    }
}
