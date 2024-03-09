using System.ComponentModel.DataAnnotations;

namespace RetoApptelinkApi.Models
{
    public class User
    {
        [Key]
        public int Id_User { get; set; }

        public string Name_User { get; set; }

        public string Email_User { get; set; }

        public string Password_User { get; set; }

        public int Is_Active_User { get; set; }

        public int Login_Attempts_User { get; set; }   

        public DateTime Date_Created_User { get; set; }
    }
}
