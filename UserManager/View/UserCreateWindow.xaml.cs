using System.Windows;
using UserManager.Extensions;
using UserManager.Helpers;
using UserManager.Model;
using UserManager.ViewModel;

namespace UserManager.View
{
    public partial class CreateUserWindow : Window
    {
        private readonly UsersViewModel _usersViewModel;

        public CreateUserWindow(Window owner, UsersViewModel usersViewModel)
        {
            Owner = owner;
            _usersViewModel = usersViewModel;
            InitializeComponent();

            userViewer.SetUser(new User());
        }

        private async void btnCreate_Click(object sender, RoutedEventArgs e)
        {
            this.SetInteractable(false);

            var result = await UserHelper.CreateUser(ApiHelper.Client, userViewer.GetUser());

            if (result.Success && result.Request != null)
            {
                _usersViewModel.CreateUser(result.Request);
            }
            else
            {
                MessageBox.Show($"Cannot create user!\n{result.ResponsePhrase}", "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
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
