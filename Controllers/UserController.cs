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
    public class UserController : Controller
    {
        private readonly IRepository<User> db;

        public UserController(IRepository<User> r)
        {
            db = r;
        }

        public ActionResult ViewUsers()
        {
            return View(db.GetList());
        }
    }
}
