namespace DownTheRabbitHole.Events
open System

type BasketLineAdded = {   
    SaleId: string; 
    ProductId: Guid;
    LinePrice: decimal;
    Quantity: int;
}
