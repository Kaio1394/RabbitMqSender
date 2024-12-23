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
using System.Windows.Shapes;

namespace RabbitMqSender.components.window
{
    /// <summary>
    /// Interaction logic for ConfigRabbit.xaml
    /// </summary>
    public partial class ConfigRabbit : Window
    {
        public ConfigRabbit()
        {
            InitializeComponent();
            LoadSettings();
        }
        private void LoadSettings()
        {
            tbHost.Text = Properties.Settings.Default.host;
            tbUser.Text = Properties.Settings.Default.user;
            tbUPass.Text = Properties.Settings.Default.password;
        }
        private void ButtonSaveConfigRabbit_Click(object sender, RoutedEventArgs e)
        {
            Properties.Settings.Default.host = tbHost.Text;
            Properties.Settings.Default.user = tbUser.Text;
            Properties.Settings.Default.password = tbUPass.Text;
            Properties.Settings.Default.Save();
            this.Close();
        }
    }
}
