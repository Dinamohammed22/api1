﻿namespace api1.Models
{
    public class Product
    {
        public int id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int? CategoryId { get; set; }
    }
}
