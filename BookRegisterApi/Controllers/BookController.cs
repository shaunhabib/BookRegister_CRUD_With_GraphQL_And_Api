using BookRegisterApi.Interfaces;
using BookRegisterApi.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookRegisterApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class BookController : ControllerBase
    {

        private readonly IBookService _bookService;

        public BookController(IBookService bookService)
        {
            _bookService = bookService;
        }

        [HttpPost]
        public async Task<IActionResult> Create(BookVm command)
        {
            var res = await _bookService.Create(command);
            return Ok(res);
        }

        [HttpPost]
        public async Task<IActionResult> Update(BookVm command)
        {
            var res = await _bookService.Update(command);
            return Ok(res);
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            var res = await _bookService.Delete(id);
            return Ok(res);
        }
    }
}
