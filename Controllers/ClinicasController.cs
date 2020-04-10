using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AgendamentoPaciente.Data;
using AgendamentoPacientes.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Agendamento.Controllers
{
    public class ClinicasController : Controller
    {
        private readonly AgendamentoPacienteDb _context;

        public ClinicasController(AgendamentoPacienteDb context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            var clinicas = _context.Clinicas.ToList();
            return View(clinicas);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Clinica clinica)
        {
            _context.Clinicas.Add(clinica);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        public IActionResult Edit(int id)
        {
            var clinica = _context.Clinicas.Find(id);
            if (clinica == null)
            {
                return NotFound();
            }
            else
            {
                return View(clinica);
            }
        }

        [HttpPost]
        public IActionResult Edit(Clinica clinica)
        {
            _context.Update(clinica);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        public IActionResult Delete(int id)
        {
            var clinica = _context.Clinicas.Find(id);            
            return View(clinica);
        }

        [HttpPost]
        public IActionResult DeleteConfirmed(int id)
        {
            ViewBag.error = null;
            try
            {
                var clinica = _context.Clinicas.Find(id);

                var agendamentoExistenteClinica = _context.Agendamentos.Where(e => e.ClinicaId == id);
                if (agendamentoExistenteClinica.Count() > 0)
                {
                    throw new Exception("Não é possível excluir essa clínica pois existem agendamentos para a mesma.");
                }
                _context.Clinicas.Remove(clinica);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            catch (Exception e)
            {
                ViewBag.error = e.Message;
                var clinica = _context.Clinicas.Find(id);
                return View("Delete", clinica);
            }
        }
    }
}