namespace Team4_YelpProject.Model
{
    using System;
    using System.Collections.Generic;
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

        private string state;
        public string State
        {
            get { return this.state; }
            set 
            { 
                this.state = value; 
                RaisePropertyChanged("State");
                RaisePropertyChanged("FullAddress");
            }
        }

        private string city;
        public string City
        {
            get { return this.city; }
            set 
            { 
                this.city = value; 
                RaisePropertyChanged("City");
                RaisePropertyChanged("FullAddress");
            }
        }

        private string address;
        public string Address
        {
            get { return this.address; }
            set
            {
                this.address = value;
                RaisePropertyChanged("Address");
                RaisePropertyChanged("FullAddress");
            }
        }

        private int zipcode;
        public int Zipcode
        {
            get { return this.zipcode; }
            set { this.zipcode = value; RaisePropertyChanged("Zipcode"); }
        }

        private double latitude;
        public double Latitude
        {
            get { return this.latitude; }
            set { this.latitude = value; RaisePropertyChanged("Latitude"); }
        }

        private double longitude;
        public double Longitude
        {
            get { return this.longitude; }
            set { this.longitude = value; RaisePropertyChanged("Longitude"); }
        }

        private double stars;
        public double Stars
        {
            get { return this.stars; }
            set { this.stars = value; RaisePropertyChanged("Stars"); }
        }

        private bool isOpen;
        public bool IsOpen
        {
            get { return this.isOpen; }
            set { this.isOpen = value; RaisePropertyChanged("IsOpen"); }
        }

        private int reviewCount;
        public int ReviewCount
        {
            get { return this.reviewCount; }
            set { this.reviewCount = value; RaisePropertyChanged("ReviewCount"); }
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

        private string category;
        public string Category
        {
            get { return category; }
            set { category = value; RaisePropertyChanged("Category"); }
        }

        private string attribute;
        public string Attribute
        {
            get { return attribute; }
            set { attribute = value; RaisePropertyChanged("Attribute"); }
        }

        private string fullAddress;
        public string FullAddress
        {
            get { return address + ", " + city + ", " + state + ", " + zipcode ; }
        }

        /*    UNDER CONSTRUCTION    */
        private double distance;
        public double Distance
        {
            get { return distance; }
        }

        public Business() { }

        public event PropertyChangedEventHandler PropertyChanged;

        private void RaisePropertyChanged(string propertyname)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyname));
        }
    }
}
