using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Abstract;
using DAL.Models;
using DAL.UnitOfWork;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApp.Controllers
{
    public class ExtruderController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;


        public ExtruderController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        // GET: Extruder
        public ActionResult Index()
        {
            //var extruders = _unitOfWork.ExtruderRepository.GetAll();
            var extruders = _unitOfWork.ExtruderRepository.GetAll();
            return View(extruders);
        }

        // GET: Extruder/Details/5
        public ActionResult Details(string id)
        {
            var extruder = _unitOfWork.ExtruderRepository.Get(id);
            return View(extruder);
        }

        // GET: Extruder/Create
        public ActionResult Create()
        {
            Extruder extruder = new Extruder();
            return View(extruder);
        }

        // POST: Extruder/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Extruder extruder)
        { 

            if(ModelState.IsValid)
            {
                _unitOfWork.ExtruderRepository.Add(extruder);
                return RedirectToAction(nameof(Index));
            }

            return View();

        }

        // GET: Extruder/Edit/5
        public ActionResult Edit(string id)
        {
            var extruder = _unitOfWork.ExtruderRepository.Get(id);
            return View(extruder);
        }

        // POST: Extruder/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(string id, Extruder extruder)
        {
            try
            {
                _unitOfWork.ExtruderRepository.Update(x => x.Id == extruder.Id, extruder);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Extruder/Delete/5
        public ActionResult Delete(string id)
        {
            _unitOfWork.ExtruderRepository.Delete(id);
            return RedirectToAction(nameof(Index));
        }

        // POST: Extruder/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}