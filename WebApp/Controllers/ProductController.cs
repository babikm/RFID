using System;
using System.Collections;
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
    public class ProductController : Controller
    {
        private readonly IProductService _productService;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IReaderService _readerService;


        public ProductController(IUnitOfWork unitOfWork, IProductService productService,
            IReaderService readerService)
        {
            _unitOfWork = unitOfWork;
            _productService = productService;
            _readerService = readerService;
        }

        // GET: Product
        public IActionResult Index(string searchString)
        {
            var products = _unitOfWork.ProductRepository.GetAll();
            if (!String.IsNullOrEmpty(searchString))
            {
                products = products.Where(x => x.Characeteristic.Contains(searchString));
            }
            return View(products);
        }

        // GET: Product/Details/5
        public IActionResult Details(string id)
        {
            var product = _unitOfWork.ProductRepository.Get(id);
            return View(product);
        }

        // GET: Product/Create
        public ActionResult Create()
        {
            var list = _unitOfWork.WorkerRepository.GetAll();
            var list1 = _unitOfWork.WorkerRepository.GetAll();
            var extList = _unitOfWork.ExtruderRepository.GetAll();

            ViewBag.list = list;
            ViewBag.list1 = list1;
            ViewBag.extList = extList;

            Product product = new Product();
            product.TagId = _readerService.GetTagId();
            if(product.TagId == null)
            {
                ModelState.AddModelError("nullTagId", "Przy³ó¿ identyfikator do czytnika!");
            }
            return View(product);
        }


        // POST: Product/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Product product)
        {
            var list = _unitOfWork.WorkerRepository.GetAll();
            var list1 = _unitOfWork.WorkerRepository.GetAll();
            var extList = _unitOfWork.ExtruderRepository.GetAll();

            ViewBag.list = list;
            ViewBag.list1 = list1;
            ViewBag.extList = extList;

            product.TagId = _readerService.GetTagId();

            if(product.TagId!=null)
            {
                var isExist = _productService.IsTagIdExist(product.TagId);
                if(isExist)
                {
                    ModelState.AddModelError("tagId", "Taki identyfikator znajduje siê ju¿ w bazie!");
                    return View();
                }
            }

            if (ModelState.IsValid)
            {                
                product.Worker1 = _unitOfWork.WorkerRepository.Get(product.Worker1.Id);
                product.Worker2 = _unitOfWork.WorkerRepository.Get(product.Worker2.Id);
                product.ExtruderName = _unitOfWork.ExtruderRepository.Get(product.ExtruderName.Id);

                product.Date = DateTime.Now;
                
                _unitOfWork.ProductRepository.Add(product);
                return RedirectToAction(nameof(Index));
            }
            else
            {
                var errors = ModelState
                    .Where(x => x.Value.Errors.Count > 0)
                    .Select(x => new { x.Key, x.Value.Errors })
                    .ToArray();
                return View(product);
            }

        }

        // GET: Product/Edit/5
        public ActionResult Edit(string id)
        {
            var list = _unitOfWork.WorkerRepository.GetAll();
            var list1 = _unitOfWork.WorkerRepository.GetAll();
            var extList = _unitOfWork.ExtruderRepository.GetAll();

            ViewBag.list = list;
            ViewBag.list1 = list1;
            ViewBag.extList = extList;
            var product = _unitOfWork.ProductRepository.Get(id);

            return View(product);
        }

        // POST: Product/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(string id, Product product)
        {
            try
            {
                var list = _unitOfWork.WorkerRepository.GetAll();
                var list1 = _unitOfWork.WorkerRepository.GetAll();
                var extList = _unitOfWork.ExtruderRepository.GetAll();

                ViewBag.list = list;
                ViewBag.list1 = list1;
                ViewBag.extList = extList;
                product.ExtruderName = _unitOfWork.ExtruderRepository.Get(product.ExtruderName.Id);
                _unitOfWork.ProductRepository.Update(x => x.Id == product.Id, product);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Product/Delete/5
        public IActionResult Delete(string id)
        {
            if (id != null)
            {
                _unitOfWork.ProductRepository.Delete(id);
                return RedirectToAction(nameof(Index));

            }
            return RedirectToAction(nameof(Index));
        }

        // POST: Product/Delete/5
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

        [HttpGet]
        public Product CzarnyMFI50(Product product)
        {
            product.MaterialType = "PP";
            return product;
        }
    }

}