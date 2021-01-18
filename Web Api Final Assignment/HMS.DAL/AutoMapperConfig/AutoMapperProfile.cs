using AutoMapper;
using HMS.DAL.Database;
using HMS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMS.DAL.AutoMapperConfig
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Hotelstbl,HotelModel>();
        }
    }
}
