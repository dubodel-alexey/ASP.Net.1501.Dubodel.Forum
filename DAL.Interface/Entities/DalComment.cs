using System;

namespace DAL.Interface.Entities
{
    public class DalComment : IDalEntity
    {
        public int Id { get; set; }
        public string Body { get; set; }
        public DateTime Time { get; set; }

        public int TopicId { get; set; }
        public int UserId { get; set; }
    }
}
