using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Team4_YelpProject
{
    public class YelpUser
    {
        public string user_id { get; set; }
        public string name { get; set; }
        public double average_stars { get; set; }
        public int fans { get; set; }
        public int cool { get; set; }
        public int funny { get; set; }
        public int useful { get; set; }
        public string yelping_since { get; set; }
        public double user_latitude { get; set; }
        public double user_longitude { get; set; }

        public YelpUser()
        {
        }
    }
}
