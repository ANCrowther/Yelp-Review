namespace Team4_YelpProject.Model
{
    using System;
    using System.ComponentModel;

    public class Business : INotifyPropertyChanged
    {
        private string businessID;
        public string BusinessID
        {
            get { return this.businessID; }
            set { this.businessID = value; RaisePropertyChanged("BusinessID"); }
        }

        private string businessName;
        public string BusinessName
        {
            get { return this.businessName; }
            set { this.businessName = value; RaisePropertyChanged("BusinessName"); }
        }

        private string address;
        public string Address
        {
            get { return this.address; }
            set { this.address = value; RaisePropertyChanged("Address"); }
        }

        private string city;
        public string City
        {
            get { return this.city; }
            set { this.city = value; RaisePropertyChanged("City"); }
        }

        private string state;
        public string State
        {
            get { return this.state; }
            set { this.state = value; RaisePropertyChanged("State"); }
        }

        private int zipcode;
        public int Zipcode
        {
            get { return this.zipcode; }
            set { this.zipcode = value; RaisePropertyChanged("Zipcode"); }
        }

        private double stars;
        public double Stars
        {
            get { return this.stars; }
            set { this.stars = value; RaisePropertyChanged("Stars"); }
        }

        private int numberOfTips;
        public int NumberOfTips
        {
            get { return this.numberOfTips; }
            set { this.numberOfTips = value; RaisePropertyChanged("NumberOfTips"); }
        }

        private int totalCheckins;
        public int TotalCheckins
        {
            get { return this.totalCheckins; }
            set { this.totalCheckins = value; RaisePropertyChanged("TotalCheckins"); }
        }

        private string latitude;
        public string Latitude
        {
            get { return this.latitude; }
            set { this.latitude = value; RaisePropertyChanged("Latitude"); }
        }

        private string longitude;
        public string Longitude
        {
            get { return this.longitude; }
            set { this.longitude = value; RaisePropertyChanged("Longitude"); }
        }

        public string ZipcodeString()
        {
            return Zipcode.ToString();
        }

        public Business() { }

        public event PropertyChangedEventHandler PropertyChanged;

        private void RaisePropertyChanged(string propertyname)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyname));
        }
    }
}
