using Microsoft.AspNet.Identity;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;

namespace MVC_Apartmani.Models
{
    public class User : Lib.Models.User, IUser
    {
        public User()
        {
            Id = Guid.NewGuid().ToString();
            //Roles = new List<string>();
        }
        public string Id { get; set; }
       // public string UserName { get; set; }
        public DateTime CreatedTime { get; set; }

        public DateTime UpdatedTime { get; set; }

        //public string Id => throw new NotImplementedException();

        //public string UserName { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        //public virtual void AddRole(string role)
        //{
        //    Roles.Add(role);
        //}

        //public virtual void RemoveRole(string role)
        //{
        //    Roles.Remove(role);
        //}


        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<User> manager)
        {
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            return userIdentity;
        }
    }
}