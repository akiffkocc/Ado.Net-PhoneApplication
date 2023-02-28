using System.Collections.Generic;
using System.Data.SqlClient;
using System.Xml.Linq;

namespace PhoneApplication
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        public void RehberiGetir()
        {
            listView1.Items.Clear();
            SqlConnection baglanti = new SqlConnection("Server=(LocalDb)\\MSSQLLocalDB;Database=PhoneBook;Integrated Security=True");
            SqlCommand komut = new SqlCommand("select * from PhoneBooks", baglanti);
            baglanti.Open();
            SqlDataReader okuyucu = komut.ExecuteReader();
            while (okuyucu.Read())
            {
                ListViewItem satir = new ListViewItem();
                satir.Text = okuyucu["Id"].ToString();
                satir.SubItems.Add(okuyucu["Name"].ToString());
                satir.SubItems.Add(okuyucu["Surname"].ToString());
                satir.SubItems.Add(okuyucu["Phone"].ToString());
                listView1.Items.Add(satir);
            }
            listView1.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent);
            baglanti.Close();
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            RehberiGetir();
        }
        public void RehberEkle()
        {
            //string sqlsorgusu = "insert into PhoneBooks(Name,Surname,Phone) values ('Adil','Koç',5363212623)";
            SqlConnection baglanti = new SqlConnection("Server=(LocalDb)\\MSSQLLocalDB;Database=PhoneBook;Integrated Security=True");
            SqlCommand rehberekle = new SqlCommand("insert PhoneBooks(Name,Surname,Phone) values(@n,@sn,@p)", baglanti);
            rehberekle.Parameters.AddWithValue("@n", txtAdi.Text);
            rehberekle.Parameters.AddWithValue("@sn",txtSoyadi.Text);
            rehberekle.Parameters.AddWithValue("@p",txtPhone.Text);
            baglanti.Open();
            int adet = rehberekle.ExecuteNonQuery();
            if (adet > 0) 
            { 
                MessageBox.Show("Ekleme Baþarýlý");
                RehberiGetir();
            }
            else
            {
                MessageBox.Show("Ekleme Baþarýsýz");
            }
        }

        private void btnEkle_Click(object sender, EventArgs e)
        {
            RehberEkle();
        }
        public void RehberSil()
        {
            SqlConnection baglanti = new SqlConnection("Server=(LocalDb)\\MSSQLLocalDB;Database=PhoneBook;Integrated Security=True");
            SqlCommand rehberSil = new SqlCommand("delete from PhoneBooks where Name=@n", baglanti);
            rehberSil.Parameters.AddWithValue("@n",txtAdi.Text);
            baglanti.Open();
            rehberSil.ExecuteNonQuery();
            baglanti.Close();
            RehberiGetir();
        }

        private void btnSil_Click(object sender, EventArgs e)
        {
            RehberSil();
        }
        public void RehberGuncelle()
        {
            SqlConnection baglanti = new SqlConnection("Server=(LocalDb)\\MSSQLLocalDB;Database=PhoneBook;Integrated Security=True");
            SqlCommand rehberGuncelle = new SqlCommand("update PhoneBooks set Name=@n,Surname=@sn,Phone=@p", baglanti);
            rehberGuncelle.Parameters.AddWithValue("@n", txtAdi.Text);
            rehberGuncelle.Parameters.AddWithValue("@sn",txtSoyadi.Text);
            rehberGuncelle.Parameters.AddWithValue("@p",txtPhone.Text);
            baglanti.Open();
            rehberGuncelle.ExecuteNonQuery();
            baglanti.Close();
            RehberiGetir();
        }
        private void btnGuncelle_Click(object sender, EventArgs e)
        {
            RehberGuncelle();
        }

        private void silToolStripMenuItem_Click(object sender, EventArgs e)
        {
            RehberSil();
        }
        private void güncelleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            RehberGuncelle();
        }
    }
}