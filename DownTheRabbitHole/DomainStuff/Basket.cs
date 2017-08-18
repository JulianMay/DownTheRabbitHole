using DownTheRabbitHole.Events;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DownTheRabbitHole.DomainStuff
{
    class Basket
    {
        private readonly string _saleId;
        private readonly List<BasketLine> _lines;
        private int _lineCount;
        public Basket(string saleId)
        {
            _saleId = saleId;
            _lines = new List<BasketLine>();
            _lineCount = 0;
        }

        public object AddProduct(Guid productId, decimal price)
        {
            BasketLine existingLine;
            if(!_lines.TryGet(l => l.ProductId.Equals(productId), out existingLine))           
                return new BasketLineAdded(_saleId, _lineCount+1, productId, price, quantity: 1);
            return new BasketLineQuantityChanged(_saleId, existingLine.LineNumber, 
                existingLine.LinePrice + price, existingLine.Quantity + 1);
        }


        public void Apply(BasketLineAdded e)
        {
            _lines.Add(new BasketLine(e.LineNumber, e.ProductId, e.Quantity, e.LinePrice));
            _lineCount++;
        }
        public void Apply(BasketLineQuantityChanged e)
        {
            BasketLine oldLine = _lines.Single(l => l.LineNumber == e.LineNumber);
            _lines.Remove(oldLine);
            _lines.Add( new BasketLine(e.LineNumber, oldLine.ProductId, e.Quantity, e.LinePrice));
        }
    }

    struct BasketLine
    {
        public readonly int LineNumber;
        public readonly Guid ProductId;
        public readonly int Quantity;
        public readonly decimal LinePrice;

        public BasketLine(int lineNumber, Guid productId, int quantity, decimal linePrice)
        {
            LineNumber = lineNumber;
            ProductId = productId;
            Quantity = quantity;
            LinePrice = linePrice;
        }
    }
}
