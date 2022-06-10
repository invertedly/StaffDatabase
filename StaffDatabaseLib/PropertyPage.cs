using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace StaffDatabaseLib
{
    public class PropertyPage
    {
        internal class PropPageProperty
        {
            public PropertyInfo PropInfo { get; set; }
            public PropAttribute Attr { get; set; }
        }

        public static void ShowProperties(object obj)
        {
            List<PropPageProperty> props = GetPropToShow(obj);

            foreach (PropPageProperty prop in props)
            {
                ShowProperty(prop, obj);
            }
        }

        private static void ShowProperty(PropPageProperty prop, object obj)
        {
            PropertyInfo propInfo = prop.PropInfo;
            object val = propInfo.GetValue(obj, null);

            Console.WriteLine("{0}: {1}", prop.Attr.Name, val);
        }

        private static List<PropPageProperty> GetPropToShow(object obj)
        {
            var propList = new List<PropPageProperty>();

            PropertyInfo[] allProps = obj.GetType().GetProperties();

            foreach (PropertyInfo prop in allProps)
            {
                object[] attrs = prop.GetCustomAttributes(typeof(PropAttribute), false);

                if (attrs.Count() == 0)
                {
                    continue;
                }

                var propToShow = new PropPageProperty();
                propToShow.PropInfo = prop;
                propToShow.Attr = attrs[0] as PropAttribute;

                propList.Add(propToShow);
            }

            propList.Sort(new PropComparer());

            return propList;
        }

        internal class PropComparer : IComparer<PropPageProperty>
        {
            public int Compare(PropPageProperty x, PropPageProperty y)
            {
                return x.Attr.Priority.CompareTo(y.Attr.Priority);
            }
        }

    }
}
