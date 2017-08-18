using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DownTheRabbitHole.DomainStuff
{
    class Basket
    {
        private readonly Dictionary<int, BasketLine> Lines = new Dictionary<int, BasketLine>();
        private readonly int LineCount = 0;
    }

    class BasketLine
    {
        public readonly Guid ProductId;
        public readonly int Quantity;
        public readonly decimal LinePrice;
    }
}
