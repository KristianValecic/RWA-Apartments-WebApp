using Lib.Dal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVC_Apartmani.Models.Auth
{
    public class DbContext : IDisposable
    {
        
        public IList<User> Users { get; set; }

        public DbContext(IList<User> users)
        {
            Users = users;
        }

        public void Dispose()
        {

        }
        // treba maknuti create()
        public static DbContext Create()
        {
            List<User> users = Repo.LoadUsers();

            //{
            //    new User
            //    {
            //        UserName = "kristian@mail.kom",
            //        Email = "kristian@mail.kom",
            //        Password = "123"
            //    },
            //    new User
            //    {
            //        UserName = "man@mail.kom",
            //        Email = "man@mail.kom",
            //        Password = "123"
            //    }
            //};


            return new DbContext(users);
        }
    }
}