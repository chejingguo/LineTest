using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace BTLProductLine
{
    public partial class TPasswordForm : Form
    {
        public TPasswordForm()
        {
            InitializeComponent();
            this.ControlBox = false; 
        }

        private void OK_Click(object sender, EventArgs e)
        {
            if (Password.Text == "1qaz2wsx")
            {
                this.Hide();
            }
            else
            {
                MessageBox.Show("密码不正确，请重新输入？");
            }
        }
    }
}
