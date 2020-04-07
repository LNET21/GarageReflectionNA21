using GarageDI.Attributes;

namespace GarageDI.Entities
{
    class Boat : Vehicle
    {
        [Include(5)]
        public int Length { get; set; }
    }
}
