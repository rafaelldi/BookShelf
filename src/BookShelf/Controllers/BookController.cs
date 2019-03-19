using System;
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
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<Book>> GetBook(Guid id)
        {
            if (id == Guid.Empty)
            {
                return BadRequest();
            }
            
            var bookEntity = await _repository.GetBookAsync(id);
            if (bookEntity is null)
            {
                return NotFound();
            }

            var book = _mapper.Map<Book>(bookEntity);
            return book;
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<Book>> CreateBook(Guid bookShelfId, Book book, ApiVersion apiVersion)
        {
            if (bookShelfId == Guid.Empty)
            {
                return BadRequest();
            }

            if (book.Id != Guid.Empty)
            {
                return BadRequest();
            }
            
            var id = Guid.NewGuid();
            book.Id = id;
            
            var bookEntity = _mapper.Map<BookEntity>(book);
            await _repository.AddBookAsync(bookShelfId, bookEntity);
            return CreatedAtAction(nameof(GetBook), new {id, version = apiVersion.ToString()}, book);
        }

        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<Book>> DeleteBook(Guid bookShelfId, Guid bookId)
        {
            if (bookShelfId == Guid.Empty || bookId == Guid.Empty)
            {
                return BadRequest();
            }

            var bookExists = await _repository.ExistsBookAsync(bookId);
            if (!bookExists)
            {
                return NotFound();
            }

            await _repository.DeleteBookAsync(bookShelfId, bookId);
            return NoContent();
        }
    }
}