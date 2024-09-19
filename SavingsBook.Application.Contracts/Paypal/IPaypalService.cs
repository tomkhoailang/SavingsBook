using SavingsBook.Application.Contracts.Paypal.Dto;
using SavingsBook.Application.Contracts.Paypal.PaypalOrderDto;
using SavingsBook.Application.Contracts.Paypal.PaypalOrderDto.Request;

namespace SavingsBook.Application.Contracts.Paypal;

public interface IPayPalService
{
    Task<PayoutBatchResponse> SendPayoutAsync(PayoutRequest payoutRequest);
    Task<PayPalOrderResponse> CreateOrderAsync(CreateOrderRequest orderRequest);
    Task<PayPalCaptureResponse> CaptureOrderAsync(string orderId);
}