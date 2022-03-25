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
    public partial class FrmGiderler : Form
    {
        public FrmGiderler()
        {
            InitializeComponent();
        }

        void Giderler()
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("Select * from Giderler order by ID asc", cReena.con);
            da.Fill(dt);
            gridControl1.DataSource = dt;

        }
        void Temizle()
        {
            txtID.Text = "";
            cmbAy.Text = "";
            cmbYil.Text = "";
            txtElektrik.Text = "0";
            txtSu.Text = "0";
            txtInternet.Text = "0";
            txtDogalgaz.Text = "0";
            txtEkstra.Text = "0";
            txtMaas.Text = "0";
            rchNotlar.Text = "";
        }


        private void FrmGiderler_Load(object sender, EventArgs e)
        {
            Giderler();
            Temizle();
        }

        private void btnTemizle_Click(object sender, EventArgs e)
        {
            Temizle();
            Giderler();
        }

        private void btnKaydet_Click(object sender, EventArgs e)
        {
            try
            {
                cReena.baglantiKontrol();
                SqlCommand komut = new SqlCommand("insert into Giderler (Ay,Yil,Elektrik,Su,Dogalgaz,Internet,Maaslar,Ekstra,Notlar) values " +
                   "(@p1,@p2,@p3,@p4,@p5,@p6,@p7,@p8,@p9)", cReena.con);
                komut.Parameters.AddWithValue("@p1", cmbAy.Text);
                komut.Parameters.AddWithValue("@p2", cmbYil.Text);
                komut.Parameters.AddWithValue("@p3", decimal.Parse(txtElektrik.Text));
                komut.Parameters.AddWithValue("@p4", decimal.Parse(txtSu.Text));
                komut.Parameters.AddWithValue("@p5", decimal.Parse(txtDogalgaz.Text));
                komut.Parameters.AddWithValue("@p6", decimal.Parse(txtInternet.Text));
                komut.Parameters.AddWithValue("@p7", decimal.Parse(txtMaas.Text));
                komut.Parameters.AddWithValue("@p8", decimal.Parse(txtEkstra.Text));
                komut.Parameters.AddWithValue("@p9", rchNotlar.Text);
                komut.ExecuteNonQuery();
                cReena.con.Close();
                MessageBox.Show("Gider Kayıt İşleminiz Başarıyla Gerçekleşti", "Bilgilendirme Penceresi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (SqlException ex)
            {
                string hata = ex.Message;
                MessageBox.Show("Veri Tabanıyla Bağlantı Kurulurken Hata Oluştu. Hata Kodu 1", "Hata Penceresi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            finally
            {
                Giderler();
                Temizle();
            }
        }

        private void gridView1_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            DataRow dr = gridView1.GetDataRow(gridView1.FocusedRowHandle);
            if (dr != null)
            {
                cmbAy.Text = dr["Ay"].ToString();
                cmbYil.Text = dr["Yil"].ToString();
                txtDogalgaz.Text = dr["Dogalgaz"].ToString();
                txtEkstra.Text = dr["Ekstra"].ToString();
                txtInternet.Text = dr["Internet"].ToString();
                txtMaas.Text = dr["Maaslar"].ToString();
                txtSu.Text = dr["Su"].ToString();
                txtID.Text = dr["ID"].ToString();
                txtElektrik.Text = dr["Elektrik"].ToString();
                rchNotlar.Text = dr["Notlar"].ToString();
            }
        }

        private void btnGuncelle_Click(object sender, EventArgs e)
        {
            try
            {
                cReena.baglantiKontrol();
                SqlCommand komut = new SqlCommand("update Giderler set Ay=@p1,Yil=@p2,Elektrik=@p3,Su=@p4,Dogalgaz=@p5,Internet=@p6,Maaslar=@p7,Ekstra=@p8,Notlar=@p9 where " +
                    "ID=@p10", cReena.con);
                komut.Parameters.AddWithValue("@p1", cmbAy.Text);
                komut.Parameters.AddWithValue("@p2", cmbYil.Text);
                komut.Parameters.AddWithValue("@p3", decimal.Parse(txtElektrik.Text));
                komut.Parameters.AddWithValue("@p4", decimal.Parse(txtSu.Text));
                komut.Parameters.AddWithValue("@p5", decimal.Parse(txtDogalgaz.Text));
                komut.Parameters.AddWithValue("@p6", decimal.Parse(txtInternet.Text));
                komut.Parameters.AddWithValue("@p7", decimal.Parse(txtMaas.Text));
                komut.Parameters.AddWithValue("@p8", decimal.Parse(txtEkstra.Text));
                komut.Parameters.AddWithValue("@p9", rchNotlar.Text);
                komut.Parameters.AddWithValue("@p10", txtID.Text);
                komut.ExecuteNonQuery();
                cReena.con.Close();
                MessageBox.Show("Güncelleme İşlemi Başarıyla Gerçekleştirildi.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
            catch (SqlException ex)
            {
                string hata = ex.Message;
                MessageBox.Show("Veri Tabanıyla Bağlantı Kurulurken Hata Oluştu. Hata Kodu 1", "Hata Penceresi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {

                Temizle();
                Giderler();
            }
        }

        private void cmbToplamYil_SelectedIndexChanged(object sender, EventArgs e)
        {
            cmbToplamAy.Text = "";
        }

        private void cmbToplamAy_SelectedIndexChanged(object sender, EventArgs e)
        {
            cReena.baglantiKontrol();
            SqlCommand komut = new SqlCommand("select sum(Dogalgaz+Elektrik+Su+Internet+Maaslar+Ekstra) from Giderler where Ay=@p1 and Yil=@p2", cReena.con);
            komut.Parameters.AddWithValue("@p1", cmbToplamAy.Text);
            komut.Parameters.AddWithValue("@p2", cmbToplamYil.Text);
            SqlDataReader dr = komut.ExecuteReader();
            while (dr.Read())
            {
                labelGider.Text = dr[0].ToString() + " ₺";
            }
        }
    }
}
