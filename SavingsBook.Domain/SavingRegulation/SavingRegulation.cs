using SavingsBook.Domain.Common;

namespace SavingsBook.Domain.SavingRegulation;

public class SavingRegulation : AuditedAggregateRoot<Guid>
{
    public double MinWithDrawValue { get; set; }
    public int  MinWithDrawDay { get; set; }
    public List<SavingType> SavingTypes { get; set; } = [];
    public class SavingType
    {
        public string Name { get; set; }
        public int Term { get; set; }
        public double InterestRate { get; set; }
    }
}