using IGrad.Context;
using IGrad.Models;
using IGrad.Models.User;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Data.Entity.Validation;
using System.Linq;
using System.Web.Mvc;

namespace IGrad.Controllers
{
    public class AdminController : Controller
    {
        // GET: Admin
        public ActionResult Index(string sortOrder)
        {
            using (UserContext db = new UserContext())
            {
                var data = db.Users
                    .Include(u => u.Name)
                    .Include(u => u.BirthPlace)
                    .Include(u => u.ConsideredRaceAndEthnicity)
                    .Include(u => u.PhoneInfo)
                    .ToList();

                return View(data);
            }
        }

        public ActionResult Details(Guid? userID)
        {
            if(userID == null)
            {
                return RedirectToAction("Index", "Admin");
            }
            using (UserContext db = new UserContext())
            {
               var data = db.Users.Where(u => u.UserID == userID)
                    .Include(u => u.Name)
                    .Include(u => u.MailingAddress)
                    .Include(u => u.PhoneInfo).SingleOrDefault();

                UserModel model = (UserModel)data;

                return View(model);
            }
        }

        public ActionResult Download(Guid userID)
        {
            PDFFillerController pdfControl = new PDFFillerController();
            return pdfControl.FillPdf(userID);
        }
    }
}