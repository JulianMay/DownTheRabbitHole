using DownTheRabbitHole.Events;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DownTheRabbitHole.DomainStuff
{
    struct BasketLine
    {
        public readonly int LineNumber;
        public readonly Guid ProductId;
        public readonly int Quantity;
        public readonly decimal LinePrice;
        public BasketLine(int lineNumber, Guid productId, 
            int quantity, decimal linePrice)
        {
            LineNumber = lineNumber;
            ProductId = productId;
            Quantity = quantity;
            LinePrice = linePrice;
        }
    }

    class Basket
    {
        private readonly List<BasketLine> _lines;
        public object AddProduct(Guid productId, decimal price)
        {
            BasketLine existingLine;
            if (!AlreadyContainsProduct(productId, out existingLine))
                return new BasketLineAdded
                    (_saleId, NextLineNumber, productId, price, quantity: 1);

            var newLinePrice = existingLine.LinePrice + price;
            var newQuantity = existingLine.Quantity + 1;
            return new BasketLineQuantityChanged
                (_saleId, existingLine.LineNumber, newLinePrice, newQuantity);
        }

        
        public void Apply(BasketLineAdded e)
        {
            _lines.Add(new BasketLine(e.LineNumber, e.ProductId,
                                        e.Quantity, e.LinePrice));
            _lastLineNuber = e.LineNumber;
        }


        private readonly string _saleId;
        private int _lastLineNuber;
        

        
        public void Apply(BasketLineQuantityChanged e)
        {
            BasketLine oldLine = _lines.Single(l =>
                                    l.LineNumber == e.LineNumber);
            _lines.Remove(oldLine);
            _lines.Add(new BasketLine(e.LineNumber, oldLine.ProductId,
                                            e.Quantity, e.LinePrice));
        }


        private int NextLineNumber { get { return _lastLineNuber + 1; } }
        private bool AlreadyContainsProduct(Guid productId, out BasketLine existingLine)
        {
            return _lines.TryGet(l => l.ProductId.Equals(productId), out existingLine);
        }
        


        public Basket(string saleId)
        {
            _saleId = saleId;
            _lines = new List<BasketLine>();
            _lastLineNuber = 0;
        }

    }

    
}
