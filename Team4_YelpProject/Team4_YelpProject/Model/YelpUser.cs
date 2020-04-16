namespace Team4_YelpProject.Model
{
    using System.ComponentModel;

    public class YelpUser : INotifyPropertyChanged
    {
        private string user_id;
        private string name;
        private double average_stars;
        private int fans;
        private int cool;
        private int funny;
        private int useful;
        private string yelping_since;
        private double user_latitude;
        private double user_longitude;

        public string User_id
        {
            get { return this.user_id; }
            set
            {
                this.user_id = value;
                this.OnPropertyChanged("User_id");
            }
        }

        public string Name
        {
            get { return this.name; }
            set
            {
                this.name = value;
                this.OnPropertyChanged("Name");
            }
        }

        public double Average_stars
        {
            get { return this.average_stars; }
            set
            {
                this.average_stars = value;
                this.OnPropertyChanged("Average_stars");

            }
        }

        public int Fans
        {
            get { return this.fans; }
            set
            {
                this.fans = value;
                this.OnPropertyChanged("Fans");
            }
        }

        public int Cool
        {
            get { return this.cool; }
            set
            {
                this.cool = value;
                this.OnPropertyChanged("Cool");
            }
        }

        public int Funny
        {
            get { return this.funny; }
            set
            {
                this.funny = value;
                this.OnPropertyChanged("Funny");
            }
        }

        public int Useful
        {
            get { return this.useful; }
            set
            {
                this.useful = value;
                this.OnPropertyChanged("Useful");
            }
        }

        public string Yelping_since
        {
            get { return this.yelping_since; }
            set
            {
                this.yelping_since = value;
                this.OnPropertyChanged("Yelping_since");
            }
        }

        public double User_latitude
        {
            get { return this.user_latitude; }
            set
            {
                this.user_latitude = value;
                this.OnPropertyChanged("User_latitude");

            }
        }

        public double User_longitude
        {
            get { return this.user_longitude; }
            set
            {
                this.user_longitude = value;
                this.OnPropertyChanged("User_longitude");

            }
        }

        public YelpUser()
        {
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string message)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(message));
        }
    }
}
