using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Cors;
using KatlaSport.Services.ProductManagement;
using KatlaSport.WebApi.CustomFilters;
using Microsoft.Web.Http;
using Swashbuckle.Swagger.Annotations;

namespace KatlaSport.WebApi.Controllers
{
    [ApiVersion("1.0")]
    [RoutePrefix("api/requests")]
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    [CustomExceptionFilter]
    [SwaggerResponseRemoveDefaults]
    public class StoreRequestController : ApiController
    {
        private readonly IProductStoreRequestService _productStoreRequestService;

        public StoreRequestController(IProductStoreRequestService productStoreRequestService)
        {
            _productStoreRequestService = productStoreRequestService ?? throw new ArgumentNullException(nameof(productStoreRequestService));
        }

        [HttpGet]
        [Route("")]
        [SwaggerResponse(HttpStatusCode.OK, Description = "Returns a store product requests.", Type = typeof(ProductStoreItemRequest))]
        [SwaggerResponse(HttpStatusCode.NotFound)]
        [SwaggerResponse(HttpStatusCode.InternalServerError)]
        public async Task<IHttpActionResult> GetRequest()
        {
            var request = await _productStoreRequestService.GetRequestsAsync();
            return Ok(request);
        }

        [HttpGet]
        [Route("{id:int:min(1)}")]
        [SwaggerResponse(HttpStatusCode.OK, Description = "Returns a store product request.", Type = typeof(ProductStoreItemRequest))]
        [SwaggerResponse(HttpStatusCode.NotFound)]
        [SwaggerResponse(HttpStatusCode.InternalServerError)]
        public async Task<IHttpActionResult> GetRequest([FromUri] int id)
        {
            var request = await _productStoreRequestService.GetRequestAsync(id);
            return Ok(request);
        }

        [HttpPost]
        [Route("")]
        [SwaggerResponse(HttpStatusCode.Created, Description = "Creates a new store product request.")]
        [SwaggerResponse(HttpStatusCode.BadRequest)]
        [SwaggerResponse(HttpStatusCode.Conflict)]
        [SwaggerResponse(HttpStatusCode.InternalServerError)]
        public async Task<IHttpActionResult> AddProductCategory([FromBody] UpdateRequestRequest createRequest)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var request = await _productStoreRequestService.CreateRequestAsync(createRequest);
            var location = string.Format("/api/requests/{0}", request.Id);
            return Created<ProductStoreItemRequest>(location, request);
        }

        [HttpPut]
        [Route("{id:int:min(1)}/completed")]
        [SwaggerResponse(HttpStatusCode.NoContent, Description = "Sets completed status for an existed product store item request.")]
        [SwaggerResponse(HttpStatusCode.NotFound)]
        [SwaggerResponse(HttpStatusCode.InternalServerError)]
        public async Task<IHttpActionResult> SetStatus([FromUri] int id)
        {
            await _productStoreRequestService.SetRequestCompletedAsync(id);
            return ResponseMessage(Request.CreateResponse(HttpStatusCode.NoContent));
        }
    }
}