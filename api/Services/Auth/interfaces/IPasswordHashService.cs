namespace api.Services.Auth.interfaces;

public interface IPasswordHashService
{

    public string Hash(string password);
    public bool Verify(string password, string passwordHash);

}
