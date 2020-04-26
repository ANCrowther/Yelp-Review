namespace Team4_YelpProject.Model
{
    using Npgsql;
    using System;
    using System.Collections.Generic;

    public class YelpServices
    {
        public YelpServices()
        {
        }

        private string buildConnectionString()
        {
            return "Host = localhost; Username = postgres; Database = milestone2db; password = spiffy";
        }

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
                    Console.WriteLine(cmd.CommandText);
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
                    cmd.CommandText = "SELECT DISTINCT date(T.tipdate), U.name, B.name, B.city, T.likes, T.business_id, T.user_id, T.text FROM tip AS t, users AS u, business AS b WHERE t.business_id=B.business_id AND T.user_id=U.user_id AND T.user_id='" + id + "';";
                    Console.WriteLine(cmd.CommandText);
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
    }
}
