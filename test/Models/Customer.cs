//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace test.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public partial class Customer
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Customer()
        {
            this.Orders = new HashSet<Order>();
            this.OrderProducts = new HashSet<OrderProduct>();
        }
    
        public int CustomerID { get; set; }
        [Required(ErrorMessage = "Please Enter Customer Name")]
        public string CustomerName { get; set; }
        [Required(ErrorMessage = "Please Enter Mobile Number ")]
        public string Contact { get; set; }
        [Required(ErrorMessage = "Please Enter Email")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Please Enter Address")]
        public string Address { get; set; }
        public string City { get; set; }
        [Required(ErrorMessage = "Please Enter Pincode")]
        public int Pincode { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Order> Orders { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<OrderProduct> OrderProducts { get; set; }
    }
}
