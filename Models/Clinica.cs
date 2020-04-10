using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AgendamentoPacientes.Models
{
    public class Clinica
    {
        public int Id { get; set; }
        public String Nome { get; set; }
        public String Cnpj { get; set; }
        public String Telefone { get; set; }
        public Endereco Endereco { get; set; }
    }
}