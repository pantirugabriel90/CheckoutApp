using CheckoutApp.Models.Domain;
using System.Collections.Generic;

namespace CheckoutApp.Models.Queries
{
    public class BasketQueryResult: IQuery
    {
        public long EntityId { get; set; }
        public List<Article> Articles { get; set; }
        public double TotalNet { get; set; }
        public double TotalGross { get; set; }
        public string Customer { get; set; }
        public bool PaysVAT { get; set; }
        public Status Status { get; set; }
    }
    public class Article { 
        public string Name { get; set; }
        public double  Price { get; set; }
    }
}
