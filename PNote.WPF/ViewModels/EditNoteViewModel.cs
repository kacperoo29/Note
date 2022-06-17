using PNote.Core;
using System;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows.Input;

namespace PNote.ViewModels
{
    public class EditNoteViewModel : INotifyPropertyChanged
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

        private Note? _note;
        public Note? Note
        {
            get => this._note;
            set
            {
                this._note = value;
                this.Deadline = value?.Deadline;
                this.Name = value?.Name;
                this.Description = value?.Content;

                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Note)));
            }
        }

        private bool _edited = false;
        public bool Edited
        {
            get => _edited;
            set
            {
                this._edited = value;
                this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Edited)));
            }
        }

        private ICommand? _saveNote;
        public ICommand SaveNote
        {
            get => _saveNote ?? (_saveNote = new CommandHandler((p) => HandleAddNote(p).Wait(), () => true));
        }

        public Action? CloseAction { get; set; }

        public EditNoteViewModel(INoteService noteService)
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

            this.Note!.Deadline = this.Deadline.Value;
            this.Note.Name = this.Name;
            this.Note.Content = this.Description;

            this.Note = await this._noteService.EditNoteAsync(Note);
            this.Edited = true;

            CloseAction?.Invoke();
        }
    }
}
