using PNote.Core;
using System.ComponentModel;
using System.Threading.Tasks;

namespace PNote.ViewModels
{
    public class StickyNoteViewModel : INotifyPropertyChanged
    {
        private readonly INoteService _noteService;

        public event PropertyChangedEventHandler? PropertyChanged;

        private Note? _note;
        public Note? Note
        {
            get => _note;
            set
            {
                _note = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Note)));
            }
        }

        public StickyNoteViewModel(INoteService noteService)
        {
            this._noteService = noteService;
        }

        public async Task UnstickNote()
        {
            if (this.Note == null)
                return;

            this.Note.IsPinned = false;
            await this._noteService.EditNoteAsync(this.Note);
        }
    }
}
