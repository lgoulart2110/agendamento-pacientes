using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AgendamentoPacientes.Models
{
    public class Paciente
    {
        public int Id { get; set; }
        [Required]
        public String Nome { get; set; }
        [Required]
        [Display(Name = "CPF")]
        public String Cpf { get; set; }
        [Required]
        public String Telefone { get; set; }
        [Display(Name = "E-mail")]
        public String Email { get; set; }
        [Display(Name = "Convênio")]
        public int? ConvenioId { get; set; }
        public Convenio Convenio { get; set; }
        [Display(Name = "Número do Convênio")]
        public int? NumeroConvenio { get; set; }
    }
}