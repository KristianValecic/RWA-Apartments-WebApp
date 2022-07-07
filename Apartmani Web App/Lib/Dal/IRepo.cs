using Lib.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lib.Dal
{
    public interface IRepo
    {
        IList<User> LoadUsers();
        int AddUser(User u);
        void AddTag(Tag tag);
        IList<string> LoadAllTypesOfTags();
        void DeleteTag(int tagID);
        IList<Tag> LoadTags();
        void UpdateUser(User selectedUser);
        void DeleteUser(int userId);
        IList<Apartment> LoadApartments();
        void DeleteImg(int imgID);
        IList<City> LoadAllCities();
        void UpdateApartment(Apartment apartment);
        void DeleteApartment(int apartmentID);
        void AddApartment(Apartment apartment);
        IList<Status> LoadAllStatuses();
        IList<Tag> LoadTagsForApartment(int apartID);
        IList<Picture> loadImagesForAparment(int apartmentID);
        void AddImageForAparment(Picture p, int apartID);
        void RemoveTagFromApartment(int tagID, int apartmentID);
        int AddTagToApartment(int tagID, int apartmentID);
        Tag GetTagById(string tagID);
        IList<Reservation> LoadAllReservationsForApartment(int apartmentID);
        void RemoveReservationFromApartment(int reservationID, int apartmentID);
        int AddReservationToApartment(string username, string email, string address, string details, int apartmentID);
        void SetReserved(int apartmentID);
        Apartment GetApartmentById(int apartmentID);
        Apartment GetApartment(string name, string owner, string address);
        dynamic GetUserById(string userID);
    }
}
