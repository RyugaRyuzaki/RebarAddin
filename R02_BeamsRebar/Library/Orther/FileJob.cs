using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autodesk.Revit.DB;
namespace R02_BeamsRebar
{
    public class FileJob
    {
        #region Basic
        public static void SaveInterger(int value, Stream stream)
        {
            var b= BitConverter.GetBytes(value);
            stream.Write(b, 0, 4);
        }
        public static int RestoreInterger( Stream stream)
        {
            var b = new byte[4];
            stream.Read(b, 0, 4);
            return BitConverter.ToInt32(b, 0);
        }
        public static void SaveDouble(double value, Stream stream)
        {
            var b = BitConverter.GetBytes(value);
            stream.Write(b, 0, 8);
        }
        public static double RestoreDouble( Stream stream)
        {
            var b = new byte[8];
            stream.Read(b, 0, 8);
            return BitConverter.ToDouble(b, 0);
        }
        public static void SaveString(string value, Stream stream)
        {
            var b = Encoding.UTF8.GetBytes(value);
            var b_Length = BitConverter.GetBytes(b.Length);
            stream.Write(b_Length, 0, 4);
            stream.Write(b, 0, b.Length);
        }
        public static string RestoreString( Stream stream)
        {
            var b_length = new byte[4];
            stream.Read(b_length, 0, 4);
            int length = BitConverter.ToInt32(b_length, 0);
            var b_String = new byte[length];
            stream.Read(b_String, 0, length);
            return Encoding.UTF8.GetString(b_String, 0, length);
        }
        #endregion
        #region XYZ
        private static void SaveXYZ(XYZ value, Stream stream)
        {
            SaveDouble(value.X, stream);
            SaveDouble(value.Y, stream);
            SaveDouble(value.Z, stream);
        }
        private static XYZ RestoreXYZ(Stream stream)
        {
            return new XYZ(RestoreDouble(stream), RestoreDouble(stream), RestoreDouble(stream));
        }
        #endregion
        #region Line
        private static void SaveLine(Line value, Stream stream)
        {
            SaveXYZ(value.GetEndPoint(0),stream);
            SaveXYZ(value.GetEndPoint(1),stream);
        }
        private static Line RestoreLine(Stream stream)
        {
            return Line.CreateBound(RestoreXYZ(stream), RestoreXYZ(stream));
        }
        private static bool CompareLine(Line line,Stream stream)
        {
            Line readLine = RestoreLine(stream);
            bool a = line.GetEndPoint(0).Equals(readLine.GetEndPoint(0));
            bool b = line.GetEndPoint(1).Equals(readLine.GetEndPoint(1));
            return a&&b;
        }
        #endregion
        #region Element
        public static void SaveElement(ElementId elementId, Stream stream)
        {
            int a = elementId.IntegerValue;
            SaveInterger(a, stream);
        }
        public static ElementId RestoreElementId( Stream stream)
        {
            int id = RestoreInterger(stream);
            return new ElementId(id);
        }
        #endregion
        #region Document
        public static void SaveDocument(Document document, Stream stream)
        {
            SaveString(document.PathName, stream);
        }
        #endregion
    }
}
