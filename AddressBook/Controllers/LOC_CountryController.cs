using AddressBook.Models;
using Microsoft.AspNetCore.Mvc;
using NuGet.Protocol.Plugins;
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
        public IActionResult Delete(int COUNTRYID)
        {
            string str = this.Configuration.GetConnectionString("myConnectionString");
            SqlConnection conn = new SqlConnection(str);
            conn.Open();
            SqlCommand cmd = conn.CreateCommand(); 
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "DELETELOC_Country";
            cmd.Parameters.AddWithValue("@COUNTRYID", COUNTRYID);
            cmd.ExecuteNonQuery();
            conn.Close();
            return RedirectToAction("Index");


        }
        public IActionResult Add(int? COUNTRYID)

        {
            if (COUNTRYID != null)
            {
                string str = this.Configuration.GetConnectionString("myConnectionString");
                SqlConnection conn = new SqlConnection(str);
                conn.Open();
                SqlCommand cmd = conn.CreateCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "SelectByPKLOC_Country";
                cmd.Parameters.AddWithValue("@COUNTRYID",COUNTRYID);
                DataTable dt = new DataTable();
                SqlDataReader sdr = cmd.ExecuteReader();
                dt.Load(sdr);
                LOC_CountryModel modelLOC_Country = new LOC_CountryModel();
                foreach (DataRow dr in dt.Rows)
                {
               
                    modelLOC_Country.COUNTRYCODE = dr["COUNTRYCODE"].ToString();
                    modelLOC_Country.COUNTRYNAME = dr["COUNTRYNAME"].ToString();
                    //modelLOC_Country.CREATED = Convert.ToDateTime(dr["CREATED"]);
                    //modelLOC_Country.MODIFIED = Convert.ToDateTime(dr["MODIFIED"]);
                }
                return View("LOC_CountryAddEdit", modelLOC_Country);
            }
            return View("LOC_CountryAddEdit");
        }
        [HttpPost]
        public IActionResult Save(LOC_CountryModel modelLOC_Country)
        {
            string str = this.Configuration.GetConnectionString("myConnectionString");
            SqlConnection conn = new SqlConnection(str);
            conn.Open();
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            if(modelLOC_Country.COUNTRYID == null)
            {
                cmd.CommandText = "InsertLOC_Country";
               
            }
            else
            {
                cmd.CommandText = "UPDATELOC_Country";
                cmd.Parameters.AddWithValue("@COUNTRYID", modelLOC_Country.COUNTRYID);
            }
            cmd.Parameters.AddWithValue("@COUNTRYNAME", modelLOC_Country.COUNTRYNAME);
            cmd.Parameters.AddWithValue("@COUNTRYCODE", modelLOC_Country.COUNTRYCODE);
           // cmd.Parameters.AddWithValue("@MODIFIED", DateTimeOffset.Now);

            cmd.ExecuteNonQuery();
            
            conn.Close();

            return RedirectToAction("Index");
        }
       
    }
}
