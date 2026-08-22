namespace AccountingSystem.NewModel.EFModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Inventory")]
    public partial class Inventory
    {
        public int id { get; set; }

        public int? measurementItemId { get; set; }

        public int? quantity { get; set; }

        public int? itemId { get; set; }

        public int? storeId { get; set; }

        public virtual Classify item { get; set; }

        public virtual MeasurementsItem MeasurementsItem { get; set; }

        public virtual Store Store { get; set; }
    }
}
