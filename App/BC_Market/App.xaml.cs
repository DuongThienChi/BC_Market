using BC_Market.Helper;
using BC_Market.Models;
using BC_Market.Services;
using BC_Market.Views;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.ApplicationModel;
using Windows.ApplicationModel.Activation;
using Windows.Foundation;
using Windows.Foundation.Collections;
using DotNetEnv;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace BC_Market
{
    /// <summary>
    /// Provides application-specific behavior to supplement the default Application class.
    /// </summary>
    public partial class App : Application
    {
        /// <summary>
        /// Gets the service provider for dependency injection.
        /// </summary>
        public IServiceProvider Services { get; }

        /// <summary>
        /// Initializes the singleton application object. This is the first line of authored code
        /// executed, and as such is the logical equivalent of main() or WinMain().
        /// </summary>
        public App()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(AppContext.BaseDirectory)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);

            IConfiguration configuration = builder.Build();

            var serviceCollection = new ServiceCollection();
            ConfigureServices(serviceCollection, configuration);

            Services = serviceCollection.BuildServiceProvider();
            this.InitializeComponent();
        }

        /// <summary>
        /// Configures the services for dependency injection.
        /// </summary>
        /// <param name="services">The service collection to configure.</param>
        /// <param name="configuration">The application configuration.</param>
        private void ConfigureServices(IServiceCollection services, IConfiguration configuration)
        {
            // Register configuration from appsettings.json
            services.Configure<MomoOptionModel>(options => configuration.GetSection("MomoAPI").Bind(options));

            // Register services
            services.AddScoped<IMomoService, MomoService>();
            services.AddSingleton(resolver => resolver.GetRequiredService<IOptions<MomoOptionModel>>().Value);
        }

        /// <summary>
        /// Gets a service of the specified type.
        /// </summary>
        /// <typeparam name="T">The type of service to get.</typeparam>
        /// <returns>The service instance.</returns>
        public static T GetService<T>() where T : class
        {
            return (Current as App).Services.GetService<T>();
        }

        /// <summary>
        /// Invoked when the application is launched.
        /// </summary>
        /// <param name="args">Details about the launch request and process.</param>
        protected override async void OnLaunched(Microsoft.UI.Xaml.LaunchActivatedEventArgs args)
        {
            await SupabaseHelper.InitializeAsync();
            m_window = new HomeWindow();
            Frame rootFrame = m_window.Content as Frame;
            NavigationService.Initialize(rootFrame);
            m_window.Activate();
        }

        /// <summary>
        /// Gets the main application window.
        /// </summary>
        public static Window m_window { get; private set; }
    }
}
