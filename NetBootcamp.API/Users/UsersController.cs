using Microsoft.AspNetCore.Mvc;
using NetBootcamp.API.Controllers;
using NetBootcamp.API.DTOs;
using NetBootcamp.API.Users.DTOs;

namespace NetBootcamp.API.Users
{
    public class UsersController : CustomBaseController
    {

        private readonly IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }


        //baseUrl/api/users
        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(_userService.GetAll());
        }

        [HttpGet("{userId}")]
        public IActionResult GetById(int userId)
        {
            return CreateActionResult(_userService.GetById(userId));
        }


        // complex type => class,record,struct => request body as Json
        // simple type => int,string,decimal => query string by default / route data

        [HttpPost]
        public IActionResult Create(UserCreateRequestDto request)
        {
            var result = _userService.Create(request);

            return CreateActionResult(result, nameof(GetById), new { userId = result.Data });
        }

        // PUT localhost/api/users/10
        [HttpPut("{userId}")]
        public IActionResult Update(int userId, UserUpdateRequestDto request)
        {
            return CreateActionResult(_userService.Update(userId, request));
        }


        //// PUT api/products   
        //[HttpPut]
        //public IActionResult Update2(ProductUpdateRequestDto request)
        //{
        //    _productService.Update(request);

        //    return NoContent();
        //}

        [HttpDelete("{userId}")]
        public IActionResult Delete(int userId)
        {
            return CreateActionResult(_userService.Delete(userId));
        }
    }
}