using AutoMapper;
using MyApi.DTOs;
using MyApi.Models;

namespace MyApi.Profiles
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Blog, BlogDto>();
            CreateMap<BlogDto, Blog>();
            CreateMap<Blog, BlogDetailDto>();
            CreateMap<BlogDetailDto, Blog>();

            CreateMap<Comment, CommentDto>();
        }
    }
}