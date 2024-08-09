using BookStore.Entities;
using BookStore.Services;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json.Nodes;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BookStore.Controllers
{
    [Route("books")]
    [ApiController]
    public class BooksController : ControllerBase
    {

        private IBookStoreRepository _bookStoreRepository;
        public struct BookReqBody
        {
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
        public async Task<ActionResult<IEnumerable<Book>>> GetAllBooks()
        {
            var books = await _bookStoreRepository.GetAllBooksAsync();
            return Ok(books);
        }

        // GET api/<BooksController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<BooksController>
        [HttpPost]
        public async Task<ActionResult<Book?>> CreateBook([FromBody] JsonObject json)
        {
            BookReqBody req;
            JsonNode? jNode;
            json.TryGetPropertyValue("title", out jNode);
            req.title = jNode.GetValue<string>();

            json.TryGetPropertyValue("price", out jNode);
            req.price = jNode.GetValue<float>();

            json.TryGetPropertyValue("publication_date", out jNode);
            req.publication_date = jNode.GetValue<DateTime>();

            json.TryGetPropertyValue("author_id", out jNode);
            req.author_id = jNode.GetValue<int>();

            var book = await _bookStoreRepository.CreateBookAsync(req.title, req.author_id, req.price, req.publication_date);
            return Ok(book);

        }

        // PUT api/<BooksController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<BooksController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
