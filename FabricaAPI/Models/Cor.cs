using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FabricaAPI.Models
{ 
    public class Cor
    {
        [Key]
        public int Id { get; set; }

        [MaxLength(50, ErrorMessage = "O campo Cor deve ter no máximo 50 caracteres.")]
        public string Nome { get; set; }

    }
}