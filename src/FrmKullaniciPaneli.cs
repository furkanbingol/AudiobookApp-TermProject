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
    public partial class FrmKullaniciPaneli : Form
    {
        public FrmKullaniciPaneli()
        {
            InitializeComponent();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Form1 frm = new Form1();
            frm.Show();
            this.Hide();
        }
        public string userID, username,password,fname,lname,wallet;
        private void FrmKullaniciPaneli_Load(object sender, EventArgs e)
        {
            lblID.Text = userID;
            lblUsername.Text = username;
            lblParola.Text = password;
            lblIsim.Text = fname;
            lblSoyisim.Text = lname;
            lblBakiye.Text = wallet;

        }

        private void button3_Click(object sender, EventArgs e)
        {
            FrmProfil frm = new FrmProfil();
            frm.userID = userID;
            frm.username = username;
            frm.password = password;
            frm.fname = fname;
            frm.lname = lname;
            frm.wallet = wallet;
            frm.Show();
           
        }

        private void button1_Click(object sender, EventArgs e)
        {
            FrmKutuphanem frm = new FrmKutuphanem();
            frm.userID = userID;
            frm.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            FrmKitapAl frm = new FrmKitapAl();
            frm.userID = userID;
            frm.wallet = wallet;
            frm.Show();
        }

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Form1 frm = new Form1();
            this.Hide();
            frm.Show();
        }
    }
}
