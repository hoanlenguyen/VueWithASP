using Microsoft.AspNetCore.Authorization;
using System.Security;
using webapi.Enum;

namespace webapi.Model.Permission
{
    public static class Permissions
    {
        public const string Type = "Permission";

        public static readonly List<string> AllPermissionModules = new List<string>
        {
            "User",
            "Role",
            "Product"
            // add more Permissons modul here
        };

        private static List<string> GeneratePermissionsForModule(string module)
        {
            return new List<string>()
            {
                $"{module}.Create",
                $"{module}.View",
                $"{module}.Update",
                $"{module}.Delete"
            };
        }

        public static IList<string> GetAllPermissions()
        {
            List<string> permissions = new();
            foreach (var module in AllPermissionModules)
            {
                permissions.AddRange(GeneratePermissionsForModule(module));
            }
            return permissions;
        }

        public static void AddCustomizedAuthorizationOptions(this AuthorizationOptions option)
        {
            foreach (var module in AllPermissionModules)
            {
                var allPermissions = GeneratePermissionsForModule(module);
                foreach (var permission in allPermissions)
                {
                    option.AddPolicy(name: permission, policy => policy.Requirements.Add(new PermissionRequirement(permission)));
                }
            }

            option.AddPolicy(BaseRoles.Admin, policy => policy.RequireRole(BaseRoles.Admin));
        }
    } 

    public static class UserPermissions
    {
        public const string View = "User.View";
        public const string Create = "User.Create";
        public const string Update = "User.Update";
        public const string Delete = "User.Delete";
    }

    public static class RolePermissions
    {
        public const string View = "Role.View";
        public const string Create = "Role.Create";
        public const string Update = "Role.Update";
        public const string Delete = "Role.Delete";
    }

    public static class ProductPermissions
    {
        public const string View = "Product.View";
        public const string Create = "Product.Create";
        public const string Update = "Product.Update";
        public const string Delete = "Product.Delete";
    }

    // add more Permissons class here
}