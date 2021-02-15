using GarageDI.DTO;
using GarageDI.Entities;
using GarageDI.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using VehicleCollection;

namespace GarageDI.Handler
{
    class GarageHandler : IGarageHandler
    {
        private readonly IGarage<IVehicle> garage;
        private readonly IUtil util;

        public GarageHandler(IGarage<IVehicle> garage, IUtil util)
        {
            this.garage = garage;
            this.util = util;
        }

        public bool IsGarageFull => garage.Count >= garage.Capacity;

        public List<IVehicle> GetAll()
        {
            return garage.ToList();
        }

        public List<VehicleCountDTO> GetByType()
        {
            return garage.GroupBy(v => v.GetType().Name)
                     .Select(v => new VehicleCountDTO
                    {
                        TypeName = v.Key,
                        Count = v.Count()
                    })
                    .ToList();
        }

        public IVehicle GetVehicle((IVehicle, PropertyInfo[]) vehicleProp)
        {
            var vehicle = vehicleProp.Item1;

            foreach (var prop in vehicleProp.Item2)
            {
                var typeCode = Type.GetTypeCode(prop.PropertyType);

                switch (typeCode)
                {
                    case TypeCode.Int32:
                        prop.SetValue(vehicle, util.AskForInt(prop.GetDisplayTest()));
                        break;
                    case TypeCode.String:
                            var r = util.AskForString(prop.GetDisplayTest());
                            vehicle[prop.Name] = r;
                        break;
                    default:
                        break;
                }

            }
            return vehicle;
        } 
        
        public IEnumerable<IVehicle> SearchVehicle((IVehicle, PropertyInfo[]) vehicleProp)
        {
            var result = vehicleProp.Item1 is null ? garage.ToList() : 
            garage.Where(v => v.GetType() == vehicleProp.Item1.GetType());

            foreach (var prop in vehicleProp.Item2)
            {
                var searchWord = util.AskForString(prop.GetDisplayTest()).ToUpper();
                result = result.Where(v => v[prop.Name].ToString() == 
                                     (searchWord == "X" ? v[prop.Name].ToString() : searchWord))
                                      .ToList();
            }

            return result.ToList();
        }


        public bool Park(IVehicle v)
        {
           return garage.Park(v);
        }

        public IVehicle Get(string regNo)
        {
            return garage.FirstOrDefault(v => v.RegNo == regNo);
        }

        public bool Leave(string regNo)
        {
            var match = Get(regNo);
            return match is null ? false : garage.Leave(match);
        }

        //public IEnumerable<IVehicle> GetVehicles(Tuple<IVehicle, PropertyInfo[]> vehicleProp)
        //{
        //    return new List<IVehicle>();
        //}

    }
}
