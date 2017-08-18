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
            var mock = new Mock<IProductCatalogue>();
            mock.Setup(x => x.GetItemPrice(productId)).Returns(89.95m);
            var sale = new SaleAggregate();

            sale.AddProductToBasket(productId, catalogue: mock.Object);

            mock.VerifyAll();
            CollectionAssert.Contains(sale.GetUnpersistedEvents(), 
                new BasketLineAdded(sale.Id, productId, linePrice: 179.90m, quantity: 1));
        }
    }
}
