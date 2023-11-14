using FabricaAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FabricaAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class VendedorController : ControllerBase
    {
        private readonly FabricaDbContext _context;

        public VendedorController(FabricaDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Vendedor>>> ListarTodos()
        {
            var vendedores = await _context.Vendedor.ToListAsync();
            return Ok(vendedores);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Vendedor>> BuscarVendedorPorId(int id)
        {
            var vendedor = await _context.Vendedor.FindAsync(id);
            if (vendedor == null)
            {
                return NotFound();
            }

            return Ok(vendedor);
        }

[HttpPost("CadastrarVendedor")]
public async Task<IActionResult> CadastrarVendedor([FromBody] CriarVendedorDTO criarVendedorDTO)
{

    var endereco = await _context.Endereco.FindAsync(criarVendedorDTO.EnderecoId);
    
    if (endereco == null)
    {
       
        endereco = new Endereco
        {
        Id = criarVendedorDTO.EnderecoId,
        Rua = criarVendedorDTO.Rua,
        Cidade = criarVendedorDTO.Cidade
            
        };

        
        _context.Endereco.Add(endereco);
    }
    var vendedor = new Vendedor
    {
        Nome = criarVendedorDTO.Nome,
	EnderecoId = criarVendedorDTO.EnderecoId,
        Email = criarVendedorDTO.Email,
        Telefone = criarVendedorDTO.Telefone
    };


    _context.Vendedor.Add(vendedor);
    await _context.SaveChangesAsync();

    return Ok("Vendedor cadastrado com sucesso.");
}

        [HttpPut("{id}")]
        public async Task<IActionResult> AtualizarVendedor(int id, Vendedor vendedor)
        {
            if (id != vendedor.Id)
            {
                return BadRequest("IDs não coincidem.");
            }

            _context.Entry(vendedor).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!VendedorExiste(id))
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
        public async Task<IActionResult> DeletarVendedor(int id)
        {
            var vendedor = await _context.Vendedor.FindAsync(id);
            if (vendedor == null)
            {
                return NotFound();
            }

            _context.Vendedor.Remove(vendedor);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool VendedorExiste(int id)
        {
            return _context.Vendedor.Any(e => e.Id == id);
        }
    }
}
