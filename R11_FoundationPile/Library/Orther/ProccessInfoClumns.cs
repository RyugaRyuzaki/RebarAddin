
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Media;
using Autodesk.Revit.DB;
using DSP;
namespace R11_FoundationPile
{
    public class ProccessInfoClumns
    {

        #region condition filter
        public static bool ConditionFilter(ColumnModel column1, ColumnModel column2)
        {
            if (!column1.Style.Equals(column2.Style))
            {
                return false;
            }
            else
            {
                if (column1.Style.Equals("RECTANGLE"))
                {
                    if (PointModel.AreEqual(column1.b, column2.b) && PointModel.AreEqual(column1.h, column2.h))
                    {
                        return true;
                    }
                    else
                    {
                        if (PointModel.AreEqual(column1.b, column2.h) && PointModel.AreEqual(column1.h, column2.b))
                        {
                            return true;
                        }
                        else
                        {
                            return false;
                        }
                    }
                }
                else
                {
                    if (PointModel.AreEqual(column1.D, column2.D))
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            
        }
        #endregion
        #region     GetGroupFoundatioModel
        public static ObservableCollection<GroupFoundationModel> GetGroupFoundationModels(ObservableCollection<ColumnModel> columnModels,SettingModel settingModel)
        {
            ObservableCollection<GroupFoundationModel> groupFoundationModels  = new ObservableCollection<GroupFoundationModel>();
            if (columnModels.Count==1)
            {
                groupFoundationModels.Add(new GroupFoundationModel(1, columnModels, settingModel));
            }
            else
            {
                ObservableCollection<ColumnModel> columnModels1 = new ObservableCollection<ColumnModel>(columnModels.ToList());
                
                ColumnModel columnModel0 = columnModels1[0];
                int i = 1;
                while (columnModels1.Count!=0)
                {
                    if (columnModels1.Count == 0)
                    {
                        break;
                    }
                    ObservableCollection<ColumnModel> columnModels2 = new ObservableCollection<ColumnModel>(columnModels1.Where(x=>ConditionFilter(x,columnModel0)).ToList());
                    groupFoundationModels.Add(new GroupFoundationModel(i, columnModels2, settingModel));
                    for (int j = 0; j < columnModels2.Count; j++)
                    {
                        columnModels1.Remove(columnModels2[j]);
                    }
                    if(columnModels1.Count!=0) columnModel0 = columnModels1[0];
                    i++;
                }
            }
            return groupFoundationModels;
        }


        #endregion
        #region infoModel
        public static ObservableCollection<ColumnModel> GetColumnModels(List<Element> columns, Document document,UnitProject unit)
        {
            ObservableCollection<ColumnModel> columnModels = new ObservableCollection<ColumnModel>();
            
            for (int i = 0; i < columns.Count; i++)
            {
                
                PlanarFace bottom = SolidFace.GetBottom(columns[i]);
                PlanarFace south = SolidFace.GetSouth(columns[i]);
                PlanarFace nouth = SolidFace.GetNouth(columns[i]);
                PlanarFace west = SolidFace.GetWest(columns[i]);
                PlanarFace east = SolidFace.GetEast(columns[i]);
                ErrorColumns.SectionStyle a = ErrorColumns.GetSectionStyle(document, columns[i]);
                if (a == ErrorColumns.SectionStyle.RECTANGLE)
                {
                    columnModels.Add(new ColumnModel(a,columns[i], bottom, west, east, south, nouth, document, unit));
                }
                else
                {
                    columnModels.Add(new ColumnModel(a,columns[i], bottom, document));
                }
            }
            return columnModels;
        }
        public static ObservableCollection<ColumnModel> GetColumnModelsCylindical(List<Element> columns, Document document)
        {
            ObservableCollection<ColumnModel> columnModels = new ObservableCollection<ColumnModel>();
            for (int i = 0; i < columns.Count; i++)
            {
                PlanarFace bottom = SolidFace.GetBottom(columns[i]);
                
            }
            return columnModels;
        }
      
        #endregion
        #region Find Child
        public static T FindChild<T>(DependencyObject parent, string childName) where T : DependencyObject
        {
            // Confirm parent and childName are valid. 
            if (parent == null) return null;
            T foundChild = null;
            int childrenCount = VisualTreeHelper.GetChildrenCount(parent);
            for (int i = 0; i < childrenCount; i++)
            {
                var child = VisualTreeHelper.GetChild(parent, i);
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
        #endregion
    }
}
