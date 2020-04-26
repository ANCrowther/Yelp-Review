namespace Team4_YelpProject.View
{
    using Npgsql;
    using System;
    using System.Text;
    using System.Windows;
    using System.Windows.Controls;
    using Team4_YelpProject.Model;

    /// <summary>
    /// Interaction logic for BusinessView.xaml
    /// </summary>
    public partial class BusinessView : UserControl
    {
        //private string categorySelection = string.Empty;
        //private Business tempBusiness = new Business();
        //private BusinessHours hours = new BusinessHours();

        public BusinessView()
        {
            InitializeComponent();
        }

        //private string buildConnectionString()
        //{
        //    return "Host = localhost; Username = postgres; Database = milestone2db; password = spiffy";
        //}

        //private void StateCB_Loaded(object sender, RoutedEventArgs e)
        //{
        //    loadState();
        //}

        //private void loadState()
        //{
        //    string sqlStr = "SELECT DISTINCT state FROM business ORDER BY state";
        //    executeQuery(sqlStr, addState);
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

        //private void addState(NpgsqlDataReader R)
        //{
        //    StateCB.Items.Add(R.GetString(0));
        //}

        //private void addCity(NpgsqlDataReader R)
        //{
        //    CityCB.Items.Add(R.GetString(0));
        //}

        //private void addZipcode(NpgsqlDataReader R)
        //{
        //    ZipcodeCB.Items.Add(R.GetInt32(0));
        //}

        //private void addCategoryItem(NpgsqlDataReader R)
        //{
        //    CategoryLB.Items.Add(R.GetString(0));
        //}

        //private void addBusinessGridRow(NpgsqlDataReader R)
        //{
        //    //businessDG.Items.Add(new Business() { BusinessName = R.GetString(0), Address = R.GetString(1), City = R.GetString(2), State = R.GetString(3), Stars = R.GetDouble(4), NumberOfTips = R.GetInt32(5), TotalCheckins = R.GetInt32(6), Latitude = R.GetDouble(7), Longitude = R.GetDouble(8), BusinessID = R.GetString(9) });
        //}

        //private void addBusinessHours(NpgsqlDataReader R)
        //{
        //    hours.BusinessID = R.GetString(0);
        //    hours.Day = R.GetString(1);
        //    hours.Open = R.GetValue(2).ToString();
        //    hours.Close = R.GetValue(3).ToString();
        //}

        //private void clearBusinessDetails()
        //{
        //    businessNameTB.Text = "";
        //    addressTB.Text = "";
        //    hoursTB.Text = "";
        //}

        //private void StateCB_SelectionChanged(object sender, SelectionChangedEventArgs e)
        //{
        //    CityCB.Items.Clear();
        //    ZipcodeCB.Items.Clear();

        //    if (StateCB.SelectedIndex >= 0)
        //    {
        //        string sqlStr = "SELECT DISTINCT city FROM business WHERE state ='" + StateCB.SelectedItem.ToString() + "' ORDER BY city;";
        //        executeQuery(sqlStr, addCity);
        //    }
        //}

        //private void CityCB_SelectionChanged(object sender, SelectionChangedEventArgs e)
        //{
        //    ZipcodeCB.Items.Clear();

        //    if (CityCB.SelectedIndex >= 0)
        //    {
        //        string sqlStr = "SELECT DISTINCT zipcode FROM business WHERE state ='" + StateCB.SelectedItem.ToString() + "' AND city='" + CityCB.SelectedItem.ToString() + "' ORDER BY zipcode;";
        //        executeQuery(sqlStr, addZipcode);
        //    }
        //}

        //private void ZipcodeCB_SelectionChanged(object sender, SelectionChangedEventArgs e)
        //{
        //    CategoryLB.Items.Clear();

        //    if (ZipcodeCB.SelectedIndex >= 0)
        //    {
        //        string sqlStr = "SELECT DISTINCT C.category FROM business AS B, categories AS C WHERE B.business_id=C.business_id AND state='" + StateCB.SelectedItem.ToString() + "' AND city='" + CityCB.SelectedItem.ToString() + "' AND zipcode='" + ZipcodeCB.SelectedItem.ToString() + "' ORDER BY category;";
        //        executeQuery(sqlStr, addCategoryItem);
        //    }
        //}

        //private void addBtn_Click(object sender, RoutedEventArgs e)
        //{
        //    selectedLB.Items.Add(categorySelection);
        //    CategoryLB.Items.Remove(categorySelection);
        //}

        //private void CategoryLB_SelectionChanged(object sender, SelectionChangedEventArgs e)
        //{
        //    if (CategoryLB.SelectedIndex >= 0)
        //    {
        //        categorySelection = CategoryLB.SelectedItem.ToString();
        //    }
        //}

        //private void selectedLB_SelectionChanged(object sender, SelectionChangedEventArgs e)
        //{
        //    if (selectedLB.SelectedIndex >= 0)
        //    {
        //        categorySelection = selectedLB.SelectedItem.ToString();
        //    }
        //}

        //private void RemoveBtn_Click(object sender, RoutedEventArgs e)
        //{
        //    selectedLB.Items.Remove(categorySelection);
        //    CategoryLB.Items.Add(categorySelection);
        //}

        //private void searchBtn_click(object sender, RoutedEventArgs e)
        //{
        //    clearBusinessDetails();
        //    businessDG.Items.Clear();
        //    updateBusinessResults();
        //    BusinessCountTB.Text = businessDG.Items.Count.ToString();
        //}

        //private void updateBusinessResults()
        //{
        //    StringBuilder sqlStr = new StringBuilder("SELECT DISTINCT B.name, B.address, B.city, B.state, B.stars, B.review_count, B.numcheckins, B.latitude, B.longitude, B.business_id FROM business as B ");
        //    StringBuilder sqlCategory = new StringBuilder();
        //    StringBuilder sqlCategoryBackEnd = new StringBuilder(" JOIN categories AS C ON B.business_id=C.business_id ");
        //    StringBuilder sqlMealFilter = new StringBuilder(", (SELECT * FROM attributes WHERE attr_name=ANY('{");
        //    StringBuilder sqlMealSelection = new StringBuilder();

        //    /*    Appends selected Category choices to Query    */
        //    for (int index = 0; index < selectedLB.Items.Count; index++)
        //    {
        //        sqlCategory.Append(" AND category='" + selectedLB.Items[index] + "' ");
        //    }



        //    /*    Append Category JOIN statement    */
        //    if (sqlCategory.Length > 0)
        //    {
        //        sqlStr.Append(sqlCategoryBackEnd);
        //    }

        //    sqlStr.Append("WHERE state='" + StateCB.SelectedItem.ToString() + "' AND city='" + CityCB.SelectedItem.ToString() + "' AND zipcode='" + ZipcodeCB.SelectedItem.ToString() + "' ");

        //    /*    Sew the queries together     */
        //    if (sqlCategory.Length > 0)
        //    {
        //        sqlStr.Append(sqlCategory.ToString());
        //    }

        //    sqlStr.Append(";");
        //    Console.WriteLine(sqlStr.ToString());
        //    executeQuery(sqlStr.ToString(), addBusinessGridRow);
        //}

        //private void businessDG_SelectionChanged(object sender, SelectionChangedEventArgs e)
        //{
        //    clearBusinessDetails();

        //    if (businessDG.SelectedIndex >= 0)
        //    {
        //        this.tempBusiness = (Business)businessDG.SelectedItem;

        //        businessNameTB.Text = this.tempBusiness.BusinessName;
        //        string str = this.tempBusiness.Address + ", " + this.tempBusiness.City + ", " + this.tempBusiness.State;
        //        addressTB.Text = (str);

        //        DayOfWeek wk = DateTime.Today.DayOfWeek;

        //        string sqlStr = "SELECT H.business_id, H.day_of_week, H.open, H.close FROM business AS B, hours AS H WHERE B.business_id=H.business_id AND B.business_id='" + tempBusiness.BusinessID + "' AND H.day_of_week='" + wk + "';";

        //        Console.WriteLine(sqlStr);
        //        executeQuery(sqlStr, addBusinessHours);

        //        //string sqlStr = "SELECT ";
        //        string day = hours.Day;
        //        string open = hours.Open;
        //        string close = hours.Close;
        //        string dayOfWeek = "";
        //        if (open != null)
        //        {
        //            dayOfWeek = day + ": Opens: " + open + "  Closes: " + close;

        //        }
        //        else
        //        {
        //            dayOfWeek = day + ": Closed today.";
        //        }

        //        hoursTB.Text = dayOfWeek;
        //    }
        //}

        //private void showTipsButton_Click(object sender, RoutedEventArgs e)
        //{
        //    if (businessDG.SelectedIndex >= 0)
        //    {
        //        Business B = businessDG.Items[businessDG.SelectedIndex] as Business;
        //        if ((B.BusinessID != null) && (B.BusinessID.ToString().CompareTo("") != 0))
        //        {
        //            BusinessTipsView tipsWindow = new BusinessTipsView(B.BusinessID.ToString());
        //            tipsWindow.Show();
        //        }
        //    }
        //}
    }
}
