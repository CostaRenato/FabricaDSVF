using System.Data.Common;
using FabricaAPI.Models;
using Microsoft.EntityFrameworkCore;

public class FabricaDbContext : DbContext

{

    public DbSet<Produto> Produto { get; set; }
    public DbSet<Endereco> Endereco { get; set; }
    public DbSet<Cliente> Cliente { get; set; }
    public DbSet<Categoria> Categoria { get; set; }
    public DbSet<Venda> Venda { get; set; }
    public DbSet<Tamanho> Tamanho { get; set; }
    public DbSet<Cor> Cor { get; set; }
    public DbSet<Vendedor> Vendedor { get; set; }


    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite("Data Source=fabrica.db");
    }

   

}