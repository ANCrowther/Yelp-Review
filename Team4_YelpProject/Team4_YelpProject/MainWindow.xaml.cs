using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using Npgsql;

namespace Team4_YelpProject
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public class FriendsList
        {
            public string name { get; set; }
            public int totalLikes { get; set; }
            public double avgStars { get; set; }
            public string yelpingSince { get; set; }
        }

        public class TipsList
        {
            public string userName { get; set; }
            public string businessName { get; set; }
            public string city { get; set; }
            public string text { get; set; }
            public string date { get; set; }
        }

        public MainWindow()
        {
            InitializeComponent();
            addFriendsGridColumns();
            addLatestTipsGridColumns();
            addState();
            initializeCategoryList();
        }

        private string buildConnectionString()
        {
            return "Host = localhost; Username = postgres; Database = milestone2db; password = spiffy";
        }

        private void addFriendsGridColumns()
        {
            DataGridTextColumn col1 = new DataGridTextColumn();
            col1.Binding = new Binding("name");
            col1.Header = "Name";
            col1.Width = 255;
            FriendListDataGrid.Columns.Add(col1);

            DataGridTextColumn col2 = new DataGridTextColumn();
            col2.Binding = new Binding("totalLikes");
            col2.Header = "Total Likes";
            col2.Width = 60;
            FriendListDataGrid.Columns.Add(col2);

            DataGridTextColumn col3 = new DataGridTextColumn();
            col3.Binding = new Binding("avgStars");
            col3.Header = "Avg Stars";
            col3.Width = 150;
            FriendListDataGrid.Columns.Add(col3);

            DataGridTextColumn col4 = new DataGridTextColumn();
            col4.Binding = new Binding("yelpingSince");
            col4.Header = "Yelping Since";
            col4.Width = 0;
            FriendListDataGrid.Columns.Add(col4);
        }

        private void addLatestTipsGridColumns()
        {
            DataGridTextColumn col1 = new DataGridTextColumn();
            col1.Binding = new Binding("userName");
            col1.Header = "User Name";
            col1.Width = 100;
            ReviewByFriendDataGrid.Columns.Add(col1);

            DataGridTextColumn col2 = new DataGridTextColumn();
            col2.Binding = new Binding("businessName");
            col2.Header = "Business";
            col2.Width = 100;
            ReviewByFriendDataGrid.Columns.Add(col2);

            DataGridTextColumn col3 = new DataGridTextColumn();
            col3.Binding = new Binding("city");
            col3.Header = "City";
            col3.Width = 70;
            ReviewByFriendDataGrid.Columns.Add(col3);

            DataGridTextColumn col4 = new DataGridTextColumn();
            col4.Binding = new Binding("text");
            col4.Header = "Text";
            col4.Width = 400;
            ReviewByFriendDataGrid.Columns.Add(col4);

            DataGridTextColumn col5 = new DataGridTextColumn();
            col4.Binding = new Binding("date");
            col4.Header = "Date";
            col4.Width = 50;
            ReviewByFriendDataGrid.Columns.Add(col5);
        }

        private void setUserData(NpgsqlDataReader R)
        {
            while(R.Read())
            {
                //userIDListBox.Items.Add(R.GetString(0));
                UserNameBox.Text = R.GetString(1);
                UserStarsResult.Content = R.GetDouble(2).ToString();
                UserFansResult.Content = R.GetInt16(3).ToString();
                UserYelpingSinceResult.Content = R.GetDate(4).ToString();
                FunnyCount.Content = R.GetInt16(5).ToString();
                CoolCount.Content = R.GetInt16(6).ToString();
                UsefulCount.Content = R.GetInt16(7).ToString();
                LatTextBox.Text = R.GetDouble(8).ToString();
                LongTextBox.Text = R.GetDouble(9).ToString();
            }
        }

        private void setFriendData(NpgsqlDataReader R)
        {
            while (R.Read())
            {
                addFriendsGridRow(R);
            }
        }

        private void setTipReviewList(NpgsqlDataReader R)
        {
            while (R.Read())
            {
                addTipsGridRow(R);
            }
        }

        private void clearUserData()
        {
            UserNameBox.Clear();
            UserStarsResult.Content = "_____";
            UserFansResult.Content = "_____";
            UserYelpingSinceResult.Content = "_____";
            FunnyCount.Content = "_____";
            CoolCount.Content = "_____";
            UsefulCount.Content = "_____";
            LatTextBox.Clear();
            LongTextBox.Clear();
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

        private void userIDListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            clearUserData();

            if (userIDListBox.SelectedIndex >= 0)
            {
                //Run user query
                using (var conn = new NpgsqlConnection(buildConnectionString()))
                {
                    conn.Open();

                    using (var cmd = new NpgsqlCommand())
                    {
                        cmd.Connection = conn;
                        cmd.CommandText = "SELECT distinct user_id,name,average_stars,fans,date(yelping_since),funny,cool,useful,user_latitude,user_longitude FROM users WHERE user_id='" + userIDListBox.SelectedItem.ToString() + "';";
                        setUserData(cmd.ExecuteReader());
                    }

                    conn.Close();
                }

                //Run friend list query
                using (var conn = new NpgsqlConnection(buildConnectionString()))
                {
                    conn.Open();

                    using (var cmd = new NpgsqlCommand())
                    {
                        cmd.Connection = conn;
                        cmd.CommandText = "SELECT name,average_stars,totallikes FROM users,friend WHERE users.user_id=friend.friend_id AND friend.user_id=(SELECT U1.user_id FROM users AS U1 WHERE U1.user_id='" + userIDListBox.SelectedItem.ToString() + "');";
                        setFriendData(cmd.ExecuteReader());
                    }

                    conn.Close();
                }

                //Run Tips list query
                using (var conn = new NpgsqlConnection(buildConnectionString()))
                {
                    conn.Open();

                    using (var cmd = new NpgsqlCommand())
                    {
                        cmd.Connection = conn;
                        cmd.CommandText = "SELECT U.name, B.name, B.city, T.text, T.tipDate FROM tip AS T, business AS B, users AS U,(SELECT distinct friend_id FROM users AS U1, friend AS F WHERE U1.name = '" + userIDListBox.SelectedItem.ToString() + "' AND U1.user_id = F.user_id) AS T1 WHERE friend_id=T.user_id AND B.business_id=T.business_id AND T.user_id=U.user_id";
                        setTipReviewList(cmd.ExecuteReader());
                    }

                    conn.Close();
                }
            }
        }

        private void addTipsGridRow(NpgsqlDataReader R)
        {
            ReviewByFriendDataGrid.Items.Add(new TipsList() { userName = R.GetString(0), businessName = R.GetString(1), city = R.GetString(2), text = R.GetString(2), date = R.GetString(3) });
        }

        private void addFriendsGridRow(NpgsqlDataReader R)
        {
            FriendListDataGrid.Items.Add(new FriendsList() { name = R.GetString(0), avgStars = R.GetDouble(1), totalLikes = R.GetInt32(2) });
        }

        private void addUserIDListBox(NpgsqlDataReader R)
        {
            userIDListBox.Items.Add(R.GetString(0));
        }

        private void UsernameTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            userIDListBox.Items.Clear();
            if (e.Key == Key.Enter)
            {
                string sqlStr = "SELECT distinct user_id,name FROM users WHERE name='" + UserNameTextBox.Text + "';";
                executeQuery(sqlStr, addUserIDListBox);
            }
        }

        private void ReviewByFriendDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
        }

        private void FriendListDataGrid_SelectionChanged_1(object sender, SelectionChangedEventArgs e)
        {
        }

        //Business Tab
        string[] categoryList = new string[10];
        public class Business
        {
            public string businessName { get; set; }
            public string street { get; set; }
            public string city { get; set; }
            public string state { get; set; }
            public double distance { get; set; }
            public double stars { get; set; }
            public int tipCount { get; set; }
            public int totalCheckins { get; set; }
            //public string business_id { get; set; }
        }

        public class CategoryOption
        {
            public string category { get; set; }
            public string business_id { get; set; }
        }

        private void initializeCategoryList()
        {
            for (int index = 0; index < 10; index++)
            {
                categoryList[index] = string.Empty;
            }
        }

        private void addState()
        {
            using (var conn = new NpgsqlConnection(buildConnectionString()))
            {
                conn.Open();
                using (var cmd = new NpgsqlCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandText = "SELECT DISTINCT state FROM business ORDER BY state";
                    try
                    {
                        var reader = cmd.ExecuteReader();
                        while (reader.Read())
                        {
                            stateDropBox.Items.Add(reader.GetString(0));
                        }
                    }
                    catch (NpgsqlException ex)
                    {
                        System.Windows.MessageBox.Show("SQL Error - " + ex.Message.ToString());
                    }
                    finally
                    {
                        conn.Close();
                    }
                }
            }
        }

        private void addCity(NpgsqlDataReader R)
        {
            cityDropBox.Items.Add(R.GetString(0));
        }

        private void addZipcode(NpgsqlDataReader R)
        {
            zipcodeDropBox.Items.Add(R.GetInt32(0));
        }

        private void addCategoryItem(NpgsqlDataReader R)
        {
            CategoryListBox.Items.Add(R.GetString(0));
        }

        //private void addCategories()
        //{
        //    CategoryListBox.Items.Clear();
        //    using (var conn = new NpgsqlConnection(buildConnectionString()))
        //    {
        //        conn.Open();
        //        using (var cmd = new NpgsqlCommand())
        //        {
        //            string sqlStr = "SELECT DISTINCT category FROM categories, business WHERE business.business_id=category.business_id AND state = '" + stateDropBox.SelectedItem.ToString() + "' AND city='" + cityDropBox.SelectedItem.ToString() + "' AND zipcode='" + zipcodeDropBox.SelectedItem.ToString() + "' ORDER BY category";
        //            executeQuery(sqlStr, addCategoryItem);
        //        }
        //    }
        //}

        private void stateDropBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            cityDropBox.Items.Clear();
            zipcodeDropBox.Items.Clear();
            SelectListBox.Items.Clear();
            CategoryListBox.Items.Clear();

            if (stateDropBox.SelectedIndex >= 0)
            {
                string sqlStr = "SELECT DISTINCT city FROM business WHERE state ='" + stateDropBox.SelectedItem.ToString() + "' ORDER BY city;";
                executeQuery(sqlStr, addCity);

                string sqlStr1 = "SELECT DISTINCT zipcode FROM business WHERE state ='" + stateDropBox.SelectedItem.ToString() + "' ORDER BY zipcode;";
                executeQuery(sqlStr1, addZipcode);
            }
        }

        private void cityDropBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            zipcodeDropBox.Items.Clear();

            if (cityDropBox.SelectedIndex >= 0)
            {
                string sqlStr = "SELECT DISTINCT zipcode FROM business WHERE state ='" + stateDropBox.SelectedItem.ToString() + "' AND city='" + cityDropBox.SelectedItem.ToString() + "' ORDER BY zipcode;";
                executeQuery(sqlStr, addZipcode);
            }
        }

        private void zipcodeDropBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            CategoryListBox.Items.Clear();

            if (zipcodeDropBox.SelectedIndex >= 0)
            {
                string sqlStr = "SELECT DISTINCT C.category FROM business AS B, categories AS C WHERE B.business_id=C.business_id AND state='"+ stateDropBox.SelectedItem.ToString() + "' AND city='" + cityDropBox.SelectedItem.ToString() + "' AND zipcode='" + zipcodeDropBox.SelectedItem.ToString() + "' ORDER BY category;";
                executeQuery(sqlStr, addCategoryItem);
            }
        }
    }
}
