using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using test.Models;

namespace test.Controllers
{
   
    public class UserController : Controller
    {
        private team3Entities db = new team3Entities();

        // GET: User
        public ActionResult Index()
        {
            return View(db.Registrations.ToList());
        }

        // GET: User/Details/5
      
        // GET: User/Create
        public ActionResult Registration()
        {
            return View();
        }

        // POST: User/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Registration([Bind(Include = "userID,FirstName,LastName,UserName,Password,Address")] Registration registration)
        {
                if (ModelState.IsValid)
                {
                    db.Registrations.Add(registration);
                    db.SaveChanges();

                    return RedirectToAction("Index");
                }

                return View(registration);
            }
            
        

        // GET: User/Edit/5
       
        // GET: User/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Registration registration = db.Registrations.Find(id);
            if (registration == null)
            {
                return HttpNotFound();
            }
            return View(registration);
        }

        // POST: User/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            Registration registration = db.Registrations.Find(id);
            db.Registrations.Remove(registration);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
        [HttpGet]
        public ActionResult LogIn()
        {
            return View();
        }

        [HttpPost]
        public ActionResult LogIn(Models.Registration userr)
        {
           
            //if (ModelState.IsValid)  
            //{  
            if (IsValid(userr.UserName, userr.Password))
            {
                Session["UserName"] = userr.UserName;
                FormsAuthentication.SetAuthCookie(userr.UserName, false);
                return RedirectToAction("ClerkHome");
            }
            else
            {
                ModelState.AddModelError("", "Login details are wrong.");
            }
            return View(userr);
        }
        private bool IsValid(string username, string password)
        {
          
            bool IsValid = false;
            using (team3Entities db = new team3Entities())
            {
                var user = db.Registrations.FirstOrDefault(u => u.UserName == username);
                if (user != null)
                {
                    if (user.Password == password)
                    {
                        IsValid = true;
                    }

                }
            }
            return IsValid;
        }
        [NoCache]
        public ActionResult LogOut()
        {
            Session.Clear();
            FormsAuthentication.SignOut();
            return RedirectToAction("home", "Admin");
        }
        public ActionResult ChangePassword(string password, string CnfmPassword)
        {

            Registration rs = new Registration();
            rs.Password = password;
            db.Registrations.Add(rs);

            if (password == CnfmPassword)
            {
               
                ViewBag.Message = "password Changed";
                return RedirectToAction("LogIn");
            }
            return View();
        }
        public ActionResult ForgotPassword()
        {
            return View();
        }
        [HttpPost]
        public ActionResult ForgotPassword(string username, string Password)
        {
          
            var details = db.Registrations.FirstOrDefault(u => u.UserName == username);
            if (details != null)
            {
                details.Password = Password;
                if (TryUpdateModel(details))
                {
                    db.SaveChanges();
                    return RedirectToAction("Login");
                }
            }
            return View();
        }
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Registration registration = db.Registrations.Find(id);
            if (registration == null)
            {
                return HttpNotFound();
            }
            return View(registration);
        }

        // POST: Admin/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "userID,FirstName,LastName,UserName,Password,Address")] Registration registration)
        {
            if (ModelState.IsValid)
            {
                db.Entry(registration).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(registration);
        }
        public ActionResult ClerkHome()
        {
            return View();
        }


    }
}
