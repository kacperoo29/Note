using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using PNote.Core;
using PNote.Services;
using PNote.Styles;
using PNote.ViewModels;
using PNote.Views;
using System;
using System.Windows;

namespace PNote
{
    public partial class App : Application
    {
        public static IServiceProvider? ServiceProvider { get; set; }

        public StyleType CurrentStyle { get; set; }

        public App()
        {
            IServiceCollection services = new ServiceCollection();
            this.ConfigureServices(services);
            ServiceProvider = services.BuildServiceProvider();

            using var scope = ServiceProvider.CreateScope();
            var dbContext = scope.ServiceProvider.GetRequiredService<PNoteDbContext>();
            try
            {
                dbContext.Database.Migrate();
            }
            catch (Exception)
            {
            }

            this.CurrentStyle = StyleType.Light;
        }

        public void ChangeTheme(StyleType style)
        {
            this.Resources.MergedDictionaries.Clear();
            this.Resources.MergedDictionaries.Add(this.CreateResourceDict(style));
            this.CurrentStyle = style;
        }

        private ResourceDictionary CreateResourceDict(StyleType style)
        {
            var resourceDictionary = new ResourceDictionary();
            string path;
            switch (style)
            {
                case StyleType.Light:
                    path = "/Styles/LightMode.xaml";                    
                    break;
                case StyleType.Dark:
                    path = "/Styles/DarkMode.xaml";
                    break;
                default:
                    path = "/Styles/LightMode.xaml";
                    break;
            }

            resourceDictionary.Source = new Uri(path, UriKind.Relative);

            return resourceDictionary;
        }

        private void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<PNoteDbContext>(opt =>
            {
                opt
                    .UseLazyLoadingProxies()
                    .UseSqlite("Data source=pnote.db");
            });

            services.AddScoped<INoteService, NoteService>();

            services.AddSingleton<MainWindow>();
            services.AddTransient<MainWindowViewModel>();

            services.AddTransient<StickyNoteView>();
            services.AddTransient<StickyNoteViewModel>();
        }

        private void OnStartup(object sender, StartupEventArgs e)
        {
            ServiceProvider?.GetService<MainWindow>()?.Show();
        }
    }
}
