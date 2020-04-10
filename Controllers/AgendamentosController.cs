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
            var agendamentos = _context.Agendamentos.Include(e => e.Clinica).Include(e => e.Paciente);
            return View(agendamentos.ToList());
        }

        [HttpPost]
        public JsonResult Delete(int id)
        {
            try
            {
                var agendamento = _context.Agendamentos.Find(id);

                if (agendamento == null)
                {
                    return Json(new { error = "Agendamento não encontrado!" });
                }
                else
                {
                    _context.Agendamentos.Remove(agendamento);
                    _context.SaveChanges();
                    return Json(new { success = "Agendamento excluído!" });
                }
            }
            catch(Exception e)
            {
                return Json(new { error = e.Message });
            }
        }

        public IActionResult Create()
        {
            ViewData["ClinicaId"] = new SelectList(_context.Clinicas, "Id", "Id");
            ViewData["PacienteId"] = new SelectList(_context.Pacientes, "Id", "Id");
            return View();
        }

        [HttpPost]
        public IActionResult Create(Agendamentos agendamento)
        {
            _context.Agendamentos.Add(agendamento);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}