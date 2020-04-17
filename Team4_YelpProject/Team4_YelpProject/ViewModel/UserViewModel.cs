namespace Team4_YelpProject.ViewModel
{
    using Team4_YelpProject.Model;
    using System.Windows.Input;
    using System;
    using System.Diagnostics;
    using System.Collections.Generic;
    using Npgsql;
    using System.Collections.ObjectModel;

    internal class UserViewModel
    {
        public UserViewModel()
        {
            //this.user = new YelpUser();
        }

        public ObservableCollection<YelpUser> User
        {
            get; 
            set;
        }

        public void LoadUsers()
        {
            ObservableCollection<YelpUser> user = new ObservableCollection<YelpUser>();
            user.Add(new YelpUser { });
        }

        //private YelpUser user;

        //public YelpUser User
        //{
        //    get { return this.user; }
        //}

        //private string buildConnectionString()
        //{
        //    return "Host = localhost; Username = postgres; Database = milestone2db; password = spiffy";
        //}

        //private void executeQuery(string sqlstr, Action<NpgsqlDataReader> myf)
        //{
        //    using (var connection = new NpgsqlConnection(buildConnectionString()))
        //    {
        //        connection.Open();
        //        using (var cmd = new NpgsqlCommand())
        //        {
        //            cmd.Connection = connection;
        //            cmd.CommandText = sqlstr;
        //            try
        //            {
        //                var reader = cmd.ExecuteReader();
        //                while (reader.Read())
        //                {
        //                    myf(reader);
        //                }
        //            }
        //            catch (NpgsqlException ex)
        //            {
        //                System.Windows.MessageBox.Show("SQL ERROR: " + ex.Message.ToString());
        //            }
        //            finally
        //            {
        //                connection.Close();
        //            }
        //        }
        //    }
        //}

        //public void BasicSearch(string userID)
        //{
        //    string sqlStr = "SELECT distinct user_id,name FROM users WHERE name='" + userID + "';";
        //    executeQuery(sqlStr, addUser);
        //}

        //public void Search(string userID)
        //{
        //    /*    Run user query    */
        //    if (userID != null)
        //    {
        //        string sqlStr = "SELECT distinct user_id,name,average_stars,fans,funny,cool,useful,date(yelping_since),user_latitude,user_longitude FROM users WHERE user_id='" + userID + "';";
        //        //executeQuery(sqlStr, addUser);
        //    }

        //    ///*    Run friend list query    */
        //    //if (userIDListBox.SelectedIndex >= 0)
        //    //{
        //    //    string sqlStr = "SELECT name,average_stars,totallikes, date(yelping_since) FROM users,friend WHERE users.user_id=friend.friend_id AND friend.user_id=(SELECT U1.user_id FROM users AS U1 WHERE U1.user_id='" + userIDListBox.SelectedItem.ToString() + "' ORDER BY name,average_stars,totallikes);";
        //    //    executeQuery(sqlStr, addFriendGridRow);
        //    //}

        //    ///*    Run Tips list query    */
        //    //if (userIDListBox.SelectedIndex >= 0)
        //    //{
        //    //    string sqlStr = "SELECT U.name, B.name, B.city, text, date(T.tipdate) FROM Business AS B, tip AS T, users AS U,(SELECT F.friend_id FROM users AS U1, friend AS F WHERE U1.user_id = '" + userIDListBox.SelectedItem.ToString() + "' AND U1.user_id = F.user_id) AS T1 WHERE T1.friend_id = T.user_id AND B.business_id = T.business_id AND T.user_id = U.user_id ORDER BY date(T.tipdate) DESC;";
        //    //    executeQuery(sqlStr, addTipGridRow);
        //    //}
        //}

        //private void addUser(NpgsqlDataReader R)
        //{
        //    this.user.User_id = R.GetString(0);
        //    this.user.Name = R.GetString(1);
        //    this.user.Average_stars = R.GetDouble(2);
        //    this.user.Fans = R.GetInt32(3);
        //    this.user.Cool = R.GetInt32(4);
        //    this.user.Funny = R.GetInt32(5);
        //    this.user.Useful = R.GetInt32(6);
        //    this.user.Yelping_since = R.GetDate(7).ToString();
        //    this.user.User_latitude = R.GetDouble(8);
        //    this.user.User_longitude = R.GetDouble(9);

        //    //addUserData();
        //}

        //private YelpUser GetUserData()
        //{
        //    return this.user;
        //}

        //public bool CanUpdate
        //{
        //    get
        //    {
        //        if (User == null) { return false; }
        //        return !String.IsNullOrWhiteSpace(User.User_id);
        //    }
        //}

        //public void SaveChanges()
        //{
        //    Debug.Assert(false, String.Format("{0} was updated.", User.Name));
        //}
    }
}
