using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace UI.Web.Models
{
    public class ChangePasswordModel
    {
        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Current password")]
        public string OldPassword { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "New password")]
        public string NewPassword { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm new password")]
        [StringLength(100, ErrorMessage = "The new password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }
    }

    public class LogOnModel
    {
        [Required]
        [Display(Name = "E-mail")]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [Display(Name = "Remember me?")]
        public bool RememberMe { get; set; }
    }

    public class RegisterModel
    {
        [Required]
        [Display(Name = "User name")]
        public string UserName { get; set; }

        [Display(Name = "Last name")]
        public string LastName { get; set; }

        [Required]
        [DataType(DataType.EmailAddress)]
        [Display(Name = "Email address")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Obrigatório defir o celular.")]
        [Display(Name = "Celular")]
        public string Celular { get; set; }
        [Display(Name = "Cpf")]
        [Required(ErrorMessage = "Obrigatório defir o cpf.")]
        public string Cpf { get; set; }
        [Required(ErrorMessage = "Obrigatório defir o cep.")]
        public string Cep { get; set; }
        [Required(ErrorMessage = "Obrigatório defir a rua.")]
        public string Rua { get; set; }
        public string Complemento { get; set; }
        [Required(ErrorMessage = "Obrigatório defir o bairro.")]
        public string Bairro { get; set; }
        [Required(ErrorMessage = "Obrigatório defir o cep.")]
        public string N { get; set; }
        [Required(ErrorMessage = "Obrigatório defir o cep.")]
        public string Es { get; set; }
        [Required(ErrorMessage = "Obrigatório defir o cep.")]
        public string Cidade { get; set; }
        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }
        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [StringLength(100, ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }
        public bool Remember { get; set; }
        public bool Terms { get; set; }
    }

}
