namespace NetBootcamp.API.Users
{
    public interface IUserRepository
    {
        IReadOnlyList<User> GetAll();

        void Update(User user);
        void Create(User user);

        User? GetById(int id);

        void Delete(int id);
    }
}