// TamanhoService.cs
using FabricaAPI.Models;

public class TamanhoService
{
    private readonly FabricaDbContext _contexto;

    public TamanhoService(FabricaDbContext contexto)
    {
        _contexto = contexto;
    }

    public Tamanho BuscarTamanhoPorId(int tamanhoId)
    {
        // Sua lógica para recuperar um Tamanho por ID do banco de dados
        return _contexto.Tamanho.FirstOrDefault(t => t.Id == tamanhoId);
    }
}
