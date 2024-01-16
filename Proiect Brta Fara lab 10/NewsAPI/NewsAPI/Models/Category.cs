namespace NewsAPI.Models
{
    public class Category
    {
        public Guid? Id { get; set; }
        public string Name { get; set; }

        public Category()
        {
            Id = Guid.NewGuid();
            Name = string.Empty;
        }
    }
}
