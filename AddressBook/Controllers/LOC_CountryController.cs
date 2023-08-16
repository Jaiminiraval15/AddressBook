using AddressBook.Models;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Data.SqlClient;
using System.Reflection;

namespace AddressBook.Controllers
{
    public class LOC_CountryController : Controller
    { 
        private IConfiguration Configuration;
        public LOC_CountryController(IConfiguration _configuration)
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
            cmd.CommandText = "SELECTALLOC_Country";
            DataTable dt = new DataTable();
            SqlDataReader sdr = cmd.ExecuteReader();
            dt.Load(sdr); 
            

            return View("LOC_CountryList",dt);
        }
        public IActionResult Delete(int ID)
        {
            string str = this.Configuration.GetConnectionString("myConnectionString");
            SqlConnection conn = new SqlConnection(str);
            conn.Open();
            SqlCommand cmd = conn.CreateCommand(); 
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "DELETELOC_Country";
            cmd.Parameters.AddWithValue("@ID", ID);
            cmd.ExecuteNonQuery();
            conn.Close();
            return RedirectToAction("Index");


        }
        [HttpPost]
        public IActionResult Save(LOC_CountryModel modelLOC_Country)
        {
            string str = this.Configuration.GetConnectionString("myConnectionString");
            SqlConnection conn = new SqlConnection(str);
            conn.Open();
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "InsertLOC_Country";
            cmd.Parameters.Add("@COUNTRYNAME", SqlDbType.VarChar).Value = modelLOC_Country.COUNTRYNAME;
            cmd.Parameters.Add("@COUNTRYCODE", SqlDbType.VarChar).Value = modelLOC_Country.COUNTRYCODE;
            cmd.Parameters.Add("@CREATED", SqlDbType.Date).Value = modelLOC_Country.CREATED;
            cmd.Parameters.Add("@MODIFIED", SqlDbType.Date).Value = modelLOC_Country.MODIFIED;

            cmd.ExecuteNonQuery();
        
            conn.Close();

            return View("LOC_CountryAddEdit");
        }
        public IActionResult Add()
        {
            return View("LOC_CountryAddEdit");
        }
    }
}
