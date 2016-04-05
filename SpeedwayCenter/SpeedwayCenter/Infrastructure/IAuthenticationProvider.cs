namespace SpeedwayCenter.Infrastructure
{
    public interface IAuthenticationProvider
    {
        bool Authenticate(string userName, string password);
    }
}