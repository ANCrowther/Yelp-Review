namespace Team4_YelpProject
{
    using System.ComponentModel;

    public class TipList : INotifyPropertyChanged
    {
        private string date;
        private string userName;
        private string businessName;
        private string city;
        private int likes;
        private string businessID;
        private string userID;
        private string text;

        public string Date
        {
            get { return this.date; }
            set
            {
                this.date = value;
                this.OnPropertyChanged("Date");
            }
        }

        public string UserName
        {
            get { return this.userName; }
            set
            {
                this.userName = value;
                this.OnPropertyChanged("UserName");
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

        public string City
        {
            get { return this.city; }
            set
            {
                this.city = value;
                this.OnPropertyChanged("City");
            }
        }

        public int Likes
        {
            get { return this.likes; }
            set
            {
                this.likes = value;
                this.OnPropertyChanged("Likes");
            }
        }

        public string BusinessID
        {
            get { return this.businessID; }
            set
            {
                this.businessID = value;
                this.OnPropertyChanged("BusinessID");
            }
        }

        public string UserID
        {
            get { return this.userID; }
            set
            {
                this.userID = value;
                this.OnPropertyChanged("UserID");
            }
        }

        public string Text
        {
            get { return this.text; }
            set
            {
                this.text = value;
                this.OnPropertyChanged("Text");
            }
        }

        public TipList()
        {
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string message)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(message));
        }
    }
}
