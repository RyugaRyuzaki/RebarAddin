
using Autodesk.Revit.DB;

namespace R02_BeamsRebar
{
    public class UnitProject
    {
        public UnitProject(int unitInt, string unitName)
        {
            UnitInt = unitInt;
            UnitName = unitName;
        }

        public int UnitInt { get; set; }
        public string UnitName { get; set; }
        public double Convert(double a)
        {
            if (UnitName.Equals("cm"))
            {
                a = UnitUtils.Convert(a, UnitTypeId.Centimeters, UnitTypeId.Feet);
            }
            if (UnitName.Equals("dm"))
            {
                a = UnitUtils.Convert(a, UnitTypeId.Decimeters, UnitTypeId.Feet);
            }
            if (UnitName.Equals("ft"))
            {
                a = UnitUtils.Convert(a, UnitTypeId.Feet, UnitTypeId.Feet);
            }
            if (UnitName.Equals("in"))
            {
                a = UnitUtils.Convert(a, UnitTypeId.Inches, UnitTypeId.Feet);
            }
            if (UnitName.Equals("m"))
            {
                a = UnitUtils.Convert(a, UnitTypeId.Meters, UnitTypeId.Feet);
            }
            if (UnitName.Equals("mm"))
            {
                a = UnitUtils.Convert(a, UnitTypeId.Millimeters, UnitTypeId.Feet);
            }
            if (UnitName.Equals("inUS"))
            {
                a = UnitUtils.Convert(a, UnitTypeId.Inches, UnitTypeId.Feet);
            }
            if (UnitName.Equals("ft-in"))
            {
                a = UnitUtils.Convert(a, UnitTypeId.FeetFractionalInches, UnitTypeId.Feet);
            }
            if (UnitName.Equals("inch"))
            {
                a = UnitUtils.Convert(a, UnitTypeId.FractionalInches, UnitTypeId.Feet);
            }
            if (UnitName.Equals("m"))
            {
                a = UnitUtils.Convert(a, UnitTypeId.Meters, UnitTypeId.Feet);
            }
            return a;
        }
    }
}
