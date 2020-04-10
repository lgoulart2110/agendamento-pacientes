using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AgendamentoPacientes.Models
{
    public class SituacaoAgendamento
    {
        public int Id { get; set; }
        [Display(Name = "Situação do Agendamento")]
        public String Situacao { get; set; }
    }
}