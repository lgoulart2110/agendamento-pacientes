using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AgendamentoPacientes.Models
{
    public class Convenio
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Campo obrigatório")]
        public String Nome { get; set; }        
    }
}