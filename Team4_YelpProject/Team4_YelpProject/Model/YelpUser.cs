namespace Team4_YelpProject.Model
{
    using Microsoft.Maps.MapControl.WPF;
    using System.ComponentModel;

    public class YelpUser : INotifyPropertyChanged
    {
        private string name;
        public string Name
        {
            get { return this.name; }
            set { this.name = value; RaisePropertyChanged("Name"); }
        }

        private int totallikes;
        public int Totallikes
        {
            get { return this.totallikes; }
            set { this.totallikes = value; RaisePropertyChanged("Totallikes"); }
        }

        private string yelping_since;
        public string Yelping_since
        {
            get { return this.yelping_since; }
            set { this.yelping_since = value; RaisePropertyChanged("Yelping_since"); }
        }

        private double avgStars;
        public double AvgStars
        {
            get { return this.avgStars; }
            set { this.avgStars = value; RaisePropertyChanged("AvgStars"); }
        }

        private int fans;
        public int Fans
        {
            get { return this.fans; }
            set { this.fans = value; RaisePropertyChanged("Fans"); }
        }

        private int cool;
        public int Cool
        {
            get { return this.cool; }
            set { this.cool = value; RaisePropertyChanged("Cool"); }
        }

        private int funny;
        public int Funny
        {
            get { return this.funny; }
            set { this.funny = value; RaisePropertyChanged("Funny"); }
        }

        private int useful;
        public int Useful
        {
            get { return this.useful; }
            set { this.useful = value; RaisePropertyChanged("Useful"); }
        }

        private int tipcount;
        public int Tipcount
        {
            get { return this.tipcount; }
            set { this.tipcount = value; RaisePropertyChanged("Tipcount"); }
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

        private string user_id;
        public string User_id
        {
            get { return this.user_id; }
            set { this.user_id = value; RaisePropertyChanged("User_id"); }
        }

        private Location uLocation;
        public Location ULocation
        {
            get { return new Location(latitude, longitude); }
        }

        public YelpUser() { }

        public event PropertyChangedEventHandler PropertyChanged;
        private void RaisePropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
