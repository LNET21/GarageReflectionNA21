using GarageDI.Entities;
using System;
using System.Collections;
using System.Collections.Generic;

namespace VehicleCollection
{
   public class InMemoryGarage<T> : IGarage<T> where T : IVehicle
    {
        private readonly T[] vehicles;

        public string Name { get; }

        public int Capacity => vehicles.Length;

        public int Count { get; private set; } = 0;

        public InMemoryGarage(ISettings sett)
        {
            if (sett is null) throw new ArgumentNullException(nameof(ISettings));

            Name = sett.Name;
            vehicles = new T[Math.Max(0,sett.Size)]; 
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

        public bool Leave(T vehicle)
        {
            bool result = false;
            if (vehicle is null) return result;

            var index = Array.IndexOf(vehicles, vehicle);

            if(index >= 0)
            {
                vehicles[index] = default;
                Count--;
                result = true;
            }
        
            return result;
        }
    }
}
