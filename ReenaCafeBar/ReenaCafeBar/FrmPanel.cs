using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ReenaCafeBar
{
    public partial class FrmPanel : Form
    {
        public FrmPanel()
        {
            InitializeComponent();
        }

        public string AktifKullanici;
        private void FrmPanel_Load(object sender, EventArgs e)
        {
            this.Text = "Panel ---- Aktif Kullanıcı: " + AktifKullanici;

            if (fr13 == null || fr13.IsDisposed)
            {
                fr13 = new FrmAnaSayfa();
                fr13.MdiParent = this;
                fr13.Show();
            }
        }

        FrmUrunler fr1;
        private void barButtonItem2_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (fr1 == null || fr1.IsDisposed)
            {
                fr1 = new FrmUrunler();
                fr1.MdiParent = this;
                fr1.Show();
            }

        }

        FrmStoklar fr2;
        private void barButtonItem3_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (fr2 == null || fr2.IsDisposed)
            {
                fr2 = new FrmStoklar();
                fr2.MdiParent = this;
                fr2.Show();
            }
        }

        FrmMusteriler fr3;
        private void barButtonItem4_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (fr3 == null || fr3.IsDisposed)
            {
                fr3 = new FrmMusteriler();
                fr3.MdiParent = this;
                fr3.Show();
            }
        }

        private void barButtonItem5_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            FrmMenu fr = new FrmMenu();
            fr.AktifKullanici = AktifKullanici;
            fr.Show();
            this.Close();
        }

        FrmPersoneller fr4;
        private void barButtonItem8_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (fr4 == null || fr4.IsDisposed)
            {
                fr4 = new FrmPersoneller();
                fr4.MdiParent = this;
                fr4.Show();
            }
        }

        FrmFirmalar fr5;
        private void barButtonItem10_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (fr5 == null || fr5.IsDisposed)
            {
                fr5 = new FrmFirmalar();
                fr5.MdiParent = this;
                fr5.Show();
            }
        }

        FrmBankalar fr6;
        private void barButtonItem11_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (fr6 == null || fr6.IsDisposed)
            {
                fr6 = new FrmBankalar();
                fr6.MdiParent = this;
                fr6.Show();
            }
        }

        FrmNotlar fr7;
        private void barButtonItem12_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (fr7 == null || fr7.IsDisposed)
            {
                fr7 = new FrmNotlar();
                fr7.MdiParent = this;
                fr7.Show();
            }
        }

        FrmRehber fr8;
        private void barButtonItem13_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (fr8 == null || fr8.IsDisposed)
            {
                fr8 = new FrmRehber();
                fr8.MdiParent = this;
                fr8.Show();
            }
        }

        FrmGiderler fr9;
        private void barButtonItem14_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (fr9 == null || fr9.IsDisposed)
            {
                fr9 = new FrmGiderler();
                fr9.MdiParent = this;
                fr9.Show();
            }
        }

        FrmRaporlar fr10;
        private void barButtonItem15_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (fr10 == null || fr10.IsDisposed)
            {
                fr10 = new FrmRaporlar();
                fr10.MdiParent = this;
                fr10.Show();
            }
        }

        
        private void barButtonItem16_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            FrmAyarlar fr = new FrmAyarlar();
            fr.AktifKullanici = AktifKullanici;
            fr.Show();
        }


        FrmHareketler fr11;
        private void barButtonItem17_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (fr11 == null || fr11.IsDisposed)
            {
                fr11 = new FrmHareketler();
                fr11.MdiParent = this;
                fr11.Show();
            }
        }

        FrmKasa fr12;
        private void barButtonItem18_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (fr12 == null || fr12.IsDisposed)
            {
                fr12 = new FrmKasa();
                fr12.MdiParent = this;
                fr12.Show();
            }
        }


        FrmAnaSayfa fr13;
        private void barButtonItem1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (fr13 == null || fr13.IsDisposed)
            {
                fr13 = new FrmAnaSayfa();
                fr13.MdiParent = this;
                fr13.Show();
            }
        }
    }
}
