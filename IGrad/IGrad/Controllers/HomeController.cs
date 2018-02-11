using System.Web.Mvc;

namespace IGrad.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            if (Request.IsAuthenticated)
            {
                return RedirectToAction("GetNewApplication", "Application");
            }
            // Example of saving a record
            //using (UserContext db = new UserContext())
            //{
            //    var usr = new UserModel();
            //    usr.UserID = Guid.NewGuid();
            //    usr.CountryBornIn = "Canada";
            //    db.Entry(usr).State = ;
            //    db.SaveChanges();
            //}

            // Example of updating a record
            //ContextHelper ch = new ContextHelper();
            //UserModel usr = new UserModel();
            //UserContext uC = new UserContext();
            //usr = uC.Users.First(a => a.UserID.ToString() == "FF7E8DDD-041F-4DE7-A649-6613EC561259");
            //usr.CountryBornIn = "NoWhere";
            //ch.UpdateRecord(usr);

            return View();
        }



        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}