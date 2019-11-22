using System;
using System.Collections.Generic;
using System.Text;

namespace GarageDI.Entities
{
   public class Vehicle
    {
        public string RegNo { get; set; }

        public virtual object this[string name]
        {
            set
            {
                this.GetType().GetProperty(name).SetValue(this, value);
            }
        }
    }
}
