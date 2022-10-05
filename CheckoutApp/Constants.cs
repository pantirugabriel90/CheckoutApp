using System.Collections.Generic;

namespace CheckoutApp
{
    public static class Constants
    {
        public static double VTA = 0.17;
        public const string createBaskeTopic = "createBaskeTopic";
        public const string updateBasketTopic = "updateBasketTopic";
        public const string addArticleTopic = "addArticleTopic";
        public static List<string> topics = new List<string> { createBaskeTopic, updateBasketTopic, addArticleTopic };
    }
}
