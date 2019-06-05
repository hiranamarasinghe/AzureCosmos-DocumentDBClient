using DocumentClient.Domain.Dto;
using DocumentClientDemo.Domain.Contracts.Business;
using DocumentClientDemo.Domain.Dto;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DocumentClientDemo.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        // GET api/User/5
        [HttpGet("{id}")]
        public async Task<UserDto> Get(string id)
        {
            return await _userService.GetAsync(id);
        }

        [HttpGet("GetUsers")]
        public async Task<List<UserDto>> GetUsers([FromQuery]string name)
        {
            var result = await _userService.GetUsersAsync(name);
            return result;
        }

        // POST api/User
        [HttpPost]
        public async Task<DocumentDBResultDto> Post([FromBody]UserDto user)
        {
            return await _userService.CreateAsync(user);
        }

        // PUT api/User/5
        [HttpPut]
        public async Task<DocumentDBResultDto> Put([FromBody] UserDto user)
        {
            return await _userService.UpdateAsync(user);
        }

        // DELETE api/User/5
        [HttpDelete("{id}")]
        public async Task Delete(string id)
        {
            await _userService.DeleteAsync(id);
        }
    }
}