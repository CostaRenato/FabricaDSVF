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
    public class ClienteController : ControllerBase
    {
        private readonly FabricaDbContext _context;

        public ClienteController(FabricaDbContext context)
        {
            _context = context;
        }

        [HttpGet("Listar Todos")]
        public async Task<ActionResult<IEnumerable<Cliente>>> ListarTodos()
        {
            var clientes = await _context.Cliente.ToListAsync();
            return Ok(clientes);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Cliente>> BuscarClientePorId(int id)
        {
            var cliente = await _context.Cliente.FindAsync(id);
            if (cliente == null)
            {
                return NotFound();
            }

            return Ok(cliente);
        }

[HttpPost("CadastrarCliente")]
public async Task<IActionResult> CadastrarCliente([FromBody] CriarClienteDTO criarClienteDTO)
{
    
    var endereco = await _context.Endereco.FindAsync(criarClienteDTO.EnderecoId);
    
    if (endereco == null)
    {
       
        endereco = new Endereco
        {
        Id = criarClienteDTO.EnderecoId,
        Rua = criarClienteDTO.Rua,
        Cidade = criarClienteDTO.Cidade
            
        };

        
        _context.Endereco.Add(endereco);
    }
    var cliente = new Cliente
    {
        Nome = criarClienteDTO.Nome,
        EnderecoId = criarClienteDTO.EnderecoId,
        Email = criarClienteDTO.Email,
        Telefone = criarClienteDTO.Telefone
    };


    _context.Cliente.Add(cliente);
    await _context.SaveChangesAsync();

    return Ok("Cliente cadastrado com sucesso.");
}

        [HttpPut("Atualizar{id}")]
        public async Task<IActionResult> AtualizarCliente(int id, Cliente cliente)
        {
            if (id != cliente.Id)
            {
                return BadRequest("IDs não coincidem.");
            }

            _context.Entry(cliente).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ClienteExiste(id))
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

        [HttpDelete("Deletar{id}")]
        public async Task<IActionResult> DeletarCliente(int id)
        {
            var cliente = await _context.Cliente.FindAsync(id);
            if (cliente == null)
            {
                return NotFound();
            }

            _context.Cliente.Remove(cliente);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ClienteExiste(int id)
        {
            return _context.Cliente.Any(e => e.Id == id);
        }
    }
}