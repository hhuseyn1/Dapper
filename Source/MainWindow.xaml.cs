using Dapper;
using Microsoft.Extensions.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Net.Http.Headers;
using System.Windows;
using System.Windows.Controls;
using Z.BulkOperations;
using Z.Dapper.Plus;

namespace Source;

public partial class MainWindow : Window
{
    SqlConnection conn = null;
    SqlDataReader? reader = null;
    SqlCommand command = null;
    DataTable table = null;
    string connectionString = null;

    public MainWindow()
    {
        InitializeComponent();
        Configure();
        DataContext = this;
    }

    private void Configure()
    {
        var configuration = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory() + "../../../../")
                              .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true).Build();

        connectionString = configuration.GetConnectionString("ProductDb");
        conn = new SqlConnection(connectionString);
    }

    private void CreateDbBtn_Click(object sender, RoutedEventArgs e)
    {
        var reader = conn.ExecuteReader("SELECT * FROM Product");
        table = new DataTable();
        table.Load(reader);
        DataGrid.ItemsSource = table.AsDataView();
    }

    private void SearchBtn_Click(object sender, RoutedEventArgs e)
    {
        string dcommand;
        if (string.IsNullOrWhiteSpace(Searchtxtbox.Text) || string.IsNullOrEmpty(Searchtxtbox.Text))
        {
            dcommand = $"SELECT * FROM Product";
            command = new SqlCommand(dcommand, conn);
        }

        else
            dcommand = $"SELECT * FROM Product WHERE LOWER(Name) LIKE '%'+LOWER(@a)+'%'";

        var reader = conn.ExecuteReader(dcommand, new {a=Searchtxtbox.Text});
        table = new DataTable();
        table.Load(reader);
        DataGrid.ItemsSource = table.AsDataView();

        Searchtxtbox.Text = null;
    }

  

    private void AddprBtn_Click(object sender, RoutedEventArgs e)
    {

    }

    private void EditprBtn_Click(object sender, RoutedEventArgs e)
    {

    }

    private void RemoveprBtn_Click(object sender, RoutedEventArgs e)
    {

    }

    private void ClearListBtn_Click(object sender, RoutedEventArgs e)
    {
        conn.Execute("TRUNCATE TABLE Product");
        MessageBox.Show("Database has successfully cleared");
    }
}
