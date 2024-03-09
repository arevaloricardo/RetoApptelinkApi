using System.ComponentModel.DataAnnotations;

namespace RetoApptelinkApi.Models
{
    public class InvoiceProduct
    {

        [Key]
        public int Id_InvoiceProduct { get; set; }

        public int Id_Invoice_InvoiceProduct { get; set; }

        public Invoice Invoice { get; set; }

        public int Id_Product_InvoiceProduct { get; set; }

        public Product Product { get; set; }

        public int Quantity_InvoiceProduct { get; set; }

        public double Price_InvoiceProduct { get; set; }

        public double Total_InvoiceProduct { get; set; }   

    }
}
