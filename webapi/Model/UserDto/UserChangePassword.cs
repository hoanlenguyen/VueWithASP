namespace webapi.Model.UserDto
{
    public record UserChangePassword(string CurrentPassword, string NewPassword);
}