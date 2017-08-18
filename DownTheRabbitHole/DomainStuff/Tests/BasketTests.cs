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
            var productId = Guid.NewGuid(); 
            var productCatalogueMock = new Mock<IProductCatalogue>();
            productCatalogueMock.Setup(x => x.GetItemPrice(productId)).Returns(89.95m);
            var sale = new SaleAggregate();

            sale.AddProductToBasket(productId, quantity: 2);

            productCatalogueMock.VerifyAll();
            CollectionAssert.Contains(sale.GetUnpersistedEvents(), 
                new BasketLineAdded(sale.Id, productId, linePrice: 179.90m, quantity: 2));
        }
    }
}
