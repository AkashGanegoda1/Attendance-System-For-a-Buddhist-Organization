using System;
using System.Windows.Forms;

namespace Purchasing_Order
{
    public partial class Events : Form
    {

        [Obsolete]
        public Events()
        {
            InitializeComponent();
        }

        readonly Database db = new Database();
        private readonly Supplier supplier = new Supplier();
        private void Report_form_Load(object sender, EventArgs e)
        {
            EventData.DataSource = db.GetData("Select * from NewEvent");
            EvDate.Text = DateTime.Now.ToString("d");
            GetID();
            EventData.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            WinAPI.AnimateWindow(this.Handle, 500, WinAPI.BLEND);
        }

        [Obsolete]
        private void Guna2ControlBox1_Click(object sender, EventArgs e)
        {

        }



        private void Btn_save_Click(object sender, EventArgs e)
        {
            if (txt_event.Text.Trim().Equals(string.Empty))
            {
                MessageBox.Show("Event cannot be empty !!", "Incomplete Data Entry", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            string queryy = "Insert into NewEvent values ('" + txt_eventid.Text + "','" + EvDate.Text + "','" + txt_event.Text + "')";
            int i = supplier.ADDEVENT(queryy);
            if (i == 1)
            {
                MessageBox.Show("Data Saved Successfully", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Data Cannot be Saved", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            GetID();
            EventData.DataSource = db.GetData("Select * from NewEvent");
            txt_event.Clear();

        }

        public void GetID()
        {
            try
            {
                string usid;
                string query = "select Event_id from NewEvent order by Event_id desc";
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
                txt_eventid.Text = usid.ToString();
            }
            catch (Exception)
            {
                MessageBox.Show("Data Error", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Btn_update_Click(object sender, EventArgs e)
        {
            if (txt_event.Text.Trim().Equals(string.Empty))
            {
                MessageBox.Show("Event cannot be empty !!", "Incomplete Data Entry", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }


            string queryy = "Update NewEvent set Event_Date='" + EvDate.Text + "',Event='" + txt_event.Text + "' where Event_id='" + txt_eventid.Text + "'";
            int i = supplier.UPDATEEVENT(queryy);
            if (i == 1)
            {
                MessageBox.Show("Data Saved Updated", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Data Cannot be Updated", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            EventData.DataSource = db.GetData("Select * from NewEvent");
            txt_event.Clear();
        }


        private void Btn_delete_Click(object sender, EventArgs e)
        {
            if (txt_event.Text.Trim().Equals(string.Empty))
            {
                MessageBox.Show("Event cannot be empty !!", "Incomplete Data Entry", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            string queryy = "delete from NewEvent where Event_id='" + txt_eventid.Text + "'";
            int i = supplier.DELEVENT(queryy);
            if (i == 1)
            {
                MessageBox.Show("Data Seleted Successfully", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Data Cannot be Deleted", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            EventData.DataSource = db.GetData("Select * from NewEvent");
            txt_event.Clear();
            EvDate.Text = DateTime.Now.ToString("d");
        }

        private void EventData_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
        {
            txt_eventid.Text = EventData.CurrentRow.Cells["Event_id"].Value.ToString();
            EvDate.Text = EventData.CurrentRow.Cells["Event_Date"].Value.ToString();
            txt_event.Text = EventData.CurrentRow.Cells["Event"].Value.ToString();
        }

        private void Btn_clear_Click(object sender, EventArgs e)
        {
            GetID();
            txt_event.Clear();
        }
    }
}
    

