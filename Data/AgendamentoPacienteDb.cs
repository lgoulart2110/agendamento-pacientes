using AgendamentoPacientes.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AgendamentoPaciente.Data
{
    public class AgendamentoPacienteDb : DbContext
    {
        public AgendamentoPacienteDb(DbContextOptions<AgendamentoPacienteDb> options) : base(options)
        {
        }
        public DbSet<Agendamentos> Agendamentos { get; set; }
        public DbSet<Clinica> Clinicas { get; set; }
        public DbSet<Convenio> Convenios { get; set; }
        public DbSet<Paciente> Pacientes { get; set; }
        public DbSet<SituacaoAgendamento> SituacaoAgendamentos { get; set; }
    }
}
