namespace BarbeariaApi.DTOs.Barbeiros;

using System.ComponentModel.DataAnnotations;

public class CriarBarbeiroDto
{
    [Required]
    public string Nome { get; set; }
}