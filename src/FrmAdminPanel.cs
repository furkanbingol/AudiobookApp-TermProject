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
    public partial class FrmAdminPanel : Form
    {
        public FrmAdminPanel()
        {
            InitializeComponent();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            FrmKutuphaneIslemleri frm = new FrmKutuphaneIslemleri();
            frm.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            FrmKullaniciİslemleri frm = new FrmKullaniciİslemleri();
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
