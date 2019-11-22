using GarageDI.Entities;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace GarageDI.Utils
{
   public static class Extensions
    {
        public static Tuple<Vehicle,PropertyInfo[]> GetProps(this VehicleType vehicleType)
        {
            var type = Type.GetType($"GarageDI.Entities.{vehicleType.ToString()}");
            var vehicle = (Vehicle)Activator.CreateInstance(type);
            var properties = vehicle.GetType().GetProperties();

            return new Tuple<Vehicle, PropertyInfo[]>(vehicle, properties);
        }
    }
}
  