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
using DevExpress.Data.Browsing.Design;

namespace ReenaCafeBar
{
    public partial class FrmKaldirilanUrun : Form
    {
        public FrmKaldirilanUrun()
        {
            InitializeComponent();
        }

        private void lblcikis_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        public void Listele()
        {
            SqlDataAdapter da = new SqlDataAdapter("select UrunID,UrunAd,UrunDetay from urunler where Durum=0", cReena.con);
            DataTable dt = new DataTable();
            da.Fill(dt);
            gridControl1.DataSource = dt;
        }

        public void Temizle()
        {
            txtID.Text = "";
            txtAd.Text = "";
             
        }

        private void FrmKaldirilanUrun_Load(object sender, EventArgs e)
        {
            Listele();
        }

        private void btnKaydet_Click(object sender, EventArgs e)
        {
            try
            {
                cReena.baglantiKontrol();
                SqlCommand cmd = new SqlCommand("Update Urunler set Durum=1 where UrunID=@p1", cReena.con);
                cmd.Parameters.AddWithValue("@p1", txtID.Text);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Geri Yükleme İşlemi Başarıyla Gerçekleşti.", "Bilgilendirme Penceresi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (SqlException ex)
            {
                string hata = ex.Message;
                MessageBox.Show("Veri Tabanıyla Bağlantı Kurulurken Hata Oluştu. Hata Kodu 1", "Hata Penceresi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                cReena.con.Close();
                FrmUrunler.fr.Listele();
                FrmUrunler.fr.Temizle();
                Listele();

            }
        }

        private void gridView1_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            DataRow dr = gridView1.GetDataRow(gridView1.FocusedRowHandle);
            if (dr!=null)
            {
                txtID.Text = dr["UrunID"].ToString();
                txtAd.Text = dr["UrunAd"].ToString();
            }
        }
    }
}
