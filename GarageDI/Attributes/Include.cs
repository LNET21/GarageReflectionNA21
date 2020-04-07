using System;

namespace GarageDI.Attributes
{
    class Include : Attribute
    {
        public int Order { get; }
        public Include(int order)
        {
            Order = order;
        }

    }
}
