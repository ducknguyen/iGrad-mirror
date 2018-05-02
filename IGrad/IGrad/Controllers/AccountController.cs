using System;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using IGrad.Models;
using IGrad.Context;
using IGrad.Models.User;
using System.Data.Entity.Validation;
using System.Collections.Generic;
using Microsoft.AspNet.Identity.EntityFramework;
using IGrad.Models.Income;
using System.Data.Entity;
using System.Xml;

namespace IGrad.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        private ApplicationSignInManager _signInManager;
        private ApplicationUserManager _userManager;

        public AccountController()
        {
        }

        public AccountController(ApplicationUserManager userManager, ApplicationSignInManager signInManager)
        {
            UserManager = userManager;
            SignInManager = signInManager;
        }

        public ApplicationSignInManager SignInManager
        {
            get
            {
                return _signInManager ?? HttpContext.GetOwinContext().Get<ApplicationSignInManager>();
            }
            private set
            {
                _signInManager = value;
            }
        }

        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }

        //
        // GET: /Account/Login
        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        //
        // POST: /Account/Login
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(LoginViewModel model, string returnUrl)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            // This doesn't count login failures towards account lockout
            // To enable password failures to trigger account lockout, change to shouldLockout: true
            var result = await SignInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, shouldLockout: false);
            switch (result)
            {
                case SignInStatus.Success:
                    return RedirectToLocal(returnUrl);
                case SignInStatus.LockedOut:
                    return View("Lockout");
                case SignInStatus.RequiresVerification:
                    return RedirectToAction("SendCode", new { ReturnUrl = returnUrl, RememberMe = model.RememberMe });
                case SignInStatus.Failure:
                default:
                    ModelState.AddModelError("", "Invalid login attempt.");
                    return View(model);
            }
        }

        //
        // GET: /Account/VerifyCode
        [AllowAnonymous]
        public async Task<ActionResult> VerifyCode(string provider, string returnUrl, bool rememberMe)
        {
            // Require that the user has already logged in via username/password or external login
            if (!await SignInManager.HasBeenVerifiedAsync())
            {
                return View("Error");
            }
            return View(new VerifyCodeViewModel { Provider = provider, ReturnUrl = returnUrl, RememberMe = rememberMe });
        }

        //
        // POST: /Account/VerifyCode
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> VerifyCode(VerifyCodeViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            // The following code protects for brute force attacks against the two factor codes. 
            // If a user enters incorrect codes for a specified amount of time then the user account 
            // will be locked out for a specified amount of time. 
            // You can configure the account lockout settings in IdentityConfig
            var result = await SignInManager.TwoFactorSignInAsync(model.Provider, model.Code, isPersistent: model.RememberMe, rememberBrowser: model.RememberBrowser);
            switch (result)
            {
                case SignInStatus.Success:
                    return RedirectToLocal(model.ReturnUrl);
                case SignInStatus.LockedOut:
                    return View("Lockout");
                case SignInStatus.Failure:
                default:
                    ModelState.AddModelError("", "Invalid code.");
                    return View(model);
            }
        }

        //
        // GET: /Account/Register
        [AllowAnonymous]
        public ActionResult Register()
        {
            return View();
        }


        //
        // POST: /Account/Register
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public void RegisterDefaultAdmin(String secretCode)
        {

            if (secretCode.Equals("7617ed14946fe0c5005b301a30b15820e8a012db"))
            {
                XmlDocument doc = new XmlDocument();
                //doc.Load("books.xml");
                doc.Load(System.Web.Hosting.HostingEnvironment.MapPath("~/admin.xml"));

                //Display all the book titles.
                XmlNodeList nodes = doc.DocumentElement.SelectNodes("/admin");

                foreach (XmlNode node in nodes)
                {
                    string adminEmail = node.SelectSingleNode("email").InnerText;
                    string pass = node.SelectSingleNode("pass").InnerText;

                    var user = new ApplicationUser { UserName = adminEmail, Email = adminEmail };
                    var result = UserManager.Create(user, pass);
                    if (result.Succeeded)
                    {
                        // Create the Users Model
                        UserModel _userModel = new UserModel();
                        using (UserContext db = new UserContext())
                        {
                            Guid newGuid = Guid.Parse(user.Id);
                            /* 
                             * Need to create the association between database
                             * Should create this on user registration which should be first form!
                             * NOTE! We are not creating the array'd objects yet as we need to still 
                             * figure out how to handle them in the database
                            */

                            _userModel.UserID = newGuid;
                            _userModel.Email = user.Email;
                            //_userModel.Birthday = new Date();
                            //_userModel.Birthday
                            _userModel.Birthday = DateTime.Now;
                            _userModel.BirthPlace = new BirthPlaceLocation();
                            _userModel.BirthPlace.UserID = newGuid;
                            _userModel.ConsideredRaceAndEthnicity = new RaceEthnicity();
                            _userModel.ConsideredRaceAndEthnicity.UserID = newGuid;
                            _userModel.HealthInfo = new Health();
                            _userModel.HealthInfo.UserID = newGuid;
                            _userModel.HealthInfo.SeriousInjuryDate = DateTime.Now;
                            _userModel.SchoolInfo = new SchoolInfo();
                            _userModel.SchoolInfo.UserID = newGuid;
                            _userModel.LivesWith = new LivesWith();
                            _userModel.LivesWith.UserID = newGuid;
                            _userModel.MailingAddress = new Address();
                            _userModel.MailingAddress.UserID = newGuid;
                            _userModel.Name = new Name();
                            _userModel.Name.UserID = newGuid;
                            _userModel.PhoneInfo = new Phone();
                            _userModel.PhoneInfo.UserID = newGuid;
                            _userModel.ResidentAddress = new Address();
                            _userModel.ResidentAddress.UserID = newGuid;
                            _userModel.Retainment = new Retained();
                            _userModel.Retainment.UserID = newGuid;
                            _userModel.StudentChildCare = new ChildCare();
                            _userModel.StudentChildCare.UserID = newGuid;
                            _userModel.StudentsParentingPlan = new ParentPlan();
                            _userModel.StudentsParentingPlan.UserID = newGuid;
                            _userModel.LastUpdateDate = DateTime.Now;
                            _userModel.role = new Roles();

                            _userModel.role.isAdmin = true;

                            // now add the form info
                            db.Users.Add(_userModel);
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
                    }



                    //await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);

                    string[] income1 = { "$ 0 to 1, 860", "$ 0 to 930", "$ 0 to 859", "$ 0 to 430", "$ 0 to 22,311" };
                    string[] income2 = { "$ 1,861 to 2,504", "$ 931 to 1,252", "$ 860 to 1,156", "$ 431 to 578", "$ 22,312 to 30,044" };
                    string[] income3 = { "$ 2,505 to 3,149", "$ 1,253 to 1,575", "$ 1,157 to 1,453", "$ 579 to 727", "$ 30,045 to 37,777" };
                    string[] income4 = { "$ 3,150 to 3,793", "$ 1,576 to 1,897", "$ 1,454 to 1,751", "$ 728 to 876", "$ 37,778 to 45,510" };
                    string[] income5 = { "$ 3,794 to 4,437", "$ 1,898 to 2,219", "$ 1,752 to 2,048", "$ 877 to 1,024", "$ 46,511 to 53,243" };
                    string[] income6 = { "$ 4,438 to 5,082", "$ 2,220 to 2,541", "$ 2,049 to 2,346", "$ 1,025 to 1,173", "$ 53,244 to 60,976" };
                    string[] income7 = { "$ 5,083 to 5,726", "$ 2,542 to 2,863", "$ 2,347 to 2,643", "$ 1,174 to 1,322", "$ 60,977 to 68,709" };
                    string[] income8 = { "$ 5,727 to 6,371", "$ 2,864 to 3,186", "$ 2,644 to 2,941", "$ 1,323 to 1,471", "$ 68,710 to 76,442" };
                    string[] income9 = { "$ 6,372 to 7,016", "$ 3, 187 to 3,509", "$ 2,942 to 3,239", "$ 1,472 to 1,620", "$ 76,443 to 84,175" };
                    string[] income10 = { "$ 7,107 to 7,661", "$ 3,510 to 3,832", "$ 3,240 to 3,537", "$ 1,621 to 1,769", "$ 84,176 to 91,908" };
                    string[] income11 = { "$ 7,662 to 8,306", "$ 3,833 to 4,155", "$ 3,538 to 3,835", "$ 1,770 to 1,918", "$ 91,909 to 99,641" };
                    string[] income12 = { "$ 8,307 to 8,951", "$ 4,156 to 4,478", "$ 3,836 to 4,133", "$ 1,919 to 2,067", "$ 99,642 to 107,374" };
                    string[] income13 = { "$ 8,952 to 9,596", "$ 4,479 to 4,801", "$ 4,134 to 4,431", "$ 2,068 to 2,216", "$ 107,375 to 115,107" };
                    string[] income14 = { "$ 9,597 to 10,241", "$ 4,802 to 5,124", "$ 4,432 to 4,729", "$ 2,217 to 2,365", "$ 115,108 to 122,840" };
                    string[] income15 = { "$ 10,242 to 10,886", "$ 5,125 to 5,447", "$ 4,730 to 5,027", "$ 2,366 to 2,514", "$ 122,841 to 130,573" };

                    using (var ic = new IncomeContext())
                    {

                        List<string[]> list = new List<string[]>();
                        list.Add(income1);
                        list.Add(income2);
                        list.Add(income3);
                        list.Add(income4);
                        list.Add(income5);
                        list.Add(income6);
                        list.Add(income7);
                        list.Add(income8);
                        list.Add(income9);
                        list.Add(income10);
                        list.Add(income11);
                        list.Add(income12);
                        list.Add(income13);
                        list.Add(income14);
                        list.Add(income15);

                        FamilyIncome income = new FamilyIncome();
                        income.incomeTable = new List<IncomeTable>();
                        income.id = 0;
                        income.EffectiveDates = "Income Chart Effective from July 1, 2017 through June 30, 2018";
                        income.IncomeTableYears = "2017-18 Family Income Survey";
                        for (int i = 0; i < list.Count; i++)
                        {
                            IncomeTable table = new IncomeTable();
                            table.Monthly = list[i][0];
                            table.TwiceMonthly = list[i][1];
                            table.TwoWeeks = list[i][2];
                            table.Weekly = list[i][3];
                            table.Annually = list[i][4];
                            income.incomeTable.Add(table);
                        }

                        ic.Income.Add(income);
                        try
                        {
                            ic.SaveChanges();
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
                }
            }
        }


        //
        // POST: /Account/Register
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Register(RegisterViewModel model, bool isAdmin = false)
        {
            XmlDocument doc = new XmlDocument();
            //doc.Load("books.xml");
            doc.Load(System.Web.Hosting.HostingEnvironment.MapPath("~/admin.xml"));

            //Display all the book titles.
            XmlNodeList nodes = doc.DocumentElement.SelectNodes("/admin");
            UserContext dbAdmin = new UserContext();
            foreach (XmlNode node in nodes)
            {
                string adminEmail = node.SelectSingleNode("email").InnerText;
                var _user = dbAdmin.Users.Where(u => u.Email == adminEmail).FirstOrDefault<UserModel>();

                if (_user == null)
                {
                    RegisterDefaultAdmin("7617ed14946fe0c5005b301a30b15820e8a012db");
                }
            }
            dbAdmin.Dispose();

            if (ModelState.IsValid)
            {
                var user = new ApplicationUser { UserName = model.Email, Email = model.Email };
                var result = await UserManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    // Create the Users Model
                    UserModel _userModel = new UserModel();
                    using (UserContext db = new UserContext())
                    {
                        Guid newGuid = Guid.Parse(user.Id);
                        /* 
                         * Need to create the association between database
                         * Should create this on user registration which should be first form!
                         * NOTE! We are not creating the array'd objects yet as we need to still 
                         * figure out how to handle them in the database
                        */

                        _userModel.UserID = newGuid;
                        _userModel.Email = user.Email;
                        //_userModel.Birthday = new Date();
                        //_userModel.Birthday
                        _userModel.Birthday = DateTime.Now;
                        _userModel.BirthPlace = new BirthPlaceLocation();
                        _userModel.BirthPlace.UserID = newGuid;
                        _userModel.ConsideredRaceAndEthnicity = new RaceEthnicity();
                        _userModel.ConsideredRaceAndEthnicity.UserID = newGuid;
                        _userModel.HealthInfo = new Health();
                        _userModel.HealthInfo.UserID = newGuid;
                        _userModel.HealthInfo.SeriousInjuryDate = DateTime.Now;
                        _userModel.SchoolInfo = new SchoolInfo();
                        _userModel.SchoolInfo.UserID = newGuid;
                        _userModel.LivesWith = new LivesWith();
                        _userModel.LivesWith.UserID = newGuid;
                        _userModel.MillitaryInfo = new MillitaryInfo();
                        _userModel.MailingAddress = new Address();
                        _userModel.MailingAddress.UserID = newGuid;
                        _userModel.Name = new Name();
                        _userModel.Name.UserID = newGuid;
                        _userModel.PhoneInfo = new Phone();
                        _userModel.PhoneInfo.UserID = newGuid;
                        _userModel.ResidentAddress = new Address();
                        _userModel.ResidentAddress.UserID = newGuid;
                        _userModel.Retainment = new Retained();
                        _userModel.Retainment.UserID = newGuid;
                        _userModel.StudentChildCare = new ChildCare();
                        _userModel.StudentChildCare.UserID = newGuid;
                        _userModel.StudentsParentingPlan = new ParentPlan();
                        _userModel.StudentsParentingPlan.UserID = newGuid;
                        _userModel.LastUpdateDate = DateTime.Now;
                        _userModel.role = new Roles();

                        if (!isAdmin)
                        {
                            _userModel.role.isUser = true;
                        }
                        else
                        {
                            _userModel.role.isAdmin = true;
                        }

                        // now add the form info
                        db.Users.Add(_userModel);
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
                    if (!isAdmin)
                    {
                        await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);
                    }

                    // For more information on how to enable account confirmation and password reset please visit http://go.microsoft.com/fwlink/?LinkID=320771
                    // Send an email with this link
                    // string code = await UserManager.GenerateEmailConfirmationTokenAsync(user.Id);
                    // var callbackUrl = Url.Action("ConfirmEmail", "Account", new { userId = user.Id, code = code }, protocol: Request.Url.Scheme);
                    // await UserManager.SendEmailAsync(user.Id, "Confirm your account", "Please confirm your account by clicking <a href=\"" + callbackUrl + "\">here</a>");

                    if (!isAdmin)
                    {
                        return RedirectToAction("Index", "Home");
                    }
                    else if(isAdmin)
                    {
                        return RedirectToAction("Index", "Admin");
                    }
                }
                AddErrors(result);
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        //
        // GET: /Account/ConfirmEmail
        [AllowAnonymous]
        public async Task<ActionResult> ConfirmEmail(string userId, string code)
        {
            if (userId == null || code == null)
            {
                return View("Error");
            }
            var result = await UserManager.ConfirmEmailAsync(userId, code);
            return View(result.Succeeded ? "ConfirmEmail" : "Error");
        }

        //
        // GET: /Account/ForgotPassword
        [AllowAnonymous]
        public ActionResult ForgotPassword()
        {
            return View();
        }

        //
        // POST: /Account/ForgotPassword
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ForgotPassword(ForgotPasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await UserManager.FindByNameAsync(model.Email);
                if (user == null || !(await UserManager.IsEmailConfirmedAsync(user.Id)))
                {
                    // Don't reveal that the user does not exist or is not confirmed
                    return View("ForgotPasswordConfirmation");
                }

                // For more information on how to enable account confirmation and password reset please visit http://go.microsoft.com/fwlink/?LinkID=320771
                // Send an email with this link
                // string code = await UserManager.GeneratePasswordResetTokenAsync(user.Id);
                // var callbackUrl = Url.Action("ResetPassword", "Account", new { userId = user.Id, code = code }, protocol: Request.Url.Scheme);		
                // await UserManager.SendEmailAsync(user.Id, "Reset Password", "Please reset your password by clicking <a href=\"" + callbackUrl + "\">here</a>");
                // return RedirectToAction("ForgotPasswordConfirmation", "Account");
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        //
        // GET: /Account/ForgotPasswordConfirmation
        [AllowAnonymous]
        public ActionResult ForgotPasswordConfirmation()
        {
            return View();
        }

        //
        // GET: /Account/ResetPassword
        [AllowAnonymous]
        public ActionResult ResetPassword(string code)
        {
            return code == null ? View("Error") : View();
        }

        //
        // POST: /Account/ResetPassword
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ResetPassword(ResetPasswordViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var user = await UserManager.FindByNameAsync(model.Email);
            if (user == null)
            {
                // Don't reveal that the user does not exist
                return RedirectToAction("ResetPasswordConfirmation", "Account");
            }
            var result = await UserManager.ResetPasswordAsync(user.Id, model.Code, model.Password);
            if (result.Succeeded)
            {
                return RedirectToAction("ResetPasswordConfirmation", "Account");
            }
            AddErrors(result);
            return View();
        }

        //
        // GET: /Account/ResetPasswordConfirmation
        [AllowAnonymous]
        public ActionResult ResetPasswordConfirmation()
        {
            return View();
        }

        //
        // POST: /Account/ExternalLogin
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult ExternalLogin(string provider, string returnUrl)
        {
            // Request a redirect to the external login provider
            return new ChallengeResult(provider, Url.Action("ExternalLoginCallback", "Account", new { ReturnUrl = returnUrl }));
        }

        //
        // GET: /Account/SendCode
        [AllowAnonymous]
        public async Task<ActionResult> SendCode(string returnUrl, bool rememberMe)
        {
            var userId = await SignInManager.GetVerifiedUserIdAsync();
            if (userId == null)
            {
                return View("Error");
            }
            var userFactors = await UserManager.GetValidTwoFactorProvidersAsync(userId);
            var factorOptions = userFactors.Select(purpose => new SelectListItem { Text = purpose, Value = purpose }).ToList();
            return View(new SendCodeViewModel { Providers = factorOptions, ReturnUrl = returnUrl, RememberMe = rememberMe });
        }

        //
        // POST: /Account/SendCode
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> SendCode(SendCodeViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            // Generate the token and send it
            if (!await SignInManager.SendTwoFactorCodeAsync(model.SelectedProvider))
            {
                return View("Error");
            }
            return RedirectToAction("VerifyCode", new { Provider = model.SelectedProvider, ReturnUrl = model.ReturnUrl, RememberMe = model.RememberMe });
        }

        //
        // GET: /Account/ExternalLoginCallback
        [AllowAnonymous]
        public async Task<ActionResult> ExternalLoginCallback(string returnUrl)
        {
            var loginInfo = await AuthenticationManager.GetExternalLoginInfoAsync();
            if (loginInfo == null)
            {
                return RedirectToAction("Login");
            }

            // Sign in the user with this external login provider if the user already has a login
            var result = await SignInManager.ExternalSignInAsync(loginInfo, isPersistent: false);
            switch (result)
            {
                case SignInStatus.Success:
                    return RedirectToLocal(returnUrl);
                case SignInStatus.LockedOut:
                    return View("Lockout");
                case SignInStatus.RequiresVerification:
                    return RedirectToAction("SendCode", new { ReturnUrl = returnUrl, RememberMe = false });
                case SignInStatus.Failure:
                default:
                    // If the user does not have an account, then prompt the user to create an account
                    ViewBag.ReturnUrl = returnUrl;
                    ViewBag.LoginProvider = loginInfo.Login.LoginProvider;
                    return View("ExternalLoginConfirmation", new ExternalLoginConfirmationViewModel { Email = loginInfo.Email });
            }
        }

        //
        // POST: /Account/ExternalLoginConfirmation
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ExternalLoginConfirmation(ExternalLoginConfirmationViewModel model, string returnUrl)
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Manage");
            }

            if (ModelState.IsValid)
            {
                // Get the information about the user from the external login provider
                var info = await AuthenticationManager.GetExternalLoginInfoAsync();
                if (info == null)
                {
                    return View("ExternalLoginFailure");
                }
                var user = new ApplicationUser { UserName = model.Email, Email = model.Email };
                var result = await UserManager.CreateAsync(user);
                if (result.Succeeded)
                {
                    result = await UserManager.AddLoginAsync(user.Id, info.Login);
                    if (result.Succeeded)
                    {
                        await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);
                        return RedirectToLocal(returnUrl);
                    }
                }
                AddErrors(result);
            }

            ViewBag.ReturnUrl = returnUrl;
            return View(model);
        }

        //
        // POST: /Account/LogOff
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LogOff()
        {
            AuthenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
            return RedirectToAction("Index", "Home");
        }

        //
        // GET: /Account/ExternalLoginFailure
        [AllowAnonymous]
        public ActionResult ExternalLoginFailure()
        {
            return View();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (_userManager != null)
                {
                    _userManager.Dispose();
                    _userManager = null;
                }

                if (_signInManager != null)
                {
                    _signInManager.Dispose();
                    _signInManager = null;
                }
            }

            base.Dispose(disposing);
        }

        #region Helpers
        // Used for XSRF protection when adding external logins
        private const string XsrfKey = "XsrfId";

        private IAuthenticationManager AuthenticationManager
        {
            get
            {
                return HttpContext.GetOwinContext().Authentication;
            }
        }

        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error);
            }
        }

        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            return RedirectToAction("Index", "Home");
        }

        internal class ChallengeResult : HttpUnauthorizedResult
        {
            public ChallengeResult(string provider, string redirectUri)
             : this(provider, redirectUri, null)
            {
            }

            public ChallengeResult(string provider, string redirectUri, string userId)
            {
                LoginProvider = provider;
                RedirectUri = redirectUri;
                UserId = userId;
            }

            public string LoginProvider { get; set; }
            public string RedirectUri { get; set; }
            public string UserId { get; set; }

            public override void ExecuteResult(ControllerContext context)
            {
                var properties = new AuthenticationProperties { RedirectUri = RedirectUri };
                if (UserId != null)
                {
                    properties.Dictionary[XsrfKey] = UserId;
                }
                context.HttpContext.GetOwinContext().Authentication.Challenge(properties, LoginProvider);
            }
        }
        #endregion
    }
}