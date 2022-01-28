using Autodesk.Revit.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace R03_FoundationRebar
{
    public class ErrorFoundation
    {
        public static int Error(Element element, Document document)
        {
            int a = 0;
            return a;
        }
        public static List<string> Errorstring = new List<string>()
        {
            "OK",
            "ERROR1",
            "ERROR1",
            "ERROR1",
            "ERROR1",
            "ERROR1",
            "ERROR1",
            "ERROR1"
        };
    }
}
