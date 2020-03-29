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
            public string state { get; set; }
            public string city { get; set; }
            public string bid { get; set; }
        }

        public MainWindow()
        {
            InitializeComponent();
        }

        private string buildConnectionString()
        {
            return "Host = localhost; Username = postgres; Database = milestone2db; password = spiffy";
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
                LatTextBox.Text = R.GetDouble(8);
                LongTextBox.Text = R.GetDouble(9);
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
    }
}
