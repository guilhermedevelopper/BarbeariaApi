namespace BarbeariaApi.DTOs.Clientes;

using System.ComponentModel.DataAnnotations;

public class CriarClienteDto
{
    [Required]
    public string Nome { get; set; }

    [Required]
    [RegularExpression(
        @"^\(?\d{2}\)?\s?\d{5}-?\d{4}$",
        ErrorMessage = "Telefone inválido"
    )]
    public string Telefone { get; set; }
}