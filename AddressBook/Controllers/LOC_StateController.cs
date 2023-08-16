using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;
using System.Data;

namespace AddressBook.Controllers
{
    public class LOC_StateController : Controller
    {
        private IConfiguration Configuration;
        public LOC_StateController(IConfiguration _configuration)
        {
            Configuration = _configuration;
        }
        

        public IActionResult Index()
        {
            string str = this.Configuration.GetConnectionString("myConnectionString");
            SqlConnection conn = new SqlConnection(str);
            conn.Open();
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "SELECTALLOC_State";
            DataTable dt = new DataTable();
            SqlDataReader sdr = cmd.ExecuteReader();
            dt.Load(sdr);
            return View("LOC_StateList",dt);
        }
        public IActionResult Delete(int ID)
        {
            string str = this.Configuration.GetConnectionString("myConnectionString");
            SqlConnection conn = new SqlConnection(str);
            conn.Open();
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "DELETELOC_State";
            cmd.Parameters.AddWithValue("@ID", ID);
           cmd.ExecuteNonQuery();
            conn.Close();
            return RedirectToAction("Index");
        }
    }
}
