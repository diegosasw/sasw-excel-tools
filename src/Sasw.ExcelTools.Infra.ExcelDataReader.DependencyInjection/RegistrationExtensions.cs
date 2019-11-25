namespace Sasw.ExcelTools.Infra.ExcelDataReader.DependencyInjection
{
    using Converters;
    using ExcelTools.Converters;
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
