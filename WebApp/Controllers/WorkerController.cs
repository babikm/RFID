using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DAL.Models;
using DAL.UnitOfWork;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApp.Controllers
{
    public class WorkerController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public WorkerController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        // GET: Worker
        public ActionResult Index()
        {
            var workers = _unitOfWork.WorkerRepository.GetAll();
            return View(workers);
        }

        // GET: Worker/Details/5
        public ActionResult Details(string id)
        {
            var worker = _unitOfWork.WorkerRepository.Get(id);
            return View(worker);
        }

        // GET: Worker/Create
        public ActionResult Create()
        {
            Worker worker = new Worker();
            return View(worker);
        }

        // POST: Worker/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Worker worker)
        {
            if(ModelState.IsValid)
            {
                _unitOfWork.WorkerRepository.Add(worker);
                return RedirectToAction(nameof(Index));                
            }
            return View();
        }

        // GET: Worker/Edit/5
        public ActionResult Edit(string id)
        {
            var worker = _unitOfWork.WorkerRepository.Get(id);
            return View(worker);
        }

        // POST: Worker/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(string id, Worker worker)
        {
            try
            {
                _unitOfWork.WorkerRepository.Update(x => x.Id == worker.Id, worker);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Worker/Delete/5
        public ActionResult Delete(string id)
        {
            var worker = _unitOfWork.WorkerRepository.Get(id);
            return View(worker);
        }

        // POST: Worker/Delete/5
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