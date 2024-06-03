using DataGridView_Merge_Cells;
using System;
using System.Windows.Forms;

namespace Purchasing_Order
{
    public partial class Add_Member_Associate : Form
    {
        readonly Database db = new Database();
        readonly Supplier supplier = new Supplier();

        [Obsolete]
        public Add_Member_Associate()
        {
            InitializeComponent();
        }

        private void Txt_nic_TextChanged(object sender, EventArgs e)
        {

        }

        private void Add_Member_Associate_Load(object sender, EventArgs e)
        {
            txt_admin.Enabled = false;
            txt_member.Enabled = false;
            SupplierData.DataSource = db.GetData("Select * from AddMemberAssociate");
            SupplierData.Columns[0].Visible = false;


            var col = new DataGridViewMergedTextBoxColumn();
            const string field = "Admin";
            col.HeaderText = field;
            col.Name = field;
            col.DataPropertyName = field;
            int colidx = SupplierData.Columns[field].Index;
            SupplierData.Columns.Remove(field);
            SupplierData.Columns.Insert(colidx, col);

            var col1 = new DataGridViewMergedTextBoxColumn();
            const string field1 = "MemID";
            col1.HeaderText = field1;
            col1.Name = field1;
            col1.DataPropertyName = field1;
            int colidx1 = SupplierData.Columns[field1].Index;
            SupplierData.Columns.Remove(field1);
            SupplierData.Columns.Insert(colidx1, col1);

            var col2 = new DataGridViewMergedTextBoxColumn();
            const string field2 = "Member";
            col2.HeaderText = field2;
            col2.Name = field2;
            col2.DataPropertyName = field2;
            int colidx2 = SupplierData.Columns[field2].Index;
            SupplierData.Columns.Remove(field2);
            SupplierData.Columns.Insert(colidx2, col2);

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
                txt_memberid.AutoCompleteCustomSource = mycollection;

                db.con.Close();
                db.cmd.Dispose();
            }
            catch (Exception)
            {
                MessageBox.Show("Data Error", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            SupplierData.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            GetUsername();
            WinAPI.AnimateWindow(this.Handle, 500, WinAPI.BLEND);
        }


        public void GetUsername()
        {
            string userid = txt_memberid.Text;
            try
            {
                db.con.Open();
                db.cmd = new System.Data.SqlClient.SqlCommand("Select Count(MemID) from AddMemberAssociate where MemID = '" + txt_memberid.Text + "'", db.con);
                int i = Convert.ToInt32(db.cmd.ExecuteScalar());
                db.con.Close();
                i++;
                txt_itemid.Text = userid + i.ToString();
                txt_itemid2.Text = userid + i.ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Btn_save_Click(object sender, EventArgs e)
        {
            if (txt_memberid.Text.Trim().Equals(string.Empty))
            {
                MessageBox.Show("Member ID cannot be empty !!", "Incomplete Data Entry", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            string query = "Select * from AddMember where MemID = '" + txt_memberid.Text + "'";
            db.da = new System.Data.SqlClient.SqlDataAdapter(query, db.con);
            db.dt = new System.Data.DataTable();
            db.da.Fill(db.dt);
            if (db.dt.Rows.Count == 0)
            {
                MessageBox.Show("Member ID wasn't a Registered one!", "Unregistered", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            else if(txt_memberid.Text.Trim().Equals(string.Empty))
            {
                MessageBox.Show("Member ID cannot be empty !!", "Incomplete Data Entry", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            else if (txt_ma.Text.Trim().Equals(string.Empty))
            {
                MessageBox.Show("Member Associate cannot be empty !!", "Incomplete Data Entry", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            else if (txt_cn.Text.Trim().Equals(string.Empty))
            {
                MessageBox.Show("Contact Number cannot be empty !!", "Incomplete Data Entry", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            else if (txtnic.Text.Trim().Equals(string.Empty))
            {
                MessageBox.Show("NIC cannot be empty !!", "Incomplete Data Entry", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            else if (txt_address.Text.Trim().Equals(string.Empty))
            {
                MessageBox.Show("Address cannot be empty !!", "Incomplete Data Entry", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            else if (txt_sr.Text.Trim().Equals(string.Empty))
            {
                MessageBox.Show("Special Remarks cannot be empty !!", "Incomplete Data Entry", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            string queryy = "Insert into AddMemberAssociate values ('" + txt_admin.Text + "','" + txt_memberid.Text + "','" + txt_member.Text + "','" + txt_itemid.Text + "','" + txt_ma.Text + "','" + txt_cn.Text + "','" + txtnic.Text + "','" + txt_address.Text + "','" + txt_sr.Text + "')";
            int i = supplier.InsertItem(queryy);
            if (i == 1)
            {
                MessageBox.Show("Data Saved Successfully", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Data Cannot be Saved", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            GetUsername();
            SupplierData.DataSource = db.GetData("Select * from AddMemberAssociate");
            txt_memberid.Clear();
            txt_sr.Clear();
            txt_member.Clear();
            txtnic.Clear();
            txt_admin.Clear();
            txt_address.Clear();
            txt_cn.Clear();
            txt_ma.Clear();
        }

        private void Txt_memberid_TextChanged(object sender, EventArgs e)
        {
            GetUsername();


            if (txt_memberid.Text != "")
            {
                db.con.Open();
                db.cmd = new System.Data.SqlClient.SqlCommand("Select Admin,New_Member from AddMember where MemID=@MemID", db.con);
                db.cmd.Parameters.AddWithValue("@MemID", (txt_memberid.Text));
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

        private void SupplierData_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            txt_itemid.Text = SupplierData.CurrentRow.Cells["AssoMemID"].Value.ToString();
            txt_itemid2.Text = SupplierData.CurrentRow.Cells["AssoMemID"].Value.ToString();
            txt_admin.Text = SupplierData.CurrentRow.Cells["Admin"].Value.ToString();
            txt_memberid.Text = SupplierData.CurrentRow.Cells["MemID"].Value.ToString();
            txt_member.Text = SupplierData.CurrentRow.Cells["Member"].Value.ToString();
            txt_ma.Text = SupplierData.CurrentRow.Cells["AssoMember"].Value.ToString();
            txt_cn.Text = SupplierData.CurrentRow.Cells["CN"].Value.ToString();
            txtnic.Text = SupplierData.CurrentRow.Cells["NIC"].Value.ToString();
            txt_address.Text = SupplierData.CurrentRow.Cells["Address"].Value.ToString();
            txt_sr.Text = SupplierData.CurrentRow.Cells["Special_Remarks"].Value.ToString();
        }

        private void btn_clear_Click(object sender, EventArgs e)
        {
            txt_memberid.Clear();
            txt_sr.Clear();
            txt_member.Clear();
            txtnic.Clear();
            txt_admin.Clear();
            txt_address.Clear();
            txt_cn.Clear();
            txt_ma.Clear();
        }

        private void btn_update_Click(object sender, EventArgs e)
        {
            if (txt_memberid.Text.Trim().Equals(string.Empty))
            {
                MessageBox.Show("Member ID cannot be empty !!", "Incomplete Data Entry", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            else if (txt_ma.Text.Trim().Equals(string.Empty))
            {
                MessageBox.Show("Member Associate cannot be empty !!", "Incomplete Data Entry", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            else if (txt_cn.Text.Trim().Equals(string.Empty))
            {
                MessageBox.Show("Contact Number cannot be empty !!", "Incomplete Data Entry", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            else if (txtnic.Text.Trim().Equals(string.Empty))
            {
                MessageBox.Show("NIC cannot be empty !!", "Incomplete Data Entry", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            else if (txt_address.Text.Trim().Equals(string.Empty))
            {
                MessageBox.Show("Address cannot be empty !!", "Incomplete Data Entry", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            else if (txt_sr.Text.Trim().Equals(string.Empty))
            {
                MessageBox.Show("Special Remarks cannot be empty !!", "Incomplete Data Entry", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            string queryy = "Update AddMemberAssociate set AssoMember='" + txt_ma.Text + "',CN='" + txt_cn.Text + "',NIC='" + txtnic.Text + "',Address='" + txt_address.Text + "',Special_Remarks='" + txt_sr.Text + "' where NIC='" + txtnic.Text + "'";
            int i = supplier.UpdateItem(queryy);
            if (i == 1)
            {
                MessageBox.Show("Data Saved Updated", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Data Cannot be Updated", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            GetUsername();
            SupplierData.DataSource = db.GetData("Select * from AddMemberAssociate");
            txt_memberid.Clear();
            txt_sr.Clear();
            txt_member.Clear();
            txtnic.Clear();
            txt_admin.Clear();
            txt_address.Clear();
            txt_cn.Clear();
            txt_ma.Clear();
        }

        private void btn_delete_Click(object sender, EventArgs e)
        {
            if (txt_memberid.Text.Trim().Equals(string.Empty))
            {
                MessageBox.Show("Member ID cannot be empty !!", "Incomplete Data Entry", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            else if (txt_ma.Text.Trim().Equals(string.Empty))
            {
                MessageBox.Show("Member Associate cannot be empty !!", "Incomplete Data Entry", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            else if (txt_cn.Text.Trim().Equals(string.Empty))
            {
                MessageBox.Show("Contact Number cannot be empty !!", "Incomplete Data Entry", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            else if (txtnic.Text.Trim().Equals(string.Empty))
            {
                MessageBox.Show("NIC cannot be empty !!", "Incomplete Data Entry", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            else if (txt_address.Text.Trim().Equals(string.Empty))
            {
                MessageBox.Show("Address cannot be empty !!", "Incomplete Data Entry", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            else if (txt_sr.Text.Trim().Equals(string.Empty))
            {
                MessageBox.Show("Special Remarks cannot be empty !!", "Incomplete Data Entry", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            string queryy = "delete from AddMemberAssociate where NIC='" + txtnic.Text + "'";
            int i = supplier.DeleteItem(queryy);
            if (i == 1)
            {
                MessageBox.Show("Data Seleted Successfully", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Data Cannot be Deleted", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            GetUsername();
            SupplierData.DataSource = db.GetData("Select * from AddMemberAssociate");
            txt_memberid.Clear();
            txt_sr.Clear();
            txt_member.Clear();
            txtnic.Clear();
            txt_admin.Clear();
            txt_address.Clear();
            txt_cn.Clear();
            txt_ma.Clear();
        }
    }
}
