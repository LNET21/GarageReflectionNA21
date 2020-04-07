using GarageDI.Attributes;
using GarageDI.Utils;
using System;
using System.Text;

namespace GarageDI.Entities
{
    public class Vehicle : IVehicle
    {
        private string regNo;
        public static Predicate<string> Check;
        public static Action Callback;

        [Beautify("Registration number")]
        [Include(1)]
        public string RegNo
        {
            get => regNo;
            set
            {
                
                var result = (bool)Check?.Invoke(value.ToUpper());
                if (result)
                    regNo = value.ToUpper();
                else
                    Callback?.Invoke();
                return;
            }
        }

        private string color;

        [Include(2)]
        public string Color
        {
            get { return color; }
            set { color = value.ToUpper(); }
        }

        public virtual object this[string name]
        {
            get
            {
                return this.GetType().GetProperty(name).GetValue(this);
            }
            set
            {
                this.GetType().GetProperty(name).SetValue(this, value);
            }
        }

        //ToDo bör bara göras en gång
        public virtual string Print()
        {
            var builder = new StringBuilder().Append($"[{this.GetType().Name}]\t");

            Array.ForEach(this.GetIncludeProps(),
                           p => builder.Append($" {p.GetDisplayTest()}:{p.GetValue(this, null)?.ToString()}"));

            return builder.ToString();
        }
    }
}
