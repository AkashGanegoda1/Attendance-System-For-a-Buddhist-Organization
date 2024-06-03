namespace Purchasing_Order
{
    class Supplier
    {
        readonly Database db = new Database();

        public string AdID;
        public string Admin_Number;
        public string AdminID;
        

        public int SupplierInsert(int Adid, int Admin_number, string Adminid)
        {

            AdID = Adid.ToString();
            Admin_Number = Admin_number.ToString();
            AdminID = Adminid;
 
            db.con.Open();
            db.cmd = new System.Data.SqlClient.SqlCommand("Insert into Admins values (@a,@b,@c) ", db.con);
            db.cmd.Parameters.AddWithValue("a", Adid);
            db.cmd.Parameters.AddWithValue("b", Admin_number);
            db.cmd.Parameters.AddWithValue("c", Adminid);
            int i = db.cmd.ExecuteNonQuery();
            db.con.Close();
            db.cmd.Dispose();
            return i;
        }
        public int SupplierUpdate(string Adminid, int Adid)
        {

            AdID = Adid.ToString();
            AdminID = Adminid;

            db.con.Open();
            db.cmd = new System.Data.SqlClient.SqlCommand("update Admins set AdminID=@a where Adid=@e", db.con);
            db.cmd.Parameters.AddWithValue("a", Adminid);
            db.cmd.Parameters.AddWithValue("e", Adid);
            int i = db.cmd.ExecuteNonQuery();
            db.con.Close();
            db.cmd.Dispose();
            return i;

        }

        public int SupplierDelete(int adid)
        {
            AdID = adid.ToString();


            db.con.Open();
            db.cmd = new System.Data.SqlClient.SqlCommand("delete from Admins where Adid=@a", db.con);
            db.cmd.Parameters.AddWithValue("a", adid);
            int i = db.cmd.ExecuteNonQuery();
            db.con.Close();
            db.cmd.Dispose();
            return i;


        }
        public int InsertItem(string a)
        {
            db.con.Open();
            db.cmd = new System.Data.SqlClient.SqlCommand(a, db.con);
            int i = db.cmd.ExecuteNonQuery();
            db.con.Close();
            db.cmd.Dispose();
            return i;
        }
        public int UpdateItem(string a)
        {
            db.con.Open();
            db.cmd = new System.Data.SqlClient.SqlCommand(a, db.con);
            int i = db.cmd.ExecuteNonQuery();
            db.con.Close();
            db.cmd.Dispose();
            return i;
        }
        public int DeleteItem(string a)
        {
            db.con.Open();
            db.cmd = new System.Data.SqlClient.SqlCommand(a, db.con);
            int i = db.cmd.ExecuteNonQuery();
            db.con.Close();
            db.cmd.Dispose();
            return i;
        }


        public int ADDEVENT(string a)
        {
            db.con.Open();
            db.cmd = new System.Data.SqlClient.SqlCommand(a, db.con);
            int i = db.cmd.ExecuteNonQuery();
            db.con.Close();
            db.cmd.Dispose();
            return i;
        }

        public int UPDATEEVENT(string a)
        {
            db.con.Open();
            db.cmd = new System.Data.SqlClient.SqlCommand(a, db.con);
            int i = db.cmd.ExecuteNonQuery();
            db.con.Close();
            db.cmd.Dispose();
            return i;
        }

        public int DELEVENT(string a)
        {
            db.con.Open();
            db.cmd = new System.Data.SqlClient.SqlCommand(a, db.con);
            int i = db.cmd.ExecuteNonQuery();
            db.con.Close();
            db.cmd.Dispose();
            return i;
        }
    }
}
