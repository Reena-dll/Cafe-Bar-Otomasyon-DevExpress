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
using DevExpress.XtraRichEdit.Import.OpenXml;

namespace ReenaCafeBar
{
    public partial class KafeSiparisMasa : Form
    {
        public KafeSiparisMasa()
        {
            InitializeComponent();
        }
        void Tarih()
        {
            mskTarih.Text = DateTime.Now.ToString();

        }

        void KategoriList()
        {
            cReena.baglantiKontrol();
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("select * from UrunKategori", cReena.con);
            da.Fill(dt);
            cmbKategori.DataSource = dt;
            cmbKategori.DisplayMember = "KategoriAd";
            cmbKategori.ValueMember = "KategoriID";
            cmbKategori.Text = "Seçiniz";

        }

        void PersonelList()
        {
            SqlDataAdapter da = new SqlDataAdapter("Select PersonelID,PersonelAd+' '+PersonelSoyad as Personel from Personeller where Rutbe=6", cReena.con);
            DataTable dt = new DataTable();
            da.Fill(dt);
            cmbPersonel.DataSource = dt;
            cmbPersonel.DisplayMember = "Personel";
            cmbPersonel.ValueMember = "PersonelID";
            cmbPersonel.Text = "Seçiniz";
        }

        void MusteriList()
        {

            SqlDataAdapter da = new SqlDataAdapter("select MusteriID, MusteriAd + ' ' + MusteriSoyad as Musteri from Musteriler", cReena.con);
            DataTable dt = new DataTable();
            da.Fill(dt);
            cmbMusteri.DataSource = dt;
            cmbMusteri.DisplayMember = "Musteri";
            cmbMusteri.ValueMember = "MusteriID";
            cmbMusteri.Text = "Seçiniz";
        }

        void ToplamUcretList()
        {
            SqlCommand cmd = new SqlCommand("select sum(Toplam) from MasaSiparis where MasaID=@p1", cReena.con);
            cmd.Parameters.AddWithValue("@p1", FrmRestoran.MasaNumarasi);
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                lbltoplamücret.Text = dr[0].ToString() + " TL";
            }
        }

        void MasaList()
        {
            SqlCommand cmd = new SqlCommand("Select Urun,Adet,Fiyat,Toplam,Note from MasaSiparis where MasaID=@p1", cReena.con);
            cmd.Parameters.AddWithValue("@p1", FrmRestoran.MasaNumarasi);
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                listBox1.Items.Add(dr[0] + " x" + dr[1] + " --  Fiyat: " + dr[2] + " -- Toplam: " + dr[3] + " TL -- " + dr[4]);
            }
        }

        void Temizle()
        {
            listBox1.Items.Clear();
            cmbUrun.Text = "Seçiniz";
            numericupdown1.Value = 0;
            txtFiyat.Text = "0.00";
            txtToplam.Text = "0.00";
            rchNotlar.Text = "";
        }


        void Listele()
        {
            Temizle();
            PersonelList();
            MusteriList();
            KategoriList();
            MasaList();
            ToplamUcretList();
            Tarih();
            
        }



        private void KafeSiparisMasa_Load(object sender, EventArgs e)
        {
            this.Text = "Masa " + FrmRestoran.MasaNumarasi.ToString();
            Listele();
        }

        private void cmbKategori_SelectedIndexChanged(object sender, EventArgs e)
        {
        }

        private void cmbKategori_SelectionChangeCommitted(object sender, EventArgs e)
        {

            cReena.baglantiKontrol();
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("Select * from Urunler where UrunKategori=" + cmbKategori.SelectedValue + " and Durum=1", cReena.con);
            da.Fill(dt);
            cmbUrun.DataSource = dt;
            cmbUrun.DisplayMember = "UrunAd";
            cmbUrun.ValueMember = "UrunID";
            cmbUrun.Text = "Seçiniz";
        }

        private void numericupdown1_ValueChanged(object sender, EventArgs e)
        {
            double toplam = 0;
            double fiyat = Convert.ToDouble(txtFiyat.Text);
            double numeric = Convert.ToDouble(numericupdown1.Value);
            toplam = fiyat * numeric;

            txtToplam.Text = toplam.ToString();
        }



        private void cmbUrun_SelectionChangeCommitted(object sender, EventArgs e)
        {

            SqlCommand cmd = new SqlCommand("Select SatisFiyat from Urunler where UrunID=@p1", cReena.con);
            cmd.Parameters.AddWithValue("@p1", cmbUrun.SelectedValue);
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                txtFiyat.Text = dr[0].ToString();
            }

            double toplam = 0;
            double fiyat = Convert.ToDouble(txtFiyat.Text);
            double numeric = Convert.ToDouble(numericupdown1.Value);
            toplam = fiyat * numeric;

            txtToplam.Text = toplam.ToString();

        }

        private void btnGuncelle_Click(object sender, EventArgs e)
        {
            Temizle();
            Listele();
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            DialogResult secenek = MessageBox.Show("Masayı Ücretsiz Bir Şekilde Kapatmak İstiyor Musunuz ?", "Bilgilendirme Penceresi", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
            if (secenek == DialogResult.Yes)
            {
                cReena.baglantiKontrol();
                SqlCommand cmd = new SqlCommand("Delete from MasaSiparis where MasaID=@p1", cReena.con);
                cmd.Parameters.AddWithValue("@p1", FrmRestoran.MasaNumarasi);
                cmd.ExecuteNonQuery();
                cReena.masaNoGetir(FrmRestoran.MasaNumarasi.ToString());
                this.Close();
                FrmRestoran.Restoran.Temizle();
                FrmRestoran.Restoran.Listele();

            }
        }

        private void simpleButton4_Click(object sender, EventArgs e)
        {
            SqlCommand cmd = new SqlCommand("Delete from MasaSiparis where MasaID=@p1", cReena.con);
            cmd.Parameters.AddWithValue("@p1", FrmRestoran.MasaNumarasi);
            cmd.ExecuteNonQuery();
            cReena.masaNoGetir(FrmRestoran.MasaNumarasi.ToString());
            MessageBox.Show("Ücret Alındı ve Masa Kapatıldı...", "Bilgilendirme Penceresi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            this.Close();
            FrmRestoran.Restoran.Temizle();
            FrmRestoran.Restoran.Listele();
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            decimal adet = 0;
            adet = numericupdown1.Value;
            int StokAdet = 0;

            cReena.baglantiKontrol();
            SqlCommand cmd3 = new SqlCommand("select Stok from Urunler where UrunID=@p1", cReena.con);
            cmd3.Parameters.AddWithValue("@p1", cmbUrun.SelectedValue);
            SqlDataReader dr3 = cmd3.ExecuteReader();
            while (dr3.Read())
            {
                StokAdet = int.Parse(dr3[0].ToString());
            }


            if (StokAdet > adet)
            {
                cReena.baglantiKontrol();
                SqlCommand cmd = new SqlCommand("insert into MasaSiparis (MasaID,Urun,Adet,Fiyat,Toplam,Note) values (@p1,@p2,@p3,@p4,@p5,@p6)", cReena.con);
                cmd.Parameters.AddWithValue("@p1", FrmRestoran.MasaNumarasi);
                cmd.Parameters.AddWithValue("@p2", cmbUrun.Text);
                cmd.Parameters.AddWithValue("@p3", (numericupdown1.Value).ToString());
                cmd.Parameters.AddWithValue("@p4", txtFiyat.Text);
                cmd.Parameters.AddWithValue("@p5", decimal.Parse(txtToplam.Text));
                cmd.Parameters.AddWithValue("@p6", rchNotlar.Text);

                cmd.ExecuteNonQuery();

                SqlCommand cmd2 = new SqlCommand("insert into Hareketler (Urun,Adet,Personel,Musteri,Fiyat,Toplam,Tarih,Notlar) values" +
                   "(@p1,@p2,@p3,@p4,@p5,@p6,@p7,@p8)", cReena.con);
                cmd2.Parameters.AddWithValue("@p1", cmbUrun.SelectedValue);
                cmd2.Parameters.AddWithValue("@p2", Convert.ToInt16(adet));
                cmd2.Parameters.AddWithValue("@p3", cmbPersonel.SelectedValue);
                cmd2.Parameters.AddWithValue("@p4", cmbMusteri.SelectedValue);
                cmd2.Parameters.AddWithValue("@p5", decimal.Parse(txtFiyat.Text));
                cmd2.Parameters.AddWithValue("@p6", decimal.Parse(txtToplam.Text));
                cmd2.Parameters.AddWithValue("@p7", mskTarih.Text);
                cmd2.Parameters.AddWithValue("@p8", rchNotlar.Text);
                cmd2.ExecuteNonQuery();

                MessageBox.Show("Sipariş Verildi", "Bilgilendirme Penceresi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Temizle();
                Listele();
                

                
            }

            else
            {
                MessageBox.Show("Yeterli Stok Yok. Stok Güncelleyin!!!", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Temizle();
                Listele();

            }

        }
    }
}
