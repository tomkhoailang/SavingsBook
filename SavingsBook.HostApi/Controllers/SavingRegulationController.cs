using Microsoft.AspNetCore.Mvc;
using SavingsBook.Application.Contracts.SavingRegulation;
using SavingsBook.Application.Contracts.SavingRegulation.Dto;
using SavingsBook.HostApi.Utility;

namespace SavingsBook.HostApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class SavingRegulationController : ControllerBase
{
    private readonly ISavingRegulationService _savingRegulationService;

    public SavingRegulationController(ISavingRegulationService savingRegulationService)
    {
        _savingRegulationService = savingRegulationService;
    }

    [HttpPost]
    public async Task<IActionResult> CreateSavingRegulation([FromBody] CreateUpdateSavingRegulationDto input)
    {
        var response = await _savingRegulationService.CreateAsync(input);
        return ResponseHelper.FormatResponse(response);
    }

    [HttpPut]
    public async Task<IActionResult> PutSavingRegulation([FromQuery] Guid id,
        [FromBody] CreateUpdateSavingRegulationDto input)
    {
        var response = await _savingRegulationService.UpdateAsync(id, input);
        return ResponseHelper.FormatResponse(response);
    }

    [HttpGet]
    public async Task<IActionResult> GetSavingRegulationList([FromQuery] QuerySavingRegulationDto input, CancellationToken cancellationToken)
    {
        var response = await _savingRegulationService.GetListAsync(input, cancellationToken);
        return ResponseHelper.FormatResponse(response);
    }

    [HttpDelete]
    public async Task<IActionResult> DeleteSavingRegulation([FromQuery] Guid id)
    {
        var response = await _savingRegulationService.DeleteAsync(id);
        return ResponseHelper.FormatResponse(response);
    }
}