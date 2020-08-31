using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ApplicationCore.Interfaces
{
    public class PromotionalViewModels
    {
        public int PromotionalId { get; set; }
        public int ProductId { get; set; }
        public int CategoryId { get; set; }
        [Required(ErrorMessage = "Informe quando será o inicio da promoção.")]
        public DateTime Begin { get; set; }
        [Required(ErrorMessage ="Informe quando será o fim da promoção.")]
        public DateTime End { get; set; }
        [Required(ErrorMessage = "Preço promocional.")]
        public Double PromotionValue { get; set; }
    }
}
