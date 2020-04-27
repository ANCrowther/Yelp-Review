namespace Team4_YelpProject.Model
{
    using System;
    using System.ComponentModel;

    public class BusinessHours : INotifyPropertyChanged
    {
        private string businessID;
        public string BusinessID
        {
            get { return this.businessID; }
            set { this.businessID = value; RaisePropertyChanged("BusinessID"); }
        }

        private string day;
        public string Day
        {
            get { return this.day; }
            set { 
                this.day = value; 
                RaisePropertyChanged("Day");
                RaisePropertyChanged("FullHours");
            }
        }

        private string open;
        public string Open
        {
            get { return this.open; }
            set { this.open = value; RaisePropertyChanged("Open"); }
        }

        private string close;
        public string Close
        {
            get { return this.close; }
            set { this.close = value; RaisePropertyChanged("Close"); }
        }

        private string fullHours;
        public string FullHours
        {
            get 
            {
                if (open != null)
                {
                    return day + ": Opens: " + open + "  Closes: " + close; ;
                }
                else
                {
                    return DateTime.Today.DayOfWeek + ": Closed today.";
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void RaisePropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
