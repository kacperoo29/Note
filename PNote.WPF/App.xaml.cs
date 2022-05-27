using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using PNote.Core;
using PNote.Services;
using PNote.ViewModels;
using PNote.Views;
using System;
using System.Windows;

namespace PNote
{
    public partial class App : Application
    {
        public static IServiceProvider? ServiceProvider { get; set; }

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
        }

        private void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<PNoteDbContext>(opt =>
            {
                opt.UseSqlite("Data source=pnote.db");
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
