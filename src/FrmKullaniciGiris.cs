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
    public partial class FrmKullaniciGiris : Form
    {
        public FrmKullaniciGiris()
        {
            InitializeComponent();
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Örnek Kullanıcı Adı : user\n Örnek Şifre : 123");
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

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Form1 frm = new Form1();
            this.Hide();
            frm.Show();
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
                string parola = txtSifre.Text;
                int x = 0;
                for (int i = 0; i < dataGridView1.Rows.Count - 1; i++)
                {
                    if (dataGridView1.Rows[i].Cells[1].Value.ToString() == kullaniciAdi && dataGridView1.Rows[i].Cells[2].Value.ToString() == parola)
                    {
                        FrmKullaniciPaneli frm = new FrmKullaniciPaneli();
                        frm.userID = dataGridView1.Rows[i].Cells[0].Value.ToString();
                        frm.username = dataGridView1.Rows[i].Cells[1].Value.ToString();
                        frm.password = dataGridView1.Rows[i].Cells[2].Value.ToString();
                        frm.fname = dataGridView1.Rows[i].Cells[3].Value.ToString();
                        frm.lname = dataGridView1.Rows[i].Cells[4].Value.ToString();
                        frm.wallet = dataGridView1.Rows[i].Cells[5].Value.ToString();

                        frm.Show();
                        this.Hide();
                        x = 1;
                    }

                }
                if (x == 0)
                    MessageBox.Show("Hatalı kullanıcı adı veya şifre girdiniz", "HATA", MessageBoxButtons.OK, MessageBoxIcon.Error);



            }

        }

        NpgsqlConnection baglanti = new NpgsqlConnection("server=localHost; port=5432; Database=dbSesliKitap; user ID=postgres; password=123456");
        String connections = "Server=localHost;Port=5432;Database=dbSesliKitap;User Id=postgres;Password=123456;";
        private void FrmKullaniciGiris_Load(object sender, EventArgs e)
        {

            try
            {
                string sorgu = "select * from person";
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

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {

        }
    }
}
