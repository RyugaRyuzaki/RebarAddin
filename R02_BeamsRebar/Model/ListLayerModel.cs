using WpfCustomControls;
using Autodesk.Revit.DB;
using System.Collections.ObjectModel;
using System.Linq;
using DSP;
namespace R02_BeamsRebar
{
    public class ListLayerModel : BaseViewModel
    {
        private ObservableCollection<LayerModel> _Model;
        public ObservableCollection<LayerModel> Model { get => _Model; set { _Model = value; OnPropertyChanged(); } }
        public ListLayerModel()
        {
            Model = new ObservableCollection<LayerModel>();
        }
        public void DeleteItem(int a)
        {
            if (a == Model.Count - 1)
            {
                Model.RemoveAt(a);
                return;
            }
            if (Model.Count == 1)
            {
                Model.RemoveAt(0);
                return;
            }

            int b = 0;
            for (int i = 0; i < Model.Count; i++)
            {
                if (i == a)
                {
                    b = i;
                }
            }
            for (int i = b; i < Model.Count - 1; i++)
            {
                Model[i + 1].Layer -= 1;
            }
            Model.RemoveAt(a);
            Model.OrderBy(x => x.Layer);
        }
        public bool CheckedLayer()
        {
            if (Model.Count > 1)
            {
                for (int i = 1; i < Model.Count; i++)
                {
                    if (Model[i].L > Model[0].L)
                    {
                        return false;
                    }
                    if (Model[i].La > Model[0].La)
                    {
                        return false;
                    }
                }
            }
            return true;

        }

        #region Create Bar
        public void CreateBar(Document document, PlanarFace planarFace, UnitProject unit, double c, double dsmax, InfoModel infoModel, SettingModel settingModel)
        {
            if (Model.Count != 0)
            {
                for (int i = 0; i < Model.Count; i++)
                {
                    Model[i].CreateAddBar(document, planarFace, unit, c, dsmax, infoModel, settingModel);
                }
            }
        }
        public void CreateBarMid(Document document, PlanarFace planarFace, UnitProject unit, double c, double dsmax, InfoModel infoModel, SettingModel settingModel)
        {
            if (Model.Count != 0)
            {
                for (int i = 0; i < Model.Count; i++)
                {
                    Model[i].CreateAddBarMid(document, planarFace, unit, c, dsmax, infoModel, settingModel);
                }
            }
        }
        #endregion
        #region Create Tag Bar Detail
        public void CreateTagRebarDetailAddTopStart(Autodesk.Revit.DB.View view, Document document, UnitProject unit, InfoModel infoModel0, NodeModel nodeModel0, SettingModel settingModel, PlanarFace planarFace0, double h0, double v0)
        {
            if (Model.Count != 0)
            {
                double detal = v0 / Model.Count;
                for (int i = 0; i < Model.Count; i++)
                {
                    Model[i].CreateTagRebarDetailTopStart(view, document, unit, infoModel0, nodeModel0, settingModel, planarFace0, h0, v0, i * detal);
                }
            }
        }
        public void CreateTagRebarDetailAddTopEnd(Autodesk.Revit.DB.View view, Document document, UnitProject unit, InfoModel infoModel0, NodeModel nodeModeEnd, SettingModel settingModel, PlanarFace planarFace0, double h0, double v0)
        {
            if (Model.Count != 0)
            {
                double detal = v0 / Model.Count;
                for (int i = 0; i < Model.Count; i++)
                {
                    Model[i].CreateTagRebarDetailTopEnd(view, document, unit, infoModel0, nodeModeEnd, settingModel, planarFace0, h0, v0, i * detal);
                }
            }
        }
        public void CreateTagRebarDetailAddTopMid(Autodesk.Revit.DB.View view, Document document, UnitProject unit, InfoModel infoModel0, NodeModel nodeMode, SettingModel settingModel, PlanarFace planarFace0, double h0, double v0)
        {
            if (Model.Count != 0)
            {
                double detal = v0 / Model.Count;
                for (int i = 0; i < Model.Count; i++)
                {
                    Model[i].CreateTagRebarDetailTopMid(view, document, unit, infoModel0, nodeMode, settingModel, planarFace0, h0, v0, i * detal);
                }
            }
        }
        public void CreateTagRebarDetailAddBottomStart(Autodesk.Revit.DB.View view, Document document, UnitProject unit, InfoModel infoModel0, InfoModel infoModel, SettingModel settingModel, PlanarFace planarFace0, double h0, double v0)
        {
            if (Model.Count != 0)
            {
                double detal = v0 / Model.Count;
                for (int i = 0; i < Model.Count; i++)
                {
                    Model[i].CreateTagRebarDetailBottomStart(view, document, unit, infoModel0, infoModel, settingModel, planarFace0, h0, v0, i * detal);
                }
            }
        }
        public void CreateTagRebarDetailAddBottomEnd(Autodesk.Revit.DB.View view, Document document, UnitProject unit, InfoModel infoModel0, InfoModel infoModel, SettingModel settingModel, PlanarFace planarFace0, double h0, double v0)
        {
            if (Model.Count != 0)
            {
                double detal = v0 / Model.Count;
                for (int i = 0; i < Model.Count; i++)
                {
                    Model[i].CreateTagRebarDetailBottomEnd(view, document, unit, infoModel0, infoModel, settingModel, planarFace0, h0, v0, i * detal);
                }
            }
        }
        public void CreateTagRebarDetailAddBottomMid(Autodesk.Revit.DB.View view, Document document, UnitProject unit, InfoModel infoModel0, InfoModel infoModel, SettingModel settingModel, PlanarFace planarFace0, double h0, double v0)
        {
            if (Model.Count != 0)
            {
                double detal = v0 / Model.Count;
                for (int i = 0; i < Model.Count; i++)
                {
                    Model[i].CreateTagRebarDetailBottomMid(view, document, unit, infoModel0, infoModel, settingModel, planarFace0, h0, v0, i * detal);
                }
            }
        }
        #endregion
        #region Create Tag Bar Section
        public void CreateTagRebarSectionAddTopStart(SectionBeamView SectionBeamView, Document document, UnitProject unit, InfoModel infoModel0, PlanarFace planarFace0, SettingModel settingModel, double tagH0, double tagV0)
        {
            if (Model.Count != 0)
            {
                for (int i = 0; i < Model.Count; i++)
                {
                    if (Model[i].NumberBar != 1)
                    {
                        Model[i].Bar.CreateTagRebarSectionItem(SectionBeamView.StartView, document, unit, infoModel0, planarFace0, settingModel, Model[i].Location[1].Y, tagH0, tagV0);
                    }

                }
            }
        }
        public void CreateTagRebarSectionAddTopEnd(SectionBeamView SectionBeamView, Document document, UnitProject unit, InfoModel infoModel0, PlanarFace planarFace0, SettingModel settingModel, double tagH0, double tagV0)
        {
            if (Model.Count != 0)
            {
                for (int i = 0; i < Model.Count; i++)
                {
                    if (Model[i].NumberBar != 1)
                    {
                        Model[i].Bar.CreateTagRebarSectionItem(SectionBeamView.EndView, document, unit, infoModel0, planarFace0, settingModel, Model[i].Location[1].Y, tagH0, tagV0);
                    }
                }
            }
        }
        public void CreateTagRebarSectionAddTopMid(SectionBeamView SectionBeamView1, SectionBeamView SectionBeamView2, Document document, UnitProject unit, InfoModel infoModel0, PlanarFace planarFace0, SettingModel settingModel, double tagH0, double tagV0)
        {
            if (Model.Count != 0)
            {
                for (int i = 0; i < Model.Count; i++)
                {
                    if (Model[i].NumberBar != 1)
                    {
                        Model[i].Bar.CreateTagRebarSectionItem(SectionBeamView1.EndView, document, unit, infoModel0, planarFace0, settingModel, Model[i].Location[1].Y, tagH0, tagV0);
                        Model[i].Bar.CreateTagRebarSectionItem(SectionBeamView2.StartView, document, unit, infoModel0, planarFace0, settingModel, Model[i].Location[1].Y, tagH0, tagV0);
                    }

                }
            }
        }
        public void CreateTagRebarSectionAddBottomStart(SectionBeamView SectionBeamView, Document document, UnitProject unit, InfoModel infoModel0, PlanarFace planarFace0, SettingModel settingModel, double tagH0, double tagV0)
        {
            if (Model.Count != 0)
            {
                for (int i = 0; i < Model.Count; i++)
                {
                    if (Model[i].NumberBar != 1)
                        Model[i].Bar.CreateTagRebarSectionItem(SectionBeamView.StartView, document, unit, infoModel0, planarFace0, settingModel, Model[i].Location[1].Y, tagH0, tagV0);
                }
            }
        }
        public void CreateTagRebarSectionAddBottomEnd(SectionBeamView SectionBeamView, Document document, UnitProject unit, InfoModel infoModel0, PlanarFace planarFace0, SettingModel settingModel, double tagH0, double tagV0)
        {
            if (Model.Count != 0)
            {
                for (int i = 0; i < Model.Count; i++)
                {
                    if (Model[i].NumberBar != 1)
                        Model[i].Bar.CreateTagRebarSectionItem(SectionBeamView.EndView, document, unit, infoModel0, planarFace0, settingModel, Model[i].Location[1].Y, tagH0, tagV0);
                }
            }
        }
        public void CreateTagRebarSectionAddBottomMid(SectionBeamView SectionBeamView, Document document, UnitProject unit, InfoModel infoModel0, PlanarFace planarFace0, SettingModel settingModel, double tagH0, double tagV0)
        {
            if (Model.Count != 0)
            {
                for (int i = 0; i < Model.Count; i++)
                {
                    if (Model[i].NumberBar != 1)
                        Model[i].Bar.CreateTagRebarSectionItem(SectionBeamView.MidView, document, unit, infoModel0, planarFace0, settingModel, Model[i].Location[1].Y, tagH0, tagV0);
                }
            }
        }
        #endregion
    }
}
