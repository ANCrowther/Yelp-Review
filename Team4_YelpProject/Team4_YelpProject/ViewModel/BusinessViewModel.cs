namespace Team4_YelpProject.ViewModel
{
    using System.Collections.ObjectModel;
    using Team4_YelpProject.Model;

    public class BusinessViewModel
    {
        public ObservableCollection<Business> ObjBusinesses
        {
            get;
            set;
        }

        public void LoadStates()
        {
            ObservableCollection<Business> objBusinesses = new ObservableCollection<Business>();

            ObjBusinesses = objBusinesses;
        }

        public Business ObjBusiness
        {
            get;
            set;
        }


    }
}
