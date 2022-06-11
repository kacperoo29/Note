using Microsoft.Extensions.DependencyInjection;
using PNote.Core;
using PNote.ViewModels;
using System;
using System.Windows;
using System.Windows.Input;

namespace PNote.Views
{
    public partial class UserSelectWindow : Window
    {
        public UserSelectWindow()
        {
            InitializeComponent();
        }

        private void UserBorder_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            var dataContext = this.DataContext as UserSelectWindowViewModel;
            if (dataContext == null)
                throw new Exception($"Invalid data context in {nameof(UserSelectWindow)} of type {this.DataContext.GetType()}.");


            dataContext.Login((sender as FrameworkElement)?.DataContext as NoteUser ??
                throw new Exception($"Invalid user."));

            App.ServiceProvider?.GetService<MainWindow>()?.Show();

            this.Close();
        }
    }
}
