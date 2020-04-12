namespace Team4_YelpProject
{
    using System.ComponentModel;

    public class TipList : INotifyPropertyChanged
    {
        public string userName { get; set; }
        public string businessName { get; set; }
        public string city { get; set; }
        public string text { get; set; }
        public string date { get; set; }

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
