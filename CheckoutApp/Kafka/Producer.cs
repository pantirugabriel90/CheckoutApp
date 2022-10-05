using CheckoutApp.Models;
using Confluent.Kafka;
using Newtonsoft.Json;
using System.Threading.Tasks;

namespace CheckoutApp.Kafka
{

    //not finalized!!
    public class Producer : IProducer
    {
        ProducerConfig Config { get; set; }
        ICustomLogger Logger { get; set; }
        public Producer(ICustomLogger logger)
        {
            Config = new ProducerConfig
            {
                BootstrapServers = "localhost:9092"
            };
        }
        public async Task Publish(AddArticleEvent evnt)
        {
            using var p = new ProducerBuilder<Null, string>(Config).Build();
            var message = new Message<Null, string>
            {
                Value = $"{JsonConvert.SerializeObject(evnt)}"
            };

            var dr = await p.ProduceAsync(Constants.addArticleTopic, message);
            Logger.LogInfo($"message publish result to topic {Constants.addArticleTopic} :{JsonConvert.SerializeObject(dr)}");
        }

        public async Task Publish(CreateBasketEvent evnt)
        {
            using var p = new ProducerBuilder<Null, string>(Config).Build();
            var message = new Message<Null, string>
            {
                Value = $"{JsonConvert.SerializeObject(evnt)}"
            };

            var dr = await p.ProduceAsync(Constants.createBaskeTopic, message);
            Logger.LogInfo($"message publish result to topic {Constants.createBaskeTopic} :{JsonConvert.SerializeObject(dr)}");
        }

        public async Task Publish(UpdateBasketEvent evnt)
        {
            using var p = new ProducerBuilder<Null, string>(Config).Build();
            var message = new Message<Null, string>
            {
                Value = $"{JsonConvert.SerializeObject(evnt)}"
            };

            var dr = await p.ProduceAsync(Constants.updateBasketTopic, message);
            Logger.LogInfo($"message publish result to topic {Constants.updateBasketTopic} :{JsonConvert.SerializeObject(dr)}");
        }
    }
}
