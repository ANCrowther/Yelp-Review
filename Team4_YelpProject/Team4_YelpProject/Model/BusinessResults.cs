namespace Team4_YelpProject.Model
{
    using System;
    using System.ComponentModel;

    public class BusinessResults : INotifyPropertyChanged
    {
        private string businessID;
        private string businessName;
        private string address;
        private string city;
        private string state;
        private double distance;
        private double stars;
        private int numberOfTips;
        private int totalCheckins;
        private double bLatitude;
        private double bLongitude;

        public string BusinessID
        {
            get { return this.businessID; }
            set
            {
                this.businessID = value;
                this.OnPropertyChanged("BusinessID");
            }
        }

        public string BusinessName
        {
            get { return this.businessName; }
            set
            {
                this.businessName = value;
                this.OnPropertyChanged("BusinessName");
            }
        }

        public string Address
        {
            get { return this.address; }
            set
            {
                this.address = value;
                this.OnPropertyChanged("Address");
            }
        }

        public string City
        {
            get { return this.city; }
            set
            {
                this.city = value;
                this.OnPropertyChanged("City");
            }

        }

        public string State
        {
            get { return this.state; }
            set
            {
                this.state = value;
                this.OnPropertyChanged("State");
            }
        }

        public double Distance
        {
            get { return this.distance; }
            set
            {
                this.distance = value;
                this.OnPropertyChanged("Distance");
            }
        }

        public double Stars
        {
            get { return this.stars; }
            set
            {
                this.stars = value;
                this.OnPropertyChanged("Stars");
            }
        }

        public int NumberOfTips
        {
            get { return this.numberOfTips; }
            set
            {
                this.numberOfTips = value;
                this.OnPropertyChanged("NumberOfTips");
            }
        }

        public int TotalCheckins
        {
            get { return this.totalCheckins; }
            set
            {
                this.totalCheckins = value;
                this.OnPropertyChanged("TotalCheckins");
            }
        }

        public double BLatitude
        {
            get { return this.bLatitude; }
            set
            {
                this.bLatitude = value;
                this.OnPropertyChanged("BLatitude");
            }
        }

        public double BLongitude
        {
            get { return this.bLongitude; }
            set
            {
                this.bLongitude = value;
                this.OnPropertyChanged("BLongitude");
            }
        }

        public double calculateDistance(double latitude, double longitude)
        {
            return Math.Sqrt(Math.Pow(bLatitude - latitude, 2) + Math.Pow(bLongitude - longitude, 2));
        }

        public BusinessResults() { }

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string v)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(v));
        }
    }
}
