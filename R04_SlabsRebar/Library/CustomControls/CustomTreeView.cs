using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Collections;
using System.Windows.Media;

namespace R04_SlabsRebar
{
    public class CustomTreeView : DependencyObject
    {
        public static bool GetEnableMultiSelect(DependencyObject obj)
        {
            return (bool)obj.GetValue(EnableMultiSelectProperty);
        }

        public static void SetEnableMultiSelect(DependencyObject obj, bool value)
        {
            obj.SetValue(EnableMultiSelectProperty, value);
        }

        // Using a DependencyProperty as the backing store for EnableMultiSelect.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty EnableMultiSelectProperty =
            DependencyProperty.RegisterAttached("EnableMultiSelect", typeof(bool), typeof(CustomTreeView),
                new FrameworkPropertyMetadata(false)
                {
                    PropertyChangedCallback = EnableMultiSelectChanged,
                    BindsTwoWayByDefault = true
                });

        public static IList GetSelectedItems(DependencyObject obj)
        {
            return (IList)obj.GetValue(SelectedItemsProperty);
        }

        public static void SetSelectedItems(DependencyObject obj, IList value)
        {
            obj.SetValue(SelectedItemsProperty, value);
        }

        // Using a DependencyProperty as the backing store for SelectedItems.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SelectedItemsProperty =
            DependencyProperty.RegisterAttached("SelectedItems", typeof(IList), typeof(CustomTreeView),
                new PropertyMetadata(null));



        static TreeViewItem GetAnchorItem(DependencyObject obj)
        {
            return (TreeViewItem)obj.GetValue(AnchorItemProperty);
        }

        static void SetAnchorItem(DependencyObject obj, TreeViewItem value)
        {
            obj.SetValue(AnchorItemProperty, value);
        }

        // Using a DependencyProperty as the backing store for AnchorItem.  This enables animation, styling, binding, etc...
        static readonly DependencyProperty AnchorItemProperty =
            DependencyProperty.RegisterAttached("AnchorItem", typeof(TreeViewItem), typeof(CustomTreeView),
                new PropertyMetadata(null));



        static void EnableMultiSelectChanged(DependencyObject s, DependencyPropertyChangedEventArgs args)
        {
            TreeView tree = (TreeView)s;
            var wasEnable = (bool)args.OldValue;
            var isEnabled = (bool)args.NewValue;
            if (wasEnable)
            {
                tree.RemoveHandler(TreeViewItem.MouseDownEvent, new MouseButtonEventHandler(ItemClicked));
                tree.RemoveHandler(TreeView.KeyDownEvent, new KeyEventHandler(KeyDown));
            }
            if (isEnabled)
            {
                tree.AddHandler(TreeViewItem.MouseDownEvent, new MouseButtonEventHandler(ItemClicked), true);
                tree.AddHandler(TreeView.KeyDownEvent, new KeyEventHandler(KeyDown));
            }
        }

        static TreeView GetTree(TreeViewItem item)
        {
            Func<DependencyObject, DependencyObject> getParent = (o) => VisualTreeHelper.GetParent(o);
            FrameworkElement currentItem = item;
            while (!(getParent(currentItem) is TreeView))
                currentItem = (FrameworkElement)getParent(currentItem);
            return (TreeView)getParent(currentItem);
        }



        static void RealSelectedChanged(DependencyObject sender, DependencyPropertyChangedEventArgs args)
        {
            TreeViewItem item = (TreeViewItem)sender;
            var selectedItems = GetSelectedItems(GetTree(item));
            if (selectedItems != null)
            {
                var isSelected = GetIsSelected(item);
                if (isSelected)
                    try
                    {
                        selectedItems.Add(item.Header);
                    }
                    catch (ArgumentException)
                    {
                    }
                else
                    selectedItems.Remove(item.Header);
            }
        }

        static void KeyDown(object sender, KeyEventArgs e)
        {
            TreeView tree = (TreeView)sender;
            if (e.Key == Key.A && e.KeyboardDevice.Modifiers == ModifierKeys.Control)
            {
                foreach (var item in GetExpandedTreeViewItems(tree))
                {
                    SetIsSelected(item, true);
                }
                e.Handled = true;
            }
        }

        static void ItemClicked(object sender, MouseButtonEventArgs e)
        {
            TreeViewItem item = FindTreeViewItem(e.OriginalSource);
            if (item == null)
                return;
            TreeView tree = (TreeView)sender;

            var mouseButton = e.ChangedButton;
            if (mouseButton != MouseButton.Left)
            {
                if ((mouseButton == MouseButton.Right) &&
                    ((Keyboard.Modifiers & (ModifierKeys.Shift | ModifierKeys.Control)) == ModifierKeys.None))
                {
                    if (GetIsSelected(item))
                    {
                        UpdateAnchorAndActionItem(tree, item);
                        return;
                    }
                    MakeSingleSelection(tree, item);
                }
                return;
            }
            if (mouseButton != MouseButton.Left)
            {
                if ((mouseButton == MouseButton.Right) &&
                    ((Keyboard.Modifiers & (ModifierKeys.Shift | ModifierKeys.Control)) == ModifierKeys.None))
                {
                    if (GetIsSelected(item))
                    {
                        UpdateAnchorAndActionItem(tree, item);
                        return;
                    }
                    MakeSingleSelection(tree, item);
                }
                return;
            }
            if ((Keyboard.Modifiers & (ModifierKeys.Shift | ModifierKeys.Control)) !=
                (ModifierKeys.Shift | ModifierKeys.Control))
            {
                if ((Keyboard.Modifiers & ModifierKeys.Control) == ModifierKeys.Control)
                {
                    MakeToggleSelection(tree, item);
                    return;
                }
                if ((Keyboard.Modifiers & ModifierKeys.Shift) == ModifierKeys.Shift)
                {
                    MakeAnchorSelection(tree, item, true);
                    return;
                }
                MakeSingleSelection(tree, item);
                return;
            }
            //MakeAnchorSelection(item, false);


            //SetIsSelected(tree.SelectedItem
        }

        private static TreeViewItem FindTreeViewItem(object obj)
        {
            DependencyObject dpObj = obj as DependencyObject;
            if (dpObj == null)
                return null;
            if (dpObj is TreeViewItem)
                return (TreeViewItem)dpObj;
            return FindTreeViewItem(VisualTreeHelper.GetParent(dpObj));
        }



        private static IEnumerable<TreeViewItem> GetExpandedTreeViewItems(ItemsControl tree)
        {
            for (int i = 0; i < tree.Items.Count; i++)
            {
                var item = (TreeViewItem)tree.ItemContainerGenerator.ContainerFromIndex(i);
                if (item == null)
                    continue;
                yield return item;
                if (item.IsExpanded)
                    foreach (var subItem in GetExpandedTreeViewItems(item))
                        yield return subItem;
            }
        }

        private static void MakeAnchorSelection(TreeView tree, TreeViewItem actionItem, bool clearCurrent)
        {
            if (GetAnchorItem(tree) == null)
            {
                var selectedItems = GetSelectedTreeViewItems(tree);
                if (selectedItems.Count > 0)
                {
                    SetAnchorItem(tree, selectedItems[selectedItems.Count - 1]);
                }
                else
                {
                    SetAnchorItem(tree, GetExpandedTreeViewItems(tree).Skip(3).FirstOrDefault());
                }
                if (GetAnchorItem(tree) == null)
                {
                    return;
                }
            }

            var anchor = GetAnchorItem(tree);

            var items = GetExpandedTreeViewItems(tree);
            bool betweenBoundary = false;
            bool end = false;
            foreach (var item in items)
            {
                bool isBoundary = item == anchor || item == actionItem;
                if (isBoundary)
                {
                    betweenBoundary = !betweenBoundary;
                }
                if (betweenBoundary || isBoundary)
                    SetIsSelected(item, true);
                else if (clearCurrent)
                    SetIsSelected(item, false);
                else
                    break;

            }
        }

        private static List<TreeViewItem> GetSelectedTreeViewItems(TreeView tree)
        {
            return GetExpandedTreeViewItems(tree).Where(i => GetIsSelected(i)).ToList();
        }

        private static void MakeSingleSelection(TreeView tree, TreeViewItem item)
        {
            foreach (TreeViewItem selectedItem in GetExpandedTreeViewItems(tree))
            {
                if (selectedItem == null)
                    continue;
                if (selectedItem != item)
                    SetIsSelected(selectedItem, false);
                else
                {
                    SetIsSelected(selectedItem, true);
                }
            }
            UpdateAnchorAndActionItem(tree, item);
        }

        private static void MakeToggleSelection(TreeView tree, TreeViewItem item)
        {
            SetIsSelected(item, !GetIsSelected(item));
            UpdateAnchorAndActionItem(tree, item);
        }

        private static void UpdateAnchorAndActionItem(TreeView tree, TreeViewItem item)
        {
            SetAnchorItem(tree, item);
        }




        public static bool GetIsSelected(DependencyObject obj)
        {
            return (bool)obj.GetValue(IsSelectedProperty);
        }

        public static void SetIsSelected(DependencyObject obj, bool value)
        {
            obj.SetValue(IsSelectedProperty, value);
        }

        // Using a DependencyProperty as the backing store for IsSelected.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IsSelectedProperty =
            DependencyProperty.RegisterAttached("IsSelected", typeof(bool), typeof(CustomTreeView),
                new PropertyMetadata(false)
                {
                    PropertyChangedCallback = RealSelectedChanged
                });

    }
}






//}
//public sealed class CustomTreeView : TreeView
//{
//    #region Fields

//    // Used in shift selections
//    private TreeViewItem _lastItemSelected;

//    #endregion Fields
//    #region Dependency Properties

//    public static readonly DependencyProperty IsItemSelectedProperty =
//        DependencyProperty.RegisterAttached("IsItemSelected", typeof(bool), typeof(CustomTreeView));

//    public static void SetIsItemSelected(UIElement element, bool value)
//    {
//        element.SetValue(IsItemSelectedProperty, value);
//    }
//    public static bool GetIsItemSelected(UIElement element)
//    {
//        return (bool) element.GetValue(IsItemSelectedProperty);
//    }

//    #endregion Dependency Properties
//    #region Properties

//    private static bool IsCtrlPressed
//    {
//        get { return Keyboard.IsKeyDown(Key.LeftCtrl) || Keyboard.IsKeyDown(Key.RightCtrl); }
//    }
//    private static bool IsShiftPressed
//    {
//        get { return Keyboard.IsKeyDown(Key.LeftShift) || Keyboard.IsKeyDown(Key.RightShift); }
//    }

//    public IList SelectedItems
//    {
//        get
//        {
//            var selectedTreeViewItems = GetTreeViewItems(this, true).Where(GetIsItemSelected);
//            var selectedModelItems = selectedTreeViewItems.Select(treeViewItem => treeViewItem.Header);

//            return selectedModelItems.ToList();
//        }
//    }

//    #endregion Properties
//    #region Event Handlers

//    protected override void OnPreviewMouseDown(MouseButtonEventArgs e)
//    {
//        base.OnPreviewMouseDown(e);

//        // If clicking on a tree branch expander...
//        if (e.OriginalSource is Shape || e.OriginalSource is Grid || e.OriginalSource is Border)
//            return;

//        var item = GetTreeViewItemClicked((FrameworkElement) e.OriginalSource);
//        if (item != null) SelectedItemChangedInternal(item);
//    }

//    #endregion Event Handlers
//    #region Utility Methods

//    private void SelectedItemChangedInternal(TreeViewItem tvItem)
//    {
//        // Clear all previous selected item states if ctrl is NOT being held down
//        if (!IsCtrlPressed)
//        {
//            var items = GetTreeViewItems(this, true);
//            foreach (var treeViewItem in items)
//                SetIsItemSelected(treeViewItem, false);
//        }

//        // Is this an item range selection?
//        if (IsShiftPressed && _lastItemSelected != null)
//        {
//            var items = GetTreeViewItemRange(_lastItemSelected, tvItem);
//            if (items.Count > 0)
//            {
//                foreach (var treeViewItem in items)
//                    SetIsItemSelected(treeViewItem, true);

//                _lastItemSelected = items.Last();
//            }
//        }
//        // Otherwise, individual selection
//        else
//        {
//            SetIsItemSelected(tvItem, true);
//            _lastItemSelected = tvItem;
//        }
//    }
//    private static TreeViewItem GetTreeViewItemClicked(DependencyObject sender)
//    {
//        while (sender != null && !(sender is TreeViewItem))
//            sender = VisualTreeHelper.GetParent(sender);
//        return sender as TreeViewItem;
//    }
//    private static List<TreeViewItem> GetTreeViewItems(ItemsControl parentItem, bool includeCollapsedItems, List<TreeViewItem> itemList = null)
//    {
//        if (itemList == null)
//            itemList = new List<TreeViewItem>();

//        for (var index = 0; index < parentItem.Items.Count; index++)
//        {
//            var tvItem = parentItem.ItemContainerGenerator.ContainerFromIndex(index) as TreeViewItem;
//            if (tvItem == null) continue;

//            itemList.Add(tvItem);
//            if (includeCollapsedItems || tvItem.IsExpanded)
//                GetTreeViewItems(tvItem, includeCollapsedItems, itemList);
//        }
//        return itemList;
//    }
//    private List<TreeViewItem> GetTreeViewItemRange(TreeViewItem start, TreeViewItem end)
//    {
//        var items = GetTreeViewItems(this, false);

//        var startIndex = items.IndexOf(start);
//        var endIndex = items.IndexOf(end);
//        var rangeStart = startIndex > endIndex || startIndex == -1 ? endIndex : startIndex;
//        var rangeCount = startIndex > endIndex ? startIndex - endIndex + 1 : endIndex - startIndex + 1;

//        if (startIndex == -1 && endIndex == -1)
//            rangeCount = 0;

//        else if (startIndex == -1 || endIndex == -1)
//            rangeCount = 1;

//        return rangeCount > 0 ? items.GetRange(rangeStart, rangeCount) : new List<TreeViewItem>();
//    }

//    #endregion Utility Methods
//}
//}
