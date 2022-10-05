namespace CheckoutApp.Models
{
    public class CreateBasketEvent: IEvent
    {
        public string Customer { get; set; }
        public bool PaysVTA { get; set; }
    }
}
