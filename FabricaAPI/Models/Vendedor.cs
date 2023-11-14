using System.ComponentModel.DataAnnotations;

namespace FabricaAPI.Models
{
    public class Vendedor
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "O campo Nome é obrigatório.")]
        [MaxLength(100, ErrorMessage = "O campo Nome deve ter no máximo 100 caracteres.")]
        public string Nome { get; set; }

        public int EnderecoId { get; set; }
        public Endereco Endereco { get; set; }


        [EmailAddress(ErrorMessage = "O campo Email não é um endereço de email válido.")]
        [MaxLength(100, ErrorMessage = "O campo Email deve ter no máximo 100 caracteres.")]
        public string Email { get; set; }

        [RegularExpression(@"^\d{10,11}$", ErrorMessage = "O campo Telefone deve conter de 10 a 11 dígitos numéricos.")]
        public string Telefone { get; set; }
    }
}