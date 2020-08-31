using System;
using System.Collections.Generic;
using System.Text;

namespace ApplicationCore.Entity
{
    public class Promotional
    {
        public int PromotionalId { get; set; }
        public int ProductId { get; set; }
        public int CategoryId { get; set; }
        public DateTime Begin { get; set; }
        public DateTime End { get; set; }
        public Double PromotionValue { get; set; }
        public bool Delete { get; set; }
    }
}
