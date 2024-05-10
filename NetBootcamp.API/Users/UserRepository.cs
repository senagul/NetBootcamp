namespace NetBootcamp.API.Users
{
    public class UserRepository : IUserRepository
    {
        private static List<User> _users =
        [
            new User { Id = 1, Name = "User 1", Surname = "User 1 Surname" , Email= "user1@email.com",Password="user1password"},
            new User { Id = 2, Name = "User 2", Surname = "User 2 Surname" , Email= "user2@email.com",Password="user2password" },
            new User { Id = 3, Name = "User 3", Surname = "User 3 Surname" , Email= "user3@email.com",Password="user3password" }
        ];

        public IReadOnlyList<User> GetAll()
        {
            return _users;
        }

        public void Update(User user)
        {
            var index = _users.FindIndex(x => x.Id == user.Id);

            _users[index] = user;
        }

        public void Create(User user)
        {
            var methodName = nameof(UsersController.GetById); // GetById
            _users.Add(user);
        }


        public User? GetById(int id)
        {
            return _users.Find(x => x.Id == id);
        }

        // write delete method
        public void Delete(int id)
        {
            var user = GetById(id);

            _users.Remove(user!);
        }
    }
}