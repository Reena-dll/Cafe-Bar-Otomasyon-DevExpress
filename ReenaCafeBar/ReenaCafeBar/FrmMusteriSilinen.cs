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
    public partial class FrmMusteriSilinen : Form
    {
        public FrmMusteriSilinen()
        {
            InitializeComponent();
        }

        private void lblcikis_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        void Temizle()
        {
            txtID.Text = "";
            txtAd.Text = "";
            txtSoyad.Text = "";

        }

        void Listele()
        {
            cReena.baglantiKontrol();
            SqlCommand cmd = new SqlCommand("select MusteriID,MusteriAD,MusteriSoyad,Telefon,Mail,Sehir,Ilce,Adres from Musteriler where Durum=0", cReena.con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            gridControl2.DataSource = dt;
        }


        private void FrmMusteriSilinen_Load(object sender, EventArgs e)
        {
            Listele();
        }

        private void gridView2_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            DataRow dr = gridView2.GetDataRow(gridView2.FocusedRowHandle);
            if (dr != null)
            {
                txtID.Text = dr["MusteriID"].ToString();
                txtAd.Text = dr["MusteriAd"].ToString();
                txtSoyad.Text = dr["MusteriSoyad"].ToString();
            }
        }

        private void btnKaydet_Click(object sender, EventArgs e)
        {
            try
            {
                cReena.baglantiKontrol();
                SqlCommand cmd = new SqlCommand("Update Musteriler set Durum=1 where MusteriID=@p1", cReena.con);
                cmd.Parameters.AddWithValue("@p1", txtID.Text);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Müşteri Tabloya Geri Alındı", "Bilgilendirme Penceresi", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
            catch
            {
                MessageBox.Show("Ağ Hatası. Bağlantı İşlemi Gerçekleştirilemedi.", "Bilgilendirme Penceresi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            finally
            {
                Listele();
                Temizle();
                FrmMusteriler.musteriler.Listele();
                FrmMusteriler.musteriler.Temizle();
            }
        }

        private void btnTemizle_Click(object sender, EventArgs e)
        {
            Temizle();
        }
    }
}
