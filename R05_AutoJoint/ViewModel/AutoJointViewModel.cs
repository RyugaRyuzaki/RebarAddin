#region Namespaces

using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Threading;
#endregion
using DSP;
namespace R05_AutoJoint
{
    public class AutoJointViewModel : BaseViewModel
    {
        public UIDocument UiDoc;
        public Document Doc;
        private ObservableCollection<JointRule> _JointRules;
        public ObservableCollection<JointRule> JointRules
        {
            get
            {
                if (_JointRules == null) { _JointRules = new ObservableCollection<JointRule>(); }
                return _JointRules;
            }
            set { _JointRules = value; OnPropertyChanged(); }
        }
        public TransactionGroup TransactionGroup { get; set; }
        #region Check
        private bool _IsProject;
        public bool IsProject { get => _IsProject; set { _IsProject = value; OnPropertyChanged(); } }
        private bool _IsCurrenView;
        public bool IsCurrenView { get => _IsCurrenView; set { _IsCurrenView = value; OnPropertyChanged(); } }
        private double _Percent;
        public double Percent { get => _Percent; set { _Percent = value; OnPropertyChanged(); } }
        #endregion
        #region Command
        public ICommand AddRuleCommand { get; set; }
        public ICommand DeleteRuleCommand { get; set; }
        public ICommand SelectionCommand { get; set; }
        public ICommand JointCommand { get; set; }
        public ICommand UnJointCommand { get; set; }
        public ICommand CancelCommand { get; set; }

        #endregion
        public AutoJointViewModel(UIDocument uiDoc, Document doc)
        {
            UiDoc = uiDoc;
            Doc = doc;
            TransactionGroup = new TransactionGroup(Doc);
            IsProject = true;

            CancelCommand = new RelayCommand<AutoJointWindow>((p) => { return true; }, (p) =>
            {
                DrawPileCap(p.Bitmap);
                ConvertCanvasToBitmap(p.Bitmap, "D:\\PileCap.png");
                p.DialogResult = false;
                if (TransactionGroup.HasStarted())
                {
                    TransactionGroup.RollBack();
                    System.Windows.MessageBox.Show("Progress is Cancel!", "Stop Progress",
                        MessageBoxButton.OK, MessageBoxImage.Stop);
                }
            });
            JointCommand = new RelayCommand<AutoJointWindow>((p) => { return p.GridCombobox.RowDefinitions.Count != 0 /*&& ConditionJointRule()*/; }, (p) =>
             {
                 TransactionGroup.Start("Action");
                 
                 for (int i = 0; i < JointRules.Count; i++)
                 {
                     if (JointRules[i].Joint.SelectedValue.ToString().Equals("Structural Floor") || JointRules[i].BeJointed.SelectedValue.ToString().Equals("Structural Floor"))
                     {
                         JointFloor(p, JointRules[i], i);
                     }
                     else
                     {
                         List<Element> jointElement = GetElementsJoint(JointRules[i].Joint.SelectedValue.ToString());
                         if (jointElement.Count != 0)
                         {
                             foreach (var itemJoint in jointElement)
                             {
                                 if (TransactionGroup.HasStarted())
                                 {
                                     List<Element> beJointElement = GetElementsBeJointed(itemJoint, JointRules[i].BeJointed.SelectedValue.ToString());

                                     p.ProgressBar.Maximum = beJointElement.Count;
                                     double value = 0;

                                     if (beJointElement.Count != 0)
                                     {
                                         using (Transaction transaction = new Transaction(Doc))
                                         {
                                             transaction.Start("Action" + i);
                                             FailureHandlingOptions option = transaction.GetFailureHandlingOptions();
                                             option.SetFailuresPreprocessor(new DeleteWarningSuper());
                                             option.SetClearAfterRollback(true);
                                             transaction.SetFailureHandlingOptions(option);
                                             foreach (var itemBejointed in beJointElement)
                                             {
                                                 value += 1;
                                                 Percent = value / p.ProgressBar.Maximum * 100;
                                                 p.ProgressBar.Dispatcher.Invoke(() => p.ProgressBar.Value = value, DispatcherPriority.Background);
                                                 
                                                 if (JoinGeometryUtils.AreElementsJoined(Doc, itemJoint, itemBejointed))
                                                 {
                                                     if (!JoinGeometryUtils.IsCuttingElementInJoin(Doc, itemJoint, itemBejointed))
                                                     {
                                                         JoinGeometryUtils.SwitchJoinOrder(Doc, itemBejointed, itemJoint);
                                                     }
                                                 }
                                                 else
                                                 {
                                                     JoinGeometryUtils.JoinGeometry(Doc, itemJoint, itemBejointed);
                                                     if (!JoinGeometryUtils.IsCuttingElementInJoin(Doc, itemJoint, itemBejointed))
                                                     {
                                                         JoinGeometryUtils.SwitchJoinOrder(Doc, itemBejointed, itemJoint);
                                                     }
                                                 }
                                               
                                             }
                                            
                                             transaction.Commit();
                                         }
                                         
                                     }
                                 }
                             }
                         }
                     }
                 }

                 if (TransactionGroup.HasStarted())
                 {
                     TransactionGroup.Commit();
                     p.DialogResult = true;
                     System.Windows.MessageBox.Show("Auto Join was successful!",
                         "Auto Join", MessageBoxButton.OK,
                         MessageBoxImage.Information);
                 }
             });
            UnJointCommand = new RelayCommand<AutoJointWindow>((p) => { return p.GridCombobox.RowDefinitions.Count != 0; }, (p) =>
            {
                TransactionGroup.Start("Action");
                for (int i = 0; i < JointRules.Count; i++)
                {
                    if (JointRules[i].Joint.SelectedValue.ToString().Equals("Structural Floor") || JointRules[i].BeJointed.SelectedValue.ToString().Equals("Structural Floor"))
                    {
                        UnJointFloor(p, JointRules[i], i);
                    }
                    else
                    {
                        List<Element> jointElement = GetElementsJoint(JointRules[i].Joint.SelectedValue.ToString());
                        if (jointElement.Count != 0)
                        {
                            foreach (var itemJoint in jointElement)
                            {
                                if (TransactionGroup.HasStarted())
                                {
                                    List<Element> beJointElement = GetElementsBeJointed(itemJoint, JointRules[i].BeJointed.SelectedValue.ToString());

                                    p.ProgressBar.Maximum = beJointElement.Count;
                                    double value = 0;

                                    if (beJointElement.Count != 0)
                                    {
                                        foreach (var itemBejointed in beJointElement)
                                        {
                                            value += 1;
                                            Percent = value / p.ProgressBar.Maximum * 100;
                                            p.ProgressBar.Dispatcher.Invoke(() => p.ProgressBar.Value = value, DispatcherPriority.Background);
                                            using (Transaction transaction = new Transaction(Doc))
                                            {
                                                transaction.Start("Action" + i);
                                                if (JoinGeometryUtils.AreElementsJoined(Doc, itemJoint, itemBejointed))
                                                {
                                                    JoinGeometryUtils.UnjoinGeometry(Doc, itemJoint, itemBejointed);
                                                }
                                                transaction.Commit();
                                            }
                                        }
                                    }

                                }
                            }
                        }
                    }
                }


                if (TransactionGroup.HasStarted())
                {
                    TransactionGroup.Commit();
                    p.DialogResult = true;
                    System.Windows.MessageBox.Show("Unjoin was successful!",
                        "Unjoin", MessageBoxButton.OK,
                        MessageBoxImage.Information);
                }
            });
            DeleteRuleCommand = new RelayCommand<AutoJointWindow>((p) => { return p.GridCombobox.RowDefinitions.Count != 0; }, (p) =>
            {
                p.GridCombobox.RowDefinitions.Clear();
                p.GridCombobox.Children.Clear();
                JointRules.Clear();
            });
            AddRuleCommand = new RelayCommand<AutoJointWindow>((p) => { return true; }, (p) =>
            {
                var a = new JointRule(AllCategory);
                a.SetRuleToGrid(p);
                JointRules.Add(a);
            });
        }

        private List<string> AllCategory = new List<string>() {
            "Structural Framing",
            "Structural Columns",
            "Structural Floor",
            "Structural Wall",
            "Structural Foundation",
        };
        #region Join
        private void JointFloor(AutoJointWindow p, JointRule jointRule, int i)
        {
            List<Element> floors = GetElementsJoint("Structural Floor");
            if (floors.Count != 0)
            {
                foreach (var item1 in floors)
                {

                    if (jointRule.Joint.SelectedValue.ToString().Equals("Structural Floor"))
                    {
                        List<Element> elements = GetElementsBeJointed(item1, jointRule.BeJointed.SelectedValue.ToString());
                        if (elements.Count != 0)
                        {
                            if (TransactionGroup.HasStarted())
                            {
                                p.ProgressBar.Maximum = elements.Count;
                                double value = 0;
                                foreach (var item2 in elements)
                                {
                                    value += 1;
                                    Percent = value / p.ProgressBar.Maximum * 100;
                                    p.ProgressBar.Dispatcher.Invoke(() => p.ProgressBar.Value = value, DispatcherPriority.Background);
                                    using (Transaction transaction = new Transaction(Doc))
                                    {
                                        transaction.Start("Action" + i);
                                        FailureHandlingOptions option = transaction.GetFailureHandlingOptions();
                                        option.SetFailuresPreprocessor(new DeleteWarningSuper());
                                        transaction.SetFailureHandlingOptions(option);
                                        if (JoinGeometryUtils.AreElementsJoined(Doc, item1, item2))
                                        {
                                            if (!JoinGeometryUtils.IsCuttingElementInJoin(Doc, item1, item2))
                                            {
                                                JoinGeometryUtils.SwitchJoinOrder(Doc, item2, item1);
                                            }
                                        }
                                        else
                                        {
                                            JoinGeometryUtils.JoinGeometry(Doc, item1, item2);
                                            if (!JoinGeometryUtils.IsCuttingElementInJoin(Doc, item1, item2))
                                            {
                                                JoinGeometryUtils.SwitchJoinOrder(Doc, item2, item1);
                                            }
                                        }

                                        transaction.Commit();
                                    }

                                }
                            }
                        }
                    }
                    else
                    {
                        List<Element> elements = GetElementsBeJointed(item1, jointRule.Joint.SelectedValue.ToString());
                        if (elements.Count != 0)
                        {
                            if (TransactionGroup.HasStarted())
                            {
                                p.ProgressBar.Maximum = elements.Count;
                                double value = 0;
                                foreach (var item2 in elements)
                                {
                                    value += 1;
                                    Percent = value / p.ProgressBar.Maximum * 100;
                                    p.ProgressBar.Dispatcher.Invoke(() => p.ProgressBar.Value = value, DispatcherPriority.Background);
                                    using (Transaction transaction = new Transaction(Doc))
                                    {
                                        transaction.Start("Action" + i);
                                        FailureHandlingOptions option = transaction.GetFailureHandlingOptions();
                                        option.SetFailuresPreprocessor(new DeleteWarningSuper());
                                        transaction.SetFailureHandlingOptions(option);
                                        if (JoinGeometryUtils.AreElementsJoined(Doc, item2, item1))
                                        {
                                            if (!JoinGeometryUtils.IsCuttingElementInJoin(Doc, item2, item1))
                                            {
                                                JoinGeometryUtils.SwitchJoinOrder(Doc, item1, item2);
                                            }
                                        }
                                        else
                                        {
                                            JoinGeometryUtils.JoinGeometry(Doc, item2, item1);
                                            if (!JoinGeometryUtils.IsCuttingElementInJoin(Doc, item2, item1))
                                            {
                                                JoinGeometryUtils.SwitchJoinOrder(Doc, item1, item2);
                                            }
                                        }

                                        transaction.Commit();
                                    }

                                }
                            }
                        }
                    }

                }

            }
        }
        private void UnJointFloor(AutoJointWindow p, JointRule jointRule, int i)
        {
            List<Element> floors = GetElementsJoint("Structural Floor");
            if (floors.Count != 0)
            {
                foreach (var item1 in floors)
                {
                    string a = (jointRule.Joint.SelectedValue.ToString().Equals("Structural Floor")) ? jointRule.BeJointed.SelectedValue.ToString() : jointRule.Joint.SelectedValue.ToString();
                    if (jointRule.Joint.SelectedValue.ToString().Equals("Structural Floor"))
                    {
                        List<Element> elements = GetElementsBeJointed(item1, jointRule.BeJointed.SelectedValue.ToString());
                        if (elements.Count != 0)
                        {
                            if (TransactionGroup.HasStarted())
                            {
                                p.ProgressBar.Maximum = elements.Count;
                                double value = 0;
                                foreach (var item2 in elements)
                                {
                                    value += 1;
                                    Percent = value / p.ProgressBar.Maximum * 100;
                                    p.ProgressBar.Dispatcher.Invoke(() => p.ProgressBar.Value = value, DispatcherPriority.Background);
                                    using (Transaction transaction = new Transaction(Doc))
                                    {
                                        transaction.Start("Action" + i);
                                        FailureHandlingOptions option = transaction.GetFailureHandlingOptions();
                                        option.SetFailuresPreprocessor(new DeleteWarningSuper());
                                        transaction.SetFailureHandlingOptions(option);
                                        if (JoinGeometryUtils.AreElementsJoined(Doc, item1, item2))
                                        {
                                            JoinGeometryUtils.UnjoinGeometry(Doc, item1, item2);
                                        }
                                        transaction.Commit();
                                    }

                                }
                            }
                        }
                    }
                    else
                    {
                        List<Element> elements = GetElementsBeJointed(item1, jointRule.Joint.SelectedValue.ToString());
                        if (elements.Count != 0)
                        {
                            if (TransactionGroup.HasStarted())
                            {
                                p.ProgressBar.Maximum = elements.Count;
                                double value = 0;
                                foreach (var item2 in elements)
                                {
                                    value += 1;
                                    Percent = value / p.ProgressBar.Maximum * 100;
                                    p.ProgressBar.Dispatcher.Invoke(() => p.ProgressBar.Value = value, DispatcherPriority.Background);
                                    using (Transaction transaction = new Transaction(Doc))
                                    {
                                        transaction.Start("Action" + i);
                                        FailureHandlingOptions option = transaction.GetFailureHandlingOptions();
                                        option.SetFailuresPreprocessor(new DeleteWarningSuper());
                                        transaction.SetFailureHandlingOptions(option);
                                        if (JoinGeometryUtils.AreElementsJoined(Doc, item1, item2))
                                        {
                                            JoinGeometryUtils.UnjoinGeometry(Doc, item1, item2);
                                        }
                                        transaction.Commit();
                                    }

                                }
                            }
                        }
                    }

                }
            }
        }
        #endregion
        private bool ConditionJointRule()
        {
            if (JointRules.Count == 0)
            {
                return false;
            }
            else
            {
                for (int i = 0; i < JointRules.Count; i++)
                {
                    if (JointRules[i].Joint.SelectedValue.ToString().Equals(JointRules[i].BeJointed.SelectedValue.ToString()))
                    {
                        return false;
                    }
                }
            }
            return true;
        }
        #region GetAllElement
        private List<Element> GetElementsJoint(string joint)
        {
            List<Element> beams;
            List<Element> columns;
            List<Element> foundations;
            List<Element> floors;
            List<Element> walls;
            if (IsCurrenView)
            {
                GetAllElementsCurrentView(out beams, out columns, out floors, out walls, out foundations);
            }
            else
            {
                GetAllElementsProject(out beams, out columns, out floors, out walls, out foundations);
            }
            switch (joint)
            {
                case "Structural Framing": return beams;
                case "Structural Columns": return columns;
                case "Structural Floor": return floors;
                case "Structural Wall": return walls;
                case "Structural Foundation": return foundations;
                default: return beams;
            }
        }

        private void GetAllElementsProject(out List<Element> beams, out List<Element> columns, out List<Element> floors, out List<Element> walls, out List<Element> foundations)
        {
            beams = new FilteredElementCollector(Doc).WhereElementIsNotElementType().OfCategory(BuiltInCategory.OST_StructuralFraming).ToList();
            columns = new FilteredElementCollector(Doc).WhereElementIsNotElementType().OfCategory(BuiltInCategory.OST_StructuralColumns).ToList();
            foundations = new FilteredElementCollector(Doc).WhereElementIsNotElementType().OfCategory(BuiltInCategory.OST_StructuralFoundation).ToList();
            floors = new FilteredElementCollector(Doc).WhereElementIsNotElementType().OfClass(typeof(Floor)).Where(x => x.get_Parameter(BuiltInParameter.FLOOR_PARAM_IS_STRUCTURAL).AsInteger() == 1).ToList();
            walls = new FilteredElementCollector(Doc).WhereElementIsNotElementType().OfClass(typeof(Wall)).Where(x => x.get_Parameter(BuiltInParameter.WALL_STRUCTURAL_SIGNIFICANT).AsInteger() == 1).ToList();
        }
        private void GetAllElementsCurrentView(out List<Element> beams, out List<Element> columns, out List<Element> floors, out List<Element> walls, out List<Element> foundations)
        {
            beams = new FilteredElementCollector(Doc, Doc.ActiveView.Id).WhereElementIsNotElementType().OfCategory(BuiltInCategory.OST_StructuralFraming).ToList();
            columns = new FilteredElementCollector(Doc, Doc.ActiveView.Id).WhereElementIsNotElementType().OfCategory(BuiltInCategory.OST_StructuralColumns).ToList();
            foundations = new FilteredElementCollector(Doc, Doc.ActiveView.Id).WhereElementIsNotElementType().OfCategory(BuiltInCategory.OST_StructuralFoundation).ToList();
            floors = new FilteredElementCollector(Doc, Doc.ActiveView.Id).WhereElementIsNotElementType().OfClass(typeof(Floor)).Where(x => x.get_Parameter(BuiltInParameter.FLOOR_PARAM_IS_STRUCTURAL).AsInteger() == 1).ToList();
            walls = new FilteredElementCollector(Doc, Doc.ActiveView.Id).WhereElementIsNotElementType().OfClass(typeof(Wall)).Where(x => x.get_Parameter(BuiltInParameter.WALL_STRUCTURAL_SIGNIFICANT).AsInteger() == 1).ToList();
        }
        #endregion
        #region GetAllElementBeJointed
        private List<Element> GetElementsBeJointed(Element element, string bejointed)
        {
            switch (bejointed)
            {
                case "Structural Framing": return GetBeamsBeJointed(element);
                case "Structural Columns": return GetColumnsBeJointed(element);
                case "Structural Floor": return GetFloorsBeJointed(element);
                case "Structural Wall": return GetWallsBeJointed(element);
                case "Structural Foundation": return GetFoundationsBeJointed(element);
                default: return GetBeamsBeJointed(element);
            }
        }
        private List<Element> GetBeamsBeJointed(Element element)
        {
            List<ElementId> elementIds = new List<ElementId>();
            elementIds.Add(element.Id);
            ElementCategoryFilter categoryFilter
                       = new ElementCategoryFilter(BuiltInCategory.OST_StructuralFraming);

            BoundingBoxXYZ box = element.get_BoundingBox(null);
            Outline outline = new Outline(box.Min, box.Max);
            BoundingBoxIntersectsFilter bbFilter
                 = new BoundingBoxIntersectsFilter(outline);

            LogicalAndFilter logicalAndFilter
                = new LogicalAndFilter(bbFilter, categoryFilter);

            List<Element> beams
                = new FilteredElementCollector(Doc)
                    .WherePasses(logicalAndFilter)
                    .WherePasses(new ExclusionFilter(elementIds))
                    .ToList();
            beams = beams.Distinct(new DistinctID()).ToList();
            return beams;
        }
        private List<Element> GetColumnsBeJointed(Element element)
        {
            List<ElementId> elementIds = new List<ElementId>();
            elementIds.Add(element.Id);
            ElementCategoryFilter categoryFilter
                       = new ElementCategoryFilter(BuiltInCategory.OST_StructuralColumns);

            BoundingBoxXYZ box = element.get_BoundingBox(null);
            Outline outline = new Outline(box.Min, box.Max);
            BoundingBoxIntersectsFilter bbFilter
                 = new BoundingBoxIntersectsFilter(outline);

            LogicalAndFilter logicalAndFilter
                = new LogicalAndFilter(bbFilter, categoryFilter);

            List<Element> columns
                = new FilteredElementCollector(Doc)
                    .WherePasses(logicalAndFilter)
                    .WherePasses(new ExclusionFilter(elementIds))
                    .ToList();
            columns = columns.Distinct(new DistinctID()).ToList();
            return columns;
        }
        private List<Element> GetFoundationsBeJointed(Element element)
        {
            List<ElementId> elementIds = new List<ElementId>();
            elementIds.Add(element.Id);
            ElementCategoryFilter categoryFilter
                       = new ElementCategoryFilter(BuiltInCategory.OST_StructuralFoundation);

            BoundingBoxXYZ box = element.get_BoundingBox(null);
            Outline outline = new Outline(box.Min, box.Max);
            BoundingBoxIntersectsFilter bbFilter
                 = new BoundingBoxIntersectsFilter(outline);

            LogicalAndFilter logicalAndFilter
                = new LogicalAndFilter(bbFilter, categoryFilter);

            List<Element> beams
                = new FilteredElementCollector(Doc)
                    .WherePasses(logicalAndFilter)
                    .WherePasses(new ExclusionFilter(elementIds))
                    .ToList();
            beams = beams.Distinct(new DistinctID()).ToList();
            return beams;
        }
        private List<Element> GetFloorsBeJointed(Element element)
        {
            List<ElementId> elementIds = new List<ElementId>();
            elementIds.Add(element.Id);
            ElementCategoryFilter categoryFilter
                       = new ElementCategoryFilter(BuiltInCategory.OST_Floors);

            BoundingBoxXYZ box = element.get_BoundingBox(null);
            Outline outline = new Outline(box.Min, box.Max);
            BoundingBoxIntersectsFilter bbFilter
                 = new BoundingBoxIntersectsFilter(outline);

            LogicalAndFilter logicalAndFilter
                = new LogicalAndFilter(bbFilter, categoryFilter);

            List<Element> beams
                = new FilteredElementCollector(Doc)
                    .WherePasses(logicalAndFilter)
                    .WherePasses(new ExclusionFilter(elementIds))
                    .Where(x => x.get_Parameter(BuiltInParameter.FLOOR_PARAM_IS_STRUCTURAL).AsInteger() == 1)
                    .ToList();
            beams = beams.Distinct(new DistinctID()).ToList();
            return beams;
        }
        private List<Element> GetWallsBeJointed(Element element)
        {
            List<ElementId> elementIds = new List<ElementId>();
            elementIds.Add(element.Id);
            ElementCategoryFilter categoryFilter
                       = new ElementCategoryFilter(BuiltInCategory.OST_Walls);

            BoundingBoxXYZ box = element.get_BoundingBox(null);
            Outline outline = new Outline(box.Min, box.Max);
            BoundingBoxIntersectsFilter bbFilter
                 = new BoundingBoxIntersectsFilter(outline);

            LogicalAndFilter logicalAndFilter
                = new LogicalAndFilter(bbFilter, categoryFilter);

            List<Element> beams
                = new FilteredElementCollector(Doc)
                    .WherePasses(logicalAndFilter)
                    .WherePasses(new ExclusionFilter(elementIds))
                    .Where(x => x.get_Parameter(BuiltInParameter.WALL_STRUCTURAL_SIGNIFICANT).AsInteger() == 1)
                    .ToList();
            beams = beams.Distinct(new DistinctID()).ToList();
            return beams;
        }
        #endregion
        private void DrawBeamsRebar(System.Windows.Controls.Canvas canvas)
        {
            DrawImage.DrawBeamsRebar(canvas);
        }
        private void DrawColumnRebar(System.Windows.Controls.Canvas canvas)
        {
            DrawImage.DrawColumnRebar(canvas);
        }
        private void DrawFootingRebar(System.Windows.Controls.Canvas canvas)
        {
            DrawImage.DrawFootingRebar(canvas);
        }
        private void DrawContinueFootingRebar(System.Windows.Controls.Canvas canvas)
        {
            DrawImage.DrawContinueFootingRebar(canvas);
        }
        private void DrawSlabRebar(System.Windows.Controls.Canvas canvas)
        {
            DrawImage.DrawSlabRebar(canvas);
        }
        private void DrawStairRebar(System.Windows.Controls.Canvas canvas)
        {
            DrawImage.DrawStairRebar(canvas);
        }
        private void DrawWallsRebar(System.Windows.Controls.Canvas canvas)
        {
            DrawImage.DrawWallsRebar(canvas);
        }
        private void DrawWallShear(System.Windows.Controls.Canvas canvas)
        {
            DrawImage.DrawWallShear(canvas);
        }
        private void DrawPileCap(System.Windows.Controls.Canvas canvas)
        {
            DrawImage.DrawPileCap(canvas);
        }
        private void DrawAutoJoint(System.Windows.Controls.Canvas canvas)
        {
            DrawImage.DrawAutoJoint(canvas);
        }
        private void ConvertCanvasToBitmap(System.Windows.Controls.Canvas canvas, string filename)
        {
            double actualWidth = canvas.RenderSize.Width;
            double actualHeight = canvas.RenderSize.Height;
            RenderTargetBitmap renderBitmap = new RenderTargetBitmap((int)actualWidth, (int)actualHeight, 96d, 96d, PixelFormats.Pbgra32);

            //RenderTargetBitmap renderBitmap = new RenderTargetBitmap(1800, 200,96d, 96d, PixelFormats.Pbgra32);
            // needed otherwise the image output is black

            canvas.Measure(new Size((int)actualWidth, (int)actualHeight));
            canvas.Arrange(new Rect(new Size((int)actualWidth, (int)actualHeight)));

            renderBitmap.Render(canvas);
            RenderOptions.SetBitmapScalingMode(renderBitmap, BitmapScalingMode.HighQuality);
            //JpegBitmapEncoder encoder = new JpegBitmapEncoder();

            // for png bitmap
            PngBitmapEncoder encoder = new PngBitmapEncoder();

            encoder.Frames.Add(BitmapFrame.Create(renderBitmap));

            using (FileStream fs = File.Create(filename))
            {
                encoder.Save(fs);
            }
        }
    }



}
