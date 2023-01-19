using Npgsql;
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
    public partial class FrmAdminGiris : Form
    {
        public FrmAdminGiris()
        {
            InitializeComponent();
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Örnek Kullanıcı Adı : admin\n Örnek Şifre : 123");
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Form1 frm = new Form1();
            this.Hide();
            frm.Show();
        
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked == true)
            {
                txtSifre.UseSystemPasswordChar = false;
            }
            else
            {
                txtSifre.UseSystemPasswordChar = true;

            }
        }

        NpgsqlConnection baglanti = new NpgsqlConnection("server=localHost; port=5432; Database=dbSesliKitap; user ID=postgres; password=123456");
        String connections = "Server=localHost;Port=5432;Database=dbSesliKitap;User Id=postgres;Password=123456;";
        private void FrmAdminGiris_Load(object sender, EventArgs e)
        {
            try
            {
                string sorgu = "select * from admin";
                NpgsqlDataAdapter da = new NpgsqlDataAdapter(sorgu, connections);
                DataSet ds = new DataSet();
                da.Fill(ds);
                dataGridView1.DataSource = ds.Tables[0];
            }
            catch (Exception a)
            {

                MessageBox.Show(a.ToString());
            }

     

            }

        private void button1_Click(object sender, EventArgs e)
        {
            if (txtSifre.Text == "" || txtKullaniciAdi.Text == "")
            {
                MessageBox.Show("Alanlar boş olamaz", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else 
            {
                string kullaniciAdi = txtKullaniciAdi.Text;
                int parola = int.Parse(txtSifre.Text);
                int x = 0;
                for (int i = 0; i < dataGridView1.Rows.Count-1; i++)
                {
                    if (dataGridView1.Rows[i].Cells[1].Value.ToString() == kullaniciAdi && dataGridView1.Rows[i].Cells[2].Value.ToString() == parola.ToString())
                    {
                        FrmAdminPanel frm = new FrmAdminPanel();
                           frm.Show();
                           this.Hide();
                           x = 1;
                    }
 
                }
                if (x == 0)
                    MessageBox.Show("Hatalı kullanıcı adı veya şifre girdiniz", "HATA", MessageBoxButtons.OK, MessageBoxIcon.Error);



             }


        }

        private void txtSifre_KeyPress(object sender, KeyPressEventArgs e)
        {
    if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) &&
        (e.KeyChar != '.'))
    {
            e.Handled = true;
    }

    // only allow one decimal point
    if ((e.KeyChar == '.') && ((sender as TextBox).Text.IndexOf('.') > -1))
    {
        e.Handled = true;
    }

        }
    }
}
