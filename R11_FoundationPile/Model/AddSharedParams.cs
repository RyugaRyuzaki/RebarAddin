using Autodesk.Revit.DB;
using System;
using System.IO;
using System.Reflection;
using Application = Autodesk.Revit.ApplicationServices.Application;
namespace R11_FoundationPile
{
    
    public class AddSharedParams 
    {
        public static string ParameterName = "Updated";
        public static string CurveIdName = "CurveElementId";
        public static void ShareParameter(Document document, Application app)
        {
            using (Transaction tran = new Transaction(document, "Add shared param"))
            {
                tran.Start();
                bool paramsAdded = AddSharedTestParameter(app,document, ParameterName, ParameterType.YesNo, false);
                paramsAdded &= AddSharedTestParameter( app,  document, CurveIdName, ParameterType.Integer, true);
               
            }
        }
       private static bool AddSharedTestParameter(Application app, Document document, string paramName, ParameterType paramType, bool userModifiable)
        {
            try
            {
                
                // check whether shared parameter exists
                if (ShareParameterExists(document, paramName))
                {
                    return true;
                }

                // create shared parameter file
                string modulePath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
                string paramFile = modulePath + "\\RebarTestParameters.txt";
                if (File.Exists(paramFile))
                {
                    File.Delete(paramFile);
                }
                FileStream fs = File.Create(paramFile);
                fs.Close();

                // cache application handle
                

                // prepare shared parameter file
                app.SharedParametersFilename  = paramFile;

                // open shared parameter file
                DefinitionFile parafile = app.OpenSharedParameterFile();

                // create a group
                DefinitionGroup apiGroup = parafile.Groups.Create("RebarTestParamGroup");

                // create a visible param 
                ExternalDefinitionCreationOptions ExtDefinitionCreationOptions = new ExternalDefinitionCreationOptions(paramName, paramType);
                ExtDefinitionCreationOptions.HideWhenNoValue = true;//used this to show the parameter only in some rebar instances that will use it
                ExtDefinitionCreationOptions.UserModifiable = userModifiable;//  set if users need to modify this
                Definition rebarSharedParamDef = apiGroup.Definitions.Create(ExtDefinitionCreationOptions);

                // get rebar category
                Category rebarCat = document.Settings.Categories.get_Item(BuiltInCategory.OST_Rebar);
                CategorySet categories = app.Create.NewCategorySet();
                categories.Insert(rebarCat);

                // insert the new parameter
                InstanceBinding binding = app.Create.NewInstanceBinding(categories);
                document.ParameterBindings.Insert(rebarSharedParamDef, binding);
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

                // find the definition of which the name is the appointed one
                if (string.Compare(tempDefinition.Name, paramName) != 0)
                {
                    continue;
                }

                // get the category which is bound
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