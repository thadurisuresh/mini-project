using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using test.Models;

namespace test.ViewModel
{
    public class MultiTables
    {

        [Key]
        public int name { get; set; }
        public IEnumerable<CreateInvoice> invoice { get; set; }
        public IEnumerable<OrderDetail> details { get; set; }
    }
}