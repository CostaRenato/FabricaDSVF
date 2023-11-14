using System.ComponentModel.DataAnnotations;

namespace FabricaAPI.Models
{
    public class Endereco
    {
        [Key]
        public int Id { get; set; }

        [MaxLength(100, ErrorMessage = "O campo Rua deve ter no máximo 100 caracteres.")]
        public string Rua { get; set; }

        [MaxLength(50, ErrorMessage = "O campo Cidade deve ter no máximo 50 caracteres.")]
        public string Cidade { get; set; }
    }
}