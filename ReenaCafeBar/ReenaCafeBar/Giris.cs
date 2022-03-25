using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ReenaCafeBar
{
    public partial class Giris : Form
    {
        public Giris()
        {
            InitializeComponent();
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            if (txtSifre.isPassword == true)
            {
                txtSifre.isPassword = false;
            }
            else if (txtSifre.isPassword == false)
            {
                txtSifre.isPassword = true;
            }
        }

        private void lblcikis_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Uygulamayı Kapatmak İstiyor Musunuz ?", "Bilgilendirme Penceresi", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
            {
                Application.Exit();
            }
        }

        private void btnGirisYap_Click(object sender, EventArgs e)
        {
            if (txtKullaniciAdi.Text.Trim() != "" && txtSifre.Text.Trim() != "")
            {

                cReena.baglantiKontrol();
                SqlCommand cmd2 = new SqlCommand("select * from Personeller where KullaniciAdi=@p1 and Sifre=@p2 and Durum=1 ", cReena.con);
                cmd2.Parameters.AddWithValue("@p1", txtKullaniciAdi.Text);
                cmd2.Parameters.AddWithValue("@p2", txtSifre.Text);
                SqlDataReader dr2 = cmd2.ExecuteReader();
                if (dr2.Read())
                {
                        if (ChkBeniHatirla.Checked)
                        {
                            try
                            {
                                cReena.baglantiKontrol();
                                SqlCommand cmd = new SqlCommand("Update BeniHatirla set username=@p1, password=@p2 where id=1", cReena.con);
                                cmd.Parameters.AddWithValue("@p1", txtKullaniciAdi.Text);
                                cmd.Parameters.AddWithValue("@p2", txtSifre.Text);
                                cmd.ExecuteNonQuery();

                            }
                            catch (SqlException ex)
                            {
                                string hata = ex.Message;
                                MessageBox.Show("Veri Tabanıyla Bağlantı Kurulurken Hata Oluştu. Hata Kodu 1", "Hata Penceresi", MessageBoxButtons.OK, MessageBoxIcon.Error);

                            }
                            finally
                            {
                                cReena.con.Close();

                            }
                        }

                    FrmMenu fr = new FrmMenu();
                    fr.AktifKullanici = txtKullaniciAdi.Text;
                    fr.Show();
                    this.Hide();
                }
                else
                {
                    MessageBox.Show("Kullanıcı Adı veya Şifre Hatalı...!!", "Hata Penceresi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtKullaniciAdi.Text = "";
                    txtSifre.Text = "";
                }

               
            }
            else
            {
                MessageBox.Show("Lütfen Kullanıcı Adı ve Şifre Alanlarını Doldurunuz...!", "Hata Penceresi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtKullaniciAdi.Text = "";
                txtSifre.Text = "";
            }
        }

        void BeniHatirla()
        {
            try
            {
                cReena.baglantiKontrol();
                SqlCommand cmd = new SqlCommand("Select * from BeniHatirla", cReena.con);
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    if (dr["username"].ToString() != "sifir" || dr["password"].ToString() != "sifir")
                    {
                        txtKullaniciAdi.Text = dr["username"].ToString();
                        txtSifre.Text = dr["password"].ToString();
                    }
                    else
                    {
                        break;
                    }

                }
               
            }
            catch (SqlException ex)
            {
                string hata = ex.Message;
                MessageBox.Show("Veri Tabanıyla Bağlantı Kurulurken Hata Oluştu. Hata Kodu 1", "Hata Penceresi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                throw;
            }
            finally
            {

                cReena.con.Close();
            }

        }


        private void Giris_Load(object sender, EventArgs e)
        {
            BeniHatirla();
            ChkBeniHatirla.Checked = false;
        }


        private void lblBilgileriSıfırla_Click(object sender, EventArgs e)
        {
            try
            {
                cReena.baglantiKontrol();
                SqlCommand cmd = new SqlCommand("Update BeniHatirla set username='sifir', password='sifir' where id=1", cReena.con);

                cmd.ExecuteNonQuery();

            }
            catch (SqlException ex)
            {
                string hata = ex.Message;
                MessageBox.Show("Veri Tabanıyla Bağlantı Kurulurken Hata Oluştu. Hata Kodu 1", "Hata Penceresi", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
            finally
            {

                cReena.con.Close();
                MessageBox.Show("Beni Hatırla Bilgileriniz Sıfırlanmıştır.", "Bilgilendirme Penceresi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtKullaniciAdi.Text = "";
                txtSifre.Text = "Password";
            }

        }

        private void pictureBox2_MouseLeave(object sender, EventArgs e)
        {
            if (txtSifre.isPassword == true)
            {
                txtSifre.isPassword = true;
            }
            else if (txtSifre.isPassword == false)
            {
                txtSifre.isPassword = false;
            }
        }

        private void bunifuImageButton1_Click(object sender, EventArgs e)
        {
            lblGirisYap.Text = "Giriş Yap";
            lblKullaniciAdi.Text = "Kullanıcı Adı";
            lblSifre.Text = "Şifre";
            lblBeniHatirla.Text = "Beni Hatırla";
            lblBilgileriSifirla.Text = "Bilgileri Sıfırla";
            btnGirisYap.Text = "Giriş Yap";
        }

        private void bunifuImageButton2_Click(object sender, EventArgs e)
        {
            lblGirisYap.Text = "Log In";
            lblKullaniciAdi.Text = "User Name";
            lblSifre.Text = "Password";
            lblBeniHatirla.Text = "Remember Me";
            lblBilgileriSifirla.Text = "Reset Information";
            btnGirisYap.Text = "Log In";
        }

        private void lblBeniHatirla_Click(object sender, EventArgs e)
        {
            if (ChkBeniHatirla.Checked==true)
            {
                ChkBeniHatirla.Checked = false;
            }
            else if (ChkBeniHatirla.Checked == false)
            {
                ChkBeniHatirla.Checked = true;
            }
        }
    }
}
