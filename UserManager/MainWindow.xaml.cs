using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using UserManager.View;

namespace UserManager
{
    public partial class MainWindow : Window
    {
        public ObservableCollection<User> Users { get; set; } = new ObservableCollection<User>();

        public MainWindow()
        {
            InitializeComponent();
            ApiHelper.Initialize();

            dataGrid.ItemsSource = Users;
            DataContext = this;

        }


        private void btnExport_Click(object sender, RoutedEventArgs e)
        {
            var csv = UserHelper.GetCSV(Users);
            FileHelper.SaveTextFile(csv, this, "CSV Filter | *.csv");
        }

        private void btnRefresh_Click(object sender, RoutedEventArgs e)
        {
            RefreshUsers();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            RefreshUsers();
        }

        private async void RefreshUsers()
        {
            var result = await UserHelper.GetUsers(ApiHelper.Client);

            if (result != null)
            {
                Users = new ObservableCollection<User>(result);
                dataGrid.ItemsSource = Users;
            }
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {

        }

        private void dataGrid_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            var grid = (DataGrid)sender;
            var user = grid.SelectedItem as User;

            if (user == null)
            {
                return;
            }

            Opacity = 0.4f;

            var userEditWindow = new UserEditWindow(this, user, OnEdit, OnDelete);
            var result = userEditWindow.ShowDialog();

            Opacity = 1f;
        }

        private void OnEdit(User user)
        {
            var index = Users.ToList().FindIndex(u => u.Id == user.Id);
            Users[index] = user;
        }

        private void OnDelete(User user)
        {
            var index = Users.ToList().FindIndex(u => u.Id == user.Id);
            Users.RemoveAt(index);
        }
    }
}
