using CsvHelper.Configuration;
using Domain.Models;

namespace Domain.ModelsDto
{
    public class StoreTransactionMap : ClassMap<StoreTransaction>
    {
        public StoreTransactionMap()
        {
            Map(m => m.GoodID).Name("Good ID");
            Map(m => m.TransactionID).Name("Transaction ID");
            Map(m => m.TransactionDate).Name("Transaction Date");
            Map(m => m.Amount).Name("Amount");
            Map(m => m.Direction).Name("Direction");
            Map(m => m.Comments).Name("Comments");
        }
    }
}
