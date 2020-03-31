using AutoMapper;

namespace Hospital.ProductCatalog.BusinessLogic.UnitTests
{
    public class MapperFactory
    {
        public static IMapper Create() 
        {
            var configurationProvider = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<HospitalProductMappingProfile>();
            });

            return configurationProvider.CreateMapper();
        }
    }
}
