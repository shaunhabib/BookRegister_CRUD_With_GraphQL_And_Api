using BookRegisterApi.DataContext;
using BookRegisterApi.Interfaces;
using BookRegisterApi.Models;
using BookRegisterApi.ViewModels;
using BookRegisterApi.Wrapper;
using Microsoft.EntityFrameworkCore;

namespace BookRegisterApi.Implementations
{
    public class BookRegisterService : IBookRegisterService
    {
        private readonly ApplicationContext _dbContext;

        public BookRegisterService(ApplicationContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Response<int>> Create(BookRegisterCommand command)
        {
            try
            {
                #region Validation
                if (command is null)
                    return Response<int>.Fail("No input given");

                var exUser = await _dbContext.Users.FirstOrDefaultAsync(x => x.Id == command.UserId);
                if (exUser is null)
                    return Response<int>.Fail("No user found");

                var exBook = await _dbContext.Books.FirstOrDefaultAsync(x => x.Id == command.BookId);
                if (exBook is null)
                    return Response<int>.Fail("No book found");

                var exRegisters = await _dbContext.BookRegisters.AnyAsync(x => x.userId == command.UserId && x.BookId == command.BookId);
                if (exRegisters)
                    return Response<int>.Fail("Already registerd");
                #endregion

                var newRegister = new BookRegister
                {
                    userId = command.UserId,
                    BookId = command.BookId
                };

                await _dbContext.BookRegisters.AddAsync(newRegister);
                await _dbContext.SaveChangesAsync();

                return Response<int>.Success(newRegister.Id, "Successfully created");
            }
            catch (Exception ex)
            {
                return Response<int>.Fail($"Failed to create due to error. Error: {ex.Message}");
            }
        }

        public async Task<Response<int>> Update(BookRegisterCommand command)
        {
            try
            {
                #region Validation
                if (command is null)
                    return Response<int>.Fail("No input given");
                var registerrepo = _dbContext.BookRegisters.AsQueryable();

                var exRegisters = await registerrepo.AnyAsync(x => x.userId == command.UserId && x.BookId == command.BookId);
                if (exRegisters)
                    return Response<int>.Fail("Already registerd");

                var exRegister = await registerrepo.FirstOrDefaultAsync(x => x.Id == command.Id);
                if (exRegister is null)
                    return Response<int>.Fail("No register found");

                var exUser = await _dbContext.Users.FirstOrDefaultAsync(x => x.Id == command.UserId);
                if (exUser is null)
                    return Response<int>.Fail("No user found");

                var exBook = await _dbContext.Books.FirstOrDefaultAsync(x => x.Id == command.BookId);
                if (exBook is null)
                    return Response<int>.Fail("No book found");
                #endregion

                exRegister.userId = command.UserId;
                exRegister.BookId = command.BookId;

                await _dbContext.SaveChangesAsync();

                return Response<int>.Success(exRegister.Id, "Successfully updated");
            }
            catch (Exception ex)
            {
                return Response<int>.Fail($"Failed to update due to error. Error: {ex.Message}");
            }
        }

        public async Task<Response<List<BookRegisterVm>>> GetAll()
        {
            try
            {
                var users = await _dbContext.BookRegisters
                    .Include(i => i.User)
                    .Include(i => i.Book)
                    .Select(b => new BookRegisterVm
                    {
                        Id = b.Id,
                        User = new UserVm
                        {
                            Id = b.userId,
                            Name = b.User.Name,
                            Email = b.User.Email
                        },
                        Book = new BookVm
                        {
                            Id = b.BookId,
                            Name = b.Book.Name,
                            PublishDate = b.Book.PublishDate
                        }
                    }).ToListAsync();

                return Response<List<BookRegisterVm>>.Success(users, "Successfully get all registers");
            }
            catch (Exception ex)
            {
                return Response<List<BookRegisterVm>>.Fail($"Failed to get due to error. Error: {ex.Message}");
            }
        }

        public async Task<Response<bool>> Delete(int id)
        {
            try
            {
                var exRegister = await _dbContext.BookRegisters.FirstOrDefaultAsync(x => x.Id == id);

                if (exRegister is null)
                    return Response<bool>.Fail("No register found to delete");

                _dbContext.BookRegisters.Remove(exRegister);
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
