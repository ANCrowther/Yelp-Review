namespace Team4_YelpProject.Model
{
    using Npgsql;
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Text;

    public class YelpServices
    {
        public YelpServices() { }

        private string buildConnectionString()
        {
            return "Host = localhost; Username = postgres; Database = milestone2db; password = spiffy";
        }

        /*    USER VIEW    */

        public List<YelpUser> SearchUser(string name)
        {
            List<YelpUser> ObjUser = new List<YelpUser>();

            using (var connection = new NpgsqlConnection(buildConnectionString()))
            {
                connection.Open();
                using (var cmd = new NpgsqlCommand())
                {
                    cmd.Connection = connection;
                    cmd.CommandText = "SELECT distinct user_id,name,average_stars, fans, cool, funny, useful,totallikes,tipcount,date(yelping_since),user_latitude,user_longitude FROM users WHERE name='" + name + "' OR user_id='" + name + "';";
                    try
                    {
                        var R = cmd.ExecuteReader();
                        while (R.Read())
                        {
                            ObjUser.Add(new YelpUser
                            {
                                User_id = R.GetString(0),
                                Name = R.GetString(1),
                                AvgStars = R.GetDouble(2),
                                Fans = R.GetInt32(3),
                                Cool = R.GetInt32(4),
                                Funny = R.GetInt32(5),
                                Useful = R.GetInt32(6),
                                Totallikes = R.GetInt32(7),
                                Tipcount = R.GetInt32(8),
                                Yelping_since = R.GetDate(9).ToString(),
                                Latitude = R.GetDouble(10),
                                Longitude = R.GetDouble(11)
                            });
                        }
                    }
                    catch (NpgsqlException ex)
                    {
                        System.Windows.MessageBox.Show("SQL ERROR: " + ex.Message.ToString());
                    }
                    finally
                    {
                        connection.Close();
                    }
                }
            }

            return ObjUser;
        }

        public List<YelpUser> SearchUserFriends(string id)
        {
            List<YelpUser> ObjUser = new List<YelpUser>();

            using (var connection = new NpgsqlConnection(buildConnectionString()))
            {
                connection.Open();
                using (var cmd = new NpgsqlCommand())
                {
                    cmd.Connection = connection;
                    cmd.CommandText = "SELECT distinct U.user_id,U.name,U.average_stars, U.fans, U.cool, U.funny, U.useful,totallikes,U.tipcount,date(U.yelping_since),U.user_latitude,U.user_longitude FROM users AS U,friend WHERE U.user_id=friend.friend_id AND friend.user_id=(SELECT U1.user_id FROM users AS U1 WHERE U1.user_id='" + id + "' ORDER BY name,average_stars,totallikes);";

                    try
                    {
                        var R = cmd.ExecuteReader();
                        while (R.Read())
                        {
                            ObjUser.Add(new YelpUser
                            {
                                User_id = R.GetString(0),
                                Name = R.GetString(1),
                                AvgStars = R.GetDouble(2),
                                Fans = R.GetInt32(3),
                                Cool = R.GetInt32(4),
                                Funny = R.GetInt32(5),
                                Useful = R.GetInt32(6),
                                Totallikes = R.GetInt32(7),
                                Tipcount = R.GetInt32(8),
                                Yelping_since = R.GetDate(9).ToString(),
                                Latitude = R.GetDouble(10),
                                Longitude = R.GetDouble(11)
                            });
                        }
                    }
                    catch (NpgsqlException ex)
                    {
                        System.Windows.MessageBox.Show("SQL ERROR: " + ex.Message.ToString());
                    }
                    finally
                    {
                        connection.Close();
                    }
                }
            }
            return ObjUser;
        }

        public List<Tips> SearchFriendTips(string id)
        {
            List<Tips> ObjUser = new List<Tips>();

            using (var connection = new NpgsqlConnection(buildConnectionString()))
            {
                connection.Open();
                using (var cmd = new NpgsqlCommand())
                {
                    cmd.Connection = connection;
                    cmd.CommandText = "SELECT date(T.tipdate), U.name, B.name, B.city, T.likes, T.business_id, T.user_id, T.text FROM Business AS B, tip AS T, users AS U,(SELECT F.friend_id FROM users AS U1, friend AS F WHERE U1.user_id = '" + id + "' AND U1.user_id = F.user_id) AS T1 WHERE T1.friend_id = T.user_id AND B.business_id = T.business_id AND T.user_id = U.user_id ORDER BY date(T.tipdate) DESC;";

                    try
                    {
                        var R = cmd.ExecuteReader();
                        while (R.Read())
                        {
                            ObjUser.Add(new Tips
                            {
                                Date = R.GetDate(0).ToString(),
                                UserName = R.GetString(1),
                                BusinessName = R.GetString(2),
                                City = R.GetString(3),
                                Likes = R.GetInt32(4),
                                BusinessID = R.GetString(5),
                                UserID = R.GetString(6),
                                Text = R.GetString(7)
                            });
                        }
                    }
                    catch (NpgsqlException ex)
                    {
                        System.Windows.MessageBox.Show("SQL ERROR: " + ex.Message.ToString());
                    }
                    finally
                    {
                        connection.Close();
                    }
                }
            }

            return ObjUser;
        }

        public bool UpdateLocation(YelpUser ObjUser)
        {
            bool IsUpdated = false;

            using (var connection = new NpgsqlConnection(buildConnectionString()))
            {
                connection.Open();
                using (var cmd = new NpgsqlCommand())
                {
                    try
                    {
                        cmd.Connection = connection;
                        cmd.CommandText = "UPDATE users SET user_latitude='" + ObjUser.Latitude + "', user_longitude='" + ObjUser.Longitude + "' WHERE user_id='" + ObjUser.User_id + "';";
                        cmd.ExecuteNonQuery();
                        IsUpdated = true;
                    }
                    catch (NpgsqlException ex)
                    {
                        System.Windows.MessageBox.Show("SQL ERROR: " + ex.Message.ToString());
                    }
                    finally
                    {
                        connection.Close();
                    }
                }
            }

            return IsUpdated;
        }

        /*    BUSINESS VIEW    */

        public List<Business> GetStates()
        {
            List<Business> ObjStateList = new List<Business>();

            using (var connection = new NpgsqlConnection(buildConnectionString()))
            {
                connection.Open();
                using (var cmd = new NpgsqlCommand())
                {
                    cmd.Connection = connection;
                    cmd.CommandText = "SELECT DISTINCT state FROM business ORDER BY state";
                    try
                    {
                        var reader = cmd.ExecuteReader();
                        while (reader.Read())
                        {
                            ObjStateList.Add(new Business { State = reader.GetString(0) });
                        }
                    }
                    catch (NpgsqlException ex)
                    {
                        System.Windows.MessageBox.Show("SQL ERROR: " + ex.Message.ToString());
                    }
                    finally
                    {
                        connection.Close();
                    }
                }
            }

            return ObjStateList;
        }

        public List<Business> SearchCities(string state)
        {
            List<Business> ObjCities = new List<Business>();

            using (var connection = new NpgsqlConnection(buildConnectionString()))
            {
                connection.Open();
                using (var cmd = new NpgsqlCommand())
                {
                    cmd.Connection = connection;
                    cmd.CommandText = "SELECT DISTINCT city FROM business WHERE state='" + state + "' ORDER BY city";
                    try
                    {
                        var reader = cmd.ExecuteReader();
                        while (reader.Read())
                        {
                            ObjCities.Add(new Business { City = reader.GetString(0) });
                        }
                    }
                    catch (NpgsqlException ex)
                    {
                        System.Windows.MessageBox.Show("SQL ERROR: " + ex.Message.ToString());
                    }
                    finally
                    {
                        connection.Close();
                    }
                }
            }

            return ObjCities;
        }

        public List<Business> SearchZipcodes(string State, string City)
        {
            List<Business> ObjCities = new List<Business>();

            using (var connection = new NpgsqlConnection(buildConnectionString()))
            {
                connection.Open();
                using (var cmd = new NpgsqlCommand())
                {
                    cmd.Connection = connection;
                    cmd.CommandText = "SELECT DISTINCT state, city, zipcode FROM business WHERE state='" + State + "' AND city='" + City + "' ORDER BY zipcode";

                    try
                    {
                        var R = cmd.ExecuteReader();
                        while (R.Read())
                        {
                            ObjCities.Add(new Business 
                            {
                                State = R.GetString(0), 
                                City = R.GetString(1), 
                                Zipcode = R.GetInt32(2) 
                            });
                        }
                    }
                    catch (NpgsqlException ex)
                    {
                        System.Windows.MessageBox.Show("SQL ERROR: " + ex.Message.ToString());
                    }
                    finally
                    {
                        connection.Close();
                    }
                }
            }

            return ObjCities;
        }

        public List<Business> SearchCategoryList(Business B)
        {
            List<Business> ObjZipcodes = new List<Business>();

            using (var connection = new NpgsqlConnection(buildConnectionString()))
            {
                connection.Open();
                using (var cmd = new NpgsqlCommand())
                {
                    cmd.Connection = connection;
                    cmd.CommandText = "SELECT DISTINCT C.category FROM business AS B, categories AS C WHERE B.business_id=C.business_id AND state='" + B.State + "' AND city='" + B.City + "' AND zipcode='" + B.Zipcode + "' ORDER BY category;";

                    try
                    {
                        var R = cmd.ExecuteReader();
                        while (R.Read())
                        {
                            ObjZipcodes.Add(new Business { Category = R.GetString(0) });
                        }
                    }
                    catch (NpgsqlException ex)
                    {
                        System.Windows.MessageBox.Show("SQL ERROR: " + ex.Message.ToString());
                    }
                    finally
                    {
                        connection.Close();
                    }
                }
            }

            return ObjZipcodes;
        }

        public double DetermineDistance(Business location, YelpUser uLoc)
        {
            Business ObjBusiness = new Business();

            using (var connection = new NpgsqlConnection(buildConnectionString()))
            {
                connection.Open();
                using (var cmd = new NpgsqlCommand())
                {
                    cmd.Connection = connection;
                    cmd.CommandText = "SELECT mydistance (" + location.Latitude + ", "+ location.Longitude + ", "+ uLoc.Latitude + ", " + uLoc.Longitude + ");";

                    try
                    {
                        var R = cmd.ExecuteReader();
                        while (R.Read())
                        {
                            ObjBusiness.Distance = R.GetDouble(0);
                        }
                    }
                    catch (NpgsqlException ex)
                    {
                        System.Windows.MessageBox.Show("SQL ERROR: " + ex.Message.ToString());
                    }
                    finally
                    {
                        connection.Close();
                    }
                }
            }

            return ObjBusiness.Distance;
        }

        public StringBuilder BusinessChosenSelection(StringBuilder sqlStr, ObservableCollection<Business> BList, Business location)
        {
            // Having to check the BList 
            if (BList.Count > 0)
            {
                foreach (Business item in BList)
                {
                    sqlStr.Append("(SELECT DISTINCT B.business_id, B.name, B.state, B.city, B.address, B.zipcode, B.latitude, B.longitude, B.stars, B.is_open, B.review_count, B.numtips, B.numcheckins FROM business as B, categories as C where B.business_id = C.business_id AND state='" + location.State + "' AND city='" + location.City + "' AND zipcode='" + location.Zipcode + "' AND category='" + item.Category + "') INTERSECT");
                };
                if (sqlStr.Length > 9)
                    sqlStr.Remove(sqlStr.Length - 9, 9);
                sqlStr.Append(";");
            }
            else
            {
                return BusinessNoSelection(sqlStr, location);
            }

            return sqlStr;
        }

        public StringBuilder BusinessNoSelection(StringBuilder sqlStr, Business location)
        {
            return sqlStr.Append("SELECT DISTINCT B.business_id,B.name,B.state,B.city,B.address,B.zipcode,B.latitude,B.longitude,B.stars,B.is_open,B.review_count,B.numtips,B.numcheckins FROM business as B, categories AS C WHERE state = '" + location.State + "' AND city = '" + location.City + "' AND zipcode = '" + location.Zipcode + "';");
        }

        public List<Business> SearchBusinesses(ObservableCollection<Business> BList, Business location)
        {
            List<Business> ObjBusinesses = new List<Business>();
            StringBuilder sqlStr = new StringBuilder();

            if (BList != null)
            {
                sqlStr = BusinessChosenSelection(sqlStr, BList, location);
            }
            else
            {
                sqlStr = BusinessNoSelection(sqlStr, location);
            }

            Console.WriteLine("Business Selection: " + sqlStr);

            using (var connection = new NpgsqlConnection(buildConnectionString()))
            {
                connection.Open();
                using (var cmd = new NpgsqlCommand())
                {
                    cmd.Connection = connection;
                    cmd.CommandText = sqlStr.ToString();

                    try
                    {
                        var R = cmd.ExecuteReader();
                        while (R.Read())
                        {
                            ObjBusinesses.Add(new Business
                            {
                                BusinessID = R.GetString(0),
                                BusinessName = R.GetString(1),
                                State = R.GetString(2),
                                City = R.GetString(3),
                                Address = R.GetString(4),
                                Zipcode = R.GetInt32(5),
                                Latitude = R.GetDouble(6),
                                Longitude = R.GetDouble(7),
                                Stars = R.GetDouble(8),
                                IsOpen = R.GetBoolean(9),
                                ReviewCount = R.GetInt32(10),
                                NumberOfTips = R.GetInt32(11),
                                TotalCheckins = R.GetInt32(12)
                            });
                        }
                    }
                    catch (NpgsqlException ex)
                    {
                        System.Windows.MessageBox.Show("SQL ERROR: " + ex.Message.ToString());
                    }
                    finally
                    {
                        connection.Close();
                    }
                }
            }

            return ObjBusinesses;
        }

        public BusinessHours SearchForHours(Business B)
        {
            BusinessHours ObjHours = new BusinessHours();

            using (var connection = new NpgsqlConnection(buildConnectionString()))
            {
                connection.Open();
                using (var cmd = new NpgsqlCommand())
                {
                    cmd.Connection = connection;
                    cmd.CommandText = "SELECT H.business_id, H.day_of_week, H.open, H.close FROM business AS B, hours AS H WHERE B.business_id=H.business_id AND B.business_id='" + B.BusinessID + "' AND H.day_of_week='" + DateTime.Today.DayOfWeek + "';";

                    try
                    {
                        var R = cmd.ExecuteReader();
                        while (R.Read())
                        {
                            ObjHours.BusinessID = R.GetString(0);
                            ObjHours.Day = R.GetString(1);
                            ObjHours.Open = R.GetValue(2).ToString();
                            ObjHours.Close = R.GetValue(3).ToString();
                        }
                    }
                    catch (NpgsqlException ex)
                    {
                        System.Windows.MessageBox.Show("SQL ERROR: " + ex.Message.ToString());
                    }
                    finally
                    {
                        connection.Close();
                    }
                }
            }

            return ObjHours;
        }

        public List<Business> GetCategories(string bid)
        {
            List<Business> ObjCatList = new List<Business>();

            using (var connection = new NpgsqlConnection(buildConnectionString()))
            {
                connection.Open();
                using (var cmd = new NpgsqlCommand())
                {
                    cmd.Connection = connection;
                    cmd.CommandText = "SELECT category, business_id FROM categories WHERE business_id='" + bid + "';";

                    try
                    {
                        var R = cmd.ExecuteReader();
                        while (R.Read())
                        {
                            ObjCatList.Add(new Business
                            {
                                Category = R.GetString(0),
                                BusinessID = R.GetString(1)
                            });
                        }
                    }
                    catch (NpgsqlException ex)
                    {
                        System.Windows.MessageBox.Show("SQL ERROR: " + ex.Message.ToString());
                    }
                    finally
                    {
                        connection.Close();
                    }
                }
            }

            return ObjCatList;
        }

        public List<Business> GetAttributes(string bid)
        {
            List<Business> ObjCatList = new List<Business>();

            using (var connection = new NpgsqlConnection(buildConnectionString()))
            {
                connection.Open();
                using (var cmd = new NpgsqlCommand())
                {
                    cmd.Connection = connection;
                    cmd.CommandText = "SELECT attr_name, business_id FROM attributes WHERE business_id='" + bid + "' AND value='True';";

                    try
                    {
                        var R = cmd.ExecuteReader();
                        while (R.Read())
                        {
                            ObjCatList.Add(new Business
                            {
                                Attribute = R.GetString(0),
                                BusinessID = R.GetString(1)
                            });
                        }
                    }
                    catch (NpgsqlException ex)
                    {
                        System.Windows.MessageBox.Show("SQL ERROR: " + ex.Message.ToString());
                    }
                    finally
                    {
                        connection.Close();
                    }
                }
            }

            return ObjCatList;
        }

        /*    LOAD BUSINESS TIPS WINDOW    */

        public List<Tips> GetTips(string bid)
        {
            List<Tips> ObjTipsList = new List<Tips>();

            using (var connection = new NpgsqlConnection(buildConnectionString()))
            {
                connection.Open();
                using (var cmd = new NpgsqlCommand())
                {
                    cmd.Connection = connection;
                    cmd.CommandText = "SELECT T.tipdate, U.name, B.name, B.city, T.text, T.likes, T.business_id, T.user_id FROM Business AS B, tip AS T, users AS U WHERE T.user_id=U.user_id AND T.business_id=B.business_id AND T.business_id='" + bid +"' ORDER BY date(T.tipdate) DESC;";

                    try
                    {
                        var R = cmd.ExecuteReader();
                        while (R.Read())
                        {
                            ObjTipsList.Add(new Tips
                            {
                                Date = R.GetTimeStamp(0).ToString(),
                                UserName = R.GetString(1),
                                BusinessName = R.GetString(2),
                                City = R.GetString(3),
                                Text = R.GetString(4),
                                Likes = R.GetInt32(5),
                                BusinessID = R.GetString(6),
                                UserID = R.GetString(7)
                            });
                        }
                    }
                    catch (NpgsqlException ex)
                    {
                        System.Windows.MessageBox.Show("SQL ERROR: " + ex.Message.ToString());
                    }
                    finally
                    {
                        connection.Close();
                    }
                }
            }

            return ObjTipsList;
        }

        public List<Tips> GetFriendTips(string B, string U)
        {
            List<Tips> ObjTipsList = new List<Tips>();

            using (var connection = new NpgsqlConnection(buildConnectionString()))
            {
                connection.Open();
                using (var cmd = new NpgsqlCommand())
                {
                    cmd.Connection = connection;
                    cmd.CommandText = "SELECT date(T.tipdate), U.name, B.name, B.city, T.text, T.likes, T.business_id, T.user_id FROM Business AS B, tip AS T, users AS U,(SELECT F.friend_id FROM users AS U1, friend AS F WHERE U1.user_id = '" + U + "' AND U1.user_id = F.user_id) AS T1 WHERE T1.friend_id = T.user_id AND B.business_id = T.business_id AND T.user_id = U.user_id AND T.business_id='" + B + "' ORDER BY date(T.tipdate) DESC;";

                    try
                    {
                        var R = cmd.ExecuteReader();
                        while (R.Read())
                        {
                            ObjTipsList.Add(new Tips
                            {
                                Date = R.GetDate(0).ToString(),
                                UserName = R.GetString(1),
                                BusinessName = R.GetString(2),
                                City = R.GetString(3),
                                Text = R.GetString(4),
                                Likes = R.GetInt32(5),
                                BusinessID = R.GetString(6),
                                UserID = R.GetString(7)
                            });
                        }
                    }
                    catch (NpgsqlException ex)
                    {
                        System.Windows.MessageBox.Show("SQL ERROR: " + ex.Message.ToString());
                    }
                    finally
                    {
                        connection.Close();
                    }
                }
            }

            return ObjTipsList;
        }

        public bool UpdateLikeTips(Tips T)
        {
            bool IsUpdated = false;

            using (var connection = new NpgsqlConnection(buildConnectionString()))
            {
                connection.Open();
                using (var cmd = new NpgsqlCommand())
                {
                    try
                    {
                        cmd.Connection = connection;
                        cmd.CommandText = "UPDATE tip SET likes=likes+1 WHERE user_id='" + T.UserID + "' AND business_id='" + T.BusinessID + "' AND tipdate='" + T.Date +"';";
                        cmd.ExecuteNonQuery();
                        IsUpdated = true;
                    }
                    catch (NpgsqlException ex)
                    {
                        System.Windows.MessageBox.Show("SQL ERROR: " + ex.Message.ToString());
                    }
                    finally
                    {
                        connection.Close();
                    }
                }
            }

            return IsUpdated;
        }

        public bool AddToTips(Tips T)
        {
            bool IsUpdated = false;

            using (var connection = new NpgsqlConnection(buildConnectionString()))
            {
                connection.Open();
                using (var cmd = new NpgsqlCommand())
                {
                    try
                    {
                        cmd.Connection = connection;
                        cmd.CommandText = "INSERT INTO tip(tipdate, business_id, user_id, text, likes) VALUES(current_timestamp, '" + T.BusinessID + "', '" + T.UserID + "', '" + T.Text + "', 0);";
                        cmd.ExecuteNonQuery();
                        IsUpdated = true;
                    }
                    catch (NpgsqlException ex)
                    {
                        System.Windows.MessageBox.Show("SQL ERROR: " + ex.Message.ToString());
                    }
                    finally
                    {
                        connection.Close();
                    }
                }
            }

            return IsUpdated;
        }

        /*    LOAD CHECKINS WINDOW    */

        public List<KeyValuePair<string, int>> GetCheckins(string bid)
        {
            List<KeyValuePair<string, int>> ObjCheckins = new List<KeyValuePair<string, int>>();

            using (var connection = new NpgsqlConnection(buildConnectionString()))
            {
                connection.Open();
                using (var cmd = new NpgsqlCommand())
                {
                    cmd.Connection = connection;
                    cmd.CommandText = "SELECT month, count(month) FROM checkins where business_id = '" + bid + "' group by month;";

                    try
                    {
                        var R = cmd.ExecuteReader();
                        while (R.Read())
                        {
                            ObjCheckins.Add(new KeyValuePair<string, int>(GetMonth(R.GetInt32(0)), R.GetInt32(1)));
                        }
                    }
                    catch (NpgsqlException ex)
                    {
                        System.Windows.MessageBox.Show("SQL ERROR: " + ex.Message.ToString());
                    }
                    finally
                    {
                        connection.Close();
                    }
                }
            }

            return ObjCheckins;
        }

        private string GetMonth(int mo)
        {
            switch (mo)
            {
                case 1:
                    return "January";
                case 2:
                    return "February";
                case 3:
                    return "March";
                case 4:
                    return "April";
                case 5:
                    return "May";
                case 6:
                    return "June";
                case 7:
                    return "July";
                case 8:
                    return "August";
                case 9:
                    return "September";
                case 10:
                    return "October";
                case 11:
                    return "November";
                case 12:
                    return "December";
                default:
                    return "Fail";
            }
        }

        public bool AddCheckin(string bid)
        {
            bool IsUpdated = false;
            
            using (var connection = new NpgsqlConnection(buildConnectionString()))
            {
                connection.Open();
                using (var cmd = new NpgsqlCommand())
                {
                    try
                    {
                        cmd.Connection = connection;
                        cmd.CommandText = "INSERT INTO checkins(business_id, year, month, day, time) VALUES ('"+bid+ "', date_part('year', current_timestamp), date_part('month', current_timestamp), date_part('day', current_timestamp), current_time);";
                        cmd.ExecuteNonQuery();
                        IsUpdated = true;
                    }
                    catch (NpgsqlException ex)
                    {
                        System.Windows.MessageBox.Show("SQL ERROR: " + ex.Message.ToString());
                    }
                    finally
                    {
                        connection.Close();
                    }
                }
            }

            return IsUpdated;
        }
    }
}
