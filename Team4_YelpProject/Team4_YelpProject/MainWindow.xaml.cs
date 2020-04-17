using Npgsql;
using System;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using Team4_YelpProject.Model;
using Team4_YelpProject.View;

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
        YelpUser currentUser = new YelpUser();

        private string buildConnectionString()
        {
            return "Host = localhost; Username = postgres; Database = milestone2db; password = spiffy";
        }

        private void addUserData()
        {
            UserNameBox.Text = currentUser.Name;
            UserStarsResult.Content = currentUser.Average_stars;
            UserFansResult.Content = (int)currentUser.Fans;
            CoolCount.Content = (int)currentUser.Cool;
            FunnyCount.Content = currentUser.Funny;
            UsefulCount.Content = currentUser.Useful;
            UserYelpingSinceResult.Content = currentUser.Yelping_since;
            LatTextBox.Text = currentUser.User_latitude.ToString();
            LongTextBox.Text = currentUser.User_longitude.ToString();
        }

        private void addFriendsGridColumns()
        {
            DataGridTextColumn col1 = new DataGridTextColumn();
            col1.Binding = new Binding("name");
            col1.Header = "Name";
            FriendListDataGrid.Columns.Add(col1);

            DataGridTextColumn col2 = new DataGridTextColumn();
            col2.Binding = new Binding("totalLikes");
            col2.Header = "Total Likes";
            FriendListDataGrid.Columns.Add(col2);

            DataGridTextColumn col3 = new DataGridTextColumn();
            col3.Binding = new Binding("avgStars");
            col3.Header = "Avg Stars";
            FriendListDataGrid.Columns.Add(col3);

            DataGridTextColumn col4 = new DataGridTextColumn();
            col4.Binding = new Binding("yelpingSince");
            col4.Header = "Yelping Since";
            FriendListDataGrid.Columns.Add(col4);
        }

        private void addLatestTipsGridColumns()
        {
            DataGridTextColumn col1 = new DataGridTextColumn();
            col1.Binding = new Binding("userName");
            col1.Header = "Customer Name";
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

        private void addUser(NpgsqlDataReader R)
        {
            //User_id = R.GetString(0);
            currentUser.Name = R.GetString(1);
            currentUser.Average_stars = R.GetDouble(2);
            currentUser.Fans = R.GetInt32(3);
            currentUser.Cool = R.GetInt32(4);
            currentUser.Funny = R.GetInt32(5);
            currentUser.Useful = R.GetInt32(6);
            currentUser.Yelping_since = R.GetDate(7).ToString();
            currentUser.User_latitude = R.GetDouble(8);
            currentUser.User_longitude = R.GetDouble(9);

            addUserData();
        }

        private void addTipGridRow(NpgsqlDataReader R)
        {
            ReviewByFriendDataGrid.Items.Add(new TipList() { UserName = R.GetString(0), BusinessName = R.GetString(1), City = R.GetString(2), Text = R.GetString(3), Date = R.GetDate(4).ToString() });
        }

        private void addFriendGridRow(NpgsqlDataReader R)
        {
            FriendListDataGrid.Items.Add(new FriendList() { name = R.GetString(0), avgStars = R.GetDouble(1), totalLikes = R.GetInt32(2), yelpingSince = R.GetDate(3).ToString() });
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
                if (userIDListBox.SelectedIndex >= 0)
                {
                    string sqlStr = "SELECT distinct user_id,name,average_stars,fans,funny,cool,useful,date(yelping_since),user_latitude,user_longitude FROM users WHERE user_id='" + userIDListBox.SelectedItem.ToString() + "';";
                    executeQuery(sqlStr, addUser);
                }

                /*    Run friend list query    */
                if (userIDListBox.SelectedIndex >= 0)
                {
                    string sqlStr = "SELECT name,average_stars,totallikes, date(yelping_since) FROM users,friend WHERE users.user_id=friend.friend_id AND friend.user_id=(SELECT U1.user_id FROM users AS U1 WHERE U1.user_id='" + userIDListBox.SelectedItem.ToString() + "' ORDER BY name,average_stars,totallikes);";
                    executeQuery(sqlStr, addFriendGridRow);
                }

                /*    Run Tips list query    */
                if (userIDListBox.SelectedIndex >= 0)
                {
                    string sqlStr = "SELECT U.name, B.name, B.city, text, date(T.tipdate) FROM Business AS B, tip AS T, users AS U,(SELECT F.friend_id FROM users AS U1, friend AS F WHERE U1.user_id = '" + userIDListBox.SelectedItem.ToString() + "' AND U1.user_id = F.user_id) AS T1 WHERE T1.friend_id = T.user_id AND B.business_id = T.business_id AND T.user_id = U.user_id ORDER BY date(T.tipdate) DESC;";
                    executeQuery(sqlStr, addTipGridRow);
                }
            }
        }

        private void UserNameTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            userIDListBox.Items.Clear();
            string sqlStr = "SELECT distinct user_id,name FROM users WHERE name='" + UserNameTextBox.Text + "';";
            executeQuery(sqlStr, addUserIDListBox);
        }

        private void setLocationBtn_Click(object sender, RoutedEventArgs e)
        {
            if (!(currentUser.User_latitude == Convert.ToDouble(LatTextBox.Text) && currentUser.User_longitude == Convert.ToDouble(LongTextBox.Text)))
            {
                /*  Updates the actual databse  */
                updateQuery();

                /*  Updates the UI  */
                string sqlStr = "SELECT distinct user_id,name,average_stars,fans,funny,cool,useful,date(yelping_since),user_latitude,user_longitude FROM users WHERE user_id='" + currentUser.User_id + "';";
                executeQuery(sqlStr, addUser);
            }
        }

        private void updateQuery()
        {
            string sqlStr = "UPDATE Users SET user_latitude='" + Convert.ToDouble(LatTextBox.Text) + "', user_longitude='" + Convert.ToDouble(LongTextBox.Text) + "' WHERE user_id='" + currentUser.User_id + "';";

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


        /*    Business Tab    */

        private string categorySelection = string.Empty;
        private BusinessHours hours = new BusinessHours();

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
            col5.Binding = new Binding("stars");
            col5.Header = "Stars";
            col5.Width = 50;
            businessResultDataGrid.Columns.Add(col5);

            DataGridTextColumn col6 = new DataGridTextColumn();
            col6.Binding = new Binding("numberOfTips");
            col6.Header = "# of Tips";
            col6.Width = 50;
            businessResultDataGrid.Columns.Add(col6);

            DataGridTextColumn col7 = new DataGridTextColumn();
            col7.Binding = new Binding("totalCheckins");
            col7.Header = "Total Checkins";
            col7.Width = 50;
            businessResultDataGrid.Columns.Add(col7);

            DataGridTextColumn col8 = new DataGridTextColumn();
            col8.Binding = new Binding("businessID");
            col8.Header = "Business ID";
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
            businessResultDataGrid.Items.Add(new BusinessResults() { BusinessName = R.GetString(0), Address = R.GetString(1), City = R.GetString(2), State = R.GetString(3), Stars = R.GetDouble(4), NumberOfTips = R.GetInt32(5), TotalCheckins = R.GetInt32(6), BLatitude = R.GetDouble(7), BLongitude = R.GetDouble(8), BusinessID = R.GetString(9) });
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

        private bool selected = false;

        private void searchBtn_Click(object sender, RoutedEventArgs e)
        {
            clearBusinessDetails();
            businessResultDataGrid.Items.Clear();
            updateBusinessResults();
            selected = true;
        }

        private void updateBusinessResults()
        {
            

            StringBuilder sqlStr = new StringBuilder("SELECT DISTINCT B.name, B.address, B.city, B.state, B.stars, B.review_count, B.numcheckins, B.latitude, B.longitude, B.business_id FROM business as B ");
            StringBuilder sqlCategory = new StringBuilder();
            StringBuilder sqlCategoryBackEnd = new StringBuilder(" JOIN categories AS C ON B.business_id=C.business_id ");
            StringBuilder sqlMealFilter = new StringBuilder(", (SELECT * FROM attributes WHERE attr_name=ANY('{");
            StringBuilder sqlMealSelection = new StringBuilder();
            //bool mealFilter = false;

            /*    Appends selected Category choices to Query    */
            for (int index = 0; index < SelectListBox.Items.Count; index++)
            {
                sqlCategory.Append(" AND category='" + SelectListBox.Items[index] + "' ");
            }

            /*    Appends filter selection to Query    */
            //if (breakfastCB.IsChecked == true)
            //{
            //    if(sqlMealSelection.Length > 0)
            //    {
            //        sqlMealSelection.Append(", ");
            //    }
            //    sqlMealSelection.Append("breakfast ");
            //    mealFilter = true;
            //}
            //if (lunchCB.IsChecked == true)
            //{
            //    if (sqlMealSelection.Length > 0)
            //    {
            //        sqlMealSelection.Append(", ");
            //    }
            //    sqlMealSelection.Append("lunch ");
            //    mealFilter = true;
            //}
            //if (brunchCB.IsChecked == true)
            //{
            //    if (sqlMealSelection.Length > 0)
            //    {
            //        sqlMealSelection.Append(", ");
            //    }
            //    sqlMealSelection.Append("brunch ");
            //    mealFilter = true;
            //}
            //if (dinnerCB.IsChecked == true)
            //{
            //    if (sqlMealSelection.Length > 0)
            //    {
            //        sqlMealSelection.Append(", ");
            //    }
            //    sqlMealSelection.Append("dinner ");
            //    mealFilter = true;
            //}
            //if (dessertCB.IsChecked == true)
            //{
            //    if (sqlMealSelection.Length > 0)
            //    {
            //        sqlMealSelection.Append(", ");
            //    }
            //    sqlMealSelection.Append("dessert ");
            //    mealFilter = true;
            //}
            //if (lateNightCB.IsChecked == true)
            //{
            //    if (sqlMealSelection.Length > 0)
            //    {
            //        sqlMealSelection.Append(", ");
            //    }
            //    sqlMealSelection.Append("lateNight ");
            //    mealFilter = true;
            //}
            //if (mealFilter == true)
            //{
            //    sqlMealFilter.Append(sqlMealSelection.ToString() + "}')AND value='True') AS A ");
            //    //sqlStr.Append(sqlMealFilter.ToString());
            //}

            //if (mealFilter == true)
            //{
            //    sqlStr.Append(sqlMealFilter.ToString());
            //}

            if (sqlCategory.Length > 0)
            {
                sqlStr.Append(sqlCategoryBackEnd);
            }

            sqlStr.Append("WHERE state='" + stateDropBox.SelectedItem.ToString() + "' AND city='" + cityDropBox.SelectedItem.ToString() + "' AND zipcode='" + zipcodeDropBox.SelectedItem.ToString() + "' ");

            /*    Sew the queries together     */
            if (sqlCategory.Length > 0)
            {
                sqlStr.Append(sqlCategory.ToString());
            }

            //if (mealFilter == true)
            //{
            //    sqlStr.Append("AND B.business_id=A.business_id");
            //}

            sqlStr.Append(";");
            Console.WriteLine(sqlStr.ToString());
            executeQuery(sqlStr.ToString(), addBusinessResultDataGrid);
        }

        BusinessResults tempBusiness = new BusinessResults();

        private void clearBusinessDetails()
        {
            
            businessNameTextBox.Text = "";
            addressTextBox.Text = "";
            hoursTextBox.Text = "";
        }

        private void businessResultDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            clearBusinessDetails();

            if (businessResultDataGrid.SelectedIndex >= 0)
            {
                this.tempBusiness = (BusinessResults)businessResultDataGrid.SelectedItem;

                businessNameTextBox.Text = this.tempBusiness.BusinessName;
                string str = this.tempBusiness.Address + ", " + this.tempBusiness.City + ", " + this.tempBusiness.State;
                addressTextBox.Text = (str);

                DayOfWeek wk = DateTime.Today.DayOfWeek;

                string sqlStr = "SELECT H.business_id, H.day_of_week, H.open, H.close FROM business AS B, hours AS H WHERE B.business_id=H.business_id AND B.business_id='" + tempBusiness.BusinessID + "' AND H.day_of_week='" + wk + "';";

                Console.WriteLine(sqlStr);
                executeQuery(sqlStr, addBusinessHours);

                //string sqlStr = "SELECT ";
                string day = hours.Day;
                string open = hours.Open;
                string close = hours.Close;
                string dayOfWeek = "";
                if (open != null)
                {
                    dayOfWeek = day + ": Opens: " + open + "  Closes: " + close;

                }
                else
                {
                    dayOfWeek = day + ": Closed today.";
                }
                Console.WriteLine(day);


                hoursTextBox.Text = dayOfWeek;
            }
        }

        private void addBusinessHours(NpgsqlDataReader R)
        {

            hours.Business_id = R.GetString(0);
            hours.Day = R.GetString(1);
            hours.Open = R.GetValue(2).ToString();
            hours.Close = R.GetValue(3).ToString();
        }

        private void showTipsButton_Click(object sender, RoutedEventArgs e)
        {
            if (businessResultDataGrid.SelectedIndex >= 0)
            {
                BusinessResults B = businessResultDataGrid.Items[businessResultDataGrid.SelectedIndex] as BusinessResults;
                if((B.BusinessID != null) && (B.BusinessID.ToString().CompareTo("") != 0))
                {
                    BusinessTipsView tipsWindow = new BusinessTipsView(B.BusinessID.ToString());
                    tipsWindow.Show();
                }
            }
        }
    }
}
