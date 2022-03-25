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
    public partial class FrmBankalar : Form
    {
        public FrmBankalar()
        {
            InitializeComponent();
        }

        void Temizle()
        {
            txtAd.Text = "";
            txtHesapNo.Text = "";
            txtiban.Text = "";
            txtID.Text = "";
            txtSube.Text = "";
            txtYetkili.Text = "";
            cmbilce.Text = "";
            cmbSehir.Text = "";
            mskTel.Text = "";
        }

        void Listele()
        {
            cReena.baglantiKontrol();
            SqlCommand cmd = new SqlCommand("select * from Bankalar", cReena.con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            gridControl1.DataSource = dt;

        }


        private void FrmBankalar_Load(object sender, EventArgs e)
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
                SqlCommand cmd = new SqlCommand("insert into Bankalar (BankaAdi,Sehir,Ilce,Sube,IBAN,HesapNo,Yetkili,Telefon) values (@p1,@p2,@p3,@p4,@p5,@p6,@p7,@p8)", cReena.con);
                cmd.Parameters.AddWithValue("@p1", txtAd.Text);
                cmd.Parameters.AddWithValue("@p2", cmbSehir.Text);
                cmd.Parameters.AddWithValue("@p3", cmbilce.Text);
                cmd.Parameters.AddWithValue("@p4", txtSube.Text);
                cmd.Parameters.AddWithValue("@p5", txtiban.Text);
                cmd.Parameters.AddWithValue("@p6", txtHesapNo.Text);
                cmd.Parameters.AddWithValue("@p7", txtYetkili.Text);
                cmd.Parameters.AddWithValue("@p8", mskTel.Text);
                cmd.ExecuteNonQuery();
                cReena.con.Close();
                MessageBox.Show("Banka Hesabı Ekleme İşlemi Başarıyla Gerçekleşti.", "Bilgilendirme Penceresi", MessageBoxButtons.OK, MessageBoxIcon.Information);

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
                txtID.Text = dr["BankaID"].ToString();
                txtAd.Text = dr["BankaAdi"].ToString();
                txtHesapNo.Text = dr["HesapNo"].ToString();
                txtiban.Text = dr["IBAN"].ToString();
                txtSube.Text = dr["Sube"].ToString();
                cmbilce.Text = dr["Ilce"].ToString();
                cmbSehir.Text = dr["Sehir"].ToString();
                txtYetkili.Text = dr["Yetkili"].ToString();
                mskTel.Text = dr["Telefon"].ToString();
            }
        }

        private void btnSil_Click(object sender, EventArgs e)
        {
            DialogResult secenek = MessageBox.Show("Banka Hesap Bilgilerini Silmek İstiyor Musunuz? \nBu işlem sonucunda hesap bilgilerine daha ulaşılamaz.", "Bilgilendirme Penceresi", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
            if (secenek == DialogResult.Yes)
            {
                try
                {
                    cReena.baglantiKontrol();
                    SqlCommand cmd = new SqlCommand("delete from Bankalar where BankaID=@p1", cReena.con);
                    cmd.Parameters.AddWithValue("@p1", txtID.Text);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Banka Hesabı Başarıyla Silindi.", "Bilgilendirme Penceresi", MessageBoxButtons.OK, MessageBoxIcon.Information);
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

        private void btnGuncelle_Click(object sender, EventArgs e)
        {
            try
            {
                cReena.baglantiKontrol();
                SqlCommand cmd = new SqlCommand("Update Bankalar set BankaAdi=@p1, Sehir=@p2, Ilce=@p3,Sube=@p4,IBAN=@p5,HesapNo=@p6,Yetkili=@p7,Telefon=@p8 where BankaID=@p9", cReena.con);
                cmd.Parameters.AddWithValue("@p1", txtAd.Text);
                cmd.Parameters.AddWithValue("@p2", cmbSehir.Text);
                cmd.Parameters.AddWithValue("@p3", cmbilce.Text);
                cmd.Parameters.AddWithValue("@p4", txtSube.Text);
                cmd.Parameters.AddWithValue("@p5", txtiban.Text);
                cmd.Parameters.AddWithValue("@p6", txtHesapNo.Text);
                cmd.Parameters.AddWithValue("@p7", txtYetkili.Text);
                cmd.Parameters.AddWithValue("@p8", mskTel.Text);
                cmd.Parameters.AddWithValue("@p9", txtID.Text);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Banka Hesabı Başarıyla Güncellendi.", "Bilgilendirme Penceresi", MessageBoxButtons.OK, MessageBoxIcon.Information);
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

