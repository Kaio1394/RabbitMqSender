using Microsoft.VisualBasic;
using RabbitMqSender.core;
using RabbitMqSender.model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

namespace RabbitMqSender.components.form
{
    /// <summary>
    /// Interaction logic for NewFormRabbit.xaml
    /// </summary>
    public partial class NewFormRabbit : UserControl
    {
        public ObservableCollection<string> ItemsCollectionQueue { get; set; }

        private RabbitConfig _rabbitConfig;
        private RabbitConnection _rabbitConnection;
        public NewFormRabbit()
        {
            InitializeComponent();
        }

        private async Task LoadQueues()
        {
            ItemsCollectionQueue = new ObservableCollection<string>();
            _rabbitConfig = new RabbitConfig()
            {
                HostName = Properties.Settings.Default.host,
                Password = Properties.Settings.Default.password,
                UserName = Properties.Settings.Default.user,
                VirtualHost = Properties.Settings.Default.virtualHost,
                Port = Convert.ToInt32(Properties.Settings.Default.Port),
            };
            _rabbitConnection = new RabbitConnection(_rabbitConfig);
            var listQueues = await _rabbitConnection.GetQueues(_rabbitConfig);
            
            foreach (var queue in listQueues)
            {
                ItemsCollectionQueue.Add(queue);
            }
        }

        private async void ButtonLoad_Click(object sender, RoutedEventArgs e)
        {
            progressBar.Visibility = Visibility.Visible;
            progressBar.IsIndeterminate = true;

            await LoadQueues();

            cbQueues.ItemsSource = ItemsCollectionQueue;

            progressBar.Visibility = Visibility.Collapsed;
        }
    }
}
