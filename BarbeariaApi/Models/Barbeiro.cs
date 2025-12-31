using System.ComponentModel.DataAnnotations;

namespace BarbeariaApi.Models;

public class Barbeiro
{
    public int Id { get; set; }

    [Required]
    public string Nome { get; set; }
}