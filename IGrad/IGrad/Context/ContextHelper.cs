using IGrad.Models.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IGrad.Context
{
    public class ContextHelper
    {
        public void UpdateRecord(UserModel updatedUser)
        {
            try
            {
                
                using (UserContext db = new UserContext())
                {
                    db.Database.Log = Console.WriteLine;
                    db.Users.Add(updatedUser);
                    db.Entry(updatedUser).State = System.Data.Entity.EntityState.Modified;
                    db.SaveChanges();
                }
            }
            catch
            {
                // TODO: what we wanna do here?
            }
        }
    }
}