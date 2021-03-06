using Microsoft.Extensions.DependencyInjection;

namespace PNote.ViewModels
{
    public class ViewModelLocator
    {
        public MainWindowViewModel? MainWindowViewModel => App.ServiceProvider?.GetService<MainWindowViewModel>();
        public StickyNoteViewModel? StickyNoteViewModel => App.ServiceProvider?.GetService<StickyNoteViewModel>();
        public UserSelectWindowViewModel? UserSelectViewModel => App.ServiceProvider?.GetService<UserSelectWindowViewModel>();
        public AddUserWindowViewModel? AddUserWindowViewModel => App.ServiceProvider?.GetService<AddUserWindowViewModel>();
        public AddNoteViewModel? AddNoteViewModel => App.ServiceProvider?.GetService<AddNoteViewModel>();
        public EditNoteViewModel? EditNoteViewModel => App.ServiceProvider?.GetService<EditNoteViewModel>();
    }
}
