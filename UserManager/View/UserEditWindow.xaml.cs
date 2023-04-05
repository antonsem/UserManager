using System;
using System.Windows;

namespace UserManager.View
{
    public partial class UserEditWindow : Window
    {
        private Action<User> OnEdit;
        private Action<User> OnDelete;

        public UserEditWindow(Window parentWindow, User user, Action<User> onEdit, Action<User> onDelete)
        {
            Owner = parentWindow;
            OnEdit = onEdit;
            OnDelete = onDelete;
            InitializeComponent();

            tbId.Text = user.Id.ToString();
            tbName.Text = user.Name;
            tbEmail.Text = user.Email;
            cbGender.SelectedIndex = (int)user.UserGender;
            cbStatus.IsChecked = user.IsActive;
        }

        private async void btnSave_Click(object sender, RoutedEventArgs e)
        {
            Opacity = 0.4;
            IsEnabled = false;

            var newUser = new User
            {
                Id = int.Parse(tbId.Text),
                Name = tbName.Text,
                Email = tbEmail.Text,
                UserGender = (Gender)cbGender.SelectedIndex,
                IsActive = cbStatus.IsChecked ?? false
            };

            var result = await UserHelper.UpdateUser(ApiHelper.Client, newUser);

            if (result != null)
            {
                OnEdit?.Invoke(result);
            }

            Opacity = 1;
            IsEnabled = true;
            Close();
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
