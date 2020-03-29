using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Npgsql;

namespace Team4_YelpProject
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public class Business
        {
            public string name { get; set; }
            public string totalLikes { get; set; }
            public string avgStars { get; set; }
            public string yelpingSince { get; set; }
        }

        public MainWindow()
        {
            InitializeComponent();
            addFriendsGridColumns();
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

        void setUserData(NpgsqlDataReader R)
        {
            while(R.Read())
            {
                userIDListBox.Items.Add(R.GetString(0));
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
            FriendListDataGrid.Items.Clear();

            using (var conn = new NpgsqlConnection(buildConnectionString()))
            {
                conn.Open();
                string userID = userIDListBox.SelectedItem.ToString();
                using (var cmd = new NpgsqlCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandText = "SELECT distinct user_id,name,average_stars,fans,date(yelping_since),funny,cool,useful,user_latitude,user_longitude FROM users WHERE user_id='" + userID + "';";

                    setUserData(cmd.ExecuteReader());
                }

                if (userIDListBox.SelectedIndex >= 0)
                {
                    string sqlStr = "SELECT name,totalLikes,average_stars,yelping_since FROM user,friend WHERE users.user_id=friend.friend_id AND friend.user_id=(SELECT U.user_id FROM users AS UWHERE U.user_id='" + userID + "')";
                    executeQuery(sqlStr, addFriendsGridRow);
                }

                conn.Close();
            }


        }

        private void addFriendsGridRow(NpgsqlDataReader R)
        {
            FriendListDataGrid.Items.Add(new Business() { name = R.GetString(0), totalLikes = R.GetString(1), avgStars = R.GetString(2), yelpingSince = R.GetString(3)});
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

        private void FriendListDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
