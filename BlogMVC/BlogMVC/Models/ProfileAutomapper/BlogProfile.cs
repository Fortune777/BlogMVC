using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlogMVC.Models.DTO;
using BlogMVC.Models.Entity;
using AutoMapper;


namespace BlogMVC.Models.ProfileAutomapper
{
    public class BlogProfile : Profile
    {
        public BlogProfile()
        {
            CreateMap<Blog, BlogDto>().ReverseMap();
        }
    }
}
