using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using System.Data.OleDb;

namespace Crypto_Database
{
    public partial class RegisterFrm : Form
    {
        CryptoClass cl = new CryptoClass();
        private int EditRow = 0;
        //for prevent reflect database password?  make this in class
        // private static string conected = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + Application.StartupPath + @"\DbHash.accdb ;Jet OLEDB:Database Password=";
        conectionstr constr = new conectionstr();
        public RegisterFrm()
        {
            InitializeComponent();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            label3.Visible = (textBox1.Text.Trim()==string.Empty);
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            label4.Visible = (textBox2.Text.Trim() == string.Empty);
        }
        void ShowGird()
        {
            OleDbConnection conn = new OleDbConnection(conectionstr.conected);
            OleDbDataAdapter adap = new OleDbDataAdapter("Select * From tblhash",conn);
            DataSet dset = new DataSet();
            adap.Fill(dset);
            dataGridView1.DataSource = dset.Tables[0].DefaultView;
        }
        private void RegisterFrm_Load(object sender, EventArgs e)
        {
            ShowGird();
            dataGridView1.AlternatingRowsDefaultCellStyle.BackColor = Color.BurlyWood;
            dataGridView1.DefaultCellStyle.BackColor = Color.Coral;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if(!label3.Visible && !label4.Visible)
            {
                if (EditRow==0)
                {
                    OleDbConnection con = new OleDbConnection(conectionstr.conected);
                    OleDbCommand cmd = new OleDbCommand("Insert Into tblhash (username,[password])Values(@user,@pass)",con);
                   // cmd.CommandText = "Insert Into tblhash (username,[password])Values(@user,@pass)";
                    cmd.Parameters.AddWithValue("@user",cl.Encrypt(textBox1.Text));
                    cmd.Parameters.AddWithValue("@pass",cl.Encrypt(textBox2.Text));
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();

                }
                else
                {
                    OleDbConnection con = new OleDbConnection(conectionstr.conected);
                    OleDbCommand cmd = new OleDbCommand("Update tblhash Set username=@user , [password]=@pass Where ID="+EditRow,con);
                    //cmd.CommandText = "Insert Into tblhash (username,[password])Values(@user,@pass)";
                    cmd.Parameters.AddWithValue("@user", textBox1.Text);
                    cmd.Parameters.AddWithValue("@pass", textBox2.Text);
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                }
                ShowGird();
                MessageBox.Show("User Add Success!");

            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            
            
                textBox1.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
                textBox2.Text = dataGridView1.CurrentRow.Cells[2].Value.ToString();
                EditRow = Convert.ToInt32(dataGridView1.CurrentRow.Cells[0].Value.ToString());
                button1.Text = "Change Now";
                ShowGird();
            
           // MessageBox.Show("Edit Success");
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if(dataGridView1.CurrentRow!=null)
            {
                if (MessageBox.Show("Do You wana to Delete this?","Warnning",MessageBoxButtons.YesNo)==DialogResult.Yes)
                {
                    int Dlt =Convert.ToInt32(dataGridView1.CurrentRow.Cells[0].Value.ToString());
                    OleDbConnection conn = new OleDbConnection(conectionstr.conected);
                    OleDbCommand cmd = new OleDbCommand("Delete From tblhash Where ID="+Dlt,conn);

                    conn.Open();
                    cmd.ExecuteNonQuery();
                    conn.Close();
                    ShowGird();
                }
               
               MessageBox.Show("Delete Success!");
            }
        }
    }
}
