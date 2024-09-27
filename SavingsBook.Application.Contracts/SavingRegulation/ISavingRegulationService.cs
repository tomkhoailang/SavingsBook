﻿using SavingsBook.Application.Contracts.Common;
using SavingsBook.Application.Contracts.SavingBook.Dto;
using SavingsBook.Application.Contracts.SavingRegulation.Dto;

namespace SavingsBook.Application.Contracts.SavingRegulation;

public interface ISavingRegulationService : ICrudService<SavingRegulationDto,Guid, QuerySavingRegulationDto,CreateUpdateSavingRegulationDto,CreateUpdateSavingRegulationDto>
{
}