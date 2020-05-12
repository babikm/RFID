using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DAL.Models;
using DAL.UnitOfWork;
using Microsoft.AspNetCore.Mvc;

namespace WebApp.Controllers
{
    public class ReaderController : Controller
    {
        //private readonly IReaderService _readerService;
        private readonly IUnitOfWork _unitOfWork;


        public ReaderController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        // GET: Reader
        public IActionResult Index()
        {
            var reader = _unitOfWork.ReaderRepository.GetAll();
            return View(reader);
        }

        // GET: Reader/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Reader/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Reader reader)
        {
            if (ModelState.IsValid)
            {
                reader.ReaderNumber = "ABC111"; //dla testu
                if (reader.ReaderNumber != null)
                {
                    _unitOfWork.ReaderRepository.Add(reader);
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    ModelState.AddModelError("Exist", "Taki czytnik jest już w bazie! Podepnij inny!");
                    return View(reader);
                }
            }
            else
            {
                return View(reader);
            }
        }

        // GET: Reader/Edit/5
        public IActionResult Edit(string id)
        {
            return View();
        }

        // POST: Reader/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(string id, Reader reader)
        {
            try
            {
                _unitOfWork.ReaderRepository.Update(x => x.Id == reader.Id, reader);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Reader/Delete/5
        //public ActionResult Delete(int? id)
        //{
        //    if (id == null)
        //    {
        //        return Problem();
        //    }
        //    var details = _readerService.Get((int)id);

        //    return View(details);
        //}


        //[ValidateAntiForgeryToken]
        public IActionResult Delete(string id)
        {
            if (id != null)
            {
                _unitOfWork.ReaderRepository.Delete(id);
                return RedirectToAction(nameof(Index));

            }
            return RedirectToAction(nameof(Index));

        }

        //public IActionResult Details()
        //{
        //    return View();
        //}
        public IActionResult Details(string id)
        {
            if (id == null)
            {
                return BadRequest();
            }

            Reader reader = _unitOfWork.ReaderRepository.Get(id);
            if (reader == null)
            {
                return NotFound();
            }
            return View(reader);

        }

    }
}