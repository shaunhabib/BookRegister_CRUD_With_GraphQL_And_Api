using BookRegisterApi.Interfaces;
using BookRegisterApi.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookRegisterApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class BookRegisterController : ControllerBase
    {
        private readonly IBookRegisterService _bookRegisterService;

        public BookRegisterController(IBookRegisterService bookRegisterService)
        {
            _bookRegisterService = bookRegisterService;
        }

        [HttpPost]
        public async Task<IActionResult> Create(BookRegisterCommand command)
        {
            var res = await _bookRegisterService.Create(command);
            return Ok(res);
        }

        [HttpPost]
        public async Task<IActionResult> Update(BookRegisterCommand command)
        {
            var res = await _bookRegisterService.Update(command);
            return Ok(res);
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            var res = await _bookRegisterService.Delete(id);
            return Ok(res);
        }
    }
}
