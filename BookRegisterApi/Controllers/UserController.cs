using BookRegisterApi.Interfaces;
using BookRegisterApi.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookRegisterApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost]
        public async Task<IActionResult> Create(UserVm command)
        {
            var res = await _userService.Create(command);
            return Ok(res);
        }

        [HttpPost]
        public async Task<IActionResult> Update(UserVm command)
        {
            var res = await _userService.Update(command);
            return Ok(res);
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            var res = await _userService.Delete(id);
            return Ok(res);
        }
    }
}
