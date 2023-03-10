using AutoMapper;
using TaskManagement.Dto;
using TaskManagement.Models;

namespace TaskManagement.Helper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<WorkSpace, WorkSpaceDto>().ReverseMap();
            CreateMap<Section, SectionDto>().ReverseMap();
            CreateMap<User, UserDto>();
            CreateMap<UserDto, User>();
            CreateMap<Models.Task, TaskDto>().ReverseMap();
            CreateMap<Comment, CommentDto>().ReverseMap();  
        }
    }
}
