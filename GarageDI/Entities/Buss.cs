using GarageDI.Attributes;

namespace GarageDI.Entities
{
    class Buss : Vehicle
    {
        [Include(4)]
        public int Seats { get; set; }
    }
}
