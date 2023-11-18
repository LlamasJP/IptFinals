using System.Data;
using System.Data.SqlClient;

namespace IptFinals.Models
{
    public class Identification
    {
        string constr = "Data Source=DESKTOP-FAE0OA1; Initial Catalog=IptFinalsDB;User ID=sa; Password=123456789";
        public DataTable GetStudentPersonalInfo()
        {
            var dt = new DataTable();
            using (SqlConnection con = new SqlConnection(constr))
            {
                SqlCommand cmd = new SqlCommand("SPGetStudentPersonalInfo", con);
                cmd.CommandType = CommandType.StoredProcedure;
                con.Open();
                SqlDataAdapter  da = new SqlDataAdapter(cmd);
                da.Fill(dt);
                con.Close();
            }
            return dt;
        }
    }
}
