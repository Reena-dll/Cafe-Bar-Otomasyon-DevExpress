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
    public partial class FrmMutfak : Form
    {
        public FrmMutfak()
        {
            InitializeComponent();
        }

        public static FrmMutfak mutfak;

        public void Listele()
        {
            cReena.baglantiKontrol();
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("select SiparisID, MasaID as MasaNumarası, Urun, Adet,Note from MasaSiparis where Durum=0", cReena.con);
            da.Fill(dt);
            gridControl1.DataSource = dt;
        }

        public void Listele2()
        {
            cReena.baglantiKontrol();
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("select SiparisID, MasaID as MasaNumarası, Urun, Adet,Note  from MasaSiparis where Durum=1", cReena.con);
            da.Fill(dt);
            gridControl2.DataSource = dt;
        }

        private void FrmMutfak_Load(object sender, EventArgs e)
        {
            mutfak = this;
            Listele();
            Listele2();
        }

        void Temizle()
        {
            richTextBox1.Text = "";
            txtID.Text = "";
        }

        private void gridView1_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            DataRow dr = gridView1.GetDataRow(gridView1.FocusedRowHandle);
            if (dr!=null)
            {
                txtID.Text = dr["SiparisID"].ToString();
                richTextBox1.Text = dr["Note"].ToString();
            }
        }

        private void btnGuncelle_Click(object sender, EventArgs e)
        {
            try
            {
                cReena.baglantiKontrol();
                SqlCommand cmd = new SqlCommand("Update MasaSiparis set Durum=1 where SiparisID=@p1", cReena.con);
                cmd.Parameters.AddWithValue("@p1", txtID.Text);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Ürün Hazırlanma İşlemi Başarıyla Gerçekleşti.", "Bilgilendirme Penceresi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (SqlException ex)
            {
                string hata = ex.Message;
                MessageBox.Show("Veri Tabanıyla Bağlantı Kurulurken Hata Oluştu. Hata Kodu 1", "Hata Penceresi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                cReena.con.Close();
                Listele();
                Listele2();
                Temizle();
            }
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            Listele();
            Listele2();
            Temizle();
        }
    }
}
