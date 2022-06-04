using Microsoft.Extensions.DependencyInjection;
using PNote.Core;
using PNote.Views;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;

namespace PNote.ViewModels
{
    public class MainWindowViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        private INoteService _noteService;

        private ObservableCollection<Note> _notes;
        public ObservableCollection<Note> Notes
        {
            get => _notes;
            set
            {
                _notes = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Notes)));
            }
        }

        private ObservableCollection<Note> _stickedNotes;
        public ObservableCollection<Note> StickedNotes
        {
            get => _stickedNotes;
            set
            {
                _stickedNotes = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(StickedNotes)));
            }

        }

        private ObservableCollection<Note> _searchNotes;
        public ObservableCollection<Note> SearchNotes
        {
            get => _searchNotes;
            set
            {
                _searchNotes = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(SearchNotes)));
            }
        }

        private ICommand? _addNote;
        public ICommand AddNote
        {
            get => _addNote ?? (_addNote = new CommandHandler((p) => HandleAddNote(p), () => true));
        }

        private ICommand? _removeNote;
        public ICommand RemoveNote
        {
            get => _removeNote ?? (_removeNote = new CommandHandler((p) => HandleRemoveNote(p), () => Notes.Count > 0));
        }

        public MainWindowViewModel(INoteService noteService)
        {
            this._noteService = noteService;
            this._notes = new(this._noteService.GetNotesAsync().Result);
            this._stickedNotes = new(this._noteService.GetPinnedNotes().Result);
            this._searchNotes = new();
        }

        public void RefreshNotes(Canvas canvas)
        {
            canvas.Children.Clear();
            foreach (var note in StickedNotes)
            {
                this.CreateStickyNoteView(canvas, note);
            }
        }

        public async Task StickNote(Canvas canvas, Note note)
        {
            if (note.PinnedNote != null)
                return;

            note.Pin();
            await this._noteService.EditNoteAsync(note);

            this.CreateStickyNoteView(canvas, note);
            this.StickedNotes.Add(note);
        }

        public async Task Search(string text)
        {
            if (string.IsNullOrEmpty(text))
                return;

            this.SearchNotes = new(await this._noteService.GetNotesByQuery(text));
        }

        private void CreateStickyNoteView(Canvas canvas, Note note)
        {
            var stickyNote = App.ServiceProvider?.GetService<StickyNoteView>();
            if (stickyNote == null)
                return;

            var viewModel = stickyNote.DataContext as StickyNoteViewModel;
            if (viewModel == null)
                return;

            viewModel.Note = note;
            Canvas.SetTop(stickyNote, note.PinnedNote?.X ?? 10);
            Canvas.SetLeft(stickyNote, note.PinnedNote?.Y ?? 10);
            stickyNote.Height = 200;
            stickyNote.Width = 200;

            canvas.Children.Add(stickyNote);
        }

        private async void HandleAddNote(object? parameter)
        {
            Note note = await this._noteService.AddNoteAsync(new Note("Test note", "Test contents", DateTime.Now.AddDays(1)));
            this.Notes.Add(note);
        }

        private async void HandleRemoveNote(object? parameter)
        {
            var note = parameter as Note;
            if (note == null)
                return;

            this.Notes.Remove(note);
            this.StickedNotes.Remove(note);
            await this._noteService.RemoveNoteAsync(note);
        }
    }
}
