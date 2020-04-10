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
        [Required(ErrorMessage = "Campo obrigatório")]
        [Display(Name = "Data do Atendimento")]
        public DateTime DataAtendimento { get; set; }
        [Required(ErrorMessage = "Campo obrigatório")]
        [Display(Name = "Clínica")]
        public int ClinicaId { get; set; }
        public Clinica Clinica { get; set; }
        [Required(ErrorMessage = "Campo obrigatório")]
        [Display(Name = "Paciente")]
        public int PacienteId { get; set; }
        public Paciente Paciente { get; set; }
        [Display(Name = "Situação do Agendamento")]
        public int SituacaoAgendamentoId { get; set; }
        public SituacaoAgendamento SituacaoAgendamento { get; set; }
    }
}