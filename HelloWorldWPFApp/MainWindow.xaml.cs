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

namespace HelloWorldWPFApp
{
    public partial class MainWindow : Window
    {
        public string Username { get; set; }
        public MainWindow()
        {
            InitializeComponent();
        }
        private void textBoxUsername_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Return)
            {
                string username = textBoxUsername.Text;
                string message = HelloWorldHelper.GenerateMessage(username);

                MessageBox.Show(message, "Hello Message");

                // Clear the TextBox for next input
                textBoxUsername.Clear();
            }
        }
    }
}