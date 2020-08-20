using System;

namespace ApplicationCore.Entity
{
    public class Product
    {
        public Product()
        {

        }
        public int ProductId { get; set; }
        public int CategoryId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Avatar { get; set; }
        public int Stock { get; set; }
        public Double Value { get; set; }
        public bool Delete { get; set; }
    }
}
