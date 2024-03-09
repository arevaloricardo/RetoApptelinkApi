using System.ComponentModel.DataAnnotations;

namespace RetoApptelinkApi.Models
{
    public class Product
    {
        [Key]
        public int Id_Product { get; set; }

        public string Code_Product { get; set; }

        public string Name_Product { get; set; }

        public double Price_Product { get; set; }

        public int Stock_Product { get; set; }

        public int Is_Active_Product { get; set; }

        public DateTime Date_Created_Product { get; set; }

    }
}
