using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BCPT.ABSTACTION
{
    public static class ErrorMessage
    {
        public static string UserExist = "The entered {0} already exist";
        public static string UserCreationFailed = "Failed to create a User";
        public static string RoleNotExist = "This role: {0} not exist";
        public static string Unauthorized = "Unauthorized";
        public static string UserNotFound = "Client not found";
        public static string EmailExsit = "Email already exists in the database";
        public static string ValidProperty = "Please Enter a valid property name";
        public static string CheckAccountId = "Account.Id is required";
    }
}
