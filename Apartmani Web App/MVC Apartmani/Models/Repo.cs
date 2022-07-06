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

        public static List<User> LoadUsers()
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

        public static int AddUser(User user)
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
        public static IList<Lib.Models.Apartment> LoadAllApartments()
        {
            return _repo.LoadApartments();
        }

        public static Lib.Models.Apartment GetApartmentById(int apartmentID)
        {
            return _repo.GetApartmentById(apartmentID);
        }

        public static IList<Lib.Models.Tag> LoadTagsForApartment(int apartmentID)
        {
            return _repo.LoadTagsForApartment(apartmentID);
        }

        internal static IList<Lib.Models.Picture> loadImagesForAparment(int apartmentID)
        {
            return _repo.loadImagesForAparment(apartmentID);
        }
    }
}