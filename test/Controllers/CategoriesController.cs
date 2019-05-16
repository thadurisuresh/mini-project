using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using test.Models;

namespace test.Controllers
{
    [NoCache]
    public class CategoriesController : Controller
    {
        private team3Entities db = new team3Entities();

        // GET: Categories
        public ActionResult Display()
        {
            return View(db.Categories.ToList());
        }

        // GET: Categories/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Category category = db.Categories.Find(id);
            if (category == null)
            {
                return HttpNotFound();
            }
            return View(category);
        }

        // GET: Categories/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Categories/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "CategoryID,CategoryName,Description")] Category category)
        {
            if (ModelState.IsValid)
            {
                db.Categories.Add(category);
                db.SaveChanges();
                return RedirectToAction("Display");
            }

            return View(category);
        }

        // GET: Categories/Edit/5
       // public ActionResult Edit(int? id)
        //{
           
        //    Category pr = new Category();
        //    pr = db.Categories.Find(id);
        //    return View(pr);
        //}
        //[HttpPost]
        //public ActionResult Edit(Category std)  //Update
        //{
         
        //    var pr = db.Categories.Where(x => x.CategoryID == std.CategoryID).FirstOrDefault();

        //    pr.CategoryName = std.CategoryName;
        //    pr.Description = std.Description;
            
        //    //tr.myProduct_174813.Add(pr);

        //    return View();
        //}
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Category category = db.Categories.Find(id);
            if (category == null)
            {
                return HttpNotFound();
            }
            return View(category);
        }

        // POST: Categories/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "CategoryID,CategoryName,Description")] Category category)
        {
            if (ModelState.IsValid)
            {
                db.Entry(category).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Display");
            }
            return View(category);
        }

        // GET: Categories/Delete/5
        public ActionResult Delete(int? id)
        {
           
            Category category = db.Categories.Find(id);
            
            return View(category);
        }

        // POST: Categories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Category category = db.Categories.Find(id);
            db.Categories.Remove(category);
            db.SaveChanges();
            return RedirectToAction("Display");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
        public ActionResult Search(string categoryName)
        {
          
            ViewBag.categoryName = new SelectList(db.Categories, "CategoryID", "categoryName");
            if (categoryName == null)
            {
                return View();
            }
            else
            {
                return View(from Category in db.Categories where Category.CategoryName == categoryName select Category);
            }
            //return View();
        }
      
        public ActionResult Searchname(string name)
        {
            var a =db.Categories.Where(X=>X.CategoryName== name);
            ViewBag.name = new SelectList(db.Categories, "CategoryID", "categoryName");
           
                return View(a);
            }
         
        
    }

    }

