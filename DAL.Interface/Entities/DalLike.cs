namespace DAL.Interface.Entities
{
    public class DalLike : IDalEntity
    {
        public int Id { get; set; }

        public int UserId { get; set; }
        public int CommentId { get; set; }
    }
}
