using System.Linq.Expressions;
using AutoMapper;
using SavingsBook.Application.Contracts.Common;
using SavingsBook.Application.Contracts.SavingRegulation;
using SavingsBook.Application.Contracts.SavingRegulation.Dto;
using SavingsBook.Domain.Common;
using SavingsBook.Domain.SavingRegulation;

namespace SavingsBook.Application.SavingRegulationAppService;

public class SavingRegulationService : ISavingRegulationService
{
    private readonly IMapper _mapper;

    private readonly IRepository<SavingRegulation> _savingRegulationRepository;

    public SavingRegulationService(IRepository<SavingRegulation> savingRegulationRepository, IMapper mapper)
    {
        _savingRegulationRepository = savingRegulationRepository;
        _mapper = mapper;
    }

    public async Task<ResponseDto<SavingRegulationDto>> CreateAsync(CreateUpdateSavingRegulationDto input)
    {
        var entity = _mapper.Map<SavingRegulation>(input);
        await _savingRegulationRepository.InsertAsync(entity);
        return new ResponseDto<SavingRegulationDto>().SetSuccess(_mapper.Map<SavingRegulationDto>(entity),201);
    }

    public async Task<ResponseDto<SavingRegulationDto>> UpdateAsync(Guid id, CreateUpdateSavingRegulationDto input)
    {
        var entity = await _savingRegulationRepository.FirstOrDefaultAsync(n => n.Id == id);
        if (entity == null)
        {
            return new ResponseDto<SavingRegulationDto>().SetFailure(404, "Saving regulation record not found");
        }

        _mapper.Map(input, entity);
        await _savingRegulationRepository.UpdateAsync(entity);

        return new ResponseDto<SavingRegulationDto>().SetSuccess(_mapper.Map<SavingRegulationDto>(entity));
    }

    public Task<ResponseDto<SavingRegulationDto>> GetAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    public async Task<ResponseDto<PageResultDto<SavingRegulationDto>>> GetListAsync(QuerySavingRegulationDto input,
        CancellationToken cancellationToken)
    {
        Expression<Func<SavingRegulation, bool>> query = x => true;

        var totalCount = await _savingRegulationRepository.CountAsync(query);
        var items = (await _savingRegulationRepository.GetQueryableAsync())
            .Skip(input.SkipCount)
            .Take(input.MaxResultCount > 0 ? input.MaxResultCount : 25)
            .ToList()
            .Select(n => _mapper.Map<SavingRegulationDto>(n))
            .ToList();


        return new ResponseDto<PageResultDto<SavingRegulationDto>>().SetSuccess(
            new PageResultDto<SavingRegulationDto>() { Items = items, TotalCount = totalCount });

    }

    public async Task<ResponseDto<bool>> DeleteAsync(Guid id)
    {
        var result = await _savingRegulationRepository.DeleteAsync(id);
        if (!result)
        {
            return new ResponseDto<bool>().SetFailure(404, "Saving regulation record not found");
        }

        return new ResponseDto<bool>().SetSuccess(result, 204);
    }
}