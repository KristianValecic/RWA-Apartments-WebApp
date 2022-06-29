using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lib.Models
{
    [Serializable]
    public class User
    {
        private const char ADRESS_DELIM = ',';
        private string addressTemp;
        private string usernameTemp;
        //private const char DELIMITER = '|';

        public int ID { get; set; }
        public string FName { get; set; }
        public string LName { get; set; }
        public string City { get; set; }
        public string Email { get; set; }
        public string Address
        {
            get => addressTemp;
            set
            {
                addressTemp = $"{value}{ADRESS_DELIM} {City}";
            }
        }
        public string UserName
        {
            get => usernameTemp;
            set
            {
                if (FName != null && LName != null)
                {
                    usernameTemp = $"{FName} {LName}";
                }
                else
                {
                    usernameTemp = value;
                }
            }
        }
        public string Password { get; set; }
        public string PasswordHash { get; set; }


        //public string Username //FullName
        //{
        //    get
        //    {
        //        return $"{FName} {LName}";
        //    }
        //}

        //internal string FormatForFileLine() => $"{FName}{DELIMITER}{LName}{DELIMITER}{City}{DELIMITER}{Username}{DELIMITER}{Password}{Environment.NewLine}";

        //internal static User ParseFromFileLine(string line)
        //{
        //    string[] details = line.Split(DELIMITER);
        //    return new User
        //    {
        //        FName = details[0],
        //        LName = details.Length > 1 ? details[1] : string.Empty,
        //        City = details.Length > 2 ? details[2] : string.Empty,
        //        Username = details.Length > 3 ? details[3] : string.Empty,
        //        Password = details.Length > 4 ? details[4] : string.Empty,
        //    };
        //}

        public User()
        {
            usernameTemp = $"{FName} {LName}";
        }

        public static string GetCityFromAdressLine(string adress)
        {
            string[] details = adress.Split(ADRESS_DELIM);
            string city = details[1].Trim();
            return city;
        }
        public static string GetStreetFromAdressLine(string adress)
        {
            string[] details = adress.Split(ADRESS_DELIM);

            return details[0];
        }

        //public User(string username, string password)
        //{
        //    Username = username;
        //    Password = password;
        //}

        //public User(string fName, string lName, string city, string username, string password)
        //{
        //    FName = fName;
        //    LName = lName;
        //    City = city;
        //    Username = username;
        //    Password = password;
        //}
    }
}
