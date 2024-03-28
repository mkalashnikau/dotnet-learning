namespace HelloWorldWinFormsApp
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void textBoxUsername_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                string username = textBoxUsername.Text;
                string message = HelloWorldHelper.GenerateMessage(username);

                MessageBox.Show(message, "Hello Message");
            }
        }
    }
}