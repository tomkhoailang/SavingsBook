using System.Net.Http.Headers;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SavingsBook.Application.Contracts.Paypal;
using SavingsBook.Application.Contracts.Paypal.Dto;
using SavingsBook.Application.Contracts.Paypal.PaypalOrderDto;
using SavingsBook.Application.Contracts.Paypal.PaypalOrderDto.Request;

namespace SavingsBook.HostApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class PaymentController : ControllerBase
{

    private readonly IPayPalService _payPalService;
    public PaymentController(IPayPalService payPalService)
    {
        _payPalService = payPalService;
    }

    [HttpPost]
    [Route("send-amount")]
    public async Task<IActionResult> SendPayoutAsync()
    {
        var response = await _payPalService.SendPayoutAsync(new());
        return Ok(response);
    }
    [HttpPost]
    [Route("create-order")]
    public async  Task<IActionResult> CreateOrder()
    {
        var response = await _payPalService.CreateOrderAsync(new());
        return Ok(response);

    }
    [HttpPost]
    [Route("capture-order")]
    public async  Task<IActionResult> CaptureOrder(CaptureOrderRequest input)
    {
        var response = await _payPalService.CaptureOrderAsync(input.OrderId);
        return Ok(response);
    }







}