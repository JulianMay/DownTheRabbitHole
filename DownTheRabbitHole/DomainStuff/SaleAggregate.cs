﻿using System;
using DownTheRabbitHole.Events;

namespace DownTheRabbitHole.DomainStuff
{
    class SaleAggregate : AggregateRoot
    {
        public void AddProductToBasket(
            Guid productId, IProductCatalogue catalogue)
        {
            decimal itemPrice = catalogue.GetItemPrice(productId);
            Emit(new BasketLineAdded(Id, 1, productId, itemPrice, quantity: 1));
        }

        
    }
}
