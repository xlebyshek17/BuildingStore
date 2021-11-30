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
    public class OrderController : Controller
    {
        private readonly IRepository<Order> repo;

        public OrderController(IRepository<Order> r)
        {
            repo = r;
        }

        public IActionResult ViewOrders()
        {
            return View(repo.GetList());
        }

        public ActionResult CreateOrder(int userID, string email)
        {
            ViewData["UserID"] = userID;
            ViewData["UserEmail"] = email;

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CreateOrder(Order order, int userID, string email)
        {
            ViewData["UserID"] = userID;
            ViewData["UserEmail"] = email;

            if (ModelState.IsValid)
            {
                repo.Create(order);

                int orderID = repo.GetList().LastOrDefault(o => o.UserID == userID).ID;
                return RedirectToAction("CreateOrderDetail", "OrderDetail", new { orderID = orderID, userID = userID, userEmail = email });
            }

            return View("CreateOrder");
        }
    }
}
