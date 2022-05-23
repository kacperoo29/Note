using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PNote.Core
{
    public interface INoteService
    {
        List<Note> GetNotes();
    }
}
