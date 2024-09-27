using SavingsBook.Application.Contracts.Common;

namespace SavingsBook.Application.Contracts.SavingBook.Dto;

public class SavingBookDto
{
    public Guid AccountId { get; set; }
    public Address Address { get; set; }
    public string IdCardNumber { get; set; }
    public string OwnerName { get; set; }
    public List<Regulation> Regulations { get; set; } = [];
    public List<TransactionTicket> TransactionTickets { get; set; } = [];
    public double Balance { get; set; }
    public string Status { get; set; }

    public class Regulation
    {
        public Guid RegulationIdRef { get; set; }
        public DateTime ApplyDate { get; set; }
        public string Name { get; set; }
        public int TermInMonth { get; set; }
        public double InterestRate { get; set; }
        public double MinWithDrawValue { get; set; }
        public int MinWithDrawDay { get; set; }
    }

    public class TransactionTicket
    {
        public Guid TransactionTicketId { get; set; }
        public Guid SavingBookId { get; set; }
        public DateTime TransactionDate { get; set; }
        public string TransactionType { get; set; }
        public string TransactionMethod { get; set; }
        public string AttachedEmail { get; set; }
        public double Amount { get; set; }
    }
}

public class SavingBookWithPaymentUrlDto : SavingBookDto
{
    public string PaymentUrl {get; set;}

}
