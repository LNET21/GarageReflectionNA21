using ConsoleUI;
using GarageDI.Entities;
using GarageDI.Handler;
using GarageDI.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace GarageDI
{
    internal class GarageManager
    {
        private readonly IUI ui;
        private readonly IGarageHandler handler;
        private readonly IVehicle vehicle;
        private Dictionary<int, Action> menyOptions;
        private string parkMenyOptions;

        public GarageManager(IUI ui, IGarageHandler handler, IVehicle vehicle)
        {
            this.ui = ui;
            this.handler = handler;
            this.vehicle = vehicle;
            Vehicle.Check = RegNoExists;
            Vehicle.Callback = Run;
        }

        internal void Run()
        {
            menyOptions = GetMenyOptions();

            do
            {
                Console.ReadKey();
                ui.Clear();
                ui.Print("Welcome");
                ui.ShowMeny();

                var input = Util.AskForKey("");
                if (menyOptions.ContainsKey(input))
                    menyOptions[input]?.Invoke();

            } while (true);
        }

        private void Seed()
        {
            handler.Park(new Buss() { RegNo = "A", Seats = 1, Color = "RED" });
            handler.Park(new Buss() { RegNo = "AA", Seats = 2, Color = "RED" });
            handler.Park(new Buss() { RegNo = "AAA", Seats = 3, Color = "BLUE" });
            handler.Park(new Buss() { RegNo = "B", Seats = 1, Color = "BLUE" });
            handler.Park(new Buss() { RegNo = "BB", Seats = 2, Color = "RED" });
            handler.Park(new Buss() { RegNo = "BBB", Seats = 3, Color = "BLUE" });
            handler.Park(new Car() { RegNo = "BBBB", Color = "BLUE" });
            handler.Park(new Car() { RegNo = "BBBBB", Color = "RED" });
        }

        private Dictionary<int, Action> GetMenyOptions()
        {
            return new Dictionary<int, Action>
            {
                {1, Park },
                {2, ListParked },
                {3, ListByType },
                {4, UnPark},
                {5, Search },
                {6, Seed },
                {0, Quit }
            };
        }

        private void Search()
        {
            ui.Clear();
            ui.Meny(false, GetParkMenyOptions(), "Search meny!" +
                                               "\nSkip search criteria with x" +
                                               "\n0, For all vehicles");
            ui.Print("");
            const int all = 0;
            var search = ChooseVehicle(search: true);

            if (search.Equals(all))
            {
                var vehicleType = vehicle.GetIncludeProps();
                var vehicles = handler.SearchVehicle(new Tuple<IVehicle, PropertyInfo[]>(null, vehicleType));
                PrintAll(vehicles);
            }
            else if (search != all)
            {
                var vehicleType = (VehicleType)search;
                var vehicleProp = vehicleType.GetPropsForType();
                var vehicles = handler.SearchVehicle(vehicleProp);
                PrintAll(vehicles);
            }
        }

        private void PrintAll(IEnumerable<IVehicle> vehicles)
        {
            if (vehicles.Count().Equals(0)) ui.Print("No result");
            else
            {
                foreach (var v in vehicles)
                {
                    ui.Print(v.Print());
                }
            }
        }

        private void UnPark()
        {
            var regNo = Util.AskForString("Enter reg number").ToUpper();
            var message = handler.Leave(regNo) ? "Vehicle unparked" : "Can´t find vehicle";
            ui.Print(message);
        }

        private void ListParked()
        {
            PrintAll(handler.GetAll());
        }

        private void ListByType()
        {
            ui.Print("List by type");
            handler.GetByType().ForEach(r => ui.Print($"Type: {r.TypeName} Count: {r.Count}"));
        }

        private void Quit()
        {
            Environment.Exit(0);
        }

        private void Park()
        {
            ui.Clear();
            ui.Meny(handler.IsFull, GetParkMenyOptions(), "Park meny");
            if (handler.IsFull) return;

            VehicleType vehicleType = (VehicleType)ChooseVehicle(search: false);
            var vehicleProp = vehicleType.GetPropsForType();
            var vehicle = handler.GetVehicle(vehicleProp);

            ui.Print(handler.Park(vehicle) ?
                $"[{vehicleType}] with registration number:{vehicle.RegNo} parked" : $"Something failed");
        }

        private static int ChooseVehicle(bool search)
        {
            int input;
            bool cont;
            do
            {
                input = Util.AskForKey("");

                cont = search ?
                    input <= Enum.GetValues(typeof(VehicleType)).Length && input >= 0 :
                    input <= Enum.GetValues(typeof(VehicleType)).Length && input > 0;
            }
            while (cont is false);

            return input;
        }

        private bool RegNoExists(string regNo)
        {
            var exists = handler.Get(regNo);
            if (exists != null)
            {
                ui.Print($"Reg number:{regNo} is already in the garage!");
                return false;
            }
            return true;
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
                    builder.AppendLine($"{(int)value}, {value}");
                }
                return parkMenyOptions = builder.ToString();
            }
        }
    }
}