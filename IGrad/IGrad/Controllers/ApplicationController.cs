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
            UserModel _user;
            try
            {
                Guid UserID = Guid.Parse(HttpContext.User.Identity.GetUserId());
                UserContext db = new UserContext();
                _user = db.Users.Where(u => u.UserID == UserID)
                    .Include(u => u.LanguageHisory)
                    .FirstOrDefault();

                if(_user.LanguageHisory == null)
                {
                    _user.LanguageHisory = new LanguageHistory();
                }
            }
            catch (Exception ex)
            {
                return View();
            }
            return View(_user);

        }

        [HttpPost]
        public ActionResult GetLanguageForm(UserModel user)
        {
            return View();
        }

        public ActionResult SubmitLanguageInfo(UserModel user)
        {
            Guid UserID = Guid.Parse(HttpContext.User.Identity.GetUserId());
            using (UserContext db = new UserContext())
            {
                var data = db.Users
                       .Include(u => u.LanguageHisory)
                       .Where(u => u.UserID == UserID)
                       .FirstOrDefault<UserModel>();

                if (data.LanguageHisory == null)
                {
                    user.LanguageHisory.UserID = UserID;
                    data.LanguageHisory = user.LanguageHisory;
                }
                else
                {
                    db.Entry(data.LanguageHisory).CurrentValues.SetValues(user.LanguageHisory);
                }
                db.SaveChanges();
            }
                return GetEducationForm();
        }
        [Authorize]
        public ActionResult GetEducationForm()
        {
            UserModel _user;
            try
            {
                Guid UserID = Guid.Parse(HttpContext.User.Identity.GetUserId());
                UserContext db = new UserContext();
                _user = db.Users.Where(u => u.UserID == UserID)
                    .Include(u => u.SchoolInfo)
                    .Include(u => u.SchoolInfo.HighSchoolInformation)
                    .Include(u => u.SchoolInfo.PreviousSchoolViolation)
                    .FirstOrDefault();
            }
            catch (Exception ex)
            {
                return View();
            }
            if(_user.SchoolInfo == null)
            {
                _user.SchoolInfo = new SchoolInfo();
            }
            if(_user.SchoolInfo.HighSchoolInformation == null)
            {
                _user.SchoolInfo.HighSchoolInformation = new List<HighSchoolInfo>();
            }
            return View(_user);
        }

        [HttpPost]
        public ActionResult GetEducationForm(UserModel user)
        {
            return View();
        }

        public ActionResult AddHighSchoolInfoPartial()
        {
            return PartialView("~/Views/Application/_AddHighSchool.cshtml", new HighSchoolInfo());
        }

        public ActionResult GetHighSchoolInfoPartial(List<HighSchoolInfo> highSchoolInfoList)
        {
            return PartialView("~/Views/Application/_GetHighSchoolInfo.cshtml", highSchoolInfoList);
        }

        [HttpPost]
        public ActionResult SubmitHighSchoolInfoPartial(HighSchoolInfo highSchoolInfo)
        {
            Guid UserID = Guid.Parse(HttpContext.User.Identity.GetUserId());
            using (UserContext db = new UserContext())
            {
                var data = db.Users
                       .Include(u => u.SchoolInfo)
                       .Include(u => u.SchoolInfo.HighSchoolInformation)
                       .Where(u => u.UserID == UserID)
                       .FirstOrDefault<UserModel>();

                if (data.SchoolInfo == null)
                {
                    data.SchoolInfo = new SchoolInfo();
                    data.SchoolInfo.HighSchoolInformation = new List<HighSchoolInfo>();
                }
                data.SchoolInfo.UserID = UserID;
                highSchoolInfo.UserID = UserID;
                data.SchoolInfo.HighSchoolInformation.Add(highSchoolInfo);
                //db.Entry(data.SchoolInfo.HighSchoolInformation).CurrentValues.SetValues(highSchoolInfo);
                db.SaveChanges();

                return GetHighSchoolInfoPartial(data.SchoolInfo.HighSchoolInformation);
            }
        }

        public ActionResult GetAddViolation()
        {
            Violation defaultViolation = new Violation();
            defaultViolation.dateOfWeaponViolation = DateTime.Now;
            return PartialView("_AddViolationInfo", defaultViolation);
        }

        public ActionResult GetViolationInfo(Violation violation)
        {
            return PartialView("_GetViolationInfo", violation);
        }

        [HttpPost]
        public ActionResult SubmitViolationInfoPartial(Violation violation)
        {
            Guid UserID = Guid.Parse(HttpContext.User.Identity.GetUserId());
            using (UserContext db = new UserContext())
            {
                var data = db.Users
                       .Include(u => u.SchoolInfo)
                       .Include(u => u.SchoolInfo.PreviousSchoolViolation)
                       .Where(u => u.UserID == UserID)
                       .FirstOrDefault<UserModel>();

                if (data.SchoolInfo == null)
                {
                    data.SchoolInfo = new SchoolInfo();
                }
                if (data.SchoolInfo.PreviousSchoolViolation == null)
                {
                    data.SchoolInfo.PreviousSchoolViolation = violation;
                }

                data.SchoolInfo.UserID = UserID;
                data.SchoolInfo.PreviousSchoolViolation.UserID = UserID;

                //db.Entry(data.SchoolInfo.PreviousSchoolViolation).CurrentValues.SetValues(violation);
                db.SaveChanges();

                return GetViolationInfo(violation);
            }
        }
        [HttpPost]
        public void SubmitHighSchoolInfo(HighSchoolInfo highSchoolInfo, UserModel user)
        {
            //Add the submitted info to user
            user.SchoolInfo.HighSchoolInformation.Add(highSchoolInfo);
        }
        public ActionResult GetHouseholdForm()
        {
            UserModel _user;
            try
            {
                Guid UserID = Guid.Parse(HttpContext.User.Identity.GetUserId());
                UserContext db = new UserContext();
                _user = db.Users.Where(u => u.UserID == UserID)
                    .Include(u => u.Guardians)
                    .Include(u => u.Siblings)
                    .Include(u => u.LivesWith)
                    .Include(u => u.ResidentAddress)
                    .Include(u => u.MailingAddress)
                    .Include(u => u.EmergencyContact)
                    .FirstOrDefault();
            }
            catch (Exception ex)
            {
                return View();
            }

            if (_user.Guardians == null)
            {
                _user.Guardians = new List<Guardian>();
            }
            if (_user.Siblings == null)
            {
                _user.Siblings = new List<Sibling>();
            }
            if (_user.ResidentAddress == null)
            {
                _user.ResidentAddress = new Address();
            }
            if (_user.MailingAddress == null)
            {
                _user.MailingAddress = new Address();
            }
            if(_user.LivesWith == null)
            {
                _user.LivesWith = new LivesWith();
            }
            if(_user.EmergencyContact == null)
            {
                _user.EmergencyContact = new EmergencyContact();
                _user.EmergencyContact.UserID = _user.UserID;
                _user.EmergencyContact.Name = new Name();
                _user.EmergencyContact.PhoneNumber = new Phone();
            }
            return View(_user);
        }

        public ActionResult GetAddGuardian()
        {
            Guardian defaultGuardian = new Guardian();
            defaultGuardian.Phone = new Phone();
            defaultGuardian.Name = new Name();
            return PartialView("_AddGuardian", defaultGuardian);
        }
        public void SubmitGuardianInfo(Guardian guardian)
        {
            Guid UserID = Guid.Parse(HttpContext.User.Identity.GetUserId());
            using (UserContext db = new UserContext())
            {
                var data = db.Users
                       .Include(u => u.Guardians)
                       .Where(u => u.UserID == UserID)
                       .FirstOrDefault<UserModel>();

                if (data.Guardians == null)
                {
                    guardian.UserID = UserID;
                    data.Guardians = new List<Guardian>();
                    data.Guardians.Add(guardian);
                }

                //db.Entry(data.SchoolInfo.PreviousSchoolViolation).CurrentValues.SetValues(violation);
                db.SaveChanges();
            }
        }

        public ActionResult GetAddSibling()
        {
            Sibling sibling = new Sibling();
            return PartialView("_AddSibling", sibling);
        }

        public void SubmitAddSiblingInfo(Sibling sibling)
        {
            Guid UserID = Guid.Parse(HttpContext.User.Identity.GetUserId());
            using (UserContext db = new UserContext())
            {
                var data = db.Users
                       .Include(u => u.Siblings)
                       .Where(u => u.UserID == UserID)
                       .FirstOrDefault<UserModel>();

                if (data.Siblings == null)
                {
                    sibling.UserID = UserID;
                    data.Siblings = new List<Sibling>();
                    data.Siblings.Add(sibling);
                }
                //db.Entry(data.SchoolInfo.PreviousSchoolViolation).CurrentValues.SetValues(violation);
                db.SaveChanges();
            }
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

                // Add gender
                data.Gender = user.Gender;

                #region Update Last Updated Date
                data.LastUpdateDate = DateTime.Now;
                #endregion

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
            PDFFillerController pdfControl = new PDFFillerController();
            return pdfControl.FillPdf(UserID);
            //return RedirectToAction("GetLanguageForm", "Application");
        }
    }
}