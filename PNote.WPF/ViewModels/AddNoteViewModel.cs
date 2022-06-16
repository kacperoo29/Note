using PNote.Core;
using System;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows.Input;

namespace PNote.ViewModels
{
    public class AddNoteViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        private readonly INoteService _noteService;

        private string? _name;
        public string? Name
        {
            get => _name;
            set
            {
                this._name = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Name)));
            }
        }

        private string? _description;
        public string? Description
        {
            get => _description;
            set
            {
                this._description = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Description)));
            }
        }

        private DateTime? _deadline;
        public DateTime? Deadline
        {
            get => _deadline;
            set
            {
                this._deadline = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Deadline)));
            }
        }

        public Note? Note { get; private set; }

        private bool _added = false;
        public bool Added
        {
            get => _added;
            set
            {
                this._added = value;
                this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Added)));
            }
        }

        private ICommand? _addNote;
        public ICommand AddNote
        {
            get => _addNote ?? (_addNote = new CommandHandler((p) => HandleAddNote(p).Wait(), () => true));
        }

        public Action? CloseAction { get; set; }

        public AddNoteViewModel(INoteService noteService)
        {
            this._noteService = noteService;
        }

        private async Task HandleAddNote(object? param)
        {
            if (string.IsNullOrEmpty(this.Name))
                throw new ArgumentNullException(nameof(Name));

            if (string.IsNullOrEmpty(this.Description))
                throw new ArgumentNullException(nameof(Description));

            if (Deadline == null)
                throw new ArgumentNullException(nameof(Deadline));

            if (Deadline < DateTime.Now)
                throw new ArgumentOutOfRangeException("Cannot set deadline in past.");

            this.Note = await this._noteService.AddNoteAsync(new Note(this.Name, this.Description, this.Deadline.Value));
            this.Added = true;

            CloseAction?.Invoke();
        }
    }
}
