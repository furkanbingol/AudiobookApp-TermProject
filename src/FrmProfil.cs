using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace $safeprojectname$
{
    public partial class FrmProfil : Form
    {
        public FrmProfil()
        {
            InitializeComponent();
        }
        public string userID, username, password, fname, lname, wallet;
        private void FrmProfil_Load(object sender, EventArgs e)
        {
            
            lblUsername.Text = username;
            lblParola.Text = password;
            lblIsim.Text = fname;
            lblSoyisim.Text = lname;
            lblBakiye.Text = wallet;
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void label10_Click(object sender, EventArgs e)
        {

        }

        private void label11_Click(object sender, EventArgs e)
        {

        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {

        }
    }
}
