using System.Linq;
using System.Net;
using System.Net.Http;
using Insurance.Api.Models;
using Insurance.Api.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Insurance.Api.Controllers
{
    [Route("api/[controller]")]
    public partial class InsuranceController : Controller
    {
        private readonly ILogger _logger;
        private readonly IInsuranceCalculator _insuranceCalculator;
        private readonly ISurchargeService _surchargeService;

        public InsuranceController(ILogger<InsuranceController> logger,
            IInsuranceCalculator insuranceCalculator,
            ISurchargeService surchargeService)
        {
            _logger = logger;
            _insuranceCalculator = insuranceCalculator;
            _surchargeService = surchargeService;
        }

        [Route("")]
        public string Index()
        {
            return "Hello from the Insurance.API";
        }

        [HttpPost]
        [Route("product")]
        public InsuranceDto CalculateInsurance([FromBody] InsuranceDto toInsure)
        {
            try
            {
                toInsure = _insuranceCalculator.Calculate(toInsure.ProductId);
                return toInsure;
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex, default);
                return null; //Should make Reponse classes same as the Microsoft Azure model so you can provide details around the exception
            }
        }

        [HttpPost]
        [Route("order")]
        public OrderDto CalculateInsuranceOrder([FromBody] OrderDto orderToInsure)
        {
            try
            {
                orderToInsure = _insuranceCalculator.Calculate(orderToInsure.ProductsToInsure.Select(p => p.ProductId));
                return orderToInsure;
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex, default);
                return null; //Should make Reponse classes same as the Microsoft Azure model so you can provide details around the exception
            }
        }

        [HttpPost]
        [Route("addsurcharge")]
        public IActionResult AddSurcharge([FromBody] SurchargeDto surchargeDto)
        {
            try
            {
                _surchargeService.AddSurcharge(surchargeDto);
                return Ok();
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex, default);
                return StatusCode(500);
            }

        }

    }
}