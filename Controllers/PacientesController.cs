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
    public class PacientesController : Controller
    {
        private readonly AgendamentoPacienteDb _context;

        public PacientesController(AgendamentoPacienteDb context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            var pacientes = _context.Pacientes.Include(e => e.Convenio);
            return View(pacientes.ToList());
        }

        public IActionResult Create()
        {
            ViewBag.Convenio = new SelectList(_context.Convenios, "Id", "Nome");
            return View();
        }

        [HttpPost]
        public IActionResult Create(Paciente paciente)
        {
            _context.Pacientes.Add(paciente);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        public IActionResult Edit(int id)
        {
            ViewBag.Convenio = new SelectList(_context.Convenios, "Id", "Nome");
            var paciente = _context.Pacientes.Find(id);
            if (paciente == null)
            {
                return NotFound();
            }
            else
            {
                return View(paciente);
            }
        }

        [HttpPost]
        public IActionResult Edit(Paciente paciente)
        {
            _context.Update(paciente);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        public IActionResult Delete(int id)
        {
            var pacientes = _context.Pacientes.Include(e => e.Convenio);
            var paciente = pacientes.Where(e => e.Id == id).FirstOrDefault();
            return View(paciente);
        }

        [HttpPost]
        public IActionResult DeleteConfirmed(int id)
        {
            ViewBag.error = null;
            try
            {
                var pacientes = _context.Pacientes.Include(e => e.Convenio);
                var paciente = pacientes.Where(e => e.Id == id).FirstOrDefault();

                var agendamentoExistentePaciente = _context.Agendamentos.Where(e => e.PacienteId == id);
                if (agendamentoExistentePaciente.Count() > 0)
                {
                    throw new Exception("Não é possível excluir esse paciente pois existem agendamentos para ele.");
                }
                _context.Pacientes.Remove(paciente);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            catch (Exception e)
            {
                ViewBag.error = e.Message;
                var pacientes = _context.Pacientes.Include(e => e.Convenio);
                var paciente = pacientes.Where(e => e.Id == id).FirstOrDefault();
                return View("Delete", paciente);
            }
        }
    }
}