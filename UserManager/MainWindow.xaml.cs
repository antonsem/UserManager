using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using UserManager.Extensions;
using UserManager.Helpers;
using UserManager.Model;
using UserManager.View;
using UserManager.ViewModel;

namespace UserManager
{
    public partial class MainWindow : Window
    {

        private UsersViewModel _usersViewModel;

        public MainWindow()
        {
            InitializeComponent();
            ApiHelper.Initialize();

            _usersViewModel = new(this);
            DataContext = _usersViewModel;
            dataGrid.ItemsSource = _usersViewModel.Users;
        }


        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            _usersViewModel.Search("");
        }

        private void btnExport_Click(object sender, RoutedEventArgs e)
        {
            _usersViewModel.Export();
        }

        private void dataGrid_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (sender is not DataGrid grid || grid.SelectedItem is not User user)
            {
                return;
            }

            this.SetInteractable(false);

            new UserEditWindow(this, user, _usersViewModel).ShowDialog();

            this.SetInteractable(true);
        }

        private void btnCreate_Click(object sender, RoutedEventArgs e)
        {
            this.SetInteractable(false);

            new CreateUserWindow(this, _usersViewModel).ShowDialog();

            this.SetInteractable(true);
        }

        private void btnSarch_Click(object sender, RoutedEventArgs e)
        {
            _usersViewModel.Search(tbSearch.Text);
        }

        private void btnPreviousPage_Click(object sender, RoutedEventArgs e)
        {
            _usersViewModel.Page--;
        }

        private void btnNextPage_Click(object sender, RoutedEventArgs e)
        {
            _usersViewModel.Page++;
        }
    }
}
