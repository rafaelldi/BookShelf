using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using BookShelf.Models;
using BookShelf.Repository;
using BookShelf.Repository.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

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
            var bookShelvesEntities = await _repository.GetBookShelvesAsync();
            var bookShelves = _mapper.Map<List<Models.BookShelf>>(bookShelvesEntities);
            return bookShelves;
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<BookShelfDetails>> GetBookShelf(Guid id)
        {
            var bookShelveEntity = await _repository.GetBookShelfAsync(id);
            if (bookShelveEntity is null)
            {
                return NotFound();
            }

            var bookShelf = _mapper.Map<BookShelfDetails>(bookShelveEntity);
            return bookShelf;
        }
        
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<Models.BookShelf>> CreateBookShelf(Models.BookShelf bookShelf, ApiVersion apiVersion)
        {
            if (bookShelf.Id != Guid.Empty)
            {
                return BadRequest();
            }
            
            var id = Guid.NewGuid();
            bookShelf.Id = id;

            var bookShelfEntity = _mapper.Map<BookShelfEntity>(bookShelf);
            await _repository.AddBookShelfAsync(bookShelfEntity);
            return CreatedAtAction(nameof(GetBookShelf), new {id, version = apiVersion.ToString()}, bookShelf);
        }

        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<Models.BookShelf>> DeleteBookShelf(Guid id)
        {
            if (id == Guid.Empty)
            {
                return BadRequest();
            }

            var bookShelfExists = await _repository.ExistsBookShelfAsync(id);
            if (!bookShelfExists)
            {
                return NotFound();
            }

            await _repository.DeleteBookShelfAsync(id);
            return NoContent();
        }
    }
}