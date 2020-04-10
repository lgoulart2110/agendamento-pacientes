using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AgendamentoPaciente.Data;
using AgendamentoPacientes.Models;
using Microsoft.AspNetCore.Mvc;

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
            var pacientes = _context.Pacientes.ToList();
            return View(pacientes);
        }

        public IActionResult Create()
        {
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
            var clinica = _context.Pacientes.Find(id);
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
        public IActionResult Edit(Paciente paciente)
        {
            _context.Update(paciente);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpPost]
        public JsonResult Delete(int id)
        {
            try
            {
                var clinica = _context.Pacientes.Find(id);

                if (clinica == null)
                {
                    return Json(new { error = "Agendamento não encontrado!" });
                }
                else
                {
                    _context.Pacientes.Remove(clinica);
                    _context.SaveChanges();
                    return Json(new { success = "Agendamento excluído!" });
                }
            }
            catch (Exception e)
            {
                return Json(new { error = e.Message });
            }
        }
    }
}