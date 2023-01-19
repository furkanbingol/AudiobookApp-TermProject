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
    public partial class FrmKutuphaneIslemleri : Form
    {
        public FrmKutuphaneIslemleri()
        {
            InitializeComponent();
        }

        NpgsqlConnection baglanti = new NpgsqlConnection("server=localHost; port=5432; Database=dbSesliKitap; user ID=postgres; password=123456");
        String connections = "Server=localHost;Port=5432;Database=dbSesliKitap;User Id=postgres;Password=123456;";

        private void yenile()
        {
            string sorgu = "select * from book";
            NpgsqlDataAdapter da = new NpgsqlDataAdapter(sorgu, connections);
            DataSet ds = new DataSet();
            da.Fill(ds);
            dataGridView1.DataSource = ds.Tables[0];
        }

        private void having()
        {

        }
        private void FrmKutuphaneIslemleri_Load(object sender, EventArgs e)
        {

            try
            {
                string sorgu = "select * from book";
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

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            lblId.Text = dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString();
            txtKitapAd.Text = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
            txtYazar.Text = dataGridView1.Rows[e.RowIndex].Cells[3].Value.ToString();
            txtUcret.Text = dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString();
            txtSure.Text = dataGridView1.Rows[e.RowIndex].Cells[4].Value.ToString();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                double price = Convert.ToDouble(txtUcret.Text);
                if (txtKitapAd.Text == "" || price < 5)
                {
                    MessageBox.Show("Kitap adını boş bırakamazsınız veya Kitap ücreti 5 dolardan az olamaz!","HATA",MessageBoxButtons.OK,MessageBoxIcon.Error); 
                }
                else
                {
                    baglanti.Open();
                    NpgsqlCommand komut1 = new NpgsqlCommand("insert into book(book_name,price,author,duration) values (@p1, @p2,@p3,@p4)", baglanti);
                    komut1.Parameters.AddWithValue("@p1", txtKitapAd.Text);
                    komut1.Parameters.AddWithValue("@p2", Convert.ToDouble(txtUcret.Text));
                    komut1.Parameters.AddWithValue("@p3", txtYazar.Text);
                    komut1.Parameters.AddWithValue("@p4", int.Parse(txtSure.Text));
                    komut1.ExecuteNonQuery();
                    baglanti.Close();
                    MessageBox.Show("Kitap ekleme işlemi başarılı bir şekilde gerçekleşti", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    yenile();
                }

            }
            catch (Exception a)
            {
                MessageBox.Show("Kitap adını boş bırakamazsınız veya Kitap ücreti 5 dolardan az olamaz!", "HATA", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void button2_Click(object sender, EventArgs e)
        {

            baglanti.Open();
            NpgsqlCommand komut2 = new NpgsqlCommand("delete from book where book_id=@p1", baglanti);
            komut2.Parameters.AddWithValue("@p1", int.Parse(lblId.Text));
            komut2.ExecuteNonQuery();
            baglanti.Close();
            MessageBox.Show("Kitap silme işlemi başarılı bir şekilde gerçekleşti", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            yenile();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            NpgsqlCommand komut3 = new NpgsqlCommand("update book set book_name=@p1, price=@p2, author=@p3, duration=@p4 where book_id=@p5", baglanti);
            komut3.Parameters.AddWithValue("@p1", txtKitapAd.Text);
            komut3.Parameters.AddWithValue("@p2", Convert.ToDouble(txtUcret.Text));
            komut3.Parameters.AddWithValue("@p3", txtYazar.Text);
            komut3.Parameters.AddWithValue("@p4", int.Parse(txtSure.Text));
            komut3.Parameters.AddWithValue("@p5", int.Parse(lblId.Text));
            komut3.ExecuteNonQuery();
            baglanti.Close();
            MessageBox.Show("Kitap güncelleme işlemi başarılı bir şekilde gerçekleşti", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            yenile();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            txtKitapAd.Text = "";
            txtSure.Text = "";
            txtUcret.Text = "";
            txtYazar.Text = "";
            lblId.Text = "";

        }

        private void button5_Click(object sender, EventArgs e)
        {
            try
            {
                MessageBox.Show("Bu butonla soyadı 'Yıldırım' olan ve kitaplığında 'War and Peace' kitabını kiralamış olan kişiler intersect sorgusu ile getirilmektedir.", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                string sorgu = "select username,fname,lname,wallet from person where lname='Yıldırım' intersect select username,fname,lname,wallet from person as p, book as b, bookLibrary as bl where p.id = bl.owner_id AND b.book_id = bl.book_id AND b.book_name = 'War and Peace' AND p.wallet >= b.price";
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

        private void button6_Click(object sender, EventArgs e)
        {
            yenile();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            string sorgu = "SELECT person.fname, person.lname, SUM(book.price) as total_price FROM bookLibrary JOIN person ON bookLibrary.owner_id = person.id JOIN book ON bookLibrary.book_id = book.book_id GROUP BY person.fname, person.lname HAVING SUM(book.price)>100";
            NpgsqlDataAdapter da = new NpgsqlDataAdapter(sorgu, connections);
            DataSet ds = new DataSet();
            da.Fill(ds);
            dataGridView1.DataSource = ds.Tables[0]; 
        }

        private void button8_Click(object sender, EventArgs e)
        {
            string sorgu = "SELECT increase_book_prices("+textBox1.Text+")";
            baglanti.Open();
            NpgsqlCommand komut1 = new NpgsqlCommand(sorgu, baglanti);
            komut1.ExecuteNonQuery();
            baglanti.Close();
            MessageBox.Show("Kitap fiyatlarına zam işlemi başarılı bir şekilde gerçekleşti", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            yenile();
            	

        }

        private void button9_Click(object sender, EventArgs e)
        {
                string sorgu = "select book_list()";
                NpgsqlDataAdapter da = new NpgsqlDataAdapter(sorgu, connections);
                DataSet ds = new DataSet();
                da.Fill(ds);
                dataGridView2.DataSource = ds.Tables[0];
                MessageBox.Show("Toplam market ücreti : " + dataGridView2.Rows[0].Cells[0].Value.ToString(), "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                dataGridView2.DataBindings.Clear();
         

        }

        private void button10_Click(object sender, EventArgs e)
        {
           
            string sorgu = "select person_info("+textBox2.Text+")";
            NpgsqlDataAdapter da = new NpgsqlDataAdapter(sorgu, connections);
            DataSet ds = new DataSet();
            da.Fill(ds);
            dataGridView3.DataSource = ds.Tables[0];
            //double x = Convert.ToDouble(dataGridView3.Rows[0].Cells[0].Value.ToString());
            MessageBox.Show("Bakiye : " + dataGridView3.Rows[0].Cells[0].Value.ToString());
           dataGridView3.DataBindings.Clear();
        }
    }
}
