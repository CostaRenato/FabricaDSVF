using FabricaAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


namespace FabricaAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class ProdutoController : ControllerBase
{

    private FabricaDbContext _context;
    private readonly TamanhoService _servicoTamanho;
    public ProdutoController(FabricaDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    [Route("ListarTodos")]
    public async Task<ActionResult<IEnumerable<Produto>>> Listar()
    {
        if (_context.Produto is null) return NotFound();
        return await _context.Produto.ToListAsync();
    }

    [HttpGet]
    [Route("ListarProduto/{id}")]
    public async Task<ActionResult<Produto>> BuscarApenasProduto(int id)
    {
        if (_context.Produto is null) return NotFound();
        var produto = await _context.Produto.FindAsync(id);
        return produto;
    }

    [HttpPost]
    [Route("Cadastrar")]
    public async Task<IActionResult> Cadastrar([FromBody] CriarProdutoDTO criarProdutoDTO)
    {

        var tamanho = await _context.Tamanho.FindAsync(criarProdutoDTO.TamanhoId);
        if (tamanho == null)
        {
            return NotFound();
        }
        var cor = await _context.Cor.FindAsync(criarProdutoDTO.CorId);
        if (cor == null)
        {
            return NotFound();
        }
        var categoria = await _context.Categoria.FindAsync(criarProdutoDTO.CategoriaId);
        if (categoria == null)
        {
            return NotFound();
        }

        var produto = new Produto
        {
            Nome = criarProdutoDTO.Nome,
            Descricao = criarProdutoDTO.Descricao,
            Preco = criarProdutoDTO.Preco,
            TamanhoId = criarProdutoDTO.TamanhoId,
            CorId = criarProdutoDTO.CorId,
            CategoriaId = criarProdutoDTO.CategoriaId
        };

        await _context.AddAsync(produto);
        await _context.SaveChangesAsync();
        return Created("", produto);
    }

    [HttpPut]
    [Route("Alterar")]
    public async Task<ActionResult> Alterar(int id, Produto produto)
    {
        _context.Produto.Update(produto);
        await _context.SaveChangesAsync();
        return Ok();
    }

    [HttpDelete]
    [Route("Deletar / {id}")]
    public async Task<ActionResult> Deletar(int id)
    {
        var produto = await _context.Produto.FindAsync(id);
        if (produto is null) return NotFound();
        _context.Produto.Remove(produto);
        await _context.SaveChangesAsync();
        return Ok();
    }

    [HttpPatch]
    [Route("ModificarDescricao/ {id}")]

    public async Task<IActionResult> MotificaDescricao(int id, [FromForm] string descricao)
    {
        var produto = await _context.Produto.FindAsync(id);
        if (produto is null) return NotFound();
        produto.Descricao = descricao;
        await _context.SaveChangesAsync();
        return Ok();

    }

}


