using System;
using DownTheRabbitHole.Events;

namespace DownTheRabbitHole.DomainStuff
{
    class SaleAggregate : AggregateRoot
    {
        public SaleAggregate() : base()
        {
            RegisterEventAppliers(
                EventHandle.For<BasketLineAdded>(Apply),
                EventHandle.For<BasketLineQuantityChanged>(Apply)
                );
        }

        public void AddProductToBasket(
            Guid productId, IProductCatalogue catalogue)
        {
            decimal itemPrice = catalogue.GetItemPrice(productId);
            Emit(new BasketLineAdded(Id, 1, productId, itemPrice, quantity: 1));
        }

        private void Apply(BasketLineAdded ev)
        {

        }

        private void Apply(BasketLineQuantityChanged ev)
        {

        }
    }
}
