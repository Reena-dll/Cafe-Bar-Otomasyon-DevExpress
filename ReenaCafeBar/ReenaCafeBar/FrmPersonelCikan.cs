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
    public partial class FrmPersonelCikan : Form
    {
        public FrmPersonelCikan()
        {
            InitializeComponent();
        }

        private void lblcikis_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        void Listele()
        {
            cReena.baglantiKontrol();
            SqlDataAdapter da = new SqlDataAdapter("exec PersonelListeCikan", cReena.con);
            DataTable dt = new DataTable();
            da.Fill(dt);
            gridControl2.DataSource = dt;
        }

        void Temizle()
        {
            txtID.Text = "";
            txtAd.Text = "";
            txtSoyad.Text = "";
        }

        private void FrmPersonelCikan_Load(object sender, EventArgs e)
        {
            Listele();
        }

        private void btnTemizle_Click(object sender, EventArgs e)
        {
            Temizle();
        }

        private void gridView2_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            DataRow dr = gridView2.GetDataRow(gridView2.FocusedRowHandle);
            if (dr != null)
            {
                txtID.Text = dr["PersonelID"].ToString();
                txtAd.Text = dr["PersonelAd"].ToString();
                txtSoyad.Text = dr["PersonelSoyad"].ToString();
            }
        }

        private void btnKaydet_Click(object sender, EventArgs e)
        {
            try
            {
                cReena.baglantiKontrol();
                SqlCommand cmd = new SqlCommand("update Personeller set Durum=1 where PersonelID=@p1", cReena.con);
                cmd.Parameters.AddWithValue("@p1", txtID.Text);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Personeliniz Yeniden İşe Alındı.", "Bilgilendirme Penceresi", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
            catch
            {
                MessageBox.Show("Ağ Hatası. Bağlantı İşlemi Gerçekleştirilemedi.", "Bilgilendirme Penceresi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            finally
            {
                Listele();
                Temizle();
                FrmPersoneller.personel.Listele();
                FrmPersoneller.personel.Temizle();

            }
        }
    }
}
