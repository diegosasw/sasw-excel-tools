namespace Sasw.ExcelTools.DependencyInjection
{
    using Contracts;
    using Contracts.Converters;
    using Converters;
    using Microsoft.Extensions.DependencyInjection;

    public static class RegistrationExtensions
    {
        public static IServiceCollection AddExcelTools(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddSingleton<IExcelConverter, ExcelConverter>();

            return serviceCollection;
        }
    }
}
