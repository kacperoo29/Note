using PNote.Core;
using System;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows.Input;

namespace PNote.ViewModels
{
    public class AddUserWindowViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;
        private readonly IUserService _userService;

        private string? _username;
        public string? Username
        {
            get => this._username;
            set
            {
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Username)));
                this._username = value;
            }
        }

        private bool _created = false;
        public bool Created
        {
            get => this._created;
            set
            {
                this._created = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Created)));
            }
        }

        private NoteUser? _user;
        public NoteUser? User
        {
            get => this._user;
            set
            {
                this._user = value;
                this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(User)));
            }
        }

        private ICommand? _createUser;
        public ICommand CreateUser
        {
            get => _createUser ?? (_createUser = new CommandHandler((p) => HandleCreateUser(p).Wait(), () => true));
        }

        public Action? CloseAction { get; set; }

        public AddUserWindowViewModel(IUserService userService)
        {
            this._userService = userService;
        }

        private async Task HandleCreateUser(object? param)
        {
            if (this.Username == null)
                throw new ArgumentException("Username cannot be empty");

            this.User = await this._userService.AddUser(new NoteUser(this._username));
            this.Created = true;
            CloseAction?.Invoke();
        }
    }
}
