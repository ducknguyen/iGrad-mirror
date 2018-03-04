﻿using IGrad.Context;
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
using System.IO;
using PagedList;

namespace IGrad.Controllers
{
    public class AdminController : Controller
    {
        // GET: Admin
        [Authorize]
        public ActionResult Index(string sortOrder, string currentFilter, string searchString, int? page)
        {
            Guid UserID = Guid.Parse(HttpContext.User.Identity.GetUserId());
            using (UserContext db = new UserContext())
            {
                var me = db.Users.Where(u => u.UserID == UserID).FirstOrDefault();

                if (me.role.isAdmin)
                {

                    ViewBag.CurrentSort = sortOrder;

                    if (searchString != null)
                    {
                        page = 1;
                    }
                    else
                    {
                        searchString = currentFilter;
                    }

                    ViewBag.CurrentFilter = searchString;


                    int pageSize = 10;
                    int pageNumber = (page ?? 1);

                    var data = db.Users
                        .Include(u => u.Name)
                        .Include(u => u.BirthPlace)
                        .Include(u => u.ConsideredRaceAndEthnicity)
                        .Include(u => u.PhoneInfo)
                        .Where(u => u.role.isAdmin == false)
                        .ToList();

                    if (!String.IsNullOrEmpty(searchString))
                    {
                        searchString = searchString.ToLower();
                        var _tempFname = data.Where(s => s.Name.FName != null).ToList();
                        var _tempLname = data.Where(s => s.Name.LName != null).ToList();

                        _tempFname = _tempFname.Where(s => s.Name.FName.ToLower().Contains(searchString)).ToList();
                        _tempLname = _tempFname.Where(s => s.Name.LName.ToLower().Contains(searchString)).ToList();

                        var _tempList = new List<UserModel>();
                        _tempList.AddRange(_tempFname);
                        _tempList.Union(_tempLname);

                        return View(_tempList.ToPagedList(pageNumber, pageSize));
                    }

                    return View(data.ToPagedList(pageNumber, pageSize));

                }
                else
                {
                    return View("Index", "Home");
                }
            }
        }

        [Authorize]
        public ActionResult Details(Guid? userID)
        {
            if (userID == null)
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

        [Authorize]
        public ActionResult Download(Guid userID)
        {
            PDFFillerController pdfControl = new PDFFillerController();
            return pdfControl.FillPdf(userID);
        }

        public ActionResult Drop()
        {
            string script = System.IO.File.ReadAllText(Server.MapPath(@"~/Content/drop.sql"));
            using (UserContext context = new UserContext())
            {
                
                for(int i = 0; i < 20; i++)
                {
                    try
                    {
                        context.Database.ExecuteSqlCommand(script);
                    }
                    catch(Exception e)
                    {
                        // do nothing
                    }
                }

                context.SaveChanges();
            }

            return View("Index", "Home");
        }
    }
}