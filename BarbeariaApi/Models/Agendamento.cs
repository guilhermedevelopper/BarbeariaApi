using System.ComponentModel.DataAnnotations;

namespace BarbeariaApi.Models;
public class Agendamento
{
    public int Id { get; set; }

    [Required]
    public int ClienteId { get; set; }
    public Cliente Cliente { get; set; }

    [Required]
    public int BarbeiroId { get; set; }
    public Barbeiro Barbeiro { get; set; }

    [Required]
    public string TipoServico { get; set; }

    [Required]
    public string Status { get; set; }

    public DateTime DataHorario { get; set; }
}
