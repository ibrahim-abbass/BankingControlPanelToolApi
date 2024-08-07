using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BCPT.ABSTACTION
{
    public static class ErrorMessage
    {
        public static string UserExist = "User already exist";
        public static string UserCreationFailed = "Failed to create a User";
        public static string RoleNotExist = "This role: {0} not exist";
        public static string Unauthorized = "Unauthorized";
    }
}
