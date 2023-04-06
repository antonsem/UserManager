using System.Windows;
using UserManager.Extensions;
using UserManager.Helpers;
using UserManager.Model;
using UserManager.ViewModel;

namespace UserManager.View
{
    public partial class UserEditWindow : Window
    {
        private readonly UsersViewModel _usersViewModel;
        private User User { get; set; }

        public UserEditWindow(Window parentWindow, User user, UsersViewModel userViewModel)
        {
            Owner = parentWindow;
            _usersViewModel = userViewModel;
            InitializeComponent();

            userViewer.SetUser(user);
        }

        private async void btnSave_Click(object sender, RoutedEventArgs e)
        {
            this.SetInteractable(false);

            var result = await UserHelper.UpdateUser(ApiHelper.Client, userViewer.GetUser());

            if (result.Success && result.Request != null)
            {
                _usersViewModel.EditUser(result.Request);
            }
            else
            {
                MessageBox.Show($"Cannot update user!\n{result.ResponsePhrase}", "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            this.SetInteractable(true);
            Close();
        }

        private async void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            this.SetInteractable(false);

            var result = await UserHelper.DeleteUser(ApiHelper.Client, userViewer.GetUser());

            if (result.Success && result.Request != null)
            {
                _usersViewModel.DeleteUser(result.Request);
            }
            else
            {
                MessageBox.Show($"Cannot delete user!\n{result.ResponsePhrase}", "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            this.SetInteractable(true);
            Close();
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
