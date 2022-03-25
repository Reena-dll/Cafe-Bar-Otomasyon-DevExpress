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
    public partial class FrmAyarlar : Form
    {
        public FrmAyarlar()
        {
            InitializeComponent();
        }

        public string AktifKullanici;

        void AktifMail()
        {
            cReena.baglantiKontrol();
            SqlCommand cmd = new SqlCommand("Select * from Mail", cReena.con);
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                lblMailAdresi.Text = dr["mailadres"].ToString();
            }
        }

        void Listele()
        {
            cReena.baglantiKontrol();
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("select PersonelID,KullaniciAdi,Sifre from Personeller", cReena.con);
            da.Fill(dt);
            gridControl2.DataSource = dt;
            AktifMail();
        }

        void Temizle()
        {
            txtKullaniciAdi.Clear();
            txtSifre.Clear();
            txtKullaniciID.Clear();
        }

     
        private void FrmAyarlar_Load(object sender, EventArgs e)
        {
            Listele();
            Temizle();
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            Temizle();
        }

        private void gridView2_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            DataRow dr = gridView2.GetDataRow(gridView2.FocusedRowHandle);
            if (dr!=null)
            {
                txtKullaniciID.Text = dr["PersonelID"].ToString();
                txtKullaniciAdi.Text = dr["KullaniciAdi"].ToString();
                txtSifre.Text = dr["Sifre"].ToString();
            }
            
        }

        private void btnGuncelle_Click(object sender, EventArgs e)
        {
            try
            {
                cReena.baglantiKontrol();
                SqlCommand cmd = new SqlCommand("update Personeller set KullaniciAdi=@p1, Sifre=@p2 where PersonelID=@p3", cReena.con);
                cmd.Parameters.AddWithValue("@p1", txtKullaniciAdi.Text);
                cmd.Parameters.AddWithValue("@p2", txtSifre.Text);
                cmd.Parameters.AddWithValue("@p3", txtKullaniciID.Text);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Güncelleme İşlemi Başarıyla Gerçekleşti.", "Bilgilendirme Penceresi", MessageBoxButtons.OK, MessageBoxIcon.Information);

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

        private void lblcikis_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start("https://myaccount.google.com/lesssecureapps?pli=1&rapt=AEjHL4O_MxuHvxnEYEyCvV6O_SCZwByY06pHRpe5DxZzTs5v012uSIwQleFbyyQlXTaJISiufZSOzHJi_9lCjzm6Mc3JyzAy2Q");
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            if (txtMailSifre.Text==txtMailSifreTekrar.Text)
            {
                try
                {
                    cReena.baglantiKontrol();
                    SqlCommand cmd = new SqlCommand("update Mail set mailadres=@p1, mailpassword=@p2", cReena.con);
                    cmd.Parameters.AddWithValue("@p1", txtYeniMail.Text);
                    cmd.Parameters.AddWithValue("@p2", txtMailSifre.Text);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Güncelleme İşlemi Başarıyla Gerçekleşti.", "Bilgilendirme Penceresi", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
                    txtMailSifre.Text = "";
                    txtMailSifreTekrar.Text = "";
                    txtYeniMail.Text = "";
                }

            }
            else
            {
                MessageBox.Show("Şifreler Uyuşmuyor. Tekrar Deneyiniz. Hata Kodu 4", "Hata Penceresi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtMailSifre.Text = "";
                txtMailSifreTekrar.Text = "";
            }
        }
    }
}
