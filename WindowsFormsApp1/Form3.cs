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
    public partial class Form3 : Form
    {
        public Form3(OracleConnection coon,int Flag,Form1 Owner)
        {
            InitializeComponent();
            cnn = coon;
            flag = Flag;
            owner = Owner;
        }
        private OracleConnection cnn;
        private int flag;
        private Form1 owner;
        private void button1_Click(object sender, EventArgs e)
        {
            if(flag==0)for (int i = 0; i < 4; i++) if (this.dataGridView1.Rows[0].Cells[i].Value == null) { MessageBox.Show("should not be blank"); return; }
            DialogResult d = MessageBox.Show("Are you sure?","", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
            if (d == DialogResult.OK)
            {

                OracleCommand cmd = cnn.CreateCommand();
                string sql="";
                if (flag == 0)
                {
                    sql = String.Format("insert into students (student_ID,name,score,classes)values({0},{1},{2},{3})",
                    this.dataGridView1.Rows[0].Cells[0].Value.ToString(),
                    this.dataGridView1.Rows[0].Cells[1].Value.ToString(),
                    this.dataGridView1.Rows[0].Cells[2].Value.ToString(),
                    this.dataGridView1.Rows[0].Cells[3].Value.ToString());
                    cmd.CommandText = sql;
                    int i = cmd.ExecuteNonQuery();
                    
                    if (i == 0){ MessageBox.Show("insert fail"); return; }
                }
                else {
                   
                    
                    sql = "select*from  students where ";
                    bool flag = false; 
                    if (this.dataGridView1.Rows[0].Cells[0].Value != null) { sql += String.Format("student_ID={0}  ", dataGridView1.Rows[0].Cells[0].Value.ToString()); flag = true; }
                    if (this.dataGridView1.Rows[0].Cells[1].Value != null) { if (flag) sql += " and "; else flag =true; sql += String.Format(" name={0} ", dataGridView1.Rows[0].Cells[1].Value.ToString()); }
                    if (this.dataGridView1.Rows[0].Cells[2].Value != null) { if (flag) sql += " and "; else flag = true; sql += String.Format(" score={0} ", dataGridView1.Rows[0].Cells[2].Value.ToString()); }
                    if (this.dataGridView1.Rows[0].Cells[3].Value != null) { if (flag) sql += " and "; else flag = true; sql += String.Format(" classes={0} ", dataGridView1.Rows[0].Cells[3].Value.ToString()); }

                    cmd.CommandText = sql;
                    OracleDataAdapter ada = new OracleDataAdapter(cmd);
                    DataSet ds = new DataSet();
                    ada.Fill(ds);
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        owner.insert(ds.Tables[0].Rows[i].ItemArray[0].ToString(),
                                     ds.Tables[0].Rows[i].ItemArray[1].ToString(),
                                     ds.Tables[0].Rows[i].ItemArray[2].ToString(),
                                     ds.Tables[0].Rows[i].ItemArray[3].ToString());
                    }
                    this.Close();
                }
            }
        }
        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
