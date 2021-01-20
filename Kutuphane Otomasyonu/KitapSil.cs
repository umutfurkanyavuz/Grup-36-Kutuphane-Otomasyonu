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
    public partial class KitapSil : Form
    {
        private OracleConnection con;
        private OracleCommand com;
        private OracleDataReader dr;
        public KitapSil()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            MenuAdmin menuAdmin = new MenuAdmin();
            menuAdmin.Show();
            this.Hide();
        }

        private void KitapSil_Load(object sender, EventArgs e)
        {
            textBox2.Visible = false;
            textBox3.Visible = false;
            textBox4.Visible = false;
            textBox5.Visible = false;
            label2.Visible = false;
            label3.Visible = false;
            label4.Visible = false;
            label5.Visible = false;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if(textBox1.Text != "") { 
            textBox2.Visible = true;
            textBox3.Visible = true;
            textBox4.Visible = true;
            textBox5.Visible = true;
            label2.Visible = true;
            label3.Visible = true;
            label4.Visible = true;
            label5.Visible = true;
            }
            DbCon dbcon = new DbCon();
            con = dbcon.connection();
            com = new OracleCommand();
            com.Connection = con;
            com.CommandText = "SELECT * FROM Books WHERE BookID ='" + textBox1.Text + "'";
            dr = com.ExecuteReader();
            if (dr.Read())
            {
                textBox2.Text = dr[1].ToString();
                textBox3.Text = dr[2].ToString();
                textBox4.Text = dr[3].ToString();
                textBox5.Text = dr[4].ToString();
            }
            else
            {
                MessageBox.Show("Kitap Bulunamadı");
            }
            con.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DialogResult kitapSil = MessageBox.Show("Gerçekten Silmek İstiyor musunuz? ",
            "Silme İşlemi", MessageBoxButtons.YesNo);
            if (kitapSil == DialogResult.Yes)
            {
                DbCon dbcon = new DbCon();
                con = dbcon.connection();
                com = new OracleCommand();
                com.Connection = con;
                com.CommandText = "DELETE FROM Books WHERE BookID ='" + textBox1.Text + "'";
                com.ExecuteNonQuery();
                con.Close();
                MessageBox.Show("Silme Başarılı");
            }
            else
            {
                MessageBox.Show("İşlem İptal Edildi.");
            }
            textBox1.Clear();
            textBox2.Clear();
            textBox3.Clear();
            textBox4.Clear();
            textBox5.Clear();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }
    }
    }

