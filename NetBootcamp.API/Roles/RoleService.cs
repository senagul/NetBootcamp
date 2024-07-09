using System.Collections.Immutable;
using System.Net;
using Bootcamp.Service.SharedDTOs;
using Microsoft.AspNetCore.Mvc;
using NetBootcamp.API.Roles.DTOs;

namespace NetBootcamp.API.Roles
{
    public class RoleService(IRoleRepository roleRepository) : IRoleService
    {
        //private readonly IProductRepository _productRepository;

        //public ProductService(IProductRepository productRepository)
        //{
        //    _productRepository = productRepository;

        //}


        public ResponseModelDto<ImmutableList<RoleDto>> GetAll()
        {
            var roleList = roleRepository.GetAll().Select(role => new RoleDto(
                role.Id,
                role.Name
            )).ToImmutableList();


            return ResponseModelDto<ImmutableList<RoleDto>>.Success(roleList);
        }

        public ResponseModelDto<RoleDto?> GetById(int id)
        {
            var hasRole = roleRepository.GetById(id);

            if (hasRole is null)
            {
                return ResponseModelDto<RoleDto?>.Fail("Rol bulunamadı", HttpStatusCode.NotFound);
            }


            var newDto = new RoleDto(
                hasRole.Id,
                hasRole.Name
            );

            return ResponseModelDto<RoleDto?>.Success(newDto);
        }

        // write Add Method
        public ResponseModelDto<int> Create(RoleCreateRequestDto request)
        {
            var newRole = new Role
            {
                Id = roleRepository.GetAll().Count + 1,
                Name = request.Name,
                Created = DateTime.Now
            };

            roleRepository.Create(newRole);

            return ResponseModelDto<int>.Success(newRole.Id, HttpStatusCode.Created);
        }

        // write update method

        public ResponseModelDto<NoContent> Update(int roleId, RoleUpdateRequestDto request)
        {
            var hasRole= roleRepository.GetById(roleId);

            if (hasRole is null)
            {
                return ResponseModelDto<NoContent>.Fail("Güncellenmeye çalışılan rol bulunamadı.",
                    HttpStatusCode.NotFound);
            }

            var updatedRole = new Role
            {
                Id = roleId,
                Name = request.Name,   
                Created = hasRole.Created
            };

            roleRepository.Update(updatedRole);

            return ResponseModelDto<NoContent>.Success(HttpStatusCode.NoContent);
        }


        public ResponseModelDto<NoContent> Delete(int id)
        {
            var hasRole = roleRepository.GetById(id);

            if (hasRole is null)
            {
                return ResponseModelDto<NoContent>.Fail("Silinmeye çalışılan rol bulunamadı.",
                    HttpStatusCode.NotFound);
            }


            roleRepository.Delete(id);

            return ResponseModelDto<NoContent>.Success(HttpStatusCode.NoContent);
        }
    }
}