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
    public partial class FrmHareketler : Form
    {
        public FrmHareketler()
        {
            InitializeComponent();
        }

        void Listele()
        {
            cReena.baglantiKontrol();
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("Exec HareketList", cReena.con);
            da.Fill(dt);
            gridControl1.DataSource = dt;
        }

        private void FrmHareketler_Load(object sender, EventArgs e)
        {
            Listele();
        }
    }
}
