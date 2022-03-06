#region Namespaces
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Windows.Forms;
using Autodesk.Revit.ApplicationServices;
using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.DB.Structure;
using Autodesk.Revit.UI;
using Autodesk.Revit.UI.Selection;
using WpfCustomControls;
using WpfCustomControls.CustomControls;
using WpfCustomControls.ViewModel;
using System.Windows.Input;
#endregion

namespace R04_SlabsRebar
{
    public class SlabViewModel : BaseViewModel
    {
        public UIDocument UiDoc;
        public Document Doc;
        #region ICommand
        public ICommand SelectionMenuCommand { get; set; }
        #endregion
       

     
        public SlabViewModel(UIDocument uiDoc, Document doc)
        {
            UiDoc = uiDoc;
            Doc = doc;
          
           
        }

    }
}
