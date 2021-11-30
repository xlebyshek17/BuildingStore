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
    public class OrderDetailController : Controller
    {
        private readonly IRepository<OrderDetail> repo;

        public OrderDetailController(IRepository<OrderDetail> r)
        {
            repo = r;
        }

        public IActionResult ViewOrderDetails()
        {
            return View(repo.GetList());
        }

        public IActionResult ViewDetails(int id)
        {
            if (id == 0)
                return NotFound();

            return View(repo.GetList().Where(o => o.OrderID == id));
        }

        public IActionResult CreateOrderDetail(int userID, string userEmail, int orderID)
        {
            ViewData["OrderID"] = orderID;
            ViewData["UserID"] = userID;
            ViewData["UserEmail"] = userEmail;

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateOrderDetail(OrderDetail orderDetail, int orderID, int userID, string userEmail)
        {
            ViewData["OrderID"] = orderID;
            ViewData["UserID"] = userID;
            ViewData["UserEmail"] = userEmail;

            if (ModelState.IsValid)
            {
                foreach (OrderItem item in orderDetail.OrderItems)
                {
                    orderDetail.OrderItemID = item.ID;
                    repo.Create(orderDetail);
                }

                return RedirectToAction("UpdateOrderItem", "OrderItem", new { userID = userID, userEmail = userEmail });
            }

            return View("CreateOrderDetail");
        }
    }
}
