using BookRegisterApi.Interfaces;
using BookRegisterApi.Models;
using BookRegisterApi.ViewModels;
using System.Threading.Tasks;

namespace BookRegisterApi.GraphQL
{
    public class Query
    {
        public async Task<List<BookVm>> GetBooks([Service] IBookService _bookService)
        {
            var books = await _bookService.GetAll();
            return books.Data;
        }

        public async Task<List<UserVm>> GetUsers([Service] IUserService _userService)
        {
            var users = await _userService.GetAll();
            return users.Data;
        }
        public async Task<List<BookRegisterVm>> GetBookRegisters([Service] IBookRegisterService _registerService)
        {
            var users = await _registerService.GetAll();
            return users.Data;
        }
    }
}
