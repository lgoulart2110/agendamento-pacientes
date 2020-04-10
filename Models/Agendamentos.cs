using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AgendamentoPacientes.Models
{
    public class Agendamentos
    {
        public int Id { get; set; }
        public DateTime DataAtendimento { get; set; }
        public int ClinicaId { get; set; }
        public Clinica Clinica { get; set; }
        public int PacienteId { get; set; }
        public Paciente Paciente { get; set; }
        public int SituacaoAgendamentoId { get; set; }
        public SituacaoAgentamento SituacaoAgentamento { get; set; }
    }
}