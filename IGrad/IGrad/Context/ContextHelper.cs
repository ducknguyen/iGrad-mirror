using IGrad.Models;
using IGrad.Models.User;
using Microsoft.AspNet.Identity;
using System;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace IGrad.Context
{
    public class ContextHelper
    {
        public static UserModel GetUserInfo(Guid userId)
        {
            try
            {
                using (UserContext db = new UserContext())
                {
                    var result = db.Users.SingleOrDefault(u => u.UserID == userId);
                    if (result != null)
                    {
                        return result;
                    }
                    else return null;
                }
            }
            catch
            {
                // TODO: what we wanna do here?
                return null;
            }
        }
    }
}