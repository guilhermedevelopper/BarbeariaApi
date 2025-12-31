namespace BarbeariaApi.Services;

using BarbeariaApi.Data;
using BarbeariaApi.DTOs.Agendamentos;
using BarbeariaApi.Models;
using Microsoft.EntityFrameworkCore;

public class AgendamentoService
{
    private readonly BarbeariaContext _context;

    public AgendamentoService(BarbeariaContext context)
    {
        _context = context;
    }

    public async Task<Agendamento> CriarAsync(CriarAgendamentoDto dto)
    {
        if (dto.DataHorario < DateTime.Now)
            throw new Exception("Não é possível criar um agendamento em uma data passada.");

        var clienteExiste = await _context.Clientes.AnyAsync(c => c.Id == dto.ClienteId);
        if (!clienteExiste)
            throw new Exception("Cliente não encontrado.");

        var barbeiroExiste = await _context.Barbeiros.AnyAsync(b => b.Id == dto.BarbeiroId);
        if (!barbeiroExiste)
            throw new Exception("Barbeiro não encontrado.");

        var horarioOcupado = await _context.Agendamentos.AnyAsync(a =>
            a.BarbeiroId == dto.BarbeiroId &&
            a.DataHorario == dto.DataHorario
        );

        if (horarioOcupado)
            throw new Exception("Este barbeiro já possui um agendamento nesse horário.");

        var agendamento = new Agendamento
        {
            ClienteId = dto.ClienteId,
            BarbeiroId = dto.BarbeiroId,
            DataHorario = dto.DataHorario,
            TipoServico = dto.TipoServico,
            Status = "Agendado"
        };

        _context.Agendamentos.Add(agendamento);
        await _context.SaveChangesAsync();

        return agendamento;
    }

    public async Task<List<Agendamento>> ListarAsync()
    {
        return await _context.Agendamentos
            .Include(a => a.Cliente)
            .Include(a => a.Barbeiro)
            .ToListAsync();
    }

    public async Task<Agendamento?> BuscarPorIdAsync(int id)
    {
        return await _context.Agendamentos
            .Include(a => a.Cliente)
            .Include(a => a.Barbeiro)
            .FirstOrDefaultAsync(a => a.Id == id);
    }

    public async Task<bool> AtualizarAsync(int id, AtualizarAgendamentoDto dto)
    {
        var agendamento = await _context.Agendamentos.FindAsync(id);

        if (agendamento == null)
            return false;

        agendamento.DataHorario = dto.DataHora;
        agendamento.TipoServico = dto.Servico;

        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> DeletarAsync(int id)
    {
        var agendamento = await _context.Agendamentos.FindAsync(id);

        if (agendamento == null)
            return false;

        _context.Agendamentos.Remove(agendamento);
        await _context.SaveChangesAsync();
        return true;
    }
}