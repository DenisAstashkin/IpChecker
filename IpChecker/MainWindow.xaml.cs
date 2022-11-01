using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using Newtonsoft.Json;
using RestSharp;
using System.IO;


namespace IpChecker
{    
    public partial class MainWindow : Window
    {        
        public MainWindow()
        {
            InitializeComponent();
        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            var client = new RestClient($"http://ip-api.com/json/{IP.Text}?lang=ru");
            var request = new RestRequest
            {
                Method = Method.Get
            };
            var response = await client.ExecuteGetAsync(request);
            var dic = JsonConvert.DeserializeObject<Dictionary<string, string>>(response.Content);
            Info.Children.Clear();
            for (int i = 0; i < NameInfo.Children.Count; i++)
            {
                var ChildLable = NameInfo.Children[i] as Label;
                var key = new StringBuilder(Convert.ToString(ChildLable.Content));
                key = key.Replace(": ", string.Empty);
                key[0] = char.ToLower(key[0]);
                try
                {
                    Info.Children.Add(new Label()
                    {
                        Content = dic[key.ToString()],
                        Foreground = new SolidColorBrush(Colors.Red),
                        FontWeight = FontWeights.Bold,
                        FontSize = 17
                    });
                }
                catch (Exception ex)
                {
                    Info.Children.Add(new Label()
                    {
                        Content = string.Empty
                    });
                    continue;
                }
            }
            if (File.Exists($"{AppDomain.CurrentDomain.BaseDirectory}SQLite.Interop.dll"))
            {
                var map = new Map();
                map.lat = Convert.ToDouble(((Info.Children[8] as Label)?.Content as string)?.Replace('.', ','));
                map.lon = Convert.ToDouble(((Info.Children[9] as Label)?.Content as string)?.Replace('.', ','));
                map.Show();
                return;
            }
            MessageBox.Show($"Чтобы узнать геолокацию установите SQLite.Interop.dll в папку {AppDomain.CurrentDomain.BaseDirectory}");
        }
    }
}