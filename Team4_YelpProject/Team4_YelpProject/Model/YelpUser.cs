namespace Team4_YelpProject.Model
{
    public class YelpUser
    {
        public string User_id { get; set; }
        public string Name { get; set; }
        public double Average_stars { get; set; }
        public int Fans { get; set; }
        public int Cool { get; set; }
        public int Funny { get; set; }
        public int Useful { get; set; }
        public string Yelping_since { get; set; }
        public double User_latitude { get; set; }
        public double User_longitude { get; set; }

        public YelpUser() {}
    }
}
