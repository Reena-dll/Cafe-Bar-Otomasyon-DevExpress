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
    public partial class FrmRaporlar : Form
    {
        public FrmRaporlar()
        {
            InitializeComponent();
        }

        private void FrmRaporlar_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'ReenaCafeBarDataSet.UrunGetir' table. You can move, or remove it, as needed.
            this.UrunGetirTableAdapter.Fill(this.ReenaCafeBarDataSet.UrunGetir);
            // TODO: This line of code loads data into the 'ReenaCafeBarDataSet.Bankalar' table. You can move, or remove it, as needed.
            this.BankalarTableAdapter.Fill(this.ReenaCafeBarDataSet.Bankalar);
            // TODO: This line of code loads data into the 'ReenaCafeBarDataSet.Firmalar' table. You can move, or remove it, as needed.
            this.FirmalarTableAdapter.Fill(this.ReenaCafeBarDataSet.Firmalar);
            // TODO: This line of code loads data into the 'ReenaCafeBarDataSet.Personeller' table. You can move, or remove it, as needed.
            this.PersonellerTableAdapter.Fill(this.ReenaCafeBarDataSet.Personeller);
            // TODO: This line of code loads data into the 'ReenaCafeBarDataSet.Musteriler' table. You can move, or remove it, as needed.
            this.MusterilerTableAdapter.Fill(this.ReenaCafeBarDataSet.Musteriler);

            this.reportViewer6.RefreshReport();
            this.reportViewer7.RefreshReport();
            this.reportViewer8.RefreshReport();
            this.reportViewer9.RefreshReport();
            this.reportViewer10.RefreshReport();
        }
    }
}
