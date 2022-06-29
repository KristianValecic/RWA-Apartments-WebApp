using Lib.Models;
using Microsoft.ApplicationBlocks.Data;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lib.Dal
{
    public class DatabaseRepo : IRepo
    {
        private static readonly string CS = ConfigurationManager.ConnectionStrings["cs"].ConnectionString;
        private static readonly string APARTMENTS_CS = ConfigurationManager.ConnectionStrings["rwa"].ConnectionString;

        public IList<Apartment> LoadApartmentsByTagID(int tagID)
        {
            IList<Apartment> apartments = new List<Apartment>();

            var tblApartments = SqlHelper.ExecuteDataset(APARTMENTS_CS, nameof(LoadApartmentsByTagID), tagID).Tables[0];
            foreach (DataRow row in tblApartments.Rows)
            {
                apartments.Add(
                    new Apartment
                    {
                        Name = row[nameof(Tag.Name)].ToString()
                    }
                );
            }

            return apartments;
        }

        public User AuthUser(string username, string password)
        {
            var tblAuth = SqlHelper.ExecuteDataset(APARTMENTS_CS, nameof(AuthUser), username, Cryptography.HashPassword(password)).Tables[0];
            if (tblAuth.Rows.Count == 0)
            {
                return null;
            }

            DataRow row = tblAuth.Rows[0];
            return new User
            {
                ID = (int)row[nameof(User.ID)],
                UserName = row[nameof(User.UserName)].ToString(),
                Email = row[nameof(User.Email)].ToString(),
                City = User.GetCityFromAdressLine(row[nameof(User.Address)].ToString()),
                Address = User.GetStreetFromAdressLine(row[nameof(User.Address)].ToString())
                //Password = row[nameof(User.Password)].ToString()
            };
        }

        public IList<User> LoadUsers()
        {
            IList<User> users = new List<User>();
            var tblUsers = SqlHelper.ExecuteDataset(APARTMENTS_CS, nameof(LoadUsers)).Tables[0];

            foreach (DataRow row in tblUsers.Rows)
            {
                users.Add(
                    new User
                    {
                        ID = (int)row[nameof(User.ID)],
                        UserName = row[nameof(User.UserName)].ToString(),
                        Email = row[nameof(User.Email)].ToString(),
                        City = User.GetCityFromAdressLine(row[nameof(User.Address)].ToString()),
                        Address = User.GetStreetFromAdressLine(row[nameof(User.Address)].ToString()),
                        PasswordHash = row[nameof(User.PasswordHash)].ToString(),
                    });
            }
            return users;
        }

        //public void SaveUser(User user)
        //{
        //    SqlHelper.ExecuteNonQuery(APARTMENTS_CS, nameof(SaveUser), Guid.NewGuid(), user.UserName, user.Email, user.Address, user.Password);
        //}
        public void UpdateUser(User user)
        {
            SqlHelper.ExecuteNonQuery(APARTMENTS_CS, nameof(UpdateUser), user.ID, user.UserName, user.Email, user.Address);
        }
        public int AddUser(User user)
        {
            var tblUsers = SqlHelper.ExecuteDataset(APARTMENTS_CS, nameof(AddUser), Guid.NewGuid(), user.UserName, user.Email, user.Address, Cryptography.HashPassword(user.Password)).Tables[0];

            if (tblUsers.Rows.Count == 0)
            {
                return 0;
            }

            DataRow row = tblUsers.Rows[0];
            return (int)row["Succsess"];
        }
        public void DeleteUser(int userId)
        {
            SqlHelper.ExecuteNonQuery(APARTMENTS_CS, nameof(DeleteUser), userId);
        }

        public void AddTag(Tag tag)
        {
            SqlHelper.ExecuteNonQuery(APARTMENTS_CS, nameof(AddTag), Guid.NewGuid(), tag.TypeOfTag, tag.Name);
        }

        public void DeleteTag(int tagID)
        {
            SqlHelper.ExecuteNonQuery(APARTMENTS_CS, nameof(DeleteTag), tagID);
        }

        public IList<Tag> LoadTags()
        {
            IList<Tag> tags = new List<Tag>();

            var tblTags = SqlHelper.ExecuteDataset(APARTMENTS_CS, nameof(LoadTags)).Tables[0];
            foreach (DataRow row in tblTags.Rows)
            {
                tags.Add(
                    new Tag
                    {
                        ID = (int)row[nameof(Tag.ID)],
                        Name = row[nameof(Tag.Name)].ToString(),
                        TypeOfTag = row[nameof(Tag.TypeOfTag)].ToString(),
                        RecurrenceCounter = (int)row[nameof(Tag.RecurrenceCounter)]
                        //CreatedAt = DateTime.Parse(row[nameof(Tag.CreatedAt)].ToString())
                    }
                );
            }

            return tags;
        }
        public IList<Tag> LoadTagsForApartment(int apartID)
        {
            IList<Tag> tags = new List<Tag>();
            var tblTags = SqlHelper.ExecuteDataset(APARTMENTS_CS, nameof(LoadTagsForApartment), apartID).Tables[0];

            foreach (DataRow row in tblTags.Rows)
            {
                tags.Add(
                    new Tag
                    {
                        ID = (int)row[nameof(Tag.ID)],
                        Name = row[nameof(Tag.Name)].ToString(),
                    });
            }
            return tags;
        }
        public IList<string> LoadAllTypesOfTags()
        {
            IList<string> tagTypes = new List<string>();
            var tblTagTypes = SqlHelper.ExecuteDataset(APARTMENTS_CS, nameof(LoadAllTypesOfTags)).Tables[0];

            foreach (DataRow row in tblTagTypes.Rows)
            {
                tagTypes.Add(row[nameof(TagType.NameEng)].ToString());
            }
            return tagTypes;
        }

        public IList<City> LoadAllCities()
        {
            IList<City> cities = new List<City>();

            var tblUsers = SqlHelper.ExecuteDataset(APARTMENTS_CS, nameof(LoadAllCities)).Tables[0];
            foreach (DataRow row in tblUsers.Rows)
            {
                cities.Add(
                    new City
                    {
                        ID = (int)row[nameof(City.ID)],
                        Name = row[nameof(City.Name)].ToString(),
                    }
                );
            }

            return cities;
        }

        public IList<Apartment> LoadApartments()
        {
            IList<Apartment> aparts = new List<Apartment>();
            var tblUsers = SqlHelper.ExecuteDataset(APARTMENTS_CS, nameof(LoadApartments)).Tables[0];

            foreach (DataRow row in tblUsers.Rows)
            {
                aparts.Add(
                    new Apartment
                    {
                        ID = (int)row[nameof(Apartment.ID)],
                        GUID = row[nameof(Apartment.GUID)].ToString(),
                        Name = row[nameof(Apartment.Name)].ToString(),
                        City = row[nameof(Apartment.City)].ToString(),
                        Owner = row[nameof(Apartment.Owner)].ToString(),
                        Price = (decimal)row[nameof(Apartment.Price)],
                        TotalRooms = (int)row[nameof(Apartment.TotalRooms)],
                        Status = row[nameof(Apartment.Status)].ToString(),
                        Address = row[nameof(Apartment.Address)].ToString(),
                        BeachDistance = (int)row[nameof(Apartment.BeachDistance)],
                        MaxAdults = (int)row[nameof(Apartment.MaxAdults)],
                        MaxChildren = (int)row[nameof(Apartment.MaxChildren)]
                    });
            }
            return aparts;
        }



        public void UpdateApartment(Apartment apartment)
        {
            SqlHelper.ExecuteNonQuery(APARTMENTS_CS, nameof(UpdateApartment), apartment.ID, apartment.Name, apartment.City, apartment.Address,
            apartment.MaxChildren, apartment.MaxAdults, apartment.Status, apartment.Price, apartment.TotalRooms, apartment.BeachDistance);
        }

        public void DeleteApartment(int apartmentID)
        {
            SqlHelper.ExecuteNonQuery(APARTMENTS_CS, nameof(DeleteApartment), apartmentID);
        }

        public void AddApartment(Apartment apartment)
        {
            SqlHelper.ExecuteNonQuery(APARTMENTS_CS, nameof(AddApartment), Guid.NewGuid(), Guid.NewGuid(), apartment.Name, apartment.Owner, apartment.City,
                apartment.Address, apartment.MaxChildren, apartment.MaxAdults, apartment.Status, apartment.Price, apartment.TotalRooms, apartment.BeachDistance);
        }

        public IList<Status> LoadAllStatuses()
        {
            IList<Status> statuses = new List<Status>();

            var tblStatus = SqlHelper.ExecuteDataset(APARTMENTS_CS, nameof(LoadAllStatuses)).Tables[0];
            foreach (DataRow row in tblStatus.Rows)
            {
                statuses.Add(new Status
                {
                    ID = (int)row[nameof(Status.ID)],
                    Name = row[nameof(Status.Name)].ToString(),
                    NameEng = row[nameof(Status.NameEng)].ToString()
                });
            }
            return statuses;
        }

        public void RemoveTagFromApartment(int tagID, int apartmentID)
        {
            SqlHelper.ExecuteNonQuery(APARTMENTS_CS, nameof(RemoveTagFromApartment), tagID, apartmentID);
        }

        public int AddTagToApartment(int tagID, int apartmentID)
        {
            var tblAdd = SqlHelper.ExecuteDataset(APARTMENTS_CS, nameof(AddTagToApartment), Guid.NewGuid(), apartmentID, tagID).Tables[0];

            if (tblAdd.Rows.Count == 0)
            {
                return 0;
            }

            DataRow row = tblAdd.Rows[0];
            return (int)row["Succsess"];
        }

        public IList<Picture> loadImagesForAparment(int apartmentID)
        {
            IList<Picture> paths = new List<Picture>();
            var tblUsers = SqlHelper.ExecuteDataset(APARTMENTS_CS, nameof(loadImagesForAparment), apartmentID).Tables[0];

            foreach (DataRow row in tblUsers.Rows)
            {
                paths.Add(
                    new Picture
                    {
                        ID = (int)row[nameof(Picture.ID)],
                        Guid = row[nameof(Picture.Guid)].ToString(),
                        Name = row[nameof(Picture.Name)].ToString(),
                        Base64 = row[nameof(Picture.Base64)].ToString()
                    });
            }
            return paths;
        }

        public void AddImageForAparment(Picture p, int apartID)
        {
            SqlHelper.ExecuteNonQuery(APARTMENTS_CS, nameof(AddImageForAparment), Guid.NewGuid(), apartID, p.Base64, p.Name, p.IsRepresentitive);
        }

        public Tag GetTagById(string tagID)
        {
            var tblTags = SqlHelper.ExecuteDataset(APARTMENTS_CS, nameof(GetTagById), tagID).Tables[0];

            if (tblTags.Rows.Count == 0)
            {
                return null;
            }

            DataRow row = tblTags.Rows[0];
            return new Tag
            {
                ID = (int)row[nameof(Tag.ID)],
                Name = row[nameof(Tag.Name)].ToString()
            };
        }

        public IList<Reservation> LoadAllReservationsForApartment(int apartmentID)
        {
            IList<Reservation> reservations = new List<Reservation>();
            var tblReservations = SqlHelper.ExecuteDataset(APARTMENTS_CS, nameof(LoadAllReservationsForApartment), apartmentID).Tables[0];

            foreach (DataRow row in tblReservations.Rows)
            {
                if (row["UserId"].ToString() == String.Empty)
                {                                                           // ///////////
                    string username = $"{row["name"]} {row["UserEmail"]}"; // mozda nepotrebno dodavat email!!!!!!
                                                                          // ///////////
                    reservations.Add(
                    new Reservation
                    {
                        ID = (int)row[nameof(Reservation.ID)],
                        UserName = username,
                        Details = row[nameof(Reservation.Details)].ToString(),
                    });
                }
                else
                {
                    reservations.Add(
                    new Reservation
                    {
                        ID = (int)row[nameof(Reservation.ID)],
                        UserName = row[nameof(Reservation.UserName)].ToString(),
                        Details = row[nameof(Reservation.Details)].ToString(),
                    });
                }
            }
            return reservations;
        }

        public void RemoveReservationFromApartment(int reservationID, int apartmentID)
        {
            SqlHelper.ExecuteNonQuery(APARTMENTS_CS, nameof(RemoveReservationFromApartment), reservationID, apartmentID);
        }

        public int AddReservationToApartment(string username, string details, int apartmentID)
        {
            var tblReservations = SqlHelper.ExecuteDataset(APARTMENTS_CS, nameof(AddReservationToApartment), Guid.NewGuid(), username, details, apartmentID).Tables[0];

            if (tblReservations.Rows.Count == 0)
            {
                return 0;
            }

            DataRow row = tblReservations.Rows[0];
            return (int)row["Succsess"];
        }

        public void SetReserved(int apartmentID)
        {
            SqlHelper.ExecuteNonQuery(APARTMENTS_CS, nameof(SetReserved), apartmentID);
        }

        public Apartment GetApartment(string name, string owner, string address)
        {
            var tblApart = SqlHelper.ExecuteDataset(APARTMENTS_CS, nameof(GetApartment), name, address, owner).Tables[0];

            if (tblApart.Rows.Count == 0)
            {
                return null;
            }

            DataRow row = tblApart.Rows[0];
            return new Apartment
            {
                ID = (int)row[nameof(Apartment.ID)],
                Name = row[nameof(Apartment.Name)].ToString(),
                City = row[nameof(Apartment.City)].ToString(),
                Owner = row[nameof(Apartment.Owner)].ToString(),
                Price = (decimal)row[nameof(Apartment.Price)],
                TotalRooms = (int)row[nameof(Apartment.TotalRooms)],
                Status = row[nameof(Apartment.Status)].ToString(),
                Address = row[nameof(Apartment.Address)].ToString(),
                BeachDistance = (int)row[nameof(Apartment.BeachDistance)],
                MaxAdults = (int)row[nameof(Apartment.MaxAdults)],
                MaxChildren = (int)row[nameof(Apartment.MaxChildren)]
            };
        }

        public Apartment GetApartmentById(int apartmentID)
        {
            var tblApart = SqlHelper.ExecuteDataset(APARTMENTS_CS, nameof(GetApartmentById), apartmentID).Tables[0];

            if (tblApart.Rows.Count == 0)
            {
                return null;
            }

            DataRow row = tblApart.Rows[0];
            return new Apartment
            {
                ID = (int)row[nameof(Apartment.ID)],
                Name = row[nameof(Apartment.Name)].ToString(),
                City = row[nameof(Apartment.City)].ToString(),
                Owner = row[nameof(Apartment.Owner)].ToString(),
                Price = (decimal)row[nameof(Apartment.Price)],
                TotalRooms = (int)row[nameof(Apartment.TotalRooms)],
                Status = row[nameof(Apartment.Status)].ToString(),
                Address = row[nameof(Apartment.Address)].ToString(),
                BeachDistance = (int)row[nameof(Apartment.BeachDistance)],
                MaxAdults = (int)row[nameof(Apartment.MaxAdults)],
                MaxChildren = (int)row[nameof(Apartment.MaxChildren)]
            };
        }
    }
}