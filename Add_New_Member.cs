using DataGridView_Merge_Cells;
using System;
using System.Windows.Forms;

namespace Purchasing_Order
{
    public partial class Add_New_Member : Form
    {
        [Obsolete]
        public Add_New_Member()
        {
            InitializeComponent();
        }
        readonly Database db = new Database();
        readonly Supplier supplier = new Supplier();
        private void Supplier_Items_Load(object sender, EventArgs e)
        {
            SupplierData.DataSource = db.GetData("Select * from AddMember");
            SupplierData.Columns[0].Visible = false;
            

            var col = new DataGridViewMergedTextBoxColumn();
            const string field = "Admin";
            col.HeaderText = field;
            col.Name = field;
            col.DataPropertyName = field;
            int colidx = SupplierData.Columns[field].Index;
            SupplierData.Columns.Remove(field);
            SupplierData.Columns.Insert(colidx, col);

            try
            {
                db.cmd = new System.Data.SqlClient.SqlCommand("SELECT AdminID FROM Admins", db.con);
                db.con.Open();
                db.dr = db.cmd.ExecuteReader();
                AutoCompleteStringCollection mycollection = new AutoCompleteStringCollection();
                while (db.dr.Read())
                {
                    mycollection.Add(db.dr.GetString(0));
                }
                txt_admin.AutoCompleteCustomSource = mycollection;
                
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
            string userid = txt_admin.Text;
            try
            {
                db.con.Open();
                db.cmd = new System.Data.SqlClient.SqlCommand("Select Count(MemID) from AddMember where Admin = '" + txt_admin.Text + "'", db.con);
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

        [Obsolete]
        private void Guna2ControlBox1_Click(object sender, EventArgs e)
        {
            this.Hide();
            Home h = new Home();
            h.Show();
        }

        private void Btn_clear_Click_1(object sender, EventArgs e)
        {
            txt_admin.Clear();
            txt_member.Clear();
            txt_sr.Clear();
            txt_address.Clear();
            txt_nic.Clear();
            txt_cn.Clear();
        }

        private void Btn_save_Click(object sender, EventArgs e)
        {
            if (txt_admin.Text.Trim().Equals(string.Empty))
            {
                MessageBox.Show("Admin cannot be empty !!", "Incomplete Data Entry", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            string query = "Select * from Admins where AdminID = '" + txt_admin.Text + "'";
            db.da = new System.Data.SqlClient.SqlDataAdapter(query, db.con);
            db.dt = new System.Data.DataTable();
            db.da.Fill(db.dt);
            if (db.dt.Rows.Count == 0)
            {
                MessageBox.Show("Admin wasn't a Registered one!", "Unregistered", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            else if (txt_member.Text.Trim().Equals(string.Empty))
            {
                MessageBox.Show("New Member cannot be empty !!", "Incomplete Data Entry", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            else if (txt_cn.Text.Trim().Equals(string.Empty))
            {
                MessageBox.Show("Contact Number cannot be empty !!", "Incomplete Data Entry", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            else if (txt_nic.Text.Trim().Equals(string.Empty))
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
            string queryy = "Insert into AddMember values ('" + txt_admin.Text + "','" + txt_itemid.Text + "','" + txt_member.Text + "','" + txt_cn.Text + "','" + txt_nic.Text + "','" + txt_address.Text + "','" + txt_sr.Text + "')";
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
            SupplierData.DataSource = db.GetData("Select * from AddMember");
            txt_member.Clear();
            txt_sr.Clear();
            txt_admin.Clear();
            txt_address.Clear();
            txt_nic.Clear();
            txt_cn.Clear();
        }

        private void Btn_update_Click(object sender, EventArgs e)
        {
            if (txt_admin.Text.Trim().Equals(string.Empty))
            {
                MessageBox.Show("Admin cannot be empty !!", "Incomplete Data Entry", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            else if (txt_member.Text.Trim().Equals(string.Empty))
            {
                MessageBox.Show("Item cannot be empty !!", "Incomplete Data Entry", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            else if (txt_cn.Text.Trim().Equals(string.Empty))
            {
                MessageBox.Show("Contact Number cannot be empty !!", "Incomplete Data Entry", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            else if (txt_nic.Text.Trim().Equals(string.Empty))
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
            string queryy = "Update AddMember set New_member='" + txt_member.Text + "',CN='" + txt_cn.Text + "',NIC='" + txt_nic.Text + "',Address='" + txt_address.Text + "',Special_Remarks='" + txt_sr.Text + "' where NIC='" + txt_nic.Text + "'";
            int i = supplier.UpdateItem(queryy);
            if (i == 1)
            {
                MessageBox.Show("Data Saved Updated", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Data Cannot be Updated", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            SupplierData.DataSource = db.GetData("Select * from AddMember");
            txt_admin.Clear();
            txt_member.Clear();
            txt_address.Clear();
            txt_nic.Clear();
            txt_cn.Clear();
            txt_sr.Clear();
        }

        private void Txt_member_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void SupplierData_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
        {
            txt_itemid.Text = SupplierData.CurrentRow.Cells["MemID"].Value.ToString();
            txt_itemid2.Text = SupplierData.CurrentRow.Cells["MemID"].Value.ToString();
            txt_admin.Text = SupplierData.CurrentRow.Cells["Admin"].Value.ToString();
            txt_member.Text = SupplierData.CurrentRow.Cells["New_member"].Value.ToString();
            txt_cn.Text = SupplierData.CurrentRow.Cells["CN"].Value.ToString();
            txt_nic.Text = SupplierData.CurrentRow.Cells["NIC"].Value.ToString();
            txt_address.Text = SupplierData.CurrentRow.Cells["Address"].Value.ToString();
            txt_sr.Text = SupplierData.CurrentRow.Cells["Special_Remarks"].Value.ToString();
        }

        private void Btn_delete_Click(object sender, EventArgs e)
        {
            if (txt_admin.Text.Trim().Equals(string.Empty))
            {
                MessageBox.Show("Admin cannot be empty !!", "Incomplete Data Entry", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            else if (txt_member.Text.Trim().Equals(string.Empty))
            {
                MessageBox.Show("Item cannot be empty !!", "Incomplete Data Entry", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            else if (txt_cn.Text.Trim().Equals(string.Empty))
            {
                MessageBox.Show("Contact Number cannot be empty !!", "Incomplete Data Entry", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            else if (txt_nic.Text.Trim().Equals(string.Empty))
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
            string queryy = "delete from AddMember where NIC='" + txt_nic.Text + "'";
            int i = supplier.DeleteItem(queryy);
            if (i == 1)
            {
                MessageBox.Show("Data Seleted Successfully", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Data Cannot be Deleted", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            SupplierData.DataSource = db.GetData("Select * from AddMember");
            txt_admin.Clear();
            txt_member.Clear();
            txt_address.Clear();
            txt_nic.Clear();
            txt_cn.Clear();
            txt_sr.Clear();
        }

        private void Txt_admin_TextChanged(object sender, EventArgs e)
        {
            GetUsername();
        }

        private void Txt_cn_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && e.KeyChar != (char)Keys.Back)
            {
                // Prevent the keypress by setting Handled to true
                e.Handled = true;
            }
        }
    }
}

