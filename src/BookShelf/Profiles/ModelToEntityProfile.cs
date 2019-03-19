using AutoMapper;
using BookShelf.Models;
using BookShelf.Repository.Entities;

namespace BookShelf.Profiles
{
    public class ModelToEntityProfile : Profile
    {
        public ModelToEntityProfile()
        {
            CreateMap<Book, BookEntity>();
        }
    }
}