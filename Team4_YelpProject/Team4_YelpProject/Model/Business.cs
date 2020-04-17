namespace Team4_YelpProject.Model
{
    using System.ComponentModel;

    public class Business : INotifyPropertyChanged
    {
        private string businessName;
        private string street;
        private string city;
        private string state;
        private double distance;
        private double stars;
        private int tipCount;
        private int totalCheckins;
        private string business_id;

        public string BusinessName
        {
            get { return this.businessName; }
            set
            {
                this.businessName = value;
                this.OnPropertyChanged("BusinessName");
            }
        }

        public string Street
        {
            get { return this.street; }
            set
            {
                this.street = value;
                this.OnPropertyChanged("Street");
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

        public int TipCount
        {
            get { return this.tipCount; }
            set
            {
                this.tipCount = value;
                this.OnPropertyChanged("TipCount");
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

        public string Business_id
        {
            get { return this.business_id; }
            set
            {
                this.business_id = value;
                this.OnPropertyChanged("Business_id");
            }

        }

        private void OnPropertyChanged(string v)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(v));
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
