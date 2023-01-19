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
    public partial class FrmKullaniciİslemleri : Form
    {
        public FrmKullaniciİslemleri()
        {
            InitializeComponent();
        }
        NpgsqlConnection baglanti = new NpgsqlConnection("server=localHost; port=5432; Database=dbSesliKitap; user ID=postgres; password=123456");
        String connections = "Server=localHost;Port=5432;Database=dbSesliKitap;User Id=postgres;Password=123456;";
        private void FrmKullaniciİslemleri_Load(object sender, EventArgs e)
        {
            string sorgu = "select * from person";
            NpgsqlDataAdapter da = new NpgsqlDataAdapter(sorgu, connections);
            DataSet ds = new DataSet();
            da.Fill(ds);
            dataGridView1.DataSource = ds.Tables[0];
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                lblId.Text = dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString();
                txtAd.Text = dataGridView1.Rows[e.RowIndex].Cells[3].Value.ToString();
                txtSoyad.Text = dataGridView1.Rows[e.RowIndex].Cells[4].Value.ToString();
                txtUsername.Text = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
                txtBakiye.Text = dataGridView1.Rows[e.RowIndex].Cells[5].Value.ToString();
                txtSifre.Text = dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString();
            }
            catch (Exception)
            {

            }
          
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
        private void yenile()
        {
            string sorgu = "select * from person";
            NpgsqlDataAdapter da = new NpgsqlDataAdapter(sorgu, connections);
            DataSet ds = new DataSet();
            da.Fill(ds);
            dataGridView1.DataSource = ds.Tables[0];
            dataGridView1.Sort(dataGridView1.Columns[0], ListSortDirection.Ascending);

        }
        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                txtBakiye.Enabled = false;
                baglanti.Open();
                NpgsqlCommand komut1 = new NpgsqlCommand("insert into person(username,password,fname,lname,wallet) values (@p1, @p2,@p3,@p4,nextval('seq'))", baglanti);
                komut1.Parameters.AddWithValue("@p1", txtUsername.Text);
                komut1.Parameters.AddWithValue("@p2", txtSifre.Text);
                komut1.Parameters.AddWithValue("@p3", txtAd.Text);
                komut1.Parameters.AddWithValue("@p4", txtSoyad.Text);
               // komut1.Parameters.AddWithValue("@p5", Convert.ToDouble(txtBakiye.Text));
                komut1.ExecuteNonQuery();
                baglanti.Close();
                MessageBox.Show("Kullanıcı ekleme işlemi başarılı bir şekilde gerçekleşti", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                yenile();
            }
            catch (Exception a)
            {
                MessageBox.Show(a.ToString());
            }
            txtBakiye.Enabled = true;
        }

        private void button2_Click(object sender, EventArgs e)
        {

            baglanti.Open();
            NpgsqlCommand komut4 = new NpgsqlCommand("delete from booklibrary where owner_id=@p1", baglanti);
            komut4.Parameters.AddWithValue("@p1", int.Parse(lblId.Text));
            komut4.ExecuteNonQuery();
            NpgsqlCommand komut2 = new NpgsqlCommand("delete from person where id=@p1", baglanti);
            komut2.Parameters.AddWithValue("@p1", int.Parse(lblId.Text));
            komut2.ExecuteNonQuery();

            baglanti.Close();
            MessageBox.Show("Kullanıcı silme işlemi başarılı bir şekilde gerçekleşti", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            yenile();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            NpgsqlCommand komut3 = new NpgsqlCommand("update person set id=@p1, username=@p2, password=@p3, fname=@p4, lname=@p5, wallet=@p6 where id=@p1", baglanti);
            komut3.Parameters.AddWithValue("@p1", int.Parse(lblId.Text));
            komut3.Parameters.AddWithValue("@p2", txtUsername.Text);
            komut3.Parameters.AddWithValue("@p3", txtSifre.Text);
            komut3.Parameters.AddWithValue("@p4", txtAd.Text);
            komut3.Parameters.AddWithValue("@p5", txtSoyad.Text);
            komut3.Parameters.AddWithValue("@p6", Convert.ToDouble(txtBakiye.Text));
            komut3.ExecuteNonQuery();
            baglanti.Close();
            MessageBox.Show("Kullanıcı güncelleme işlemi başarılı bir şekilde gerçekleşti", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            yenile();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            txtAd.Text = "";
            txtBakiye.Text = "";
            txtSifre.Text = "";
            txtSoyad.Text = "";
            txtUsername.Text = "";
            lblId.Text = "";
        }

        private void button5_Click(object sender, EventArgs e)
        {
            //string sorgu = "select * from bakiyeler";
            //NpgsqlDataAdapter da = new NpgsqlDataAdapter(sorgu, connections);
            //DataSet ds = new DataSet();
            //da.Fill(ds);
            //dataGridView1.DataSource = ds.Tables[0];
            //dataGridView1.Sort(dataGridView1.Columns[0], ListSortDirection.Ascending);

          double min = Convert.ToDouble(textBox1.Text);
                    double max = Convert.ToDouble(textBox2.Text);
          

                try
                {
                    baglanti.Open();
                    string query = "CREATE VIEW bakiye"+min+max+" AS select username,wallet from person where wallet BETWEEN " + min + " AND " + max;
                    using (NpgsqlCommand command = new NpgsqlCommand(query, baglanti))
                    {
                        command.ExecuteNonQuery();
                    }
                    baglanti.Close();

                    string sorgu = "select * from bakiye" + min + max;
                    NpgsqlDataAdapter da = new NpgsqlDataAdapter(sorgu, connections);
                    DataSet ds = new DataSet();
                    da.Fill(ds);
                    dataGridView1.DataSource = ds.Tables[0];
                    dataGridView1.Sort(dataGridView1.Columns[0], ListSortDirection.Ascending);
        
                }
                catch (Exception)
                {
                    string sorgu = "select username,wallet from person where wallet BETWEEN "+min+" AND "+max;
                    NpgsqlDataAdapter da = new NpgsqlDataAdapter(sorgu, connections);
                    DataSet ds = new DataSet();
                    da.Fill(ds);
                    dataGridView1.DataSource = ds.Tables[0];
                    dataGridView1.Sort(dataGridView1.Columns[0], ListSortDirection.Ascending);
                }
              

           
   
        }

        private void button6_Click(object sender, EventArgs e)
        {
            yenile();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            try
            {
                baglanti.Open();
                NpgsqlCommand komut1 = new NpgsqlCommand("insert into person(username,password,fname,lname,wallet) values (@p1, @p2,@p3,@p4,@p5)", baglanti);
                komut1.Parameters.AddWithValue("@p1", txtUsername.Text);
                komut1.Parameters.AddWithValue("@p2", txtSifre.Text);
                komut1.Parameters.AddWithValue("@p3", txtAd.Text);
                komut1.Parameters.AddWithValue("@p4", txtSoyad.Text);
               
              komut1.Parameters.AddWithValue("@p5", Convert.ToDouble(txtBakiye.Text));
                komut1.ExecuteNonQuery();
                baglanti.Close();
                MessageBox.Show("Kullanıcı ekleme işlemi başarılı bir şekilde gerçekleşti", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                yenile();
            }
            catch (Exception a)
            {
                MessageBox.Show(a.ToString());
            }
        }
    }
}
