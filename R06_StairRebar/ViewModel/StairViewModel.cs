#region Namespaces
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Windows.Forms;
using System.Windows.Input;
using Autodesk.Revit.ApplicationServices;
using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.DB.Structure;
using Autodesk.Revit.UI;
using WpfCustomControls.ViewModel;
#endregion

namespace R06_StairRebar
{
    public class StairViewModel : BaseViewModel
    {
        public UIDocument UiDoc;
        public Document Doc;

        #region ICommand
        public ICommand SelectionMenuCommand { get; set; }
        #endregion
    
        public StairViewModel(UIDocument uiDoc, Document doc)
        {
            UiDoc = uiDoc;
            Doc = doc;
        }

    }
}
