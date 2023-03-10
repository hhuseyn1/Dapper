using Microsoft.Extensions.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Windows;

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
}
