using Microsoft.EntityFrameworkCore;
using PNote.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PNote.Services
{
    public class PNoteDbContext : DbContext
    {
        public DbSet<Note> Notes { get; set; }

        public PNoteDbContext(DbContextOptions options)
            : base(options)
        {
            Database.EnsureCreated();
        }
    }
}
