using DownTheRabbitHole.Events;
using Moq;
using NUnit.Framework;
using System;

namespace DownTheRabbitHole.DomainStuff.Tests
{
    [TestFixture]
    public class BasketTests
    {
        [Test]
        public void AddingProductToBasket_GetsPricesFromProductCatalogue()
        {
            var itemId = Guid.NewGuid(); 
            var mock = new Mock<IProductCatalogue>();
            mock.Setup(x => x.GetItemPrice(itemId)).Returns(89.95m);
            var sale = new SaleAggregate();

            sale.AddProductToBasket(itemId, catalogue: mock.Object);

            mock.VerifyAll();
            AssertEventsEmitted(sale,
                new BasketLineAdded(sale.Id, lineNumber: 1, 
                productId: itemId, linePrice: 89.95m, quantity: 1));
        }

        [Test]
        public void AddingTheSameProductSeveralTimes_UpdatesTheRespectiveBasketLine()
        {
            var itemId = Guid.NewGuid();
            var mock = new Mock<IProductCatalogue>();
            mock.Setup(x => x.GetItemPrice(itemId)).Returns(89.95m);            
            var sale = new SaleAggregate();
            sale.ApplyEvents(new[]{
                new BasketLineAdded(sale.Id, 1, itemId, linePrice: 89.95m, quantity: 1)
            });

            sale.AddProductToBasket(itemId, mock.Object);

            AssertEventsEmitted(sale,
                new BasketLineQuantityChanged(sale.Id, lineNumber: 1,
                linePrice: 179.90m, quantity: 2)
            );
        }


        private void AssertEventsEmitted(AggregateRoot agg, params object[] eventsEmitted)
        {
            CollectionAssert.AreEquivalent(agg.GetEmittedEvents(), eventsEmitted);
        }
    }
}
