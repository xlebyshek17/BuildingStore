using BuildingStore.Models.Entity;
using BuildingStore.Models.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BuildingStore.Controllers
{
    public class CategoryController : Controller
    {
        private readonly IRepository<Category> repo;

        public CategoryController(IRepository<Category> r)
        {
            repo = r;
        }

        public IActionResult ViewCategory()
        {
            return View(repo.GetList());
        }

        public IActionResult CreateCategory()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CreateCategory(Category category)
        {
            if (ModelState.IsValid)
            {
                repo.Create(category);
                return RedirectToAction("ViewCategory");
            }
            return View("CreateCategory");
        }

        public IActionResult EditCategory(int id)
        {
            Category category = repo.Get(id);

            if (category == null)
                return NotFound();
            return View(category);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult EditCategory(Category category)
        {
            if (ModelState.IsValid)
            {
                repo.Update(category);
                return RedirectToAction("ViewCategory");
            }
            return View("EditCategory");
        }

        [HttpGet]
        [ActionName("DeleteCategory")]
        public IActionResult ConfirmDeleteCategory(int id)
        {
            Category category = repo.Get(id);

            if (category == null)
                return NotFound();

            return View(category);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteCategory(int id)
        {
            repo.Delete(id);
            return RedirectToAction("ViewCategory");
        }

        [AcceptVerbs("Get", "Post")]
        public IActionResult CheckCategory(string name)
        {
            foreach (Category c in repo.GetList())
            {
                if (c.Name == name)
                    return Json(false);
            }
            return Json(true);
        }
    }
}
