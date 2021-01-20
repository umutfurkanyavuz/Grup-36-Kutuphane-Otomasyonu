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
    public partial class UyeKitapBagis : Form
    {
        private OracleConnection con;
        private OracleCommand com;
        public UyeKitapBagis()
        {
            InitializeComponent();
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
            com = new OracleCommand();
            com.Connection = con;
            com.CommandText = "INSERT INTO Books (BookName, BookAuthor, BookPublisher, BookPageNumber) " +
                "VALUES ('" + textBox1.Text + "','" + textBox2.Text + "','" + textBox4.Text + "','"
                + Convert.ToDecimal(textBox3.Text) + "')";
            com.ExecuteNonQuery();
            con.Close();
            MessageBox.Show("Bağışınız İçin Teşekkürler");
            textBox1.Clear();
            textBox2.Clear();
            textBox3.Clear();
            textBox4.Clear();
        }

        private void textBox3_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }
    }
}
