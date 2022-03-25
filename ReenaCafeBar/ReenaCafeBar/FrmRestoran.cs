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
    public partial class FrmRestoran : Form
    {
        public FrmRestoran()
        {
            InitializeComponent();
        }

        int DoluMasaSayisi = 0;
        public static int MasaSayisi;
        public static int MasaNumarasi;
        public static FrmRestoran Restoran;
        public string AktifKullanici;

        public void Listele()
        {
            cReena.baglantiKontrol();
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("select count(*) as MasaSayi from masalar", cReena.con);
            da.Fill(dt);
            for (int i = 0; i < int.Parse(dt.Rows[0]["MasaSayi"].ToString()); i++)
            {
                if (cReena.veriKontrol("select * from Masalar where MasaID=" + (i + 1) + "And Durum=1"))
                {
                    lstMasalar.Items.Add((i + 1) + " .Masa");
                    lstMasalar.Items[i].ImageKey = "dolu.png";
                    DoluMasaSayisi++;
                }
                else
                {
                    lstMasalar.Items.Add((i + 1) + " .Masa");
                    lstMasalar.Items[i].ImageKey = "bos.png";
                }
            }
            MasaSayisi = Convert.ToInt32(dt.Rows[0]["MasaSayi"].ToString());
        }

        public void Temizle()
        {
            lstMasalar.Clear();
        }
        private void FrmRestoran_Load(object sender, EventArgs e)
        {
            Restoran = this;
            Temizle();
            Listele();

        }


        private void barButtonItem1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            FrmMenu fr = new FrmMenu();
            fr.AktifKullanici = AktifKullanici;
            fr.Show();
            this.Hide();
        }

        private void barButtonItem2_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Temizle();
            Listele();
        }

        private void barButtonItem4_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            int guncelmasa = 0;
            cReena.baglantiKontrol();
            SqlCommand cmd = new SqlCommand("select count(*) from Masalar where Durum=1", cReena.con);
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                guncelmasa = int.Parse(dr[0].ToString());
            }
            MessageBox.Show("Toplam Masa Sayısı: " + MasaSayisi + "\n  Dolu Masa Sayısı: " + guncelmasa + " \n Boş Masa Sayısı: " + (MasaSayisi - guncelmasa).ToString(), "Bilgilendirme Penceresi", MessageBoxButtons.OK, MessageBoxIcon.Information);

        }

        private void barButtonItem6_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            cReena.baglantiKontrol();
            SqlCommand cmd = new SqlCommand("exec MasaArttır", cReena.con);
            cmd.ExecuteNonQuery();
            Temizle();
            Listele();
        }

        private void masayıKapatToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (lstMasalar.SelectedItems.Count > 0 && lstMasalar.SelectedItems[0].ImageKey == "dolu.png")
            {
                int masaNo = Convert.ToInt32(lstMasalar.SelectedItems[0].Text.Substring(0, lstMasalar.SelectedItems[0].Text.IndexOf('.')));

                if (MessageBox.Show("Masayı Kapatmak İstiyor musunuz ? Eğer masayı kapatırsanız içerisinde ki siparişler silinecektir." +

                    "\nDevam etmek istiyor musunuz?", "Uyarı", MessageBoxButtons.YesNo,
                    MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
                    cReena.masaNoGetir(masaNo);

                SqlCommand cmd = new SqlCommand("Delete from MasaSiparis where MasaID=@p1", cReena.con);
                cmd.Parameters.AddWithValue("@p1", FrmRestoran.MasaNumarasi);
                cmd.ExecuteNonQuery();

                Temizle();
                Listele();

            }
            else
            {
                MessageBox.Show("Masa Zaten Açılmamış...");
            }
        }

        private void lstMasalar_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lstMasalar.SelectedItems.Count > 0)
            {
                int masaNo = Convert.ToInt32(lstMasalar.SelectedItems[0].Text.Substring(0, lstMasalar.SelectedItems[0].Text.IndexOf('.')));

            }
        }

        private void lstMasalar_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                if (lstMasalar.FocusedItem.Bounds.Contains(e.Location) == true)
                {
                    contextMenuStrip1.Show(Cursor.Position);
                }
            }
        }

        private void yenileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Temizle();
            Listele();
        }

        private void lstMasalar_DoubleClick(object sender, EventArgs e)
        {
            MasaNumarasi = (int.Parse(lstMasalar.SelectedIndices[0].ToString()) + 1);
            if (lstMasalar.SelectedItems.Count > 0 && lstMasalar.SelectedItems[0].ImageKey == "bos.png")
            {
                DialogResult secenek = MessageBox.Show("Masayı Açmak İstiyor Musunuz ?", "Bilgilendirme Penceresi", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                if (secenek == DialogResult.Yes)
                {
                    cReena.baglantiKontrol();
                    SqlCommand cmd = new SqlCommand("update Masalar set Durum=1 where MasaID=@p1", cReena.con);
                    cmd.Parameters.AddWithValue("@p1", MasaNumarasi);
                    cmd.ExecuteNonQuery();
                    Temizle();
                    Listele();

                    KafeSiparisMasa fr = new KafeSiparisMasa();
                    fr.ShowDialog();

                }
            }
            else
            {
                KafeSiparisMasa fr = new KafeSiparisMasa();
                fr.ShowDialog();
            }
        }

        private void barButtonItem7_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            FrmUrunler fr = new FrmUrunler();
            fr.Show();
        }

        private void barButtonItem8_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            FrmMusteriler fr = new FrmMusteriler();
            fr.Show();
        }

        private void barButtonItem9_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            FrmMutfak fr = new FrmMutfak();
            fr.Show();
        }

        private void barButtonItem10_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            FrmNotlar fr = new FrmNotlar();
            fr.Show();
        }

        private void bunifuCards1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
