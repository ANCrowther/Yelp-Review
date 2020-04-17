namespace Team4_YelpProject.Model
{
    using System.ComponentModel;

    public class BusinessHours : INotifyPropertyChanged
    {
        private string business_id;
        private string day;
        private string open;
        private string close;

        public string Business_id
        {
            get { return this.business_id; }
            set
            {
                this.business_id = value;
                this.OnPropertyChanged("Business_id");
            }
        }

        public string Day
        {
            get { return this.day; }
            set
            {
                this.day = value;
                this.OnPropertyChanged("Day");
            }
        }

        public string Open
        {
            get { return this.open; }
            set
            {
                this.open = value;
                this.OnPropertyChanged("Open");
            }
        }

        public string Close
        {
            get { return this.close; }
            set
            {
                this.close = value;
                this.OnPropertyChanged("Close");
            }
        }

        public BusinessHours() { }

        private void OnPropertyChanged(string v)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(v));
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
