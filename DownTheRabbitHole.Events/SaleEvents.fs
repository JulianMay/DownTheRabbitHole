namespace DownTheRabbitHole.Events
open System

type BasketLineAdded = {   
    SaleId: string; 
    LineNumber: int;
    ProductId: Guid;
    LinePrice: decimal;
    Quantity: int;
}

type BasketLineQuantityChanged = {   
    SaleId: string; 
    LineNumber: int;
    LinePrice: decimal;
    Quantity: int;
}
