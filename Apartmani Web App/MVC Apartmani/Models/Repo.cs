using Lib.Dal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVC_Apartmani.Models
{
    public static class Repo 
    {
        private static IRepo _repo = RepoFactory.GetRepo();

        internal static List<User> LoadUsers()
        {
            List<User> users = new List<User>();
            _repo.LoadUsers().ToList().ForEach(u =>
            {
                users.Add(new Models.User
                {
                    Id = u.ID.ToString(), //potencijalna greska
                    UserName = u.UserName,
                    Password = u.PasswordHash,
                    Email = u.Email,
                    Address = u.Address,
                    City = u.City
                });
            });

            return users;
        }

        internal static int AddUser(User user)
        {
            if (_repo.AddUser(user) == 1)
            {
                return 1;
            }
            else
            {
                return 0;
            }
        }
        internal static IList<Lib.Models.Apartment> LoadAllApartments()
        {
            return _repo.LoadApartments();
        }

        internal static object GetApartmentById(int apartmentID)
        {
            return _repo.GetApartmentById(apartmentID);
        }
    }
}