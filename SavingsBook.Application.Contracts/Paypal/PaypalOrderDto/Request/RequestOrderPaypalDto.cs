using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace SavingsBook.Application.Contracts.Paypal.PaypalOrderDto.Request;


public class CaptureOrderRequest
{
    public string OrderId { get; set; }
}
public class InitOrderRequest
{
    [Required]
    public string SavingBookId { get; set; }
    [Required]
    public string Amount { get; set; }
}
public class CreateOrderRequest
{
    [JsonProperty("intent")] public string Intent { get; set; }

    [JsonProperty("purchase_units")] public List<PurchaseUnit> PurchaseUnits { get; set; }

    [JsonProperty("payment_source")] public PaymentSource PaymentSource { get; set; }
}

public class PurchaseUnit
{
    [JsonProperty("reference_id")] public string ReferenceId { get; set; }

    [JsonProperty("amount")] public Amount Amount { get; set; }
}

public class Amount
{
    [JsonProperty("currency_code")] public string CurrencyCode { get; set; }

    [JsonProperty("value")] public string Value { get; set; }
}



public class ExperienceContext
{
    [JsonProperty("payment_method_preference")]
    public string PaymentMethodPreference { get; set; }


    [JsonProperty("locale")] public string Locale { get; set; }

    [JsonProperty("shipping_preference")] public string ShippingPreference { get; set; }

    [JsonProperty("user_action")] public string UserAction { get; set; }

    [JsonProperty("return_url")] public string ReturnUrl { get; set; }

    [JsonProperty("cancel_url")] public string CancelUrl { get; set; }
}

public class PaymentSource
{
    [JsonProperty("paypal")] public Paypal Paypal { get; set; }
}
public class Paypal
{
    [JsonProperty("experience_context")] public ExperienceContext ExperienceContext { get; set; }
}
