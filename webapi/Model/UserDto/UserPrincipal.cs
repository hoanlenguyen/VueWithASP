﻿using System.Security.Claims;

namespace webapi.Model.UserDto
{
    public interface IUserPrincipal
    {
        public int Id { get; }
        public string? Email { get; }
        public string? FullName { get; }
        public string? RoleName { get; }
    }

    public class UserPrincipal : IUserPrincipal
    {
        public int Id { get; }
        public string? Email { get; }
        public string? FullName { get; }
        public string? RoleName { get; }

        public UserPrincipal(ClaimsPrincipal user)
        {
            int.TryParse(user.FindFirstValue(ClaimTypes.NameIdentifier), out var id);
            Id = id;
            Email = user.FindFirstValue(ClaimTypes.Email);
            FullName = user.FindFirstValue(ClaimTypes.Name);
            RoleName = user.FindFirstValue(ClaimTypes.Role);
        }
    }
}
