using CheckoutApp.Models;
using CheckoutApp.Models.Domain;
using CheckoutApp.Services;
using NUnit.Framework;

namespace CheckoutAppTests
{
    public class Tests
    {
        public EventProcessor EventProcessor  { get;set;}
        [SetUp]
        public void Setup()
        {
            EventProcessor = new EventProcessor(null,null);
        }

        [Test]
        public void TestCreateBasket()
        {
            var createBasket = new CreateBasketEvent() { Customer = "Gabi", PaysVTA = true };

            var basket = EventProcessor.MapCreateBasketEventToBasket(createBasket);




            Assert.AreEqual(basket.Customer, "Gabi");
            Assert.AreEqual(basket.PaysVAT, true);
        }

        [Test]
        public void TestUpdateBasket()
        {
            var updateBasket = new UpdateBasketEvent() { Status = 0 };
            var basket = new Basket() { Status = (Status) 1 };

            EventProcessor.UpdateBasketFromEvent(basket, updateBasket);


            Assert.AreEqual(basket.Status, (Status)0);
        }

        [Test]
        public void TestAddArticle()
        {
            var updateBasket = new AddArticleEvent() { Name = "food", Price = 100 };
            var basket = new Basket() { TotalGross = 0, TotalNet = 0, PaysVAT = true };

            EventProcessor.AddArticleToBasket(basket, updateBasket);

            Assert.AreEqual(basket.TotalNet, 100);
            Assert.AreEqual(basket.TotalGross, 117);
        }

        [Test]
        public void TestNoVATAddArticle()
        {
            var addArticleEvent = new AddArticleEvent() { Name = "food", Price = 100 };
            var basket = new Basket() { TotalGross = 100, TotalNet = 100, PaysVAT = false };

            EventProcessor.AddArticleToBasket(basket, addArticleEvent);

            Assert.AreEqual(basket.TotalNet, 200);
            Assert.AreEqual(basket.TotalGross, 200);
        }
        [Test]
        public void Test2AddArticle()
        {
            var addArticleEvent = new AddArticleEvent() { Name = "food", Price = 100 };
            var basket = new Basket() { TotalGross = 100, TotalNet = 100, PaysVAT = true };

            EventProcessor.AddArticleToBasket(basket, addArticleEvent);

            Assert.AreEqual(basket.TotalNet, 200);
            Assert.AreEqual(basket.TotalGross, 217);
        }
        [Test]
        public void TestMultipleAddArticle()
        {
            var addArticleEvent = new AddArticleEvent() { Name = "food", Price = 100 };
            var addArticleEvent2 = new AddArticleEvent() { Name = "food", Price = 100 };
            var addArticleEvent3 = new AddArticleEvent() { Name = "food", Price = 100 };
            var basket = new Basket() { TotalGross = 0, TotalNet = 0, PaysVAT = true };

            EventProcessor.AddArticleToBasket(basket, addArticleEvent);
            EventProcessor.AddArticleToBasket(basket, addArticleEvent2);
            EventProcessor.AddArticleToBasket(basket, addArticleEvent3);

            Assert.AreEqual(basket.TotalNet, 300);
            Assert.AreEqual(basket.TotalGross, 351);
        }
    }
}