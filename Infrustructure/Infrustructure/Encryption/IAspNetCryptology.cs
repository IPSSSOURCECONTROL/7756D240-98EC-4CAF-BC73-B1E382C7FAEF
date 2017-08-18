namespace KhanyisaIntel.Kbit.Framework.Infrustructure.Encryption
{
    public interface IAspNetCryptology
    {
        string HashPassword(string password);
        bool VerifyHashedPassword(string hashedPassword, string password);
    }
}