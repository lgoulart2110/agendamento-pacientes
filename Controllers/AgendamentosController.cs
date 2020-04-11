using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AgendamentoPaciente.Data;
using AgendamentoPacientes.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Agendamento.Controllers
{
    public class AgendamentosController : Controller
    {
        private readonly AgendamentoPacienteDb _context;

        public AgendamentosController(AgendamentoPacienteDb context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            var agendamentos = _context.Agendamentos.Include(e => e.Clinica).Include(e => e.Paciente).Include(e => e.SituacaoAgendamento).Include(e => e.Paciente.Convenio).OrderBy(e => e.DataAtendimento);
            return View(agendamentos.ToList());
        }

        public IActionResult Create()
        {
            ViewBag.Clinica = new SelectList(_context.Clinicas, "Id", "Nome");
            ViewBag.Paciente = new SelectList(_context.Pacientes, "Id", "Nome");
            return View();
        }

        [HttpPost]
        public IActionResult Create(Agendamentos agendamento)
        {
            ViewBag.error = null;
            try
            {
                var diaVerificacao = agendamento.DataAtendimento.Day;
                var mesVerificacao = agendamento.DataAtendimento.Month;
                var anoVerificacao = agendamento.DataAtendimento.Year;

                var qtdAgendamentosMarcados = _context.Agendamentos.Where(e => 
                    e.ClinicaId == agendamento.ClinicaId &&
                    e.DataAtendimento.Day == diaVerificacao &&
                    e.DataAtendimento.Month == mesVerificacao &&
                    e.DataAtendimento.Year == anoVerificacao);

                if (qtdAgendamentosMarcados.Count() >= 20)
                {
                    throw new Exception("Não é possível realizar mais de 20 agendamentos por dia para a clínica.");
                }

                var pacienteJaAgendado = _context.Agendamentos.Where(e =>
                    e.PacienteId == agendamento.PacienteId &&
                    e.DataAtendimento.Day == diaVerificacao &&
                    e.DataAtendimento.Month == mesVerificacao &&
                    e.DataAtendimento.Year == anoVerificacao);

                if (pacienteJaAgendado.Count() > 0)
                {
                    throw new Exception("Já existe um agendamento para esse paciente nessa data.");
                }

                _context.Agendamentos.Add(agendamento);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            catch(Exception e)
            {
                ViewBag.Clinica = new SelectList(_context.Clinicas, "Id", "Nome");
                ViewBag.Paciente = new SelectList(_context.Pacientes, "Id", "Nome");
                ViewBag.error = e.Message;
                return View();
            }
        }

        public IActionResult Edit(int id)
        {
            var agendamento = _context.Agendamentos.Find(id);
            ViewBag.Convenio = new SelectList(_context.Convenios, "Id", "Nome");
            ViewBag.SituacaoAgendamento = new SelectList(_context.SituacaoAgendamentos, "Id", "Situacao");
            ViewBag.Clinica = new SelectList(_context.Clinicas, "Id", "Nome");
            ViewBag.Paciente = new SelectList(_context.Pacientes, "Id", "Nome");
            if (agendamento == null)
            {
                return NotFound();
            }
            else
            {
                return View(agendamento);
            }
        }

        [HttpPost]
        public IActionResult Edit(Agendamentos agendamento)
        {
            agendamento.Paciente = null;
            _context.Update(agendamento);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        public IActionResult Delete(int id)
        {
            var agendamentos = _context.Agendamentos.Include(e => e.Paciente).Include(e => e.Clinica).Include(e => e.SituacaoAgendamento);
            var agendamento = agendamentos.Where(e => e.Id == id).FirstOrDefault();
            return View(agendamento);
        }

        [HttpPost]
        public IActionResult DeleteConfirmed(int id)
        {
            ViewBag.error = null;
            try
            {
                var agendamento = _context.Agendamentos.Find(id);
                if (agendamento.SituacaoAgendamentoId != 1)
                {
                    throw new Exception("Não é possível excluir agendamento com status diferente de 'Aguardando atendimento'.");
                }
                _context.Agendamentos.Remove(agendamento);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            catch (Exception e)
            {
                ViewBag.error = e.Message;
                var agendamentos = _context.Agendamentos.Include(e => e.Paciente).Include(e => e.Clinica).Include(e => e.SituacaoAgendamento);
                var agendamento = agendamentos.Where(e => e.Id == id).FirstOrDefault();
                return View("Delete", agendamento);
            }
        }
    }
}