using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Survey_Application.Models;

namespace Survey_Application.Controllers
{
    public class SurveyController : Controller
    {
        int[] RadioBtnVals = { 0, 0, 0, 0, 0, 0, 0 };
        SqlConnection connection = new SqlConnection("Data Source=ISBLT-8524;Initial Catalog=SurveyAppDB;Integrated Security=True");
        

        protected void Page_Load(object sender, EventArgs e)
        {
            /*connection.ConnectionString = "Data Source=ISBLT-8524;Initial Catalog=SurveyAppDB;Integrated Security=True";*/
            for (int i = 0; i <= 6; i++)
            {
                RadioBtnVals[i] = -1;
            }
        }

        
        // GET: Survey

        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult SubmitForm()
        {
            SetValues();
            string success = AddSurveyRecord();
            Console.WriteLine(success);
            return View("ThankyouPage");
        }

        public void SetValues()
        {
            RadioBtnVals[0] = Convert.ToInt32(Request.Form["rating1"]);
            RadioBtnVals[1] = Convert.ToInt32(Request.Form["rating2"]);
            RadioBtnVals[2] = Convert.ToInt32(Request.Form["rating3"]);
            RadioBtnVals[3] = Convert.ToInt32(Request.Form["rating4"]);
            RadioBtnVals[4] = Convert.ToInt32(Request.Form["rating5"]);
            RadioBtnVals[5] = Convert.ToInt32(Request.Form["rating6"]);
            RadioBtnVals[6] = Convert.ToInt32(Request.Form["rating7"]);
        }

        [HttpGet]
        public ActionResult Welcomepage()
        {
            
            return View();
        }


        [HttpPost]
        public ActionResult GoToIndex()
        {
            TempData["Name"] = Request.Form["fname"];
            ViewBag.Username = Request.Form["fname"];
            Console.WriteLine("123");
            return View("Index");
        }

        public ActionResult Thankyoupage()
        {
            return View();
        }

        public string AddSurveyRecord()
        {
            DateTime dateTime = DateTime.Now;



            SqlCommand com = new SqlCommand("SELECT MAX(CustomerID) as LastID FROM[SurveyAppDB].[dbo].[tbl_Surveys]", connection);
            connection.Open();
            string lastCustomer = com.ExecuteScalar().ToString();
            connection.Close();

            int lastCustomerID = Convert.ToInt32(lastCustomer);

                for ( int i = 0; i < 7; i++)
                {
                    SqlCommand cmd = new SqlCommand("dbo.AddNewSurveys", connection);
                    cmd.CommandType = CommandType.StoredProcedure;

                    connection.Open();

                    cmd.Parameters.AddWithValue("@CustomerID", lastCustomerID+1);
                    cmd.Parameters.AddWithValue("@QuestionID", i + 1);
                    cmd.Parameters.AddWithValue("@Response", RadioBtnVals[i]);
                    cmd.Parameters.AddWithValue("@CreateDate", dateTime);

                    cmd.ExecuteNonQuery();
                    connection.Close();
            }

              
                
                return ("Data save Successfully");
            /*}
            catch (Exception ex)
            {
                if (connection.State == ConnectionState.Open)
                {
                    connection.Close();
                }
                return (ex.Message.ToString());
            }*/
        }

    }
}