using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PNote.Services
{
    public class PNoteDbContextFactory : IDesignTimeDbContextFactory<PNoteDbContext>
    {
        public PNoteDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<PNoteDbContext>();
            optionsBuilder.UseSqlite("Data source = pnote.db");

            return new PNoteDbContext(optionsBuilder.Options);
        }
    }
}
