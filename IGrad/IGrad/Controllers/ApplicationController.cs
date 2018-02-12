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
    public class ApplicationController : Controller
    {
        // GET: Application
        [Authorize]
        public ActionResult Index()
        {
            return View();
        }

        [Authorize]
        [HttpPost]
        public ActionResult Index(UserModel user)
        {
            // this would be a new user record database if not logged in
            // this will not update a existing user
            using (UserContext db = new UserContext())
            {
                // Need to create the association between database; 
                // Should create this on user registration which should be first form!
                user.UserID = Guid.NewGuid();
                //user.Birthday = new Date();
                //user.Birthday.UserID = user.UserID;
               // user.BirthPlace.UserID = user.UserID;
                //user.ConsideredRaceAndEthnicity.UserID = user.UserID;
                //user.HealthInfo.UserID = user.UserID;
                //user.EmergencyContacts[0].UserID = user.UserID;
                //user.Guardians[0].UserID = user.UserID;
                //user.HealthInfo.UserID = user.UserID;
                //user.HighSchoolInfo.HighSchoolInformation[0].UserID = user.UserID;
                //user.LivesWith.UserID = user.UserID;
                //user.LoginInfo.UserID = user.UserID;
                //user.MailingAddress.UserID = user.UserID;
                //user.Name.UserID = user.UserID;
                //user.PhoneInfo.UserID = user.UserID;
                //user.PreSchool[0].UserID = user.UserID;
                //user.QualifiedOrEnrolledProgam[0].UserID = user.UserID;
                //user.ResidentAddress.UserID = user.UserID;
                //user.Retainment.UserID = user.UserID;
                //user.Siblings[0].UserID = user.UserID;
                //user.StudentChildCare.UserID = user.UserID;
                //user.StudentsParentingPlan.UserID = user.UserID;

                // now add the form info
                db.Users.Add(user);
                try
                {
                    db.SaveChanges();
                }
                catch (DbEntityValidationException ex)
                {
                    foreach (var entityValidationErrors in ex.EntityValidationErrors)
                    {
                        foreach (var validationError in entityValidationErrors.ValidationErrors)
                        {
                            Response.Write("Property: " + validationError.PropertyName + " Error: " + validationError.ErrorMessage);
                        }
                    }
                }
            }

            return RedirectToAction("GetLanguageForm", "Application");
        }

        [Authorize]
        public ActionResult GetLanguageForm()
        {
            return View();
        }

        [HttpPost]
        public ActionResult GetLanguageForm(UserModel user)
        {
            return View();
        }

        public ActionResult GetEducationForm()
        {
            UserModel user = new UserModel();
            user.SchoolInfo = new SchoolInfo();
            user.SchoolInfo.HighSchoolInformation = new List<HighSchoolInfo>();
            HighSchoolInfo hsi = new HighSchoolInfo();
            hsi.HighSchoolCity = "Auburn";
            hsi.HighSchoolGrade = 10;
            hsi.HighSchoolName = "Fake High School";
            hsi.isLastHighSchoolAttended = true;
            hsi.HighSchoolYear = "2016";
            user.SchoolInfo.HighSchoolInformation.Add(hsi);
            return View(user);
        }


        [HttpPost]
        public ActionResult GetEducationForm(UserModel user)
        {
            return View();
        }

        public ActionResult GetHighSchoolInfoPartial(UserModel user)
        {
            return PartialView("~/Views/Application/_GetHighSchoolInfo.cshtml", new HighSchoolInfo());
        }

        [HttpPost]
        public void SubmitHighSchoolInfo(HighSchoolInfo highSchoolInfo, UserModel user)
        {
            //Add the submitted info to user
            user.SchoolInfo.HighSchoolInformation.Add(highSchoolInfo);
        }
        public ActionResult GetHouseholdForm()
        {
            return View();
        }

        [HttpPost]
        public ActionResult GetHouseHoldForm(UserModel user)
        {
            return View();
        }

        public ActionResult GetHealthForm()
        {
            return View();
        }
        
        [HttpPost]
        public ActionResult GetHealthForm(UserModel user)
        {
            return View();
        }

        public ActionResult GetOtherInfoForm()
        {
            return View();
        }

        [HttpPost]
        public ActionResult GetOtherInfoForm(UserModel user)
        {
            return View();
        }

        
        [Authorize]
        public ActionResult GetNewApplication()
        {
            UserModel _user;
            try
            {
                string userid = HttpContext.User.Identity.GetUserId();
                Guid UserID = Guid.Parse(userid);
                UserContext db = new UserContext();
                _user = db.Users.Where(u => u.UserID == UserID)
                    .Include(u => u.Name)
                    .Include(u => u.BirthPlace)
                    .Include(u => u.ConsideredRaceAndEthnicity)
                    .Include(u => u.PhoneInfo)
                    .FirstOrDefault();
            }
            catch (Exception ex)
            {
                return View();
            }
            return View(_user);
        }

        [HttpPost]
        public ActionResult GetNewApplication(UserModel user)
        {
            string userid = HttpContext.User.Identity.GetUserId();
            Guid UserID = Guid.Parse(userid);
            user.UserID = UserID;
            using (UserContext db = new UserContext())
            {
                var data = db.Users
                       .Include(u => u.Name)
                       .Include(u => u.BirthPlace)
                       .Include(u => u.ConsideredRaceAndEthnicity)
                       .Include(u => u.PhoneInfo)
                       .Where(u => u.UserID == user.UserID)
                       .FirstOrDefault<UserModel>();

                #region updateName
                if (data.Name != null)
                {
                    // name already exists so lets update it!
                    user.Name.fieldId = data.Name.fieldId;
                    db.Entry(data.Name).CurrentValues.SetValues(user.Name);
                }
                // does not exist so lets add it :)
                else
                {
                    data.Name = user.Name;
                }
                // make sure user id is set for child
                data.Name.UserID = user.UserID;
                #endregion

                #region updateBirthPlace
                if (data.BirthPlace != null)
                {
                    user.BirthPlace.fieldId = data.BirthPlace.fieldId;
                    db.Entry(data.BirthPlace)
                        .CurrentValues
                        .SetValues(user.BirthPlace);
                }
                else
                {
                    data.BirthPlace = user.BirthPlace;
                }
                // make sure user id is set for child
                data.BirthPlace.UserID = user.UserID;
                #endregion

                #region updateConsideredRace
                if (data.ConsideredRaceAndEthnicity != null)
                {
                    user.ConsideredRaceAndEthnicity.fieldId = data.ConsideredRaceAndEthnicity.fieldId;
                    db.Entry(data.ConsideredRaceAndEthnicity)
                        .CurrentValues
                        .SetValues(user.ConsideredRaceAndEthnicity);
                }
                else
                {
                    data.ConsideredRaceAndEthnicity = user.ConsideredRaceAndEthnicity;
                }

                // make sure user id is set for child
                data.ConsideredRaceAndEthnicity.UserID = user.UserID;
                #endregion

                #region updatePhoneNumber

                if (data.PhoneInfo != null)
                {
                    user.PhoneInfo.fieldId = data.PhoneInfo.fieldId;
                    db.Entry(data.PhoneInfo)
                        .CurrentValues
                        .SetValues(user.PhoneInfo);
                }
                else
                {
                    data.PhoneInfo = user.PhoneInfo;
                }
                // make sure user id is set for child
                data.PhoneInfo.UserID = user.UserID;
                #endregion

                db.SaveChanges();
            }
            return RedirectToAction("GetLanguageForm", "Application");
        }
    }
}