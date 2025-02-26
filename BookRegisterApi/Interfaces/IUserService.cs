using BookRegisterApi.ViewModels;
using BookRegisterApi.Wrapper;

namespace BookRegisterApi.Interfaces
{
    public interface IUserService
    {
        Task<Response<int>> Create(UserVm command);
        Task<Response<int>> Update(UserVm command);
        Task<Response<UserVm>> Get(int id);
        Task<Response<List<UserVm>>> GetAll();
        Task<Response<bool>> Delete(int id);
    }
}
