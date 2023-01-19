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
    public partial class FrmKitapAl : Form
    {
        public FrmKitapAl()
        {
            InitializeComponent();
        }
        NpgsqlConnection baglanti = new NpgsqlConnection("server=localHost; port=5432; Database=dbSesliKitap; user ID=postgres; password=123456");
        String connections = "Server=localHost;Port=5432;Database=dbSesliKitap;User Id=postgres;Password=123456;";
        public string userID,wallet;

        private void FrmKitapAl_Load(object sender, EventArgs e)
        {
            string sorgu = "select * from book";
            NpgsqlDataAdapter da = new NpgsqlDataAdapter(sorgu, connections);
            DataSet ds = new DataSet();
            da.Fill(ds);
            dataGridView1.DataSource = ds.Tables[0];
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            label3.Text = dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString();
            textBox1.Text = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
            textBox2.Text = dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString();
     
        }

        public double wlt=-1;
        private void button1_Click(object sender, EventArgs e)
        {
            if(textBox1.Text=="")
            {
                MessageBox.Show("Lütfen önce seçiminizi yapınız","HATA",MessageBoxButtons.OK,MessageBoxIcon.Error);
            }else
            {
               if(wlt == -1)
                 wlt = Convert.ToDouble(wallet);

                  if (wlt > Convert.ToDouble(textBox2.Text))
                     {
                wlt = wlt - Convert.ToDouble(textBox2.Text);

                baglanti.Open();
                NpgsqlCommand komut3 = new NpgsqlCommand("update person set wallet=@p6 where id=@p1", baglanti);
                komut3.Parameters.AddWithValue("@p1", int.Parse(userID));
                komut3.Parameters.AddWithValue("@p6", wlt);
                komut3.ExecuteNonQuery();

                NpgsqlCommand komut1 = new NpgsqlCommand("insert into booklibrary(owner_id,book_id) values (@p2,@p3)", baglanti);
                komut1.Parameters.AddWithValue("@p2", int.Parse(userID));
                komut1.Parameters.AddWithValue("@p3", int.Parse(label3.Text));
                komut1.ExecuteNonQuery();
                baglanti.Close();
                MessageBox.Show("Kitap satın alma işlemi başarılı bir şekilde gerçekleşti", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
            else
            {
                MessageBox.Show("Bu kitabı satın almak için bakiyeniz yetersiz! Code:10001", "HATA", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        }
    }
}
