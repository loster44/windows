using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
//using System.Data.OracleClient;
//using System.Data.OleDb;
using Oracle.DataAccess.Client;
namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            string connString = "Data Source=localhost/orcl;User ID=system;PassWord=oracle12c";
                //"Provider = Microsoft.Jet.OLEDB.4.0;Data Source=(DESCRIPTION = (ADDRESS_LIST= (ADDRESS = (PROTOCOL = TCP)(HOST = localhost)(PORT = 1521))) (CONNECT_DATA = (SERVICE_NAME =system-orcl)));User ID=system;Password=oracle12c";
            cnn = new OracleConnection(connString);
           
            if (cnn == null) {
                MessageBox.Show("data base connect fail");
            }
 cnn.Open();
        }
        private OracleConnection cnn;
        private void button1_Click(object sender, EventArgs e)
        {
            Form3 form3 = new Form3(cnn, 0, this);
            form3.Show(this);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Form3 form3 = new Form3(cnn, 1, this);
            form3.Show();
        }
        public void insert(string Student_ID, string Name, string Score, string Classes) {
            dataGridView1.Rows.Add(Student_ID, Name, Score, Classes);
        }
        public void delete(int i) {
            dataGridView1.Rows.RemoveAt(i);
        }
        public void update(int i, string Student_ID, string Name, string Score, string Classes) {
            dataGridView1.Rows[i].SetValues(Student_ID, Name, Score, Classes);
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            Form2 form2 = new Form2(cnn, this);
            int row = dataGridView1.CurrentCell.RowIndex;
            if (dataGridView1.Rows[row].Cells[0].Value != null)
            {
                form2.getdata(row, dataGridView1.Rows[row].Cells[0].Value.ToString(), dataGridView1.Rows[row].Cells[1].Value.ToString(), dataGridView1.Rows[row].Cells[2].Value.ToString(), dataGridView1.Rows[row].Cells[3].Value.ToString());
                form2.Show();
            }
        }
        private void set(DataSet ds){
            dataGridView1.DataSource = ds;
        }
    }
}
