using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Oracle.DataAccess.Client;
using Oracle.DataAccess.Types;

namespace CarRental
{
    public partial class Form2 : Form
    {
        string ordb = "Data source=orcl;User Id=scott;Password=tiger;";
        OracleConnection conn;
        public Form2()
        {
            InitializeComponent();
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            conn = new OracleConnection(ordb);
            conn.Open();
           textBox7.Text=DateTime.Now.ToString();
           textBox8.Text=DateTime.Now.AddDays(3).ToString();
            textBox10.Text=DateTime.Now.ToString();
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int maxid, newid;
            OracleCommand c = new OracleCommand();
            c.Connection = conn;
            c.CommandText = "GerRentId";
            c.CommandType = CommandType.StoredProcedure;
            c.Parameters.Add("id", OracleDbType.Int32, ParameterDirection.Output);
            c.ExecuteNonQuery();
            try
            {
                maxid = Convert.ToInt32(c.Parameters["id"].Value.ToString());
                newid = maxid + 1;
            }
            catch { newid = 1; }
            OracleCommand cmd= new OracleCommand();
            cmd.Connection = conn;
            cmd.CommandText = "RENTAL";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("id", newid);
            cmd.Parameters.Add("dib", textBox2.Text);
            cmd.Parameters.Add("pm", textBox3.Text);
            cmd.Parameters.Add("refu", textBox4.Text);
            cmd.Parameters.Add("dm", textBox5.Text);
            cmd.Parameters.Add("cid", textBox1.Text);
            cmd.Parameters.Add("resid", textBox6.Text);
            cmd.ExecuteNonQuery();
            textBox11.Text = newid.ToString();
            MessageBox.Show("car is rented");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            int maxid2, newid2;
            OracleCommand cc = new OracleCommand();
            cc.Connection = conn;
            cc.CommandText = "GetrReserveId";
            cc.CommandType = CommandType.StoredProcedure;
            cc.Parameters.Add("id", OracleDbType.Int32, ParameterDirection.Output);
            cc.ExecuteNonQuery();
            try
            {
                maxid2 = Convert.ToInt32(cc.Parameters["id"].Value.ToString());
                newid2 = maxid2 + 1;
            }
            catch { newid2 = 1; }
            OracleCommand cmd = new OracleCommand();
            cmd.Connection = conn;
            cmd.CommandText = "RESERVE";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("id", newid2);
            cmd.Parameters.Add("rd", Convert.ToDateTime(textBox7.Text));
            cmd.Parameters.Add("resd", Convert.ToDateTime(textBox8.Text));
            cmd.Parameters.Add("can", textBox9.Text);
            cmd.Parameters.Add("pd", Convert.ToDateTime(textBox10.Text));
            cmd.Parameters.Add("cid", textBox1.Text);
            cmd.ExecuteNonQuery();
            textBox6.Text = newid2.ToString();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Form3 adminform = new Form3();
            adminform.Show();
        }
    }
}
