using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AgendamentoPacientes.Models
{
    public class Endereco
    {
        public int Id { get; set; }
        public String Cep { get; set; }
        public String Logradouro { get; set; }
        public String Numero { get; set; }
        public String Complemento { get; set; }
        public int? ClinicaId { get; set; }
        public Clinica Clinica { get; set; }
        public int? PacienteId { get; set; }
        public Paciente Paciente { get; set; }
    }
}