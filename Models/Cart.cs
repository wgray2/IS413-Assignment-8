﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IS413_Assignment_5.Models
{
    public class Cart
    {
        public List<CartLine> Lines { get; set; } = new List<CartLine>();

        public virtual void AddItem (Book boo, int qty)
        {
            CartLine line = Lines
                .Where(b => b.Book.ISBN == boo.ISBN)
                .FirstOrDefault();

            if (line == null)
            {
                Lines.Add(new CartLine
                {
                    Book = boo,
                    Quantity = qty
                });
                
            }

            else
            {
                line.Quantity += qty;
            }
        }

        public virtual void RemoveLine(Book boo) =>
            Lines.RemoveAll(x => x.Book.ISBN == boo.ISBN);

        public virtual void Clear() => Lines.Clear();

        public float ComputeTotalSum() =>
            Lines.Sum(e => e.Book.Price * e.Quantity);

        public class CartLine
        {
            public int CartLineID { get; set; }
            public Book Book { get; set; }
            public int Quantity { get; set; }
        }
    }
}
