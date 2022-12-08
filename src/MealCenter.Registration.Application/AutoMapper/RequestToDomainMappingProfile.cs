using AutoMapper;
using MealCenter.Registration.Application.Contracts.Clients;
using MealCenter.Registration.Application.Contracts.Posts;
using MealCenter.Registration.Application.Contracts.Restaurants;
using MealCenter.Registration.Domain.Clients;
using MealCenter.Registration.Domain.Posts;
using MealCenter.Registration.Domain.Restaurants;

namespace MealCenter.Registration.Application.AutoMapper
{
    internal class RequestToDomainMappingProfile : Profile
    {
        public RequestToDomainMappingProfile()
        {
            CreateMap<UpdateClient, Client>();

            CreateMap<CreatePost, Post>();
            CreateMap<UpdatePost, Post>();
            CreateMap<CreatePostComment, PostComment>();
            CreateMap<UpdatePostComment, PostComment>();
            CreateMap<CreatePostReaction, PostReaction>();

            CreateMap<UpdateRestaurant, Restaurant>();
            CreateMap<CreateMenu, Menu>();
            CreateMap<UpdateMenu, Menu>();
            CreateMap<CreateTable, Table>();
            CreateMap<UpdateTable, Table>();
            CreateMap<CreateMenuOption, MenuOption>();
            CreateMap<UpdateMenuOption, MenuOption>();
        }
    }
}
