using Autodesk.Revit.DB;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace R02_BeamsRebar
{
    public class ProcessInfoBeamRebar
    {

        #region
        public static List<NodeModel> GetNodeModel(List<InfoModel> infoModels, List<Element> beams, Document doc, Line line, string level)
        {
            List<NodeModel> nodeModels = new List<NodeModel>();
            double left = 0;
            double right = 0;
            XYZ l = line.Direction;
            List<PlanarFace> planrBeams = SolidFace.GetAllPlanrFacePerpendicular(beams, l);
            planrBeams = SolidFace.ArrangePlanrFace(planrBeams, l);
            List<Element> columns = BeamsBoundBox.GetColumnsBoudingBoxBeams(beams, doc);
            List<Element> columnsTopLevel = BeamsBoundBox.GetColumnsSameTopLevelBeams(columns, level);
            List<Element> beamPer = BeamsBoundBox.GetBeamsPerNodeBeams(beams, doc);
            List<PlanarFace> planars = new List<PlanarFace>();
            if (columnsTopLevel.Count == 0)
            {
                List<PlanarFace> planrBeamPer0 = SolidFace.GetAllPlanrFacePerpendicular(beamPer, l);
                planrBeamPer0 = SolidFace.ArrangePlanrFace(planrBeamPer0, l);
                planars = SolidFace.AddRangePlanarFace(planrBeamPer0, planrBeams, l);
                planars = SolidFace.ArrangePlanrFace(planars, l);
                left = PointModel.DistanceTo2(planars[0], planars[0].Origin, doc);
                right = PointModel.DistanceTo2(planars[0], planars[planars.Count - 1].Origin, doc);
            }
            else
            {
                List<Element> beamPer1 = BeamsBoundBox.GetBeamsBoudingBoxSameTopLevelColumns(columnsTopLevel, doc, line, level);
                List<Element> beamPer0 = BeamsBoundBox.RemoveList(beamPer, beamPer1, line);
                if (beamPer0.Count == 0)
                {
                    List<PlanarFace> planrColumnsTop = SolidFace.GetAllPlanrFacePerpendicular(columnsTopLevel, l);
                    planrColumnsTop = SolidFace.ArrangePlanrFace(planrColumnsTop, l);
                    planars = SolidFace.AddRangePlanarFace(planrColumnsTop, planrBeams, l);
                    planars = SolidFace.ArrangePlanrFace(planars, l);
                    left = PointModel.DistanceTo2(planars[0], planars[0].Origin, doc);
                    right = PointModel.DistanceTo2(planars[0], planars[planars.Count - 1].Origin, doc);
                }
                else
                {
                    #region
                    List<PlanarFace> planrColumnsTop = SolidFace.GetAllPlanrFacePerpendicular(columnsTopLevel, l);
                    planrColumnsTop = SolidFace.ArrangePlanrFace(planrColumnsTop, l);
                    List<PlanarFace> planrBeamPer0 = SolidFace.GetAllPlanrFacePerpendicular(beamPer0, l);
                    planrBeamPer0 = SolidFace.ArrangePlanrFace(planrBeamPer0, l);
                    planars = SolidFace.AddRangePlanarFace(planrColumnsTop, planrBeams, l);
                    planars = SolidFace.AddRangePlanarFace(planars, planrBeamPer0, l);
                    planars = SolidFace.ArrangePlanrFace(planars, l);
                    double d1 = PointModel.DistanceTo2(planars[0], planrColumnsTop[0].Origin, doc);
                    double d2 = PointModel.DistanceTo2(planars[0], planrBeamPer0[0].Origin, doc);
                    double d = Math.Min(d1, d2);
                    left = d;
                    right = PointModel.DistanceTo2(planars[0], planars[planars.Count - 1].Origin, doc);
                    #endregion
                }
            }
            if (infoModels[0].ConsolLeft)
            {
                if (infoModels[infoModels.Count - 1].ConsolRight)
                {
                    for (int i = 0; i < infoModels.Count - 1; i++)
                    {
                        nodeModels.Add(new NodeModel(i + 1, infoModels[i].endPosition, infoModels[i + 1].startPosition, infoModels[i].h, infoModels[i + 1].h, infoModels[i].zOffset, infoModels[i + 1].zOffset, infoModels[i].EndPlanar, infoModels[i + 1].StartPlanar));
                    }
                }
                else
                {
                    for (int i = 0; i < infoModels.Count; i++)
                    {
                        if (i == infoModels.Count - 1)
                        {
                            nodeModels.Add(new NodeModel(i + 1, infoModels[i].endPosition, right, infoModels[i].h, infoModels[i].h, infoModels[i].zOffset, infoModels[i].zOffset, infoModels[i].EndPlanar,planars[planars.Count-1]));
                        }
                        else
                        {
                            nodeModels.Add(new NodeModel(i + 1, infoModels[i].endPosition, infoModels[i + 1].startPosition, infoModels[i].h, infoModels[i + 1].h, infoModels[i].zOffset, infoModels[i + 1].zOffset, infoModels[i].EndPlanar, infoModels[i + 1].StartPlanar));
                        }

                    }
                }
            }
            else
            {
                if (infoModels[infoModels.Count - 1].ConsolRight)
                {
                    for (int i = 0; i < infoModels.Count; i++)
                    {
                        if (i == 0)
                        {
                            nodeModels.Add(new NodeModel(i + 1, left, infoModels[i].startPosition, infoModels[i].h, infoModels[i].h, infoModels[i].zOffset, infoModels[i].zOffset,planars[0], infoModels[i].StartPlanar));
                        }
                        else
                        {
                            nodeModels.Add(new NodeModel(i + 1, infoModels[i - 1].endPosition, infoModels[i].startPosition, infoModels[i - 1].h, infoModels[i].h, infoModels[i - 1].zOffset, infoModels[i].zOffset, infoModels[i - 1].EndPlanar, infoModels[i].StartPlanar));
                        }

                    }
                }
                else
                {
                    for (int i = 0; i < infoModels.Count + 1; i++)
                    {
                        if (i == 0)
                        {
                            nodeModels.Add(new NodeModel(i + 1, left, infoModels[i].startPosition, infoModels[i].h, infoModels[i].h, infoModels[i].zOffset, infoModels[i].zOffset,planars[0], infoModels[i].StartPlanar));
                        }
                        else
                        {
                            if (i == infoModels.Count)
                            {
                                nodeModels.Add(new NodeModel(i + 1, infoModels[i - 1].endPosition, right, infoModels[i - 1].h, infoModels[i - 1].h, infoModels[i - 1].zOffset, infoModels[i - 1].zOffset, infoModels[i - 1].EndPlanar,planars[planars.Count-1]));
                            }
                            else
                            {
                                nodeModels.Add(new NodeModel(i + 1, infoModels[i - 1].endPosition, infoModels[i].startPosition, infoModels[i - 1].h, infoModels[i].h, infoModels[i - 1].zOffset, infoModels[i].zOffset, infoModels[i - 1].EndPlanar, infoModels[i].StartPlanar));
                            }
                        }

                    }
                }
            }
            return nodeModels;
        }
        public static List<PlanarFace> GetPlanarFace(List<InfoModel> infoModels, List<Element> beams, Document doc, Line line, string level)
        {
            XYZ l = line.Direction;
            List<PlanarFace> planrBeams = SolidFace.GetAllPlanrFacePerpendicular(beams, l);
            planrBeams = SolidFace.ArrangePlanrFace(planrBeams, l);
            List<Element> columns = BeamsBoundBox.GetColumnsBoudingBoxBeams(beams, doc);
            List<Element> columnsTopLevel = BeamsBoundBox.GetColumnsSameTopLevelBeams(columns, level);
            List<Element> beamPer = BeamsBoundBox.GetBeamsPerNodeBeams(beams, doc);
            List<PlanarFace> planars = null;
            if (columnsTopLevel.Count == 0)
            {
                List<PlanarFace> planrBeamPer0 = SolidFace.GetAllPlanrFacePerpendicular(beamPer, l);
                planrBeamPer0 = SolidFace.ArrangePlanrFace(planrBeamPer0, l);
                planars = SolidFace.AddRangePlanarFace(planrBeamPer0, planrBeams, l);
                planars = SolidFace.ArrangePlanrFace(planars, l);
            }
            else
            {
                List<Element> beamPer1 = BeamsBoundBox.GetBeamsBoudingBoxSameTopLevelColumns(columnsTopLevel, doc, line, level);
                List<Element> beamPer0 = BeamsBoundBox.RemoveList(beamPer, beamPer1, line);
                if (beamPer0.Count == 0)
                {
                    List<PlanarFace> planrColumnsTop = SolidFace.GetAllPlanrFacePerpendicular(columnsTopLevel, l);
                    planrColumnsTop = SolidFace.ArrangePlanrFace(planrColumnsTop, l);
                    planars = SolidFace.AddRangePlanarFace(planrColumnsTop, planrBeams, l);
                    planars = SolidFace.ArrangePlanrFace(planars, l);
                }
                else
                {
                    #region
                    List<PlanarFace> planrColumnsTop = SolidFace.GetAllPlanrFacePerpendicular(columnsTopLevel, l);
                    planrColumnsTop = SolidFace.ArrangePlanrFace(planrColumnsTop, l);
                    List<PlanarFace> planrBeamPer0 = SolidFace.GetAllPlanrFacePerpendicular(beamPer0, l);
                    planrBeamPer0 = SolidFace.ArrangePlanrFace(planrBeamPer0, l);
                    planars = SolidFace.AddRangePlanarFace(planrColumnsTop, planrBeams, l);
                    planars = SolidFace.AddRangePlanarFace(planars, planrBeamPer0, l);
                    planars = SolidFace.ArrangePlanrFace(planars, l);
                    #endregion
                }
            }
          
            return planars;
        }
        #endregion
        #region Main Top bar
        public static List<MainTopBarModel> GetMainTopBarModels(List<InfoModel> infoModels, List<NodeModel> nodeModels, RebarBarModel bar, double c)
        {
            List<MainTopBarModel> mainTopBarModels = new List<MainTopBarModel>();


            if (infoModels.Count > nodeModels.Count)
            {
                List<int> z = new List<int>();
                for (int i = 1; i < infoModels.Count; i++)
                {
                    if (infoModels[i - 1].zOffset != infoModels[i].zOffset)
                    {
                        z.Add(i);
                    }
                }
                if (z.Count == 0)
                {
                    mainTopBarModels.Add(new MainTopBarModel(1, 2, bar,
                        Math.Abs(infoModels[0].startPosition - infoModels[infoModels.Count - 1].endPosition) - 2 * c,
                        infoModels[0].h - 2 * c,
                        0,
                        infoModels[infoModels.Count - 1].h - 2 * c,
                        0,
                        c,
                        Math.Abs(infoModels[0].zOffset) + c, Math.Abs(infoModels[0].zOffset)
                        ));
                }
                else
                {
                    if (z.Count == 1)
                    {
                        double l1 = Math.Abs(infoModels[0].startPosition - infoModels[z[0] - 1].endPosition) + nodeModels[z[0] - 1].Width - 2 * c;
                        mainTopBarModels.Add(new MainTopBarModel(1, 2, bar,
                        l1,
                        infoModels[0].h - 2 * c,
                        0,
                        infoModels[z[0] - 1].h - 2 * c,
                        0,
                        c,
                        Math.Abs(infoModels[0].zOffset) + c, Math.Abs(infoModels[0].zOffset)
                        ));
                        double l2 = Math.Abs(infoModels[infoModels.Count - 1].endPosition - infoModels[z[0]].startPosition) + nodeModels[z[0] - 1].Width - 2 * c;
                        mainTopBarModels.Add(new MainTopBarModel(2, 2, bar,
                        l2,
                        infoModels[z[0]].h - 2 * c,
                        0,
                        infoModels[infoModels.Count - 1].h - 2 * c,
                        0,
                        nodeModels[z[0]-1 ].Start + c,
                        Math.Abs(infoModels[z[0]].zOffset) + c, Math.Abs(infoModels[z[0]].zOffset)
                        ));
                    }
                    else
                    {
                        for (int i = 0; i <= z.Count; i++)
                        {
                            if (i == 0)
                            {
                                double l = Math.Abs(infoModels[0].startPosition - infoModels[z[i] - 1].endPosition) + nodeModels[z[i] - 1].Width - 2 * c;
                                mainTopBarModels.Add(new MainTopBarModel(i + 1, 2, bar,
                                l,
                                infoModels[z[i]].h - 2 * c,
                                0,
                                infoModels[z[i]].h - 2 * c,
                                0,
                                c,
                                Math.Abs(infoModels[0].zOffset) + c, Math.Abs(infoModels[0].zOffset) 
                                ));
                            }
                            else
                            {
                                if (i == z.Count)
                                {
                                    double l = Math.Abs(infoModels[infoModels.Count - 1].endPosition - infoModels[z[i - 1]].startPosition) + nodeModels[z[i - 1] - 1].Width - 2 * c;
                                    mainTopBarModels.Add(new MainTopBarModel(i + 1, 2, bar,
                               l,
                               infoModels[z[i - 1]].h - 2 * c,
                               0,
                               infoModels[z[i - 1]].h - 2 * c,
                               0,
                               nodeModels[z[i - 1] - 1].Start + c,
                               Math.Abs(infoModels[z[i - 1]].zOffset) + c, Math.Abs(infoModels[z[i - 1]].zOffset) 
                               ));
                                }
                                else
                                {
                                    double l = Math.Abs(infoModels[z[i - 1]].startPosition - infoModels[z[i] - 1].endPosition) + nodeModels[z[i - 1] - 1].Width + nodeModels[z[i] - 1].Width - 2 * c;
                                    mainTopBarModels.Add(new MainTopBarModel(i + 1, 2, bar,
                                        l,
                                        infoModels[z[i - 1]].h - 2 * c,
                                        0,
                                        infoModels[z[i]].h - 2 * c,
                                        0,
                                        nodeModels[z[i - 1] - 1].Start + c,
                                        Math.Abs(infoModels[z[i - 1]].zOffset) + c, Math.Abs(infoModels[z[i - 1]].zOffset) 
                                        ));
                                }

                            }

                        }
                    }

                }
            }
            else
            {
                if (infoModels.Count == nodeModels.Count)
                {
                    if (infoModels[0].ConsolLeft)
                    {
                        List<int> z = new List<int>();
                        for (int i = 1; i < infoModels.Count; i++)
                        {
                            if (infoModels[i - 1].zOffset != infoModels[i].zOffset)
                            {
                                z.Add(i);
                            }
                        }
                        if (z.Count == 0)
                        {
                            mainTopBarModels.Add(new MainTopBarModel(1, 2, bar,
                       Math.Abs(infoModels[0].startPosition - nodeModels[nodeModels.Count - 1].End) - 2 * c,
                       infoModels[0].h - 2 * c,
                       0,
                       infoModels[infoModels.Count - 1].h - 2 * c,
                       0,
                       c,
                       Math.Abs(infoModels[0].zOffset) + c, Math.Abs(infoModels[0].zOffset) 
                       ));
                        }
                        else
                        {
                            if (z.Count == 1)
                            {
                                double l1 = Math.Abs(infoModels[0].startPosition - infoModels[z[0] - 1].endPosition) + nodeModels[z[0] - 1].Width - 2 * c;
                                mainTopBarModels.Add(new MainTopBarModel(1, 2, bar,
                                l1,
                                infoModels[0].h - 2 * c,
                                0,
                                infoModels[z[0] - 1].h - 2 * c,
                                0,
                                c,
                                Math.Abs(infoModels[0].zOffset) + c, Math.Abs(infoModels[0].zOffset) 
                                ));
                                double l2 = Math.Abs(infoModels[infoModels.Count - 1].endPosition - infoModels[z[0]].startPosition) + nodeModels[z[0] - 1].Width + nodeModels[z[0]].Width - 2 * c;
                                mainTopBarModels.Add(new MainTopBarModel(2, 2, bar,
                                l2,
                                infoModels[z[0]].h - 2 * c,
                                0,
                                infoModels[infoModels.Count - 1].h - 2 * c,
                                0,
                                infoModels[z[0] - 1].endPosition + c,
                                Math.Abs(infoModels[z[0]].zOffset) + c, Math.Abs(infoModels[z[0]].zOffset) 
                                ));
                            }
                            else
                            {
                                for (int i = 0; i <= z.Count; i++)
                                {
                                    if (i == 0)
                                    {
                                        double l = Math.Abs(infoModels[0].startPosition - infoModels[z[i] - 1].endPosition) + nodeModels[z[i] - 1].Width - 2 * c;
                                        mainTopBarModels.Add(new MainTopBarModel(i + 1, 2, bar,
                                        l,
                                        infoModels[z[i]].h - 2 * c,
                                        0,
                                        infoModels[z[i]].h - 2 * c,
                                        0,
                                        c,
                                        Math.Abs(infoModels[0].zOffset) + c, Math.Abs(infoModels[0].zOffset)
                                        ));
                                    }
                                    else
                                    {
                                        if (i == z.Count)
                                        {
                                            double l = Math.Abs(infoModels[infoModels.Count - 1].endPosition - infoModels[z[i - 1]].startPosition) + nodeModels[z[i - 1] - 1].Width - 2 * c + nodeModels[z[i - 1] + 1].Width;
                                            mainTopBarModels.Add(new MainTopBarModel(i + 1, 2, bar,
                                       l,
                                       infoModels[z[i - 1]].h - 2 * c,
                                       0,
                                       infoModels[z[i - 1]].h - 2 * c,
                                       0,
                                       nodeModels[z[i - 1] - 1].Start + c,
                                       Math.Abs(infoModels[z[i - 1]].zOffset) + c, Math.Abs(infoModels[z[i - 1]].zOffset) 
                                       ));
                                        }
                                        else
                                        {
                                            double l = Math.Abs(infoModels[z[i - 1]].startPosition - infoModels[z[i] - 1].endPosition) + nodeModels[z[i - 1] - 1].Width + nodeModels[z[i] - 1].Width - 2 * c;
                                            mainTopBarModels.Add(new MainTopBarModel(i + 1, 2, bar,
                                                l,
                                                infoModels[z[i - 1]].h - 2 * c,
                                                0,
                                                infoModels[z[i] - 1].h - 2 * c,
                                                0,
                                                nodeModels[z[i - 1] - 1].Start - c,
                                                Math.Abs(infoModels[z[i - 1]].zOffset) + c, Math.Abs(infoModels[z[i - 1]].zOffset) 
                                                ));
                                        }

                                    }

                                }
                            }

                        }
                    }
                    else
                    {
                        List<int> z = new List<int>();
                        for (int i = 1; i < infoModels.Count; i++)
                        {
                            if (infoModels[i - 1].zOffset != infoModels[i].zOffset)
                            {
                                z.Add(i);
                            }
                        }
                        if (z.Count == 0)
                        {
                            mainTopBarModels.Add(new MainTopBarModel(1, 2, bar,
                       Math.Abs(infoModels[0].startPosition - nodeModels[nodeModels.Count - 1].End) - 2 * c,
                       infoModels[0].h - 2 * c,
                       0,
                       infoModels[infoModels.Count - 1].h - 2 * c,
                       0,
                       c,
                       Math.Abs(infoModels[0].zOffset) + c, Math.Abs(infoModels[0].zOffset) 
                       ));
                        }
                        else
                        {
                            if (z.Count == 1)
                            {
                                double l1 = Math.Abs(infoModels[0].startPosition - infoModels[z[0] - 1].endPosition) + nodeModels[0].Width + nodeModels[z[0] - 1].Width - 2 * c;
                                mainTopBarModels.Add(new MainTopBarModel(1, 2, bar,
                                l1,
                                infoModels[0].h - 2 * c,
                                0,
                                infoModels[z[0] - 1].h - 2 * c,
                                0,
                                c,
                                Math.Abs(infoModels[0].zOffset) + c, Math.Abs(infoModels[0].zOffset) 
                                ));
                                double l2 = Math.Abs(infoModels[infoModels.Count - 1].endPosition - infoModels[z[0]].startPosition) + nodeModels[z[0] - 1].Width - 2 * c;
                                mainTopBarModels.Add(new MainTopBarModel(2, 2, bar,
                                l2,
                                infoModels[z[0]].h - 2 * c,
                                0,
                                infoModels[infoModels.Count - 1].h - 2 * c,
                                0,
                                infoModels[z[0] - 1].endPosition + c,
                                Math.Abs(infoModels[z[0]].zOffset) + c, Math.Abs(infoModels[z[0]].zOffset) 
                                ));
                            }
                            else
                            {
                                for (int i = 0; i <= z.Count; i++)
                                {
                                    if (i == 0)
                                    {
                                        double l = Math.Abs(infoModels[0].startPosition - infoModels[z[i] - 1].endPosition) + nodeModels[0].Width + nodeModels[z[i]].Width - 2 * c;
                                        mainTopBarModels.Add(new MainTopBarModel(i + 1, 2, bar,
                                        l,
                                        infoModels[z[i]].h - 2 * c,
                                        0,
                                        infoModels[z[i]].h - 2 * c,
                                        0,
                                        c,
                                        Math.Abs(infoModels[0].zOffset) + c, Math.Abs(infoModels[0].zOffset) 
                                        ));
                                    }
                                    else
                                    {
                                        if (i == z.Count)
                                        {
                                            double l = Math.Abs(infoModels[infoModels.Count - 1].endPosition - infoModels[z[i - 1]].startPosition) + nodeModels[z[i - 1]].Width - 2 * c;
                                            mainTopBarModels.Add(new MainTopBarModel(i + 1, 2, bar,
                                       l,
                                       infoModels[z[i - 1]].h - 2 * c,
                                       0,
                                       infoModels[z[i - 1]].h - 2 * c,
                                       0,
                                       nodeModels[z[i - 1]].Start + c,
                                       Math.Abs(infoModels[z[i - 1]].zOffset) + c, Math.Abs(infoModels[z[i - 1]].zOffset) 
                                       ));
                                        }
                                        else
                                        {
                                            double l = Math.Abs(infoModels[z[i - 1]].startPosition - infoModels[z[i] - 1].endPosition) + nodeModels[z[i - 1] - 1].Width + nodeModels[z[i] - 1].Width - 2 * c;
                                            mainTopBarModels.Add(new MainTopBarModel(i + 1, 2, bar,
                                                l,
                                                infoModels[z[i - 1]].h - 2 * c,
                                                0,
                                                infoModels[z[i] - 1].h - 2 * c,
                                                0,
                                                nodeModels[z[i - 1] - 1].Start - c,
                                                Math.Abs(infoModels[z[i - 1]].zOffset) + c, Math.Abs(infoModels[z[i - 1]].zOffset) 
                                                ));
                                        }
                                    }
                                }
                            }

                        }
                    }
                }
                else
                {


                    List<int> z = new List<int>();
                    for (int i = 1; i < infoModels.Count; i++)
                    {
                        if (infoModels[i - 1].zOffset != infoModels[i].zOffset)
                        {
                            z.Add(i);
                        }
                    }
                    if (z.Count == 0)
                    {
                        mainTopBarModels.Add(new MainTopBarModel(1, 2, bar,
                   Math.Abs(infoModels[0].startPosition - infoModels[infoModels.Count - 1].endPosition) + nodeModels[0].Width + nodeModels[nodeModels.Count - 1].Width - 2 * c,
                   infoModels[0].h - 2 * c,
                   0,
                   infoModels[infoModels.Count - 1].h - 2 * c,
                   0,
                   c,
                   Math.Abs(infoModels[0].zOffset) + c, Math.Abs(infoModels[0].zOffset) 
                   ));
                    }
                    else
                    {
                        if (z.Count == 1)
                        {
                            double l1 = Math.Abs(infoModels[0].startPosition - infoModels[z[0] - 1].endPosition) + nodeModels[0].Width + nodeModels[z[0] - 1].Width - 2 * c;
                            mainTopBarModels.Add(new MainTopBarModel(1, 2, bar,
                            l1,
                            infoModels[0].h - 2 * c,
                            0,
                            infoModels[z[0] - 1].h - 2 * c,
                            0,
                            c,
                            Math.Abs(infoModels[0].zOffset) + c, Math.Abs(infoModels[0].zOffset) 
                            ));
                            double l2 = Math.Abs(infoModels[infoModels.Count - 1].endPosition - infoModels[z[0]].startPosition) + nodeModels[z[0] - 1].Width + nodeModels[nodeModels.Count - 1].Width - 2 * c;
                            mainTopBarModels.Add(new MainTopBarModel(2, 2, bar,
                            l2,
                            infoModels[z[0]].h - 2 * c,
                            0,
                            infoModels[infoModels.Count - 1].h - 2 * c,
                            0,
                            infoModels[z[0]].startPosition + c - nodeModels[z[0]].Width,
                            Math.Abs(infoModels[z[0]].zOffset) + c, Math.Abs(infoModels[z[0]].zOffset) 
                            ));
                        }
                        else
                        {
                            for (int i = 0; i <= z.Count; i++)
                            {
                                if (i == 0)
                                {
                                    double l = Math.Abs(infoModels[0].startPosition - infoModels[z[i] - 1].endPosition) + nodeModels[0].Width + nodeModels[z[i]].Width - 2 * c;
                                    mainTopBarModels.Add(new MainTopBarModel(i + 1, 2, bar,
                                    l,
                                    infoModels[z[i]].h - 2 * c,
                                    0,
                                    infoModels[z[i]].h - 2 * c,
                                    0,
                                    c,
                                    Math.Abs(infoModels[0].zOffset) + c, Math.Abs(infoModels[0].zOffset) 
                                    ));
                                }
                                else
                                {
                                    if (i == z.Count)
                                    {
                                        double l = Math.Abs(infoModels[infoModels.Count - 1].endPosition - infoModels[z[i - 1]].startPosition) + nodeModels[z[i - 1]].Width + nodeModels[nodeModels.Count - 1].Width - 2 * c;
                                        mainTopBarModels.Add(new MainTopBarModel(i + 1, 2, bar,
                                   l,
                                   infoModels[z[i - 1]].h - 2 * c,
                                   0,
                                   infoModels[z[i - 1]].h - 2 * c,
                                   0,
                                   nodeModels[z[i - 1]].Start + c,
                                   Math.Abs(infoModels[z[i - 1]].zOffset) + c, Math.Abs(infoModels[z[i - 1]].zOffset) 
                                   ));
                                    }
                                    else
                                    {
                                        double l = Math.Abs(infoModels[z[i - 1]].startPosition - infoModels[z[i] - 1].endPosition) + nodeModels[z[i - 1]].Width + nodeModels[z[i]].Width - 2 * c;
                                        mainTopBarModels.Add(new MainTopBarModel(i + 1, 2, bar,
                                            l,
                                            infoModels[z[i - 1]].h - 2 * c,
                                            0,
                                            infoModels[z[i] - 1].h - 2 * c,
                                            0,
                                            nodeModels[z[i - 1]].Start + c,
                                            Math.Abs(infoModels[z[i - 1]].zOffset) + c, Math.Abs(infoModels[z[i - 1]].zOffset) 
                                            ));
                                    }
                                }
                            }
                        }
                    }
                }
            }


            return mainTopBarModels;
        }
        #endregion

        #region Single Main Top bar
        public static SingleMainTopBarModel GetSingleMainTopBarModels(List<InfoModel> infoModels, List<NodeModel> nodeModels, RebarBarModel bar, double c)
        {

            double x = c;
            double y = Math.Abs(infoModels[0].zOffset) + c;
            double la = infoModels[0].h - 2 * c;
            double lb = infoModels[infoModels.Count - 1].h - 2 * c;
            SingleMainTopBarModel singleMainTopBarModel = new SingleMainTopBarModel(2, bar, la, lb, x, y, Math.Abs(infoModels[0].zOffset));
            return singleMainTopBarModel;
        }
        public static List<LocationBarModel> GetLocationSingleMainTopBarModels(List<InfoModel> infoModels, List<NodeModel> nodeModels, double c,double la, double lb)
        {
            double x = c;
            double y = Math.Abs(infoModels[0].zOffset) + c;
            //double la = infoModels[0].h - 2 * c;
            //double lb = infoModels[infoModels.Count - 1].h - 2 * c;
            List<LocationBarModel> Location = new List<LocationBarModel>();
            Location.Add(new LocationBarModel(x, y + la));
            Location.Add(new LocationBarModel(x, y));
            if (infoModels.Count > nodeModels.Count)
            {
                List<int> z = new List<int>();
                for (int i = 1; i < infoModels.Count; i++)
                {
                    if (infoModels[i - 1].zOffset != infoModels[i].zOffset)
                    {
                        z.Add(i);
                    }
                }
                if (z.Count == 0)
                {
                    Location.Add(new LocationBarModel(infoModels[infoModels.Count - 1].endPosition - c, Math.Abs(infoModels[infoModels.Count - 1].zOffset) + c));
                    Location.Add(new LocationBarModel(infoModels[infoModels.Count - 1].endPosition - c, Math.Abs(infoModels[infoModels.Count - 1].zOffset) + c + lb));
                    return Location;
                }
                else
                {
                    for (int i = 0; i < z.Count; i++)
                    {
                        Location.Add(new LocationBarModel(infoModels[z[i] - 1].endPosition + c, Math.Abs(infoModels[z[i] - 1].zOffset) + c));
                        Location.Add(new LocationBarModel(infoModels[z[i]].startPosition - c, Math.Abs(infoModels[z[i] - 1].zOffset) + c + (infoModels[z[i] - 1].zOffset - infoModels[z[i]].zOffset)));

                    }
                    Location.Add(new LocationBarModel(infoModels[infoModels.Count - 1].endPosition - c, Math.Abs(infoModels[infoModels.Count - 1].zOffset) + c));
                    Location.Add(new LocationBarModel(infoModels[infoModels.Count - 1].endPosition - c, Math.Abs(infoModels[infoModels.Count - 1].zOffset) + c + lb));
                }
            }
            else
            {
                if (infoModels.Count == nodeModels.Count)
                {
                    if (infoModels[0].ConsolLeft)
                    {

                        List<int> z = new List<int>();
                        for (int i = 1; i < infoModels.Count; i++)
                        {
                            if (infoModels[i - 1].zOffset != infoModels[i].zOffset)
                            {
                                z.Add(i);
                            }
                        }
                        if (z.Count == 0)
                        {
                            Location.Add(new LocationBarModel(infoModels[infoModels.Count - 1].endPosition + nodeModels[nodeModels.Count - 1].Width - c, Math.Abs(infoModels[infoModels.Count - 1].zOffset) + c));
                            Location.Add(new LocationBarModel(infoModels[infoModels.Count - 1].endPosition + nodeModels[nodeModels.Count - 1].Width - c, Math.Abs(infoModels[infoModels.Count - 1].zOffset) + c + lb));
                            return Location;
                        }
                        else
                        {
                            for (int i = 0; i < z.Count; i++)
                            {
                                Location.Add(new LocationBarModel(infoModels[z[i] - 1].endPosition + c, Math.Abs(infoModels[z[i] - 1].zOffset) + c));
                                Location.Add(new LocationBarModel(infoModels[z[i]].startPosition - c, Math.Abs(infoModels[z[i] - 1].zOffset) + c + (infoModels[z[i] - 1].zOffset - infoModels[z[i]].zOffset)));
                            }
                            Location.Add(new LocationBarModel(infoModels[infoModels.Count - 1].endPosition + nodeModels[nodeModels.Count - 1].Width - c, Math.Abs(infoModels[infoModels.Count - 1].zOffset) + c));
                            Location.Add(new LocationBarModel(infoModels[infoModels.Count - 1].endPosition + nodeModels[nodeModels.Count - 1].Width - c, Math.Abs(infoModels[infoModels.Count - 1].zOffset) + c + lb));
                        }
                    }
                    else
                    {
                        List<int> z = new List<int>();
                        for (int i = 1; i < infoModels.Count; i++)
                        {
                            if (infoModels[i - 1].zOffset != infoModels[i].zOffset)
                            {
                                z.Add(i);
                            }
                        }
                        if (z.Count == 0)
                        {
                            Location.Add(new LocationBarModel(infoModels[infoModels.Count - 1].endPosition - c, Math.Abs(infoModels[infoModels.Count - 1].zOffset) + c));
                            Location.Add(new LocationBarModel(infoModels[infoModels.Count - 1].endPosition - c, Math.Abs(infoModels[infoModels.Count - 1].zOffset) + c + lb));
                            return Location;
                        }
                        else
                        {
                            for (int i = 0; i < z.Count; i++)
                            {
                                Location.Add(new LocationBarModel(infoModels[z[i] - 1].endPosition + c, Math.Abs(infoModels[z[i] - 1].zOffset) + c));
                                Location.Add(new LocationBarModel(infoModels[z[i]].startPosition - c, Math.Abs(infoModels[z[i] - 1].zOffset) + c + (infoModels[z[i] - 1].zOffset - infoModels[z[i]].zOffset)));

                            }
                            Location.Add(new LocationBarModel(infoModels[infoModels.Count - 1].endPosition - c, Math.Abs(infoModels[infoModels.Count - 1].zOffset) + c));
                            Location.Add(new LocationBarModel(infoModels[infoModels.Count - 1].endPosition - c, Math.Abs(infoModels[infoModels.Count - 1].zOffset) + c + lb));
                        }
                    }
                }
                else
                {
                    List<int> z = new List<int>();
                    for (int i = 1; i < infoModels.Count; i++)
                    {
                        if (infoModels[i - 1].zOffset != infoModels[i].zOffset)
                        {
                            z.Add(i);
                        }
                    }
                    if (z.Count == 0)
                    {
                        Location.Add(new LocationBarModel(infoModels[infoModels.Count - 1].endPosition + nodeModels[nodeModels.Count - 1].Width - c, Math.Abs(infoModels[infoModels.Count - 1].zOffset) + c));
                        Location.Add(new LocationBarModel(infoModels[infoModels.Count - 1].endPosition + nodeModels[nodeModels.Count - 1].Width - c, Math.Abs(infoModels[infoModels.Count - 1].zOffset) + c + lb));
                        return Location;
                    }
                    else
                    {
                        for (int i = 0; i < z.Count; i++)
                        {
                            Location.Add(new LocationBarModel(infoModels[z[i] - 1].endPosition + c, Math.Abs(infoModels[z[i] - 1].zOffset) + c));
                            Location.Add(new LocationBarModel(infoModels[z[i]].startPosition - c, Math.Abs(infoModels[z[i] - 1].zOffset) + c + (infoModels[z[i] - 1].zOffset - infoModels[z[i]].zOffset)));
                        }
                        Location.Add(new LocationBarModel(infoModels[infoModels.Count - 1].endPosition + nodeModels[nodeModels.Count - 1].Width - c, Math.Abs(infoModels[infoModels.Count - 1].zOffset) + c));
                        Location.Add(new LocationBarModel(infoModels[infoModels.Count - 1].endPosition + nodeModels[nodeModels.Count - 1].Width - c, Math.Abs(infoModels[infoModels.Count - 1].zOffset) + c + lb));
                    }
                }
            }



            return Location;
        }
        #endregion
        #region Main Bottom bar
        public static List<MainBottomBarModel> GetMainBottomBarModels(List<InfoModel> infoModels, List<NodeModel> nodeModels, RebarBarModel bar, double c)
        {
            List<MainBottomBarModel> mainBottomBarModels = new List<MainBottomBarModel>();
            if (infoModels.Count > nodeModels.Count)
            {
                List<int> z = new List<int>();
                for (int i = 1; i < infoModels.Count; i++)
                {
                    if ((Math.Abs(infoModels[i - 1].zOffset) + infoModels[i - 1].h) != (Math.Abs(infoModels[i].zOffset) + infoModels[i].h))
                    {
                        z.Add(i);
                    }
                }
                if (z.Count == 0)
                {
                    mainBottomBarModels.Add(new MainBottomBarModel(1, 2, bar,
                        Math.Abs(infoModels[0].startPosition - infoModels[infoModels.Count - 1].endPosition) - 2 * c,
                        infoModels[0].h - 2 * c,
                        0,
                        infoModels[infoModels.Count - 1].h - 2 * c,
                        0,
                        c,
                        infoModels[0].h + Math.Abs(infoModels[0].zOffset) - c, infoModels[0].h + Math.Abs(infoModels[0].zOffset)
                        ));
                    
                }
                else
                {
                    if (z.Count == 1)
                    {
                        double l1 = Math.Abs(infoModels[0].startPosition - infoModels[z[0] - 1].endPosition) + nodeModels[z[0] - 1].Width - 2 * c;
                        mainBottomBarModels.Add(new MainBottomBarModel(1, 2, bar,
                        l1,
                        infoModels[0].h - 2 * c,
                        0,
                        infoModels[z[0] - 1].h - 2 * c,
                        0,
                        c,
                        infoModels[0].h + Math.Abs(infoModels[0].zOffset) - c, infoModels[0].h + Math.Abs(infoModels[0].zOffset) 
                        ));
                        double l2 = Math.Abs(infoModels[infoModels.Count - 1].endPosition - infoModels[z[0]].startPosition) + nodeModels[z[0] - 1].Width - 2 * c;
                        mainBottomBarModels.Add(new MainBottomBarModel(2, 2, bar,
                        l2,
                        infoModels[z[0]].h - 2 * c,
                        0,
                        infoModels[infoModels.Count - 1].h - 2 * c,
                        0,
                       nodeModels[z[0] - 1].Start + c,
                         infoModels[z[0]].h + Math.Abs(infoModels[z[0]].zOffset) - c, infoModels[z[0]].h + Math.Abs(infoModels[z[0]].zOffset) 
                        ));
                        
                    }
                    else
                    {
                        for (int i = 0; i <= z.Count; i++)
                        {
                            if (i == 0)
                            {
                                double l = Math.Abs(infoModels[0].startPosition - infoModels[z[i] - 1].endPosition) + nodeModels[z[i] - 1].Width - 2 * c;
                                mainBottomBarModels.Add(new MainBottomBarModel(i + 1, 2, bar,
                                l,
                                infoModels[z[i]].h - 2 * c,
                                0,
                                infoModels[z[i]].h - 2 * c,
                                0,
                                c,
                                infoModels[0].h + Math.Abs(infoModels[0].zOffset) - c, infoModels[0].h + Math.Abs(infoModels[0].zOffset) 
                                ));
                            }
                            else
                            {
                                if (i == z.Count)
                                {
                                    double l = Math.Abs(infoModels[infoModels.Count - 1].endPosition - infoModels[z[i - 1]].startPosition) + nodeModels[z[i - 1] - 1].Width - 2 * c;
                                    mainBottomBarModels.Add(new MainBottomBarModel(i + 1, 2, bar,
                               l,
                               infoModels[z[i - 1]].h - 2 * c,
                               0,
                               infoModels[z[i - 1]].h - 2 * c,
                               0,
                               nodeModels[z[i - 1] - 1].Start + c,
                               infoModels[z[i - 1]].h + Math.Abs(infoModels[z[i - 1]].zOffset) - c, infoModels[z[i - 1]].h + Math.Abs(infoModels[z[i - 1]].zOffset) 
                               ));
                                }
                                else
                                {
                                    double l = Math.Abs(infoModels[z[i - 1]].startPosition - infoModels[z[i] - 1].endPosition) + nodeModels[z[i - 1] - 1].Width + nodeModels[z[i] - 1].Width - 2 * c;
                                    mainBottomBarModels.Add(new MainBottomBarModel(i + 1, 2, bar,
                                        l,
                                        infoModels[z[i - 1]].h - 2 * c,
                                        0,
                                        infoModels[z[i]].h - 2 * c,
                                        0,
                                        nodeModels[z[i - 1] - 1].Start + c,
                                        infoModels[z[i - 1]].h + Math.Abs(infoModels[z[i - 1]].zOffset) - c,infoModels[z[i - 1]].h + Math.Abs(infoModels[z[i - 1]].zOffset) 
                                        ));
                                }

                            }

                        }
                    }

                }
            }
            else
            {
                if (infoModels.Count == nodeModels.Count)
                {
                    if (infoModels[0].ConsolLeft)
                    {
                        List<int> z = new List<int>();
                        for (int i = 1; i < infoModels.Count; i++)
                        {
                            if ((Math.Abs(infoModels[i - 1].zOffset) + infoModels[i - 1].h) != (Math.Abs(infoModels[i].zOffset) + infoModels[i].h))
                            {
                                z.Add(i);
                            }
                        }
                        if (z.Count == 0)
                        {
                            mainBottomBarModels.Add(new MainBottomBarModel(1, 2, bar,
                       Math.Abs(infoModels[0].startPosition - nodeModels[nodeModels.Count - 1].End) - 2 * c,
                       infoModels[0].h - 2 * c,
                       0,
                       infoModels[infoModels.Count - 1].h - 2 * c,
                       0,
                       c,
                       infoModels[0].h + Math.Abs(infoModels[0].zOffset) - c, infoModels[0].h + Math.Abs(infoModels[0].zOffset) 
                       ));
                        }
                        else
                        {
                            if (z.Count == 1)
                            {
                                double l1 = Math.Abs(infoModels[0].startPosition - infoModels[z[0] - 1].endPosition) + nodeModels[z[0] - 1].Width - 2 * c;
                                mainBottomBarModels.Add(new MainBottomBarModel(1, 2, bar,
                                l1,
                                infoModels[0].h - 2 * c,
                                0,
                                infoModels[z[0] - 1].h - 2 * c,
                                0,
                                c,
                                infoModels[0].h + Math.Abs(infoModels[0].zOffset) - c, infoModels[0].h + Math.Abs(infoModels[0].zOffset) 
                                ));
                                double l2 = Math.Abs(infoModels[infoModels.Count - 1].endPosition - infoModels[z[0]].startPosition) + nodeModels[z[0] - 1].Width + nodeModels[z[0]].Width - 2 * c;
                                mainBottomBarModels.Add(new MainBottomBarModel(2, 2, bar,
                                l2,
                                infoModels[z[0]].h - 2 * c,
                                0,
                                infoModels[infoModels.Count - 1].h - 2 * c,
                                0,
                                infoModels[z[0] - 1].endPosition + c,
                                infoModels[z[0]].h + Math.Abs(infoModels[z[0]].zOffset) - c, infoModels[z[0]].h + Math.Abs(infoModels[z[0]].zOffset) 
                                ));
                            }
                            else
                            {
                                for (int i = 0; i <= z.Count; i++)
                                {
                                    if (i == 0)
                                    {
                                        double l = Math.Abs(infoModels[0].startPosition - infoModels[z[i] - 1].endPosition) + nodeModels[z[i] - 1].Width - 2 * c;
                                        mainBottomBarModels.Add(new MainBottomBarModel(i + 1, 2, bar,
                                        l,
                                        infoModels[z[i]].h - 2 * c,
                                        0,
                                        infoModels[z[i]].h - 2 * c,
                                        0,
                                        c,
                                        infoModels[0].h + Math.Abs(infoModels[0].zOffset) - c, infoModels[0].h + Math.Abs(infoModels[0].zOffset) 
                                        ));
                                    }
                                    else
                                    {
                                        if (i == z.Count)
                                        {
                                            double l = Math.Abs(infoModels[infoModels.Count - 1].endPosition - infoModels[z[i - 1]].startPosition) + nodeModels[z[i - 1] - 1].Width - 2 * c + nodeModels[z[i - 1] + 1].Width;
                                            mainBottomBarModels.Add(new MainBottomBarModel(i + 1, 2, bar,
                                       l,
                                       infoModels[z[i - 1]].h - 2 * c,
                                       0,
                                       infoModels[z[i - 1]].h - 2 * c,
                                       0,
                                       nodeModels[z[i - 1] - 1].Start + c,
                                       infoModels[z[i - 1]].h + Math.Abs(infoModels[z[i - 1]].zOffset) - c, infoModels[z[i - 1]].h + Math.Abs(infoModels[z[i - 1]].zOffset) 
                                       ));
                                        }
                                        else
                                        {
                                            double l = Math.Abs(infoModels[z[i - 1]].startPosition - infoModels[z[i] - 1].endPosition) + nodeModels[z[i - 1] - 1].Width + nodeModels[z[i] - 1].Width - 2 * c;
                                            mainBottomBarModels.Add(new MainBottomBarModel(i + 1, 2, bar,
                                                l,
                                                infoModels[z[i - 1]].h - 2 * c,
                                                0,
                                                infoModels[z[i] - 1].h - 2 * c,
                                                0,
                                                nodeModels[z[i - 1] - 1].Start - c,
                                                infoModels[z[i - 1]].h + Math.Abs(infoModels[z[i - 1]].zOffset) - c, infoModels[z[i - 1]].h + Math.Abs(infoModels[z[i - 1]].zOffset) 
                                                ));
                                        }

                                    }

                                }
                            }

                        }
                    }
                    else
                    {
                        List<int> z = new List<int>();
                        for (int i = 1; i < infoModels.Count; i++)
                        {
                            if ((Math.Abs(infoModels[i - 1].zOffset) + infoModels[i - 1].h) != (Math.Abs(infoModels[i].zOffset) + infoModels[i].h))
                            {
                                z.Add(i);
                            }
                        }
                        if (z.Count == 0)
                        {
                            mainBottomBarModels.Add(new MainBottomBarModel(1, 2, bar,
                       Math.Abs(nodeModels[0].Start - infoModels[infoModels.Count - 1].endPosition) - 2 * c,
                       infoModels[0].h - 2 * c,
                       0,
                       infoModels[infoModels.Count - 1].h - 2 * c,
                       0,
                       c,
                       infoModels[0].h + Math.Abs(infoModels[0].zOffset) - c, infoModels[0].h + Math.Abs(infoModels[0].zOffset) 
                       ));
                        }
                        else
                        {
                            if (z.Count == 1)
                            {
                                double l1 = Math.Abs(infoModels[0].startPosition - infoModels[z[0] - 1].endPosition) + nodeModels[0].Width + nodeModels[z[0] - 1].Width - 2 * c;
                                mainBottomBarModels.Add(new MainBottomBarModel(1, 2, bar,
                                l1,
                                infoModels[0].h - 2 * c,
                                0,
                                infoModels[z[0] - 1].h - 2 * c,
                                0,
                                c,
                                infoModels[0].h + Math.Abs(infoModels[0].zOffset) - c, infoModels[0].h + Math.Abs(infoModels[0].zOffset) 
                                ));
                                double l2 = Math.Abs(infoModels[infoModels.Count - 1].endPosition - infoModels[z[0]].startPosition) + nodeModels[z[0] - 1].Width - 2 * c;
                                mainBottomBarModels.Add(new MainBottomBarModel(2, 2, bar,
                                l2,
                                infoModels[z[0]].h - 2 * c,
                                0,
                                infoModels[infoModels.Count - 1].h - 2 * c,
                                0,
                                infoModels[z[0] - 1].endPosition + c,
                                infoModels[z[0]].h + Math.Abs(infoModels[z[0]].zOffset) - c, infoModels[z[0]].h + Math.Abs(infoModels[z[0]].zOffset)
                                ));
                            }
                            else
                            {
                                for (int i = 0; i <= z.Count; i++)
                                {
                                    if (i == 0)
                                    {
                                        double l = Math.Abs(infoModels[0].startPosition - infoModels[z[i] - 1].endPosition) + nodeModels[0].Width + nodeModels[z[i]].Width - 2 * c;
                                        mainBottomBarModels.Add(new MainBottomBarModel(i + 1, 2, bar,
                                        l,
                                        infoModels[z[i]].h - 2 * c,
                                        0,
                                        infoModels[z[i]].h - 2 * c,
                                        0,
                                        c,
                                        infoModels[0].h + Math.Abs(infoModels[0].zOffset) - c, infoModels[0].h + Math.Abs(infoModels[0].zOffset) 
                                        ));
                                    }
                                    else
                                    {
                                        if (i == z.Count)
                                        {
                                            double l = Math.Abs(infoModels[infoModels.Count - 1].endPosition - infoModels[z[i - 1]].startPosition) + nodeModels[z[i - 1]].Width - 2 * c;
                                            mainBottomBarModels.Add(new MainBottomBarModel(i + 1, 2, bar,
                                       l,
                                       infoModels[z[i - 1]].h - 2 * c,
                                       0,
                                       infoModels[z[i - 1]].h - 2 * c,
                                       0,
                                       nodeModels[z[i - 1]].Start + c,
                                       infoModels[z[i - 1]].h + Math.Abs(infoModels[z[i - 1]].zOffset) - c, infoModels[z[i - 1]].h + Math.Abs(infoModels[z[i - 1]].zOffset) 
                                       ));
                                        }
                                        else
                                        {
                                            double l = Math.Abs(infoModels[z[i - 1]].startPosition - infoModels[z[i] - 1].endPosition) + nodeModels[z[i - 1] - 1].Width + nodeModels[z[i] - 1].Width - 2 * c;
                                            mainBottomBarModels.Add(new MainBottomBarModel(i + 1, 2, bar,
                                                l,
                                                infoModels[z[i - 1]].h - 2 * c,
                                                0,
                                                infoModels[z[i] - 1].h - 2 * c,
                                                0,
                                                nodeModels[z[i - 1] - 1].Start - c,
                                                infoModels[z[i - 1]].h + Math.Abs(infoModels[z[i - 1]].zOffset) - c, infoModels[z[i - 1]].h + Math.Abs(infoModels[z[i - 1]].zOffset) 
                                                ));
                                        }
                                    }
                                }
                            }

                        }
                    }
                }
                else
                {
                    List<int> z = new List<int>();
                    for (int i = 1; i < infoModels.Count; i++)
                    {
                        if ((Math.Abs(infoModels[i - 1].zOffset) + infoModels[i - 1].h) != (Math.Abs(infoModels[i].zOffset) + infoModels[i].h))
                        {
                            z.Add(i);
                        }
                    }
                    if (z.Count == 0)
                    {
                        mainBottomBarModels.Add(new MainBottomBarModel(1, 2, bar,
                   Math.Abs(infoModels[0].startPosition - infoModels[infoModels.Count - 1].endPosition) + nodeModels[0].Width + nodeModels[nodeModels.Count - 1].Width - 2 * c,
                   infoModels[0].h - 2 * c,
                   0,
                   infoModels[infoModels.Count - 1].h - 2 * c,
                   0,
                   c,
                   infoModels[0].h + Math.Abs(infoModels[0].zOffset) - c, infoModels[0].h + Math.Abs(infoModels[0].zOffset) 
                   ));
                    }
                    else
                    {
                        if (z.Count == 1)
                        {
                            double l1 = Math.Abs(infoModels[0].startPosition - infoModels[z[0] - 1].endPosition) + nodeModels[0].Width + nodeModels[z[0] ].Width - 2 * c;
                            mainBottomBarModels.Add(new MainBottomBarModel(1, 2, bar,
                            l1,
                            infoModels[0].h - 2 * c,
                            0,
                            infoModels[z[0] - 1].h - 2 * c,
                            0,
                            c,
                            infoModels[0].h + Math.Abs(infoModels[0].zOffset) - c, infoModels[0].h + Math.Abs(infoModels[0].zOffset)
                            ));
                            double l2 = Math.Abs(infoModels[infoModels.Count - 1].endPosition - infoModels[z[0]].startPosition) + nodeModels[z[0] ].Width + nodeModels[nodeModels.Count - 1].Width - 2 * c;
                            mainBottomBarModels.Add(new MainBottomBarModel(2, 2, bar,
                            l2,
                            infoModels[z[0]].h - 2 * c,
                            0,
                            infoModels[infoModels.Count - 1].h - 2 * c,
                            0,
                            infoModels[z[0]].startPosition + c - nodeModels[z[0]].Width,
                            infoModels[z[0]].h + Math.Abs(infoModels[z[0]].zOffset) - c, infoModels[z[0]].h + Math.Abs(infoModels[z[0]].zOffset) 
                            ));
                        }
                        else
                        {
                            for (int i = 0; i <= z.Count; i++)
                            {
                                if (i == 0)
                                {
                                    double l = Math.Abs(infoModels[0].startPosition - infoModels[z[i] - 1].endPosition) + nodeModels[0].Width + nodeModels[z[i]].Width - 2 * c;
                                    mainBottomBarModels.Add(new MainBottomBarModel(i + 1, 2, bar,
                                    l,
                                    infoModels[z[i]].h - 2 * c,
                                    0,
                                    infoModels[z[i]].h - 2 * c,
                                    0,
                                    c,
                                    infoModels[0].h + Math.Abs(infoModels[0].zOffset) - c, infoModels[0].h + Math.Abs(infoModels[0].zOffset) 
                                    ));
                                }
                                else
                                {
                                    if (i == z.Count)
                                    {
                                        double l = Math.Abs(infoModels[infoModels.Count - 1].endPosition - infoModels[z[i - 1]].startPosition) + nodeModels[z[i - 1]].Width + nodeModels[nodeModels.Count - 1].Width - 2 * c;
                                        mainBottomBarModels.Add(new MainBottomBarModel(i + 1, 2, bar,
                                   l,
                                   infoModels[z[i - 1]].h - 2 * c,
                                   0,
                                   infoModels[z[i - 1]].h - 2 * c,
                                   0,
                                   nodeModels[z[i - 1]].Start + c,
                                   infoModels[z[i - 1]].h + Math.Abs(infoModels[z[i - 1]].zOffset) - c, infoModels[z[i - 1]].h + Math.Abs(infoModels[z[i - 1]].zOffset)
                                   ));
                                    }
                                    else
                                    {
                                        double l = Math.Abs(infoModels[z[i - 1]].startPosition - infoModels[z[i] - 1].endPosition) + nodeModels[z[i - 1]].Width + nodeModels[z[i]].Width - 2 * c;
                                        mainBottomBarModels.Add(new MainBottomBarModel(i + 1, 2, bar,
                                            l,
                                            infoModels[z[i - 1]].h - 2 * c,
                                            0,
                                            infoModels[z[i] - 1].h - 2 * c,
                                            0,
                                            nodeModels[z[i - 1]].Start + c,
                                            infoModels[z[i - 1]].h + Math.Abs(infoModels[z[i - 1]].zOffset) - c, infoModels[z[i - 1]].h + Math.Abs(infoModels[z[i - 1]].zOffset) 
                                            ));
                                    }

                                }

                            }
                        }
                    }
                }
            }
            return mainBottomBarModels;
        }
        #endregion
        #region SpecialPoint
        public static List<SpecialNodeModel> GetSpecialNodeModel(List<InfoModel> infoModels, List<Element> beams, Document doc, Line line, string level)
        {
            List<Level> levels = new FilteredElementCollector(doc).OfClass(typeof(Level)).Cast<Level>().ToList();
            levels.OrderBy(x => x.get_Parameter(BuiltInParameter.LEVEL_ELEV).AsDouble());
            Level level0 = levels[0];
            XYZ l = line.Direction;
            List<SpecialNodeModel> specialNodeModels = new List<SpecialNodeModel>();
            if (beams[0].get_Parameter(BuiltInParameter.STRUCTURAL_REFERENCE_LEVEL_ELEVATION).AsDouble() == level0.get_Parameter(BuiltInParameter.LEVEL_ELEV).AsDouble())
            {
                PlanarFace planar = GetPlanrFaceFirst(infoModels, beams, doc, line, level);
                List<Element> beamsSpecial = BeamsBoundBox.GetAllSpecialBeams(beams, doc, line, level);
                int node = 1;
                for (int i = 0; i < infoModels.Count; i++)
                {
                    for (int j = 0; j < beamsSpecial.Count; j++)
                    {
                        List<PlanarFace> beam1 = SolidFace.GetPlanrFacePerpendicularOne(beamsSpecial[j], l);
                        double d0, d1;
                        PlanarFace planarFace0, planarFace1;
                        Getd0d1(beam1, planar, doc, out d0, out d1, out planarFace0, out planarFace1);
                        //if (ConditionPlanarFace(beam1, planar, infoModels[i].startPosition, infoModels[i].endPosition, doc))
                        //{
                        //    double d0 = PointModel.DistanceTo2(planar ,beam1[0].Origin, doc);
                        //    double d1 = PointModel.DistanceTo2(planar, beam1[1].Origin, doc);
                        //    specialNodeModels.Add(new SpecialNodeModel(node,infoModels[i].NumberSpan,d0,d1,beamsSpecial[j],beam1[0],beam1[1],doc));
                        //    node++;
                        //}
                        if (ConditionPlanarFace(d0, d1, infoModels[i].startPosition, infoModels[i].endPosition))
                        {

                            specialNodeModels.Add(new SpecialNodeModel(node, infoModels[i].NumberSpan, d0, d1, beamsSpecial[j], planarFace0, planarFace1, doc));
                            node++;
                        }
                    }
                    
                }
            }
            else
            {
                PlanarFace planar = GetPlanrFaceFirst(infoModels, beams, doc, line, level);
                List<Element> beamsSpecial = BeamsBoundBox.GetAllSpecialBeams(beams, doc, line, level);
                int node = 1;
                List<Element> columnsSpecial = BeamsBoundBox.GetAllSpacialColumns(beams, doc, level);
                for (int i = 0; i < infoModels.Count; i++)
                {
                    for (int j = 0; j < beamsSpecial.Count; j++)
                    {
                        List<PlanarFace> beam1 = SolidFace.GetPlanrFacePerpendicularOne(beamsSpecial[j], l);
                        double d0, d1;
                        PlanarFace planarFace0, planarFace1;
                        Getd0d1(beam1, planar, doc, out d0, out d1, out planarFace0, out planarFace1);
                        //if (ConditionPlanarFace(beam1, planar, infoModels[i].startPosition, infoModels[i].endPosition, doc))
                        //{
                        //    double d0 = PointModel.DistanceTo2(planar ,beam1[0].Origin, doc);
                        //    double d1 = PointModel.DistanceTo2(planar, beam1[1].Origin, doc);
                        //    specialNodeModels.Add(new SpecialNodeModel(node,infoModels[i].NumberSpan,d0,d1,beamsSpecial[j],beam1[0],beam1[1],doc));
                        //    node++;
                        //}
                        if (ConditionPlanarFace(d0, d1, infoModels[i].startPosition, infoModels[i].endPosition))
                        {

                            specialNodeModels.Add(new SpecialNodeModel(node, infoModels[i].NumberSpan, d0, d1, beamsSpecial[j], planarFace0, planarFace1, doc));
                            node++;
                        }

                    }
                    for (int k = 0; k < columnsSpecial.Count; k++)
                    {
                        List<PlanarFace> column1 = SolidFace.GetPlanrFacePerpendicularOne(columnsSpecial[k], l);
                        double d0, d1;
                        PlanarFace planarFace0, planarFace1;
                        Getd0d1(column1, planar, doc, out d0, out d1, out planarFace0, out planarFace1);
                        //if (ConditionPlanarFace(column1, planar, infoModels[i].startPosition, infoModels[i].endPosition, doc))
                        //{
                        //    double d0 = PointModel.DistanceTo2(planar, column1[0].Origin, doc);
                        //    double d1 = PointModel.DistanceTo2(planar, column1[1].Origin, doc);
                        //    specialNodeModels.Add(new SpecialNodeModel(node,infoModels[i].NumberSpan, d0, d1, columnsSpecial[k], column1[0], column1[1], doc));
                        //    node++;
                        //}
                        if (ConditionPlanarFace(d0, d1, infoModels[i].startPosition, infoModels[i].endPosition))
                        {
                            specialNodeModels.Add(new SpecialNodeModel(node, infoModels[i].NumberSpan, d0, d1, columnsSpecial[k], planarFace0, planarFace1, doc));
                            node++;
                        }
                    }
                }
            }
           
            if (specialNodeModels.Count!=0)
            {

                specialNodeModels = specialNodeModels.Distinct(new DistinctSpecialNode()).ToList();
                specialNodeModels.Sort((x,y)=>x.Mid.CompareTo(y.Mid));
                for (int i = 0; i < specialNodeModels.Count; i++)
                {
                    specialNodeModels[i].NumberNode = i + 1;
                }
            }
            return specialNodeModels;
        }
        #endregion
        #region Orther
        public static double GetDiameterStirrupMax(List<InfoModel> infoModels,List<DistributeStirrup> distributeStirrups, List<StirrupModel> stirrupModels)
        {
            double dsmax = 0;
            for (int i = 0; i < infoModels.Count; i++)
            {
                distributeStirrups[i].GetL1L2(infoModels[i].Length);
                if (dsmax <= stirrupModels[i].BarS.Diameter)
                {
                    dsmax = stirrupModels[i].BarS.Diameter;
                }
            }
            return dsmax;
        }

       
        public static IEnumerable<T> FindLogicalChildren<T>(DependencyObject depObj) where T : DependencyObject
        {
            if (depObj != null)
            {
                foreach (object rawChild in LogicalTreeHelper.GetChildren(depObj))
                {
                    if (rawChild is DependencyObject)
                    {
                        DependencyObject child = (DependencyObject)rawChild;
                        if (child is T)
                        {
                            yield return (T)child;
                        }

                        foreach (T childOfChild in FindLogicalChildren<T>(child))
                        {
                            yield return childOfChild;
                        }
                    }
                }
            }
        }

        public static T FindVisualParent<T>(DependencyObject child) where T : DependencyObject
        {
            // get parent item
            DependencyObject parentObject = VisualTreeHelper.GetParent(child);

            // we’ve reached the end of the tree
            if (parentObject == null) return null;

            // check if the parent matches the type we’re looking for
            T parent = parentObject as T;
            if (parent != null)
            {
                return parent;
            }
            else
            {
                // use recursion to proceed with next level
                return FindVisualParent<T>(parentObject);
            }
        }
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
        #region PlanrFace 0
        public static PlanarFace GetPlanrFaceFirst(List<InfoModel> infoModels, List<Element> beams, Document doc, Line line, string level)
        {
            XYZ l = line.Direction;
            List<PlanarFace> planrBeams = SolidFace.GetAllPlanrFacePerpendicular(beams, l);
            planrBeams = SolidFace.ArrangePlanrFace(planrBeams, l);
            List<Element> columns = BeamsBoundBox.GetColumnsBoudingBoxBeams(beams, doc);
            List<Element> columnsTopLevel = BeamsBoundBox.GetColumnsSameTopLevelBeams(columns, level);
            List<Element> beamPer = BeamsBoundBox.GetBeamsPerNodeBeams(beams, doc);
            List<PlanarFace> planars = null;
            if (columnsTopLevel.Count == 0)
            {
                List<PlanarFace> planrBeamPer0 = SolidFace.GetAllPlanrFacePerpendicular(beamPer, l);
                planrBeamPer0 = SolidFace.ArrangePlanrFace(planrBeamPer0, l);
                planars = SolidFace.AddRangePlanarFace(planrBeamPer0, planrBeams, l);
                planars = SolidFace.ArrangePlanrFace(planars, l);
            }
            else
            {
                List<Element> beamPer1 = BeamsBoundBox.GetBeamsBoudingBoxSameTopLevelColumns(columnsTopLevel, doc, line, level);
                List<Element> beamPer0 = BeamsBoundBox.RemoveList(beamPer, beamPer1, line);
                if (beamPer0.Count == 0)
                {
                    List<PlanarFace> planrColumnsTop = SolidFace.GetAllPlanrFacePerpendicular(columnsTopLevel, l);
                    planrColumnsTop = SolidFace.ArrangePlanrFace(planrColumnsTop, l);
                    planars = SolidFace.AddRangePlanarFace(planrColumnsTop, planrBeams, l);
                    planars = SolidFace.ArrangePlanrFace(planars, l);
                }
                else
                {
                    #region
                    List<PlanarFace> planrColumnsTop = SolidFace.GetAllPlanrFacePerpendicular(columnsTopLevel, l);
                    planrColumnsTop = SolidFace.ArrangePlanrFace(planrColumnsTop, l);
                    List<PlanarFace> planrBeamPer0 = SolidFace.GetAllPlanrFacePerpendicular(beamPer0, l);
                    planrBeamPer0 = SolidFace.ArrangePlanrFace(planrBeamPer0, l);
                    planars = SolidFace.AddRangePlanarFace(planrColumnsTop, planrBeams, l);
                    planars = SolidFace.AddRangePlanarFace(planars, planrBeamPer0, l);
                    planars = SolidFace.ArrangePlanrFace(planars, l);
                    #endregion
                }
            }

            return planars[0];
        }
        public static List<PlanarFace> GetPlanarFaceNode(List<Element> beams, Document document,Line line, string level, List<Element> foundation, List<Element> floor, List<Element> beamper, List<Element> columnsLevel,List<Element> walls)
        {
            XYZ l = line.Direction;
            List<Level> levels = new FilteredElementCollector(document).OfClass(typeof(Level)).Cast<Level>().ToList();
            levels.OrderBy(x => x.get_Parameter(BuiltInParameter.LEVEL_ELEV).AsDouble());
            Level level0 = levels[0];
            List<PlanarFace> planarFaces = new List<PlanarFace>();
            if (beams[0].get_Parameter(BuiltInParameter.STRUCTURAL_REFERENCE_LEVEL_ELEVATION).AsDouble() == level0.get_Parameter(BuiltInParameter.LEVEL_ELEV).AsDouble())
            {

                if (foundation.Count != 0)
                {
                    for (int i = 0; i < foundation.Count; i++)
                    {
                        Solid a = SolidFace.GetSolidElement(foundation[i]);
                        List<PlanarFace> p = SolidFace.GetPlanrFacePerpendicularOne(foundation[i], l);
                        if (p.Count == 2)
                        {
                            planarFaces.AddRange(p);
                        }
                    }
                }

                if (floor.Count != 0)
                {
                    for (int i = 0; i < floor.Count; i++)
                    {
                        Solid a = SolidFace.GetSolidElement(floor[i]);
                        List<PlanarFace> p = SolidFace.GetPlanrFacePerpendicularOne(floor[i], l);
                        if (p.Count == 2)
                        {
                            planarFaces.AddRange(p);
                        }
                    }
                }

               
                if (beamper.Count != 0)
                {
                    for (int i = 0; i < beamper.Count; i++)
                    {
                        Solid a = SolidFace.GetSolidElement(beamper[i]);
                        List<PlanarFace> p = SolidFace.GetPlanrFacePerpendicularOne(beamper[i], l);
                        planarFaces.AddRange(p);
                    }
                }
                if (columnsLevel.Count != 0)
                {
                    for (int i = 0; i < columnsLevel.Count; i++)
                    {
                        Solid a = SolidFace.GetSolidElement(columnsLevel[i]);
                        List<PlanarFace> p = SolidFace.GetPlanrFacePerpendicularOne(columnsLevel[i], l);
                        if (p.Count == 2)
                        {
                            planarFaces.AddRange(p);
                        }
                    }
                }
                if (walls.Count != 0)
                {
                    for (int i = 0; i < walls.Count; i++)
                    {
                        Solid a = SolidFace.GetSolidElement(walls[i]);
                        List<PlanarFace> p = SolidFace.GetPlanrFacePerpendicularOne(walls[i], l);
                        planarFaces.AddRange(p);
                    }
                }
            }
            else
            {
                if (beamper.Count!=0)
                {
                    for (int i = 0; i < beamper.Count; i++)
                    {
                        Solid a = SolidFace.GetSolidElement(beamper[i]);
                        List<PlanarFace> p = SolidFace.GetPlanrFacePerpendicularOne(beamper[i], l);
                        planarFaces.AddRange(p);
                    }
                }
                if (columnsLevel.Count != 0)
                {
                    for (int i = 0; i < columnsLevel.Count; i++)
                    {
                        Solid a = SolidFace.GetSolidElement(columnsLevel[i]);
                        List<PlanarFace> p = SolidFace.GetPlanrFacePerpendicularOne(columnsLevel[i], l);
                        if (p.Count == 2)
                        {
                            planarFaces.AddRange(p);
                        }
                    }
                }
                if (walls.Count != 0)
                {
                    for (int i = 0; i < walls.Count; i++)
                    {
                        Solid a = SolidFace.GetSolidElement(walls[i]);
                        List<PlanarFace> p = SolidFace.GetPlanrFacePerpendicularOne(walls[i], l);
                        planarFaces.AddRange(p);
                    }
                }
            }
            if (planarFaces.Count!=0)
            {
                planarFaces = SolidFace.ArrangePlanrFace(planarFaces, l);
            }
            return planarFaces;
        }
        #endregion
        private static bool ConditionPlanarFace(double d0, double d1, double start, double end)
        {
            //if (planarFaces.Count > 2)
            //{
            //    return false;
            //}
            //double d0 = PointModel.DistanceTo2(planarFace, planarFaces[0].Origin, document);
            //double d1 = PointModel.DistanceTo2(planarFace, planarFaces[1].Origin, document);
            
            
            return ((d0>start)&&(d0<end))&&((d1>start)&&(d1<end));
        }
        private static void Getd0d1(List<PlanarFace> planarFaces, PlanarFace planarFace,  Document document,out double d0,out double d1,out PlanarFace planarFace0,out PlanarFace planarFace1)
        {
            if (planarFaces.Count == 2)
            {
                d0 = PointModel.DistanceTo2(planarFace, planarFaces[0].Origin, document);
                d1 = PointModel.DistanceTo2(planarFace, planarFaces[1].Origin, document);
                planarFace0 = planarFaces[0];
                planarFace1 = planarFaces[1];
            }
            else
            {
                d0 = PointModel.DistanceTo2(planarFace, planarFaces[0].Origin, document);
                planarFace0 = planarFaces[0];
                planarFace1 = planarFaces[1];
                double d10 = 0;
                for (int i = 0; i < planarFaces.Count; i++)
                {
                    if (!PointModel.AreEqual(PointModel.DistanceTo2(planarFace, planarFaces[i].Origin, document),d0))
                    {
                        d10 = PointModel.DistanceTo2(planarFace, planarFaces[i].Origin, document);
                        planarFace1 = planarFaces[i];
                    }
                }
                d1 = d10;
            }
        }
        #region Draft
        public static List<InfoModel> GetInfoModelFull(List<Element> beams, Document doc, Line line, string level)
        {
            XYZ l = line.Direction;
            List<InfoModel> infoModels = new List<InfoModel>();
            List<Level> levels = new FilteredElementCollector(doc).OfClass(typeof(Level)).Cast<Level>().ToList();
            levels.OrderBy(x => x.get_Parameter(BuiltInParameter.LEVEL_ELEV).AsDouble());
            Level level01 = levels[0];
            ElementId level0 = beams[0].get_Parameter(BuiltInParameter.INSTANCE_REFERENCE_LEVEL_PARAM).AsElementId();
            int span = 1;
            #region get All List Element
            List<Element> foundation = BeamsBoundBox.GetFoundationBoudingBoxBeams(beams, doc);
            List<Element> floor = BeamsBoundBox.GetFloorBoudingBoxBeams(beams, doc);
            List<Element> walls = BeamsBoundBox.GetWallBoudingBoxBeams(beams, doc);
            List<Element> wallsLevel = BeamsBoundBox.GetWallBoudingBoxSameTopLevel(walls, doc, level0);
            List<Element> columns = BeamsBoundBox.GetColumnsBoudingBoxBeams(beams, doc);
            List<Element> columnsLevel = BeamsBoundBox.GetColumnsSameTopLevelBeams(columns, level);
            List<Element> beamPerBeams = BeamsBoundBox.GetBeamsPerNodeBeams(beams, doc);
            List<Element> beamPerColumns = BeamsBoundBox.GetBeamsBoudingBoxSameTopLevelColumns(columnsLevel, doc, line, level);
            List<Element> beamPerFoundation = BeamsBoundBox.GetBeamsBoudingBoxSameTopLevelFoundation(foundation, doc, line, level);
            List<Element> beamPerFloor = BeamsBoundBox.GetBeamsBoudingBoxSameTopLevelFloor(floor, doc, line, level);
            List<Element> beamPerWall = BeamsBoundBox.GetBeamsBoudingBoxSameTopLevelWalls(wallsLevel, doc, line, level);
           
            if (beams[0].get_Parameter(BuiltInParameter.STRUCTURAL_REFERENCE_LEVEL_ELEVATION).AsDouble() == level01.get_Parameter(BuiltInParameter.LEVEL_ELEV).AsDouble())
            {
                beamPerBeams.RemoveAll(x => beamPerColumns.Any(y => y.Id == x.Id));
                beamPerBeams.RemoveAll(x => beamPerWall.Any(y => y.Id == x.Id));
                beamPerBeams.RemoveAll(x => beamPerFoundation.Any(y => y.Id == x.Id));
                beamPerBeams.RemoveAll(x => beamPerFloor.Any(y => y.Id == x.Id));
            }
            else
            {
                beamPerBeams.RemoveAll(x => beamPerColumns.Any(y => y.Id == x.Id));
                beamPerBeams.RemoveAll(x => beamPerWall.Any(y => y.Id == x.Id));
            }
            #endregion
            List<PlanarFace> planarBeam = SolidFace.GetAllPlanrFacePerpendicular(beams, l);
            planarBeam = SolidFace.ArrangePlanrFace(planarBeam, l);
            List<PlanarFace> planarFaceNode = GetPlanarFaceNode(beams, doc, line, level, foundation, floor, beamPerBeams, columnsLevel, wallsLevel);
            List<PlanarFace> planars = SolidFace.AddRangePlanarFace(planarBeam, planarFaceNode, l);
            double d = PointModel.DistanceTo2(planars[0], planarFaceNode[0].Origin, doc);
            double d1 = PointModel.DistanceTo2(planars[planars.Count - 1], planarFaceNode[planarFaceNode.Count - 1].Origin, doc);
            
            if (beams.Count==1)
            {
                #region get All List Element
                List<Element> foundation1 = BeamsBoundBox.GetFoundationBoudingBoxOneBeam(beams[0], doc);
                List<Element> floor1 = BeamsBoundBox.GetFloorBoudingBoxOneBeam(beams[0], doc);
                List<Element> walls1 = BeamsBoundBox.GetWallBoudingBoxOneBeam(beams[0], doc);
                List<Element> wallsLevel1 = BeamsBoundBox.GetWallBoudingBoxSameTopLevel(walls1, doc, level0);
               
                List<Element> columns1 = BeamsBoundBox.GetColumnsBoudingBoxOneBeam(beams[0], doc);
                List<Element> columnsLevel1 = BeamsBoundBox.GetColumnsSameTopLevelBeams(columns1, level);
                List<Element> beamPerBeams1 = BeamsBoundBox.GetBeamsPerNodeOneBeam(beams[0], doc);
                List<Element> beamPerColumns1 = BeamsBoundBox.GetBeamsBoudingBoxSameTopLevelColumns(columnsLevel1, doc, line, level);
                List<Element> beamPerFoundation1 = BeamsBoundBox.GetBeamsBoudingBoxSameTopLevelFoundation(foundation1, doc, line, level);
                List<Element> beamPerFloor1 = BeamsBoundBox.GetBeamsBoudingBoxSameTopLevelFloor(floor1, doc, line, level);
                List<Element> beamPerWall1 = BeamsBoundBox.GetBeamsBoudingBoxSameTopLevelWalls(wallsLevel1, doc, line, level);
                if (beams[0].get_Parameter(BuiltInParameter.STRUCTURAL_REFERENCE_LEVEL_ELEVATION).AsDouble() == level01.get_Parameter(BuiltInParameter.LEVEL_ELEV).AsDouble())
                {
                    beamPerBeams1.RemoveAll(x => beamPerColumns1.Any(y => y.Id == x.Id));
                    beamPerBeams1.RemoveAll(x => beamPerWall1.Any(y => y.Id == x.Id));
                    beamPerBeams1.RemoveAll(x => beamPerFoundation1.Any(y => y.Id == x.Id));
                    beamPerBeams1.RemoveAll(x => beamPerFloor1.Any(y => y.Id == x.Id));
                }
                else
                {
                    beamPerBeams1.RemoveAll(x => beamPerColumns1.Any(y => y.Id == x.Id));
                    beamPerBeams1.RemoveAll(x => beamPerWall1.Any(y => y.Id == x.Id));
                }
                List<PlanarFace> planarFaceNode1 = GetPlanarFaceNode(beams, doc, line, level, foundation1, floor1, beamPerBeams1, columnsLevel1, wallsLevel1);
                #endregion
                if (planarFaceNode1.Count <= 2)
                {
                    if (d > 0)
                    {
                        if (d1 > 0)
                        {
                            double start = PointModel.DistanceTo2(planars[0], planars[0].Origin, doc);
                            double end = PointModel.DistanceTo2(planars[0], planarFaceNode1[0].Origin, doc);
                            if (!AreEquals(start,end))
                            {
                                infoModels.Add(new InfoModel(beams[0], doc, span, start, end, true, false, planars[0], planarFaceNode1[0]));
                                span++;
                            }
                            double start1 = PointModel.DistanceTo2(planars[0], planarFaceNode1[planarFaceNode1.Count - 1].Origin, doc);
                            double end1 = PointModel.DistanceTo2(planars[0], planars[planars.Count - 1].Origin, doc);
                            if (!AreEquals(start1, end1))
                            {
                                infoModels.Add(new InfoModel(beams[0], doc, span, start1, end1, false, true, planarFaceNode1[planarFaceNode1.Count - 1], planars[planars.Count - 1]));
                                span++;
                            }
                        }
                        else
                        {
                            double start = PointModel.DistanceTo2(planars[0], planars[0].Origin, doc);
                            double end = PointModel.DistanceTo2(planars[0], planarFaceNode1[0].Origin, doc);
                            if (!AreEquals(start, end))
                            {
                                infoModels.Add(new InfoModel(beams[0], doc, span, start, end, true, false, planars[0], planarFaceNode1[0]));
                                span++;
                            }
                        }
                    }
                    else
                    {
                        double start1 = PointModel.DistanceTo2(planars[0], planarFaceNode1[planarFaceNode1.Count - 1].Origin, doc);
                        double end1 = PointModel.DistanceTo2(planars[0], planars[planars.Count - 1].Origin, doc);
                        if (!AreEquals(start1, end1))
                        {
                            infoModels.Add(new InfoModel(beams[0], doc, span, start1, end1, false, true, planarFaceNode1[planarFaceNode1.Count - 1], planars[planars.Count - 1]));
                            span++;
                        }
                    }

                }
                else
                {
                    if (d > 0)
                    {
                        if (d1 > 0)
                        {
                            double start = PointModel.DistanceTo2(planars[0], planars[0].Origin, doc);
                            double end = PointModel.DistanceTo2(planars[0], planarFaceNode1[0].Origin, doc);
                            if (!AreEquals(start, end))
                            {
                                infoModels.Add(new InfoModel(beams[0], doc, span, start, end, true, false, planars[0], planarFaceNode1[0]));
                                span++;
                            }
                            for (int j = 0; j < planarFaceNode1.Count - 2; j += 2)
                            {
                                double start1 = PointModel.DistanceTo2(planars[0], planarFaceNode1[j + 1].Origin, doc);
                                double end1 = PointModel.DistanceTo2(planars[0], planarFaceNode1[j + 2].Origin, doc);
                                if (!AreEquals(start1, end1))
                                {
                                    infoModels.Add(new InfoModel(beams[0], doc, span, start1, end1, false, false, planarFaceNode1[j + 1], planarFaceNode1[j + 2]));
                                    span++;
                                }
                            }
                            double start2 = PointModel.DistanceTo2(planars[0], planarFaceNode1[planarFaceNode1.Count - 1].Origin, doc);
                            double end2 = PointModel.DistanceTo2(planars[0], planars[planars.Count - 1].Origin, doc);
                            if (!AreEquals(start2, end2))
                            {
                                infoModels.Add(new InfoModel(beams[0], doc, span, start2, end2, false, true, planarFaceNode1[planarFaceNode1.Count - 1], planars[planars.Count - 1]));
                                span++;
                            }
                        }
                        else
                        {
                            double start = PointModel.DistanceTo2(planars[0], planars[0].Origin, doc);
                            double end = PointModel.DistanceTo2(planars[0], planarFaceNode1[0].Origin, doc);
                            if (!AreEquals(start, end))
                            {
                                infoModels.Add(new InfoModel(beams[0], doc, span, start, end, true, false, planars[0], planarFaceNode1[0]));
                                span++;
                            }
                            for (int j = 0; j < planarFaceNode1.Count - 2; j += 2)
                            {
                                double start1 = PointModel.DistanceTo2(planars[0], planarFaceNode1[j + 1].Origin, doc);
                                double end1 = PointModel.DistanceTo2(planars[0], planarFaceNode1[j + 2].Origin, doc);
                                if (!AreEquals(start1, end1))
                                {
                                    infoModels.Add(new InfoModel(beams[0], doc, span, start1, end1, false, false, planarFaceNode1[j + 1], planarFaceNode1[j + 2]));
                                    span++;
                                }
                            }
                        }

                    }
                    else
                    {
                        if (d1 > 0)
                        {
                            for (int j = 0; j < planarFaceNode1.Count - 2; j += 2)
                            {
                                double start1 = PointModel.DistanceTo2(planars[0], planarFaceNode1[j + 1].Origin, doc);
                                double end1 = PointModel.DistanceTo2(planars[0], planarFaceNode1[j + 2].Origin, doc);
                                if (!AreEquals(start1, end1))
                                {
                                    infoModels.Add(new InfoModel(beams[0], doc, span, start1, end1, false, false, planarFaceNode1[j + 1], planarFaceNode1[j + 2]));
                                    span++;
                                }
                            }
                            double start2 = PointModel.DistanceTo2(planars[0], planarFaceNode1[planarFaceNode1.Count - 1].Origin, doc);
                            double end2 = PointModel.DistanceTo2(planars[0], planars[planars.Count - 1].Origin, doc);
                            if (!AreEquals(start2, end2))
                            {
                                infoModels.Add(new InfoModel(beams[0], doc, span, start2, end2, false, true, planarFaceNode1[planarFaceNode1.Count - 1], planars[planars.Count - 1]));
                                span++;
                            }
                        }
                        else
                        {
                            for (int j = 0; j < planarFaceNode1.Count - 2; j += 2)
                            {
                                double start1 = PointModel.DistanceTo2(planars[0], planarFaceNode1[j + 1].Origin, doc);
                                double end1 = PointModel.DistanceTo2(planars[0], planarFaceNode1[j + 2].Origin, doc);
                                if (!AreEquals(start1, end1))
                                {
                                    infoModels.Add(new InfoModel(beams[0], doc, span, start1, end1, false, false, planarFaceNode1[j + 1], planarFaceNode1[j + 2]));
                                    span++;
                                }
                            }
                        }

                    }
                }
            }
            else
            {
                for (int i = 0; i < beams.Count; i++)
                {
                    #region get All List Element
                    List<Element> foundation1 = BeamsBoundBox.GetFoundationBoudingBoxOneBeam(beams[i], doc);
                    List<Element> floor1 = BeamsBoundBox.GetFloorBoudingBoxOneBeam(beams[i], doc);
                    List<Element> walls1 = BeamsBoundBox.GetWallBoudingBoxOneBeam(beams[i], doc);
                    List<Element> wallsLevel1 = BeamsBoundBox.GetWallBoudingBoxSameTopLevel(walls1, doc, level0);
                    List<Element> columns1 = BeamsBoundBox.GetColumnsBoudingBoxOneBeam(beams[i], doc);
                    List<Element> columnsLevel1 = BeamsBoundBox.GetColumnsSameTopLevelBeams(columns1, level);
                    List<Element> beamPerBeams1 = BeamsBoundBox.GetBeamsPerNodeOneBeam(beams[i], doc);
                    List<Element> beamPerColumns1 = BeamsBoundBox.GetBeamsBoudingBoxSameTopLevelColumns(columnsLevel1, doc, line, level);
                    List<Element> beamPerFoundation1 = BeamsBoundBox.GetBeamsBoudingBoxSameTopLevelFoundation(foundation1, doc, line, level);
                    List<Element> beamPerFloor1 = BeamsBoundBox.GetBeamsBoudingBoxSameTopLevelFloor(floor1, doc, line, level);
                    List<Element> beamPerWall1 = BeamsBoundBox.GetBeamsBoudingBoxSameTopLevelWalls(wallsLevel1, doc, line, level);
                    if (beams[0].get_Parameter(BuiltInParameter.STRUCTURAL_REFERENCE_LEVEL_ELEVATION).AsDouble() == level01.get_Parameter(BuiltInParameter.LEVEL_ELEV).AsDouble())
                    {
                        beamPerBeams1.RemoveAll(x => beamPerColumns1.Any(y => y.Id == x.Id));
                        beamPerBeams1.RemoveAll(x => beamPerWall1.Any(y => y.Id == x.Id));
                        beamPerBeams1.RemoveAll(x => beamPerFoundation1.Any(y => y.Id == x.Id));
                        beamPerBeams1.RemoveAll(x => beamPerFloor1.Any(y => y.Id == x.Id));
                    }
                    else
                    {
                        beamPerBeams1.RemoveAll(x => beamPerColumns1.Any(y => y.Id == x.Id));
                        beamPerBeams1.RemoveAll(x => beamPerWall1.Any(y => y.Id == x.Id));
                    }
                    List<PlanarFace> planarFaceNode1 = GetPlanarFaceNode(beams, doc, line, level, foundation1, floor1, beamPerBeams1, columnsLevel1, wallsLevel1);
                    #endregion
                    if (i == 0)
                    {
                        if (planarFaceNode1.Count <= 2)
                        {
                            double start = PointModel.DistanceTo2(planars[0], planars[0].Origin, doc);
                            double end = PointModel.DistanceTo2(planars[0], planarFaceNode1[0].Origin, doc);
                            if (!AreEquals(start, end))
                            {
                                infoModels.Add(new InfoModel(beams[i], doc, span, start, end, true, false, planars[0], planarFaceNode1[0]));
                                span++;
                            }
                        }
                        else
                        {
                            if (d > 0)
                            {
                                double start = PointModel.DistanceTo2(planars[0], planars[0].Origin, doc);
                                double end = PointModel.DistanceTo2(planars[0], planarFaceNode1[0].Origin, doc);
                                if (!AreEquals(start, end))
                                {
                                    infoModels.Add(new InfoModel(beams[i], doc, span, start, end, true, false, planars[0], planarFaceNode1[0]));
                                    span++;
                                }
                                for (int j = 0; j < planarFaceNode1.Count - 2; j += 2)
                                {
                                    double start1 = PointModel.DistanceTo2(planars[0], planarFaceNode1[j + 1].Origin, doc);
                                    double end1 = PointModel.DistanceTo2(planars[0], planarFaceNode1[j + 2].Origin, doc);
                                    if (!AreEquals(start1, end1))
                                    {
                                        infoModels.Add(new InfoModel(beams[i], doc, span, start1, end1, false, false, planarFaceNode1[j + 1], planarFaceNode1[j + 2]));
                                        span++;
                                    }
                                }
                            }
                            else
                            {
                                for (int j = 0; j < planarFaceNode1.Count - 2; j += 2)
                                {
                                    double start1 = PointModel.DistanceTo2(planars[0], planarFaceNode1[j + 1].Origin, doc);
                                    double end1 = PointModel.DistanceTo2(planars[0], planarFaceNode1[j + 2].Origin, doc);
                                    if (!AreEquals(start1, end1))
                                    {
                                        infoModels.Add(new InfoModel(beams[i], doc, span, start1, end1, false, false, planarFaceNode1[j + 1], planarFaceNode1[j + 2]));
                                        span++;
                                    }
                                }
                            }
                        }
                    }
                    else
                    {
                        if (i == beams.Count - 1)
                        {
                            double d0 = PointModel.DistanceTo2(planars[planars.Count - 1], planarFaceNode1[planarFaceNode1.Count - 1].Origin, doc);
                            if (planarFaceNode1.Count <= 2)
                            {
                                double start = PointModel.DistanceTo2(planars[0], planarFaceNode1[planarFaceNode1.Count - 1].Origin, doc);
                                double end = PointModel.DistanceTo2(planars[0], planars[planars.Count - 1].Origin, doc);
                                if (!AreEquals(start, end))
                                {
                                    infoModels.Add(new InfoModel(beams[i], doc, span, start, end, false, true, planarFaceNode1[planarFaceNode1.Count - 1], planars[planars.Count - 1]));
                                    span++;
                                }
                            }
                            else
                            {
                                if (d0 > 0)
                                {
                                    for (int j = 0; j < planarFaceNode1.Count - 2; j += 2)
                                    {
                                        double start1 = PointModel.DistanceTo2(planars[0], planarFaceNode1[j + 1].Origin, doc);
                                        double end1 = PointModel.DistanceTo2(planars[0], planarFaceNode1[j + 2].Origin, doc);
                                        if (!AreEquals(start1, end1))
                                        {
                                            infoModels.Add(new InfoModel(beams[i], doc, span, start1, end1, false, false, planarFaceNode1[j + 1], planarFaceNode1[j + 2]));
                                            span++;
                                        }

                                    }
                                    double start = PointModel.DistanceTo2(planars[0], planarFaceNode1[planarFaceNode1.Count - 1].Origin, doc);
                                    double end = PointModel.DistanceTo2(planars[0], planars[planars.Count - 1].Origin, doc);
                                    if (!AreEquals(start, end))
                                    {
                                        infoModels.Add(new InfoModel(beams[i], doc, span, start, end, false, true, planarFaceNode1[planarFaceNode1.Count - 1], planars[planars.Count - 1]));
                                        span++;
                                    }
                                }
                                else
                                {
                                    for (int j = 0; j < planarFaceNode1.Count - 2; j += 2)
                                    {
                                        double start1 = PointModel.DistanceTo2(planars[0], planarFaceNode1[j + 1].Origin, doc);
                                        double end1 = PointModel.DistanceTo2(planars[0], planarFaceNode1[j + 2].Origin, doc);
                                        if (!AreEquals(start1, end1))
                                        {
                                            infoModels.Add(new InfoModel(beams[i], doc, span, start1, end1, false, false, planarFaceNode1[j + 1], planarFaceNode1[j + 2]));
                                            span++;
                                        }
                                    }
                                }
                            }
                        }
                        else
                        {
                            for (int j = 0; j < planarFaceNode1.Count - 2; j += 2)
                            {
                                double start1 = PointModel.DistanceTo2(planars[0], planarFaceNode1[j + 1].Origin, doc);
                                double end1 = PointModel.DistanceTo2(planars[0], planarFaceNode1[j + 2].Origin, doc);
                                if (!AreEquals(start1, end1))
                                {
                                    infoModels.Add(new InfoModel(beams[i], doc, span, start1, end1, false, false, planarFaceNode1[j + 1], planarFaceNode1[j + 2]));
                                    span++;
                                }
                            }
                        }
                    }
                }
            }
            if (infoModels.Count != 0)
            {
                //PlanarFace p0 = GetPlanrFaceFirst(infoModels, beams, doc, l, level);
                PlanarFace p0 = planars[0];
                for (int i = 0; i < infoModels.Count; i++)
                {
                    infoModels[i].TopBottomPlanar = new List<PlanarFace>();
                    infoModels[i].LeftRightPlanar = new List<PlanarFace>();
                    Solid a = SolidFace.GetSolidElement(infoModels[i].Element);
                    List<PlanarFace> top = SolidFace.GetPlanrFaceTopBottom(a);
                    List<PlanarFace> left = SolidFace.GetPlanrFaceLeftRight(a, line);
                    if (top.Count != 0)
                    {
                        for (int j = 0; j < top.Count; j++)
                        {
                            infoModels[i].TopBottomPlanar.Add(top[j]);
                        }
                    }
                    if (left.Count != 0)
                    {
                        for (int k = 0; k < left.Count; k++)
                        {
                            if (PointModel.AreEqual(left[k].FaceNormal.DotProduct(l),0))
                            {
                                infoModels[i].LeftRightPlanar.Add(left[k]);
                            }
                            
                        }
                    }
                    infoModels[i].TopBottomPlanar.OrderBy(x => x.Origin.Z);
                    infoModels[i].LeftRightPlanar = SolidFace.ArrangePlanrFaceNormal(infoModels[i].LeftRightPlanar, infoModels[i].LeftRightPlanar[0].FaceNormal);
                }
                
            }
            return infoModels;
        }
        public static List<NodeModel> GetNodeModelFull(List<InfoModel> infoModels, List<Element> beams, Document doc, Line line, string level)
        {
            Level level01 = new FilteredElementCollector(doc).OfClass(typeof(Level)).Cast<Level>().FirstOrDefault();
            List<NodeModel> nodeModels = new List<NodeModel>();
            double left = 0;
            double right = 0;
            XYZ l = line.Direction;
            ElementId level0 = beams[0].get_Parameter(BuiltInParameter.INSTANCE_REFERENCE_LEVEL_PARAM).AsElementId();
            #region get All List Element
            List<Element> foundation = BeamsBoundBox.GetFoundationBoudingBoxBeams(beams, doc);
            List<Element> floor = BeamsBoundBox.GetFloorBoudingBoxBeams(beams, doc);
            List<Element> walls = BeamsBoundBox.GetWallBoudingBoxBeams(beams, doc);
            List<Element> wallsLevel = BeamsBoundBox.GetWallBoudingBoxSameTopLevel(walls, doc, level0);
            List<Element> columns = BeamsBoundBox.GetColumnsBoudingBoxBeams(beams, doc);
            List<Element> columnsLevel = BeamsBoundBox.GetColumnsSameTopLevelBeams(columns, level);
            List<Element> beamPerBeams = BeamsBoundBox.GetBeamsPerNodeBeams(beams, doc);
            List<Element> beamPerColumns = BeamsBoundBox.GetBeamsBoudingBoxSameTopLevelColumns(columnsLevel, doc, line, level);
            List<Element> beamPerFoundation = BeamsBoundBox.GetBeamsBoudingBoxSameTopLevelFoundation(foundation, doc, line, level);
            List<Element> beamPerFloor = BeamsBoundBox.GetBeamsBoudingBoxSameTopLevelFloor(floor, doc, line, level);
            List<Element> beamPerWall = BeamsBoundBox.GetBeamsBoudingBoxSameTopLevelWalls(wallsLevel, doc, line, level);
            if (beams[0].get_Parameter(BuiltInParameter.STRUCTURAL_REFERENCE_LEVEL_ELEVATION).AsDouble() == level01.get_Parameter(BuiltInParameter.LEVEL_ELEV).AsDouble())
            {
                beamPerBeams.RemoveAll(x => beamPerColumns.Any(y => y.Id == x.Id));
                beamPerBeams.RemoveAll(x => beamPerWall.Any(y => y.Id == x.Id));
                beamPerBeams.RemoveAll(x => beamPerFoundation.Any(y => y.Id == x.Id));
                beamPerBeams.RemoveAll(x => beamPerFloor.Any(y => y.Id == x.Id));
            }
            else
            {
                beamPerBeams.RemoveAll(x => beamPerColumns.Any(y => y.Id == x.Id));
                beamPerBeams.RemoveAll(x => beamPerWall.Any(y => y.Id == x.Id));
            }
            #endregion
            List<PlanarFace> planarBeam = SolidFace.GetAllPlanrFacePerpendicular(beams, l);
            planarBeam = SolidFace.ArrangePlanrFace(planarBeam, l);
            List<PlanarFace> planarFaceNode = GetPlanarFaceNode(beams, doc, line, level, foundation, floor, beamPerBeams, columnsLevel, wallsLevel);
            List<PlanarFace> planars = SolidFace.AddRangePlanarFace(planarBeam, planarFaceNode, l);
            left = PointModel.DistanceTo2(planars[0], planarFaceNode[0].Origin, doc);
            right = PointModel.DistanceTo2(planars[0], planarFaceNode[planarFaceNode.Count - 1].Origin, doc);
            if (infoModels[0].ConsolLeft)
            {
                if (infoModels[infoModels.Count - 1].ConsolRight)
                {
                    for (int i = 0; i < infoModels.Count - 1; i++)
                    {
                        nodeModels.Add(new NodeModel(i + 1, infoModels[i].endPosition, infoModels[i + 1].startPosition, infoModels[i].h, infoModels[i + 1].h, infoModels[i].zOffset, infoModels[i + 1].zOffset, infoModels[i].EndPlanar, infoModels[i + 1].StartPlanar));
                    }
                }
                else
                {
                    for (int i = 0; i < infoModels.Count; i++)
                    {
                        if (i == infoModels.Count - 1)
                        {
                            nodeModels.Add(new NodeModel(i + 1, infoModels[i].endPosition, right, infoModels[i].h, infoModels[i].h, infoModels[i].zOffset, infoModels[i].zOffset, infoModels[i].EndPlanar, planars[planars.Count - 1]));
                        }
                        else
                        {
                            nodeModels.Add(new NodeModel(i + 1, infoModels[i].endPosition, infoModels[i + 1].startPosition, infoModels[i].h, infoModels[i + 1].h, infoModels[i].zOffset, infoModels[i + 1].zOffset, infoModels[i].EndPlanar, infoModels[i + 1].StartPlanar));
                        }

                    }
                }
            }
            else
            {
                if (infoModels[infoModels.Count - 1].ConsolRight)
                {
                    for (int i = 0; i < infoModels.Count; i++)
                    {
                        if (i == 0)
                        {
                            nodeModels.Add(new NodeModel(i + 1, left, infoModels[i].startPosition, infoModels[i].h, infoModels[i].h, infoModels[i].zOffset, infoModels[i].zOffset, planars[0], infoModels[i].StartPlanar));
                        }
                        else
                        {
                            nodeModels.Add(new NodeModel(i + 1, infoModels[i - 1].endPosition, infoModels[i].startPosition, infoModels[i - 1].h, infoModels[i].h, infoModels[i - 1].zOffset, infoModels[i].zOffset, infoModels[i - 1].EndPlanar, infoModels[i].StartPlanar));
                        }

                    }
                }
                else
                {
                    for (int i = 0; i < infoModels.Count + 1; i++)
                    {
                        if (i == 0)
                        {
                            nodeModels.Add(new NodeModel(i + 1, left, infoModels[i].startPosition, infoModels[i].h, infoModels[i].h, infoModels[i].zOffset, infoModels[i].zOffset, planars[0], infoModels[i].StartPlanar));
                        }
                        else
                        {
                            if (i == infoModels.Count)
                            {
                                nodeModels.Add(new NodeModel(i + 1, infoModels[i - 1].endPosition, right, infoModels[i - 1].h, infoModels[i - 1].h, infoModels[i - 1].zOffset, infoModels[i - 1].zOffset, infoModels[i - 1].EndPlanar, planars[planars.Count - 1]));
                            }
                            else
                            {
                                nodeModels.Add(new NodeModel(i + 1, infoModels[i - 1].endPosition, infoModels[i].startPosition, infoModels[i - 1].h, infoModels[i].h, infoModels[i - 1].zOffset, infoModels[i].zOffset, infoModels[i - 1].EndPlanar, infoModels[i].StartPlanar));
                            }
                        }

                    }
                }
            }
            return nodeModels;
        }
        #endregion
        #region
        private static bool AreEquals(double a, double b, double t=1e-10)
        {
            return Math.Abs(a - b) < t;
        }
        #endregion
    }
}
