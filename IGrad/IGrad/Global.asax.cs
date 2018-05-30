using IGrad.Context;
using System;
using System.Data.Entity;
using System.IO;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace IGrad
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            Database.SetInitializer<UserContext>(new DropCreateDatabaseIfModelChanges<UserContext>());
        }

        protected void Application_Error(object sender, EventArgs e)
        {
            Exception exc = Server.GetLastError();
                try
                {
                    string filename = string.Format("Error_{0}_log.txt", DateTime.Now.ToString("dd_MM_yyyy-HH-mm-ss"));
                    // Log the error:
                    using (StreamWriter sw = new StreamWriter(Server.MapPath("~/Errors/" + filename), true))
                    {
                        if (exc.InnerException != null)
                        {
                            sw.Write("Inner Exception Type: ");
                            sw.WriteLine(exc.InnerException.GetType().ToString());
                            sw.Write("Inner Exception: ");
                            sw.WriteLine(exc.InnerException.Message);
                            sw.Write("Inner Source: ");
                            sw.WriteLine(exc.InnerException.Source);
                            if (exc.InnerException.StackTrace != null)
                            {
                                sw.WriteLine("Inner Stack Trace: ");
                                sw.WriteLine(exc.InnerException.StackTrace);
                            }
                        }
                        sw.Write("Exception Type: ");
                        sw.WriteLine(exc.GetType().ToString());
                        sw.WriteLine("Exception: " + exc.Message);
                        sw.WriteLine("Stack Trace: ");
                        if (exc.StackTrace != null)
                        {
                            sw.WriteLine(exc.StackTrace);
                            sw.WriteLine();
                        }
                        sw.Close();
                    }
                }
                catch(Exception ex)
                {

                }
        }
    }
}
