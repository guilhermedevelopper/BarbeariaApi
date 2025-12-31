using BarbeariaApi.Data;
using BarbeariaApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BarbeariaApi.DTOs.Clientes;
namespace BarbeariaApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ClientesController : ControllerBase
{
    private readonly BarbeariaContext _context;

    public ClientesController(BarbeariaContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<List<Cliente>>> RetornarClientes()
    {
        var clientes = await _context.Clientes.ToListAsync();
        return Ok(clientes);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Cliente>> RetornarClientePorId(int id)
    {
        var cliente = await _context.Clientes.FindAsync(id);

        if (cliente == null)
            return NotFound();

        return Ok(cliente);
    }

    [HttpPost]
    public async Task<ActionResult> CadastrarCliente([FromBody] CriarClienteDto dto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var cliente = new Cliente
        {
            Nome = dto.Nome,
            Telefone = dto.Telefone
        };

        _context.Clientes.Add(cliente);
        await _context.SaveChangesAsync();

        return CreatedAtAction(
            nameof(RetornarClientePorId),
            new { id = cliente.Id },
            cliente
        );
    }
}