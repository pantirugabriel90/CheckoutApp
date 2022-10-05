using CheckoutApp.Models;
using CheckoutApp.Services;
using Confluent.Kafka;
using System;
using System.Threading.Tasks;

namespace CheckoutApp.Kafka
{
    public class Consumer : IConsumer
    {
        IEventProcessor EventProcessor { get; set; }
        private bool subscribe = true;
        public Consumer(EventProcessor eventProcessor)
        {
            EventProcessor = eventProcessor;
        }

        //not finalized!!
        public void SubscribeToChanges()
        {
            try
            {

                var conf = new ConsumerConfig
                {
                    GroupId = "test-consumer-group",
                    BootstrapServers = "localhost:9092",
                    AutoOffsetReset = AutoOffsetReset.Earliest
                };

                using var c = new ConsumerBuilder<Ignore, string>(conf).Build();
                c.Subscribe(Constants.topics);

                while (subscribe)
                {
                   var message =  c.Consume();
                    switch (message.Topic) {
                        case Constants.addArticleTopic:
                            DelegateAddArticleEvent(message.Message.Value);
                            break;
                        case Constants.updateBasketTopic:
                            DelegateUpdateBasketEvent(message.Message.Value);
                            break;
                        case Constants.createBaskeTopic:
                            DelegateCreateBasketEvent(message.Message.Value);
                            break;
                    }
                }
            }
            catch (Exception ex)
            {

            }

        }

        public void DelegateAddArticleEvent(string evnt)
        {
            var addArticle = System.Text.Json.JsonSerializer.Deserialize<AddArticleEvent>(evnt);
            EventProcessor.ProcessAddArticle(addArticle, addArticle.EntityId);
        }

        public void DelegateCreateBasketEvent(string evnt)
        {
            var createBasket = System.Text.Json.JsonSerializer.Deserialize<CreateBasketEvent>(evnt);
            EventProcessor.ProcessCreateBascket(createBasket);
        }

        public void DelegateUpdateBasketEvent(string evnt)
        {
            var updateBasket = System.Text.Json.JsonSerializer.Deserialize<UpdateBasketEvent>(evnt);
            EventProcessor.ProcessUpdateBasket(updateBasket, updateBasket.EntityId);
        }
    }
}
