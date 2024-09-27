using AspNetCore.Identity.MongoDbCore.Models;
using MongoDbGenericRepository.Attributes;
using SavingsBook.Application.Contracts.Common;

namespace SavingsBook.Application.Contracts.Authentication;

[CollectionName("Users")]
public class ApplicationUser : MongoIdentityUser<Guid>
{
    public string CustomerCode { get; set; }
    public Address Address { get; set; }
    public string FullName { get; set; }
    public string IdCardNumber { get; set; }

}