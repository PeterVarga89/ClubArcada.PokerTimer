using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClubArcada.BusinessObjects.DataClasses;

namespace ClubArcada.BusinessObjects.Data
{
    public class UserData
    {
        public static List<User> GetList()
        {
            using (var appDC = new PSDBDataContext(Settings.ConnectionString))
            {
                return appDC.Users.ToList();
            }
        }

        public static List<User> GetListBySearchString(string searchString)
        {
            using (var app = new PSDBDataContext(Settings.ConnectionString))
            {
                return app.Users.Where(u =>
                    u.NickName.ToLower().Contains(searchString.ToLower())
                    ||
                    u.FirstName.ToLower().Contains(searchString.ToLower())
                    ||
                    u.LastName.ToLower().Contains(searchString.ToLower())
                    ||
                    (u.FirstName + " " + u.LastName).ToLower().StartsWith(searchString.ToLower())
                    ).ToList();
            }
        }

        public static User Create(User entity)
        {
            entity.DateCreated = DateTime.Now;
            entity.UserId = Guid.NewGuid();
            entity.PeronalId = string.Empty;
            entity.Comment = string.Empty;
            entity.Password = string.Empty;

            using (var appDC = new PSDBDataContext(Settings.ConnectionString))
            {
                appDC.Users.InsertOnSubmit(entity);
                appDC.SubmitChanges();
            }

            return entity;
        }
    }
}
