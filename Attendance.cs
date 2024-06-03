using System;
using System.Data;
using System.Windows.Forms;
using DataGridView_Merge_Cells;

namespace Purchasing_Order
{
    public partial class Attendance : Form
    {
        [Obsolete]
        public Attendance()
        {
            InitializeComponent();
        }
        readonly Database db = new Database();
        readonly Supplier sup = new Supplier();
        private void Saves_Load(object sender, EventArgs e)
        {
            EvDate.Text = DateTime.Now.ToString("d");
            purchasingview.DataSource = db.GetData("Select * from Attendance");
            purchasingview.Columns[0].Visible = false;

            var col = new DataGridViewMergedTextBoxColumn();
            const string field = "MemID";
            col.HeaderText = field;
            col.Name = field;
            col.DataPropertyName = field;
            int colidx = purchasingview.Columns[field].Index;
            purchasingview.Columns.Remove(field);
            purchasingview.Columns.Insert(colidx, col);

            try
            {
                db.cmd = new System.Data.SqlClient.SqlCommand("SELECT MemID FROM AddMember", db.con);
                db.con.Open();
                db.dr = db.cmd.ExecuteReader();
                AutoCompleteStringCollection mycollection = new AutoCompleteStringCollection();
                while (db.dr.Read())
                {
                    mycollection.Add(db.dr.GetString(0));
                }
                txt_memid.AutoCompleteCustomSource = mycollection;

                db.con.Close();
                db.cmd.Dispose();
            }
            catch (Exception)
            {
                MessageBox.Show("Data Error", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            

            txt_admin.Enabled = false;
            purchasingview.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            WinAPI.AnimateWindow(this.Handle, 500, WinAPI.BLEND);

        }

        private void Txt_filter_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsControl(e.KeyChar) && char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }
        private void Txt_filter_TextChanged(object sender, EventArgs e)
        {
            try
            {
                db.con.Open();
                db.cmd = new System.Data.SqlClient.SqlCommand("select * from Attendance where Member like '%" + txt_filter.Text + "%'", db.con);
                db.da = new System.Data.SqlClient.SqlDataAdapter(db.cmd);
                DataTable dt = new DataTable();
                db.da.Fill(dt);
                purchasingview.DataSource = dt;
                db.con.Close();
            }
            catch (Exception)
            {
                MessageBox.Show("Data Error", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        

        private void Btn_save_Click(object sender, EventArgs e)
        {
            if (txt_memid.Text.Trim().Equals(string.Empty))
            {
                MessageBox.Show("Member ID cannot be empty !!", "Incomplete Data Entry", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (cmb_status.SelectedItem == null)
            {
                MessageBox.Show("Please Select Attendance Status!!", "Incomplete Data Entry", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (txt_comment.Text.Trim().Equals(string.Empty))
            {
                MessageBox.Show("Comment cannot be empty !!", "Incomplete Data Entry", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }


            string queryy = "Insert into Attendance values ('" + txt_memid.Text + "','" + txt_member.Text + "','" + cmb_status.Text + "','" + EvDate.Text + "','" + txt_comment.Text + "')";
            int i = sup.ADDEVENT(queryy);
            if (i == 1)
            {
                MessageBox.Show("Data Saved Successfully", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Data Cannot be Saved", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            
            purchasingview.DataSource = db.GetData("Select * from Attendance");
            txt_admin.Clear();
            cmb_status.Text = null;
            txt_comment.Clear();
            txt_member.Clear();
            txt_memid.Clear();
            EvDate.Text = DateTime.Now.ToString("yyyy-MM-dd");
        }

        private void Btn_clear_Click(object sender, EventArgs e)
        {
            txt_admin.Clear();
            cmb_status.Text = null;
            txt_comment.Clear();
            txt_member.Clear();
            txt_memid.Clear();
            EvDate.Text = DateTime.Now.ToString("yyyy-MM-dd");

        }

        private void Txt_memid_TextChanged(object sender, EventArgs e)
        {
            if (txt_memid.Text != "")
            {
                db.con.Open();
                db.cmd = new System.Data.SqlClient.SqlCommand("Select Admin,New_Member from AddMember where MemID=@MemID", db.con);
                db.cmd.Parameters.AddWithValue("@MemID",(txt_memid.Text));
                db.dr = db.cmd.ExecuteReader();
                while (db.dr.Read())
                {
                    txt_admin.Text = db.dr.GetValue(0).ToString();
                    txt_member.Text = db.dr.GetValue(1).ToString();
                }
                db.con.Close();

            }
        }

        [Obsolete]
        private void Guna2ControlBox1_Click(object sender, EventArgs e)
        {
            this.Hide();
            Home h = new Home();
            h.Show();
        }

        private void Purchasingview_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void Txt_member_KeyPress(object sender, KeyPressEventArgs e)
        {
            
        }
    }
}
