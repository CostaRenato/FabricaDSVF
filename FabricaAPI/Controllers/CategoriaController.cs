using FabricaAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FabricaAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CategoriaController : ControllerBase
    {
        private FabricaDbContext _context;

        public CategoriaController(FabricaDbContext context)
        {
            _context = context;
        }


        [HttpPost("Cadastrar")]
        public async Task<ActionResult<Cor>> CadastrarCategoria(Categoria categoria)
        {

            await _context.Categoria.AddAsync(categoria);
            await _context.SaveChangesAsync();

            return Created("", categoria);
        }

        [HttpGet("Categoria")]
        public async Task<ActionResult<IEnumerable<Categoria>>> ListarCategorias()
        {
            var categorias = await _context.Categoria.ToListAsync();
            return Ok(categorias);
        }

        [HttpGet("Categoria/{id}")]
        public async Task<ActionResult<Categoria>> BuscarCategoriaPorId(int id)
        {
            var categoria = await _context.Categoria.FindAsync(id);
            if (categoria == null)
            {
                return NotFound();
            }

            return Ok(categoria);
        }

        [HttpDelete("Categoria/{id}")]
        public async Task<ActionResult<Categoria>> DeletarCategoria(int id)
        {
            var categoria = await _context.Categoria.FindAsync(id);
            if (categoria == null)
            {
                return NotFound();
            }

            _context.Categoria.Remove(categoria);
            await _context.SaveChangesAsync();

            return Ok(categoria);
        }
    }
}
