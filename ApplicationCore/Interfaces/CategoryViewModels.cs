using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ApplicationCore.Interfaces
{
    public class CategoryViewModel
    {
        public int CategoryId { get; set; }        
        [Required(ErrorMessage = "Obrigatório defir o nome do produto.")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Obrigatório descrever o mesmo.")]
        public string Description { get; set; }        
        public bool Delete { get; set; }

    }

}
