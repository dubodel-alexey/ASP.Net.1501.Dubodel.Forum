namespace DAL.Interface.Entities
{
    public class DalCategory : IDalEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool ForRegistered { get; set; }

        public int? ParentCategoryId { get; set; }
    }
}
