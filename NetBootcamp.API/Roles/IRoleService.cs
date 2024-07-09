
using Bootcamp.Service.SharedDTOs;
using NetBootcamp.API.Roles.DTOs;
using System.Collections.Immutable;

namespace NetBootcamp.API.Roles
{
    public interface IRoleService
    {
        ResponseModelDto<ImmutableList<RoleDto>> GetAll();
        ResponseModelDto<RoleDto?> GetById(int id);
        ResponseModelDto<int> Create(RoleCreateRequestDto request);
        ResponseModelDto<NoContent> Update(int roleId, RoleUpdateRequestDto request);

        ResponseModelDto<NoContent> Delete(int id);
    }
}