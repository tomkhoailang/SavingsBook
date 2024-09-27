using AspNetCore.Identity.MongoDbCore.Models;
using MongoDbGenericRepository.Attributes;
using SavingsBook.Application.Contracts.Common;

namespace SavingsBook.Infrastructure.Authentication;

[CollectionName("Users")]
public class ApplicationUser : MongoIdentityUser<Guid>
{
    public string CustomerCode { get; set; }
    public string IdCardNumber { get; set; }
    public Address Address { get; set; }

}