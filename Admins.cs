using System;
using System.Data;
using System.Windows.Forms;

namespace Purchasing_Order
{
    public partial class Admins : Form
    {
        [Obsolete]
        public Admins()
        {
            InitializeComponent();
        }
        readonly Database db = new Database();
        readonly Supplier sp = new Supplier();
        
        public void SupplierDataLoad()
        {
            GetID();
            GetNum();
            SupplierData.DataSource = db.GetData("Select * from Admins");
        }

        private void Suppliers_Load(object sender, EventArgs e)
        {
            SupplierDataLoad();
            SupplierData.Columns[0].Visible = false;
            WinAPI.AnimateWindow(this.Handle, 500, WinAPI.BLEND);
        }

        
        
        private void Txt_supplier_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txt_adminid.Focus();
            }
        }

        private void Txt_address_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btn_save.Focus();
            }
        }

        

        public void GetID()
        {
            try
            {
                string usid;
                string query = "select Adid from Admins order by Adid desc";
                db.con.Open();
                db.cmd = new System.Data.SqlClient.SqlCommand(query, db.con);
                db.dr = db.cmd.ExecuteReader();
                if (db.dr.Read())
                {
                    int id = int.Parse(db.dr[0].ToString()) + 1;
                    usid = id.ToString("00000");
                }
                else if (Convert.IsDBNull(db.dr))
                {
                    usid = ("00001");
                }
                else
                {
                    usid = ("00001");
                }
                db.con.Close();
                db.cmd.Dispose();
                txt_adid.Text = usid.ToString();
            }
            catch (Exception)
            {
                MessageBox.Show("Data Error", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void GetNum()
        {
            try
            {
                string usid;
                string query = "select Admin_Number from Admins order by Admin_Number desc";
                db.con.Open();
                db.cmd = new System.Data.SqlClient.SqlCommand(query, db.con);
                db.dr = db.cmd.ExecuteReader();
                if (db.dr.Read())
                {
                    int id = int.Parse(db.dr[0].ToString()) + 1;
                    usid = id.ToString("00000");
                }
                else if (Convert.IsDBNull(db.dr))
                {
                    usid = ("00001");
                }
                else
                {
                    usid = ("00001");
                }
                db.con.Close();
                db.cmd.Dispose();
                txt_adminnum.Text = usid.ToString();
            }
            catch (Exception)
            {
                MessageBox.Show("Data Error", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void SupplierData_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            txt_adid.Text = SupplierData.CurrentRow.Cells["Adid"].Value.ToString();
            txt_adminnum.Text = SupplierData.CurrentRow.Cells["Admin_Number"].Value.ToString();
            txt_adminid.Text = SupplierData.CurrentRow.Cells["AdminID"].Value.ToString();

        }

        [Obsolete]
        private void Guna2ControlBox1_Click_1(object sender, EventArgs e)
        {
            
        }

        private void Btn_update_Click_1(object sender, EventArgs e)
        {
            try
            {
                if (txt_adminnum.Text.Trim().Equals(string.Empty))
                {
                    MessageBox.Show("Supplier cannot be empty !!", "Incomplete Data Entry", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                else if (txt_adminid.Text.Trim().Equals(string.Empty))
                {
                    MessageBox.Show("Address cannot be empty !!", "Incomplete Data Entry", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                

                
                int v = Convert.ToInt32(txt_adid.Text);
                int i = sp.SupplierUpdate(txt_adminid.Text, v);
                SupplierDataLoad();
                if (i == 1)
                {
                    MessageBox.Show("Data Updated Successfully", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Data cannot be Updated", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Please check data again", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            
            txt_adminid.Clear();
            
        }

        private void Btn_delete_Click_1(object sender, EventArgs e)
        {
            try
            {
                if (txt_adminnum.Text.Trim().Equals(string.Empty))
                {
                    MessageBox.Show("Supplier cannot be empty !!", "Incomplete Data Entry", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                else if (txt_adminid.Text.Trim().Equals(string.Empty))
                {
                    MessageBox.Show("Address cannot be empty !!", "Incomplete Data Entry", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                

                int d = Convert.ToInt32(txt_adid.Text);
                int i = sp.SupplierDelete(d);
                SupplierDataLoad();
                if (i == 1)
                {
                    MessageBox.Show("Data Deleted Successfully", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Data Cannot be deleted", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Please check data again", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
           
            txt_adminid.Clear();
            
        }

        private void Btn_save_Click_1(object sender, EventArgs e)
        {
            try
            {
                db.cmd = new System.Data.SqlClient.SqlCommand("SELECT * FROM Admins WHERE Admin_Number = '" + txt_adminnum.Text + "'", db.con);
                db.da = new System.Data.SqlClient.SqlDataAdapter(db.cmd);
                DataSet ds = new DataSet();
                db.da.Fill(ds);
                int s = ds.Tables[0].Rows.Count;
                if (s > 0)
                {
                    MessageBox.Show("Admin_Number : " + txt_adminnum.Text + " Already Exists!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Please try again", "error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            try
            {
                db.cmd = new System.Data.SqlClient.SqlCommand("SELECT * FROM Admins WHERE AdminID = '" + txt_adminid.Text + "'", db.con);
                db.da = new System.Data.SqlClient.SqlDataAdapter(db.cmd);
                DataSet dss = new DataSet();
                db.da.Fill(dss);
                int v = dss.Tables[0].Rows.Count;
                if (v > 0)
                {
                    MessageBox.Show("AdminID : " + txt_adminid.Text + " Already Exists!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Please try again", "error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            
            


            if (txt_adminnum.Text.Trim().Equals(string.Empty))
            {
                MessageBox.Show("Supplier cannot be empty !!", "Incomplete Data Entry", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            else if (txt_adminid.Text.Trim().Equals(string.Empty))
            {
                MessageBox.Show("Address cannot be empty !!", "Incomplete Data Entry", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            

            try
            {
                int b = Convert.ToInt32(txt_adid.Text);
                int q = Convert.ToInt32(txt_adminnum.Text);
                int i = sp.SupplierInsert(b, q, txt_adminid.Text);
                if (i == 1)
                {
                    MessageBox.Show("Data saved Successfully", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    GetNum();
                }
                else
                {
                    MessageBox.Show("Data Cannot be Saved", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Data Cannot be Duplicated", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }


            try
            {
                SupplierDataLoad();
            }

            catch (Exception)
            {
                MessageBox.Show("Please check data again", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            txt_adminid.Clear();
            
        }
    }
}
