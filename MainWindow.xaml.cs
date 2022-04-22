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

//So I can do network requests duh
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using Newtonsoft.Json;

namespace Trophy
{
    public partial class MainWindow : Window
    { 
        HttpClient client;
        string id = string.Empty;
        string title = string.Empty;

        public MainWindow()
        {
            client = new HttpClient();
            InitializeComponent();
        }

        private async Task<object> Request(string uri)
        {
            using var httpResponse = await client.GetAsync(uri, HttpCompletionOption.ResponseHeadersRead);

            httpResponse.EnsureSuccessStatusCode();

            if (httpResponse.Content is object && httpResponse.Content.Headers.ContentType.MediaType == "application/json")
            {
                var contentStream = await httpResponse.Content.ReadAsStreamAsync();

                using var streamReader = new StreamReader(contentStream);
                using var jsonReader = new JsonTextReader(streamReader);

                JsonSerializer serializer = new JsonSerializer();

                try
                {
                    return serializer.Deserialize(jsonReader);
                }
                catch (JsonReaderException)
                {
                    Console.WriteLine("Invalid JSON.");
                }
            }
            else
            {
                Console.WriteLine("HTTP Response was invalid and cannot be deserialised.");
            }

            return null;
        }

        private void ShowTitleInput()
        {
            TitleInput ti = new TitleInput("Astro's Playroom");
            ti.ShowDialog();
            title = ti.Answer;
        }

        public void account_id_clear_btn_clk(object sender, EventArgs e)
        {
            account_id_field.Text = string.Empty;
        }

        public void account_id_field_changed(object sender, TextChangedEventArgs e)
        {
            id = account_id_field.Text;
        }
    }
}
