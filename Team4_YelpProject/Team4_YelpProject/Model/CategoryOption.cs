namespace Team4_YelpProject.Model
{
    using System;
    using System.ComponentModel;

    public class CategoryOption : INotifyPropertyChanged
    {
        private string category;
        private string business_id;

        public string Category
        {
            get { return this.category; }
            set
            {
                this.category = value;
                this.OnPropertyChanged("Category");
            }
        }

        private string Business_id
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
