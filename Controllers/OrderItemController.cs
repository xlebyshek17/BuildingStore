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
    public class OrderItemController : Controller
    {
        private readonly IRepository<OrderItem> repo;

        public OrderItemController(IRepository<OrderItem> r)
        {
            repo = r;
        }
        
        public IActionResult ViewOrderItems(int id, int userID, int qty, string email)
        {
            ViewData["UserID"] = userID;
            ViewData["UserEmail"] = email;

            if (id != 0)
            {
                if (repo.GetList().FirstOrDefault(o => o.ProductID == id && o.UserID == userID && o.Status == false) == null)
                {
                    var item = new OrderItem()
                    {
                        UserID = userID,
                        ProductID = id,
                        Qty = 1,
                        Status = false
                    };

                    repo.Create(item);
                }
                else
                {
                    var item = ChangeQty(repo.GetList().Where(o => o.ProductID == id && o.UserID == userID 
                                                            && o.Status == false).FirstOrDefault(), qty);
                    repo.Update(item);
                }
            }
            return View(repo.GetList().Where(o => o.UserID == userID && o.Status == false));
        }

        public OrderItem ChangeQty(OrderItem item, int qty)
        {
            if (qty == 0)
                item.Qty += 1;
            else
                item.Qty = qty;
            return item;
        }

        public IActionResult DeleteOrderItem(int id, int userID, string email)
        {
            var item = repo.Get(id);

            if (item == null)
                return NotFound();
            
            repo.Delete(id);

            return RedirectToAction("ViewOrderItems", new { userID = userID, email = email });
        }

        public IActionResult UpdateOrderItem(int userID, string userEmail)
        {
            foreach(var item in repo.GetList().Where(o => o.UserID == userID && o.Status == false))
            {
                item.Status = true;
                repo.Update(item);
            }

            return RedirectToAction("ThankYou", "Home", new { userID = userID, userEmail = userEmail });
        }
    }
}
