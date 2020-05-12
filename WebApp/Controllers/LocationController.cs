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
    public class LocationController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;


        public LocationController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        // GET: Location
        public ActionResult Index()
        {
            var locations = _unitOfWork.LocationRepository.GetAll();
            return View(locations);
        }

        // GET: Location/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Location/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Location location)
        {
            if (ModelState.IsValid)
            {
                _unitOfWork.LocationRepository.Add(location);
                return RedirectToAction(nameof(Index));
            }

            return View();
        }

        // GET: Location/Edit/5
        public ActionResult Edit(string id)
        {
            var location = _unitOfWork.LocationRepository.Get(id);
            return View(location);
        }

        // POST: Location/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(string id, Location location)
        {
            try
            {
                _unitOfWork.LocationRepository.Update(x => x.Id == location.Id, location);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Location/Delete/5
        public ActionResult Delete(string id)
        {
            _unitOfWork.LocationRepository.Delete(id);
            return RedirectToAction(nameof(Index));
        }

        // POST: Location/Delete/5
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