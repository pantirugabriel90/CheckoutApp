using CheckoutApp.Kafka;
using CheckoutApp.Models;
using CheckoutApp.Models.Queries;
using CheckoutApp.Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CheckoutApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BasketsController : ControllerBase
    {
        public IEventProcessor EventProcessor { get; set; }
        public IBasketQueryHandler QueryHandler { get; set; }
        public ICustomLogger Logger { get; set; }
        public IProducer Publisher { get; set; }
        public BasketsController(IEventProcessor eventProcessor, IBasketQueryHandler basketQueryHandler, IProducer publisher, ICustomLogger logger)
        {
            QueryHandler = basketQueryHandler;
            Logger = logger;
            Publisher = publisher;
            EventProcessor = eventProcessor;
        }
        // GET api/<BasketsController>/5
        [HttpGet("{id}")]
        public ActionResult Get(long id)
        {
            Logger.LogInfo("Get request received");
            var result = QueryHandler.ExecuteBasketQuery(id);
            if(result != null)
                return Ok(result);
            return NotFound();
        }

        // POST api/<BasketsController>
        [HttpPost]
        public async Task<ActionResult> CreateBascket(CreateBasketEvent createBasketEvent)
        {

            Logger.LogInfo("Create request received");
            // for async processing of events using kafka
            //await Publisher.Publish(createBasketEvent);

            var result = await EventProcessor.ProcessCreateBascket(createBasketEvent);
            if (result != null)
                return Ok(result);
            return BadRequest();
        }
        [HttpPost("{id}/addArticle")]
        public ActionResult AddArticle(long id, [FromBody] AddArticleEvent addArticleEvent)
        {
            Logger.LogInfo($"Add article for basketId {id}");

            // for async processing of events using kafka
            //await Publisher.Publish(addArticleEvent);

            var result = EventProcessor.ProcessAddArticle(addArticleEvent, id);
            if (result) return Ok();
            return BadRequest();
        }

        // PUT api/<BasketsController>/5
        [HttpPut("{id}")]
        public ActionResult UpdateBasket(long id, [FromBody] UpdateBasketEvent updateBasketEvent)
        {

            Logger.LogInfo($"Update  basket {id}");

            // for async processing of events using kafka
            //await Publisher.Publish(updateBasketEvent);

            var result = EventProcessor.ProcessUpdateBasket(updateBasketEvent, id);
            if (result) return Ok();
            return BadRequest();
        }
    }
}
