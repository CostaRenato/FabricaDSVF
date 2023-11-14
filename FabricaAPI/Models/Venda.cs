using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FabricaAPI.Models
{
    public class Venda
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "O campo Data da Venda é obrigatório.")]
        public DateTime DataVenda { get; set; }

        [Required(ErrorMessage = "O campo ID do Cliente é obrigatório.")]
        public int ClienteId { get; set; }

        [ForeignKey("ClienteId")]
        public Cliente Cliente { get; set; }

         public int VendedorId { get; set; }

         [ForeignKey("VendedorId")]
        public Vendedor Vendedor { get; set; }
        
        public int ProdutoId { get; set; }

        [ForeignKey("ProdutoId")]
        public Produto Produto { get; set; }

        [Required(ErrorMessage = "O campo Valor Total é obrigatório.")]
        [Range(0.01, double.MaxValue, ErrorMessage = "O campo Valor Total deve ser maior que zero.")]
        public decimal ValorTotal { get; set; }
    }
}