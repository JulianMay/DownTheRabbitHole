namespace DownTheRabbitHole.Commands
open System

type AddProductToBasket = {   
    SaleId: string; 
    ProductId: Guid;
}