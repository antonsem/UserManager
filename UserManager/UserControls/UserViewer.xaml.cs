using System.Windows.Controls;
using UserManager.Model;

namespace UserManager.UserControls
{
    public partial class UserViewer : UserControl
    {

        private int _id;

        public UserViewer()
        {
            InitializeComponent();
        }

        public User GetUser()
        {
            var newUser = new User
            {
                id = _id,
                name = tbName.Text,
                email = tbEmail.Text,
                UserGender = (Gender)cbGender.SelectedIndex,
                IsActive = cbStatus.IsChecked ?? false
            };

            return newUser;
        }

        public void SetUser(User user)
        {
            _id = user.id;
            tbName.Text = user.name;
            tbEmail.Text = user.email;
            cbGender.SelectedIndex = (int)user.UserGender;
            cbStatus.IsChecked = user.IsActive;
        }
    }
}
