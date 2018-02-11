using IGrad.Models.User;
using System;
using System.Linq;

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
                        result = updatedUser;
                        db.SaveChanges();
                    }
                }
            }
            catch
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