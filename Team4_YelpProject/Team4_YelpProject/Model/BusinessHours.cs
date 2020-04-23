namespace Team4_YelpProject.Model
{
    using System.ComponentModel;

    public class BusinessHours : INotifyPropertyChanged
    {
        private string businessID;
        public string BusinessID
        {
            get { return this.businessID; }
            set { this.businessID = value; OnPropertyChanged("BusinessID"); }
        }

        private string day;
        public string Day
        {
            get { return this.day; }
            set { this.day = value; OnPropertyChanged("Day"); }
        }

        private string open;
        public string Open
        {
            get { return this.open; }
            set { this.open = value; OnPropertyChanged("Open"); }
        }

        private string close;
        public string Close
        {
            get { return this.close; }
            set { this.close = value; OnPropertyChanged("Close"); }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
