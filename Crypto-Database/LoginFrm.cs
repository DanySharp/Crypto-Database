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
    public partial class LoginFrm : Form
    {
        conectionstr conect = new conectionstr();
        CryptoClass cl = new CryptoClass();
        public LoginFrm()
        {
            InitializeComponent();
        }
        OleDbCommand cmd;
        OleDbConnection conn;
        OleDbDataReader reader;
        private void label1_Click(object sender, EventArgs e)
        {
            LoginFrm.ActiveForm.Hide();
            new RegisterFrm().ShowDialog();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if(txtusername.Text.Trim()==string.Empty || txtpassword.Text.Trim()==string.Empty)
            {
                label4.Visible = true;
                return;
            }
            string userEncrpt =cl.Encrypt(txtusername.Text);
            string passEncrp =cl.Encrypt(txtpassword.Text);
            conn = new OleDbConnection(conectionstr.conected);
            cmd = new OleDbCommand();
            cmd.Connection = conn;
            conn.Open();
            cmd.CommandText = "Select * From tblhash Where username='"+userEncrpt+"' AND password='"+passEncrp+"'";
            reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                LoginFrm.ActiveForm.Hide();
                new MainFrm().ShowDialog();
            }
            else
            {
                MessageBox.Show("User or pass invalid");
                txtusername.Clear();
                txtpassword.Clear();
                txtusername.Focus();
            }
            conn.Close();
        }

        private void LoginFrm_MouseHover(object sender, EventArgs e)
        {
            label4.Visible = false;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start("https://sourcecodester.com");
        }
    }
}
