namespace son_atik_takip.Services
{
    public interface IUserService
    {
        bool ValidateUser(string username, string password);
    }
}
