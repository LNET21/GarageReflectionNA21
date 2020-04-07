using GarageDI.Attributes;
using GarageDI.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace GarageDI.Utils
{
   public static class Extensions
    {
        public static Tuple<IVehicle,PropertyInfo[]> GetPropsForType(this VehicleType vehicleType)
        {
            var type = Type.GetType($"GarageDI.Entities.{vehicleType}");
            if (type is null) return null;
            var vehicle = (IVehicle)Activator.CreateInstance(type);
            var properties = vehicle.GetIncludeProps();

            return new Tuple<IVehicle, PropertyInfo[]>(vehicle, properties);
        } 

        public static PropertyInfo[] GetIncludeProps<T>(this T type)
        {
            return  type.GetType()
                            .GetProperties()
                            .Where(p => p.GetCustomAttribute(typeof(Include)) != null)
                            .OrderBy(o => ((Include)o.GetCustomAttribute(typeof(Include))).Order)
                            .ToArray();
        }

        public static string GetDisplayTest(this PropertyInfo prop)
        {
            var attr = prop.GetCustomAttribute<Beautify>();
            return attr is null ? prop.Name : attr.Text;
        }

        //Not Used!
        //Just testing
        public static Dictionary<PropertyInfo, string> GetProps2(this VehicleType vehicleType)
        {
            var dict = new Dictionary<PropertyInfo, string>();

            var type = Type.GetType($"GarageDI.Entities.{vehicleType}");
            if (type is null) return null;
            var vehicle = (Vehicle)Activator.CreateInstance(type);

            var properties = vehicle.GetType().GetProperties();

            foreach (var prop in properties)
            {
                var attrs = prop.GetCustomAttributes(true);
                foreach (var attr in attrs)
                {
                    if(attr is Beautify beauty)
                    {
                        dict.Add(prop, beauty.Text);
                    }
                }
            }
            return dict;
        }
    }
}
  
