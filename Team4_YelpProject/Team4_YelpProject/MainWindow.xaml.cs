using System;
using System.Text;
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
        public MainWindow()
        {
            InitializeComponent();
            addFriendsGridColumns();
            addLatestTipsGridColumns();
            addState();
            addBusinessResultGridColumns();
        }

        /*    User Information Tab    */
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
            //col1.Width = 100;
            ReviewByFriendDataGrid.Columns.Add(col1);

            DataGridTextColumn col2 = new DataGridTextColumn();
            col2.Binding = new Binding("businessName");
            col2.Header = "Business";
            col2.Width = 200;
            ReviewByFriendDataGrid.Columns.Add(col2);

            DataGridTextColumn col3 = new DataGridTextColumn();
            col3.Binding = new Binding("city");
            col3.Header = "City";
            col3.Width = 120;
            ReviewByFriendDataGrid.Columns.Add(col3);

            DataGridTextColumn col4 = new DataGridTextColumn();
            col4.Binding = new Binding("text");
            col4.Header = "Text";
            col4.Width = 400;
            ReviewByFriendDataGrid.Columns.Add(col4);

            DataGridTextColumn col5 = new DataGridTextColumn();
            col5.Binding = new Binding("date");
            col5.Header = "Date";
            col5.Width = 100;
            ReviewByFriendDataGrid.Columns.Add(col5);
        }

        private void addUserIDListBox(NpgsqlDataReader R)
        {
            userIDListBox.Items.Add(R.GetString(0));
        }

        private void addTipGridRow(NpgsqlDataReader R)
        {
            ReviewByFriendDataGrid.Items.Add(new TipsList() { userName = R.GetString(0), businessName = R.GetString(1), city = R.GetString(2), text = R.GetString(3), date = R.GetDate(4).ToString() });
        }

        private void addFriendGridRow(NpgsqlDataReader R)
        {
            FriendListDataGrid.Items.Add(new FriendsList() { name = R.GetString(0), avgStars = R.GetDouble(1), totalLikes = R.GetInt32(2) });
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
                /*    Run user query    */
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

                /*    Run friend list query    */
                if (userIDListBox.SelectedIndex >= 0)
                {
                    string sqlStr = "SELECT name,average_stars,totallikes FROM users,friend WHERE users.user_id=friend.friend_id AND friend.user_id=(SELECT U1.user_id FROM users AS U1 WHERE U1.user_id='" + userIDListBox.SelectedItem.ToString() + "' ORDER BY name,average_stars,totallikes);";
                    executeQuery(sqlStr, addFriendGridRow);
                }

                /*    Run Tips list query    */
                if(userIDListBox.SelectedIndex >= 0)
                {
                    string sqlStr = "SELECT U.name, B.name, B.city, text, date(T.tipdate) FROM Business AS B, tip AS T, users AS U,(SELECT F.friend_id FROM users AS U1, friend AS F WHERE U1.user_id = '" + userIDListBox.SelectedItem.ToString() + "' AND U1.user_id = F.user_id) AS T1 WHERE T1.friend_id = T.user_id AND B.business_id = T.business_id AND T.user_id = U.user_id ORDER BY date(T.tipdate) DESC;";
                    executeQuery(sqlStr, addTipGridRow);
                }
            }
        }

        private void UsernameTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            userIDListBox.Items.Clear();
            string sqlStr = "SELECT distinct user_id,name FROM users WHERE name='" + UserNameTextBox.Text + "';";
            executeQuery(sqlStr, addUserIDListBox);
        }

        private void setLocationBtn_Click(object sender, RoutedEventArgs e)
        {
            /*    UNDER CONSTRUCTION    */
        }


        /*    Business Tab    */


        private string categorySelection = string.Empty;

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

        public class BusinessResults
        {
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

            public double calculateDistance(double latitude, double longitude)
            {
                return Math.Sqrt(Math.Pow(bLatitude - latitude, 2) + Math.Pow(bLongitude - longitude, 2));
            }
        }

        private void addBusinessResultGridColumns()
        {
            DataGridTextColumn col1 = new DataGridTextColumn();
            col1.Binding = new Binding("businessName");
            col1.Header = "Business Name";
            col1.Width = 100;
            businessResultDataGrid.Columns.Add(col1);

            DataGridTextColumn col2 = new DataGridTextColumn();
            col2.Binding = new Binding("address");
            col2.Header = "Address";
            col2.Width = 200;
            businessResultDataGrid.Columns.Add(col2);

            DataGridTextColumn col3 = new DataGridTextColumn();
            col3.Binding = new Binding("city");
            col3.Header = "City";
            col3.Width = 120;
            businessResultDataGrid.Columns.Add(col3);

            DataGridTextColumn col4 = new DataGridTextColumn();
            col4.Binding = new Binding("state");
            col4.Header = "State";
            col4.Width = 30;
            businessResultDataGrid.Columns.Add(col4);

            DataGridTextColumn col5 = new DataGridTextColumn();
            col5.Binding = new Binding("distance");
            col5.Header = "Distance";
            col5.Width = 50;
            businessResultDataGrid.Columns.Add(col5);

            DataGridTextColumn col6 = new DataGridTextColumn();
            col6.Binding = new Binding("stars");
            col6.Header = "Stars";
            col6.Width = 50;
            businessResultDataGrid.Columns.Add(col6);

            DataGridTextColumn col7 = new DataGridTextColumn();
            col7.Binding = new Binding("numberOfTips");
            col7.Header = "# of Tips";
            col7.Width = 50;
            businessResultDataGrid.Columns.Add(col7);

            DataGridTextColumn col8 = new DataGridTextColumn();
            col8.Binding = new Binding("totalCheckins");
            col8.Header = "Total Checkins";
            col8.Width = 50;
            businessResultDataGrid.Columns.Add(col8);
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

        private void addBusinessResultDataGrid(NpgsqlDataReader R)
        {
            businessResultDataGrid.Items.Add(new BusinessResults() { businessName = R.GetString(0), address = R.GetString(1), city = R.GetString(2), state = R.GetString(3), stars = R.GetDouble(4), numberOfTips = R.GetInt32(5), totalCheckins = R.GetInt32(6), bLatitude = R.GetDouble(7), bLongitude = R.GetDouble(8) });
        }

        private void addCategoriesBtn_Click(object sender, RoutedEventArgs e)
        {
            SelectListBox.Items.Add(categorySelection);
            CategoryListBox.Items.Remove(categorySelection);
        }

        private void removeCateoriesBtn_Click(object sender, RoutedEventArgs e)
        {
            SelectListBox.Items.Remove(categorySelection);
            CategoryListBox.Items.Add(categorySelection);
        }

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

        private void CategoryListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (CategoryListBox.SelectedIndex >= 0)
            {
                categorySelection = CategoryListBox.SelectedItem.ToString();
            }
        }

        private void SelectListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (SelectListBox.SelectedIndex >= 0)
            {
                categorySelection = SelectListBox.SelectedItem.ToString();
            }
        }

        private void searchBtn_Click(object sender, RoutedEventArgs e)
        {
            businessResultDataGrid.Items.Clear();

            StringBuilder sqlCategory = new StringBuilder();

            for (int index = 0; index < SelectListBox.Items.Count; index++)
            {
                sqlCategory.Append(" AND category='" + SelectListBox.Items[index] + "' ");
            }



            StringBuilder sqlStr = new StringBuilder("SELECT B.name, B.address, B.city, B.state, B.stars, B.review_count, B.numcheckins, B.latitude, B.longitude FROM business as B ");

            sqlStr.Append("JOIN categories AS C ON B.business_id=C.business_id ");
            sqlStr.Append("WHERE state='"+ stateDropBox.SelectedItem.ToString() +"' AND city='"+ cityDropBox.SelectedItem.ToString() +"' AND zipcode='"+ zipcodeDropBox.SelectedItem.ToString() +"' ");

            sqlStr.Append(sqlCategory.ToString());
            sqlStr.Append(";");

            executeQuery(sqlStr.ToString(), addBusinessResultDataGrid);
        }
    }
}
