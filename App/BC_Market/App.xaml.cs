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
        /// Initializes the singleton application object.  This is the first line of authored code
        /// executed, and as such is the logical equivalent of main() or WinMain().
        /// </summary>
        public IServiceProvider Services { get; }
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
        private void ConfigureServices(IServiceCollection services, IConfiguration configuration)
        {
            // Đăng ký cấu hình từ appsettings.json

            var section = configuration.GetSection("MomoAPI");



            services.Configure<MomoOptionModel>(options => configuration.GetSection("MomoAPI").Bind(options));

            // Đăng ký các dịch vụ
            services.AddScoped<IMomoService, MomoService>();
            services.AddSingleton(resolver => resolver.GetRequiredService<IOptions<MomoOptionModel>>().Value);

            // Đăng ký ViewModels nếu cần
            //services.AddSingleton<PaymentViewModel>();
            //services.AddSingleton<PaymentView>();
        }

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

        public static Window m_window { get; private set; }
    }
}
