using Autodesk.Revit.DB;
using System;
using static R01_ColumnsRebar.ErrorColumns;

namespace R01_ColumnsRebar
{
    public class TagColumn : BaseViewModel
    {
        private double _HeightCell;
        public double HeightCell { get => _HeightCell; set { _HeightCell = value; OnPropertyChanged(); } }
        private double _H1;
        public double H1 { get => _H1; set { _H1 = value; OnPropertyChanged(); } }
        private double _H2;
        public double H2 { get => _H2; set { _H2 = value; OnPropertyChanged(); } }
        private TextNote _TextNote;
        public TextNote TextNote { get => _TextNote; set { _TextNote = value; OnPropertyChanged(); } }
        private DetailCurve _DetailCurve;
        public DetailCurve DetailCurve { get => _DetailCurve; set { _DetailCurve = value; OnPropertyChanged(); } }
        public TagColumn(SettingModel settingModel, Document document)
        {
            double a = (settingModel.SelectedTextNote != null) ? 12 * settingModel.SelectedTextNote.get_Parameter(BuiltInParameter.TEXT_SIZE).AsDouble() : 5;
            HeightCell = double.Parse(UnitFormatUtils.Format(document.GetUnits(), SpecTypeId.Length, a, false));
            double scale = 100 / (double.Parse(settingModel.SelectedSectionTemplate.get_Parameter(BuiltInParameter.VIEW_SCALE).AsValueString()));
            HeightCell *= scale;
            H1 = 5.0 * HeightCell;
            H2 = 5.0 * HeightCell;
        }
        private XYZ GetXYZOrigin(ViewSection viewSection, SectionStyle sectionStyle, UnitProject unit, Document document, InfoModel infoModel, double offset0)
        {
            XYZ p0 = null;
            if (sectionStyle == SectionStyle.RECTANGLE)
            {
                XYZ p1 = PointModel.ProjectToPlane(infoModel.East.Origin, infoModel.Nouth);
                p0 = new XYZ(p1.X, p1.Y, viewSection.Origin.Z);
            }
            else
            {
                p0 = new XYZ(infoModel.PointPosition.X, infoModel.PointPosition.Y, viewSection.Origin.Z);
                p0 += unit.Convert(infoModel.D) * 0.5 * XYZ.BasisY;
            }
            double offset = unit.Convert(offset0);
            return (p0 + ( offset) * XYZ.BasisX);
        }
        private void CreateCellItem(XYZ p0, ViewSection viewSection, Document document, UnitProject unit, SettingModel settingModel, bool left, string leftString, string value)
        {
            XYZ p1 = p0 + unit.Convert(H1) * XYZ.BasisX;
            XYZ p2 = p0 + unit.Convert(H2 + H1) * XYZ.BasisX;
            XYZ p3 = p2 + unit.Convert(HeightCell) * (-1)*XYZ.BasisY;
            XYZ p4 = p1 + unit.Convert(HeightCell) * (-1) * XYZ.BasisY;
            XYZ p5 = p0 + unit.Convert(HeightCell) * (-1) * XYZ.BasisY;
            DetailCurve = document.Create.NewDetailCurve(viewSection, Line.CreateBound(p0, p1));
            DetailCurve = document.Create.NewDetailCurve(viewSection, Line.CreateBound(p1, p2));
            DetailCurve = document.Create.NewDetailCurve(viewSection, Line.CreateBound(p2, p3));
            DetailCurve = document.Create.NewDetailCurve(viewSection, Line.CreateBound(p3, p4));
            DetailCurve = document.Create.NewDetailCurve(viewSection, Line.CreateBound(p4, p1));
            DetailCurve = document.Create.NewDetailCurve(viewSection, Line.CreateBound(p4, p5));
            DetailCurve = document.Create.NewDetailCurve(viewSection, Line.CreateBound(p5, p0));
            DetailCurve = document.Create.NewDetailCurve(viewSection, Line.CreateBound(p0, p1));
            if (left)
            {
                TextNote = TextNote.Create(document, viewSection.Id, p0, leftString, settingModel.SelectedTextNote.Id);
            }
            TextNote = TextNote.Create(document, viewSection.Id, p1, value, settingModel.SelectedTextNote.Id);

        }
        private void CreateTableStirrup(int i0, XYZ p0, ViewSection viewSection, SectionStyle sectionStyle, UnitProject unit, Document document, InfoModel infoModel, StirrupModel stirrupModel, BarMainModel barMainModel, SettingModel settingModel)
        {
            int i = i0;
            if (stirrupModel.TypeDis == 0)
            {

                XYZ p2 = p0 + unit.Convert(HeightCell * i) * (-1) * XYZ.BasisY;
                string stirrup = stirrupModel.BarS.Type + " @ " + stirrupModel.S;
                CreateCellItem(p2, viewSection, document, unit, settingModel, true, "Stirrup", stirrup);
                i++;
                if (stirrupModel.AddH)
                {
                    if (stirrupModel.TypeH == 0)
                    {
                        XYZ p3 = p0 + unit.Convert(HeightCell * i) * (-1) * XYZ.BasisY;
                        string AddH = stirrupModel.BarH.Type + " @ " + stirrupModel.S;
                        CreateCellItem(p3, viewSection, document, unit, settingModel, true, "Add-Horizontal", AddH);
                    }
                    else
                    {
                        XYZ p3 = p0 + unit.Convert(HeightCell * i) * (-1) * XYZ.BasisY;
                        string AddH = stirrupModel.nH +" "+ stirrupModel.BarH.Type +  " @ " + stirrupModel.S;
                        CreateCellItem(p3, viewSection, document, unit, settingModel, true, "Add-Horizontal", AddH);
                    }
                    i++;
                }
                if (stirrupModel.AddV)
                {
                    if (stirrupModel.TypeV == 0)
                    {
                        XYZ p3 = p0 + unit.Convert(HeightCell * i) * (-1) * XYZ.BasisY;
                        string AddV = stirrupModel.BarV.Type + " @ " + stirrupModel.S;
                        CreateCellItem(p3, viewSection, document, unit, settingModel, true, "Add-Vertical", AddV);
                    }
                    else
                    {
                        XYZ p3 = p0 + unit.Convert(HeightCell * i) * (-1) * XYZ.BasisY;
                        string AddV = stirrupModel.nV + " "+stirrupModel.BarV.Type + " @ " + stirrupModel.S;
                        CreateCellItem(p3, viewSection, document, unit, settingModel, true, "Add-Vertical", AddV);
                    }
                    i++;
                }
            }
            else
            {
                XYZ p2 = p0 + unit.Convert(HeightCell * i) * (-1) * XYZ.BasisY;
                string stirrup2 = stirrupModel.BarS.Type + " @ " + stirrupModel.S1;
                CreateCellItem(p2, viewSection, document, unit, settingModel, true, "Stirrup", stirrup2);
                i++;
                XYZ p3 = p0 + unit.Convert(HeightCell * i) * (-1) * XYZ.BasisY;
                string stirrup3 = stirrupModel.BarS.Type + " @ " + stirrupModel.S2;
                CreateCellItem(p3, viewSection, document, unit, settingModel, false, "Stirrup", stirrup3);
                i++;
                XYZ p4 = p0 + unit.Convert(HeightCell * i) * (-1) * XYZ.BasisY;
                string stirrup4 = stirrupModel.BarS.Type + " @ " + stirrupModel.S1;
                CreateCellItem(p4, viewSection, document, unit, settingModel, false, "Stirrup", stirrup4);
                i++;
                if (stirrupModel.AddH)
                {
                    if (stirrupModel.TypeH == 0)
                    {
                        XYZ p5 = p0 + unit.Convert(HeightCell * i) * (-1) * XYZ.BasisY;
                        string AddH5 = stirrupModel.BarH.Type + " @ " + stirrupModel.S1;
                        CreateCellItem(p5, viewSection, document, unit, settingModel, true, "Add-Horizontal", AddH5);
                        i++;
                        XYZ p6 = p0 + unit.Convert(HeightCell * i) * (-1) * XYZ.BasisY;
                        string AddH6 = stirrupModel.BarH.Type + " @ " + stirrupModel.S2;
                        CreateCellItem(p6, viewSection, document, unit, settingModel, false, "Add-Horizontal", AddH6);
                        i++;
                        XYZ p7 = p0 + unit.Convert(HeightCell * i) * (-1) * XYZ.BasisY;
                        string AddH7 = stirrupModel.BarH.Type + " @ " + stirrupModel.S1;
                        CreateCellItem(p7, viewSection, document, unit, settingModel, false, "Add-Horizontal", AddH7);
                        i++;
                    }
                    else
                    {
                        XYZ p5 = p0 + unit.Convert(HeightCell * i) * (-1) * XYZ.BasisY;
                        string AddH5 = stirrupModel.nH +" "+ stirrupModel.BarH.Type +  " @ " + stirrupModel.S1;
                        CreateCellItem(p5, viewSection, document, unit, settingModel, true, "Add-Horizontal", AddH5);
                        i++;
                        XYZ p6 = p0 + unit.Convert(HeightCell * i) * (-1) * XYZ.BasisY;
                        string AddH6 = stirrupModel.nH +" "+ stirrupModel.BarH.Type + " @ " + stirrupModel.S2;
                        CreateCellItem(p6, viewSection, document, unit, settingModel, false, "Add-Horizontal", AddH6);
                        i++;
                        XYZ p7 = p0 + unit.Convert(HeightCell * i) * (-1) * XYZ.BasisY;
                        string AddH7 = stirrupModel.nH +" "+ stirrupModel.BarH.Type +  " @ " + stirrupModel.S1;
                        CreateCellItem(p7, viewSection, document, unit, settingModel, false, "Add-Horizontal", AddH7);
                        i++;
                    }

                }
                if (stirrupModel.AddV)
                {
                    if (stirrupModel.TypeV == 0)
                    {
                        XYZ p5 = p0 + unit.Convert(HeightCell * i) * (-1) * XYZ.BasisY;
                        string AddV5 = stirrupModel.BarV.Type + " @ " + stirrupModel.S1;
                        CreateCellItem(p5, viewSection, document, unit, settingModel, true, "Add-Vertical", AddV5);
                        i++;
                        XYZ p6 = p0 + unit.Convert(HeightCell * i) * (-1) * XYZ.BasisY;
                        string AddV6 = stirrupModel.BarV.Type + " @ " + stirrupModel.S2;
                        CreateCellItem(p6, viewSection, document, unit, settingModel, false, "Add-Vertical", AddV6);
                        i++;
                        XYZ p7 = p0 + unit.Convert(HeightCell * i) * (-1) * XYZ.BasisY;
                        string AddV7 = stirrupModel.BarV.Type + " @ " + stirrupModel.S1;
                        CreateCellItem(p7, viewSection, document, unit, settingModel, false, "Add-Vertical", AddV7);
                        i++;
                    }
                    else
                    {
                        XYZ p5 = p0 + unit.Convert(HeightCell * i) * (-1) * XYZ.BasisY;
                        string AddV5 = stirrupModel.nV +" "+ stirrupModel.BarV.Type +  " @ " + stirrupModel.S1;
                        CreateCellItem(p5, viewSection, document, unit, settingModel, true, "Add-Vertical", AddV5);
                        i++;
                        XYZ p6 = p0 + unit.Convert(HeightCell * i) * (-1) * XYZ.BasisY;
                        string AddV6 = stirrupModel.nV +" "+ stirrupModel.BarV.Type +  " @ " + stirrupModel.S2;
                        CreateCellItem(p6, viewSection, document, unit, settingModel, false, "Add-Vertical", AddV6);
                        i++;
                        XYZ p7 = p0 + unit.Convert(HeightCell * i) * (-1) * XYZ.BasisY;
                        string AddV7 = stirrupModel.nV +" "+ stirrupModel.BarV.Type +  " @ " + stirrupModel.S1;
                        CreateCellItem(p7, viewSection, document, unit, settingModel, false, "Add-Vertical", AddV7);
                        i++;
                    }

                }
            }
        }
        public void CreateTable(ViewSection viewSection, SectionStyle sectionStyle, UnitProject unit, Document document, InfoModel infoModel,StirrupModel stirrupModel,BarMainModel barMainModel, SettingModel settingModel, double offset0)
        {
            XYZ p0 = GetXYZOrigin(viewSection, sectionStyle, unit, document, infoModel, offset0);
            int i = 0;
            if (barMainModel.BarModels.Count != 0)
            {
                XYZ p1 = p0 + unit.Convert(HeightCell * i) * (-1) * XYZ.BasisY;
                string bar = (sectionStyle == SectionStyle.RECTANGLE) ? (2 * barMainModel.nx + 2 * (barMainModel.ny - 2) + "") : (barMainModel.nd + "");
                CreateCellItem(p1, viewSection, document, unit, settingModel, true, "Bar", bar + "-" + barMainModel.BarModels[0].Bar.Type);
                i++;
            }
            CreateTableStirrup(i,p0, viewSection, sectionStyle, unit, document, infoModel, stirrupModel, barMainModel, settingModel);
        }

        
    }
}
