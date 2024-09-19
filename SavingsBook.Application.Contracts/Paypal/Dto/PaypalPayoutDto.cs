using Newtonsoft.Json;

namespace SavingsBook.Application.Contracts.Paypal.Dto;


#region Request
public class PayoutRequest
{
    [JsonProperty("sender_batch_header")] public SenderBatchHeader SenderBatchHeader { get; set; }

    [JsonProperty("items")] public List<PayoutItem> Items { get; set; }
}



public class PayoutItem
{
    [JsonProperty("recipient_type")] public string RecipientType { get; set; }

    [JsonProperty("amount")] public PayoutAmount Amount { get; set; }

    [JsonProperty("note")] public string Note { get; set; }

    [JsonProperty("sender_item_id")] public string SenderItemId { get; set; }

    [JsonProperty("receiver")] public string Receiver { get; set; }

    [JsonProperty("notification_language")]
    public string NotificationLanguage { get; set; }

    [JsonProperty("purpose")] public string Purpose { get; set; }
}

public class PayoutAmount
{
    [JsonProperty("value")] public string Value { get; set; }

    [JsonProperty("currency")] public string Currency { get; set; }
}

#endregion


#region Response
public class BatchHeader
{
    [JsonProperty("sender_batch_header")]
    public SenderBatchHeader SenderBatchHeader { get; set; }

    [JsonProperty("payout_batch_id")]
    public string PayoutBatchId { get; set; }

    [JsonProperty("batch_status")]
    public string BatchStatus { get; set; }
}

public class PayoutBatchResponse
{
    [JsonProperty("batch_header")]
    public BatchHeader BatchHeader { get; set; }
}


#endregion

#region Shared

public class SenderBatchHeader
{
    [JsonProperty("sender_batch_id")] public string SenderBatchId { get; set; }

    [JsonProperty("email_subject")] public string EmailSubject { get; set; }

    [JsonProperty("email_message")] public string EmailMessage { get; set; }
}

#endregion