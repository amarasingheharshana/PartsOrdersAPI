﻿namespace PartsOrdersAPI.Models
{
    public class Part
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
    }
}
