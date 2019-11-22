using ConsoleUI;
using GarageDI.DTO;
using GarageDI.Entities;
using GarageDI.Handler;
using GarageDI.Utils;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using VehicleCollection;

namespace GarageDI
{
    internal class GarageManager
    {
        private readonly IUI ui;
        private readonly IGarageHandler handler;
        private readonly Dictionary<int, Action> menyOptions;
        private string parkMenyOptions;

        public GarageManager(IUI ui, IGarageHandler handler)
        {
            this.ui = ui;
            this.handler = handler;
            menyOptions = GetMenyOptions();
        }
        internal void Run()
        {
            do
            {
                ui.Clear();
                ui.Print("Welcome");
                ui.ShowMeny();

                var input = Util.AskForKey("");
                if (menyOptions.ContainsKey(input))
                    menyOptions[input].Invoke();

            } while (true);
        }

        private Dictionary<int, Action> GetMenyOptions()
        {
            return new Dictionary<int, Action>
            {
                {1, Park },
               // {2, ListParked },
                {3, ListByType },
                //{4,  UnPark},
                //{5, Search }
                {0, Quit }
            };
        }

        private void ListByType()
        {
            ui.Print("List by type");
            handler.GetByType().ForEach(r => ui.Print($"Type: {r.TypeName} Count: {r.Count}"));
            Console.ReadKey();
        }

        private void Quit()
        {
            Environment.Exit(0);
        }

        private void Park()
        {
            ui.Clear();
            ui.ParkMeny(handler.IsFull, GetParkMenyOptions());
            if (handler.IsFull) return;

            var vehicleType = (VehicleType)Util.AskForKey("");
            var vehicleProp = vehicleType.GetProps();
            var vehicle = handler.GetVehicle(vehicleProp);

            ui.Print(handler.Park(vehicle) ? $"{vehicleType} {vehicle.RegNo} parked" : $"Something failed");
            Console.ReadKey();

        }

        private string GetParkMenyOptions()
        {
            if (!string.IsNullOrEmpty(parkMenyOptions))
                return parkMenyOptions;

            else
            {
                var values = Enum.GetValues(typeof(VehicleType));
                StringBuilder builder = new StringBuilder();
                foreach (var value in values)
                {
                    builder.AppendLine($"{(int)value}, {value.ToString()}");
                }
                return parkMenyOptions = builder.ToString();
            }
        } 
    }
}