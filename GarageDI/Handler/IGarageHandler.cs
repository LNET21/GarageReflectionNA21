using GarageDI.DTO;
using GarageDI.Entities;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace GarageDI.Handler
{
    public interface IGarageHandler
    {
        bool IsFull { get; }

        bool Park(IVehicle v);
        IVehicle GetVehicle(Tuple<IVehicle, PropertyInfo[]> vehicleProp);
        IVehicle Get(string regNo);
        List<VehicleCountDTO> GetByType();
        List<IVehicle> GetAll();
        bool Leave(string regNo);
        IEnumerable<IVehicle> GetVehicles(Tuple<IVehicle, PropertyInfo[]> vehicleProp);
        IEnumerable<IVehicle> SearchVehicle(Tuple<IVehicle, PropertyInfo[]> vehicleProp);
    }
}
