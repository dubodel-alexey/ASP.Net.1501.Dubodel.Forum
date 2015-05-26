namespace Bll.Interface.Entities
{
    public class BllCategory
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool ForRegistered { get; set; }

        public int? ParentCategoryId { get; set; }
    }
}
