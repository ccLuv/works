namespace WinFormsApp1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Button1(object sender, EventArgs e)
        {
            int a = int.Parse(textBox1.Text);
            int b = int.Parse(textBox2.Text);
            string answer = (a + b).ToString();
            label1.Text = "答案是" + answer;
        }

        private void Button2(object sender, EventArgs e)
        {
            int a = int.Parse(textBox1.Text);
            int b = int.Parse(textBox2.Text);
            string answer = (a - b).ToString();
            label1.Text = "答案是" + answer;
        }

        private void Button3(object sender, EventArgs e)
        {
            int a = int.Parse(textBox1.Text);
            int b = int.Parse(textBox2.Text);
            string answer = (a * b).ToString();
            label1.Text = "答案是" + answer;
        }

        private void Button4(object sender, EventArgs e)
        {
            int a = int.Parse(textBox1.Text);
            int b = int.Parse(textBox2.Text);
            if (b == 0) label1.Text = "Error";
            else
            {
                string answer = (a / b).ToString();
                label1.Text = "答案是" + answer;
            }
        }
    }
}
