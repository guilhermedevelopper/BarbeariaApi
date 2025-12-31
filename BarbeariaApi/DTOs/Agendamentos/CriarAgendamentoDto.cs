using System.ComponentModel.DataAnnotations;

namespace BarbeariaApi.DTOs.Agendamentos;

public class CriarAgendamentoDto
{
    [Required]
    public int ClienteId { get; set; }

    [Required]
    public int BarbeiroId { get; set; }

    [Required]
    public string TipoServico { get; set; }

    [Required]
    public DateTime DataHorario { get; set; }
}