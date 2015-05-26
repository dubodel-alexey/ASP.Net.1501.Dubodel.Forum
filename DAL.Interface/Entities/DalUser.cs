using System;

namespace DAL.Interface.Entities
{
    public class DalUser : IDalEntity
    {
        public int Id { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public DateTime RegistrationDateTime { get; set; }

        public int RoleId { get; set; }
    }
}
