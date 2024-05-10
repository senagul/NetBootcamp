using NetBootcamp.API.DTOs;
using NetBootcamp.API.Users.DTOs;
using System.Collections.Immutable;

namespace NetBootcamp.API.Users
{
    public interface IUserService
    {
        ResponseModelDto<ImmutableList<UserDto>> GetAll();
        ResponseModelDto<UserDto?> GetById(int id);
        ResponseModelDto<int> Create(UserCreateRequestDto request);
        ResponseModelDto<NoContent> Update(int userId, UserUpdateRequestDto request);

        ResponseModelDto<NoContent> Delete(int id);
    }
}