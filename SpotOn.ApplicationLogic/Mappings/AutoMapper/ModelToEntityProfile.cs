using AutoMapper;
using SpotOn.ApplicationLogic.Entities.Posts;
using SpotOn.ApplicationLogic.Entities.Tags;
using SpotOn.ApplicationLogic.Entities.Users;
using SpotOn.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace SpotOn.ApplicationLogic.Mappings.AutoMapper
{
    public class ModelToEntityProfile : Profile
    {
        public ModelToEntityProfile()
        {
            ConfigureMapping();
        }

        private void ConfigureMapping()
        {

            CreateMap<Post, PostEntity>();
            CreateMap<PostEntity, Post>();

            CreateMap<Tag, TagEntity>();
            CreateMap<TagEntity, Tag>();

            CreateMap<User, RegisterUserEntity>();
            CreateMap<RegisterUserEntity, User>();

            CreateMap<User, UserEntity>();
            CreateMap<UserEntity, User>();


        }
    }
}
