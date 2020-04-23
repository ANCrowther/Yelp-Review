namespace Team4_YelpProject.Model
{
    public class BusinessResults
    {
        public string businessID { get; set; }
        public string businessName { get; set; }
        public string address { get; set; }
        public string city { get; set; }
        public string state { get; set; }
        public double distance { get; set; }
        public double stars { get; set; }
        public int numberOfTips { get; set; }
        public int totalCheckins { get; set; }
        public double bLatitude { get; set; }
        public double bLongitude { get; set; }

        public BusinessResults() { }
    }
}
