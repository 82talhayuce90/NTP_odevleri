using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.Sql;
using System.Data.SqlClient;
using System.Net;
using System.Net.Mail;

namespace Sifremi_Unuttum_Ödevi
{
    public partial class Form1 : Form
    {
        private string smptsrvr;

        public Form1()
        {
            InitializeComponent();
        }
        SqlConnection baglanti = new SqlConnection(@"Data Source=DESKTOP-JH1OVRK;Initial Catalog=Sifremi_Unuttum_Data;Integrated Security=True");
        private string mailadresin;

        public NetworkCredential Credentials { get; private set; }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "" || textBox2.Text == "")
            {
                MessageBox.Show("Lütfen boş bir alan bırakmayınız");
            }
            else
            {
                baglanti.Open();
                string user;
                string password;
                user = textBox1.Text;
                password = textBox2.Text;
                SqlCommand komut = new SqlCommand(" Select * from  Personel_Giris_Bilgileri_Tablosu where kullanici_adi='" + user + "'and sifre='" + password + "'", baglanti);
                SqlDataReader oku = komut.ExecuteReader();
                if (oku.Read())
                {
                    MessageBox.Show("hoş geldinz " + user);
                }
                else
                {
                    MessageBox.Show("hatalı kullanıcı adı veya şifre....");
                }
                baglanti.Close();
            }
        }
        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }
        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            string user;
            string password;
            user = textBox1.Text;
            password = textBox2.Text;
            SqlCommand kontrol = new SqlCommand(" Select * from  Personel_Giris_Bilgileri_Tablosu where kullanici_adi='" + user + "'", baglanti);
            SqlDataReader oku = kontrol.ExecuteReader();
            while (oku.Read())
            {


                try
                {
                    SmtpClient smtpserver = new SmtpClient();
                    MailMessage mail = new MailMessage();
                    String tarih = DateTime.Now.ToLongDateString();
                    String ben = "sifremi.unuttum.info@gmail.com";
                    String smtpstvr = "smtp.gmail.com";
                    String kime = (oku["eposta"].ToString());
                    String konu = ("şifre doğrulama maili");
                    String yaz = ("sayın," + oku["kullanici_adi"].ToString() + "\n" + "bizden " + tarih + " tarihinde şiifre hatırlatma isteğinde bulundunuz" + "\n" + "parolanız:" + oku["sifre"].ToString() + "\n iyi günler");
                    var client = new SmtpClient()
                    {
                        Host = "smtp.gmail.com",
                        Port = 587,
                        Credentials = new NetworkCredential("sifremi.unuttum.info@gmail.com", "itxgawdphpcjebhj"),
                        EnableSsl = true,

                    };
                    var mailmessage = new MailMessage(ben, kime, konu, yaz);

                    client.Send(mailmessage);
                    DialogResult bilgi = new DialogResult();
                    bilgi = MessageBox.Show("girmiş olduğunuz bilgiler uyuşuyor. şifreniz mail adresinine gönderilmişit.");
                    this.Hide();


                }
                catch (Exception Hata)
                {
                    MessageBox.Show("mesaj gönderme hatası" + Hata.Message);
                }



            }
            oku.Close();
            baglanti.Close();
        }

    }
}

