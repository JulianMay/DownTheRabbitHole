using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DownTheRabbitHole.DomainStuff
{
    interface IProductCatalogue
    {
        decimal GetItemPrice(Guid productId);
    }
}
