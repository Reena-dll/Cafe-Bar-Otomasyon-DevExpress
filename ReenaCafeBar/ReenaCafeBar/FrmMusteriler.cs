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
    public partial class FrmMusteriler : Form
    {
        public FrmMusteriler()
        {
            InitializeComponent();
        }
        public static FrmMusteriler musteriler;


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

        void MusteriSayi()
        {
            cReena.baglantiKontrol();
            SqlCommand cmd = new SqlCommand("Select count(*) from Musteriler where Durum=1", cReena.con);
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                lblToplamMusteri.Text = dr[0].ToString();
            }
        }

        public void Listele()
        {
            cReena.baglantiKontrol();
            SqlDataAdapter da = new SqlDataAdapter("select MusteriID,MusteriAD,MusteriSoyad,Telefon,Mail,Sehir,Ilce,Adres from Musteriler where Durum=1 order by MusteriAd", cReena.con);
            DataTable dt = new DataTable();
            da.Fill(dt);
            gridControl1.DataSource = dt;

            Sehirler();
            MusteriSayi();
        }

        public void Temizle()
        {
            txtID.Text = "";
            txtAd.Text = "";
            txtSoyad.Text = "";
            mskTel.Text = "";
            txtMail.Text = "";
            cmbSehir.Text = "Seçiniz";
            cmbilce.Text = "Seçiniz";
            rchAdres.Text = "";
        }


        private void FrmMusteriler_Load(object sender, EventArgs e)
        {
            musteriler = this;
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

        private void btnTemizle_Click(object sender, EventArgs e)
        {
            Temizle();
        }

        private void btnKaydet_Click(object sender, EventArgs e)
        {
            try
            {
                cReena.baglantiKontrol();
                SqlCommand cmd = new SqlCommand("insert into Musteriler (MusteriAd,MusteriSoyad,Telefon,Mail,Sehir,Ilce,Adres) values (@p1,@p2,@p3,@p4,@p5,@p6,@p7)", cReena.con);
                cmd.Parameters.AddWithValue("@p1", txtAd.Text);
                cmd.Parameters.AddWithValue("@p2", txtSoyad.Text);
                cmd.Parameters.AddWithValue("@p3", mskTel.Text);
                cmd.Parameters.AddWithValue("@p4", txtMail.Text);
                cmd.Parameters.AddWithValue("@p5", cmbSehir.Text);
                cmd.Parameters.AddWithValue("@p6", cmbilce.Text);
                cmd.Parameters.AddWithValue("@p7", rchAdres.Text);
                cmd.ExecuteNonQuery();
                
                MessageBox.Show("Müşteri Ekleme İşlemi Başarıyla Gerçekleşti.", "Bilgilendirme Penceresi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            catch(SqlException ex)
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

        private void gridView1_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            DataRow dr = gridView1.GetDataRow(gridView1.FocusedRowHandle);

            if (dr != null)
            {
                txtID.Text = dr["MusteriID"].ToString();
                txtAd.Text = dr["MusteriAd"].ToString();
                txtSoyad.Text = dr["MusteriSoyad"].ToString();
                mskTel.Text = dr["Telefon"].ToString();
                txtMail.Text = dr["Mail"].ToString();
                cmbSehir.Text = dr["Sehir"].ToString();
                cmbilce.Text = dr["Ilce"].ToString();
                rchAdres.Text = dr["Adres"].ToString();
            }
        }

        private void btnGuncelle_Click(object sender, EventArgs e)
        {
            try
            {
                cReena.baglantiKontrol();
                SqlCommand cmd = new SqlCommand("Update Musteriler set MusteriAd=@p1, MusteriSoyad=@p2, Telefon=@p3,Mail=@p4,Sehir=@p5,Ilce=@p6,Adres=@p7 where MusteriID=@p8", cReena.con);
                cmd.Parameters.AddWithValue("@p1", txtAd.Text);
                cmd.Parameters.AddWithValue("@p2", txtSoyad.Text);
                cmd.Parameters.AddWithValue("@p3", mskTel.Text);
                cmd.Parameters.AddWithValue("@p4", txtMail.Text);
                cmd.Parameters.AddWithValue("@p5", cmbSehir.Text);
                cmd.Parameters.AddWithValue("@p6", cmbilce.Text);
                cmd.Parameters.AddWithValue("@p7", rchAdres.Text);
                cmd.Parameters.AddWithValue("@p8", txtID.Text);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Müşteri Güncelleme İşlemi Başarıyla Gerçekleşti.", "Bilgilendirme Penceresi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch(SqlException ex)
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

        private void simpleButton3_Click(object sender, EventArgs e)
        {

            try
            {
                cReena.baglantiKontrol();
                SqlCommand cmd = new SqlCommand("Update Musteriler set Durum=0 where MusteriID=@p1", cReena.con);
                cmd.Parameters.AddWithValue("@p1", txtID.Text);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Müşteri Bilgileri Kaldırıldı", "Bilgilendirme Penceresi", MessageBoxButtons.OK, MessageBoxIcon.Information);

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

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            FrmMusteriSilinen fr = new FrmMusteriSilinen();
            fr.Show();
        }
    }
}
