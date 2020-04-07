using System.Collections;
using System.Collections.Generic;

namespace VehicleCollection
{
   public class InMemoryGarage<T> : IGarage<T>, IEnumerable<T> 
    {
        public string Name { get; }

        private readonly T[] vehicles;
        public int Capacity => vehicles.Length;

        public int Count { get; private set; } = 0;

        public InMemoryGarage(ISettings sett)
        {
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

        public bool Leave(T vehicle)
        {
            bool result = false;

            for (int i = 0; i < vehicles.Length; i++)
            {
                if(vehicles[i].Equals(vehicle))
                {
                    vehicles[i] = default;
                    result = true;
                    Count--;
                    return result;
                }
            }
            return result;
        }
    }
}
