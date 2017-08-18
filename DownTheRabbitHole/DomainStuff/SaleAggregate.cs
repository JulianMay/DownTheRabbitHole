using System;
using DownTheRabbitHole.Events;

namespace DownTheRabbitHole.DomainStuff
{
    class SaleAggregate : AggregateRoot
    {
        private Basket _basket;

        public SaleAggregate(string id) : base(id)
        {
            _basket = new Basket(id);
            RegisterEventAppliers(
                EventHandle.For<BasketLineAdded>(_basket.Apply),
                EventHandle.For<BasketLineQuantityChanged>(_basket.Apply)
                );
        }

        public void AddProductToBasket(
            Guid productId, IProductCatalogue catalogue)
        {
            decimal itemPrice = catalogue.GetItemPrice(productId);
            var outcome = _basket.AddProduct(productId, itemPrice);
            Emit(outcome);
        }
    }
}
