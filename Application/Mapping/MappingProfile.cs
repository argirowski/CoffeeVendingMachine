﻿using Application.DTOs;
using Application.Features.Commands.AddCoffee;
using Application.Features.Commands.UpdateCoffee;
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

            CreateMap<AddCoffeeDTO, AddCoffeeCommand>();
            CreateMap<UpdateCoffeeDTO, UpdateCoffeeCommand>();
        }
    }
}
