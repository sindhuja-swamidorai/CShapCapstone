using BookStore.Entities;
using BookStore.Services;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using NuGet.Protocol;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Xml.Linq;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BookStore.Controllers
{
    [Route("genres")]
    [ApiController]
    public class GenresController : ControllerBase
    {
        private IBookStoreRepository _bookStoreRepository;
        public struct GenreReqBody
        {
            public string genre_name;
        };

        public GenresController(IBookStoreRepository bookStoreRepository)
        {
            _bookStoreRepository = bookStoreRepository;
        }

        // GET: api/<GenresController>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Genre>>> GetAllGenres()
        {
            var genres = await _bookStoreRepository.GetAllGenresAsync();
            return Ok(genres);
        }

        // GET api/<GenresController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Genre?>> GetGenreById(int id)
        {
            var genre = await _bookStoreRepository.GetGenreByIdAsync(id);
            if (genre == null)
            {
                return NotFound();
            }
            return Ok(genre);
        }

        // POST api/<GenresController>
        [HttpPost]
        public async Task<ActionResult<Genre?>> CreateGenre([FromBody] JsonObject json)
        {
            JsonNode? jNode;
            string genre_name;
            if (json.TryGetPropertyValue("genre_name", out jNode))
            {
                genre_name = jNode.GetValue<string>();

                var genre = await _bookStoreRepository.CreateGenreAsync(genre_name);
                if (genre != null)
                {
                    return Ok(genre);
                }
                else
                {
                    return BadRequest();
                }
            }
            return BadRequest();
        }

    }
}
