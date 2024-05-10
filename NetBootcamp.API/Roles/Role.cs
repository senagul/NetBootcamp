namespace NetBootcamp.API.Roles
{
    public class Role
    {
        public int Id { get; set; }
        public string Name { get; set; } = default!;
        public DateTime Created { get; set; } = new();

    }
}