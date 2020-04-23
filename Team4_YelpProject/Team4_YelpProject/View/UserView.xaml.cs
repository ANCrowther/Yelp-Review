namespace Team4_YelpProject.View
{
    using Npgsql;
    using System;
    using System.Windows;
    using System.Windows.Controls;
    using Team4_YelpProject.Model;

    /// <summary>
    /// Interaction logic for UserView.xaml
    /// </summary>
    public partial class UserView : UserControl
    {
        YelpUser currentUser = new YelpUser();

        public UserView()
        {
            InitializeComponent();
        }

        private string buildConnectionString()
        {
            return "Host = localhost; Username = postgres; Database = milestone2db; password = spiffy";
        }

        private void clearFields()
        {
            UserNameTB.Clear();
            UserLatitudeTB.Clear();
            UserLongitudeTB.Clear();
            UserNameBlock.Text = string.Empty;
            YelpingSinceBlock.Text = string.Empty;
            UserStarsBlock.Text = string.Empty;
            UserFansBlock.Text = string.Empty;
            UserTipCountBlock.Text = string.Empty;
            UserLikeBlock.Text = string.Empty;
            UserFunnyBlock.Text = string.Empty;
            UserCoolBlock.Text = string.Empty;
            UserUsefulBlock.Text = string.Empty;
            FriendListDataGrid.Items.Clear();
            ReviewByFriendDataGrid.Items.Clear();
        }

        private void executeQuery(string sqlstr, Action<NpgsqlDataReader> myf)
        {
            using (var connection = new NpgsqlConnection(buildConnectionString()))
            {
                connection.Open();
                using (var cmd = new NpgsqlCommand())
                {
                    cmd.Connection = connection;
                    cmd.CommandText = sqlstr;
                    try
                    {
                        var reader = cmd.ExecuteReader();
                        while (reader.Read())
                        {
                            myf(reader);
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
        }

        private void BtnSearch_Click(object sender, RoutedEventArgs e)
        {
            userIDLB.Items.Clear();
            string sqlStr = "SELECT distinct user_id,name FROM users WHERE name='" + UserNameTB.Text + "';";
            executeQuery(sqlStr, addUserIDListBox);

        }

        private void addUserIDListBox(NpgsqlDataReader R)
        {
            userIDLB.Items.Add(R.GetString(0));
        }

        private void userIDLB_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            clearFields();

            if (userIDLB.SelectedIndex >= 0)
            {
                /*    Run user query    */
                string sqlStr = "SELECT distinct user_id,name,average_stars,fans,funny,cool,useful,date(yelping_since),user_latitude,user_longitude, tipcount, totallikes FROM users WHERE user_id='" + userIDLB.SelectedItem.ToString() + "';";
                executeQuery(sqlStr, addUser);

                /*    Run friend list query    */
                string sqlStr1 = "SELECT name,average_stars,totallikes, date(yelping_since) FROM users,friend WHERE users.user_id=friend.friend_id AND friend.user_id=(SELECT U1.user_id FROM users AS U1 WHERE U1.user_id='" + userIDLB.SelectedItem.ToString() + "' ORDER BY name,average_stars,totallikes);";
                executeQuery(sqlStr1, addFriendsGridRow);

                /*    Run Tips list query    */
                string sqlStr2 = "SELECT U.name, B.name, B.city, text, date(T.tipdate) FROM Business AS B, tip AS T, users AS U,(SELECT F.friend_id FROM users AS U1, friend AS F WHERE U1.user_id = '" + userIDLB.SelectedItem.ToString() + "' AND U1.user_id = F.user_id) AS T1 WHERE T1.friend_id = T.user_id AND B.business_id = T.business_id AND T.user_id = U.user_id ORDER BY date(T.tipdate) DESC;";
                executeQuery(sqlStr2, addTipsGridRow);
            }
        }

        private void addUser(NpgsqlDataReader R)
        {
            currentUser.user_id = R.GetString(0);
            currentUser.name = R.GetString(1);
            currentUser.avgStars = R.GetDouble(2);
            currentUser.fans = R.GetInt32(3);
            currentUser.funny = R.GetInt32(4);
            currentUser.cool = R.GetInt32(5);
            currentUser.useful = R.GetInt32(6);
            currentUser.yelping_since = R.GetDate(7).ToString();
            currentUser.latitude = R.GetDouble(8);
            currentUser.longitude = R.GetDouble(9);
            currentUser.tipcount = R.GetInt32(10);
            currentUser.totallikes = R.GetInt32(11);

            addUserData();
        }

        private void addUserData()
        {
            UserNameBlock.Text = currentUser.name;
            YelpingSinceBlock.Text = currentUser.yelping_since;
            UserStarsBlock.Text = currentUser.avgStars.ToString();
            UserFansBlock.Text = currentUser.fans.ToString();
            UserTipCountBlock.Text = currentUser.tipcount.ToString();
            UserLikeBlock.Text = currentUser.totallikes.ToString();
            UserFunnyBlock.Text = currentUser.funny.ToString();
            UserCoolBlock.Text = currentUser.cool.ToString();
            UserUsefulBlock.Text = currentUser.useful.ToString();
            UserLatitudeTB.Text = currentUser.latitude.ToString();
            UserLongitudeTB.Text = currentUser.longitude.ToString();
        }

        private void addFriendsGridRow(NpgsqlDataReader R)
        {
            FriendListDataGrid.Items.Add(new YelpUser() { name = R.GetString(0), avgStars = R.GetDouble(1), totallikes = R.GetInt32(2), yelping_since = R.GetDate(3).ToString() });
        }

        private void addTipsGridRow(NpgsqlDataReader R)
        {
            ReviewByFriendDataGrid.Items.Add(new Tips() { userName = R.GetString(0), businessName = R.GetString(1), city = R.GetString(2), text = R.GetString(3), date = R.GetDate(4).ToString() });
        }

        private void updateLocation_Click(object sender, RoutedEventArgs e)
        {
            if (!(currentUser.latitude == Convert.ToDouble(UserLatitudeTB.Text) && currentUser.longitude == Convert.ToDouble(UserLongitudeTB.Text)))
            {
                updateQuery();
                addUserData();
            }
        }

        private void updateQuery()
        {
            string sqlStr = "UPDATE Users SET user_latitude='" + Convert.ToDouble(UserLatitudeTB.Text) + "', user_longitude='" + Convert.ToDouble(UserLongitudeTB.Text) + "' WHERE user_id='" + currentUser.user_id + "';";

            /*    Updates the user object for display purposes    */
            currentUser.latitude = Convert.ToDouble(UserLatitudeTB.Text);
            currentUser.longitude = Convert.ToDouble(UserLongitudeTB.Text);

            /*    Updates the DB    */
            using (var connection = new NpgsqlConnection(buildConnectionString()))
            {
                connection.Open();
                using (var cmd = new NpgsqlCommand())
                {
                    cmd.Connection = connection;
                    cmd.CommandText = sqlStr;
                    try
                    {
                        cmd.ExecuteNonQuery();
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
        }
    }
}
