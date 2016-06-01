using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace AliceApi.Common
{
    public static class EnumExtensions
    {
        public static string GetDescription(this Enum value)
        {
            FieldInfo field = value.GetType().GetField(value.ToString());
            object[] attribs = field.GetCustomAttributes(typeof(DescriptionAttribute), true);
            if (attribs.Length > 0)
            {
                return ((DescriptionAttribute)attribs[0]).Description;
            }
            return string.Empty;
        }

        //MyEnum val = MyEnum.Black;
        //Console.WriteLine(val.GetDescription()); //writes "the descriptive text"


        //public static string GetLocalizedDescription(this Enum value)
        //{
        //    FieldInfo field = value.GetType().GetField(value.ToString());
        //    object[] attribs = field.GetCustomAttributes(typeof(DescriptionAttribute), true));
        //    if (attribs.Length > 0)
        //    {
        //        string message = ((DescriptionAttribute)attribs[0]).Description;
        //        return resourceMgr.GetString(message, CultureInfo.CurrentCulture);
        //    }
        //    return string.Empty;
        //}

    //    myDDL.DataSource = Enum.GetValues(typeof(MyEnum)).OfType<MyEnum>().Select(
    //val => new { Description = val.GetDescription(), Value = val.ToString() });


    }
}
