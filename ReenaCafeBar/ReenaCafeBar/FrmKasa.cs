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
    public partial class FrmKasa : Form
    {
        public FrmKasa()
        {
            InitializeComponent();
        }

        void ToplamKasa()
        {
            cReena.baglantiKontrol();
            SqlCommand cmd = new SqlCommand("Select sum(Toplam) from Hareketler", cReena.con);
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                lblKasaToplam.Text = dr[0].ToString() + " TL";
            }
        }

        void Odemeler()
        {
            cReena.baglantiKontrol();
            SqlCommand cmd = new SqlCommand("select (Elektrik+Su+DogalGaz+Internet+Ekstra) from Giderler order by ID asc", cReena.con);
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                lblOdemeler.Text = dr[0].ToString() + " TL";
            }
        }

        void Maaslar()
        {
            cReena.baglantiKontrol();
            SqlCommand cmd = new SqlCommand("select Maaslar from Giderler order by ID asc", cReena.con);
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                lblMaaslar.Text = dr[0].ToString() + " TL";
            }
        }

        void MusteriSayi()
        {
            cReena.baglantiKontrol();
            SqlCommand cmd = new SqlCommand("Select count(*) from Musteriler where Durum=1", cReena.con);
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                lblMusteriSayisi.Text = dr[0].ToString();
            }
        }

        void ToplamPersonel()
        {
            cReena.baglantiKontrol();
            SqlCommand cmd = new SqlCommand("select count(*) from personeller where Durum=1", cReena.con);
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                lblPersonelSayi.Text = dr[0].ToString();
            }
            cReena.con.Close();
        }

        void FirmaSayi()
        {
            cReena.baglantiKontrol();
            SqlCommand cmd = new SqlCommand("select count(*) from Firmalar", cReena.con);
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                lblFirmaSayi.Text = dr[0].ToString();
            }
            cReena.con.Close();

        }

        void ToplamStok()
        {

            cReena.baglantiKontrol();
            SqlCommand cmd = new SqlCommand("select sum(stok) from Urunler", cReena.con);
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                lblStokSayi.Text = dr[0].ToString();
            }
            cReena.con.Close();
        }

        void Listele()
        {
            cReena.baglantiKontrol();
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("Exec HareketList", cReena.con);
            da.Fill(dt);
            gridControl1.DataSource = dt;
            ToplamKasa();
            Odemeler();
            Maaslar();
            MusteriSayi();
            ToplamPersonel();
            FirmaSayi();
            ToplamStok();
        }


        private void FrmKasa_Load(object sender, EventArgs e)
        {
            Listele();
        }
    }
}
