using GMap.NET.MapProviders;
using System;
using System.Data;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace Purchasing_Order
{
    public partial class Home : Form
    {

        readonly Database db = new Database();
        [Obsolete]
        public Home()
        {
            InitializeComponent();
            Summaryview.CellFormatting += Summaryview_CellFormatting_2;
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            Summaryview.BorderStyle = BorderStyle.None;
            Summaryview.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(238, 239, 249);
            Summaryview.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            Summaryview.DefaultCellStyle.SelectionBackColor = Color.DarkTurquoise;
            Summaryview.DefaultCellStyle.SelectionForeColor = Color.WhiteSmoke;
            Summaryview.BackgroundColor = Color.White;

            Summaryview.EnableHeadersVisualStyles = false;
            Summaryview.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            Summaryview.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(20, 25, 72);
            Summaryview.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;

            Summaryview.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            AttendanceCal22();
            Hehe();
            label2.Text = Login.Cmbval;
            Summaryview.ClearSelection();
            WinAPI.AnimateWindow(this.Handle, 500, WinAPI.BLEND);
        }
       

        public void Hehe()
        {
            try
            {
                db.con.Open();
                db.cmd = new System.Data.SqlClient.SqlCommand("SELECT Event_id,Event_Date, Event FROM NewEvent", db.con);
                db.cmd.ExecuteNonQuery();
                db.dr = db.cmd.ExecuteReader();
                while (db.dr.Read())
                {
                    string notificationDate = (string)db.dr["Event"].ToString();
                    DateTime notifydate = (DateTime)db.dr["Event_Date"];
                    if (notifydate <= DateTime.Now)
                    {
                        //Nothing Notify
                    }
                    else
                    {
                        TimeSpan timeRemaining = notifydate - DateTime.Now.AddDays(2);
                        TimeSpan timeRemainingg = notifydate - DateTime.Now.AddDays(1);
                        if (timeRemainingg <= TimeSpan.Zero)
                        {
                            notifyIcon1.Icon = new System.Drawing.Icon(Path.GetFullPath("4165012.ico"));
                            notifyIcon1.Text = "some text";
                            notifyIcon1.Visible = true;
                            notifyIcon1.BalloonTipTitle = notificationDate;
                            notifyIcon1.BalloonTipText = "Event in 1 Day";
                            notifyIcon1.ShowBalloonTip(10000);

                        }
                        else if (timeRemaining <= TimeSpan.Zero)
                        {
                            notifyIcon1.Icon = new System.Drawing.Icon(Path.GetFullPath("4165012.ico"));
                            notifyIcon1.Text = "some text";
                            notifyIcon1.Visible = true;
                            notifyIcon1.BalloonTipTitle = notificationDate;
                            notifyIcon1.BalloonTipText = "Event in 2 Days";
                            notifyIcon1.ShowBalloonTip(10000);
                            return;
                        }


                    }

                }
                db.con.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            
            


        }

        private Form activeform = null;
        private void OpenChildForm(Form ChildForm)
        {
            activeform?.Close();
            activeform = ChildForm;
            ChildForm.TopLevel = false;
            panel.Controls.Add(ChildForm);
            panel.Tag = ChildForm;
            ChildForm.BringToFront();
            ChildForm.Show();
        }
  

        
        

        [Obsolete]
        private void Btn_saves_Click_1(object sender, EventArgs e)
        {
            this.Hide();
            Attendance attendance = new Attendance();
            attendance.Show();
        }

        [Obsolete]
        private void Btn_supplier_Click_1(object sender, EventArgs e)
        {
            OpenChildForm(new Admins());
        }

        [Obsolete]
        private void Btn_supplieritem_Click(object sender, EventArgs e)
        {
            this.Hide();
            Add_New_Member add = new Add_New_Member();
            add.Show();
        }

        [Obsolete]
        private void Btn_report_Click_1(object sender, EventArgs e)
        {
            OpenChildForm(new Events());
        }

        [Obsolete]
        private void Guna2Button5_Click(object sender, EventArgs e)
        {
            this.Hide();
            Login l = new Login();
            l.Show();
        }
        

        public void AttendanceCal22()
        {
            db.con.Open();

            // Create a SQL query to calculate the counts.
            string query = @"SELECT
    MemID,
    Member,
    SUM(CASE WHEN Astatus = 'Present' THEN 1 ELSE 0 END) AS PresentCount,
    SUM(CASE WHEN Astatus = 'Absent' THEN 1 ELSE 0 END) AS AbsentCount,
    COUNT(DISTINCT Date) AS DateCount,
    CONCAT(
        ROUND(
            (SUM(CASE WHEN Astatus = 'Present' THEN 1 ELSE 0 END) * 100) / NULLIF(COUNT(DISTINCT Date), 0),
            2
        ),
        '%'
    ) AS Attendance
FROM
    Attendance
GROUP BY
    MemID,
    Member;";
                    

            using (db.cmd = new System.Data.SqlClient.SqlCommand(query, db.con))
            using (db.da = new System.Data.SqlClient.SqlDataAdapter(db.cmd))
            {
                DataTable resultTable = new DataTable();
                db.da.Fill(resultTable);

                // Display the result in a DataGridView.
                Summaryview.DataSource = resultTable;
            }
            db.con.Close();
        }


        private void Summaryview_CellFormatting_2(object sender, DataGridViewCellFormattingEventArgs e)
        {
            // Check if we are formatting the "Attendance" column and it's not a header cell.
            if (e.ColumnIndex == Summaryview.Columns["Attendance"].Index && e.RowIndex >= 0)
            {
                if (e.Value != null && !Convert.IsDBNull(e.Value))
                {
                    string attendancePercentageStr = e.Value.ToString();

                    // Remove the '%' sign from the attendance percentage string.
                    attendancePercentageStr = attendancePercentageStr.Replace("%", "");

                    if (double.TryParse(attendancePercentageStr, out double attendancePercentage))
                    {
                        // Set the background color based on the attendance percentage.
                        if (attendancePercentage >= 80)
                        {
                            e.CellStyle.BackColor = Color.Green;
                        }
                        else if (attendancePercentage >= 60)
                        {
                            e.CellStyle.BackColor = Color.Yellow;
                        }
                        else
                        {
                            e.CellStyle.BackColor = Color.Red;
                        }
                    }
                }
                else
                {
                    // Handle DBNull values (e.g., set a default value or leave it as is).
                    // For example, you can set a gray background for DBNull values:
                    e.CellStyle.BackColor = Color.White;
                }
            }
        }

        [Obsolete]
        private void Btn_addmemasso_Click(object sender, EventArgs e)
        {
            this.Hide();
            Add_Member_Associate ams = new Add_Member_Associate();
            ams.Show();
        }
    }
}
       
    

