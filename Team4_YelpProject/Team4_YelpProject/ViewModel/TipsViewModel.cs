namespace Team4_YelpProject.ViewModel
{
    using System;
    using System.Collections.ObjectModel;
    using System.ComponentModel;
    using Team4_YelpProject.Model;

    class TipsViewModel : INotifyPropertyChanged
    {
        YelpServices ObjYelpService;
        private Business ObjBusiness;
        private YelpUser ObjUser;

        public TipsViewModel(Business B, YelpUser U)
        {
            ObjYelpService = new YelpServices();
            ObjBusiness = B;
            ObjUser = U;
            Console.WriteLine(B.BusinessName);
            Console.WriteLine(U.Name);
            LoadBusinessTips();
            LoadFriendsList();
        }

        #region Load Business Tips Grid

        private ObservableCollection<Tips> tipsList;
        public ObservableCollection<Tips> TipsList
        {
            get { return tipsList; }
            set { tipsList = value; OnPropertyChanged("TipsList"); }
        }

        private void LoadBusinessTips()
        {
            TipsList = new ObservableCollection<Tips>(ObjYelpService.GetTips(ObjBusiness.BusinessID));
        }

        #endregion

        #region Load Friends Grid
        private void LoadFriendsList()
        {

        }
        #endregion

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string v)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(v));
        }
    }
}
