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
    public partial class FrmNotlar : Form
    {
        public FrmNotlar()
        {
            InitializeComponent();
        }

        void PersonelList()
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("select PersonelID, PersonelAd+' '+PersonelSoyad as Olusturan from Personeller inner join PersonelRutbe on Personeller.Rutbe=PersonelRutbe.RutbeID order by RutbeID", cReena.con);
            da.Fill(dt);
            cmbPersonel.ValueMember = "PersonelID";
            cmbPersonel.DisplayMember = "Olusturan";
            cmbPersonel.DataSource = dt;
        }

        void Listele()
        {
            cReena.baglantiKontrol();
            SqlDataAdapter da = new SqlDataAdapter("exec NotList", cReena.con);
            DataTable dt = new DataTable();
            da.Fill(dt);
            gridControl1.DataSource = dt;
            PersonelList();

        }

        void Temizle()
        {
            txtID.Text = "";
            txtBaslik.Text = "";
            txtHitap.Text = "";
            cmbPersonel.Text = "Seçiniz";
            mskSaat.Text = "";
            mskTarih.Text = "";
            rchMesaj.Text = "";
        }

        private void FrmNotlar_Load(object sender, EventArgs e)
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
                SqlCommand cmd = new SqlCommand("insert into Notlar (Olusturan,Hitap,Tarih,Saat,Baslik,Mesaj) values (@p1,@p2,@p3,@p4,@p5,@p6)", cReena.con);
                cmd.Parameters.AddWithValue("@p1", cmbPersonel.SelectedValue);
                cmd.Parameters.AddWithValue("@p2", txtHitap.Text);
                cmd.Parameters.AddWithValue("@p3", mskTarih.Text);
                cmd.Parameters.AddWithValue("@p4", mskSaat.Text);
                cmd.Parameters.AddWithValue("@p5", txtBaslik.Text);
                cmd.Parameters.AddWithValue("@p6", rchMesaj.Text);
                cmd.ExecuteNonQuery();
                cReena.con.Close();
                MessageBox.Show("Müşteri Ekleme İşlemi Başarıyla Gerçekleşti.", "Bilgilendirme Penceresi", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
                txtID.Text = dr["NotID"].ToString();
                cmbPersonel.Text = dr["Olusturan"].ToString();
                txtHitap.Text = dr["Hitap"].ToString();
                mskTarih.Text = dr["Tarih"].ToString();
                mskSaat.Text = dr["Saat"].ToString();
                txtBaslik.Text = dr["Baslik"].ToString();
                rchMesaj.Text = dr["Mesaj"].ToString();
            }
        }

        private void btnSil_Click(object sender, EventArgs e)
        {
            DialogResult secenek = MessageBox.Show("Notu Silmek İstiyor Musunuz? \nBu işlem sonucunda not bilgilerine daha ulaşılamaz.", "Bilgilendirme Penceresi", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
            if (secenek == DialogResult.Yes)
            {
                try
                {
                    cReena.baglantiKontrol();
                    SqlCommand cmd = new SqlCommand("Delete From Notlar where NotID=@p1", cReena.con);
                    cmd.Parameters.AddWithValue("@p1", txtID.Text);
                    cmd.ExecuteNonQuery();
                    cReena.con.Close();
                    MessageBox.Show("Not Başarıyla Silindi.", "Bilgilendirme Penceresi", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
                SqlCommand cmd = new SqlCommand("update Notlar set Olusturan=@p1, Hitap=@p2,Tarih=@p3,Saat=@p4,Baslik=@p5,Mesaj=@p6 where NotID=@p7", cReena.con);
                cmd.Parameters.AddWithValue("@p1", cmbPersonel.SelectedValue);
                cmd.Parameters.AddWithValue("@p2", txtHitap.Text);
                cmd.Parameters.AddWithValue("@p3", mskTarih.Text);
                cmd.Parameters.AddWithValue("@p4", mskSaat.Text);
                cmd.Parameters.AddWithValue("@p5", txtBaslik.Text);
                cmd.Parameters.AddWithValue("@p6", rchMesaj.Text);
                cmd.Parameters.AddWithValue("@p7", txtID.Text);
                cmd.ExecuteNonQuery();
                cReena.con.Close();
                MessageBox.Show("Not Başarıyla Güncellendi.", "Bilgilendirme Penceresi", MessageBoxButtons.OK, MessageBoxIcon.Information);
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

        private void gridControl1_DoubleClick(object sender, EventArgs e)
        {

        }

        private void gridView1_DoubleClick(object sender, EventArgs e)
        {
            FrmNotDetay nd = new FrmNotDetay();
            DataRow dr = gridView1.GetDataRow(gridView1.FocusedRowHandle);
            if (dr != null)
            {
                nd.mesaj = dr["Mesaj"].ToString();
            }
            nd.Show();
        }
    }
}
