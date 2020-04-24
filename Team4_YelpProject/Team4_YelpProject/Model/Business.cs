namespace Team4_YelpProject.Model
{
    using System.ComponentModel;

    public class Business : INotifyPropertyChanged
    {
        private string businessID;
        public string BusinessID
        {
            get { return this.businessID; }
            set { this.businessID = value; OnPropertyRaised("BusinessID"); }
        }

        private string businessName;
        public string BusinessName
        {
            get { return this.businessName; }
            set { this.businessName = value; OnPropertyRaised("BusinessName"); }
        }

        private string address;
        public string Address
        {
            get { return this.address; }
            set { this.address = value; OnPropertyRaised("Address"); }
        }

        private string city;
        public string City
        {
            get { return this.city; }
            set { this.city = value; OnPropertyRaised("City"); }
        }

        private string state;
        public string State
        {
            get { return this.state; }
            set { this.state = value; OnPropertyRaised("State"); }
        }

        private double stars;
        public double Stars
        {
            get { return this.stars; }
            set { this.stars = value; OnPropertyRaised("Stars"); }
        }

        private int numberOfTips;
        public int NumberOfTips
        {
            get { return this.numberOfTips; }
            set { this.numberOfTips = value; OnPropertyRaised("NumberOfTips"); }
        }

        private int totalCheckins;
        public int TotalCheckins
        {
            get { return this.totalCheckins; }
            set { this.totalCheckins = value; OnPropertyRaised("TotalCheckins"); }
        }

        private double latitude;
        public double Latitude
        {
            get { return this.latitude; }
            set { this.latitude = value; OnPropertyRaised("Latitude"); }
        }

        private double longitude;
        public double Longitude
        {
            get { return this.longitude; }
            set { this.longitude = value; OnPropertyRaised("Longitude"); }
        }

        public Business() { }

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyRaised(string propertyname)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyname));
        }
    }
}
