using RabbitMqSender.components.window;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace RabbitMqSender
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private static ConfigRabbit? _winconfigRabbit = null;
        public MainWindow()
        {
            InitializeComponent();
        }

        private void MenuItemSettingRabbit_Click(object sender, RoutedEventArgs e)
        {
            _winconfigRabbit = new ConfigRabbit();
            _winconfigRabbit.ShowDialog();
        }
    }
}