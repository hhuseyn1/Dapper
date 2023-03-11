using Microsoft.Extensions.Configuration;
using Source.Models;
using System;
using System.Data.SqlClient;
using System.Data;
using System.Text;
using System.Windows;
using Dapper;
using System.IO;

namespace Source.Views;

public partial class AddPrView : Window
{
    StringBuilder sb= null;
    string connectionString = null;
    SqlConnection conn;
    public Product Product { get; set; }
    public AddPrView()
    {
        InitializeComponent();
        DataContext = this;
        Product = new();
        Configure();
        
    }
    private void Configure()
    {
        var configuration = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory() + "../../../../")
                              .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true).Build();

        connectionString = configuration.GetConnectionString("ProductDb");
        conn = new SqlConnection(connectionString);
    }

    private void CancelBtn_Click(object sender, RoutedEventArgs e)
    {
        clearAll();
        this.Close();
    }

    private void AddBtn_Click(object sender, RoutedEventArgs e)
    {
        if(string.IsNullOrEmpty(Nametxtbox.Text) || string.IsNullOrWhiteSpace(Nametxtbox.Text))
            sb.Append("Name is null\n");
        if (string.IsNullOrEmpty(Pricetxtbox.Text) || string.IsNullOrWhiteSpace(Pricetxtbox.Text))
            sb.Append("Price is null\n");
        if (string.IsNullOrEmpty(Quantitytxtbox.Text) || string.IsNullOrWhiteSpace(Quantitytxtbox.Text))
            sb.Append("Quantity is null\n");
        if (string.IsNullOrEmpty(Ratingtxtbox.Text) || string.IsNullOrWhiteSpace(Ratingtxtbox.Text))
            sb.Append("Rating is null\n");
        if (sb is not null)
            MessageBox.Show(sb.ToString());
        try
        {
            var reader = conn.ExecuteReader("SELECT * FROM Product");
            var table = new DataTable();
            table.Load(reader);
            Product.Id = table.Rows.Count;
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
        finally
        {
            clearAll();
        }
    }
    private void clearAll()
    {
        Nametxtbox.Text = string.Empty;
        Pricetxtbox.Text = 0.ToString();
        Quantitytxtbox.Text = 0.ToString();
        Ratingtxtbox.Text = 0.ToString();
    }
}
