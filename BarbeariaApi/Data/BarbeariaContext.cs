using BarbeariaApi.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace BarbeariaApi.Data;

public class BarbeariaContext : DbContext
{
    public BarbeariaContext(DbContextOptions<BarbeariaContext> options)
        : base(options)
    {
    }   

    public DbSet<Cliente> Clientes { get; set; }
    public DbSet<Barbeiro> Barbeiros { get; set; }
    public DbSet<Agendamento> Agendamentos { get; set; }
}