﻿using System.Globalization;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using SavingsBook.Application.Contracts.Common;
using SavingsBook.Application.Contracts.Paypal;
using SavingsBook.Application.Contracts.Paypal.PaypalOrderDto.Request;
using SavingsBook.Application.Contracts.SavingBook;
using SavingsBook.Application.Contracts.SavingBook.Dto;
using SavingsBook.Domain.Common;
using SavingsBook.Domain.SavingBook;
using SavingsBook.Infrastructure.Authentication;
using Address = SavingsBook.Domain.Common.Address;

namespace SavingsBook.Application.SavingBookAppService;

public class SavingBookService : ISavingBookService
{
    private readonly IRepository<SavingBook> _savingBookRepository;
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly IMapper _mapper;
    private readonly IPayPalService _payPalService;

    public SavingBookService(IMapper mapper, UserManager<ApplicationUser> userManager, IRepository<SavingBook> savingBookRepository, IPayPalService payPalService)
    {
        _mapper = mapper;
        _userManager = userManager;
        _savingBookRepository = savingBookRepository;
        _payPalService = payPalService;
    }

    //for manager with cash
    public async Task<SavingBookDto> CreateAsync(CreateUpdateSavingBookDto input)
    {
        var user = await _userManager.FindByIdAsync(input.AccountId);
        if (user == null)
            throw new ApplicationException($"User  not found");
        var entity = _mapper.Map<SavingBook>(input);
        entity.Address = _mapper.Map<Address>(user.Address);
        entity.AccountId = user.Id;
        entity.IdCardNumber = user.IdCardNumber;
        await _savingBookRepository.InsertAsync(entity);

        return _mapper.Map<SavingBookDto>(entity);
    }
    //for user with paypal
    public async Task<SavingBookWithPaymentUrlDto> CreateWithUserDataAsync (CreateUpdateSavingBookUserDto input, string name)
    {

        var user = await _userManager.FindByNameAsync(name);
        if (user == null)
            throw new ApplicationException($"User {name} not found");
        var entity = new SavingBook
        {
            Id = Guid.NewGuid(),
            Address = _mapper.Map<Address>(user.Address),
            AccountId = user.Id,
            IdCardNumber = user.IdCardNumber,
            IsActive = false
        };

        try
        {
            var insertTask = _savingBookRepository.InsertAsync(entity);
            var paymentTask = _payPalService.CreateOrderAsync(new InitOrderRequest()
            {
                Amount = input.Balance.ToString(CultureInfo.InvariantCulture),
                SavingBookId = entity.Id.ToString()
            });

            await Task.WhenAll(insertTask, paymentTask);

            var response = _mapper.Map<SavingBookWithPaymentUrlDto>(entity);
            response.PaymentUrl = paymentTask.Result.Links.LastOrDefault().Href;

            return response;
        }
        catch (Exception ex)
        {
            if (entity.Id != Guid.Empty)
            {
                await _savingBookRepository.DeleteAsync(entity.Id);
            }

            throw new ApplicationException("Failed to create SavingBook with payment", ex);
        }
    }


    public Task<SavingBookDto> UpdateAsync(Guid id, CreateUpdateSavingBookDto input)
    {
        throw new NotImplementedException();
    }

    public Task<SavingBookDto> GetAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    public Task<PageResultDto<SavingBookDto>> GetListAsync(QuerySavingBookDto input, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task<bool> DeleteAsync(Guid id)
    {
        throw new NotImplementedException();
    }
}