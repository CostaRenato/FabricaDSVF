using FabricaAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FabricaAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CorController : ControllerBase
    {
        private FabricaDbContext _context;

        public CorController(FabricaDbContext context)
        {
            _context = context;
        }

        // Endpoints relacionados à model Cor

        [HttpPost("Cadastrar")]
        public async Task<ActionResult<Cor>> CadastrarCor(Cor cor)
        {

            await _context.Cor.AddAsync(cor);
            await _context.SaveChangesAsync();

            return Created("", cor);
        }

        [HttpGet]
        [Route("Listar cor")]
        public async Task<ActionResult<IEnumerable<Cor>>> Listar()
        {
            if (_context.Cor is null) return NotFound();
            return await _context.Cor.ToListAsync();
        }

        [HttpGet("Cor/{id}")]
        public async Task<ActionResult<Cor>> BuscarCorPorId(int id)
        {
            var cor = await _context.Cor.FindAsync(id);
            if (cor == null)
            {
                return NotFound();
            }

            return Ok(cor);
        }

        [HttpDelete("Cor/{id}")]
        public async Task<ActionResult<Cor>> DeletarCor(int id)
        {
            var cor = await _context.Cor.FindAsync(id);
            if (cor == null)
            {
                return NotFound();
            }

            _context.Cor.Remove(cor);
            await _context.SaveChangesAsync();

            return Ok(cor);
        }
    }
}
