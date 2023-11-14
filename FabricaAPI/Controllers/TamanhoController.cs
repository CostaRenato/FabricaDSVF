using FabricaAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FabricaAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TamanhoController : ControllerBase
    {
        private FabricaDbContext _context;

           public  TamanhoController (FabricaDbContext context)
        {
            _context = context;
        }

        [HttpPost("Cadastrar")]
        public async Task<ActionResult<Cor>> CadastrarTamanho(Tamanho tamanho)
        {

            await _context.Tamanho.AddAsync(tamanho);
            await _context.SaveChangesAsync();

            return Created("", tamanho);
        }

        [HttpGet("Tamanho")]
        public async Task<ActionResult<IEnumerable<Tamanho>>> ListarTamanhos()
        {
            var tamanhos = await _context.Tamanho.ToListAsync();
            return Ok(tamanhos);
        }

        [HttpGet("Tamanho/{id}")]
        public async Task<ActionResult<Tamanho>> BuscarTamanhoPorId(int id)
        {
            var tamanho = await _context.Tamanho.FindAsync(id);
            if (tamanho == null)
            {
                return NotFound();
            }

            return Ok(tamanho);
        }

       [HttpDelete("Tamanho/{id}")]
        public async Task<ActionResult<Tamanho>> DeletarTamanho(int id)
        {
            var tamanho = await _context.Tamanho.FindAsync(id);
            if (tamanho == null)
            {
                return NotFound();
            }

            _context.Tamanho.Remove(tamanho);
            await _context.SaveChangesAsync();

            return Ok(tamanho);
        }
    }
}
