namespace Team4_YelpProject.View
{
    using Npgsql;
    using System;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Data;

    /// <summary>
    /// Interaction logic for BusinessTipsView.xaml
    /// </summary>
    public partial class BusinessTipsView : Window
    {
        //private string bid = string.Empty;
        //Tips tip = new Tips();

        //public BusinessTipsView(string bid)
        //{
        //    InitializeComponent();
        //    this.bid = String.Copy(bid);
        //    loadBusinessDetails();
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

        //private void updateQuery()
        //{
        //    this.tip = (Tips)tipList.SelectedItem;

        //    string sqlStr = "UPDATE tip SET likes=likes+1 WHERE date(tipdate)='" + this.tip.date + "' AND user_id='" + this.tip.userID + "' AND business_id='" + this.tip.businessID + "';";

        //    using (var connection = new NpgsqlConnection(buildConnectionString()))
        //    {
        //        connection.Open();
        //        using (var cmd = new NpgsqlCommand())
        //        {
        //            cmd.Connection = connection;
        //            cmd.CommandText = sqlStr;
        //            try
        //            {
        //                cmd.ExecuteNonQuery();
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

        //private void loadBusinessDetails()
        //{
        //    string sqlStr = "SELECT date(T.tipdate), U.name, T.likes, T.text FROM users AS U, business AS B, tip AS T WHERE T.user_id=U.user_id AND T.business_id=B.business_id AND B.business_id='" + this.bid + "'";
        //    Console.WriteLine(sqlStr);
        //    executeQuery(sqlStr, addTipData);
        //}

        //private void addTipData(NpgsqlDataReader R)
        //{
        //    tipList.Items.Add(new Tips { date = R.GetDate(0).ToString(), userName = R.GetString(1), likes = R.GetInt32(2), text = R.GetString(3) });
        //}

        //private void likeBtn_Click(object sender, RoutedEventArgs e)
        //{
        //    if (tipList.SelectedIndex >= 0)
        //    {
        //        tipList.Items.Clear();
        //        updateQuery();

        //        string sqlStr = "SELECT date(T.tipdate), U.name, T.likes, T.text FROM users AS U, business AS B, tip AS T WHERE T.user_id=U.user_id AND T.business_id=B.business_id AND B.business_id='" + this.bid + "'";
        //        executeQuery(sqlStr, addTipData);
        //    }
        //}

        //private void addTipBtn_click(object sender, RoutedEventArgs e)
        //{
        //}
    }
}
