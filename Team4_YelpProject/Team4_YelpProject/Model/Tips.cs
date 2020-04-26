namespace Team4_YelpProject
{
    using System.ComponentModel;

    public class Tips : INotifyPropertyChanged
    {
        private string date;
        public string Date
        {
            get { return this.date; }
            set { this.date = value; RaisePropertyChanged("Date"); }
        }

        private string userName;
        public string UserName
        {
            get { return this.userName; }
            set { this.userName = value; RaisePropertyChanged("UserName"); }
        }

        private string businessName;
        public string BusinessName
        {
            get { return this.businessName; }
            set { this.businessName = value; RaisePropertyChanged("BusinessName"); }
        }

        private string city;
        public string City
        {
            get { return this.city; }
            set { this.city = value; RaisePropertyChanged("City"); }
        }

        private int likes;
        public int Likes
        {
            get { return this.likes; }
            set { this.likes = value; RaisePropertyChanged("Likes"); }
        }

        private string businessID;
        public string BusinessID
        {
            get { return this.businessID; }
            set { this.businessID = value; RaisePropertyChanged("BusinessID"); }
        }

        private string userID;
        public string UserID
        {
            get { return this.userID; }
            set { this.userID = value; RaisePropertyChanged("UserID"); }
        }

        private string text;
        public string Text
        {
            get { return this.text; }
            set { this.text = value; RaisePropertyChanged("Text"); }
        }

        public Tips() { }

        public event PropertyChangedEventHandler PropertyChanged;
        private void RaisePropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
