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
    public partial class KitapEkle : Form
    {
        private OracleConnection con;
        private OracleCommand com;
        public KitapEkle()
        {
            InitializeComponent();
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            DbCon dbcon = new DbCon();
            con = dbcon.connection();
            com = new OracleCommand();
            com.Connection = con;
            com.CommandType = CommandType.StoredProcedure;
            com.CommandText = "sp_addbook";
            com.Parameters.Add("bookName", OracleDbType.Varchar2, 100).Value = textBox1.Text;
            com.Parameters.Add("bookAuthor", OracleDbType.Varchar2, 100).Value = textBox2.Text;
            com.Parameters.Add("bookPublisher", OracleDbType.Varchar2, 100).Value = textBox4.Text;
            com.Parameters.Add("bookPageNumber", OracleDbType.Decimal).Value = Convert.ToDecimal(textBox3.Text);
            com.ExecuteNonQuery();
            con.Close();
            MessageBox.Show("Kayıt Başarılı");
            textBox1.Clear();
            textBox2.Clear();
            textBox3.Clear();
            textBox4.Clear();

        }

        private void button2_Click(object sender, EventArgs e)
        {
            MenuAdmin menuAdmin = new MenuAdmin();
            menuAdmin.Show();
            this.Hide();
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox3_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }
    }
}
