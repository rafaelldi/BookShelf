using System.Threading.Tasks;
using AutoMapper;
using BookShelf.Models;
using BookShelf.Repository;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using Microsoft.AspNetCore.Http;

namespace BookShelf.Controllers
{
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class BookController : ControllerBase
    {
        private readonly BookShelfRepository _repository;
        private readonly IMapper _mapper;

        public BookController(BookShelfRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<Book>> GetBook(string id)
        {
            var bookEntity = await _repository.GetBook(id).SingleOrDefaultAsync();
            if (bookEntity is null)
            {
                return NotFound();
            }

            var book = _mapper.Map<Book>(bookEntity);
            return book;
        }
    }
}