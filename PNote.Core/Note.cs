using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PNote.Core
{
    public class Note
    {
        public Guid Id { get; private set; }
        public string Name { get; private set; }
        public string Content { get; private set; }
        public DateTime DateAdded { get; private set; }
        public DateTime Deadline { get; private set; }

        public Note(string name, string content, DateTime deadline)
        {
            this.Id = Guid.NewGuid();
            this.Name = name;
            this.Content = content;
            this.DateAdded = DateTime.Now;
            this.Deadline = deadline;
        }
    }
}
