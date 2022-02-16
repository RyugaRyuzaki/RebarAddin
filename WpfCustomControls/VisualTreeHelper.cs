using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Windows;

using WPFVisualTreeHelper = System.Windows.Media.VisualTreeHelper;

namespace WpfCustomControls
{
    public static class VisualTreeHelper
    {
        public static T FindVisualChild<T>(this DependencyObject source, string name) where T : FrameworkElement
        {
            return GetAllVisualChildren(source).OfType<T>().Single(x => x.Name == name);
        }
        public static T FindVisualChildTag<T>(this DependencyObject source, string tag) where T : FrameworkElement
        {
            return GetAllVisualChildren(source).OfType<T>().Single(x => x.Tag == tag as Object);
        }
        public static IEnumerable<T> GetVisualChildren<T>(this DependencyObject source) where T : DependencyObject
        {
            if (source == null) throw new ArgumentNullException(nameof(source));

            return GetVisualChildrenImplementation();

            IEnumerable<T> GetVisualChildrenImplementation()
            {
                int count = WPFVisualTreeHelper.GetChildrenCount(source);
                for (int i = 0; i < count; i++)
                {
                    if (WPFVisualTreeHelper.GetChild(source, i) is T child)
                    {
                        yield return child;
                    }
                }
            }
        }

        private static IEnumerable<DependencyObject> GetAllVisualChildren(DependencyObject source)
        {
            var stack = new Queue<DependencyObject>();
            stack.Enqueue(source);

            while (stack.Any())
            {
                DependencyObject current = stack.Dequeue();
                int childCount = WPFVisualTreeHelper.GetChildrenCount(current);
                for (int i = 0; i < childCount; i++)
                {
                    var child = WPFVisualTreeHelper.GetChild(current, i);
                    yield return child;
                    stack.Enqueue(child);
                }
            }
        }
        public static T FindChild<T>(DependencyObject parent, string childName) where T : DependencyObject
        {
            // Confirm parent and childName are valid. 
            if (parent == null) return null;
            T foundChild = null;
            int childrenCount = WPFVisualTreeHelper.GetChildrenCount(parent);
            for (int i = 0; i < childrenCount; i++)
            {
                var child = WPFVisualTreeHelper.GetChild(parent, i);
                // If the child is not of the request child type child
                T childType = child as T;
                if (childType == null)
                {
                    // recursively drill down the tree
                    foundChild = FindChild<T>(child, childName);
                    // If the child is found, break so we do not overwrite the found child. 
                    if (foundChild != null) break;
                }
                else if (!string.IsNullOrEmpty(childName))
                {
                    var frameworkElement = child as FrameworkElement;
                    // If the child's name is set for search
                    if (frameworkElement != null && frameworkElement.Name == childName)
                    {
                        // if the child's name is of the request name
                        foundChild = (T)child;
                        break;
                    }
                }
                else
                {
                    // child element found.
                    foundChild = (T)child;
                    break;
                }
            }
            return foundChild;
        }
    }
}