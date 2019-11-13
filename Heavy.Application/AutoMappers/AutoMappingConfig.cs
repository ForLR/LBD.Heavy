using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;

namespace Heavy.Application.AutoMappers
{
    public class AutoMappingConfig
    {
        public static MapperConfiguration RegisterAuto()
        {
            return new MapperConfiguration(x=> 
            {
                x.AddProfile(new UserViewModelMappingProfile());
            });
        }
    }
}
