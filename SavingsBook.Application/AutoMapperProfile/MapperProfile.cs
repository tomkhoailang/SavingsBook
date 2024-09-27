using AutoMapper;
using SavingsBook.Application.Contracts.Common;
using SavingsBook.Application.Contracts.SavingBook.Dto;
using SavingsBook.Application.Contracts.SavingRegulation.Dto;
using SavingsBook.Domain.SavingBook;
using SavingsBook.Domain.SavingRegulation;

namespace SavingsBook.Application.AutoMapperProfile;

public class MapperProfile : Profile
{
    public MapperProfile()
    {
        #region Saving regulations

        CreateMap<CreateUpdateSavingRegulationDto, SavingRegulation>();
        CreateMap<CreateUpdateSavingRegulationDto.SavingType, SavingRegulation.SavingType>();

        CreateMap<SavingRegulation, SavingRegulationDto>();
        CreateMap<SavingRegulation.SavingType, SavingRegulationDto.SavingType>();

        #endregion

        #region Saving book

        CreateMap<CreateUpdateSavingBookDto, SavingBook>();

        CreateMap<SavingBook, SavingBookDto>();
        CreateMap<SavingBook.Regulation, SavingBookDto.Regulation>();
        CreateMap<SavingBook.TransactionTicket, SavingBookDto.TransactionTicket>();

        CreateMap<SavingBook, SavingBookWithPaymentUrlDto>();



        #endregion

        CreateMap<Address, Domain.Common.Address>().ReverseMap();



    }
}