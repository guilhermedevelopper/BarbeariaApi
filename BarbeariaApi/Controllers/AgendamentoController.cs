namespace BarbeariaApi.Controllers;

using BarbeariaApi.DTOs.Agendamentos;
using BarbeariaApi.Models;
using BarbeariaApi.Services;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class AgendamentoController : ControllerBase
{
    private readonly AgendamentoService _service;

    public AgendamentoController(AgendamentoService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<ActionResult<List<Agendamento>>> RetornarAgendamentos()
    {
        var agendamentos = await _service.ListarAsync();
        return Ok(agendamentos);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Agendamento>> RetornarAgendamentoPorId(int id)
    {
        var agendamento = await _service.BuscarPorIdAsync(id);

        if (agendamento == null)
            return NotFound();

        return Ok(agendamento);
    }

    [HttpPost]
    public async Task<ActionResult> CadastrarAgendamento([FromBody] CriarAgendamentoDto dto)
    {
        try
        {
            var agendamento = await _service.CriarAsync(dto);

            return CreatedAtAction(
                nameof(RetornarAgendamentoPorId),
                new { id = agendamento.Id },
                agendamento
            );
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPut("{id}")]
    public async Task<ActionResult> AtualizarAgendamento(
        int id,
        [FromBody] AtualizarAgendamentoDto dto)
    {
        var atualizado = await _service.AtualizarAsync(id, dto);

        if (!atualizado)
            return NotFound();

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> DeletarAgendamento(int id)
    {
        var deletado = await _service.DeletarAsync(id);

        if (!deletado)
            return NotFound();

        return NoContent();
    }
}