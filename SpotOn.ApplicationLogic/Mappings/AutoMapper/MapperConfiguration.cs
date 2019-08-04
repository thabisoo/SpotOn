using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;

namespace SpotOn.ApplicationLogic.Mappings.AutoMapper
{
    public class MapperConfig
    {
        public static void Configure()
        {
            Mapper.Initialize(config =>
            {
                config.AddProfile<ModelToEntityProfile>();
                config.AddProfile<EntityToViewModelProfile>();
            });
        }
    }
}
