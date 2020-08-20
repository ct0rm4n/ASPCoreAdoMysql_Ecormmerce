using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;


namespace ApplicationCore.Interfaces
{
    public class ProductViewModel
    {
        public int ProductId { get; set; }
        [Required(ErrorMessage = "É necessário definir a qual categoria o produto é pertencente.")]
        public int CategoryId { get; set; }
        [Required(ErrorMessage = "Obrigatório defir o nome do produto.")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Obrigatório descrever o mesmo.")]
        public string Description { get; set; }
        [Required(ErrorMessage = "Obrigatório definir o valor do produto.")]
        public Double Value { get; set; }
        public string Avatar { get; set; }
        public int Stock { get; set; }
        public bool Delete { get; set; }
        public virtual List<IFormFile> file { get; set; }
        
    }
    
}
