using AutoMapper;
using BookShelf.Models;
using BookShelf.Repository.Entities;

namespace BookShelf.Profiles
{
    public class EntityToModelProfile : Profile
    {
        public EntityToModelProfile()
        {
            CreateMap<BookEntity, Book>();
            CreateMap<BookShelfEntity, Models.BookShelf>();
            CreateMap<BookShelfEntity, BookShelfDetails>();
        }
    }
}