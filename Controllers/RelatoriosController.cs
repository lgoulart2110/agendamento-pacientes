using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AgendamentoPaciente.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Rotativa;

namespace Agendamento.Controllers
{
    public class RelatoriosController : Controller
    {
        private readonly AgendamentoPacienteDb _context;

        public RelatoriosController(AgendamentoPacienteDb context)
        {
            _context = context;
        }
        public IActionResult RelatorioPacientes()
        {
            ViewBag.SituacaoAgendamento = new SelectList(_context.SituacaoAgendamentos, "Id", "Situacao");
            return View();
        }

        [HttpPost]
        public IActionResult ListagemPacientes(DateTime dataInicio, DateTime dataFim, int situacaoAgendamentoId)
        {
            var listagem = _context.Agendamentos.Include(e => e.Paciente).Include(e => e.Clinica).Include(e => e.SituacaoAgendamento);

            var listaFiltrada = listagem.Where(e =>
                e.DataAtendimento >= dataInicio &&
                e.DataAtendimento <= dataFim &&
                e.SituacaoAgendamentoId == situacaoAgendamentoId).ToList();
            
            return View(listaFiltrada);
            
        }

        public IActionResult RelatorioClinicas()
        {
            ViewBag.Clinicas = new SelectList(_context.Clinicas, "Id", "Nome");
            return View();
        }

        [HttpPost]
        public IActionResult ListagemClinicas(DateTime data, int clinicaId)
        {
            var clinica = _context.Clinicas.Find(clinicaId);

            var dia = data.Day;
            var mes = data.Month;
            var ano = data.Year;

            var agendamentos = _context.Agendamentos.Where(e =>
                e.DataAtendimento.Day == dia &&
                e.DataAtendimento.Month == mes &&
                e.DataAtendimento.Year == ano &&
                e.ClinicaId == clinicaId).Count();

            ViewBag.Clinica = clinica.Nome;
            ViewBag.AgendamentoDisponivel = 20 - agendamentos;

            return View();

        }
    }
}