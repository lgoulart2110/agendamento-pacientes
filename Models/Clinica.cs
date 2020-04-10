using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AgendamentoPacientes.Models
{
    public class Clinica
    {
        public int Id { get; set; }
        [Required]
        [Display(Name = "Nome da Clínica")]
        public String Nome { get; set; }
        [Required]
        [Display(Name = "CNPJ")]
        public String Cnpj { get; set; }
        [Required]
        [Display(Name = "Telefone")]
        public String Telefone { get; set; }
        [Required]
        public String Cep { get; set; }
        [Required]
        public String Logradouro { get; set; }
        public String Numero { get; set; }
        [Required]
        public String Complemento { get; set; }
    }
}