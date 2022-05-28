using System;

namespace PNote.Core
{
    public class Note
    {
        public Guid Id { get; private set; }
        public string Name { get; private set; }
        public string Content { get; private set; }
        public DateTime DateAdded { get; private set; }
        public DateTime Deadline { get; private set; }

        public virtual PinnedNote PinnedNote { get; private set; }

        public Note(string name, string content, DateTime deadline)
        {
            this.Id = Guid.NewGuid();
            this.Name = name;
            this.Content = content;
            this.DateAdded = DateTime.Now;
            this.Deadline = deadline;
        }

        public void Pin()
        {
            if (this.PinnedNote != null)
                return;

            this.PinnedNote = new PinnedNote(10, 10, this);
        }

        public void Unpin()
        {
            this.PinnedNote = null;
        }
    }
}
