using BookRegisterApi.DataContext;
using BookRegisterApi.Interfaces;
using BookRegisterApi.Models;
using BookRegisterApi.ViewModels;
using BookRegisterApi.Wrapper;
using Microsoft.EntityFrameworkCore;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace BookRegisterApi.Implementations
{
    public class UserService : IUserService
    {
        private readonly ApplicationContext _dbContext;

        public UserService(ApplicationContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Response<int>> Create(UserVm command)
        {
            try
            {
                if (command is null)
                    return Response<int>.Fail("No input given");

                var newUser = new User
                {
                    Name = command.Name,
                    Email = command.Email,
                    Password = BCrypt.Net.BCrypt.HashPassword(command.Password),
                    CreationDate = DateTime.Now
                };

                await _dbContext.Users.AddAsync(newUser);
                await _dbContext.SaveChangesAsync();

                return Response<int>.Success(newUser.Id, "Successfully created");
            }
            catch (Exception ex)
            {
                return Response<int>.Fail($"Failed to create due to error. Error: {ex.Message}");
            }
        }


        public async Task<Response<int>> Update(UserVm command)
        {
            try
            {
                if (command is null)
                    return Response<int>.Fail("No input given");

                var exUser = await _dbContext.Users.FirstOrDefaultAsync(x => x.Id == command.Id);

                if (exUser is null)
                    return Response<int>.Fail("No User found to update");


                exUser.Name = command.Name;
                exUser.Email = command.Email;
                exUser.Password = BCrypt.Net.BCrypt.HashPassword(command.Password);
                await _dbContext.SaveChangesAsync();

                return Response<int>.Success(exUser.Id, "Successfully updated");
            }
            catch (Exception ex)
            {
                return Response<int>.Fail($"Failed to update due to error. Error: {ex.Message}");
            }
        }


        public async Task<Response<UserVm>> Get(int id)
        {
            try
            {
                var exUser = await _dbContext.Users.FirstOrDefaultAsync(x => x.Id == id);

                if (exUser is null)
                    return Response<UserVm>.Fail("No user found");

                var res = new UserVm
                {
                    Id = exUser.Id,
                    Name = exUser.Name,
                    Email = exUser.Email
                };

                return Response<UserVm>.Success(res, "Successfully get user details");
            }
            catch (Exception ex)
            {
                return Response<UserVm>.Fail($"Failed to get due to error. Error: {ex.Message}");
            }
        }

        public async Task<Response<List<UserVm>>> GetAll()
        {
            try
            {
                var users = await _dbContext.Users
                    .Select(b => new UserVm
                    {
                        Id = b.Id,
                        Name = b.Name,
                        Email = b.Email
                    }).ToListAsync();

                return Response<List<UserVm>>.Success(users, "Successfully get all users");
            }
            catch (Exception ex)
            {
                return Response<List<UserVm>>.Fail($"Failed to get due to error. Error: {ex.Message}");
            }
        }

        public async Task<Response<bool>> Delete(int id)
        {
            try
            {
                var exUser = await _dbContext.Users.FirstOrDefaultAsync(x => x.Id == id);

                if (exUser is null)
                    return Response<bool>.Fail("No user found to delete");

                bool userExist = await _dbContext.BookRegisters.AnyAsync(x => x.userId == exUser.Id);
                if (userExist)
                    return Response<bool>.Fail("This User already exist in register");

                _dbContext.Users.Remove(exUser);
                await _dbContext.SaveChangesAsync();

                return Response<bool>.Success(true, "Successfully deleted");
            }
            catch (Exception ex)
            {
                return Response<bool>.Fail($"Failed to delete due to error. Error: {ex.Message}");
            }
        }
    }
}
