using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using test.Models;
using Rotativa;

namespace test.Controllers
{
    [NoCache]
    public class CreateInvoicesController : Controller
    {
        private team3Entities db = new team3Entities();

        // GET: CreateInvoices
        [NoCache]
        public ActionResult Index()
        {
            var createInvoices = db.CreateInvoices.Include(c => c.OrderDetail);
           
            return View(createInvoices.ToList());
        }
        [HttpPost]
        public ActionResult Index(int invoiceID)
        {
            var det = (from d in db.CreateInvoices
                       where d.invoiceID == invoiceID
                       select d).ToList();


            return View(det);
        }
       


        // GET: CreateInvoices/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CreateInvoice createInvoice = db.CreateInvoices.Find(id);
            if (createInvoice == null)
            {
                return HttpNotFound();
            }
            return View(createInvoice);
        }

        // GET: CreateInvoices/Create
        public ActionResult Create()
        {
            ViewBag.oderid = new SelectList(db.Product1, "OrderID", "OrderID");
            return View();
        }

     
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "invoiceID,fromAddress,toaddress,oderid,notes,total,PaidorNot")] CreateInvoice createInvoice)
        {

            if (ModelState.IsValid)
            {
                var a = db.OrderDetails.Where(x => x.OrderID == createInvoice.oderid).FirstOrDefault();
                //var b = db.Products.Where(x => x.ProductName == createInvoice.invoiceID.ToString()).FirstOrDefault();
                //if (a != null)
                //{
                //    //using (var tr = new Training_20Feb_MumbaiEntities3())
                //    //{
                //    //    var use = tr.CreateInvoices.Create();
                //    //    use.PaidorNot = createInvoice.PaidorNot;

                     createInvoice.total = Convert.ToInt32(a.total);
                        db.CreateInvoices.Add(createInvoice);
                        db.SaveChanges();
                        return RedirectToAction("Index");

                    //}
                    }
            
            ViewBag.oderid = new SelectList(db.OrderDetails, "OrderID", "OrderID", createInvoice.oderid);
            return View(createInvoice);
        }

        // GET: CreateInvoices/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CreateInvoice createInvoice = db.CreateInvoices.Find(id);
            if (createInvoice == null)
            {
                return HttpNotFound();
            }
            ViewBag.oderid = new SelectList(db.OrderDetails, "OrderID", "OrderID", createInvoice.oderid);
            return View(createInvoice);
        }

        // POST: CreateInvoices/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "invoiceID,fromAddress,toaddress,oderid,notes,total,PaidorNot")] CreateInvoice createInvoice)
        {
            if (ModelState.IsValid)
            {
               
                 db.Entry(createInvoice).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.oderid = new SelectList(db.OrderDetails, "OrderID", "OrderID", createInvoice.oderid);
            return View(createInvoice);
        }

        // GET: CreateInvoices/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CreateInvoice createInvoice = db.CreateInvoices.Find(id);
            if (createInvoice == null)
            {
                return HttpNotFound();
            }
            return View(createInvoice);
        }

        // POST: CreateInvoices/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            CreateInvoice createInvoice = db.CreateInvoices.Find(id);
            db.CreateInvoices.Remove(createInvoice);
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
        public ActionResult GetAll(int? id)
        {
            //var det = (from d in db.CreateInvoices
            //           where d.invoiceID == invoiceIDd
            //           select d).ToList();


            //return View(det);
            var all = db.CreateInvoices.Where(x => x.invoiceID == id).ToList();
            return View(all);
        }

        public ActionResult PrintAll()
        {
            //    var det = (from d in db.CreateInvoices
            //               where d.invoiceID == invoiceID
            //               select d).ToList();


            //    return View(det);
            var q = new ActionAsPdf("GetAll");
            return q;
        }

    }
    public class NoCacheAttribute : ActionFilterAttribute
    {
        public override void OnResultExecuting(ResultExecutingContext filterContext)
        {
            filterContext.HttpContext.Response.Cache.SetExpires(DateTime.UtcNow.AddDays(-1));
            filterContext.HttpContext.Response.Cache.SetValidUntilExpires(false);
            filterContext.HttpContext.Response.Cache.SetCacheability(HttpCacheability.NoCache);
            filterContext.HttpContext.Response.Cache.SetNoStore();

            base.OnResultExecuting(filterContext);
        }
    }


}
