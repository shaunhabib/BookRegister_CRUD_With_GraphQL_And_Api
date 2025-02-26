
using BookRegisterApi.ViewModels;
using BookRegisterApi.Wrapper;

namespace BookRegisterApi.Interfaces
{
    public interface IBookService
    {
        Task<Response<int>> Create(BookVm command);
        Task<Response<int>> Update(BookVm command);
        Task<Response<BookVm>> Get(int id);
        Task<Response<List<BookVm>>> GetAll();
        Task<Response<bool>> Delete(int id);
    }
}
