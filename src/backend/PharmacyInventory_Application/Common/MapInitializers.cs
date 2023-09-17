using AutoMapper;
using Microsoft.AspNetCore.Identity;
using PharmacyInventory_Domain.Dtos.Requests;
using PharmacyInventory_Domain.Dtos.Responses;
using PharmacyInventory_Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PharmacyInventory_Application.Common
{
    public class MapInitializers : Profile
    {
        public MapInitializers()
        {
            CreateMap<SupplierRequestDto, Supplier>();
            CreateMap<Supplier, SupplierResponseDto>();

            CreateMap<DrugRequestDto, Drug>();
            CreateMap<Drug, DrugResponseDto>();

            CreateMap<BrandRequestDto, Brand>();
            CreateMap<Brand, BrandResponseDto>();

            CreateMap<GenericNameRequestDto, GenericName>();
            CreateMap<GenericName, GenericNameResponseDto>();

            CreateMap<UnitRequestDto, Unit>();
            CreateMap<Unit, UnitResponseDto>();

            CreateMap<UserRequestDto, User>();
            CreateMap<User, UserResponseDto>();

            CreateMap<IdentityResult, UserResponseDto>();
            CreateMap<UserResponseDto, IdentityResult>();
                       

        }

    }
}
