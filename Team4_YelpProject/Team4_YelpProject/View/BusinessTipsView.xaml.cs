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
        private string bid = string.Empty;
        public BusinessTipsView(string bid)
        {
            InitializeComponent();
            this.bid = String.Copy(bid);
            addTipsGridColumns();
            loadBusinessDetails();
        }

        private void addTipsGridColumns()
        {
            DataGridTextColumn col1 = new DataGridTextColumn();
            col1.Binding = new Binding("date");
            col1.Header = "Date";
            TipList.Columns.Add(col1);

            DataGridTextColumn col2 = new DataGridTextColumn();
            col2.Binding = new Binding("userName");
            col2.Header = "User Name";
            TipList.Columns.Add(col2);

            DataGridTextColumn col3 = new DataGridTextColumn();
            col3.Binding = new Binding("likes");
            col3.Header = "Likes";
            TipList.Columns.Add(col3);

            DataGridTextColumn col4 = new DataGridTextColumn();
            col4.Binding = new Binding("text");
            col4.Header = "Text";
            TipList.Columns.Add(col4);
        }

        private string buildConnectionString()
        {
            return "Host = localhost; Username = postgres; Database = milestone2db; password = spiffy";
        }

        private void addTipData(NpgsqlDataReader R)
        {
            TipList.Items.Add(new TipList { Date = R.GetDate(0).ToString(), UserName = R.GetString(1), Likes = R.GetInt32(2), Text = R.GetString(3)});
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

        private void loadBusinessDetails()
        {
            string sqlStr = "SELECT date(T.tipdate), U.name, T.likes, T.text FROM users AS U, business AS B, tip AS T WHERE T.user_id=U.user_id AND T.business_id=B.business_id AND B.business_id='" + this.bid + "'";
            Console.WriteLine(sqlStr);
            executeQuery(sqlStr, addTipData);
        }
    }
}
