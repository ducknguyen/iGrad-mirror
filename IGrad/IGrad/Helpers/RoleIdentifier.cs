using IGrad.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;

namespace IGrad.Helpers
{
    public static class RoleIdentifier
    {
        public static bool isAdmin()
        {
            try
            {
                var httpContext = HttpContext.Current;
                Guid UserID = Guid.Parse(httpContext.User.Identity.GetUserId());
                using (UserContext uc = new UserContext())
                {
                    var me = uc.Users.Where(u => u.UserID == UserID).FirstOrDefault();

                    if (me.role.isAdmin)
                    {
                        return true;
                    }
                    return false;
                }
            }
            catch(Exception ex)
            {
                return false;
            }
        }
    }
}