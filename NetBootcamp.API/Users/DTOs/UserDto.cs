namespace NetBootcamp.API.Users.DTOs
{
    public record UserDto(int Id, string Name, string Surname ,string Email, string Created);

    //public record ProductDto
    //{
    //    public int Id { get; init; }
    //    public string Name { get; init; } = default!;
    //    public decimal Price { get; init; }

    //    public string Created { get; init; } = default!;
    //}
}