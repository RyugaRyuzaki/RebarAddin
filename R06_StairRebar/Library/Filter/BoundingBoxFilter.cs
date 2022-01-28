using Autodesk.Revit.DB;
using System.Collections.Generic;
using System.Linq;
namespace R06_StairRebar.Library.Filter
{
    public static class BoundingBoxFilter
    {
        public static List<Element> GetStructuralColumsBoudingBoxFilter(List<Reference> references, Document document)
        {

            ElementCategoryFilter categoryFilter
                         = new ElementCategoryFilter(BuiltInCategory.OST_StructuralColumns);
            List<Element> allColumns = new List<Element>();
            foreach (Reference item in references)
            {
                Element element = document.GetElement(item);
                BoundingBoxXYZ box = element.get_BoundingBox(document.ActiveView);
                Outline outline = new Outline(box.Min, box.Max);
                BoundingBoxIntersectsFilter bbFilter
                     = new BoundingBoxIntersectsFilter(outline);
                LogicalAndFilter logicalAndFilter
                = new LogicalAndFilter(bbFilter, categoryFilter);
                List<Element> columns
                                = new FilteredElementCollector(document,
                                        document.ActiveView.Id)
                                    .WherePasses(logicalAndFilter)
                                    .ToList();
                foreach (Element e in columns)
                {
                    allColumns.Add(e);
                }

            }
            allColumns = allColumns.Distinct(new DistinctCompare()).ToList();

            return allColumns;
        }
        public static List<Element> GetStructuralFramingBoudingBoxFilter(List<Reference> references, Document document)
        {

            ElementCategoryFilter categoryFilter
                         = new ElementCategoryFilter(BuiltInCategory.OST_StructuralFraming);
            List<Element> allBeams = new List<Element>();
            foreach (Reference item in references)
            {
                Element element = document.GetElement(item);
                BoundingBoxXYZ box = element.get_BoundingBox(document.ActiveView);
                Outline outline = new Outline(box.Min, box.Max);
                BoundingBoxIntersectsFilter bbFilter
                     = new BoundingBoxIntersectsFilter(outline);
                LogicalAndFilter logicalAndFilter
                = new LogicalAndFilter(bbFilter, categoryFilter);
                List<Element> columns
                                = new FilteredElementCollector(document,
                                        document.ActiveView.Id)
                                    .WherePasses(logicalAndFilter)
                                    .ToList();
                foreach (Element e in columns)
                {
                    allBeams.Add(e);
                }

            }
            allBeams = allBeams.Distinct(new DistinctCompare()).ToList();

            return allBeams;
        }
    }
}
