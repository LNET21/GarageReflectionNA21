using System;
using System.Collections.Generic;
using System.Text;

namespace VehicleCollection
{
    public interface IGarage<T>: IEnumerable<T>
    {
         string Name { get; }
         int Count { get; }
         int Capacity { get; }
         bool Park(T vehicle);
         bool Leave(T vehicle);

    }
}
