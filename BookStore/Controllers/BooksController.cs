using BookStore.Entities;
using BookStore.Services;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using NuGet.Protocol;
using System.Text.Json;
using System.Text.Json.Nodes;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BookStore.Controllers
{
    [Route("books")]
    [ApiController]
    public class BooksController : ControllerBase
    {

        private IBookStoreRepository _bookStoreRepository;
        public struct BookBody
        {
            public int book_id;
            public string title;
            public int author_id;
            public float price;
            public DateTime publication_date;
        }

        public struct ReqBody
        {
            public string book_title;
        };

        public BooksController(IBookStoreRepository bookStoreRepository)
        {
            _bookStoreRepository = bookStoreRepository;
        }


        // GET: books>
        [HttpGet]
        public async Task<ActionResult<JsonArray>> GetAllBooks(string? start, 
            int? genre_id, int? author_id, string? title, bool? count, string? author_name,
            int? limit)
        {
            var books = await _bookStoreRepository.GetAllBooksAsync();

            if (start != null)
            {
                books = books.Where(b => b.Title.StartsWith(start, StringComparison.InvariantCultureIgnoreCase));
            }
            if (genre_id != null)
            {
                books = books.Where(b => b.GenreId == genre_id);
            }
            if (author_id != null)
            {
                books = books.Where(b => b.AuthorId == author_id);
            }
            if(title != null)
            {
                books = books.Where(b => b.Title.Contains(title, StringComparison.InvariantCultureIgnoreCase));
            }

            if (author_name != null)
            {
                var authors = await _bookStoreRepository.GetAllAuthorsAsync();
                authors = authors.Where(a => a.AuthorName.Contains(author_name, StringComparison.InvariantCultureIgnoreCase));
                books = books.Where(b => authors.Contains(b.Author));
            }

            if ((count != null) && (genre_id != null))
            {
                JsonArray result = new JsonArray();
                var genre = await _bookStoreRepository.GetGenreByIdAsync(genre_id.GetValueOrDefault());
                if (genre != null)
                {
                    genre.Count = genre.Books.Count();
                    result.Add(genre);
                    return Ok(result);
                }
                else
                {
                    return NotFound();
                }
            }

            if ((count != null) && (author_id != null))
            {
                JsonArray result = new JsonArray();
                var author = await _bookStoreRepository.GetAuthorByIdAsync(author_id.GetValueOrDefault());
                if (author != null)
                {
                    author.Count = author.Books.Count();
                    result.Add(author);
                    return Ok(result);
                }
                else
                {
                    return NotFound();
                }
            }

            if(limit != null)
            {
                books = books.OrderByDescending(a => a.PublicationDate).Take(limit.GetValueOrDefault());
            }

            return Ok(books);
        }

        // GET api/<BooksController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Book?>> GetBookById(int id)
        {
            var book = await _bookStoreRepository.GetBookByIdAsync(id);
            return Ok(book);
        }

        // POST api/<BooksController>
        [HttpPost]
        public async Task<ActionResult<Book?>> CreateBook([FromBody] JsonObject json)
        {
            BookBody req;
            JsonNode? jNode;
            json.TryGetPropertyValue("title", out jNode);
            if (jNode == null) return BadRequest();
            req.title = jNode.GetValue<string>();

            json.TryGetPropertyValue("price", out jNode);
            if (jNode == null) return BadRequest();
            req.price = float.Parse((jNode.GetValue<string>()));

            json.TryGetPropertyValue("publication_date", out jNode);
            if (jNode == null) return BadRequest();
            req.publication_date = DateTime.Parse(jNode.GetValue<string>());

            json.TryGetPropertyValue("author_id", out jNode);
            if (jNode == null) return BadRequest();
            req.author_id = int.Parse(jNode.GetValue<string>());

            var book = await _bookStoreRepository.CreateBookAsync(req.title, req.author_id, req.price, req.publication_date);
            return Ok(book);

        }

        // PUT api/<BooksController>/5
        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateBook(int id, [FromBody] JsonObject json)
        {
            JsonNode? jNode;
            if (json.TryGetPropertyValue("price", out jNode))
            {
                string value = jNode.GetValue<string>();
                bool result = await _bookStoreRepository.UpdateBookAsync(id, double.Parse(value));
                if (result)
                {
                    return Ok();
                }
                else
                {
                    return BadRequest();
                }
            }
            else
            {
                return BadRequest();
            }
        }

        // DELETE api/<BooksController>/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteBook(int id)
        {
            bool result = await _bookStoreRepository.DeleteBookAsync(id);
            //book.Price = double.Parse(value);
            if (result)
            {
                return Ok();
            }
            else
            {
                return BadRequest();
            }
        }
    }
}
