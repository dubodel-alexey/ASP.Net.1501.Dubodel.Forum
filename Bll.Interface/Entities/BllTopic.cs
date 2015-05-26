namespace Bll.Interface.Entities
{
    public class BllTopic
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool ForRegistered { get; set; }
        public int CategoryId { get; set; }
    }
}
