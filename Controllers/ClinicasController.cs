using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AgendamentoPaciente.Data;
using AgendamentoPacientes.Models;
using Microsoft.AspNetCore.Mvc;

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

        [HttpPost]
        public JsonResult Delete(int id)
        {
            try
            {
                var clinica = _context.Clinicas.Find(id);

                if (clinica == null)
                {
                    return Json(new { error = "Agendamento não encontrado!" });
                }
                else
                {
                    _context.Clinicas.Remove(clinica);
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