using Microsoft.EntityFrameworkCore;
using PNote.Core;

namespace PNote.Services
{
    public class PNoteDbContext : DbContext
    {
        public DbSet<Note> Notes { get; set; }
        public DbSet<PinnedNote> PinnedNotes { get; set; }
        public DbSet<NoteUser> Users { get; set; }

        public PNoteDbContext(DbContextOptions options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Note>()
                .HasOne(n => n.PinnedNote)
                .WithOne(pn => pn.Note)
                .HasForeignKey<PinnedNote>(pn => pn.NoteId);
        }
    }
}
