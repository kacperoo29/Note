using Microsoft.EntityFrameworkCore;
using PNote.Core;

namespace PNote.Services
{
    public class NoteService : INoteService
    {
        private readonly PNoteDbContext _db;
        private readonly IUserService _userService;

        private DbSet<Note> NoteDbSet { get => _db.Set<Note>(); }
        private DbSet<PinnedNote> PinnedNoteDbSet { get => _db.Set<PinnedNote>(); }

        public NoteService(PNoteDbContext db, IUserService userService)
        {
            _db = db;
            _userService = userService;
        }

        public async Task<List<Note>> GetNotesAsync(CancellationToken cancellationToken = default)
        {
            return await NoteDbSet
                .Where(n => n.User.Id == this._userService.CurrentUser!.Id)
                .ToListAsync(cancellationToken);
        }

        public async Task<Note?> GetNoteAsync(Guid id, CancellationToken cancellationToken = default)
        {
            return await NoteDbSet.FindAsync(id, cancellationToken);
        }

        public async Task<List<Note>> GetPinnedNotes(CancellationToken cancellationToken = default)
        {
            return await PinnedNoteDbSet
                .Where(n => n.Note.User.Id == this._userService.CurrentUser!.Id)
                .Select(x => x.Note)
                .ToListAsync(cancellationToken);
        }

        public async Task<Note> AddNoteAsync(Note note, CancellationToken cancellationToken = default)
        {
            note.SetUser(this._userService.CurrentUser);

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

        public async Task<List<Note>> GetNotesByQuery(string query, CancellationToken cancellationToken = default)
        {
            return await NoteDbSet
                .Where(x => (x.Content.Contains(query) || x.Name.Contains(query)) && x.User.Id == this._userService.CurrentUser.Id)
                .ToListAsync();
        }
    }
}
