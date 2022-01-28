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
#endregion

namespace R00_Startup
{
    public class LicenseViewModel : BaseViewModel
    {
        private LicenseModel _LicenseModel;
        public LicenseModel LicenseModel { get => _LicenseModel; set { _LicenseModel = value; OnPropertyChanged(); } }
        public LicenseViewModel()
        {
            LicenseModel = new LicenseModel();
        }

    }
}
