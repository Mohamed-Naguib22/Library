﻿namespace Library.Domain.Models
{
    public class OrderItem
    {
        public int OrderId { get; set; }
        public int BookId { get; set; }
        public int Quantity { get; set; }
        public Order Order { get; set; }
        public Book Book { get; set; }
    }
}