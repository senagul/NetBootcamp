using System.Collections.Immutable;
using System.Net;
using Microsoft.AspNetCore.Mvc;
using NetBootcamp.API.DTOs;
using NetBootcamp.API.Users.DTOs;

namespace NetBootcamp.API.Users
{
    public class UserService(IUserRepository userRepository) : IUserService
    {
        //private readonly IProductRepository _productRepository;

        //public ProductService(IProductRepository productRepository)
        //{
        //    _productRepository = productRepository;

        //}


        public ResponseModelDto<ImmutableList<UserDto>> GetAll()
        {
            var userList = userRepository.GetAll().Select(user => new UserDto(
                user.Id,
                user.Name,
                user.Surname,
                user.Email,
                user.Created.ToShortDateString()
            )).ToImmutableList();


            return ResponseModelDto<ImmutableList<UserDto>>.Success(userList);
        }

        public ResponseModelDto<UserDto?> GetById(int id)
        {
            var hasUser = userRepository.GetById(id);

            if (hasUser is null)
            {
                return ResponseModelDto<UserDto?>.Fail("Kullanıcı bulunamadı", HttpStatusCode.NotFound);
            }


            var newDto = new UserDto(
                hasUser.Id,
                hasUser.Name,
                hasUser.Surname,
                hasUser.Email,
                hasUser.Created.ToShortDateString()
            );

            return ResponseModelDto<UserDto?>.Success(newDto);
        }

        // write Add Method
        public ResponseModelDto<int> Create(UserCreateRequestDto request)
        {
            var newUser = new User
            {
                Id = userRepository.GetAll().Count + 1,
                Name = request.Name,
                Surname = request.Surname,
                Password = request.Password,
                Email = request.Email,
                Created = DateTime.Now
            };

            userRepository.Create(newUser);

            return ResponseModelDto<int>.Success(newUser.Id, HttpStatusCode.Created);
        }

        // write update method

        public ResponseModelDto<NoContent> Update(int userId, UserUpdateRequestDto request)
        {
            var hasUser= userRepository.GetById(userId);

            if (hasUser is null)
            {
                return ResponseModelDto<NoContent>.Fail("Güncellenmeye çalışılan kullanıcı bulunamadı.",
                    HttpStatusCode.NotFound);
            }

            var updatedUser = new User
            {
                Id = userId,
                Name = request.Name,
                Surname = request.Surname,
                Email = request.Email,
                Password = request.Password,    
                Created = hasUser.Created
            };

            userRepository.Update(updatedUser);

            return ResponseModelDto<NoContent>.Success(HttpStatusCode.NoContent);
        }


        public ResponseModelDto<NoContent> Delete(int id)
        {
            var hasUser = userRepository.GetById(id);

            if (hasUser is null)
            {
                return ResponseModelDto<NoContent>.Fail("Silinmeye çalışılan kullanıcı bulunamadı.",
                    HttpStatusCode.NotFound);
            }


            userRepository.Delete(id);

            return ResponseModelDto<NoContent>.Success(HttpStatusCode.NoContent);
        }
    }
}