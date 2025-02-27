﻿using Application.DTOs;
using AutoMapper;
using Domain.Entities;

namespace Application.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<CoffeeType, CoffeeTypeDTO>().ReverseMap();

            CreateMap<CoffeeIngredient, CoffeeIngredientDTO>().ReverseMap();
        }
    }
}
