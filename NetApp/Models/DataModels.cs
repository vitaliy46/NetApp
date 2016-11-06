using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NetApp.Models
{
    public class Customer
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public long Inn { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
    }

    public class CustomerContact
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

    public class CustomerContactPhone
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public string Phone { get; set; }
    }

    public class Supplier
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Url { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
    }
}