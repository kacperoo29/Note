using System;
using System.Collections.Generic;

namespace PNote.Core
{
    public class NoteUser
    {
        public Guid Id { get; private set; }
        public string Name { get; private set; }
        public byte[] Avatar { get; private set; }
        public virtual List<Note> Notes { get; private set; }

        protected NoteUser()
        {
            this.Notes = new List<Note>();
            this.Id = Guid.NewGuid();
        }

        public NoteUser(string name, byte[] avatar = null)
            : this()
        {
            this.Name = name;
            if (avatar != null)
                this.Avatar = avatar;
            else
                this.Avatar = DefaultAvatarConst.DefaultAvatarBytes;
        }
    }
}
