using CheckoutApp.Models;
using System.Threading.Tasks;

namespace CheckoutApp.Kafka
{
    public interface IProducer
    {
        Task Publish(AddArticleEvent evnt);
        Task Publish(CreateBasketEvent evnt);
        Task Publish(UpdateBasketEvent evnt);
    }
}
