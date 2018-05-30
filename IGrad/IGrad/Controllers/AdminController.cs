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
using System.IO;
using PagedList;
using IGrad.Models.Income;
using IGrad.Models.OpenHouse;

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

                    #region Number of Late Applications
                    // show a number of apps late! 
                    DateTime oldApp = DateTime.Now.AddDays(-7);

                    var lateApps = db.Users
                        .Where(u => u.role.isAdmin == false)
                        .Where(u => u.LastUpdateDate <= oldApp)
                        .ToList();

                    if (lateApps != null)
                    {
                        ViewBag.LateAppCount = lateApps.Count();
                    }
                    else
                    {
                        ViewBag.LateAppCount = 0;
                    }
                    #endregion

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
        public ActionResult LateApps(string sortOrder, string currentFilter, string searchString, int? page)
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

                    DateTime oldApp = DateTime.Now.AddDays(-7);

                    var data = db.Users
                        .Include(u => u.Name)
                        .Include(u => u.BirthPlace)
                        .Include(u => u.ConsideredRaceAndEthnicity)
                        .Include(u => u.PhoneInfo)
                        .Where(u => u.role.isAdmin == false)
                        .Where(u => u.LastUpdateDate <= oldApp)
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
        public ActionResult AddAdmin()
        {
            return View();
        }

        [Authorize]
        public ActionResult AddImmunization()
        {
            return View();
        }

        [HttpPost]
        [Authorize]
        public ActionResult AddImmunizationPDF()
        {
            /* Code to save save model data into database. */

            foreach (string upload in Request.Files)
            {
                if (Request.Files[upload].ContentLength == 0) continue;
                string pathToSave = Server.MapPath("~/media/documents/");
                string filename = "ImmunizationStatus.pdf";
                Request.Files[upload].SaveAs(Path.Combine(pathToSave, filename));
            }
            return RedirectToAction("Index", "Admin");
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

        [Authorize]
        public ActionResult FamilyIncome()
        {
            using (IncomeContext ic = new IncomeContext())
            {
                var data = ic.Income
                    .Include(m => m.incomeTable)
                    .FirstOrDefault();

                return View(data);
            }
        }

        [Authorize]
        public ActionResult OpenHouse()
        {
            using (OpenHouseContext ohc = new OpenHouseContext())
            {
                int idToUse = ohc.OpenHouse.Select(m => m.id).Max(); // get latest if needed
                var data = ohc.OpenHouse.Where(m => m.id == idToUse).FirstOrDefault();
                return View(data);
            }
        }

        [Authorize]
        [HttpPost]
        public ActionResult FinalPage(OpenHouse oh)
        {
            using (OpenHouseContext ohc = new OpenHouseContext())
            {
                int idToUse = ohc.OpenHouse.Select(m => m.id).Max(); // get latest if needed
                var data = ohc.OpenHouse.Where(m => m.id == idToUse).FirstOrDefault();


                data.Announcements = oh.Announcements;
                data.OpenDays = oh.OpenDays;
                data.WhatToBring = oh.WhatToBring;
                ohc.SaveChanges();
            }
            return RedirectToAction("Index", "Admin");
        }

        [Authorize]
        [HttpPost]
        public ActionResult FamilyIncome(FamilyIncome income)
        {
            using (IncomeContext ic = new IncomeContext())
            {
                var data = ic.Income
                    .Include(m => m.incomeTable)
                    .FirstOrDefault();
                
                data.EffectiveDates = income.EffectiveDates;
                data.IncomeTableYears = income.IncomeTableYears;

                for(int i = 0; i < data.incomeTable.Count; i++)
                {
                    data.incomeTable[i].Monthly = income.incomeTable[i].Monthly;
                    data.incomeTable[i].TwiceMonthly = income.incomeTable[i].TwiceMonthly;
                    data.incomeTable[i].TwoWeeks = income.incomeTable[i].TwoWeeks;
                    data.incomeTable[i].Weekly = income.incomeTable[i].Weekly;
                    data.incomeTable[i].Annually = income.incomeTable[i].Annually;
                }

                ic.SaveChanges();
            }
            return RedirectToAction("Index", "Admin");
        }
    }
}