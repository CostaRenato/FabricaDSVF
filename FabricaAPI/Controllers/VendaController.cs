using FabricaAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FabricaAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class VendaController : ControllerBase
    {
        private readonly FabricaDbContext _context;

        public VendaController(FabricaDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Venda>>> ListarTodasAsVenda()
        {
            var Venda = await _context.Venda.ToListAsync();
            return Ok(Venda);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Venda>> BuscarVendaPorId(int id)
        {
            var venda = await _context.Venda.FindAsync(id);
            if (venda == null)
            {
                return NotFound();
            }

            return Ok(venda);
        }

        [HttpPost]
        public async Task<IActionResult> CadastrarVenda(Venda venda)
        {
            try
            {
                _context.Venda.Add(venda);
                await _context.SaveChangesAsync();
                return CreatedAtAction(nameof(BuscarVendaPorId), new { id = venda.Id }, venda);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> AtualizarVenda(int id, Venda venda)
        {
            if (id != venda.Id)
            {
                return BadRequest("IDs não coincidem.");
            }

            _context.Entry(venda).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!VendaExiste(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletarVenda(int id)
        {
            var venda = await _context.Venda.FindAsync(id);
            if (venda == null)
            {
                return NotFound();
            }

            _context.Venda.Remove(venda);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool VendaExiste(int id)
        {
            return _context.Venda.Any(e => e.Id == id);
        }
    }
}
