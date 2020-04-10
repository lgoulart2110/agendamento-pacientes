using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AgendamentoPacientes.Models
{
    public class Agendamentos
    {
        public int Id { get; set; }
        [Required]
        [Display(Name = "Data do Atendimento")]
        public DateTime DataAtendimento { get; set; }
        [Required]
        [Display(Name = "Clínica")]
        public int ClinicaId { get; set; }
        public Clinica Clinica { get; set; }
        [Required]
        [Display(Name = "Paciente")]
        public int PacienteId { get; set; }
        public Paciente Paciente { get; set; }
        public int SituacaoAgendamentoId { get; set; }
        public SituacaoAgentamento SituacaoAgentamento { get; set; }
    }
}