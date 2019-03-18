using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using BookShelf.Repository;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using Microsoft.AspNetCore.Http;

namespace BookShelf.Controllers
{
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class BookShelfController : ControllerBase
    {
        private readonly BookShelfRepository _repository;
        private readonly IMapper _mapper;

        public BookShelfController(BookShelfRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<IEnumerable<Models.BookShelf>>> GetBookShelves()
        {
            var bookShelvesEntities = await _repository.GetBookShelves().ToListAsync();
            var bookShelves = _mapper.Map<List<Models.BookShelf>>(bookShelvesEntities);
            return bookShelves;
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<Models.BookShelfDetails>> GetBookShelf(string id)
        {
            var bookShelveEntity = await _repository.GetBookShelf(id).FirstOrDefaultAsync();
            if (bookShelveEntity is null)
            {
                return NotFound();
            }

            var bookShelf = _mapper.Map<Models.BookShelfDetails>(bookShelveEntity);
            return bookShelf;
        }
    }
}