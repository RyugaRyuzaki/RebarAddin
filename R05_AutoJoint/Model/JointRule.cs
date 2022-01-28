using Autodesk.Revit.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace R05_AutoJoint
{
    public class JointRule : BaseViewModel
    {
        private ComboBox _Joint;
        public ComboBox Joint { get => _Joint; set { _Joint = value; OnPropertyChanged(); } }
        private ComboBox _BeJointed;
        public ComboBox BeJointed { get => _BeJointed; set { _BeJointed = value; OnPropertyChanged(); } }
        /// <summary>
        /// Tạo class 2 comboBox, hàm dựng gán Itemsource
        /// </summary>
        /// <param name="allCategory"></param>
        public JointRule(List<string> allCategory)
        {
            Joint = new ComboBox();
            Joint.ItemsSource = allCategory;
            Joint.SelectedIndex = 0;
            BeJointed = new ComboBox();
            BeJointed.ItemsSource = allCategory;
            BeJointed.SelectedIndex = 0;

        }
        /// <summary>
        /// Set các Controls vào trong Grid của 1 Window or UserControls
        /// </summary>
        /// <param name="p"></param>
        public void SetRuleToGrid(AutoJointWindow p)
        {
            var a = new System.Windows.Controls.RowDefinition();
            a.Height = new System.Windows.GridLength(30);
            p.GridCombobox.RowDefinitions.Add(a);
            Style style = p.FindResource("ComboBoxStyle") as Style;
            Joint.Style = style;
            BeJointed.Style = style;
            System.Windows.Controls.Grid.SetRow(Joint, p.GridCombobox.RowDefinitions.Count - 1);
            System.Windows.Controls.Grid.SetColumn(Joint, 0);
            System.Windows.Controls.Grid.SetRow(BeJointed, p.GridCombobox.RowDefinitions.Count - 1);
            System.Windows.Controls.Grid.SetColumn(BeJointed, 1);
            p.GridCombobox.Children.Add(Joint);
            p.GridCombobox.Children.Add(BeJointed);
        }
        //private BuiltInCategory GetCategoryJoint()
        //{
        //    switch (Joint.SelectedValue.ToString())
        //    {
        //        case "Structural Framing":
        //            return BuiltInCategory.OST_StructuralFraming;
        //        case "Structural Columns":
        //            return BuiltInCategory.OST_StructuralFraming;
        //        case "Structural Floor":
        //            return BuiltInCategory.OST_StructuralFraming;
        //        case "Structural Wall":
        //            return BuiltInCategory.OST_StructuralFraming;
        //        case "Structural Foundation":
        //            return BuiltInCategory.OST_StructuralFraming;
        //        default:
        //            return BuiltInCategory.OST_StructuralFraming;
        //    }
        //}
        //private BuiltInCategory GetCategoryBeJointed()
        //{
        //    switch (BeJointed.SelectedValue.ToString())
        //    {
        //        case "Structural Framing":
        //            return BuiltInCategory.OST_StructuralFraming;
        //        case "Structural Columns":
        //            return BuiltInCategory.OST_StructuralColumns;
        //        case "Structural Floor":
        //            return BuiltInCategory.OST_Floors;
        //        case "Structural Wall":
        //            return BuiltInCategory.OST_Walls;
        //        case "Structural Foundation":
        //            return BuiltInCategory.OST_StructuralFoundation;
        //        default:
        //            return BuiltInCategory.OST_StructuralFraming;
        //    }
        //}

        
    }
}
