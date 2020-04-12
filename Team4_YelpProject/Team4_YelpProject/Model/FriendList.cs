namespace Team4_YelpProject
{
    using System.ComponentModel;

    public class FriendList : INotifyPropertyChanged
    {
        public string name { get; set; }
        public int totalLikes { get; set; }
        public double avgStars { get; set; }
        public string yelpingSince { get; set; }

        public FriendList()
        {
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string message)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(message));
        }
    }
}
