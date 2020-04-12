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
            var convenios = _context.Convenios.OrderBy(e => e.Id).ToList();
            return View(convenios);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Convenio convenio)
        {
            ViewBag.error = null;
            try
            {
                if (_context.Convenios.Where(e => e.Nome == convenio.Nome).Count() > 0)
                {
                    throw new Exception("Já existe um Convênio criado com esse nome.");
                }
                _context.Convenios.Add(convenio);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            catch(Exception e)
            {
                ViewBag.error = e.Message;
                return View();
            }
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
            ViewBag.error = null;
            try
            {
                if (_context.Convenios.Where(e => e.Nome == convenio.Nome && e.Id != convenio.Id).Count() > 0)
                {
                    throw new Exception("Já existe um Convênio criado com esse nome.");
                }
                _context.Update(convenio);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            catch(Exception e)
            {
                ViewBag.error = e.Message;
                return View(convenio);
            }            
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