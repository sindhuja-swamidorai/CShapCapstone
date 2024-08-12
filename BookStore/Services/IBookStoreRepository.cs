using BookStore.Entities;
using System.Runtime.CompilerServices;

namespace BookStore.Services
{
    public interface IBookStoreRepository
    {
        Task<IEnumerable<Book>> GetAllBooksAsync();

        Task<IEnumerable<Author>> GetAllAuthorsAsync();

        Task<IEnumerable<Genre>> GetAllGenresAsync();

        Task<Book?> GetBookByIdAsync(int id);

        Task<Author?> GetAuthorByIdAsync(int id);

        Task<Author?> GetAuthorByNameAsync(string author_name);

        Task<Genre?> GetGenreByIdAsync(int id);

        Task<Genre?> GetGenreByNameAsync(string genre_name);

        Task<Book?> CreateBookAsync(string title, int author_id, float price, DateTime publication_date); 
        Task<Author?> CreateAuthorAsync(string author_name, string biography);

        Task<Genre?> CreateGenreAsync(string genre_name);

        Task<bool> UpdateBookAsync(int id, double price);

        Task<bool> UpdateAuthorAsync(int id, string biography);

        Task<bool> DeleteBookAsync(int id);

        Task<bool> DeleteAuthorAsync(int id);

    }
}
