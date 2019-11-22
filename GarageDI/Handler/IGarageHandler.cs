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

        bool Park(Vehicle v);
        Vehicle GetVehicle(Tuple<Vehicle, PropertyInfo[]> vehicleProp);
        List<VehicleCountDTO> GetByType();
    }
}
