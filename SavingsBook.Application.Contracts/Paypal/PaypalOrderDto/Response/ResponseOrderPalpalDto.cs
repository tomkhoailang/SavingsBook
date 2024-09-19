using Newtonsoft.Json;

namespace SavingsBook.Application.Contracts.Paypal.PaypalOrderDto;



public class PayPalOrderResponse
{
    [JsonProperty("id")]
    public string Id { get; set; }

    [JsonProperty("status")]
    public string Status { get; set; }

    [JsonProperty("payment_source")]
    public PaymentSource PaymentSource { get; set; }

    [JsonProperty("links")]
    public List<Link> Links { get; set; }
}

public class PayPalCaptureResponse
{
    [JsonProperty("id")]
    public string Id { get; set; }

    [JsonProperty("status")]
    public string Status { get; set; }

    [JsonProperty("payment_source")]
    public PaymentSource PaymentSource { get; set; }

    [JsonProperty("purchase_units")]
    public List<PurchaseUnit> PurchaseUnits { get; set; }

    [JsonProperty("payer")]
    public Payer Payer { get; set; }

    [JsonProperty("links")]
    public List<Link> Links { get; set; }
}

public class PaymentSource
{
    [JsonProperty("paypal")]
    public PayPal PayPal { get; set; }
}

public class PayPal
{
    [JsonProperty("email_address")]
    public string EmailAddress { get; set; }

    [JsonProperty("account_id")]
    public string AccountId { get; set; }

    [JsonProperty("account_status")]
    public string AccountStatus { get; set; }

    [JsonProperty("name")]
    public Name Name { get; set; }

    [JsonProperty("address")]
    public Address Address { get; set; }
}

public class Name
{
    [JsonProperty("given_name")]
    public string GivenName { get; set; }

    [JsonProperty("surname")]
    public string Surname { get; set; }
}

public class Address
{
    [JsonProperty("country_code")]
    public string CountryCode { get; set; }
}

public class PurchaseUnit
{
    [JsonProperty("reference_id")]
    public string ReferenceId { get; set; }

    [JsonProperty("payments")]
    public Payments Payments { get; set; }
}

public class Payments
{
    [JsonProperty("captures")]
    public List<Capture> Captures { get; set; }
}

public class Capture
{
    [JsonProperty("id")]
    public string Id { get; set; }

    [JsonProperty("status")]
    public string Status { get; set; }

    [JsonProperty("amount")]
    public Amount Amount { get; set; }

    [JsonProperty("final_capture")]
    public bool FinalCapture { get; set; }

    [JsonProperty("seller_protection")]
    public SellerProtection SellerProtection { get; set; }

    [JsonProperty("seller_receivable_breakdown")]
    public SellerReceivableBreakdown SellerReceivableBreakdown { get; set; }

    [JsonProperty("links")]
    public List<Link> Links { get; set; }

    [JsonProperty("create_time")]
    public string CreateTime { get; set; }

    [JsonProperty("update_time")]
    public string UpdateTime { get; set; }
}

public class Amount
{
    [JsonProperty("currency_code")]
    public string CurrencyCode { get; set; }

    [JsonProperty("value")]
    public string Value { get; set; }
}

public class SellerProtection
{
    [JsonProperty("status")]
    public string Status { get; set; }

    [JsonProperty("dispute_categories")]
    public List<string> DisputeCategories { get; set; }
}

public class SellerReceivableBreakdown
{
    [JsonProperty("gross_amount")]
    public Amount GrossAmount { get; set; }

    [JsonProperty("paypal_fee")]
    public Amount PaypalFee { get; set; }

    [JsonProperty("net_amount")]
    public Amount NetAmount { get; set; }
}

public class Link
{
    [JsonProperty("href")]
    public string Href { get; set; }

    [JsonProperty("rel")]
    public string Rel { get; set; }

    [JsonProperty("method")]
    public string Method { get; set; }
}

public class Payer
{
    [JsonProperty("name")]
    public Name Name { get; set; }

    [JsonProperty("email_address")]
    public string EmailAddress { get; set; }

    [JsonProperty("payer_id")]
    public string PayerId { get; set; }

    [JsonProperty("address")]
    public Address Address { get; set; }
}
