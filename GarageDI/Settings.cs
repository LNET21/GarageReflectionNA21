using ConsoleUI;
using System;
using System.Collections.Generic;
using System.Text;
using VehicleCollection;

namespace GarageDI
{
    class Settings : ISettings
    {
        public int Size { get; set; }
        public string Name { get; set; }

    }
}

