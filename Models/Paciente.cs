using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AgendamentoPacientes.Models
{
    public class Paciente
    {
        public int Id { get; set; }
        public String Nome { get; set; }
        public String Cpf { get; set; }
        public String Email { get; set; }
        public int? ConvenioId { get; set; }
        public Convenio Convenio { get; set; }
        public int? NumeroConvenio { get; set; }
    }
}