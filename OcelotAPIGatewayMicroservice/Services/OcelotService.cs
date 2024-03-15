namespace OcelotAPIGatewayMicroservice.Services
{
    public static class OcelotService
    {
        public static IServiceCollection AddOcelotServices(this IServiceCollection services, ConfigurationManager configuration)
        {
            string baseDirectory = AppContext.BaseDirectory;
            configuration.AddJsonFile(Path.Combine(baseDirectory, "ConfigurationFile/Ocelot.json"), optional: false, reloadOnChange: true);
            configuration.AddJsonFile(Path.Combine(baseDirectory, "ConfigurationFile/Routes.json"), optional: false, reloadOnChange: true);

            services.AddOcelot(configuration)
            .AddCacheManager(x =>
            {
                x.WithDictionaryHandle();
            }).AddPolly();
            return services;
        }
    }
}
