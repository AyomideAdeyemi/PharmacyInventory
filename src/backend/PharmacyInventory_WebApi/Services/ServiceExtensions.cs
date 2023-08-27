using PharmacyInventory_Infrastructure.UnitOfWorkManager;

namespace PharmacyInventory_WebApi.Services
{
    public static class ServiceExtensions
    {
        public static void ConfigureIUnitOfWork(this IServiceCollection services) =>
         services.AddScoped<IUnitOfWork, UnitOfWork>();
        
    }
}
