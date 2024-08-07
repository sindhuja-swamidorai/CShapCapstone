using BookStore.DbContexts;
using BookStore.Entities;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;

namespace BookStore.Services
{
    public class BookStoreRepository : IBookStoreRepository
    {
        private BookStoreContext _bookStoreContext;

        public BookStoreRepository(BookStoreContext bookStoreContext)
        {
            _bookStoreContext = bookStoreContext;

        }
        public async Task<IEnumerable<Book>> GetAllBooks()
        {
            var books = await _bookStoreContext.Books.ToListAsync();
            return books;
        }
    }
}
