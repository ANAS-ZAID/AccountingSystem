namespace AccountingSystem.NewModel.EFModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Trade
    {
        public int id { get; set; }

        public string type { get; set; }

        public string description { get; set; }

        [Column(TypeName = "text")]
        public string date { get; set; }

        public int? currencyFromId { get; set; }

        public int? currencyToId { get; set; }

        public decimal? conversionPrice { get; set; }

        public int? accountId { get; set; }

        public int? cashierId { get; set; }

        public int? employeeId { get; set; }

        public virtual Cashier Cashier { get; set; }

        public virtual ChartOfAccount ChartOfAccount { get; set; }

        public virtual Currency Currency { get; set; }

        public virtual Currency Currency1 { get; set; }

        public virtual Employee Employee { get; set; }
    }
}
