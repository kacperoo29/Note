using Microsoft.Extensions.DependencyInjection;

namespace PNote.ViewModels
{
    public class ViewModelLocator
    {
        public MainWindowViewModel? MainWindowViewModel => App.ServiceProvider?.GetService<MainWindowViewModel>();
        public StickyNoteViewModel? StickyNoteViewModel => App.ServiceProvider?.GetService<StickyNoteViewModel>();
    }
}
