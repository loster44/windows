using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
//using System.Data.OleDb;
//using System.Data.OracleClient;
using Oracle.DataAccess.Client;
namespace WindowsFormsApp1
{
    public partial class Form2 : Form
    {
        public Form2(OracleConnection coon, Form1 Owner)
        {
            InitializeComponent();
            cnn = coon;
            owner = Owner;
        }
        private OracleConnection cnn;
        private Form1 owner;
        private int row;
        private string Ostudent_ID;
        private string Oname;
        private string Oscore;
        private string Oclasses;
        
        private void button1_Click(object sender, EventArgs e)
        {
           DialogResult d= MessageBox.Show("Are you sure?", "", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
            if (d == DialogResult.OK) {
                OracleCommand cmd = cnn.CreateCommand();
                string sql = String.Format("delete from students where student_ID={0} and name={1} and score= {2} and classes={3}",
                    Ostudent_ID,
                    Oname,
                    Oscore,
                    Oclasses);
                cmd.CommandText = sql;
                int i=cmd.ExecuteNonQuery();
                if (i == 0) { MessageBox.Show("delete fail"); return; }
                owner.delete(row);
                this.Close();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < 4; i++) if (this.dataGridView1.Rows[0].Cells[i].Value == null) { MessageBox.Show("should not be blank"); return; }
            DialogResult d = MessageBox.Show("Are you sure?", "", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
            if (d == DialogResult.OK)
            {
                OracleCommand cmd = cnn.CreateCommand();
                string sql = String.Format("update  students set student_ID={0},name={1},score={2},classes={3}  where student_ID={4} and name={5} and score= {6} and classes={7} ",
                    Ostudent_ID,
                    Oname,
                    Oscore,
                    Oclasses,
                    this.dataGridView1.Rows[0].Cells[0].Value.ToString(),
                    this.dataGridView1.Rows[0].Cells[1].Value.ToString(),
                    this.dataGridView1.Rows[0].Cells[2].Value.ToString(),
                    this.dataGridView1.Rows[0].Cells[3].Value.ToString());
                cmd.CommandText = sql;
                int i= cmd.ExecuteNonQuery();
                if (i == 0) { MessageBox.Show("update fail"); return; }
                owner.update(row, this.dataGridView1.Rows[0].Cells[0].Value.ToString(),
                    this.dataGridView1.Rows[0].Cells[1].Value.ToString(),
                    this.dataGridView1.Rows[0].Cells[2].Value.ToString(),
                    this.dataGridView1.Rows[0].Cells[3].Value.ToString());
                this.Close();
            }
        }
        public void getdata(int i, string Student_ID, string Name, string Score, string Classes) {
            row = i;
            Ostudent_ID = Student_ID;
            Oname = Name;
            Oscore = Score;
            Oclasses = Classes;
            dataGridView1.Rows.Add(Ostudent_ID, Oname, Oscore, Oclasses);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
