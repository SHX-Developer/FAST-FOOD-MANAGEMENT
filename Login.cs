using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FastFood
{
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string username = textBox1.Text;
            string password = textBox2.Text;

            bool isValid = username == "Shaxa" && password == "123456";
            if (isValid)
            {
                Form1 form1 = new Form1();
                form1.Show();
                this.Hide();
            }
            else 
            {
                MessageBox.Show("Error username or password");
            }
        }
    }
}
