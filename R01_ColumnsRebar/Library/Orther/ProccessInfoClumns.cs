
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Media;
using Autodesk.Revit.DB;
namespace R01_ColumnsRebar
{
    public class ProccessInfoClumns
    {
        #region PlanrFace
        public static ObservableCollection<PlanarFace> GetPlanarFaces(List<Element> columns, Document document)
        {
            ObservableCollection<PlanarFace> planarFaces = new ObservableCollection<PlanarFace>();
            PlanarFace planarFace1 = GetPlanarFace0(columns[0], document);
            if (planarFace1==null)
            {
                planarFaces.Add(SolidFace.GetBottom(columns[0]));
            }
            else
            {
                planarFaces.Add(planarFace1);
                planarFaces.Add(SolidFace.GetBottom(columns[0]));
            }
            for (int i = 0; i < columns.Count; i++)
            {
                planarFaces.Add(SolidFace.GetTop(columns[i]));
                List<Element> beams = ColumnsBoundingBox.GetBeamsBoudingBoxSameTopLevelPerpencularZOneColumn(columns[i], document);
                if (beams.Count != 0)
                {
                    List<PlanarFace> planarFacesbeam = new List<PlanarFace>();
                    for (int j = 0; j < beams.Count; j++)
                    {
                        List<PlanarFace> planarFaces1 = SolidFace.GetTopPlanarFaceBeam(beams[j], document);
                        planarFacesbeam.AddRange(planarFaces1);
                    }
                    if (planarFacesbeam.Count!=0)
                    {
                        planarFacesbeam.OrderBy(x => x.Origin.Z);
                        planarFaces.Add(planarFacesbeam[0]);
                        planarFaces.Add(planarFacesbeam[planarFacesbeam.Count-1]);
                    }
                }
            }
            return planarFaces;
        }
        #endregion
        #region infoModel
        public static ObservableCollection<InfoModel> GetInfoModelsRectanggle(List<Element> columns,Document document)
        {
            ObservableCollection<InfoModel> infoModels = new ObservableCollection<InfoModel>();
            PlanarFace planarFace1= GetPlanarFace0(columns[0], document);
            PlanarFace planarFace0 = (planarFace1 != null) ? planarFace1 : SolidFace.GetBottom(columns[0]);
            PlanarFace south0 = SolidFace.GetSouth(columns[0]);
            PlanarFace west0 = SolidFace.GetWest(columns[0]);
            for (int i = 0; i < columns.Count; i++)
            {
                PlanarFace top = SolidFace.GetTop(columns[i]);
                PlanarFace bottom = SolidFace.GetBottom(columns[i]);
                PlanarFace south = SolidFace.GetSouth(columns[i]);
                PlanarFace nouth = SolidFace.GetNouth(columns[i]);
                PlanarFace west = SolidFace.GetWest(columns[i]);
                PlanarFace east = SolidFace.GetEast(columns[i]);
                infoModels.Add(new InfoModel(i+1, planarFace0, south0, west0, top, bottom, west, east, south, nouth, columns[i], document));
            }
            return infoModels;
        }
        public static ObservableCollection<InfoModel> GetInfoModelsCylindical(List<Element> columns, Document document)
        {
            ObservableCollection<InfoModel> infoModels = new ObservableCollection<InfoModel>();
            PlanarFace planarFace1 = GetPlanarFace0(columns[0], document);
            PlanarFace planarFace0 = (planarFace1 != null) ? planarFace1 : SolidFace.GetBottom(columns[0]);
            XYZ point0 = (columns[0].Location as LocationPoint).Point as XYZ;
            for (int i = 0; i < columns.Count; i++)
            {
                PlanarFace top = SolidFace.GetTop(columns[i]);
                PlanarFace bottom = SolidFace.GetBottom(columns[i]);
                infoModels.Add(new InfoModel(i + 1, planarFace0, point0, top, bottom, columns[i], document));
            }
            return infoModels;
        }
        private static PlanarFace GetPlanarFace0(Element column0, Document document)
        {
            PlanarFace a = null;
            Element foundation = ColumnsBoundingBox.GetFoundationBoudingBoxOneColumn(column0, document);
            if (foundation != null)
            {
                PlanarFace bottom = SolidFace.GetBottom(column0);
                List<PlanarFace> planarFaces = SolidFace.GetTopPlanarFaceBeam(foundation, document);
                if (planarFaces.Count != 0)
                {
                    planarFaces = planarFaces.OrderBy(x => x.Origin.Z).ToList();
                    a = planarFaces[0];
                    for (int i = 0; i < planarFaces.Count; i++)
                    {
                        if (a.Origin.Z>planarFaces[i].Origin.Z)
                        {
                            a = planarFaces[i];
                        }
                    }
                }
                else
                {
                    return null;
                }
            }
            else
            {
                Element floor = ColumnsBoundingBox.GetFloorBoudingBoxOneColumn(column0, document);
                if (floor != null)
                {
                    PlanarFace bottom = SolidFace.GetBottom(column0);
                    List<PlanarFace> planarFaces = SolidFace.GetTopPlanarFaceBeam(floor, document);
                    if (planarFaces.Count != 0)
                    {
                        planarFaces = planarFaces.OrderBy(x => x.Origin.Z).ToList();
                        a = planarFaces[0];
                        for (int i = 0; i < planarFaces.Count; i++)
                        {
                            if (a.Origin.Z > planarFaces[i].Origin.Z)
                            {
                                a = planarFaces[i];
                            }
                        }
                    }
                    else
                    {
                        return null;
                    }
                }
                else
                {
                    Element wall = ColumnsBoundingBox.GetWallBoudingBoxOneColumn(column0, document);
                    if (wall != null)
                    {
                        PlanarFace bottom = SolidFace.GetBottom(column0);
                        List<PlanarFace> planarFaces = SolidFace.GetTopPlanarFaceBeam(wall, document);
                        if (planarFaces.Count != 0)
                        {
                            planarFaces.OrderBy(x => x.Origin.Z);
                            a = planarFaces[0];
                            for (int i = 0; i < planarFaces.Count; i++)
                            {
                                if (a.Origin.Z > planarFaces[i].Origin.Z)
                                {
                                    a = planarFaces[i];
                                }
                            }
                        }
                        else
                        {
                            return null;
                        }
                    }
                    else
                    {
                        List<Element> beams = ColumnsBoundingBox.GetBeamsBoudingBoxSameBottomLevelPerpencularZOneColumn(column0, document);
                        if (beams.Count != 0)
                        {
                            List<PlanarFace> planarFaces = new List<PlanarFace>();
                            PlanarFace bottom = SolidFace.GetBottom(column0);
                            for (int i = 0; i < beams.Count; i++)
                            {
                                List<PlanarFace> p1 = SolidFace.GetTopPlanarFaceBeam(beams[i], document);
                                planarFaces.AddRange(p1);
                            }
                            if (planarFaces.Count != 0)
                            {
                                planarFaces.OrderBy(x => x.Origin.Z);
                                a = planarFaces[0];
                                for (int i = 0; i < planarFaces.Count; i++)
                                {
                                    if (a.Origin.Z > planarFaces[i].Origin.Z)
                                    {
                                        a = planarFaces[i];
                                    }
                                }
                            }
                            else
                            {
                                return null;
                            }
                        }
                        else
                        {
                            return null;
                        }
                    }
                }
            }
            return a;
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
