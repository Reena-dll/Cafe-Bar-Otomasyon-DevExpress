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
    public partial class FrmFirmalar : Form
    {
        public FrmFirmalar()
        {
            InitializeComponent();
        }
        void Listele()
        {
            cReena.baglantiKontrol();
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("select * from Firmalar", cReena.con);
            da.Fill(dt);
            gridControl1.DataSource = dt;
        }

        void Temizle()
        {
            txtFirmaAd.Text = "";
            txtFirmaID.Text = "";
            txtMail.Text = "";
            txtSektor.Text = "";
            txtYetkiliAdSoyad.Text = "";
            txtYetkiliStatu.Text = "";
            rchAdres.Text = "";
            rchDetay.Text = "";
            mskTC.Text = "";
            mskTel.Text = "";
        }
        private void FrmFirmalar_Load(object sender, EventArgs e)
        {
            Listele();
            Temizle();
        }

        private void btnTemizle_Click(object sender, EventArgs e)
        {
            Temizle();
        }

        private void btnKaydet_Click(object sender, EventArgs e)
        {
            try
            {
                cReena.baglantiKontrol();
                SqlCommand cmd = new SqlCommand("insert into Firmalar (FirmaAd,YetkiliAdSoyad,YetkiliStatu,YetkiliTC,Sektor,Telefon,Mail,Adres,FirmaDetay) values (@p1,@p2,@p3,@p4,@p5,@p6,@p7,@p8,@p9)", cReena.con);
                cmd.Parameters.AddWithValue("@p1", txtFirmaAd.Text);
                cmd.Parameters.AddWithValue("@p2", txtYetkiliAdSoyad.Text);
                cmd.Parameters.AddWithValue("@p3", txtYetkiliStatu.Text);
                cmd.Parameters.AddWithValue("@p4", mskTC.Text);
                cmd.Parameters.AddWithValue("@p5", txtSektor.Text);
                cmd.Parameters.AddWithValue("@p6", mskTel.Text);
                cmd.Parameters.AddWithValue("@p7", txtMail.Text);
                cmd.Parameters.AddWithValue("@p8", rchAdres.Text);
                cmd.Parameters.AddWithValue("@p9", rchDetay.Text);
                cmd.ExecuteNonQuery();
                cReena.con.Close();
                MessageBox.Show("Firma Ekleme İşlemi Başarıyla Gerçekleşti.", "Bilgilendirme Penceresi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (SqlException ex)
            {
                string hata = ex.Message;
                MessageBox.Show("Veri Tabanıyla Bağlantı Kurulurken Hata Oluştu. Hata Kodu 1", "Hata Penceresi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                Listele();
                Temizle();
            }
        }

        private void gridView1_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            DataRow dr = gridView1.GetDataRow(gridView1.FocusedRowHandle);
            if (dr != null)
            {
                txtFirmaID.Text = dr["FirmaID"].ToString();
                txtFirmaAd.Text = dr["FirmaAd"].ToString();
                txtMail.Text = dr["Mail"].ToString();
                txtSektor.Text = dr["Sektor"].ToString();
                txtYetkiliAdSoyad.Text = dr["YetkiliAdSoyad"].ToString();
                txtYetkiliStatu.Text = dr["YetkiliStatu"].ToString();
                mskTC.Text = dr["YetkiliTC"].ToString();
                mskTel.Text = dr["Telefon"].ToString();
                rchDetay.Text = dr["FirmaDetay"].ToString();
                rchAdres.Text = dr["Adres"].ToString();

            }
        }

        private void btnGuncelle_Click(object sender, EventArgs e)
        {
            try
            {
                cReena.baglantiKontrol();
                SqlCommand cmd = new SqlCommand("update Firmalar Set FirmaAd=@p1,YetkiliAdSoyad=@p2,YetkiliStatu=@p3,YetkiliTC=@p4,Sektor=@p5,Telefon=@p6,Mail=@p7,Adres=@p8,FirmaDetay=@p9 where FirmaID=@p10", cReena.con);
                cmd.Parameters.AddWithValue("@p1", txtFirmaAd.Text);
                cmd.Parameters.AddWithValue("@p2", txtYetkiliAdSoyad.Text);
                cmd.Parameters.AddWithValue("@p3", txtYetkiliStatu.Text);
                cmd.Parameters.AddWithValue("@p4", mskTC.Text);
                cmd.Parameters.AddWithValue("@p5", txtSektor.Text);
                cmd.Parameters.AddWithValue("@p6", mskTel.Text);
                cmd.Parameters.AddWithValue("@p7", txtMail.Text);
                cmd.Parameters.AddWithValue("@p8", rchAdres.Text);
                cmd.Parameters.AddWithValue("@p9", rchDetay.Text);
                cmd.Parameters.AddWithValue("@p10", txtFirmaID.Text);
                cmd.ExecuteNonQuery();
                cReena.con.Close();
                MessageBox.Show("Firma Başarıyla Güncellendi.", "Bilgilendirme Penceresi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (SqlException ex)
            {
                string hata = ex.Message;
                MessageBox.Show("Veri Tabanıyla Bağlantı Kurulurken Hata Oluştu. Hata Kodu 1", "Hata Penceresi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                Listele();
                Temizle();
            }

        }
    }
}
