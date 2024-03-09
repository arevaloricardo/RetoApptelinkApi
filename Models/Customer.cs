using System.ComponentModel.DataAnnotations;

namespace RetoApptelinkApi.Models
{
    public class Customer
    {
        [Key]
        public int Id_Customer { get; set; }


        public string RucDni_Customer { get; set; }

        public string Name_Customer { get; set; }

        public string Alias_Customer { get; set; }

        public string Address_Customer { get; set; }

        public string Email_Customer { get; set; }

        public int Is_Active_Customer { get; set; }

        public DateTime Date_Created_Customer { get; set; }

    }
}
