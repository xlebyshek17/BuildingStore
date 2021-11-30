using BuildingStore.Models.Entity;
using BuildingStore.Models.Repositories;
using BuildingStore.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Web;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using System.Net;
using System.Drawing;
using System.Drawing.Imaging;

namespace BuildingStore.Controllers
{
    public class ProductController : Controller
    {
        private readonly IWebHostEnvironment _environment;
        private readonly IRepository<Product> repo;

        public ProductController(IRepository<Product> r, IWebHostEnvironment environment)
        {
            repo = r;
            _environment = environment;
        }

        public IActionResult ViewProducts()
        {
            return View(repo.GetList());
        }

        public IActionResult ViewCatalog(int id, int userID, string userEmail)
        {
            ViewData["UserID"] = userID;
            ViewData["UserEmail"] = userEmail;

            if (id != 0)
                return View(repo.GetList().Where(p => p.CategoryID == id));

            return View(repo.GetList());
        }

        public IActionResult ProductDetails(int id, int userID, string email)
        {
            ViewData["UserID"] = userID;
            ViewData["UserEmail"] = email;

            Product product = repo.Get(id);

            if (product == null)
                return NotFound();
            return View(product);
        }

        public IActionResult CreateProduct()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CreateProduct(ProductViewModel model)
        {
            if (ModelState.IsValid)
            {
                string uniqueFileName = ProcessUploadedFile(model);
                Product product = new Product
                {
                    CategoryID = model.CategoryID,
                    Name = model.Name,
                    Price = model.Price,
                    Description = model.Description,
                    Image = uniqueFileName
                };

                repo.Create(product);
                return RedirectToAction("ViewProducts", "Product");
            }
            return View("CreateProduct");
        }

        public IActionResult EditProduct(int id)
        {
            if (id == 0)
                return NotFound();

            var product = repo.Get(id);

            if (product == null)
                return NotFound();

            else
            {
                var productViewModel = new ProductViewModel()
                {
                    CategoryID = product.CategoryID,
                    Name = product.Name,
                    Price = product.Price,
                    Description = product.Description,
                    ExistingImage = product.Image
                };

                return View(productViewModel);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult EditProduct(int id, ProductViewModel model)
        {
            if (ModelState.IsValid)
            {
                var product = repo.Get(id);
                product.CategoryID = model.CategoryID;
                product.Name = model.Name;
                product.Price = model.Price;
                product.Description = model.Description;

                if (model.ProductPicture != null)
                {
                    if (model.ExistingImage != null)
                    {
                        string filePath = Path.Combine(_environment.WebRootPath, "images", model.ExistingImage);
                        System.IO.File.Delete(filePath);
                    }

                    product.Image = ProcessUploadedFile(model);
                }

                repo.Update(product);
                return RedirectToAction("ViewProducts");
            }
            return View("EditProduct");
        }

        [HttpGet]
        [ActionName("DeleteProduct")]
        public IActionResult ConfirmDeleteProduct(int id)
        {
            if (id == 0)
            {
                return NotFound();
            }

            var product = repo.Get(id);

            if (product == null)
                return NotFound();

            else
            {
                var productViewModel = new ProductViewModel()
                {
                    ID = product.ID,
                    CategoryID = product.CategoryID,
                    Name = product.Name,
                    Price = product.Price,
                    Description = product.Description,
                    ExistingImage = product.Image
                };

                return View(productViewModel);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteProduct(int id)
        {
            var product = repo.Get(id);
            var CurrentImage = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\images", product.Image);
            repo.Delete(id);

            if (System.IO.File.Exists(CurrentImage))
            {
                System.IO.File.Delete(CurrentImage);
            }

            return RedirectToAction("ViewProducts");
        }

        [AcceptVerbs("Get", "Post")]
        public IActionResult CheckName(string name)
        {
            foreach (Product p in repo.GetList())
            {
                if (p.Name == name)
                    return Json(false);
            }
            return Json(true);
        }

        public string ProcessUploadedFile(ProductViewModel product)
        {
            string uniqueFileName = null;

            if (product.ProductPicture != null)
            {
                string uploadsFolder = Path.Combine(_environment.WebRootPath, "images");
                uniqueFileName = Guid.NewGuid().ToString() + "_" + product.ProductPicture.FileName;
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    product.ProductPicture.CopyTo(fileStream);
                }
            }

            return uniqueFileName;
        }
    }
}
