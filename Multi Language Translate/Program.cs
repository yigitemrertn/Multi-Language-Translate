using Microsoft.Extensions.DependencyInjection;
using Multi_Language_Translate.Interfaces;
using Multi_Language_Translate.Services;

namespace Multi_Language_Translate
{
    internal static class Program
    {
        [STAThread]
        static void Main()
        {
            // Configure Dependency Injection
            var services = new ServiceCollection();
            ConfigureServices(services);
            var serviceProvider = services.BuildServiceProvider();

            ApplicationConfiguration.Initialize();

            // Create main form with dependency injection
            var mainForm = serviceProvider.GetRequiredService<Form1>();
            Application.Run(mainForm);
        }

        private static void ConfigureServices(ServiceCollection services)
        {
            // Register HttpClient for translation service
            services.AddHttpClient<ITranslator, TranslationService>(client =>
            {
                client.Timeout = TimeSpan.FromSeconds(30);
            });

            // Register main form
            services.AddSingleton<Form1>();
        }
    }
}
