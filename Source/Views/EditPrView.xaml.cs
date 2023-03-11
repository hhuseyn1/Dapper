using Microsoft.Extensions.Configuration;
using Source.Models;
using System.Data.SqlClient;
using System.IO;
using System.Text;
using System.Windows;

namespace Source.Views;
public partial class EditPrView : Window
{
    StringBuilder sb = null;
    string connectionString = null;
    SqlConnection conn;
    public Product Product { get; set; }
    public EditPrView(Product product)
    {
        InitializeComponent();
        DataContext = this;
        Configure();
        Product = product;
    }
    private void Configure()
    {
        var configuration = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory() + "../../../../")
                              .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true).Build();

        connectionString = configuration.GetConnectionString("ProductDb");
        conn = new SqlConnection(connectionString);
    }

    private void UpdateBtn_Click(object sender, RoutedEventArgs e)
    {

    }

    private void CancelBtn_Click(object sender, RoutedEventArgs e)
    {
        this.Close();
    }
}
