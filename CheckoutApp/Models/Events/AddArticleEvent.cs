namespace CheckoutApp.Models
{
    public class AddArticleEvent : IEvent
    {
        public string Name { get; set; }
        public float Price { get; set; }
        public long EntityId { get; set; }
    }
}
