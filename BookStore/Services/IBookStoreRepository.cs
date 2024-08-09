using BookStore.Entities;

namespace BookStore.Services
{
    public interface IBookStoreRepository
    {
        Task<IEnumerable<Book>> GetAllBooksAsync();

        Task<IEnumerable<Author>> GetAllAuthorsAsync();

        Task<IEnumerable<Genre>> GetAllGenresAsync();

        Task<Book?> GetBookByIdAsync(int id);

        Task<Author?> GetAuthorByIdAsync(int id);

        Task<Genre?> GetGenreByIdAsync(int id);

        Task<Genre?> GetGenreByNameAsync(string genre_name);

        Task<Book?> CreateBookAsync(string title, int author_id, float price, DateTime publication_date); 
        Task<Author?> CreateAuthorAsync(string author_name, string biography);

        Task<Genre?> CreateGenreAsync(string genre_name);

    }
}
