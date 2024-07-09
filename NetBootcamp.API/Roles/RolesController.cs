using Microsoft.AspNetCore.Mvc;
using NetBootcamp.API.Controllers;
using NetBootcamp.API.Roles.DTOs;

namespace NetBootcamp.API.Roles
{
    public class RolesController : CustomBaseController
    {
        //private readonly IProductService _productService = ProductServiceFactory.GetService();

        private readonly IRoleService _roleService;

        public RolesController(IRoleService roleService)
        {
            _roleService = roleService;
        }


        //baseUrl/api/products
        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(_roleService.GetAll());
        }

        [HttpGet("{roleId}")]
        public IActionResult GetById(int roleId)
        {
            return CreateActionResult(_roleService.GetById(roleId));
        }


        // complex type => class,record,struct => request body as Json
        // simple type => int,string,decimal => query string by default / route data

        [HttpPost]
        public IActionResult Create(RoleCreateRequestDto request)
        {
            var result = _roleService.Create(request);

            return CreateActionResult(result, nameof(GetById), new { roleId = result.Data });
        }

        // PUT localhost/api/products/10
        [HttpPut("{roleId}")]
        public IActionResult Update(int roleId, RoleUpdateRequestDto request)
        {
            return CreateActionResult(_roleService.Update(roleId, request));
        }


        //// PUT api/products   
        //[HttpPut]
        //public IActionResult Update2(ProductUpdateRequestDto request)
        //{
        //    _productService.Update(request);

        //    return NoContent();
        //}

        [HttpDelete("{roleId}")]
        public IActionResult Delete(int roleId)
        {
            return CreateActionResult(_roleService.Delete(roleId));
        }
    }
}