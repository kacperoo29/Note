using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PNote.Core
{
    public class NoteService : INoteService
    {
        public List<Note> GetNotes()
        {
            return new List<Note>() 
            {
                new Note("Note1", "Note1", DateTime.Now.AddDays(1)),
                new Note("Note2", "Note2", DateTime.Now.AddDays(2)),
                new Note("Note3", "Note3", DateTime.Now.AddDays(3)),
                new Note("Note4", "Note4", DateTime.Now.AddDays(4)),
            };
        }
    }
}
