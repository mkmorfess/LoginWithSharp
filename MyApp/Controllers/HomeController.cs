using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MySQL.Models;
using MySql.Data.MySqlClient;
using System.Configuration;

namespace MySQL.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Authorize(user LoginModel)
        {
            using (DBModels db = new DBModels())
            {
                var userDetails = db.users.Where(x => x.username == LoginModel.username && x.password == LoginModel.password).FirstOrDefault();
                if (userDetails == null)
                {
                    LoginModel.LoginErrorMessage = "Wrong Username and/or Password";
                    return View("Index", LoginModel);
                }
                else
                {
                    Session["UserId"] = userDetails.id;
                    return RedirectToAction("About", "Home");
                }
            }
        }

        public ActionResult Logout()
        {
            Session["UserId"] = null;
            return RedirectToAction("Index", "Home");
        }

        public ActionResult Registration()
        {
            return View();
        }

        public ActionResult RegistrationNew (user LoginModel)
        {
            using (DBModels db = new DBModels())
            {
                 
                string MyConnection2 = "datasource=localhost;port=3306;username=root;password=root"; 
                string Query = "use Login; Insert into users(username, password) values('" + LoginModel.username + "','" + LoginModel.password + "');";                 
                MySqlConnection MyConn2 = new MySqlConnection(MyConnection2); 
                MySqlCommand MyCommand2 = new MySqlCommand(Query, MyConn2);
                MySqlDataReader MyReader2;
                MyConn2.Open();
                MyReader2 = MyCommand2.ExecuteReader();
                
                while (MyReader2.Read())
                {
                }
                MyConn2.Close();
                

                return RedirectToAction("About", "Home");
            }
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}