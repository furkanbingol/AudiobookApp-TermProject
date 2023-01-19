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
    public partial class FrmKutuphanem : Form
    {
        public FrmKutuphanem()
        {
            InitializeComponent();
        }
        NpgsqlConnection baglanti = new NpgsqlConnection("server=localHost; port=5432; Database=dbSesliKitap; user ID=postgres; password=123456");
        String connections = "Server=localHost;Port=5432;Database=dbSesliKitap;User Id=postgres;Password=123456;";
        public string userID;
        private void FrmKutuphanem_Load(object sender, EventArgs e)
        {
            string sorgu = "select A.book_name,A.author,A.price,A.duration from bookLibrary as B,book as A where B.owner_id ="+userID+" and A.book_id=B.book_id";
            NpgsqlDataAdapter da = new NpgsqlDataAdapter(sorgu, connections);
            DataSet ds = new DataSet();
            da.Fill(ds);
            dataGridView1.DataSource = ds.Tables[0];

            lblAdet.Text = "Kütüphanenizdeki Kitap Sayısı : " + (dataGridView1.Rows.Count - 1).ToString();
        }
    }
}
