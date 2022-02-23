using Autodesk.Revit.DB;
using System;
using System.IO;
using System.Reflection;
using Application = Autodesk.Revit.ApplicationServices.Application;
namespace DSP
{
    
    public class AddSharedParams 
    {
        public static string XVector = "XVector";
        public static string YVector = "YVector";
        public static void ShareParameterPile(Document document, Application app, BuiltInCategory categoryPile)
        {
            using (Transaction tran = new Transaction(document, "Add shared param"))
            {
                tran.Start();
                bool paramsAdded = AddSharedPileParameter(app,document, XVector, ParameterType.Length, categoryPile, true);
                paramsAdded &= AddSharedPileParameter( app,  document, YVector, ParameterType.Length, categoryPile, true);
                tran.Commit();
            }
        }
       private static bool AddSharedPileParameter(Application app, Document document, string paramName, ParameterType paramType, BuiltInCategory categoryPile, bool userModifiable)
        {
            try
            {
                if (ShareParameterExists(document, paramName))
                {
                    return true;
                }

                // create shared parameter file
                string modulePath = Path.GetDirectoryName(document.PathName);
                string paramFile = modulePath + "\\DSPParameters.txt";
                if (File.Exists(paramFile))
                {
                    File.Delete(paramFile);
                }
                FileStream fs = File.Create(paramFile);
                fs.Close();
                app.SharedParametersFilename  = paramFile;
                DefinitionFile parafile = app.OpenSharedParameterFile();
                DefinitionGroup apiGroup = parafile.Groups.Create("PileParamGroup");
                ExternalDefinitionCreationOptions ExtDefinitionCreationOptions = new ExternalDefinitionCreationOptions(paramName, paramType);
                ExtDefinitionCreationOptions.HideWhenNoValue = true;
                ExtDefinitionCreationOptions.UserModifiable = userModifiable;
                Definition pileSharedParamDef = apiGroup.Definitions.Create(ExtDefinitionCreationOptions);
                Category rebarCat = document.Settings.Categories.get_Item(categoryPile);
                CategorySet categories = app.Create.NewCategorySet();
                categories.Insert(rebarCat);
                InstanceBinding binding = app.Create.NewInstanceBinding(categories);
                document.ParameterBindings.Insert(pileSharedParamDef, binding);
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to create shared parameter: " + ex.Message);
            }
        }
        
        private static bool ShareParameterExists(Document doc, string paramName)
        {
            BindingMap bindingMap = doc.ParameterBindings;
            DefinitionBindingMapIterator iter = bindingMap.ForwardIterator();
            iter.Reset();

            while (iter.MoveNext())
            {
                Definition tempDefinition = iter.Key;
                if (string.Compare(tempDefinition.Name, paramName) != 0)
                {
                    continue;
                }
                ElementBinding binding = bindingMap.get_Item(tempDefinition) as ElementBinding;
                CategorySet bindCategories = binding.Categories;
                foreach (Category category in bindCategories)
                {
                    if (category.Name
                        == doc.Settings.Categories.get_Item(BuiltInCategory.OST_Rebar).Name)
                    {
                        return true;
                    }
                }
            }
            return false;
        }
    }
}