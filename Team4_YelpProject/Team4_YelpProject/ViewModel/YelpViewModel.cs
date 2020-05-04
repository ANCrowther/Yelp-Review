namespace Team4_YelpProject.ViewModel
{
    using Microsoft.Maps.MapControl.WPF;
    using System.Collections.ObjectModel;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using Team4_YelpProject.Commands;
    using Team4_YelpProject.Model;
    using Team4_YelpProject.View;
    using System.Windows;

    public class YelpViewModel : INotifyPropertyChanged
    {
        YelpServices ObjYelpService;
        BusinessTipsView tipWindow;
        CheckinView checkinWindow;

        public YelpViewModel()
        {
            ObjYelpService = new YelpServices();
            CurrentUser = new YelpUser();
            CurrentBusiness = new Business();
            Hours = new BusinessHours();
            NewTip = new Tips();

            LoadStates();

            searchUserCommand = new RelayCommand(SearchUser);
            updateUserLocationCommand = new RelayCommand(UpdateUserLocation);
            addCommand = new RelayCommand(AddSelectedCategories);
            removeCommand = new RelayCommand(RemoveSelectedCategories);
            searchBusinessesCommand = new RelayCommand(SearchBusinesses);
            searchTipsCommand = new RelayCommand(SearchTips);
            likeCommand = new RelayCommand(UpdateLikeTips);
            addTipCommand = new RelayCommand(AddToTips);
            checkinCommand = new RelayCommand(checkinSearch);
            checkinWindowCommand = new RelayCommand(addCheckin);
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
                    BusinessCategories = new ObservableCollection<Business>(ObjYelpService.GetCategories(CurrentBusiness.BusinessID));
                    BusinessAttributes = new ObservableCollection<Business>(ObjYelpService.GetAttributes(CurrentBusiness.BusinessID));
                }
            }
        }

        private BusinessHours hours;
        public BusinessHours Hours
        {
            get { return hours; }
            set { hours = value; OnPropertyChanged("Hours"); }
        }

        private Tips selectedTip;
        public Tips SelectedTip
        {
            get { return selectedTip; }
            set { selectedTip = value; OnPropertyChanged("SelectedTip"); }
        }

        private Tips newTip;
        public Tips NewTip
        {
            get { return newTip; }
            set { newTip = value; OnPropertyChanged("NewTip"); }
        }

        private string itemCount;
        public string ItemCount
        {
            get { return itemCount; }
            set { itemCount = value;OnPropertyChanged("ItemCount"); }
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

            SelectionList.Remove(SelectedItem);
            CategoryList.Add(SelectedItem);
        }

        private Business selectedItem;
        public Business SelectedItem
        {
            get { return selectedItem; }
            set
            {
                selectedItem = value;
                OnPropertyChanged("SelectedItem");
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
                OnPropertyChanged("ItemCount");
            }
        }

        public void SearchBusinesses()
        {
            BusinessList = new ObservableCollection<Business>(ObjYelpService.SearchBusinesses(SelectionList, CurrentBusiness));

            if (SelectedUser != null)
            {
                foreach (Business bus in BusinessList)
                {
                    bus.Distance = ObjYelpService.DetermineDistance(bus, SelectedUser);
                }
            }

            ItemCounter();
            LoadBusinessLocations();
        }

        public void ItemCounter()
        {
            ItemCount = BusinessList.Count.ToString();
        }
        #endregion

        #region Load Business Attributes and Categories
        private ObservableCollection<Business> businessAttributes;
        public ObservableCollection<Business> BusinessAttributes
        {
            get { return businessAttributes; }
            set
            {
                businessAttributes = value;
                OnPropertyChanged("BusinessAttributes");
            }
        }

        private ObservableCollection<Business> businessCategories;
        public ObservableCollection<Business> BusinessCategories
        {
            get { return businessCategories; }
            set
            {
                businessCategories = value;
                OnPropertyChanged("BusinessCategories");
            }
        }
        #endregion

        /*    BUSINESS VIEW MAP    */

        #region Populate Bing Map
        private ObservableCollection<Pushpin> businessLocations;
        public ObservableCollection<Pushpin> BusinessLocations
        {
            get { return businessLocations; }
            set
            {
                businessLocations = value;
                OnPropertyChanged("BusinessList");
                OnPropertyChanged("BusinessLocations");
            }
        }

        private void LoadBusinessLocations()
        {
            List<Pushpin> pins = new List<Pushpin>();
            foreach(Business B in BusinessList)
            {
                Location loc = new Location(B.Latitude, B.Longitude);
                Pushpin pin = new Pushpin();
                pin.Location = loc;
                pin.Content = B.BusinessName;
                pins.Add(pin);
            }
            BusinessLocations = new ObservableCollection<Pushpin>(pins);
        }
        #endregion

        /*    LOAD BUSINESS TIPS WINDOW    */

        #region Load the Business Tips window
        private RelayCommand searchTipsCommand;
        public RelayCommand SearchTipsCommand { get { return searchTipsCommand; } }

        public void SearchTips()
        {
            tipWindow = new BusinessTipsView();
            tipWindow.DataContext = this;
            LoadBusinessTips();
            LoadFriendsList();
            tipWindow.Show();
        }
        #endregion

        #region Load Business Tips Grid
        private ObservableCollection<Tips> tipsList;
        public ObservableCollection<Tips> TipsList
        {
            get { return tipsList; }
            set { tipsList = value; OnPropertyChanged("TipsList"); }
        }

        private void LoadBusinessTips()
        {
            TipsList = new ObservableCollection<Tips>(ObjYelpService.GetTips(CurrentBusiness.BusinessID));
        }
        #endregion

        #region Load Friends Grid
        private ObservableCollection<Tips> friendTipsList;
        public ObservableCollection<Tips> FriendTipsList
        {
            get { return friendTipsList; }
            set { friendTipsList = value; OnPropertyChanged("FriendTipsList"); }
        }

        private void LoadFriendsList()
        {
            if (SelectedUser != null)
            {
                FriendTipsList = new ObservableCollection<Tips>(ObjYelpService.GetFriendTips(CurrentBusiness.BusinessID, SelectedUser.User_id));
            }
            else
            {
                FriendTipsList = new ObservableCollection<Tips>(ObjYelpService.GetFriendTips(CurrentBusiness.BusinessID, ""));
            }
        }
        #endregion

        #region Like tips Command
        private RelayCommand likeCommand;
        public RelayCommand LikeCommand { get { return likeCommand; } }

        public void UpdateLikeTips()
        {
            try
            {
                var IsUpdate = ObjYelpService.UpdateLikeTips(SelectedTip);
                if (IsUpdate)
                {
                    TipsList = new ObservableCollection<Tips>(ObjYelpService.GetTips(CurrentBusiness.BusinessID));
                }
            }
            catch (Exception ex)
            {
                Message = ex.Message;
            }
        }
        #endregion

        #region Add a tip Command
        private RelayCommand addTipCommand;
        public RelayCommand AddTipCommand { get { return addTipCommand; } }

        public void AddToTips()
        {
            try
            {
                NewTip.BusinessID = currentBusiness.BusinessID;
                NewTip.UserID = SelectedUser.User_id;
                var IsUpdate = ObjYelpService.AddToTips(NewTip);
                if (IsUpdate)
                {
                    TipsList = new ObservableCollection<Tips>(ObjYelpService.GetTips(CurrentBusiness.BusinessID));
                }

            }
            catch (Exception ex)
            {
                Message = ex.Message;
            }
        }
        #endregion

        /*    LOAD CHECKINS WINDOW    */

        #region Load the Checkins Window
        private RelayCommand checkinCommand;
        public RelayCommand CheckinCommand { get { return checkinCommand; } }

        public void checkinSearch()
        {
            checkinWindow = new CheckinView();
            checkinWindow.DataContext = this;
            LoadCheckinsAtBusiness();
            checkinWindow.Show();
        }

        private ObservableCollection<KeyValuePair<string, int>> checkins;
        public ObservableCollection<KeyValuePair<string, int>> Checkins
        {
            get { return checkins; }
            set { checkins = value; OnPropertyChanged("Checkins"); }
        }

        private void LoadCheckinsAtBusiness()
        {
            Checkins = new ObservableCollection<KeyValuePair<string, int>>(ObjYelpService.GetCheckins(CurrentBusiness.BusinessID));

            Console.WriteLine("LoadCheckinsAtBusiness: " + Checkins.Count);
            foreach(KeyValuePair<string, int> key in Checkins)
            {
                Console.WriteLine($"Pair here: {key.Key}, {key.Value}");
            }
        }
        #endregion

        #region Check-in
        private RelayCommand checkinWindowCommand;
        public RelayCommand CheckinWindowCommand { get { return checkinWindowCommand; } }

        private void addCheckin()
        {
            var IsUpdate = ObjYelpService.AddCheckin(CurrentBusiness.BusinessID);
            if (IsUpdate)
            {
                LoadCheckinsAtBusiness();
            }
        }
        #endregion
    }
}
