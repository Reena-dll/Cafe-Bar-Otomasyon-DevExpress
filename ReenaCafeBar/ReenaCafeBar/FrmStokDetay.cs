using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace ReenaCafeBar
{
    public partial class FrmStokDetay : Form
    {
        public FrmStokDetay()
        {
            InitializeComponent();
        }

        public string Kategori;

        void Listele()
        {
            cReena.baglantiKontrol();
            SqlCommand cmd = new SqlCommand("select UrunAd,Stok,GelisFiyat,SatisFiyat,UrunDetay from Urunler where UrunKategori=(select KategoriID from UrunKategori where KategoriAd=@p1)", cReena.con);
            cmd.Parameters.AddWithValue("@p1", Kategori);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            gridControl1.DataSource = dt;
            this.Text = Kategori;

        }

        private void FrmStokDetay_Load(object sender, EventArgs e)
        {
            Listele();
        }
    }
}
