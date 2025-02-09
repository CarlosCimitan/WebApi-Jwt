namespace WepApiProjetoReact.Services.Authenticate
{
    public interface IAuthenticateInterface
    {
        Task<bool> Authenticate(string email, string password);
        Task<bool> RegisterUser(string email, string password);
        Task Logout();
    }
}
