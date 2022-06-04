using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace PNote.Core
{
    public interface INoteService
    {
        Task<List<Note>> GetNotesAsync(CancellationToken cancellationToken = default);
        Task<Note> GetNoteAsync(Guid id, CancellationToken cancellationToken = default);
        Task<List<Note>> GetPinnedNotes(CancellationToken cancellationToken = default);
        Task<Note> AddNoteAsync(Note note, CancellationToken token = default);
        Task<Note> EditNoteAsync(Note note, CancellationToken cancellationToken = default);
        Task RemoveNoteAsync(Note note, CancellationToken cancellationToken = default);
        Task<List<Note>> GetNotesByQuery(string query, CancellationToken cancellationToken = default);
    }
}
