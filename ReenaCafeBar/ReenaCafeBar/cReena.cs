using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace ReenaCafeBar
{
    class cReena
    {
        public static SqlConnection con = new SqlConnection("Data Source=localhost;Initial Catalog=ReenaCafeBar;Integrated Security=True; MultipleActiveResultSets=True");

        public static void baglantiKontrol()
        {
            if (con.State == ConnectionState.Closed)
            {
                try
                {
                    con.Open();
                }
                catch (SqlException ex)
                {
                    string hata = ex.Message;
                    MessageBox.Show("Veri Tabanı Bağlantısı Yapılamadı","Bilgilendirme Penceresi",MessageBoxButtons.OK,MessageBoxIcon.Warning);
                    Application.Exit();
                }

            }
        }
        public static bool veriKontrol(string sql)
        {
            cReena.baglantiKontrol();
            SqlCommand cmd = new SqlCommand(sql, cReena.con);
            SqlDataReader dr = cmd.ExecuteReader();
            if (dr.Read())
            {
                return true;

            }
            else
            {
                return false;

            }
        }

        public static bool masaNoGetir(object masaNo)
        {
            try
            {

                cReena.baglantiKontrol();
                SqlCommand cmd2 = new SqlCommand("Update Masalar set Durum=0 where MasaID=" + masaNo, cReena.con);
                cmd2.ExecuteNonQuery();
                return true;
            }
            catch
            {
                return false;
            }


        }
    }
}
