using Microsoft.AspNetCore.Authorization;

namespace webapi.Model.Permission
{
    public class PermissionRequirement : IAuthorizationRequirement
    {

        public string Permission { get; }
        public PermissionRequirement(string permission) => Permission = permission; 
    }
}
