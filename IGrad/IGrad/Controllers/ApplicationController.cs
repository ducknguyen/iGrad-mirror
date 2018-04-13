using IGrad.Context;
using IGrad.Models.User;
using IGrad.Models.User.NativeAmerican;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
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
        public ActionResult GetNewApplication()
        {
            UserModel _user;
            try
            {
                Guid UserID = Guid.Parse(HttpContext.User.Identity.GetUserId());
                UserContext db = new UserContext();
                _user = db.Users.Where(u => u.UserID == UserID)
                    .Include(u => u.Name)
                    .Include(u => u.BirthPlace)
                    .Include(u => u.ConsideredRaceAndEthnicity)
                    .Include(u => u.PhoneInfo)
                    .FirstOrDefault<UserModel>();
            }
            catch (Exception ex)
            {
                return View();
            }
            if (_user.BirthPlace == null)
            {
                _user.BirthPlace = new BirthPlaceLocation();
            }
            return View(_user);
        }

        [HttpPost]
        public ActionResult GetNewApplication(UserModel user)
        {
            Guid UserID = Guid.Parse(HttpContext.User.Identity.GetUserId());
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

                data.Birthday = user.Birthday;

                db.SaveChanges();
            }

            //PDFFillerController pdfControl = new PDFFillerController();
            //return pdfControl.FillPdf(UserID);
            return RedirectToAction("GetLanguageForm", "Application");
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
                    .Include(u => u.QualifiedOrEnrolledInProgam)
                    .Include(u => u.SchoolInfo.HighSchoolInformation)
                    .Include(u => u.SchoolInfo.PreviousSchoolViolation)
                    .FirstOrDefault();
            }
            catch (Exception ex)
            {
                return View();
            }
            if (_user.SchoolInfo == null)
            {
                _user.SchoolInfo = new SchoolInfo();
            }
            if (_user.SchoolInfo.HighSchoolInformation == null)
            {
                _user.SchoolInfo.HighSchoolInformation = new List<HighSchoolInfo>();
            }
            return View(_user);
        }

        [HttpPost]
        public ActionResult GetEducationForm(UserModel user)
        {
            Guid UserID = Guid.Parse(HttpContext.User.Identity.GetUserId());
            using (UserContext db = new UserContext())
            {
                var data = db.Users
                       .Include(u => u.SchoolInfo)
                       .Include(u => u.QualifiedOrEnrolledInProgam)
                       .Include(u => u.SchoolInfo.PriorEducation)
                       .Where(u => u.UserID == UserID)
                       .FirstOrDefault<UserModel>();
                if(data.SchoolInfo == null)
                {
                    data.SchoolInfo = new SchoolInfo();
                }
                if(data.QualifiedOrEnrolledInProgam == null)
                {
                    data.QualifiedOrEnrolledInProgam = new QualifiedOrEnrolledInProgram();
                }
                //set IDs for Entities
                user.UserID = UserID;
                user.QualifiedOrEnrolledInProgam.UserID = UserID;
                //Set object reference to entity with UserID
                data.QualifiedOrEnrolledInProgam = user.QualifiedOrEnrolledInProgam;
                data.SchoolInfo = user.SchoolInfo;

                user.SchoolInfo.PriorEducation.UserID = UserID;
                data.SchoolInfo.PriorEducation = user.SchoolInfo.PriorEducation;
                

                db.SaveChanges();
            }

            return RedirectToAction("GetHouseholdForm","Application");
        }

        public ActionResult AddHighSchoolInfoPartial()
        {
            return PartialView("~/Views/Application/_AddHighSchool.cshtml", new HighSchoolInfo());
        }

        public ActionResult GetHighSchoolInfo(List<HighSchoolInfo> highSchoolInfoList)
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
                db.SaveChanges();

                return GetHighSchoolInfo(data.SchoolInfo.HighSchoolInformation);
            }
        }

        [HttpPost]
        public void DeleteHighSchool(string fieldId)
        {
            HighSchoolInfo hsi = new HighSchoolInfo();
            hsi.fieldId = Int32.Parse(fieldId);
            using (UserContext db = new UserContext())
            {
                db.HighSchoolInfoes.Attach(hsi);
                db.HighSchoolInfoes.Remove(hsi);
                db.SaveChanges();
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

                db.SaveChanges();

                return GetViolationInfo(violation);
            }
        }
        [HttpPost]
        public void DeleteViolation()
        {
            Guid UserID = Guid.Parse(HttpContext.User.Identity.GetUserId());
            using (UserContext db = new UserContext())
            {
                var data = db.Users
                      .Include(u => u.SchoolInfo)
                      .Include(u => u.SchoolInfo.PreviousSchoolViolation)
                      .Where(u => u.UserID == UserID)
                      .FirstOrDefault<UserModel>();

                data.SchoolInfo.PreviousSchoolViolation = null;
                db.SaveChanges();
            }
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

                if (_user.LanguageHisory == null)
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
                    data.LanguageHisory = user.LanguageHisory;
                }
                db.SaveChanges();
            }
            return RedirectToAction("GetEducationForm", "Application");
        }

        [Authorize]
        public ActionResult GetHouseholdForm()
        {
            UserModel _user;
            try
            {
                Guid UserID = Guid.Parse(HttpContext.User.Identity.GetUserId());
                UserContext db = new UserContext();
                _user = db.Users.Where(u => u.UserID == UserID)
                    .Include(u => u.Guardians)
                    .Include(u => u.Guardians.Select(n => n.Name))
                    .Include(u => u.Guardians.Select(p => p.Phone))
                    .Include(u => u.Siblings)
                    .Include(u => u.LivesWith)
                    .Include(u => u.ResidentAddress)
                    .Include(u => u.MailingAddress)
                    .Include(u => u.EmergencyContacts)
                    .Include(u => u.EmergencyContacts.Select(n => n.Name))
                    .Include(u => u.EmergencyContacts.Select(p => p.PhoneNumber))
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
            if (_user.LivesWith == null)
            {
                _user.LivesWith = new LivesWith();
            }
            if (_user.EmergencyContacts == null)
            {
                _user.EmergencyContacts = new List<EmergencyContact>();
            }
            return View(_user);
        }
        [HttpPost]
        public ActionResult GetHouseholdForm(UserModel user)
        {
            Guid UserID = Guid.Parse(HttpContext.User.Identity.GetUserId());
            using (UserContext db = new UserContext())
            {
                var data = db.Users
                       .Include(u => u.LivesWith)
                       .Include(u => u.ResidentAddress)
                       .Include(u => u.MailingAddress)
                       .Include(u => u.EmergencyContacts)
                       .Where(u => u.UserID == UserID)
                       .FirstOrDefault<UserModel>();

                //set userID of objects
                user.LivesWith.UserID = UserID;
                user.MailingAddress.UserID = UserID;
                user.ResidentAddress.UserID = UserID;
                //assign entity the values submitted by form
                data.MailingAddress = user.MailingAddress;
                data.ResidentAddress = user.ResidentAddress;
                data.LivesWith = user.LivesWith;

                db.SaveChanges();
            }
            return RedirectToAction("GetHealthForm", "Application");
        }

        public ActionResult GetAddEmergencyContact()
        {
            EmergencyContact contact = new EmergencyContact();
            contact.Name = new Name();
            contact.PhoneNumber = new Phone();
            return PartialView("_AddEmergencyContact", contact);
        }
        [HttpPost]
        public ActionResult SubmitEmergencyContact(EmergencyContact contact)
        {
            Guid UserID = Guid.Parse(HttpContext.User.Identity.GetUserId());
            using (UserContext db = new UserContext())
            {

                var data = db.Users
                       .Include(u => u.EmergencyContacts.Select(n => n.Name))
                       .Include(u => u.EmergencyContacts.Select(p => p.PhoneNumber))
                       .Where(u => u.UserID == UserID)
                       .FirstOrDefault<UserModel>();

                if (data.EmergencyContacts == null)
                {
                    data.EmergencyContacts = new List<EmergencyContact>();
                }

                contact.UserID = UserID;
                contact.Name.UserID = UserID;
                contact.PhoneNumber.UserID = UserID;
                data.EmergencyContacts.Add(contact);

                db.SaveChanges();

                return GetEmergencyContactsPartial(data.EmergencyContacts);
            }
        }

        public ActionResult GetEmergencyContactsPartial(List<EmergencyContact> emergencyContactList)
        {
            if (emergencyContactList == null)
            {
                emergencyContactList = new List<EmergencyContact>();
            }
            return PartialView("_GetEmergencyContacts", emergencyContactList);
        }
        [HttpPost]
        public void DeleteEmergencyContact(string fieldId)
        {
            EmergencyContact contact = new EmergencyContact();
            contact.fieldId = Int32.Parse(fieldId);
            using (UserContext db = new UserContext())
            {
                db.EmergencyContacts.Attach(contact);
                db.EmergencyContacts.Remove(contact);
                db.SaveChanges();
            }
        }

        public ActionResult GetAddGuardian()
        {
            Guardian defaultGuardian = new Guardian();
            defaultGuardian.Phone = new Phone();
            defaultGuardian.Name = new Name();
            return PartialView("_AddGuardian", defaultGuardian);
        }
        
        [HttpPost]
        public ActionResult SubmitGuardianInfo(Guardian guardian)
        {
            Guid UserID = Guid.Parse(HttpContext.User.Identity.GetUserId());
            using (UserContext db = new UserContext())
            {
                var data = db.Users
                       .Include(u => u.Guardians)
                       .Include(u => u.Guardians.Select(n => n.Name))
                       .Include(u => u.Guardians.Select(p => p.Phone))
                       .Where(u => u.UserID == UserID)
                       .FirstOrDefault<UserModel>();

                if (data.Guardians == null)
                {
                    data.Guardians = new List<Guardian>();
                }

                guardian.UserID = UserID;
                data.Guardians.Add(guardian);

                db.SaveChanges();

                return PartialView("_GetGuardianInfo", data.Guardians);
            }

        }

        public ActionResult GetGuardiansPartial(List<Guardian> guardianList)
        {
            if (guardianList == null)
            {
                guardianList = new List<Guardian>();
            }
            return PartialView("_GetGuardianInfo", guardianList);
        }
        [HttpPost]
        public void DeleteGuardian(string fieldId)
        {
            Guardian guardian = new Guardian();
            guardian.fieldId = Int32.Parse(fieldId);
            using(UserContext db = new UserContext())
            {
                db.Guardians.Attach(guardian);
                db.Guardians.Remove(guardian);
                db.SaveChanges();
            }
        }

        public ActionResult GetAddSibling()
        {
            Sibling sibling = new Sibling();
            return PartialView("_AddSibling", sibling);
        }

        [HttpPost]
        public ActionResult SubmitAddSiblingInfo(Sibling sibling)
        {
            Guid UserID = Guid.Parse(HttpContext.User.Identity.GetUserId());
            using (UserContext db = new UserContext())
            {
                //get user
                var data = db.Users
                       .Include(u => u.Siblings)
                       .Where(u => u.UserID == UserID)
                       .FirstOrDefault<UserModel>();
                //check null sibling list
                if (data.Siblings == null)
                {
                    data.Siblings = new List<Sibling>();
                }

                //set ID and add sibling
                sibling.UserID = UserID;
                data.Siblings.Add(sibling);
                //save
                db.SaveChanges();

                return PartialView("_GetSiblingInfo", data.Siblings);
            }
        }

        public ActionResult GetSiblingsPartial(List<Sibling> siblingList)
        {
            if (siblingList == null)
            {
                siblingList = new List<Sibling>();
            }
            return PartialView("_GetSiblingInfo", siblingList);
        }

        [HttpPost]
        public void DeleteSibling(string fieldId)
        {
            Sibling sibling = new Sibling();
            sibling.fieldId = Int32.Parse(fieldId);
            using(UserContext db = new UserContext())
            {
                db.Siblings.Attach(sibling);
                db.Siblings.Remove(sibling);
                db.SaveChanges();
            }
        }

        [Authorize]
        public ActionResult GetHealthForm()
        {
            Guid UserID = Guid.Parse(HttpContext.User.Identity.GetUserId());
            using (UserContext db = new UserContext())
            {

                var data = db.Users
                       .Include(u => u.HealthInfo)
                       .Where(u => u.UserID == UserID)
                       .FirstOrDefault<UserModel>();

                if (data.HealthInfo == null)
                {
                    data.HealthInfo = new Health();
                }
                return View(data.HealthInfo);
            }
        }

        public ActionResult GetAddHealthInfo()
        {
            Health defaultHealth = new Health();
            return PartialView("_AddHealthInfo", defaultHealth);
        }

        public void SubmitHealthInfo(Health health)
        {
            Guid UserID = Guid.Parse(HttpContext.User.Identity.GetUserId());
            using (UserContext db = new UserContext())
            {
                //get user
                var data = db.Users
                       .Include(u => u.HealthInfo)
                       .Where(u => u.UserID == UserID)
                       .FirstOrDefault<UserModel>();

                //set health info
                data.HealthInfo = health;
                data.HealthInfo.UserID = UserID;
                //save
                db.SaveChanges();
            }
        }

        [HttpPost]
        public ActionResult GetHealthForm(Health health)
        {
            Guid UserID = Guid.Parse(HttpContext.User.Identity.GetUserId());
            using (UserContext db = new UserContext())
            {
                var data = db.Users
                       .Include(u => u.HealthInfo)
                       .Where(u => u.UserID == UserID)
                       .FirstOrDefault<UserModel>();

                //set userID of objects
                health.UserID = UserID;

                data.HealthInfo = health;

                db.SaveChanges();
            }
            return RedirectToAction("GetOtherInfoForm","Application");
        }

        [Authorize]
        public ActionResult GetOtherInfoForm()
        {
            Guid UserID = Guid.Parse(HttpContext.User.Identity.GetUserId());
            using (UserContext db = new UserContext())
            {
                //get user
                var data = db.Users
                       .Where(u => u.UserID == UserID)
                       .FirstOrDefault<UserModel>();
                return View(data);
            }

        }

        [HttpPost]
        public ActionResult GetOtherInfoForm(UserModel user)
        {
            return View();
        }

        public ActionResult GetFilledPDF()
        {
            Guid UserID = Guid.Parse(HttpContext.User.Identity.GetUserId());
            PDFFillerController pdfcontrol = new PDFFillerController();
            return pdfcontrol.FillPdf(UserID);
        }

        public ActionResult GetNativeAmericanEducationForm(UserModel user)
        {
            if(user.NativeAmericanEducation != null)
            {
                return PartialView("_GetNativeAmericanEducation", user.NativeAmericanEducation);
            }
            else
            {
                user.NativeAmericanEducation = new NativeAmericanEducation();
                return PartialView("_GetNativeAmericanEducation", user.NativeAmericanEducation);
            }
        }

        [HttpPost]
        public ActionResult SubmitNativeAmericanEducationPartial(NativeAmericanEducation ed)
        {
            Guid UserID = Guid.Parse(HttpContext.User.Identity.GetUserId());
            using (UserContext db = new UserContext())
            {
                //get user
                var data = db.Users
                       .Include(u => u.NativeAmericanEducation)
                       //add select for native American address
                       //.Include(u => u.NativeAmericanEducation.Select(n => n.Address))
                       .Where(u => u.UserID == UserID)
                       .FirstOrDefault<UserModel>();

                ed.UserID = UserID;
                data.NativeAmericanEducation = ed;

                //save
                db.SaveChanges();
            }

            return null;
        }
    }
}