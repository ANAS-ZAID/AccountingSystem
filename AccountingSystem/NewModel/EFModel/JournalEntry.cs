namespace AccountingSystem.NewModel.EFModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class JournalEntry
    {
        public int id { get; set; }

        public int? transactionId { get; set; }

        public string transactionType { get; set; }

        public DateTime? transactionDate { get; set; }

        public int? accountId { get; set; }

        public int? currencyId { get; set; }

        public decimal? ExchangeRate { get; set; }

        public decimal? debit { get; set; }

        public decimal? credit { get; set; }

        public string description { get; set; }

        public int? employeeId { get; set; }

        public virtual ChartOfAccount Account { get; set; }

        public virtual Currency Currency { get; set; }

        public virtual Employee Employee { get; set; }
    }
}
