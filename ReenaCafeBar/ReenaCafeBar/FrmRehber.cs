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
    public partial class FrmRehber : Form
    {
        public FrmRehber()
        {
            InitializeComponent();
        }
        void MusteriList()
        {
            cReena.baglantiKontrol();
            SqlDataAdapter da = new SqlDataAdapter("select MusteriAd+' '+MusteriSoyad as Müşteri, Telefon, Mail, Sehir,Ilce  from musteriler order by Müşteri", cReena.con);
            DataTable dt = new DataTable();
            da.Fill(dt);
            gridControl1.DataSource = dt;
        }

        void FirmaList()
        {
            cReena.baglantiKontrol();
            SqlDataAdapter da = new SqlDataAdapter("select FirmaAd, YetkiliAdSoyad, YetkiliStatu, Telefon, Mail from Firmalar", cReena.con);
            DataTable dt = new DataTable();
            da.Fill(dt);
            gridControl2.DataSource = dt;

        }

        void PersonelList()
        {
            cReena.baglantiKontrol();
            SqlDataAdapter da = new SqlDataAdapter("select PersonelAd+' ' + PersonelSoyad as Personel, Telefon,Mail,Sehir,Ilce,PersonelRutbe.Rutbe from Personeller inner join PersonelRutbe on Personeller.Rutbe = PersonelRutbe.RutbeID order by RutbeID", cReena.con);
            DataTable dt = new DataTable();
            da.Fill(dt);
            gridControl3.DataSource = dt;


        }
        private void FrmRehber_Load(object sender, EventArgs e)
        {
            MusteriList();
            FirmaList();
            PersonelList();
        }

        private void gridView1_DoubleClick(object sender, EventArgs e)
        {
            FrmMail fr = new FrmMail();
            DataRow dr = gridView1.GetDataRow(gridView1.FocusedRowHandle);
            if (dr != null)
            {
                fr.mailAdres = dr["Mail"].ToString();
            }
            fr.Show();
        }

        private void gridView2_DoubleClick(object sender, EventArgs e)
        {
            FrmMail fr = new FrmMail();
            DataRow dr = gridView2.GetDataRow(gridView2.FocusedRowHandle);
            if (dr != null)
            {
                fr.mailAdres = dr["Mail"].ToString();
            }
            fr.Show();
        }

        private void gridView3_DoubleClick(object sender, EventArgs e)
        {
            FrmMail fr = new FrmMail();
            DataRow dr = gridView3.GetDataRow(gridView3.FocusedRowHandle);
            if (dr != null)
            {
                fr.mailAdres = dr["Mail"].ToString();
            }
            fr.Show();
        }
    }
}
