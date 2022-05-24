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
                new Note("Note1", "Description1", DateTime.Now.AddDays(1)),
                new Note("Note2", "Description2", DateTime.Now.AddDays(2)),
                new Note("Note3", "Description3", DateTime.Now.AddDays(3)),
                new Note("Note4", "Description4", DateTime.Now.AddDays(4)),
            };
        }
    }
}
