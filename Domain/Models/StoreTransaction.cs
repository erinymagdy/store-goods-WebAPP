using CsvHelper.Configuration;
using CsvHelper.Configuration.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class StoreTransaction
    {
        public string GoodID { get; set; }
        public string TransactionID { get; set; }
        public string TransactionDate { get; set; }
        public string Amount { get; set; }
        public string Direction { get; set; }
        public string Comments { get; set; }
    }
}