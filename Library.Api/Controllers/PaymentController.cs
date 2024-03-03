using Library.Application.Dtos.PaymentDtos;
using Library.Application.Interfaces;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using PayPalCheckoutSdk.Core;
using PayPalCheckoutSdk.Orders;
using RestSharp;
using Library.Application.Dtos;
using Microsoft.IdentityModel.Tokens;
using Azure.Core;

namespace Library.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PaymentController : ControllerBase
    {
        private readonly string clientId = "AbvDdHQtUTfH7mBoMkGnyI0pbGjIhvTtChhGXdSvQkMvHGAJ_ZS25O7W5syvaxRA0V_YnfClaPC5degX";
        private readonly string clientSecret = "EA6KcRrT-OU2LxiegPnjpqTrDryo-eIcITd6GAuvT851uQKEuikoADMBBTZzcEmmiifsVQygtaQ-c0fw";
        private readonly HttpClient _httpClient;

        public PaymentController()
        {

        }
        [HttpPost]
        public async Task<IActionResult> ValidateCreditCard([FromBody]PaymentDto paymentDto)
        {
            string bin = paymentDto.CardNumber.Substring(0, 6);
            var client = new RestClient("https://lookup.binlist.net/" + bin);
            var request = new RestRequest(Method.GET);

            IRestResponse response = client.Execute(request);

            var content = JsonConvert.DeserializeObject<JToken>(response.Content)?.ToObject<BinlistResponse>();

            return Ok(content.Bank.Name);

            //return !string.IsNullOrEmpty(binlistResponse.Bank);
        }


        [HttpGet("create-payment")]
        public async Task<ActionResult> CreatePayment()
        {
            PayPalEnvironment environment = new SandboxEnvironment(clientId, clientSecret);
            PayPalHttpClient client = new PayPalHttpClient(environment);

            var request = new OrdersCreateRequest();
            request.Prefer("return=representation");
            request.RequestBody(BuildRequestBody());

            var response = await client.Execute(request);
            var result = response.Result<Order>();

            return Ok($"https://www.sandbox.paypal.com/checkoutnow?token={result.Id}");
        }
        private OrderRequest BuildRequestBody()
        {
            var request = new OrderRequest()
            {
                CheckoutPaymentIntent = "CAPTURE",
                PurchaseUnits = new List<PurchaseUnitRequest>()
                {
                    new PurchaseUnitRequest()
                    {
                        AmountWithBreakdown = new AmountWithBreakdown()
                        {
                            CurrencyCode = "USD",
                            Value = "100.00"
                        }
                    }
                }
            };

            return request;
        }
        [HttpGet("capture-payment/{orderId}")]
        public async Task<IActionResult> CapturePayment(string orderId)
        {
            PayPalEnvironment environment = new SandboxEnvironment(clientId, clientSecret);
            PayPalHttpClient client = new PayPalHttpClient(environment);

            var request = new OrdersCaptureRequest(orderId); // Replace orderId with the actual order ID
            request.RequestBody(new OrderActionRequest());

            var response = await client.Execute(request);
            var result = response.Result<Order>();
            return Ok(result.Status);
        }
        [HttpGet("/{orderId}")]
        public async Task<IActionResult> GetOrderInfo(string orderId)
        {
            return Ok($"https://api.sandbox.paypal.com/v2/checkout/orders/{orderId}");
        }
    }
}
