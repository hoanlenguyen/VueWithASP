namespace webapi.Enum
{
    public enum UserType
    {
        EndUser = 0,
        Admin = 10,
        Manager= 20,
        SuperAdmin = 100
    }

    public enum MaxLengthLimit
    {
        Name = 150,
        ShortName = 100,
        Description = 500,
        ShortDescription = 100
    }
}