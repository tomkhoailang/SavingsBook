using System.Net.Http.Headers;
using System.Text;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using SavingsBook.Application.Contracts.Paypal;
using SavingsBook.Application.Contracts.Paypal.Dto;
using SavingsBook.Application.Contracts.Paypal.PaypalOrderDto;
using SavingsBook.Application.Contracts.Paypal.PaypalOrderDto.Request;
using Amount = SavingsBook.Application.Contracts.Paypal.PaypalOrderDto.Request.Amount;
using PaymentSource = SavingsBook.Application.Contracts.Paypal.PaypalOrderDto.Request.PaymentSource;
using PurchaseUnit = SavingsBook.Application.Contracts.Paypal.PaypalOrderDto.Request.PurchaseUnit;

namespace SavingsBook.Application.Paypal;

public class PaypalService: IPayPalService
{
    private readonly HttpClient _httpClient;
    private readonly string _clientId;
    private readonly string _clientSecret;

    public PaypalService(HttpClient httpClient, IConfiguration configuration)
    {
        _httpClient = httpClient;
        _clientId = configuration["PayPal:ClientId"];
        _clientSecret = configuration["PayPal:ClientSecret"];
    }


    public async Task<PayoutBatchResponse> SendPayoutAsync(PayoutRequest payoutRequest)
    {
        var token = await GetPayPalTokenAsync();
        _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
        var payload = new PayoutRequest
        {
            SenderBatchHeader = new SenderBatchHeader
            {
                SenderBatchId = $"PayoutsBatch_{DateTime.Now:yyyyMMddHHmmss}_{Guid.NewGuid()}",
                EmailSubject = "You have a payout!",
                EmailMessage = "You have received a payout! Thanks for using our service!"
            },
            Items =
            [
                new PayoutItem
                {
                    RecipientType = "EMAIL",
                    Amount = new PayoutAmount() { Value = "9.87", Currency = "USD" },
                    Note = "Thanks for your patronage!",
                    SenderItemId = $"PayoutsItem_{DateTime.Now:yyyyMMddHHmmss}_{Guid.NewGuid()}",
                    Receiver = "sb-cji7p32764589@personal.example.com",
                    NotificationLanguage = "en-US"
                }
            ]
        };

        string jsonPayload = JsonConvert.SerializeObject(payload);

        // Create the HTTP content with the JSON payload
        var content = new StringContent(jsonPayload, Encoding.UTF8, "application/json");

        var response = await _httpClient.PostAsync("https://api-m.sandbox.paypal.com/v1/payments/payouts", content);

        var responseString = await response.Content.ReadAsStringAsync();
        return JsonConvert.DeserializeObject<PayoutBatchResponse>(responseString);
    }

    public async Task<PayPalOrderResponse> CreateOrderAsync(InitOrderRequest input)
    {
        var token = await GetPayPalTokenAsync();
        _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

        var payload = new CreateOrderRequest()
        {
            Intent = "CAPTURE",
            PurchaseUnits =
            [
                new PurchaseUnit
                {
                    ReferenceId = input.SavingBookId,
                    Amount = new Amount { CurrencyCode = "USD", Value = input.Amount }
                }
            ],
            PaymentSource = new PaymentSource
            {
                Paypal = new Contracts.Paypal.PaypalOrderDto.Request.Paypal
                {
                    ExperienceContext = new ExperienceContext
                    {
                        PaymentMethodPreference = "IMMEDIATE_PAYMENT_REQUIRED",
                        Locale = "en-US",
                        ShippingPreference = "NO_SHIPPING",
                        UserAction = "PAY_NOW",
                        ReturnUrl = "https://example.com/returnUrl",
                        CancelUrl = "https://example.com/cancelUrl"
                    }
                }
            }
        };

        string jsonPayload = Newtonsoft.Json.JsonConvert.SerializeObject(payload);

        // Create the HTTP content with the JSON payload
        var content = new StringContent(jsonPayload, Encoding.UTF8, "application/json");

        var response = await _httpClient.PostAsync("https://api-m.sandbox.paypal.com/v2/checkout/orders", content);

        var responseString = await response.Content.ReadAsStringAsync();
        return JsonConvert.DeserializeObject<PayPalOrderResponse>(responseString);

    }

    public async Task<PayPalCaptureResponse> CaptureOrderAsync(string orderId)
    {
        var token = await GetPayPalTokenAsync();
        _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);


        // Create the HTTP content with the JSON payload
        var content = new StringContent("{}", Encoding.UTF8, "application/json");

        var response = await _httpClient.PostAsync($"https://api-m.sandbox.paypal.com/v2/checkout/orders/{orderId}/capture", content);


        var responseString = await response.Content.ReadAsStringAsync();
        return JsonConvert.DeserializeObject<PayPalCaptureResponse>(responseString);
    }

    private async Task<string> GetPayPalTokenAsync()
    {
        var authToken = Encoding.ASCII.GetBytes($"{_clientId}:{_clientSecret}");
        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(authToken));

        var content = new StringContent("grant_type=client_credentials", Encoding.UTF8, "application/x-www-form-urlencoded");

        var response = await _httpClient.PostAsync("https://api-m.sandbox.paypal.com/v1/oauth2/token", content);
        var responseContent = await response.Content.ReadAsStringAsync();

        if (response.IsSuccessStatusCode)
        {
            var tokenResult = JsonConvert.DeserializeObject<PayPalTokenResponse>(await response.Content.ReadAsStringAsync());
            return tokenResult.AccessToken;
        }
        else
        {
            throw new Exception($"Error getting PayPal access token: {responseContent}");
        }
    }
}