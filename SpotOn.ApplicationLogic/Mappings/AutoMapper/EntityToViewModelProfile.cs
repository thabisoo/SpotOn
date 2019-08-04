using AutoMapper;
using SpotOn.ApplicationLogic.Entities.Posts;
using SpotOn.ApplicationLogic.Entities.Users;
using SpotOn.ApplicationLogic.ViewModels.Posts;
using SpotOn.ApplicationLogic.ViewModels.Users;
using System;
using System.Collections.Generic;
using System.Text;

namespace SpotOn.ApplicationLogic.Mappings.AutoMapper
{
    public class EntityToViewModelProfile : Profile
    {
        public EntityToViewModelProfile()
        {
            ConfigureMapping();
        }

        private void ConfigureMapping()
        {

            CreateMap<PostEntity, PostViewModel>();
            CreateMap<PostViewModel, PostEntity>();

            CreateMap<RegisterUserEntity, RegisterUserViewModel>();
            CreateMap<RegisterUserViewModel, RegisterUserEntity>();

            CreateMap<LogInEntity, LogInViewModel>();
            CreateMap<LogInViewModel, LogInEntity>();
        }
    }
}
