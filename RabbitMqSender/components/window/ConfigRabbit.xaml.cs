﻿using RabbitMqSender.core;
using RabbitMqSender.model;
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
        private RabbitConfig _rabbitConfig;
        private RabbitConnection _rabbitConnection;
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
            tbVirtualHost.Text = Properties.Settings.Default.virtualHost;
            tbPort.Text = $"{Properties.Settings.Default.Port}";
        }
        private void ButtonSaveConfigRabbit_Click(object sender, RoutedEventArgs e)
        {
            Properties.Settings.Default.host = tbHost.Text;
            Properties.Settings.Default.user = tbUser.Text;
            Properties.Settings.Default.password = tbUPass.Text;
            Properties.Settings.Default.virtualHost = tbVirtualHost.Text;
            Properties.Settings.Default.Port = Convert.ToInt32(tbPort.Text);
            Properties.Settings.Default.Save();
            MessageBox.Show("Settings Save with successfully!", "Information", MessageBoxButton.OK);
        }
        private bool ValidVaraibles()
        {
            return !string.IsNullOrEmpty(Properties.Settings.Default.host) &&
                !string.IsNullOrEmpty(Properties.Settings.Default.user) &&
                !string.IsNullOrEmpty(Properties.Settings.Default.password) &&
                !string.IsNullOrEmpty(Properties.Settings.Default.virtualHost);
        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            if (!ValidVaraibles())
            {
                MessageBox.Show("Some Field is empty!", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            _rabbitConfig = new RabbitConfig()
            {
                HostName = Properties.Settings.Default.host,
                Password = Properties.Settings.Default.password,
                UserName = Properties.Settings.Default.user,
                VirtualHost = Properties.Settings.Default.virtualHost,
                Port = Properties.Settings.Default.Port
            };

            loadingBar.Visibility = Visibility.Visible;
            tbkStatusTest.Text = "Checking connection...";
            tbkStatusTest.Foreground = new SolidColorBrush(Colors.Gray);

            try
            {
                _rabbitConnection = new RabbitConnection(_rabbitConfig);
                await Task.Run(() =>
                {
                    Task.Delay(2000).Wait();
                });

                if (!_rabbitConnection.ChannelIsOpen())
                {
                    tbkStatusTest.Text = "Error connection!";
                    tbkStatusTest.Foreground = new SolidColorBrush(Colors.Red);
                }
                else
                {
                    tbkStatusTest.Text = "Successful connection!";
                    tbkStatusTest.Foreground = new SolidColorBrush(Colors.Green);
                }
            }
            catch (Exception ex)
            {
                tbkStatusTest.Text = $"Error: {ex.Message}";
                tbkStatusTest.Foreground = new SolidColorBrush(Colors.Red);
            }
            finally
            {
                loadingBar.Visibility = Visibility.Collapsed;
            }
        }

    }
}
