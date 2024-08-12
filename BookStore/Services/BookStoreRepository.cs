using BookStore.DbContexts;
using BookStore.Entities;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;
using System.Text.Json.Nodes;

namespace BookStore.Services
{
    public class BookStoreRepository : IBookStoreRepository
    {
        static int currentGenreId = 0;
        static int currentAuthorId = 0;
        private BookStoreContext _bookStoreContext;

        public BookStoreRepository(BookStoreContext bookStoreContext)
        {
            _bookStoreContext = bookStoreContext;
            currentGenreId = bookStoreContext.Genres.Max(g => g.GenreId);
            currentAuthorId = bookStoreContext.Authors.Max(a => a.AuthorId);

        }
        public async Task<IEnumerable<Book>> GetAllBooksAsync()
        {
            var books = await _bookStoreContext.Books.OrderBy(b => b.Title).ToListAsync();
            return books;
        }

        public async Task<IEnumerable<Author>> GetAllAuthorsAsync()
        {
            var authors = await _bookStoreContext.Authors.OrderBy(a => a.AuthorName).ToListAsync();
            return authors;
        }

        public async Task<IEnumerable<Genre>> GetAllGenresAsync()
        {
            var genres = await _bookStoreContext.Genres.OrderBy(g => g.GenreName).ToListAsync();
            return genres;
        }

        public async Task<Book?> GetBookByIdAsync(int id)
        {
            var book = await _bookStoreContext.Books.Include(b => b.Author).Include(b => b.Genre).Where(b => b.BookId == id).FirstOrDefaultAsync();
            if (book != null)
            {
                book.AuthorName = book.Author.AuthorName;
                book.GenreName = book.Genre.GenreName;
            }
            return book;
        }

        public async Task<Author?> GetAuthorByIdAsync(int id)
        {
            var author = await _bookStoreContext.Authors.Where(a => a.AuthorId == id).FirstOrDefaultAsync();
            return author;
        }

        public async Task<Genre?> GetGenreByIdAsync(int id)
        {
            var genre = await _bookStoreContext.Genres.Where(g => g.GenreId == id).FirstOrDefaultAsync();
            return genre;
        }

        public async Task<Genre?> GetGenreByNameAsync(string genre_name)
        {
            var genre = await _bookStoreContext.Genres.Where(g => g.GenreName == genre_name).FirstOrDefaultAsync();
            return genre;
        }
        public async Task<Book?> CreateBookAsync(string title, int author_id, float price, DateTime publication_date)
        {
            Book bookNew = new Book();
            bookNew.Title = title;
            bookNew.PublicationDate = publication_date;
            bookNew.Price = price;
            bookNew.AuthorId = author_id;
            _bookStoreContext.Add(bookNew);
            await _bookStoreContext.SaveChangesAsync();
            return bookNew;
        }

        public async Task<Author?> CreateAuthorAsync(string author_name, string biography)
        {
            /* Create new Author */
            Author authorNew = new Author();
            authorNew.AuthorId = ++currentAuthorId;
            authorNew.AuthorName = author_name;
            authorNew.Biography = biography;
            _bookStoreContext.Add(authorNew);
            await _bookStoreContext.SaveChangesAsync();
            return authorNew;
        }

        public async Task<Genre?> CreateGenreAsync(string genre_name)
        {
            var genre = await _bookStoreContext.Genres.Where(g => g.GenreName == genre_name).FirstOrDefaultAsync();
            if (genre == null)
            {
                    /* Create new Genre */
                    Genre genreNew = new Genre();
                    genreNew.GenreName = genre_name;
                    genreNew.GenreId = ++currentGenreId;
                    _bookStoreContext.Genres.Add(genreNew);
                    await _bookStoreContext.SaveChangesAsync();
                    return genreNew;
            }
            else
            {
                return null;
            }
        }

        public async Task<bool> UpdateBookAsync(int id, double price)
        {
            var book = await _bookStoreContext.Books.Where(b => b.BookId == id).FirstOrDefaultAsync();
            if (book != null)
            {
                book.Price = price;
                await _bookStoreContext.SaveChangesAsync();
            }
            return true;
        }
        public async Task<bool> UpdateAuthorAsync (int id, string biography)
        {
            var author = await _bookStoreContext.Authors.Where(a => a.AuthorId == id).FirstOrDefaultAsync();
            if (author != null)
            {
                author.Biography = biography;
                await _bookStoreContext.SaveChangesAsync();
            }
            return true;
        }

        public async Task<bool> DeleteBookAsync(int id)
        {
            var book = await _bookStoreContext.Books.Where(b => b.BookId == id).FirstOrDefaultAsync();
            if (book != null)
            {
                _bookStoreContext.Remove<Book>(book);
                await _bookStoreContext.SaveChangesAsync();
            }
            return true;
        }

        public async Task<bool> DeleteAuthorAsync(int id)
        {
            var author = await _bookStoreContext.Authors.Where(a => a.AuthorId == id).FirstOrDefaultAsync();
            if (author != null)
            {
                _bookStoreContext.Remove<Author>(author);
                await _bookStoreContext.SaveChangesAsync();
            }
            return true;
        }

    }
}
