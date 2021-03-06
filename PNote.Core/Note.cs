using System;

namespace PNote.Core
{
    public class Note
    {
        public Guid Id { get; private set; }
        public string Name { get; set; }
        public string Content { get; set; }
        public DateTime DateAdded { get; private set; }
        public DateTime Deadline { get; set; }

        public virtual PinnedNote PinnedNote { get; private set; }
        public virtual NoteUser User { get; private set; }

        protected Note()
        {
            this.Id = Guid.NewGuid();
            this.DateAdded = DateTime.Now;
        }

        public Note(string name, string content, DateTime deadline)
            : this()
        {
            this.Name = name;
            this.Content = content;
            this.Deadline = deadline;
        }

        public void SetUser(NoteUser user)
        {
            this.User = user;
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
