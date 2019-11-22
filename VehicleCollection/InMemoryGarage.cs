using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace VehicleCollection
{
   public class InMemoryGarage<T> : IGarage<T>, IEnumerable<T> 
    {
        public string Name { get; }

        private T[] vehicles;
        public int Capacity => vehicles.Length;

        public int Count { get; private set; } = 0;

        public InMemoryGarage(IConfiguration configuration, ISettings sett)
        {
            //var settings = configuration.GetSection("Garage:Settings");
            //var size = int.Parse(settings["Size"]);
            //vehicles = new T[size];
            Name = sett.Name;
            vehicles = new T[sett.Size];
        }

        public IEnumerator<T> GetEnumerator()
        {
            foreach (var v in vehicles)
            {
                if (v != null) yield return v;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public bool Park(T vehicle)
        {
            bool result = false;

            for (int i = 0; i < vehicles.Length; i++)
            {
                if(vehicles[i] == null)
                {
                    vehicles[i] = vehicle;
                    result = true;
                    Count++;
                    break;
                }
            }
            return result;
        }
    }
}
