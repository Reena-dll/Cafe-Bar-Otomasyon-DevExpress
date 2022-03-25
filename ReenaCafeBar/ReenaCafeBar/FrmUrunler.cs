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
using System.Management.Instrumentation;

namespace ReenaCafeBar
{
    public partial class FrmUrunler : Form
    {
        public FrmUrunler()
        {
            InitializeComponent();
        }
        public static FrmUrunler fr;


        void ComboBoxDoldur()
        {
            SqlDataAdapter da = new SqlDataAdapter("select * from UrunKategori", cReena.con);
            DataTable dt = new DataTable();
            da.Fill(dt);
            cmbKategori.DataSource = dt;
            cmbKategori.DisplayMember = "KategoriAd";
            cmbKategori.ValueMember = "KategoriID";

        }

        public void Listele()
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("execute UrunGetir", cReena.con);
            da.Fill(dt);
            gridControl1.DataSource = dt;
            ComboBoxDoldur();
        }

        public void Temizle()
        {
            txtAd.Text = "";
            txtID.Text = "";
            txtGelis.Text = "";
            txtSatis.Text = "";
            cmbKategori.Text = "";
            nudStok.Value = 0;
            rchDetay.Text = "";

        }

        private void FrmUrunler_Load(object sender, EventArgs e)
        {
            fr = this;
            Listele();
            Temizle();
        }

        private void gridView1_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            DataRow dr = gridView1.GetDataRow(gridView1.FocusedRowHandle);

            if (dr!=null)
            {
                txtID.Text = dr["UrunID"].ToString();
                txtAd.Text = dr["UrunAd"].ToString();
                cmbKategori.Text = dr["KategoriAd"].ToString();
                nudStok.Value = Convert.ToInt32(dr["Stok"]);
                txtGelis.Text = dr["GelisFiyat"].ToString();
                txtSatis.Text = dr["SatisFiyat"].ToString();
                rchDetay.Text = dr["UrunDetay"].ToString();
            }
        }

        private void btnTemizle_Click(object sender, EventArgs e)
        {
            Temizle();
        }

        private void btnGuncelle_Click(object sender, EventArgs e)
        {
            // Güncelleme butonu

            try
            {
                cReena.baglantiKontrol();
                SqlCommand cmd = new SqlCommand("Update Urunler set UrunAd=@p1, UrunKategori=@p2,Stok=@p3,GelisFiyat=@p4,SatisFiyat=@p5,UrunDetay=@p6 where UrunID=@p7", cReena.con);
                cmd.Parameters.AddWithValue("@p1", txtAd.Text);
                cmd.Parameters.AddWithValue("@p2", cmbKategori.SelectedValue);
                cmd.Parameters.AddWithValue("@p3", nudStok.Value);
                cmd.Parameters.AddWithValue("@p4", Convert.ToDecimal(txtGelis.Text));
                cmd.Parameters.AddWithValue("@p5", Convert.ToDecimal(txtSatis.Text));
                cmd.Parameters.AddWithValue("@p6", rchDetay.Text);
                cmd.Parameters.AddWithValue("@p7", txtID.Text);
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

        private void btnKaydet_Click(object sender, EventArgs e)
        {
            // Kaydetme Butonu
            try
            {
                cReena.baglantiKontrol();
                SqlCommand cmd = new SqlCommand("insert into Urunler (UrunAd,UrunKategori,Stok,GelisFiyat,SatisFiyat,UrunDetay) values (@p1,@p2,@p3,@p4,@p5,@p6)", cReena.con);
                cmd.Parameters.AddWithValue("@p1", txtAd.Text);
                cmd.Parameters.AddWithValue("@p2", cmbKategori.SelectedValue);
                cmd.Parameters.AddWithValue("@p3", nudStok.Value);
                cmd.Parameters.AddWithValue("@p4", Convert.ToDecimal(txtGelis.Text));
                cmd.Parameters.AddWithValue("@p5", Convert.ToDecimal(txtSatis.Text));
                cmd.Parameters.AddWithValue("@p6", rchDetay.Text);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Ekleme İşlemi Başarıyla Gerçekleşti.", "Bilgilendirme Penceresi", MessageBoxButtons.OK, MessageBoxIcon.Information);
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

        private void btnUrunKaldir_Click(object sender, EventArgs e)
        {
            try
            {
                cReena.baglantiKontrol();
                SqlCommand cmd = new SqlCommand("Update Urunler set Durum=0 where UrunID=@p1", cReena.con);
                cmd.Parameters.AddWithValue("@p1", txtID.Text);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Kaldırma İşlemi Başarıyla Gerçekleşti.", "Bilgilendirme Penceresi", MessageBoxButtons.OK, MessageBoxIcon.Information);
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

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            FrmKategoriler fr = new FrmKategoriler();
            fr.Show();
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            FrmKaldirilanUrun fr = new FrmKaldirilanUrun();
            fr.Show();
        }
    }
}
