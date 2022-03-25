using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net.Mail;
using System.Data.SqlClient;

namespace ReenaCafeBar
{
    public partial class FrmMail : Form
    {
        public FrmMail()
        {
            InitializeComponent();
        }
        public string mailAdres;
        private void FrmMail_Load(object sender, EventArgs e)
        {
            txtMailAdres.Text = mailAdres;
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void lblcikis_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void bunifuImageButton1_Click(object sender, EventArgs e)
        {
            btnGonder.Text = "Gönder";
            lblKonu.Text = "Konu";
            lblMessage.Text = "Mesaj";
            lblMailGonder.Text = "Mail Gönder";
            lblMailAdres.Text = "Mail Adres";
        }

        private void bunifuImageButton2_Click(object sender, EventArgs e)
        {
            btnGonder.Text = "Send";
            lblKonu.Text = "Subject";
            lblMessage.Text = "Message";
            lblMailGonder.Text = "Send Mail";
            lblMailAdres.Text = "Mail Address";
        }

        private void btnGonder_Click(object sender, EventArgs e)
        {
            try
            {
                cReena.baglantiKontrol();
                SqlCommand cmd = new SqlCommand("select * from Mail", cReena.con);
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    MailMessage mesajim = new MailMessage();
                    SmtpClient istemci = new SmtpClient();
                    istemci.Credentials = new System.Net.NetworkCredential(dr["mailadres"].ToString(), dr["mailpassword"].ToString());
                    istemci.Port = 587;
                    istemci.Host = "smtp.gmail.com";
                    istemci.EnableSsl = true;
                    mesajim.To.Add(txtMailAdres.Text);
                    mesajim.From = new MailAddress(dr["mailadres"].ToString());
                    mesajim.Subject = txtKonu.Text;
                    mesajim.Body = txtMesaj.Text;
                    istemci.Send(mesajim);
                    MessageBox.Show("Mailiniz Gönderildi.", "Bilgilendirme Penceresi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

               
            }
            catch (Exception ex)
            {
                string Hata = ex.Message;
                MessageBox.Show("Bağlantı Hatası. Mail Gönderilemedi. Hata Kodu 3","Hata Penceresi",MessageBoxButtons.OK,MessageBoxIcon.Error);
                throw;
            }
           
        }
    }
}
