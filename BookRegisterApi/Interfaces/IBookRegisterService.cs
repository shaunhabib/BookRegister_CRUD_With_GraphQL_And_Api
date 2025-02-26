using BookRegisterApi.ViewModels;
using BookRegisterApi.Wrapper;

namespace BookRegisterApi.Interfaces
{
    public interface IBookRegisterService
    {
        Task<Response<int>> Create(BookRegisterCommand command);
        Task<Response<int>> Update(BookRegisterCommand command);
        Task<Response<List<BookRegisterVm>>> GetAll();
        Task<Response<bool>> Delete(int id);
    }
}
