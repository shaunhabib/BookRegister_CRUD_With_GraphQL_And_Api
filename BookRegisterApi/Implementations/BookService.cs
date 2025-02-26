using BookRegisterApi.DataContext;
using BookRegisterApi.Interfaces;
using BookRegisterApi.Models;
using BookRegisterApi.ViewModels;
using BookRegisterApi.Wrapper;
using Microsoft.EntityFrameworkCore;

namespace BookRegisterApi.Implementations
{
    public class BookService : IBookService
    {
        private readonly ApplicationContext _dbContext;

        public BookService(ApplicationContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Response<int>> Create(BookVm command)
        {
            try
            {
                if (command is null)
                    return Response<int>.Fail("No input given");

                var newBook = new Book
                {
                    Name = command.Name,
                    PublishDate = command.PublishDate
                };

                await _dbContext.Books.AddAsync(newBook);
                await _dbContext.SaveChangesAsync();

                return Response<int>.Success(newBook.Id, "Successfully created");
            }
            catch (Exception ex)
            {
                return Response<int>.Fail($"Failed to create due to error. Error: {ex.Message}");
            }
        }

        public async Task<Response<int>> Update(BookVm command)
        {
            try
            {
                if (command is null)
                    return Response<int>.Fail("No input given");

                var exBook = await _dbContext.Books.FirstOrDefaultAsync(x => x.Id == command.Id);

                if (exBook is null)
                    return Response<int>.Fail("No book found to update");


                exBook.Name = command.Name;
                exBook.PublishDate = command.PublishDate;
                await _dbContext.SaveChangesAsync();

                return Response<int>.Success(exBook.Id, "Successfully updated");
            }
            catch (Exception ex)
            {
                return Response<int>.Fail($"Failed to update due to error. Error: {ex.Message}");
            }
        }

        public async Task<Response<BookVm>> Get(int id)
        {
            try
            {
                var exBook = await _dbContext.Books.FirstOrDefaultAsync(x => x.Id == id);

                if (exBook is null)
                    return Response<BookVm>.Fail("No book found");

                var res = new BookVm
                {
                    Id = exBook.Id,
                    Name = exBook.Name,
                    PublishDate = exBook.PublishDate
                };

                return Response<BookVm>.Success(res, "Successfully get book details");
            }
            catch (Exception ex)
            {
                return Response<BookVm>.Fail($"Failed to get due to error. Error: {ex.Message}");
            }
        }

        public async Task<Response<List<BookVm>>> GetAll()
        {
            try
            {
                var books = await _dbContext.Books
                    .Select(b => new BookVm
                    {
                        Id = b.Id,
                        Name = b.Name,
                        PublishDate = b.PublishDate
                    }).ToListAsync();

                return Response<List<BookVm>>.Success(books, "Successfully get all books");
            }
            catch (Exception ex)
            {
                return Response<List<BookVm>>.Fail($"Failed to get due to error. Error: {ex.Message}");
            }
        }

        public async Task<Response<bool>> Delete(int id)
        {
            try
            {
                var exBook = await _dbContext.Books.FirstOrDefaultAsync(x => x.Id == id);

                if (exBook is null)
                    return Response<bool>.Fail("No book found to delete");

                bool userExist = await _dbContext.BookRegisters.AnyAsync(x => x.BookId == exBook.Id);
                if (userExist)
                    return Response<bool>.Fail("This Book already exist in register");

                _dbContext.Books.Remove(exBook);
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
