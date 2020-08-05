using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using AutoMapper;
using Supermarket.API.Domain.Models;
using Supermarket.API.Resources;
using Supermarket.API.Utilies;

namespace Supermarket.API.Mapping
{
    public class ResourceToModelProfile : Profile
    {
        public ResourceToModelProfile()
        {
            CreateMap<SaveCategoryResource, Category>();

            CreateMap<SaveProductResource, Product>()
                .ForMember(src => src.UnitOfMeasurement,
                            opt => opt.MapFrom(src => EnumEx.GetValueFromDescription<EUnitOfMeasurement>(src.UnitOfMeasurement)));
        }
    }
}
