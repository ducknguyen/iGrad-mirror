using System;
using System.Web.Mvc;
using IGrad.Models.User;
using IGrad.Context;
using System.Collections.Generic;

namespace IGrad.Controllers
{
    public class AdminController : Controller
    {
        // GET: Admin
        public ActionResult Index(string sortOrder)
        {
            List<UserModel> users = new List<UserModel>();
            UserContext context = new UserContext();
            foreach (var row in context.Users)
            {
                users.Add(row);
            }
            return View(users);
        }
    }
}