namespace AccountingSystem.NewModel.EFModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("BeginningInventory")]
    public partial class BeginningInventory
    {
        public int id { get; set; }

        public int? measurementItemId { get; set; }

        public int? itemId { get; set; }

        public int? employeeId { get; set; }

        public int? storeId { get; set; }

        public int? brancheId { get; set; }

        public decimal? quantity { get; set; }

        public decimal? unitPrice { get; set; }

        public DateTime? enteryDate { get; set; }

        public DateTime? updateDate { get; set; }

        public virtual Branch Branch { get; set; }

        public virtual Employee Employee { get; set; }

        public virtual Classify item { get; set; }

        public virtual MeasurementsItem MeasurementsItem { get; set; }

        public virtual Store Store { get; set; }
    }
}
