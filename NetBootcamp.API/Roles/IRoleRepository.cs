namespace NetBootcamp.API.Roles
{
    public interface IRoleRepository
    {
        IReadOnlyList<Role> GetAll();

        void Update(Role role);
        void Create(Role role);

        Role? GetById(int id);

        void Delete(int id);
    }
}