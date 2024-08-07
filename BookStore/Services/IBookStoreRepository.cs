using BookStore.Entities;

namespace BookStore.Services
{
    public interface IBookStoreRepository
    {
        Task<IEnumerable<Book>> GetAllBooks();
    }
}
