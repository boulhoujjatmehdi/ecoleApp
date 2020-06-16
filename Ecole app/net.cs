using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;


namespace Ecole_app
{
    class net
    {

        //public SqlConnection con = new SqlConnection(@"Data Source=.\sqlexpress;Initial Catalog=Ecole;Integrated Security=True");
        public SqlConnection con = new SqlConnection(@"Data Source=.\sqlexpress;Initial Catalog=Ecole;Integrated Security=True");
        public SqlDataReader dr;
        public SqlCommand cmd = new SqlCommand();
        public DataTable dt = new DataTable();

        public void connecter()
        {
            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }
        }
        public void deconnecter()
        {
            if (con.State == ConnectionState.Open)
            {
                con.Close();
            }
        }
    }
}
