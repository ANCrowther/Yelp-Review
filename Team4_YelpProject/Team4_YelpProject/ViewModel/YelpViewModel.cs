namespace Team4_YelpProject.ViewModel
{
    using Team4_YelpProject.Commands;
    using Team4_YelpProject.Model;
    using System.Collections.ObjectModel;
    using System.ComponentModel;
    using System;
    using Team4_YelpProject.View;

    public class YelpViewModel : INotifyPropertyChanged
    {
        YelpServices ObjYelpService;

        public YelpViewModel()
        {
            ObjYelpService = new YelpServices();
            LoadStates();
            CurrentUser = new YelpUser();
            CurrentBusiness = new Business();
            Hours = new BusinessHours();
            searchUserCommand = new RelayCommand(SearchUser);
            updateUserLocationCommand = new RelayCommand(UpdateUserLocation);
            addCommand = new RelayCommand(AddSelectedCategories);
            removeCommand = new RelayCommand(RemoveSelectedCategories);
            searchBusinessesCommand = new RelayCommand(SearchBusinesses);
            searchTipsCommand = new RelayCommand(SearchTips);
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string v)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(v));
        }

        /*    GENERAL USE    */

        #region General Use Objects
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
                currentBusiness = value; 
                OnPropertyChanged("CurrentBusiness");
                if(currentBusiness.BusinessID != null)
                {
                    Hours = ObjYelpService.SearchForHours(currentBusiness);
                }
            }
        }

        private BusinessHours hours;
        public BusinessHours Hours
        {
            get { return hours; }
            set
            {
                hours = value; OnPropertyChanged("Hours");
            }
        }
        #endregion

        /*    USER VIEW    */

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

        /*    BUSINESS VIEW    */

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
                CurrentBusiness.City = SelectedCity.City;
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

        #region Load Categories
        private Business selectedZipcode;
        public Business SelectedZipcode
        {
            get { return selectedZipcode; }
            set
            {
                selectedZipcode = value;
                CurrentBusiness.Zipcode = SelectedZipcode.Zipcode;
                OnPropertyChanged("SelectedZipcode");
                CategoryList = new ObservableCollection<Business>(ObjYelpService.SearchCategoryList(CurrentBusiness));
            }
        }

        private ObservableCollection<Business> categoryList;
        public ObservableCollection<Business> CategoryList
        {
            get { return categoryList; }
            set
            {
                categoryList = value;
                OnPropertyChanged("CategoryList");
            }
        }
        #endregion

        #region Choosing Categories
        private RelayCommand addCommand;
        public RelayCommand AddCommand { get { return addCommand; } }

        private void AddSelectedCategories()
        {
            if(SelectionList == null)
            {
                SelectionList = new ObservableCollection<Business>();
            }

            SelectionList.Add(SelectedItem);
            CategoryList.Remove(SelectedItem);
        }

        private RelayCommand removeCommand;
        public RelayCommand RemoveCommand { get { return removeCommand; } }

        private void RemoveSelectedCategories()
        {
            if (CategoryList == null)
            {
                CategoryList = new ObservableCollection<Business>();
            }

            CategoryList.Add(SelectedItem);
            SelectionList.Remove(SelectedItem);
        }

        private Business selectedItem;
        public Business SelectedItem
        {
            get { return selectedItem; }
            set
            {
                selectedItem = value;
            }
        }

        private ObservableCollection<Business> selectionList;
        public ObservableCollection<Business> SelectionList
        {
            get { return selectionList; }
            set
            {
                selectionList = value;
                OnPropertyChanged("SelectionList");
            }
        }
        #endregion

        #region Search for Businesses
        private RelayCommand searchBusinessesCommand;
        public RelayCommand SearchBusinessesCommand { get { return searchBusinessesCommand; } }

        private ObservableCollection<Business> businessList;
        public ObservableCollection<Business> BusinessList
        {
            get { return businessList; }
            set
            {
                businessList = value;
                OnPropertyChanged("BusinessList");
            }
        }

        public void SearchBusinesses()
        {
            BusinessList = new ObservableCollection<Business>(ObjYelpService.SearchBusinesses(SelectionList, CurrentBusiness));
        }
        #endregion

        /*    LOAD BUSINESS TIPS WINDOW    */

        #region Load the Business Tips window
        private RelayCommand searchTipsCommand;
        public RelayCommand SearchTipsCommand { get { return searchTipsCommand; } }

        public void SearchTips()
        {
            BusinessTipsView tipWindow = new BusinessTipsView(CurrentBusiness, CurrentUser);
            tipWindow.DataContext = this;
            tipWindow.Show();
        }
        #endregion
    }
}
