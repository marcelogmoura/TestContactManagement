using System.ComponentModel.DataAnnotations;

namespace ContactManagement.Models
{
    public class Contact
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "O nome é obrigatório.")]
        [StringLength(100, MinimumLength = 5, ErrorMessage = "O nome deve ter entre 5 e 100 caracteres.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "O contato é obrigatório.")]
        [RegularExpression(@"^\d{9}$", ErrorMessage = "O contato deve ter exatamente 9 dígitos.")]
        public string Phone { get; set; } 

        [Required(ErrorMessage = "O e-mail é obrigatório.")]
        [EmailAddress(ErrorMessage = "O formato do e-mail é inválido.")]
        public string Email { get; set; }
                
        public bool IsDeleted { get; set; } = false;
    }
}