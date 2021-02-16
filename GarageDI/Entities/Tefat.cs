using GarageDI.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace GarageDI.Entities
{
    class Tefat : Vehicle
    {
        [Include(6)]
        public int Range { get; set; }
    }
}
