using System;

namespace PNote.Core
{
    public class PinnedNote
    {
        public Guid Id { get; private set; }
        public double X { get; private set; }
        public double Y { get; private set; }

        public Guid NoteId { get; private set; }
        public virtual Note Note { get; private set; }

        protected PinnedNote() { }

        public PinnedNote(double x, double y, Note note)
        {
            this.NoteId = note.Id;
            this.X = x;
            this.Y = y;
            this.Note = note;
        }

        public void SetPosition(double x, double y)
        {
            this.X = x;
            this.Y = y;
        }
    }
}
