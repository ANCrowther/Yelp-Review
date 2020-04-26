namespace Team4_YelpProject.ViewModel
{
    using Team4_YelpProject.Commands;
    using Team4_YelpProject.Model;
    using System.Collections.ObjectModel;
    using System.ComponentModel;

    public class YelpViewModel : INotifyPropertyChanged
    {
        YelpServices ObjYelpService;

        public YelpViewModel()
        {
            ObjYelpService = new YelpServices();
            LoadStates();
            CurrentUser = new YelpUser();
            CurrentBusiness = new Business();
            searchUserCommand = new RelayCommand(SearchUser);
        }

        private string message;
        public string Message
        {
            get { return message; }
            set { message = value; OnPropertyChanged("Message"); }
        }

        private YelpUser currentUser;
        public YelpUser CurrentUser
        {
            get { return currentUser; }
            set { currentUser = value; OnPropertyChanged("CurrentUser"); }
        }

        private Business currentBusiness;
        public Business CurrentBusiness
        {
            get { return currentBusiness; }
            set { currentBusiness = value; OnPropertyChanged("CurrentBusiness"); }
        }

        #region Load StateList
        private ObservableCollection<Business> statesList;
        public ObservableCollection<Business> StatesList
        {
            get { return statesList; }
            set { statesList = value; OnPropertyChanged("StatesList"); }
        }

        private void LoadStates()
        {
            StatesList = new ObservableCollection<Business>(ObjYelpService.GetStates());
        }
        #endregion

        #region Load CityList
        /*    UNDER CONSTRUCTION    */

        private ObservableCollection<Business> cityList;
        public ObservableCollection<Business> CityList
        {
            get { return cityList; }
            set { cityList = value; OnPropertyChanged("CityList"); }
        }
        #endregion

        #region Search User by Name
        private RelayCommand searchUserCommand;
        public RelayCommand SearchUserCommand { get { return searchUserCommand; } }

        private ObservableCollection<YelpUser> userList;
        public ObservableCollection<YelpUser> UserList
        {
            get { return userList; }
            set { userList = value; OnPropertyChanged("UserList"); }
        }

        public void SearchUser()
        {
            UserList = new ObservableCollection<YelpUser>(ObjYelpService.SearchUser(currentUser.Name));
        }
        #endregion

        #region Search User by ID
        private YelpUser selectedUser;
        public YelpUser SelectedUser
        {
            get { return selectedUser; }
            set
            {
                selectedUser = value;
                OnPropertyChanged("SelectedUser");

                FriendsList = new ObservableCollection<YelpUser>(ObjYelpService.SearchUserFriends(SelectedUser.User_id));
                TipList = new ObservableCollection<Tips>(ObjYelpService.SearchFriendTips(SelectedUser.User_id));

            }
        }
        #endregion

        #region Search for User's Friend's
        // SelectUser under Search User by ID region calls the SearchUserFriends()
        private ObservableCollection<YelpUser> friendsList;
        public ObservableCollection<YelpUser> FriendsList
        {
            get { return friendsList; }
            set { friendsList = value; OnPropertyChanged("FriendsList"); }
        }
        #endregion

        #region Search for Friends' Recent Tips
        // SelectUser under Search User by ID region calls the SearchFriendTips()
        private ObservableCollection<Tips> tipList;
        public ObservableCollection<Tips> TipList
        {
            get { return tipList; }
            set { tipList = value; OnPropertyChanged("TipList"); }
        }
        #endregion

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string v)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(v));
        }
    }
}
