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
        public static void UpdateRecord(UserModel updatedUser)
        {
            try
            {
                using (UserContext db = new UserContext())
                { 
                    var result = db.Users.SingleOrDefault(u => u.UserID == updatedUser.UserID);
                    if (result != null)
                    {
                        var Name = result.Name;
                        if(Name == null)
                        {
                            db.Users.SingleOrDefault(u => u.UserID == updatedUser.UserID).Name = updatedUser.Name;
                        }
                        db.Entry(result).CurrentValues.SetValues(updatedUser);
                        db.SaveChanges();
                        //result = updatedUser;
                        //db.SaveChanges();
                    }
                }
            }
            catch(Exception ex)
            {
                // TODO: what we wanna do here?
            }
        }

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