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
    public partial class GeriVer : Form
    {
        private OracleConnection con;
        private OracleCommand com;
        private OracleDataReader dr;

        public GeriVer()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            MenuAdmin menuAdmin = new MenuAdmin();
            menuAdmin.Show();
            this.Hide();
        }

        private void GeriVer_Load(object sender, EventArgs e)
        {
            DbCon dbcon = new DbCon();
            con = dbcon.connection();
            DataTable dataTable = new DataTable();
            OracleDataAdapter oracleDataAdapter = new OracleDataAdapter("SELECT * FROM OduncVer", con);
            oracleDataAdapter.Fill(dataTable);
            dataGridView1.DataSource = dataTable;
            //kolon isimlerini değiştirme
            dataGridView1.Columns[0].HeaderText = "Kitap ID";
            dataGridView1.Columns[1].HeaderText = "TC Kimlik No";
            dataGridView1.Columns[2].HeaderText = "Adı";
            dataGridView1.Columns[3].HeaderText = "Soyadı";
            dataGridView1.Columns[4].HeaderText = "Kitap Adı";
            dataGridView1.Columns[5].HeaderText = "Ödünç Tarihi";
            dataGridView1.Columns[6].HeaderText = "İade Tarihi";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DbCon dbcon = new DbCon();
            con = dbcon.connection();
            com = new OracleCommand();
            com.Connection = con;

            int secilenKitapId = Convert.ToInt16(dataGridView1.CurrentRow.Cells[0].Value);
            try
            {
                com.CommandText = "DELETE FROM OduncVer WHERE KitapID ='" + secilenKitapId + "'";
                com.ExecuteNonQuery();
                con.Close();
                MessageBox.Show("İade İşlemi Başarılı TEŞEKKÜR EDERİZ");
            }
            catch
            {
                MessageBox.Show("İade İşlemi Başarısız");
            }
            DataTable dataTable2 = new DataTable();
            OracleDataAdapter oracleDataAdapter2 = new OracleDataAdapter("SELECT * FROM OduncVer", con);
            oracleDataAdapter2.Fill(dataTable2);
            dataGridView1.DataSource = dataTable2;
            oracleDataAdapter2.Update(dataTable2);
            con.Close();
            textBox8.Clear();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            DbCon dbcon = new DbCon();
            con = dbcon.connection();
            OracleCommand komut = new OracleCommand("SELECT * FROM OduncVer WHERE KitapID LIKE '%" + textBox8.Text + "%'", con);
            DataTable dataTable2 = new DataTable();
            OracleDataAdapter dataAdapter = new OracleDataAdapter(komut);
            dataAdapter.Fill(dataTable2);
            dataGridView1.DataSource = dataTable2;
        }

        private void textBox8_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }
    }
}
