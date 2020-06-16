using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace Ecole_app.img
{
     class ado
    {
        //public SqlConnection con = new SqlConnection(@"Data Source=.\sqlexpress;Initial Catalog=Ecole;Integrated Security=True");
        public SqlConnection con = new SqlConnection(@"Data Source=.\sqlexpress;Initial Catalog=imagetest;Integrated Security=True");
        public SqlDataReader dr;
        public SqlCommand cmd = new SqlCommand();

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
