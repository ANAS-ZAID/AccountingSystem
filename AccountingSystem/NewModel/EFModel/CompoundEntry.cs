namespace AccountingSystem.NewModel.EFModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class CompoundEntry
    {
        public int id { get; set; }

        public DateTime? date { get; set; }

        public DateTime? entryDate { get; set; }

        public DateTime? updateDate { get; set; }

        [StringLength(255)]
        public string description { get; set; }

        public string type { get; set; }

        public decimal? debitTotal { get; set; }

        public decimal? creditTotal { get; set; }

        public int? currencyId { get; set; }

        public int? employeeId { get; set; }

        public int? brancheId { get; set; }

        public virtual Branch Branch { get; set; }

        public virtual Currency Currency { get; set; }

        public virtual Employee Employee { get; set; }
    }
}
