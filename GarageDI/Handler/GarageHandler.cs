using GarageDI.DTO;
using GarageDI.Entities;
using GarageDI.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using VehicleCollection;

namespace GarageDI.Handler
{
    class GarageHandler : IGarageHandler
    {
        private readonly IGarage<Vehicle> garage;

        public GarageHandler(IGarage<Vehicle> garage)
        {
            this.garage = garage;
        }

        public bool IsFull => garage.Count >= garage.Capacity;

        public List<VehicleCountDTO> GetByType()
        {
            return garage.GroupBy(v => v.GetType().Name)
                     .Select(v => new VehicleCountDTO
                   // .Select(v => new Tuple<string, int>(v.Key, v.Count())
                    {
                        TypeName = v.Key,
                        Count = v.Count()
                    })
                    .ToList();
        }

        public Vehicle GetVehicle(Tuple<Vehicle, PropertyInfo[]> vehicleProp)
        {
            var vehicle = vehicleProp.Item1;

            foreach (var prop in vehicleProp.Item2)
            {
                switch (prop.PropertyType.Name)
                {
                    case "Int32":
                        var tempInt = Util.AskForInt(prop.Name);
                        prop.SetValue(vehicle, tempInt);
                        break;
                    case "String":
                        vehicle[prop.Name] = Util.AskForString(prop.Name);

                        break;
                    default:
                        break;
                }

            }
            return vehicle;
        }

        public bool Park(Vehicle v)
        {
           return garage.Park(v);
        }
    }
}
