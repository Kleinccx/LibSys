using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LibSys
{
    public partial class Form1 : Form
    {
        private OleDbConnection con;
        public Form1()
        {
            InitializeComponent();

            con = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=Z:\\QQ136\\LibSys\\LibSys\\LibSys.mdb");
        }
        private void btnAdd_Click(object sender, EventArgs e)
        {

            con.Open();
            OleDbCommand com = new OleDbCommand("Insert into book values ('" + txtno.Text + "', '" + txttitle.Text + "', '" + txtauthor.Text + "')", con);
            com.ExecuteNonQuery();

            MessageBox.Show("Successfully Saved!", "info", MessageBoxButtons.OK, MessageBoxIcon.Information);
            con.Close();
            loadDatagrid();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            con.Open();
            string num = txtno.Text;
            DialogResult dr = MessageBox.Show("Are you sure you want to delete this?", "Confirm Deletion", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dr == DialogResult.Yes)
            {
                int ascensionNumber;
                if (int.TryParse(num, out ascensionNumber))
                {
                    OleDbCommand com = new OleDbCommand("DELETE FROM book WHERE [Ascension Number] = " + ascensionNumber, con);
                    com.ExecuteNonQuery();
                    MessageBox.Show("Successfully DELETED!", "info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Invalid input. Please enter a valid integer.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("CANCELLED!", "info", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            con.Close();
            loadDatagrid();
        }



    

        private void btnEdit_Click(object sender, EventArgs e)
        {
            con.Open();
            string no;
            no = txtno.Text;

            OleDbCommand com = new OleDbCommand("Update book SET title= '" + txttitle.Text + "', author= '" + txtauthor.Text + "' where Ascension Number= " + no, con);
            com.ExecuteNonQuery();
            MessageBox.Show("Succesfully UPDATED!", "info", MessageBoxButtons.OK, MessageBoxIcon.Information);
            con.Close();
            loadDatagrid();
        }

        private void grid1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            txtno.Text = grid1.Rows[e.RowIndex].Cells["Ascension Number"].Value.ToString();
            txttitle.Text = grid1.Rows[e.RowIndex].Cells["title"].Value.ToString();
            txtauthor.Text = grid1.Rows[e.RowIndex].Cells["author"].Value.ToString();
            loadDatagrid();
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {


            con.Open();
            OleDbCommand com = new OleDbCommand("Select * from book where title like '%" + txtSearch.Text + "%'", con);
            com.ExecuteNonQuery();

            OleDbDataAdapter adap = new OleDbDataAdapter(com);
            DataTable tab = new DataTable();

            adap.Fill(tab);
            grid1.DataSource = tab;

            con.Close();
        }
        private void loadDatagrid()
        {

            con.Open();
            OleDbCommand com = new OleDbCommand("Select * from book order by [Ascension Number]", con);
            com.ExecuteNonQuery();

            OleDbDataAdapter adap = new OleDbDataAdapter(com);
            DataTable tab = new DataTable();

            adap.Fill(tab);
            grid1.DataSource = tab;
            con.Close();

        }
    }
}