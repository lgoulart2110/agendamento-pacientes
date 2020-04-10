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
    public class ConveniosController : Controller
    {
        private readonly AgendamentoPacienteDb _context;

        public ConveniosController(AgendamentoPacienteDb context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            var convenios = _context.Convenios.ToList();
            return View(convenios);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Convenio convenio)
        {
            _context.Convenios.Add(convenio);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        public IActionResult Edit(int id)
        {
            var convenio = _context.Convenios.Find(id);
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

        public IActionResult Delete(int id)
        {
            var convenio = _context.Convenios.Find(id);
            return View(convenio);
        }

        [HttpPost]
        public IActionResult DeleteConfirmed(int id)
        {
            ViewBag.error = null;
            try
            {
                var convenio = _context.Convenios.Find(id);

                var pacienteComConvenio = _context.Pacientes.Where(e => e.ConvenioId == id);
                if (pacienteComConvenio.Count() > 0)
                {
                    throw new Exception("Não é possível excluir esse convênio pois existem pacientes que o utilizam.");
                }
                _context.Convenios.Remove(convenio);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            catch (Exception e)
            {
                ViewBag.error = e.Message;
                var convenio = _context.Convenios.Find(id);
                return View("Delete", convenio);
            }
        }
    }
}