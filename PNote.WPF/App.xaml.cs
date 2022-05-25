using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using PNote.Core;
using PNote.Services;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace PNote
{
    public partial class App : Application
    {
        private readonly IServiceProvider _serviceProvider;

        public App()
        {
            IServiceCollection services = new ServiceCollection();
            this.ConfigureServices(services);
            this._serviceProvider = services.BuildServiceProvider();

            using var scope = this._serviceProvider.CreateScope();
            var dbContext = scope.ServiceProvider.GetRequiredService<PNoteDbContext>();
            dbContext.Database.Migrate();
        }

        private void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<PNoteDbContext>(opt =>
            {
                opt.UseSqlite("Data source=pnote.db");
            });

            services.AddScoped<INoteService, NoteService>();

            services.AddSingleton<MainWindow>();
        }

        private void OnStartup(object sender, StartupEventArgs e)
        {
            this._serviceProvider.GetService<MainWindow>()?.Show();
        }
    }
}
