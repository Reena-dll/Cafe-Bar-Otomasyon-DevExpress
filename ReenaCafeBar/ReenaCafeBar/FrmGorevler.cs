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
    public partial class FrmGorevler : Form
    {
        public FrmGorevler()
        {
            InitializeComponent();
        }

        private void lblcikis_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        void Listele()
        {
            SqlDataAdapter da = new SqlDataAdapter("Select RutbeID,Rutbe from PersonelRutbe", cReena.con);
            DataTable dt = new DataTable();
            da.Fill(dt);
            gridControl2.DataSource = dt;
        }

        void Temizle()
        {
            txtGorevAdi.Text = "";
            txtGorevID.Text = "";
        }

        private void FrmGorevler_Load(object sender, EventArgs e)
        {
            Listele();
        }

        private void gridView2_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            DataRow dr = gridView2.GetDataRow(gridView2.FocusedRowHandle);
            if (dr != null)
            {
                txtGorevID.Text = dr["RutbeID"].ToString();
                txtGorevAdi.Text = dr["Rutbe"].ToString();
            }
        }

        private void btnKaydet_Click(object sender, EventArgs e)
        {
            try
            {
                cReena.baglantiKontrol();
                SqlCommand cmd = new SqlCommand("insert into PersonelRutbe (Rutbe) values (@p1)", cReena.con);
                cmd.Parameters.AddWithValue("@p1", txtGorevAdi.Text);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Yeni Görev Ekleme İşlemi Başarıyla Gerçekleşti.", "Bilgilendirme Penceresi", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
                FrmPersoneller.personel.Listele();
                FrmPersoneller.personel.Temizle();

            }

        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            Temizle();
        }

        private void btnGuncelle_Click(object sender, EventArgs e)
        {
            if (txtGorevID.Text== "1" || txtGorevID.Text == "2" || txtGorevID.Text== "3" || txtGorevID.Text=="10")
            {
                MessageBox.Show("Dokunulmazlığı Olan Kategori. Yetkililer Değiştirilemez. Hata Kodu 2", "Hata Penceresi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                try
                {
                    cReena.baglantiKontrol();
                    SqlCommand cmd = new SqlCommand("update PersonelRutbe set Rutbe=@p1 where RutbeID=@p2", cReena.con);
                    cmd.Parameters.AddWithValue("@p1", txtGorevAdi.Text);
                    cmd.Parameters.AddWithValue("@p2", txtGorevID.Text);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Görev Güncelleme İşlemi Başarıyla Gerçekleşti.", "Bilgilendirme Penceresi", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
                    FrmPersoneller.personel.Listele();
                    FrmPersoneller.personel.Temizle();

                }
            }
           
        }
    }
}
