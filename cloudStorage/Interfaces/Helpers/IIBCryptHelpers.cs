namespace CloudStorage.Interfaces
{
    public interface IBCryptHelpers
    {
        public string HashPassword(string password);
        public bool VerifyPassword(string password, string hashedPassword);
    }
}