namespace AccountingSystem.NewModel.EFModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class SimpleEntry
    {
        public int id { get; set; }

        public DateTime? entryDate { get; set; }

        public DateTime? updateDate { get; set; }

        public string description { get; set; }

        public int? debitAccount { get; set; }

        public int? creditAccount { get; set; }

        public decimal? amount { get; set; }

        public int? currencyId { get; set; }

        public int? employeeId { get; set; }

        public DateTime? date { get; set; }

        public int? brancheId { get; set; }

        public virtual Branch Branch { get; set; }

        public virtual ChartOfAccount AccountCredit { get; set; }

        public virtual ChartOfAccount AccountDebit { get; set; }

        public virtual Currency Currency { get; set; }

        public virtual Employee Employee { get; set; }
    }
}
