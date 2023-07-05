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

    public partial class Form1 : Form
    {

        string ordb = "Data source=orcl;User Id=scott;Password=tiger;";
        OracleConnection conn;
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            conn = new OracleConnection(ordb);
            conn.Open();
            OracleCommand cmd = new OracleCommand();
            cmd.Connection = conn;
            cmd.CommandText = "select carid from car";
            cmd.CommandType = CommandType.Text;
            OracleDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                comboBox1.Items.Add(dr[0]);
              
            }
            dr.Close();
            OracleCommand c = new OracleCommand();
            c.Connection = conn;
            c.CommandText = "CARCOLOR ";
            c.CommandType = CommandType.StoredProcedure;
            c.Parameters.Add("id", OracleDbType.RefCursor, ParameterDirection.Output);
            OracleDataReader r = c.ExecuteReader();
            while (r.Read())
            {
                comboBox2.Items.Add(r[0]);

            }
            r.Close();

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            OracleCommand c = new OracleCommand();
            c.Connection = conn;
            c.CommandText = "select car.carName,car.dailyprice,car_Model.carmodel from car,car_Model where car.carid =:id";
            c.CommandType = CommandType.Text;
            c.Parameters.Add("id", comboBox1.SelectedItem.ToString());
            OracleDataReader dr = c.ExecuteReader();
            if (dr.Read())
            {
                textBox1.Text = dr[0].ToString();
                textBox2.Text = dr[1].ToString();
                textBox3.Text = dr[2].ToString();
            }
            dr.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form2 adminform = new Form2();
            adminform.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {

            int maxid, newid;
            OracleCommand c = new OracleCommand();
            c.Connection = conn;
            c.CommandText = "GETCUSTOMERID";
            c.CommandType = CommandType.StoredProcedure;
            c.Parameters.Add("id",OracleDbType.Int32,ParameterDirection.Output);
            c.ExecuteNonQuery();
            try
            {
                maxid = Convert.ToInt32(c.Parameters["id"].Value.ToString());
                newid = maxid + 1;
            }
            catch { newid = 1; }
          OracleCommand cmd = new OracleCommand();
            cmd.Connection = conn;
            cmd.CommandText= "InsertCustomer";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("hnum", textBox4.Text);
            cmd.Parameters.Add("cityy", textBox6.Text);
            cmd.Parameters.Add("countryy", textBox5.Text);
            cmd.Parameters.Add("fname", textBox7.Text);
            cmd.Parameters.Add("lname", textBox8.Text);
            cmd.Parameters.Add("id",newid);
            cmd.Parameters.Add("lis", textBox11.Text);
            cmd.Parameters.Add("mid", textBox9.Text);
            cmd.Parameters.Add("crid", textBox10.Text);
            cmd.ExecuteNonQuery();
            textBox12.Text = newid.ToString();
            MessageBox.Show("customer info stored successfully");
        }
    }
}
