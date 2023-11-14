using System.ComponentModel.DataAnnotations;

namespace FabricaAPI.Models
{
    public class Produto
    {
        [Key]
        public int Id { get; set; }

        public string Nome { get; set; } = string.Empty;

        public string Descricao { get; set; } = string.Empty;

        public double Preco { get; set; }

        // Propriedade de navegação para a classe Cor
        public int CorId { get; set; }
        public Cor Cor { get; set; }

        // Propriedade de navegação para a classe Tamanho
        public int TamanhoId { get; set; }
        public Tamanho Tamanho { get; set; }

        // Propriedade de navegação para a classe Categoria
        public int CategoriaId { get; set; }
        public Categoria Categoria { get; set; }
    }
}
