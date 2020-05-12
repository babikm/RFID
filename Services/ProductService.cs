using Abstract;
using DAL.Models;
using DAL.UnitOfWork;
using System;
using System.Collections.Generic;

namespace Services
{

    //Zarządzanie pracownikami admin + kierownik/szef
    //Rejestracja/logowanie tylko dla admina + kierownik/szef


    //Magazyn produktów 
    //wyszukiwanie po danym szczególe tj. Pracownik, materiał itp 
    //Masowa zmiana produktów

    public class ProductService : IProductService
    {
        private readonly IReaderService _readerService;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IProductService _productService;

        public ProductService(IUnitOfWork unitOfWork, IReaderService readerService)
        {
            //_productService = productService;
            _unitOfWork = unitOfWork;
            _readerService = readerService;
        }

        public void Add(Product product)
        {
            _unitOfWork.ProductRepository.Add(new Product
            {
                Id = product.Id,
                TagId = product.TagId,
                CompanyName = product.CompanyName,
                SupplierCode = product.SupplierCode,
                MaterialType = product.MaterialType,
                Weight = product.Weight,
                Characeteristic = product.Characeteristic,
                Comments = product.Comments,
                ExtruderName = product.ExtruderName,
                Status = product.Status,
                Worker1 = product.Worker1,
                Worker2 = product.Worker2,
                Date = product.Date
            });
        }

        public bool Check(Product product, string tagId)
        {
            //sprawdź jaki jest status produktu
            //sprawdź jaki czytnik zczytuje dane
            //jeśli czytnik różni się od czytnika wcześniejszego to zmień

            var id = product.TagId;
            product = _unitOfWork.ProductRepository.GetByTagId(id);
            var readerVersion = _readerService.GetReaderVersion();
            var reader = _readerService.Get(readerVersion);

            if (product == null || readerVersion == null)
            {
                return false;
            }
            else
            {
                product.Status = reader.Location;
                _productService.Update(product);
                return true;
            }

        }

        public void Delete(string id)
        {
            _unitOfWork.ProductRepository.Delete(id);
        }

        public Product Get(string id)
        {
            return _unitOfWork.ProductRepository.Get(id);
        }

        public IEnumerable<Product> GetAll()
        {
            return _unitOfWork.ProductRepository.GetAll();
        }

        public bool IsTagIdExist(string tagId)
        {
            var tag = _unitOfWork.ReaderRepository.GetByTagId(tagId);
            return true;
        }

        public void Update(Product product)
        {
            _unitOfWork.ProductRepository.Update(x => x.Id == product.Id, product);
        }
    }
}
