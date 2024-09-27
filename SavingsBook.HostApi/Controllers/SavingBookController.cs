using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using SavingsBook.Application.Contracts.SavingBook;
using SavingsBook.Application.Contracts.SavingBook.Dto;

namespace SavingsBook.HostApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class SavingBookController : ControllerBase
{
    private readonly ISavingBookService _savingBookService;

    public SavingBookController(ISavingBookService savingBookService)
    {
        _savingBookService = savingBookService;
    }
    //for manager with cash
    [HttpPost]
    public async Task<IActionResult> CreateSavingBook([FromBody] CreateUpdateSavingBookDto input)
    {
        // var username = User.Identity.Name;
        // var username = "123456789aA@";
        //
        // if (string.IsNullOrEmpty(username))
        // {
        //     return NotFound();
        // }
        // var response = await _savingBookService.CreateWithUserDataAsync(input, username);
        var response = await _savingBookService.CreateAsync(input);
        return response != null ? Ok(response) : BadRequest();
    }
    //for manager with cash
    [HttpPost("Payment")]
    public async Task<IActionResult> CreateSavingBookWithPayment([FromBody] CreateUpdateSavingBookUserDto input)
    {
        // var username = User.Identity.Name;
        var username = "123456789aA@";
        //
        // if (string.IsNullOrEmpty(username))
        // {
        //     return NotFound();
        // }
        // var response = await _savingBookService.CreateWithUserDataAsync(input, username);
        var response = await _savingBookService.CreateWithUserDataAsync(input, username);
        return response != null ? Ok(response) : BadRequest();
    }
}