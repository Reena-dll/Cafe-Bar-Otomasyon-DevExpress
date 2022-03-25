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
using System.Xml;

namespace ReenaCafeBar
{
    public partial class FrmAnaSayfa : Form
    {
        public FrmAnaSayfa()
        {
            InitializeComponent();
        }

        void Stoklar()
        {

            SqlDataAdapter da = new SqlDataAdapter("Select UrunAd, SUM(Stok) as Adet from Urunler where Durum=1  group by UrunAd having  sum(Stok)<=15 order by SUM(Stok)", cReena.con);
            DataTable dt = new DataTable();
            da.Fill(dt);
            gridControlStok.DataSource = dt;
        }

        void SonHareket()
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("exec AnaSayfaHareket", cReena.con);
            da.Fill(dt);
            gridControlHareket.DataSource = dt;
        }
        void Notlar()
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("select top 25 Tarih,Saat,Baslik from notlar order by NotID desc", cReena.con);
            da.Fill(dt);
            gridcontrolnotlar.DataSource = dt;
        }
        void Haberler()
        {
            XmlTextReader xmloku = new XmlTextReader("https://www.haberturk.com/rss");
            while (xmloku.Read())
            {
                if (xmloku.Name == "title")
                {
                    listBox1.Items.Add(xmloku.ReadString());
                }
            }
        }

        private void FrmAnaSayfa_Load(object sender, EventArgs e)
        {
            Stoklar();
            SonHareket();
            Notlar();
            webBrowser1.Navigate("https://www.tcmb.gov.tr/kurlar/today.xml");
            Haberler();
        }
    }
}
