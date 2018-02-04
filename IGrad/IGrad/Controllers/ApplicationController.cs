using IGrad.Context;
using IGrad.Models.User;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Web;
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

            return RedirectToAction("Page2", "Application");
        }

        [Authorize]
        public ActionResult Page2()
        {
            return View();
        }

        [Authorize]
        public ActionResult GetNewApplication()
        {
            return View();
        }
    }
}