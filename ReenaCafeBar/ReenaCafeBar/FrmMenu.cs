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
using DevExpress.DataAccess.Native.ExpressionEditor;

namespace ReenaCafeBar
{
    public partial class FrmMenu : Form
    {
        public FrmMenu()
        {
            InitializeComponent();
        }

        public string AktifKullanici;
        string adsoyad = "";
        void SayfaDuzen()
        {
            
            cReena.baglantiKontrol();
            SqlCommand cmd = new SqlCommand("select PersonelAd,PersonelSoyad,PersonelRutbe.Rutbe,KullaniciAdi,Sifre from Personeller inner join PersonelRutbe on PersonelRutbe.RutbeID=Personeller.Rutbe where KullaniciAdi=@p1", cReena.con);
            cmd.Parameters.AddWithValue("@p1", AktifKullanici);
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                
                if (dr["Rutbe"].ToString()=="Patron" || dr["Rutbe"].ToString() == "Müdür" || dr["Rutbe"].ToString() == "Müdür Yardımcısı" || dr["Rutbe"].ToString() == "Sekreter")
                {
                    btnPanel.Enabled = true;
                    btnRestoran.Enabled = true;
                    lblyetki.Text = (dr["PersonelAd"] + " " + dr["PersonelSoyad"] + " // " + dr["Rutbe"] + " // Yetki Sınırsız").ToString();
                }
                else
                {
                    btnPanel.Enabled = false;
                    btnRestoran.Enabled = true;
                    lblyetki.Text = (dr["PersonelAd"] + " " + dr["PersonelSoyad"] + " // " + dr["Rutbe"] + " // Yetki Sınırlı").ToString();
                }
                adsoyad = (dr["PersonelAd"] + " " + dr["PersonelSoyad"]).ToString();
            }
            
        }

        private void FrmPanel_Load(object sender, EventArgs e)
        {
            SayfaDuzen();
        }

        private void bunifuImageButton2_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Uygulamayı Kapatmak İstiyor Musunuz ?", "Bilgilendirme Penceresi", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
            {
                Application.Exit();
            }
        }

        private void btnPanel_Click(object sender, EventArgs e)
        {
            FrmPanel fr = new FrmPanel();
            fr.AktifKullanici = AktifKullanici;
            this.Hide();
            fr.Show();
        }

        private void btnRestoran_Click(object sender, EventArgs e)
        {
            FrmRestoran fr = new FrmRestoran();
            fr.AktifKullanici = AktifKullanici;
            fr.Show();
            this.Hide();
        }
    }
}
