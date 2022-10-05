using System.Collections.Generic;

namespace CheckoutApp.Models.Domain
{
    public class Basket
    {
        public long BasketID { get; set; }
        public virtual List<Article> Articles { get; set; }
        public double TotalNet { get; set; }
        public double TotalGross { get; set; }
        public string Customer { get; set; }
        public bool PaysVAT { get; set; }
        public Status Status { get; set; }
    }
    public enum Status { 
        Open,
        Closed
    }
}
