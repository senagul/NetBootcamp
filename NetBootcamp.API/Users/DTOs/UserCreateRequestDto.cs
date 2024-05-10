namespace NetBootcamp.API.Users.DTOs
{
    // yeni hal
    public record UserCreateRequestDto(string Name, string Surname,string Email,string Password);

    //eski hal
    //public class ProductCreateRequestDtoLegacy
    //{
    //    public string Name { get; set; }
    //    public decimal Price { get; set; }


    //public A(){}

    //    public A(string name,decimal price)
    //    {
    //        Name = name;
    //        Price = price;
    //    }

    //}
}