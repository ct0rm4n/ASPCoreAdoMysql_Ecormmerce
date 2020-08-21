using System;
using System.Collections.Generic;
using System.Text;

namespace ApplicationCore.Entity
{
    public class Category
    {
        public Category(){
        
        }
        public int CategoryId { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }
        public bool Delete { get; set; }

    }
}
