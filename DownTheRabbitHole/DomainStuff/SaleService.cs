using DownTheRabbitHole.Commands;

namespace DownTheRabbitHole.DomainStuff
{
    public class SaleService : IHandle<AddProductToBasket>
    {
        private readonly IProductCatalogue _catalogue;
        private readonly IRepository _repo;

        public void Handle(AddProductToBasket c)
        {
            var sale = _repo.Load<SaleAggregate>(c.SaleId);
            sale.AddProductToBasket(c.ProductId, _catalogue);
        }
    }
}
