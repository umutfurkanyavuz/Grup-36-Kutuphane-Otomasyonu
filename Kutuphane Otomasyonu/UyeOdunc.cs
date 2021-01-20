using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.Odbc;
using Oracle.DataAccess.Client;

namespace Kutuphane_Otomasyonu
{
    public partial class UyeOdunc : Form
    {
        private OracleConnection con;
        private OracleCommand com;
        private OracleDataReader dr;

        public UyeOdunc()
        {
            InitializeComponent();
        }

        private void UyeOdunc_Load(object sender, EventArgs e)
        {
            {
                DbCon dbcon = new DbCon();
                con = dbcon.connection();
                DataTable dataTable = new DataTable();
                OracleDataAdapter oracleDataAdapter = new OracleDataAdapter("SELECT * FROM Books", con);
                oracleDataAdapter.Fill(dataTable);
                dataGridView1.DataSource = dataTable;
                //kolon isimlerini değiştirme
                dataGridView1.Columns[0].HeaderText = "Kitap ID";
                dataGridView1.Columns[1].HeaderText = "Kitap Adı";
                dataGridView1.Columns[2].HeaderText = "Yazarı";
                dataGridView1.Columns[3].HeaderText = "Yayınevi";
                dataGridView1.Columns[4].HeaderText = "Sayfa Sayısı";

                DataTable dataTable2 = new DataTable();
                OracleDataAdapter oracleDataAdapter2 = new OracleDataAdapter("SELECT * FROM OduncVer WHERE UyeTc = '" + UyeLogin.metin + "'", con);
                oracleDataAdapter2.Fill(dataTable2);
                dataGridView2.DataSource = dataTable2;
                //kolon isimlerini değiştirme
                dataGridView2.Columns[0].HeaderText = "Kitap ID";
                dataGridView2.Columns[1].HeaderText = "TC Kimlik No";
                dataGridView2.Columns[2].HeaderText = "Adı";
                dataGridView2.Columns[3].HeaderText = "Soyadı";
                dataGridView2.Columns[4].HeaderText = "Kitap Adı";
                dataGridView2.Columns[5].HeaderText = "Ödünç Tarihi";
                dataGridView2.Columns[6].HeaderText = "İade Tarihi";

            }

            com = new OracleCommand();
            com.Connection = con;
            com.CommandText = "SELECT * FROM Uyeler WHERE UyeTc ='" + UyeLogin.metin + "'";

            dr = com.ExecuteReader();

            if (dr.Read())
            {
                textBox2.Text = dr[0].ToString();
                textBox1.Text = dr[1].ToString();
                textBox3.Text = dr[2].ToString();
                textBox4.Text = dr[3].ToString();
                textBox5.Text = dr[4].ToString();
                textBox6.Text = dr[5].ToString();
            }
            con.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            MenuUye menuUye = new MenuUye();
            menuUye.Show();
            this.Hide();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DbCon dbcon = new DbCon();
            con = dbcon.connection();
            OracleCommand komut = new OracleCommand("SELECT * FROM Books WHERE BookName LIKE '%" + textBox7.Text + "%' or BookAuthor like '%" + textBox7.Text + "%' or BookPublisher like '%" + textBox7.Text + "%'", con);
            DataTable dataTable2 = new DataTable();
            OracleDataAdapter dataAdapter = new OracleDataAdapter(komut);
            dataAdapter.Fill(dataTable2);
            dataGridView1.DataSource = dataTable2;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            DbCon dbcon = new DbCon();
            con = dbcon.connection();
            com = new OracleCommand();
            com.Connection = con;

            string tarih = dateTimePicker1.Value.ToString();

            com.CommandText = "SELECT * FROM Uyeler WHERE UyeTc ='" + textBox1.Text + "'";


            int secilenKitapId = Convert.ToInt16(dataGridView1.CurrentRow.Cells[0].Value);
            string secilenKitapAdi = Convert.ToString(dataGridView1.CurrentRow.Cells[1].Value);
            com.CommandText = "SELECT * FROM Books WHERE BookId ='" + secilenKitapId + "'";
            try
            {
                com.CommandText = "INSERT INTO OduncVer (UyeTc, UyeAd, UyeSoyad, KitapID, KitapAdi, OduncTarihi, IadeTarihi) " +
              "VALUES ('" + textBox1.Text + "','" + textBox3.Text + "','" + textBox4.Text + "','" + secilenKitapId + "','" + secilenKitapAdi + "','" + tarih + "','"
              + dateTimePicker1.Value.AddDays(15).ToString() + "')";
                com.ExecuteNonQuery();
                MessageBox.Show("Ödünç Verme Başarılı");
            }
            catch
            {
                MessageBox.Show("Bu Kitap Daha Önce Alınmış");
            }
            DataTable dataTable2 = new DataTable();
            OracleDataAdapter oracleDataAdapter2 = new OracleDataAdapter("SELECT * FROM OduncVer WHERE UyeTc ='" + textBox1.Text + "'", con);
            oracleDataAdapter2.Fill(dataTable2);
            dataGridView2.DataSource = dataTable2;
            oracleDataAdapter2.Update(dataTable2);
            con.Close();
            textBox7.Clear();
        }
    }
}
