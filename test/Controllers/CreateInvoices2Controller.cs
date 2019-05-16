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
    public class CreateInvoices2Controller : Controller
    {
        private team3Entities db = new team3Entities();

        // GET: CreateInvoices2
        public ActionResult Index()
        {
            var createInvoices = db.CreateInvoices.Include(c => c.OrderDetail).Include(c => c.OrderProduct);
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

        // GET: CreateInvoices2/Details/5
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

        // GET: CreateInvoices2/Create
        public ActionResult Create()
        {
            ViewBag.oderid = new SelectList(db.OrderDetails, "OrderID", "OrderID");
            ViewBag.oderid = new SelectList(db.OrderProducts, "orderId", "orderId");
            return View();
        }

        // POST: CreateInvoices2/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "invoiceID,fromAddress,toaddress,oderid,notes,total,PaidorNot")] CreateInvoice createInvoice)
        {
            if (ModelState.IsValid)
            {
                OrderProduct pr = new OrderProduct();
                var res = db.OrderProducts.Where(p => p.orderId == createInvoice.oderid).FirstOrDefault();
                createInvoice.total = Convert.ToDecimal(res.total);
                //  createInvoice.total = 
                db.CreateInvoices.Add(createInvoice);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.oderid = new SelectList(db.OrderDetails, "OrderID", "OrderID", createInvoice.oderid);
            ViewBag.oderid = new SelectList(db.OrderProducts, "orderId", "orderId", createInvoice.oderid);
            return View(createInvoice);
        }

        // GET: CreateInvoices2/Edit/5
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
            ViewBag.oderid = new SelectList(db.OrderProducts, "orderId", "orderId", createInvoice.oderid);
            return View(createInvoice);
        }

        // POST: CreateInvoices2/Edit/5
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
            ViewBag.oderid = new SelectList(db.OrderProducts, "orderId", "orderId", createInvoice.oderid);
            return View(createInvoice);
        }

        // GET: CreateInvoices2/Delete/5
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

        // POST: CreateInvoices2/Delete/5
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
        public ActionResult Getall(int? invoiceID)
        {
            var det = (from d in db.CreateInvoices
                       where d.invoiceID == invoiceID
                       select d).ToList();


            return View(det);
        }
    }
    }

