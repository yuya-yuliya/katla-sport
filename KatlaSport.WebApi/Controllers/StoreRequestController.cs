using System;
using System.Net;
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
    }
}