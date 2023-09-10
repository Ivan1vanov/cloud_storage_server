using CloudStorage.Entity;

namespace CloudStorage.Interfaces
{
    public interface IUserRespository
    {
        Task<User?> GetUserById(string id);
        Task<User?> GetByEmailAddress(string email);
        Task<bool> DoesUserExistByEmail(string email);
        Task<User> CreateUser(User user);
    }
}