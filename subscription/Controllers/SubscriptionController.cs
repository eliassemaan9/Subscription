using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using subscription.models.DTO;
using subscription.models.Models;
using subscription.repositories.IRepositories;
using subscription.services.IServices;

namespace subscription.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/subscription")]
    public class SubscriptionController : ControllerBase
    {
        private ISubscriptionService _subscriptionService;
        public SubscriptionController(ISubscriptionService subscriptionService) 
        {
            _subscriptionService = subscriptionService;
        }

        /// <summary>
        /// Add new subscription
        /// </summary>
        /// «remarks>
        /// body input parameters are required to call this API
        /// </remarks> 
        /// <response code="200">Success</response> 
        /// «response code="400">Bad Request</response> 
        /// «response code="500"> Internal Server Error</responses / «response code-"401">Unauthorized</responses
        [ProducesResponseType(200, Type = typeof(SubscriptionDTO))]
        [ProducesResponseType(400, Type = typeof(BadRequestObjectResult))]
        [ProducesResponseType(401, Type = typeof(UnauthorizedObjectResult))]
        [HttpPost]
        public IActionResult AddSubscription([FromBody] SubscriptionDTO subscription)
        {
            return Ok(_subscriptionService.Add(subscription));
        }
        /// <summary>
        /// Update subscription
        /// </summary>
        /// «remarks>
        /// body input parameters are required to call this API
        /// </remarks> 
        /// <response code="200">Success</response> 
        /// «response code="400">Bad Request</response> 
        /// «response code="500"> Internal Server Error</responses / «response code-"401">Unauthorized</responses
        [ProducesResponseType(200, Type = typeof(SubscriptionDTO))]
        [ProducesResponseType(400, Type = typeof(BadRequestObjectResult))]
        [ProducesResponseType(401, Type = typeof(UnauthorizedObjectResult))]
        [HttpPut]
        public IActionResult UpdateSubscription([FromBody] SubscriptionDTO subscription)
        {
            return Ok(_subscriptionService.update(subscription));
        }
        /// <summary>
        /// this Api return list of subscription related to a user
        /// </summary>
        /// «remarks>
        /// The parameter is required to call this API
        /// </remarks> 
        /// <response code="200">Success</response> 
        /// «response code="400">Bad Request</response> 
        /// «response code="500"> Internal Server Error</responses / «response code-"401">Unauthorized</responses
        [ProducesResponseType(200, Type = typeof(List<Subscription>))]
        [ProducesResponseType(400, Type = typeof(BadRequestObjectResult))]
        [ProducesResponseType(401, Type = typeof(UnauthorizedObjectResult))]
        [HttpGet]
        [Route("byUserId")]
        public IActionResult GetSubscription(long userId)
        {
            return Ok(_subscriptionService.GetByUserId(userId));
        }
        /// <summary>
        /// This Api get the last subscription for a user in a specific product.
        /// </summary>
        /// «remarks>
        /// The parameters are required to call this API
        /// </remarks> 
        /// <response code="200">Success</response> 
        /// «response code="400">Bad Request</response> 
        /// «response code="500"> Internal Server Error</responses / «response code-"401">Unauthorized</responses
        [ProducesResponseType(200, Type = typeof(Subscription))]
        [ProducesResponseType(400, Type = typeof(BadRequestObjectResult))]
        [ProducesResponseType(401, Type = typeof(UnauthorizedObjectResult))]
        [HttpGet]
        [Route("byUserIdAndProductId")]
        public IActionResult GetSubscriptionByProductAndUserId(long userId,long productId)
        {
            return Ok(_subscriptionService.GetSubscriptionByUserIdProductId(productId,userId));
        }
    }
}
