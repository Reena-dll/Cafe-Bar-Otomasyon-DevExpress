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
    public partial class FrmPersoneller : Form
    {
        public FrmPersoneller()
        {
            InitializeComponent();
        }

        public static FrmPersoneller personel;
        public void Temizle()
        {
            txtID.Text = "";
            txtAd.Text = "";
            txtSoyad.Text = "";
            mskTel.Text = "";
            mskTC.Text = "";
            txtMail.Text = "";
            cmbSehir.Text = "Seçiniz";
            cmbilce.Text = "Seçiniz";
            cmbGorev.Text = "Seçiniz";
            rchAdres.Text = "";
            txtKullaniciAd.Text = "";
            txtSifre.Text = "";
        }

        void Sehirler()
        {
            cReena.baglantiKontrol();
            SqlDataAdapter da = new SqlDataAdapter("select * from Sehirler", cReena.con);
            DataTable dt = new DataTable();
            da.Fill(dt);
            cmbSehir.DisplayMember = "sehir";
            cmbSehir.ValueMember = "id";
            cmbSehir.DataSource = dt;
            cmbSehir.Text = "Seçiniz";
        }

        void Rutbeler()
        {
            cReena.baglantiKontrol();
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("Select * from PersonelRutbe", cReena.con);
            da.Fill(dt);
            cmbGorev.DataSource = dt;
            cmbGorev.DisplayMember = "Rutbe";
            cmbGorev.ValueMember = "RutbeID";
            cmbGorev.Text = "Seçiniz";
        }

        void Toplamisci()
        {
            cReena.baglantiKontrol();
            SqlCommand cmd = new SqlCommand("select count(*) from personeller where Durum=1", cReena.con);
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                lblToplamCalisan.Text = dr[0].ToString();
            }
            cReena.con.Close();
        }

        void istenAyrilan()
        {
            cReena.baglantiKontrol();
            SqlCommand cmd = new SqlCommand("select count(*) from personeller where Durum=0", cReena.con);
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                lblAyrilanToplam.Text = dr[0].ToString();
            }
            cReena.con.Close();
        }

        public void Listele()
        {
            cReena.baglantiKontrol();
            SqlDataAdapter da = new SqlDataAdapter("exec PersonelListe", cReena.con);
            DataTable dt = new DataTable();
            da.Fill(dt);
            gridControl1.DataSource = dt;
            Sehirler();
            Rutbeler();
            Toplamisci();
            istenAyrilan();
        }

        private void FrmPersoneller_Load(object sender, EventArgs e)
        {
            personel = this;
            Listele();
            Temizle();
        }

        private void cmbSehir_SelectedIndexChanged(object sender, EventArgs e)
        {
            cReena.baglantiKontrol();
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("Select * from ilceler where sehir=" + cmbSehir.SelectedValue + "", cReena.con);
            da.Fill(dt);
            cmbilce.DataSource = dt;
            cmbilce.DisplayMember = "ilce";
            cmbilce.ValueMember = "ilce";
            cmbilce.Text = "Seçiniz";
        }

        private void gridView1_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            DataRow dr = gridView1.GetDataRow(gridView1.FocusedRowHandle);
            if (dr != null)
            {
                txtID.Text = dr["PersonelID"].ToString();
                txtAd.Text = dr["PersonelAd"].ToString();
                txtSoyad.Text = dr["PersonelSoyad"].ToString();
                mskTel.Text = dr["Telefon"].ToString();
                mskTC.Text = dr["TC"].ToString();
                txtMail.Text = dr["Mail"].ToString();
                cmbSehir.Text = dr["Sehir"].ToString();
                cmbilce.Text = dr["Ilce"].ToString();
                cmbGorev.Text = dr["Rutbe"].ToString();
                rchAdres.Text = dr["Adres"].ToString();
                txtKullaniciAd.Text = dr["KullaniciAdi"].ToString();
                txtSifre.Text = dr["Sifre"].ToString();
            }
        }

        private void btnKaydet_Click(object sender, EventArgs e)
        {
            try
            {
                cReena.baglantiKontrol();
                SqlCommand cmd = new SqlCommand("insert into Personeller (PersonelAd,PersonelSoyad,TC,Telefon,Mail,Rutbe,Sehir,Ilce,Adres,KullaniciAdi,Sifre) values (@p1,@p2,@p3,@p4,@p5,@p6,@p7,@p8,@p9,@p10,@p11)", cReena.con);
                cmd.Parameters.AddWithValue("@p1", txtAd.Text);
                cmd.Parameters.AddWithValue("@p2", txtSoyad.Text);
                cmd.Parameters.AddWithValue("@p3", mskTC.Text);
                cmd.Parameters.AddWithValue("@p4", mskTel.Text);
                cmd.Parameters.AddWithValue("@p5", txtMail.Text);
                cmd.Parameters.AddWithValue("@p6", cmbGorev.SelectedValue);
                cmd.Parameters.AddWithValue("@p7", cmbSehir.Text);
                cmd.Parameters.AddWithValue("@p8", cmbilce.Text);
                cmd.Parameters.AddWithValue("@p9", rchAdres.Text);
                cmd.Parameters.AddWithValue("@p10", txtKullaniciAd.Text);
                cmd.Parameters.AddWithValue("@p11", txtSifre.Text);

                cmd.ExecuteNonQuery();
               
                MessageBox.Show("Personel Ekleme İşlemi Başarıyla Gerçekleşti.", "Bilgilendirme Penceresi", MessageBoxButtons.OK, MessageBoxIcon.Information);

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
                cReena.con.Close();
            }

        }

        private void btnTemizle_Click(object sender, EventArgs e)
        {
            Temizle();
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {

            DialogResult secenek = MessageBox.Show("Yetkiliyi İşten Çıkarmak İstiyor Musunuz ?", "Bilgilendirme Penceresi", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
            if (secenek == DialogResult.Yes)
            {
                try
                {
                    cReena.baglantiKontrol();
                    SqlCommand cmd = new SqlCommand("update Personeller set Durum=0 where PersonelID=@p1", cReena.con);
                    cmd.Parameters.AddWithValue("@p1", txtID.Text);
                    cmd.ExecuteNonQuery();     
                    MessageBox.Show("Kişi İşten Başarıyla Çıkarıldı.", "Bilgilendirme Penceresi", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
                    Temizle();
                }

            }
        }

        private void btnGuncelle_Click(object sender, EventArgs e)
        {

            try
            {
                cReena.baglantiKontrol();
                SqlCommand cmd = new SqlCommand("update Personeller set PersonelAd=@p1,PersonelSoyad=@p2,Telefon=@p3,TC=@p4,Mail=@p5,Sehir=@p6,Ilce=@p7,Rutbe=@p8,Adres=@p9,KullaniciAdi=@p11,Sifre=@p12 where PersonelID=@p10", cReena.con);
                cmd.Parameters.AddWithValue("@p1", txtAd.Text);
                cmd.Parameters.AddWithValue("@p2", txtSoyad.Text);
                cmd.Parameters.AddWithValue("@p3", mskTel.Text);
                cmd.Parameters.AddWithValue("@p4", mskTC.Text);
                cmd.Parameters.AddWithValue("@p5", txtMail.Text);
                cmd.Parameters.AddWithValue("@p6", cmbSehir.Text);
                cmd.Parameters.AddWithValue("@p7", cmbilce.Text);
                cmd.Parameters.AddWithValue("@p8", cmbGorev.SelectedValue);
                cmd.Parameters.AddWithValue("@p9", rchAdres.Text);
                cmd.Parameters.AddWithValue("@p10", txtID.Text);
                cmd.Parameters.AddWithValue("@p11", txtKullaniciAd.Text);
                cmd.Parameters.AddWithValue("@p12", txtSifre.Text);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Personel Güncelleme İşlemi Başarıyla Gerçekleşti.", "Bilgilendirme Penceresi", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
                Temizle();
            }
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            FrmPersonelCikan fr = new FrmPersonelCikan();
            fr.Show();
        }

        private void simpleButton3_Click(object sender, EventArgs e)
        {
            FrmGorevler fr = new FrmGorevler();
            fr.Show();
        }
    }
}
