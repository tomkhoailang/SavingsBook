using SavingsBook.Application.Contracts.Common;

namespace SavingsBook.Application.Contracts.SavingBook.Dto;

public class CreateUpdateSavingBookDto
{
    public string AccountId { get; set; }
    public double Balance { get; set; }

}
public class CreateUpdateSavingBookUserDto
{
    public double Balance { get; set; }

}