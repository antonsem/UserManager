using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using UserManager.Extensions;
using UserManager.Helpers;
using UserManager.Model;

namespace UserManager.ViewModel
{
    public class UsersViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;
        public ObservableCollection<User> Users { get; set; } = new ObservableCollection<User>();


        private readonly Window _owner;

        private int _page = 1;
        public int Page
        {
            get => _page;
            set
            {
                if (value < 1)
                {
                    return;
                }

                _page = value;
                OnPageChange();
                OnPropertyChanged();
            }
        }

        private string _searchPhrase = "";

        public string SearchPhrase
        {
            get => _searchPhrase;
            set
            {
                _searchPhrase = value;
                OnPropertyChanged();
            }
        }



        public UsersViewModel(Window owner)
        {
            _owner = owner;
        }


        public void Export()
        {
            var csv = UserHelper.GetCSV(Users);
            FileHelper.SaveTextFile(csv, _owner, "CSV Filter | *.csv");
        }

        public async void Search(string search)
        {
            _owner.SetInteractable(false);

            GetRequestResult<IList<User>> result;

            if (string.IsNullOrWhiteSpace(search))
            {
                result = await UserHelper.GetUsers(ApiHelper.Client, Page);
            }
            else
            {
                var arguments = search.Split(':');
                var filter = arguments.Length > 1 ? arguments[0] : "name";
                var keyword = arguments[^1];

                result = await UserHelper.GetUsers(ApiHelper.Client, Page, 10, filter, keyword);
            }

            _owner.SetInteractable(true);


            if (result.TotalPageCount < Page)
            {
                Page = result.TotalPageCount;
            }

            Users.Clear();

            if(result.Request == null)
            {
                return;
            }

            foreach (var user in result.Request)
            {
                Users.Add(user);
            }
        }

        public void EditUser(User user)
        {
            var index = Users.ToList().FindIndex(u => u.id == user.id);
            Users[index] = user;
        }

        public void DeleteUser(User user)
        {
            var index = Users.ToList().FindIndex(u => u.id == user.id);
            Users.RemoveAt(index);
        }

        public void CreateUser(User user)
        {
            Users.Add(user);
        }

        private void OnPageChange()
        {
            Search(SearchPhrase);
        }

        private void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

    }
}
