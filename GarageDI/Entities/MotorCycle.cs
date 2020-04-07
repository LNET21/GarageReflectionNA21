using GarageDI.Attributes;

namespace GarageDI.Entities
{
    class MotorCycle :Vehicle
    {
        [Beautify("Cylinder volyme")]
        [Include(3)]
        public int CylinderVolyme { get; set; }
    }
}
