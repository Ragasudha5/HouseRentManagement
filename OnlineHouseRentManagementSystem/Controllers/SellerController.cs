using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using OnlineHouseRentManagementSystem.Models;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;
using System.Web.SessionState;

namespace OnlineHouseRentManagementSystem.Controllers
{
    public class SellerController : Controller
    {
        // GET: Seller
        DBuserSignupLoginEntities db = new DBuserSignupLoginEntities();

        // GET: Home
        public ActionResult Index()
        {
            string mainconn = ConfigurationManager.ConnectionStrings["Myconnection"].ConnectionString;
            SqlConnection sqlconn = new SqlConnection(mainconn);

            string sqlquery = "select * from [dbo].[TBLSellerInfo] where Email='" + Session["EmailSS"] + "'";
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

        public ActionResult PopulateData()
        {
            
            string mainconn = ConfigurationManager.ConnectionStrings["Myconnection"].ConnectionString;
            SqlConnection sqlconn = new SqlConnection(mainconn);

            string sqlquery = "select * from [dbo].[HouseDetail] where Email='" +Session["EmailSS"]+"'";
            SqlCommand sqlcomm = new SqlCommand(sqlquery, sqlconn);
           
            sqlconn.Open();
            SqlDataAdapter sda = new SqlDataAdapter(sqlcomm);
            DataSet ds = new DataSet();
            
            sda.Fill(ds);
            
            List <HouseDetailcs> lemp = new List<HouseDetailcs>();
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
                    Description = Convert.ToString(dr["Description"])
                });
            }


            sqlconn.Close();
            return View(lemp);
        }
        public ActionResult BookingDetails()
        {

            string mainconn = ConfigurationManager.ConnectionStrings["Myconnection"].ConnectionString;
            SqlConnection sqlconn = new SqlConnection(mainconn);

            string sqlquery = "select * from [dbo].[BookingDetail] where SellerEmail='" + Session["EmailSS"] + "'";
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
                    Status = Convert.ToString(dr["Status"]),
                });
            }


            sqlconn.Close();
            return View(lemp);
        }
        public ActionResult Accept(int id)
        {
            string mainconn = ConfigurationManager.ConnectionStrings["Myconnection"].ConnectionString;
            SqlConnection sqlconn = new SqlConnection(mainconn);
            string sqlquery = "Update BookingDetail set Status='Accepted' where IdUs='"+id+"'";
            SqlCommand sqlcomm = new SqlCommand(sqlquery, sqlconn);
            sqlconn.Open();
            SqlDataAdapter sda = new SqlDataAdapter(sqlcomm);
            DataSet ds = new DataSet();
            sda.Fill(ds);
            return RedirectToAction("BookingDetails", "Seller");
        }
        public ActionResult Decline(int id)
        {
            string mainconn = ConfigurationManager.ConnectionStrings["Myconnection"].ConnectionString;
            SqlConnection sqlconn = new SqlConnection(mainconn);
            string sqlquery = "Update BookingDetail set Status='Declined' where IdUs='" + id + "'";
            SqlCommand sqlcomm = new SqlCommand(sqlquery, sqlconn);
            sqlconn.Open();
            SqlDataAdapter sda = new SqlDataAdapter(sqlcomm);
            DataSet ds = new DataSet();
            sda.Fill(ds);
            return RedirectToAction("BookingDetails", "Seller");
        }
        public ActionResult Signup()
        {
            return View();
        }


        [HttpPost]
        public ActionResult Signup(TBLSellerInfo tBLSellerInfo)
        {
            if (db.TBLSellerInfoes.Any(x => x.Email == tBLSellerInfo.Email))
            {
                ViewBag.Notification = "This account has already existed";
                return View();
            }
            else
            {
                db.TBLSellerInfoes.Add(tBLSellerInfo);
                db.SaveChanges();

                Session["IdUsSS"] = tBLSellerInfo.IdUs.ToString();
                Session["EmailSS"] = tBLSellerInfo.Email.ToString();

                return RedirectToAction("Login", "Seller");
            }
        }

        public ActionResult Logout()
        {
            Session.Clear();
            return RedirectToAction("Login", "Seller");
        }

        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(TBLSellerInfo tBLSellerInfo)
        {
            var checkLogin = db.TBLSellerInfoes.Where(x => x.Email.Equals(tBLSellerInfo.Email) && x.Password.Equals(tBLSellerInfo.Password)).FirstOrDefault();
            if (checkLogin == null)
            {
                ViewBag.msg = "No such Account Exixts";
            }
            else
            {
               if (checkLogin != null && checkLogin.Status == 1)
                {
                    Session["IdUsSS"] = tBLSellerInfo.IdUs.ToString();
                    Session["EmailSS"] = tBLSellerInfo.Email.ToString();
                    return RedirectToAction("PopulateData", "Seller");
                }                
                else if(checkLogin.Status==0)
                {
                    ViewBag.Notification = "Your Account has not been verified Yet.....Wait for the Admin's Approval";
             
                }
                else
                {
                    ViewBag.Notification = "Wrong Email or Password";
                }
            }
            ViewBag.Notification = "Your Account has not been verified Yet.....Wait for the Admin's Approval";
            return View();
        }
        
    }
}