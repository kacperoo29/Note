using PNote.Core;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;

namespace PNote.ViewModels
{
    public class UserSelectWindowViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        private readonly IUserService _userService;

        private ObservableCollection<NoteUser> _users;
        public ObservableCollection<NoteUser> Users
        {
            get => this._users;
            set
            {
                this._users = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Users)));
            }
        }

        public UserSelectWindowViewModel(IUserService userService)
        {
            this._userService = userService;
            this._users = new(this._userService.GetUsers().Result);
        }

        public void Login(NoteUser user)
        {
            this._userService.SetCurrentUser(user);
        }
    }
}
