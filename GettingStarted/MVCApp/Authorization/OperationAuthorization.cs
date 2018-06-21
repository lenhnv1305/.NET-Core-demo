using Microsoft.AspNetCore.Authorization.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVCApp.Authorization
{
    public static class OperationAuthorization
    {
        public static OperationAuthorizationRequirement Create = new OperationAuthorizationRequirement { Name = Constants.Modify };
    }
    public static class Constants
    {
        public static readonly string Modify = "Modify";

        public static readonly string AdministratorsRole = "Administrator";
        public static readonly string BlogerRole = "Bloger";
    }
}
