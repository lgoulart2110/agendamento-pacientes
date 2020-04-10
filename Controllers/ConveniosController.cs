using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AgendamentoPaciente.Data;
using AgendamentoPacientes.Models;
using Microsoft.AspNetCore.Mvc;

namespace Agendamento.Controllers
{
    public class ConveniosController : Controller
    {
        private readonly AgendamentoPacienteDb _context;

        public ConveniosController(AgendamentoPacienteDb context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            var convenios = _context.Conveios.ToList();
            return View(convenios);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Convenio convenio)
        {
            _context.Conveios.Add(convenio);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        public IActionResult Edit(int id)
        {
            var convenio = _context.Conveios.Find(id);
            if (convenio == null)
            {
                return NotFound();
            }
            else
            {
                return View(convenio);
            }
        }

        [HttpPost]
        public IActionResult Edit(Convenio convenio)
        {
            _context.Update(convenio);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpPost]
        public JsonResult Delete(int id)
        {
            try
            {
                var convenio = _context.Conveios.Find(id);

                if (convenio == null)
                {
                    return Json(new { error = "Agendamento não encontrado!" });
                }
                else
                {
                    _context.Conveios.Remove(convenio);
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