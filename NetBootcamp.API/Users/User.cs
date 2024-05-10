namespace NetBootcamp.API.Users
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; } = default!;
        public string Surname { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public DateTime Created { get; set; } = new();

    }
}