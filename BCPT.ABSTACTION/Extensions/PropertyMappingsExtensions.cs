using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BCPT.ABSTACTION
{
    public static class PropertyMappingsExtensions
    {
        public static Dictionary<string, string> ClientPropertyMap = new Dictionary<string, string>
        {
            { "id", "Id" },
            { "firstname", "FirstName" },
            { "lastname", "LastName" },
            { "email", "Email" },
            { "personalid", "PersonalId" },
            { "mobilenumber", "MobileNumber" },
            { "sex", "Sex" }
         };

        public static bool IsValidProperty(this string propertyName)
        {
            return ClientPropertyMap.ContainsKey(propertyName.ToLower());
        }
        public static string GetProperty(this string propertyName)
        {
            return ClientPropertyMap[propertyName.ToLower()];
        }
    }
}
