using AutoMapper;
using Hospital.ProductCatalog.BusinessLogic.Products.Queries;
using Hospital.ProductCatalog.Domain.Entities;
using System;

namespace Hospital.ProductCatalog.BusinessLogic
{
    public class HospitalProductMappingProfile : Profile
    {
        // Because there is only a very small domain model I chose to have
        // a single mapper profile, in more complicated situations this file 
        // could easily be split
        public HospitalProductMappingProfile()
        {
            CreateMap<Product, ProductDTO>();

            // For the outside world a barcode is just a string 
            CreateMap<Barcode, string>().ConvertUsing(src => src.ToString());
            CreateMap<string, Barcode>().ConvertUsing(src => new Barcode { Code = src });

            // The outside world does not care about the int representaion of my 
            // UnitOfMeasurement so i convert ToString(), and back on the way in
            CreateMap<UnitOfMeasurement, string>().ConvertUsing(src => src.ToString());
            CreateMap<string, UnitOfMeasurement>().ConvertUsing(src => Enum.Parse<UnitOfMeasurement>(src, true));
        }
    }
}
