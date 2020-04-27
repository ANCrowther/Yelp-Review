namespace Team4_YelpProject.ViewModel
{
    using Team4_YelpProject.Commands;
    using Team4_YelpProject.Model;
    using System.Collections.ObjectModel;
    using System.ComponentModel;
    using System;

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
            updateUserLocationCommand = new RelayCommand(UpdateUserLocation);
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
            set 
            { 
                currentBusiness = value; OnPropertyChanged("CurrentBusiness");
            }
        }

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

        #region Update User Location
        private RelayCommand updateUserLocationCommand;
        public RelayCommand UpdateUserLocationCommand { get { return updateUserLocationCommand; } }

        public void UpdateUserLocation()
        {
            try
            {
                var IsUpdate = ObjYelpService.UpdateLocation(SelectedUser);
            }
            catch(Exception ex)
            {
                Message = ex.Message;
            }
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
        private Business selectedState;
        public Business SelectedState
        {
            get { return selectedState; }
            set
            {
                selectedState = value;
                OnPropertyChanged("SelectedState");
                CurrentBusiness.State = SelectedState.State;
                CityList = new ObservableCollection<Business>(ObjYelpService.SearchCities(SelectedState.State));
            }
        }

        private ObservableCollection<Business> cityList;
        public ObservableCollection<Business> CityList
        {
            get { return cityList; }
            set { cityList = value; OnPropertyChanged("CityList"); }
        }
        #endregion

        #region Load ZipcodeList
        private Business selectedCity;
        public Business SelectedCity
        {
            get { return selectedCity; }
            set
            {
                selectedCity = value;
                OnPropertyChanged("SelectedCity");
                ZipcodeList = new ObservableCollection<Business>(ObjYelpService.SearchZipcodes(SelectedState.State, SelectedCity.City));
            }
        }

        private ObservableCollection<Business> zipcodeList;
        public ObservableCollection<Business> ZipcodeList
        {
            get { return zipcodeList; }
            set
            { 
                zipcodeList = value; 
                OnPropertyChanged("ZipcodeList");
            }
        }
        #endregion

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string v)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(v));
        }
    }
}
