using System;
using System.Collections.Generic;

namespace PNote.Core
{
    public class NoteUser
    {
        public Guid Id { get; private set; }
        public string Name { get; private set; }
        public virtual List<Note> Notes { get; private set; }

        public NoteUser(string name)
        {
            this.Id = Guid.NewGuid();
            this.Name = name;
            this.Notes = new List<Note>();
        }
    }
}
