using BookStore.Services;
using BookStore.Entities;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json.Nodes;
using Newtonsoft.Json;
using Microsoft.IdentityModel.Tokens;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BookStore.Controllers
{
    [Route("authors")]
    [ApiController]
    public class AuthorsController : ControllerBase
    {
        IBookStoreRepository _bookStoreRepository;
        public struct AuthorReqBody
        {
            public string name;
            public string biography;
        };

        public AuthorsController( IBookStoreRepository bookStoreRepository)
        {
            _bookStoreRepository = bookStoreRepository;
        }

        // GET: api/<AuthorsController>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Author>>> GetAllAuthors()
        {
            var authors = await _bookStoreRepository.GetAllAuthorsAsync();
            return Ok(authors);
        }

        // GET api/<AuthorsController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Author?>> GetAuthorById(int id)
        {
            var author = await _bookStoreRepository.GetAuthorByIdAsync(id);
            return Ok(author);
        }

        // POST api/<AuthorsController>
        [HttpPost]
        public async Task<ActionResult<Author?>> CreateAuthor([FromBody] JsonObject json)
        {
            AuthorReqBody req;
            JsonNode? jNode;
            if (json.TryGetPropertyValue("name", out jNode))
            {
                req.name = jNode.GetValue<string>();
            }
            else
            {
                req.name = "xxx";
            }

            if (json.TryGetPropertyValue("biography", out jNode))
            {
                req.biography = jNode.GetValue<string>();
            }
            else
            {
                req.biography = "yyy";
            }

                var author = await _bookStoreRepository.CreateAuthorAsync(req.name, req.biography);
                if (author != null)
                {
                    return Ok(author);
                }
                else
                {
                    return BadRequest();
                }
        }

        // PUT api/<AuthorsController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<AuthorsController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
