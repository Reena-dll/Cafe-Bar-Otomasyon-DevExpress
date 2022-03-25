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
    public partial class FrmStoklar : Form
    {
        public FrmStoklar()
        {
            InitializeComponent();
        }

        void Listele()
        {
            try
            {
                cReena.baglantiKontrol();
                SqlDataAdapter da = new SqlDataAdapter("select UrunKategori.KategoriAd, SUM(Stok) as StokSayi from urunler inner join UrunKategori on urunler.UrunKategori=UrunKategori.KategoriID where Durum=1 group by UrunKategori.KategoriAd ", cReena.con);
                DataTable dt = new DataTable();
                da.Fill(dt);
                gridControl1.DataSource = dt;



                SqlCommand komut = new SqlCommand("Select KategoriAd, sum(Stok) as Miktar from Urunler inner join UrunKategori on Urunler.UrunKategori= UrunKategori.KategoriID where Durum=1 group by KategoriAd ", cReena.con);
                SqlDataReader dr = komut.ExecuteReader();
                while (dr.Read())
                {
                    chartControl1.Series["Series 1"].Points.AddPoint(Convert.ToString(dr[0]), Convert.ToInt32(dr[1]));
                }

            }
            catch (SqlException ex)
            {
                string hata = ex.Message;
                MessageBox.Show("Veri Tabanıyla Bağlantı Kurulurken Hata Oluştu. Hata Kodu 1", "Hata Penceresi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                cReena.con.Close();
            }
          
        }

        private void FrmStoklar_Load(object sender, EventArgs e)
        {
            Listele();
        }

        private void gridView1_DoubleClick(object sender, EventArgs e)
        {

            FrmStokDetay fr = new FrmStokDetay();
            DataRow dr = gridView1.GetDataRow(gridView1.FocusedRowHandle);

            if (dr != null)
            {
                fr.Kategori = dr["KategoriAd"].ToString();
                fr.Show();
            }

        }
    }
}
