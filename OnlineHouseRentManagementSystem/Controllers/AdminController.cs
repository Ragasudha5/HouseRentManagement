using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using OnlineHouseRentManagementSystem.Models;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;

namespace OnlineHouseRentManagementSystem.Controllers
{
    public class AdminController : Controller
    {
        // GET: Admin
        DBuserSignupLoginEntities db = new DBuserSignupLoginEntities();
        public ActionResult Index()
        {
            string mainconn = ConfigurationManager.ConnectionStrings["Myconnection"].ConnectionString;
            SqlConnection sqlconn = new SqlConnection(mainconn);

            string sqlquery = "select * from [dbo].[TBLAdminInfo] where Email='" + Session["EmailSS"] + "'";
            SqlCommand sqlcomm = new SqlCommand(sqlquery, sqlconn);

            sqlconn.Open();
            SqlDataAdapter sda = new SqlDataAdapter(sqlcomm);
            DataSet ds = new DataSet();

            sda.Fill(ds);

            List<ProfileUpdate> lemp = new List<ProfileUpdate>();
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                lemp.Add(new ProfileUpdate
                {
                    IdUs = Convert.ToInt32(dr["IdUs"]),
                    FirstName = Convert.ToString(dr["FirstName"]),
                    LastName = Convert.ToString(dr["LastName"]),
                    Email = Convert.ToString(dr["Email"]),
                    Password = Convert.ToString(dr["Password"])
                });
            }


            sqlconn.Close();
            return View(lemp);
        }

        public ActionResult HouseDetails()
        {
            string mainconn = ConfigurationManager.ConnectionStrings["Myconnection"].ConnectionString;
            SqlConnection sqlconn = new SqlConnection(mainconn);

            string sqlquery = "select * from [dbo].[HouseDetail]";
            SqlCommand sqlcomm = new SqlCommand(sqlquery, sqlconn);

            sqlconn.Open();
            SqlDataAdapter sda = new SqlDataAdapter(sqlcomm);
            DataSet ds = new DataSet();

            sda.Fill(ds);

            List<HouseDetailcs> lemp = new List<HouseDetailcs>();
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                lemp.Add(new HouseDetailcs
                {
                    IdUs = Convert.ToInt32(dr["IdUs"]),
                    Name = Convert.ToString(dr["Name"]),
                    Email = Convert.ToString(dr["Email"]),
                    ContactNumber = Convert.ToString(dr["ContactNumber"]),
                    Location = Convert.ToString(dr["Location"]),
                    Bedrooms = Convert.ToString(dr["Bedrooms"]),
                    HouseRent = Convert.ToString(dr["HouseRent"]),
                    Description = Convert.ToString(dr["Description"]),
                    Status= Convert.ToInt32(dr["Status"])
                });
            }


            sqlconn.Close();
            return View(lemp);
        }

        public ActionResult SellerDetails()
        {
            string mainconn = ConfigurationManager.ConnectionStrings["Myconnection"].ConnectionString;
            SqlConnection sqlconn = new SqlConnection(mainconn);

            string sqlquery = "select * from [dbo].[TBLSellerInfo]";
            SqlCommand sqlcomm = new SqlCommand(sqlquery, sqlconn);

            sqlconn.Open();
            SqlDataAdapter sda = new SqlDataAdapter(sqlcomm);
            DataSet ds = new DataSet();

            sda.Fill(ds);

            List<ProfileUpdate> lemp = new List<ProfileUpdate>();
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                lemp.Add(new ProfileUpdate
                {
                    IdUs = Convert.ToInt32(dr["IdUs"]),
                    FirstName = Convert.ToString(dr["FirstName"]),
                    LastName = Convert.ToString(dr["LastName"]),
                    Email = Convert.ToString(dr["Email"]),
                    
                    Status = Convert.ToInt32(dr["Status"])
                });
            }


            sqlconn.Close();
            return View(lemp);
        }
        public ActionResult BuyerDetails()
        {
            string mainconn = ConfigurationManager.ConnectionStrings["Myconnection"].ConnectionString;
            SqlConnection sqlconn = new SqlConnection(mainconn);

            string sqlquery = "select * from [dbo].[TBLUserInfo]";
            SqlCommand sqlcomm = new SqlCommand(sqlquery, sqlconn);

            sqlconn.Open();
            SqlDataAdapter sda = new SqlDataAdapter(sqlcomm);
            DataSet ds = new DataSet();

            sda.Fill(ds);

            List<ProfileUpdate> lemp = new List<ProfileUpdate>();
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                lemp.Add(new ProfileUpdate
                {
                    IdUs = Convert.ToInt32(dr["IdUs"]),
                    FirstName = Convert.ToString(dr["FirstName"]),
                    LastName = Convert.ToString(dr["LastName"]),
                    Email = Convert.ToString(dr["Email"]),
                    
                    Status = Convert.ToInt32(dr["Status"])
                });
            }


            sqlconn.Close();
            return View(lemp);
        }
        public ActionResult BookingDetails()        {

            string mainconn = ConfigurationManager.ConnectionStrings["Myconnection"].ConnectionString;
            SqlConnection sqlconn = new SqlConnection(mainconn);
            string sqlquery = "select * from [dbo].[BookingDetail]";
            SqlCommand sqlcomm = new SqlCommand(sqlquery, sqlconn);
            sqlconn.Open();
            SqlDataAdapter sda = new SqlDataAdapter(sqlcomm);
            DataSet ds = new DataSet();
            sda.Fill(ds);
            List<BookingDetail> lemp = new List<BookingDetail>();
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                lemp.Add(new BookingDetail
                {
                    IdUs = Convert.ToInt32(dr["IdUs"]),                   
                    BuyerEmail = Convert.ToString(dr["BuyerEmail"]),                  
                    SellerEmail = Convert.ToString(dr["SellerEmail"]),
                    Location = Convert.ToString(dr["Location"]),                    
                    HouseRent = Convert.ToString(dr["HouseRent"]),
                    Description = Convert.ToString(dr["Description"]),
                    Status = Convert.ToString(dr["Status"])
                });
            }


            sqlconn.Close();
            return View(lemp);
        }
        public ActionResult Signup()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Signup(TBLAdminInfo tBLAdminInfo)
        {
            if (db.TBLAdminInfoes.Any(x => x.Email == tBLAdminInfo.Email))
            {
                ViewBag.Notification = "This account has already existed";
                return View();
            }
            else
            {
                db.TBLAdminInfoes.Add(tBLAdminInfo);
                db.SaveChanges();

                Session["IdUsSS"] = tBLAdminInfo.IdUs.ToString();
                Session["EmailSS"] = tBLAdminInfo.Email.ToString();
                return RedirectToAction("Index", "Admin");
            }
        }

        public ActionResult Logout()
        {
            Session.Clear();
            return RedirectToAction("Login", "Buyer");
        }

        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(TBLAdminInfo tBLAdminInfo)
        {
            var checkLogin = db.TBLAdminInfoes.Where(x => x.Email.Equals(tBLAdminInfo.Email) && x.Password.Equals(tBLAdminInfo.Password)).FirstOrDefault();
            if (checkLogin != null)
            {
                Session["IdUsSS"] = tBLAdminInfo.IdUs.ToString();
                Session["EmailSS"] = tBLAdminInfo.Email.ToString();
                return RedirectToAction("HouseDetails", "Admin");
            }
            else
            {
                ViewBag.Notification = "Wrong Email or password";
            }
            return View();
        }
    }
}