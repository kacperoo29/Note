using Microsoft.EntityFrameworkCore;
using PNote.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PNote.Services
{
    public class NoteService : INoteService
    {
        private readonly PNoteDbContext _db;
        private DbSet<Note> NoteDbSet { get => _db.Set<Note>(); }

        public NoteService(PNoteDbContext db)
        {
            _db = db;
        }

        public async Task<List<Note>> GetNotesAsync(CancellationToken cancellationToken = default)
        {
            return await NoteDbSet.ToListAsync(cancellationToken);
        }

        public async Task<Note?> GetNoteAsync(Guid id, CancellationToken cancellationToken = default)
        {
            return await NoteDbSet.FindAsync(id, cancellationToken);
        }

        public async Task<List<Note>> GetPinnedNotes(CancellationToken cancellationToken = default)
        {
            return await NoteDbSet.Where(x => x.IsPinned).ToListAsync(cancellationToken);
        }

        public async Task<Note> AddNoteAsync(Note note, CancellationToken cancellationToken = default)
        {
            var entity = (await NoteDbSet.AddAsync(note, cancellationToken)).Entity;

            await _db.SaveChangesAsync(cancellationToken);

            return entity;
        }

        public async Task<Note> EditNoteAsync(Note note, CancellationToken cancellationToken = default)
        {
            _db.Entry(note).State = EntityState.Modified;
            await _db.SaveChangesAsync(cancellationToken);

            return note;
        }

        public async Task RemoveNoteAsync(Note note, CancellationToken cancellationToken = default)
        {
            NoteDbSet.Remove(note);

            await _db.SaveChangesAsync(cancellationToken);
        }
    }
}
