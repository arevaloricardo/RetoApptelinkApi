using System.ComponentModel.DataAnnotations;

namespace RetoApptelinkApi.Models
{
    public class Invoice
    {
        [Key]
        public int Id_Invoice { get; set; }

        public double Total_Invoice { get; set; }

        public int Id_Customer_Invoice { get; set; }

        public Customer Customers { get; set; }

        public DateTime Date_Created_Invoice { get; set; }
    }
}
