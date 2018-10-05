using AssetManagement.Models;
using AutoMapper;
using Data.Entities;

namespace AssetManagement.AutoMapper
{
    public class AutoMapperConfiguration
    {
        public static void Configure()
        {
            Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<Product, ProductViewModel>();
            });
        }
    }
}