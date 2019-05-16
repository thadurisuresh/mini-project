using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using test.Models;
using test.ViewModel;


namespace test.Controllers
{
    public class DisplayController : Controller
    {
        public ActionResult Multidata()
        {
            team3Entities Context = new team3Entities();
            var mymodel = new MultiTables();
            mymodel.invoice = Context.CreateInvoices.ToList();
            mymodel.details = Context.OrderDetails.ToList();
            MultiTables obj = new MultiTables();

            return View(mymodel);

        }
    }
    }
