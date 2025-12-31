namespace BarbeariaApi.DTOs.Agendamentos;

using System.ComponentModel.DataAnnotations;

public class AtualizarAgendamentoDto
{
    [Required]
    public DateTime DataHora { get; set; }

    [Required]
    public string Servico { get; set; } = string.Empty;
}
