﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data;
using System.Data.SqlClient;

namespace Library.WinUI
{
    public partial class frmSearchBooks : Form
    {
        public frmSearchBooks()
        {
            InitializeComponent();
        }
        public Books SelectedBook { get; set; }
        SqlConnection con;
        SqlCommand cmd;
        SqlDataAdapter da;
        DataSet dsBooks = new DataSet();
        private void frmSearchBooks_Load(object sender, EventArgs e)
        {
            SelectedBook = new Books();
            con = new SqlConnection();
            con.ConnectionString = DBHelperDAL.ConnectionString;
            con.Open();
            cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandText = "select * from books";
            da = new SqlDataAdapter(cmd);
            da.Fill(dsBooks);
            dataGridView1.DataSource = dsBooks.Tables[0];
            con.Close();
            cmd.Dispose();
        }

        private void txtSearchTitle_TextChanged(object sender, EventArgs e)
        {
            var rows = dsBooks.Tables[0].Select(string.Format("BookTitle like '%{0}%'", txtSearchTitle.Text));
            DataTable temp = dsBooks.Tables[0].Clone();
            foreach (var item in rows)
            {
                string[] values = {item[0].ToString(),item[1].ToString(),item[2].ToString(),item[3].ToString() };
                temp.Rows.Add(values);
                
            }
            dataGridView1.DataSource = temp;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int currentrow = this.dataGridView1.CurrentCell.RowIndex;
            SelectedBook.Code = Convert.ToInt32( this.dataGridView1.Rows[currentrow].Cells[0].Value);
            SelectedBook.Title = this.dataGridView1.Rows[currentrow].Cells[1].Value.ToString();
            SelectedBook.Author = this.dataGridView1.Rows[currentrow].Cells[2].Value.ToString();
            SelectedBook.Publisher = this.dataGridView1.Rows[currentrow].Cells[3].Value.ToString();
            this.Close();
        }
    }
}
