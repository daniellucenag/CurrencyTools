using Application;
using Application.CurrencyContext;
using Application.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.ComponentModel.DataAnnotations;
using System.Threading;
using System.Threading.Tasks;

namespace Api.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    [ApiVersion("1")]
    [SwaggerTag("Requests about Currency")]
    public class CurrencyController : ControllerBase
    {
        private readonly ICurrencyApplication currencyApplication;

        public CurrencyController(ICurrencyApplication currencyApplication)
        {
            this.currencyApplication = currencyApplication;
        }

        [HttpPost]
        [SwaggerResponse(StatusCodes.Status201Created, Type = typeof(Result), Description = "Created a currency")]
        [SwaggerResponse(StatusCodes.Status400BadRequest, Type = typeof(Result), Description = "Currency is not valid")]
        [SwaggerResponse(StatusCodes.Status409Conflict, description: "RequestId already exists")]
        [SwaggerOperation(Summary = "Accept a currency", Description = "Include new currency")]
        public async Task<IActionResult> Post([Required(AllowEmptyStrings = false, ErrorMessage = "Invalid requestId"), FromHeader(Name = "x-requestid")] string requestId,
            [FromBody] CurrencyRequestModel currencyRequest,
            CancellationToken ctx)
        {
            if (!Guid.TryParse(requestId, out Guid identified))
                return BadRequest(ResultWrapper.Error(nameof(requestId), "Invalid requestId").Result);

            var result = await currencyApplication.SendCurrency(currencyRequest, identified, ctx);

            return StatusCode(result.Status, result);
        }
    }
}
