using BarbeariaApi.Data;
using BarbeariaApi.DTOs.Barbeiros;
using BarbeariaApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BarbeariaApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class BarbeirosController : ControllerBase
{
    private readonly BarbeariaContext _context;

    public BarbeirosController(BarbeariaContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<List<Barbeiro>>> RetornarBarbeiros()
    {
        var barbeiros = await _context.Barbeiros.ToListAsync();
        return Ok(barbeiros);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Barbeiro>> RetornarBarbeiroPorId(int id)
    {
        var barbeiro = await _context.Barbeiros.FindAsync(id);

        if (barbeiro == null)
            return NotFound();

        return Ok(barbeiro);
    }

    [HttpPost]
    public async Task<ActionResult> CadastrarBarbeiro(
       [FromBody] CriarBarbeiroDto dto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var barbeiro = new Barbeiro
        {
            Nome = dto.Nome
        };

        _context.Barbeiros.Add(barbeiro);
        await _context.SaveChangesAsync();

        return CreatedAtAction(
            nameof(RetornarBarbeiroPorId),
            new { id = barbeiro.Id },
            barbeiro
        );
    }   
}